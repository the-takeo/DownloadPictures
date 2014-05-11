using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    class DownloadPictures
    {
        List<string> picUrls = new List<string>();

        string[] invalidPathStrings = new string[9] { @"\", @"/", @"?", @":", @"|", @"*", @"<", @">", @"""" };
        List<string> picExtensions = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };

        bool isDownloading = false;

        string downloadingfileName = string.Empty;

        int Count = 1;

        public void StartDownload(string url, string folder)
        {
            UnDisplayedBrowser udb = new UnDisplayedBrowser();
            udb.NavigateAndWait(url);

            HtmlDocument doc = udb.Document;

            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;

            //Webページに表示されている画像の取得
            foreach (HtmlElement picElement in doc.GetElementsByTagName("IMG"))
            {
                string picUrl = picElement.GetAttribute("src");

                if (picExtensions.Contains(Path.GetExtension(picUrl)) == false)
                    continue;

                if (picUrls.Contains(picUrl) == false)
                {
                    picUrls.Add(picElement.GetAttribute("src"));
                }
            }

            //サムネイル画像をリンク先画像に差し替え
            foreach (HtmlElement linkElement in doc.GetElementsByTagName("A"))
            {
                string picUrl = linkElement.GetAttribute("href");
                if (picExtensions.Contains(Path.GetExtension(picUrl)) == false)
                    continue;

                foreach (HtmlElement picElement in linkElement.GetElementsByTagName("IMG"))
                {
                    if (picUrls.Contains(picElement.GetAttribute("src")))
                    {
                        picUrls.Remove(picElement.GetAttribute("src"));
                        picUrls.Add(picUrl);
                    }
                }
            }

            foreach (var picUrl in picUrls)
            {
                waitforAsync();

                downloadingfileName = picUrl;

                //禁則文字の置換
                foreach (var InvalidPathChar in Path.GetInvalidPathChars())
                {
                    if (picUrl.Contains(InvalidPathChar))
                        downloadingfileName = downloadingfileName.Replace(InvalidPathChar, '_');
                }

                downloadingfileName = Path.GetFileName(downloadingfileName);

                //禁則文字の置換
                foreach (var InvalidPathString in invalidPathStrings)
                {
                    if (picUrl.Contains(InvalidPathString))
                        downloadingfileName = downloadingfileName.Replace(InvalidPathString, "_");
                }

                isDownloading = true;

                wc.DownloadFileAsync(new Uri(picUrl), folder + downloadingfileName);
            }
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("{0}件中{1}件のダウンロードが完了しました。", picUrls.Count, Count);
            Count++;
            isDownloading = false;
        }

        private void waitforAsync()
        {
            while (isDownloading)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
