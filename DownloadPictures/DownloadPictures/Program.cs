using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace DownloadPictures
{
    public class UnDisplayedBrowser : WebBrowser
    {
        bool isCompleted = false;

        TimeSpan timeout = new TimeSpan(0, 0, 10);

        protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url == this.Url)
                isCompleted = true;
        }

        protected override void OnNewWindow(CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public UnDisplayedBrowser()
        {
            this.ScriptErrorsSuppressed = true;
        }

        public bool NavigateAndWait(string url)
        {
            base.Navigate(url);

            isCompleted = false;
            DateTime start = DateTime.Now;

            while (isCompleted == false)
            {
                if (DateTime.Now - start > timeout)
                {
                    return false;
                }
                Application.DoEvents();
            }
            return true;
        }
    }

    class Program
    {
        static List<string> PicUrls = new List<string>();
        static string[] InvalidPathStrings = new string[9] { @"\", @"/", @"?", @":", @"|", @"*", @"<", @">", @"""" };
        static List<string> PicExtensions = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };


        [STAThread]
        static void Main(string[] args)
        {
            //第一コマンドライン引数はダウンロード先WebページのURL
            //第二コマンドライン引数は保存先ディレクトリ

            if (args.Count() != 2 || !Directory.Exists(args[1]))
                return;

            UnDisplayedBrowser udb = new UnDisplayedBrowser();
            udb.NavigateAndWait(args[0]);

            HtmlDocument doc = udb.Document;

            WebClient wc = new WebClient();

            string PicUrl;

            foreach (HtmlElement e in doc.GetElementsByTagName("IMG"))
            {
                PicUrl = e.GetAttribute("src");

                string fileName = PicUrl;

                if (PicExtensions.Contains(Path.GetExtension(PicUrl)) == false)
                    continue;

                if(PicUrls.Contains(PicUrl)==false)
                {
                    PicUrls.Add(e.GetAttribute("src"));

                    foreach (var InvalidPathChar in Path.GetInvalidPathChars())
                    {
                        if (PicUrl.Contains(InvalidPathChar))
                            fileName = fileName.Replace(InvalidPathChar, '_');
                    }

                    fileName = Path.GetFileName(fileName);

                    foreach (var InvalidPathString in InvalidPathStrings)
                    {
                        if (PicUrl.Contains(InvalidPathString))
                            fileName = fileName.Replace(InvalidPathString, "_");
                    }


                    wc.DownloadFile(PicUrl, args[1] + fileName);
                }
            }

        }
    }
}