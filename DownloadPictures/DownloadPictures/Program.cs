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

                if(PicUrls.Contains(PicUrl)==false)
                {
                    PicUrls.Add(e.GetAttribute("src"));

                    foreach (var item in Path.GetInvalidPathChars())
                    {
                        if (PicUrl.Contains(item))
                            fileName = fileName.Replace(item, '_');
                    }

                    fileName = Path.GetFileName(fileName);

                    foreach (var item in new string[9]{@"\",@"/",@"?",@":",@"|",@"*",@"<",@">",@""""})
                    {
                        if (PicUrl.Contains(item))
                            fileName = fileName.Replace(item, "_");
                    }


                    wc.DownloadFile(PicUrl, args[1] + fileName);
                }
            }

        }
    }
}