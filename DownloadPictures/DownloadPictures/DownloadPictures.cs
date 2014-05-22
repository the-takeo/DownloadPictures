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
    /// <summary>
    /// Webページ情報を取得するための非表示ウィブブラウザクラス
    /// </summary>
    public class UnDisplayedBrowser : WebBrowser
    {
        bool isCompleted = false;

        TimeSpan timeout = new TimeSpan(0, 0, 10);

        public UnDisplayedBrowser()
        {
            this.ScriptErrorsSuppressed = true;
        }

        protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url == this.Url)
                isCompleted = true;
        }

        protected override void OnNewWindow(CancelEventArgs e)
        {
            e.Cancel = true;
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

    /// <summary>
    /// 情報を取得し、ダウンロードを行うクラス
    /// </summary>
    public class DownloadPictures
    {
        string[] invalidPathStrings = new string[9] { @"\", @"/", @"?", @":", @"|", @"*", @"<", @">", @"""" };
        List<string> picExtensions = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };

        bool isDownloading = false;
        int numberOfPictures;
        int count = 1;

        TimeSpan timeout = new TimeSpan(0, 0, 10);
        DateTime start = new DateTime();

        List<string> filter_ = new List<string>();

        public DownloadPictures(List<string> filter=null)
        {
            if (filter != null)
                filter_ = filter;
        }

        /// <summary>
        /// 指定したURLのWebページに表示されている画像アドレスを取得し、
        /// リストにして返す。
        /// </summary>
        /// <param name="url">Webページのアドレス</param>
        public List<string> GetPictures(string url)
        {
            List<string> pictureAdresses = new List<string>();

            UnDisplayedBrowser udb = new UnDisplayedBrowser();
            udb.NavigateAndWait(url);

            HtmlDocument doc = udb.Document;


            //Webページに表示されている画像の取得
            foreach (HtmlElement picElement in doc.GetElementsByTagName("IMG"))
            {
                string picUrl = picElement.GetAttribute("src");

                if (picExtensions.Contains(Path.GetExtension(picUrl)) == false)
                    continue;

                //フィルター式が設定されている場合、除外する
                if (filter_ != null)
                {
                    bool isFiltered = false;

                    foreach (var item in filter_)
                    {
                        if(picUrl.Contains(item))
                        {
                            isFiltered = true;
                            break;
                        }
                    }

                    if (isFiltered) 
                        continue;
                }

                if (pictureAdresses.Contains(picUrl) == false)
                {
                    pictureAdresses.Add(picUrl);
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
                    if (pictureAdresses.Contains(picElement.GetAttribute("src")))
                    {
                        pictureAdresses.Remove(picElement.GetAttribute("src"));
                        pictureAdresses.Add(picUrl);
                    }
                }
            }

            return pictureAdresses;
        }

        /// <summary>
        /// 渡されたアドレスリストを元に画像のダウンロードを開始する
        /// </summary>
        /// <param name="pictureAdresses">ダウンロード対象画像リスト</param>
        /// <param name="folder">ダウンロード先ディレクトリ</param>
        public void StartDownload(List<string> pictureAdresses, string folder)
        {
            if (folder.EndsWith(@"\") == false)
                folder = folder + @"\";

            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;

            string downloadingfileName = string.Empty;
            numberOfPictures = pictureAdresses.Count;

            foreach (var picUrl in pictureAdresses)
            {
                if (waitforAsync() == false)
                    wc.CancelAsync();

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
                start = DateTime.Now;

                wc.DownloadFileAsync(new Uri(picUrl), folder + downloadingfileName);
            }

            if(waitforAsync()==false)
            {
                wc.CancelAsync();
                Console.WriteLine("{0}件中{1}件目のダウンロードが失敗しました。", numberOfPictures, count);
                count++;
                isDownloading = false;
            }
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("{0}件中{1}件目のダウンロードが完了しました。", numberOfPictures, count);
            count++;
            isDownloading = false;
        }

        private bool waitforAsync()
        {
            while (isDownloading)
            {
                if (DateTime.Now - start > timeout)
                    return false;

                Thread.Sleep(1000);
            }

            return true;
        }
    }
}
