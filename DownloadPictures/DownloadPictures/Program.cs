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

            string folder = args[1];

            HtmlDocument doc = udb.Document;

            WebClient wc = new WebClient();

            //Webページに表示されている画像の取得
            foreach (HtmlElement picElement in doc.GetElementsByTagName("IMG"))
            {
                string picUrl = picElement.GetAttribute("src");

                if (PicExtensions.Contains(Path.GetExtension(picUrl)) == false)
                    continue;

                if (PicUrls.Contains(picUrl) == false)
                {
                    PicUrls.Add(picElement.GetAttribute("src"));
                }
            }

            //サムネイル画像をリンク先画像に差し替え
            foreach (HtmlElement linkElement in doc.GetElementsByTagName("A"))
            {
                string picUrl = linkElement.GetAttribute("href");
                if (PicExtensions.Contains(Path.GetExtension(picUrl)) == false)
                    continue;

                foreach (HtmlElement picElement in linkElement.GetElementsByTagName("IMG"))
                {
                    if (PicUrls.Contains(picElement.GetAttribute("src")))
                    {
                        PicUrls.Remove(picElement.GetAttribute("src"));
                        PicUrls.Add(picUrl);
                    }
                }
            }

            foreach (var picUrl in PicUrls)
            {
                string fileName = picUrl;

                //禁則文字の置換
                foreach (var InvalidPathChar in Path.GetInvalidPathChars())
                {
                    if (picUrl.Contains(InvalidPathChar))
                        fileName = fileName.Replace(InvalidPathChar, '_');
                }

                fileName = Path.GetFileName(fileName);

                //禁則文字の置換
                foreach (var InvalidPathString in InvalidPathStrings)
                {
                    if (picUrl.Contains(InvalidPathString))
                        fileName = fileName.Replace(InvalidPathString, "_");
                }

                wc.DownloadFile(picUrl, folder + fileName);
            }
        }
    }
}