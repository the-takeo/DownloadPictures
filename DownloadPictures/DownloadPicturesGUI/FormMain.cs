using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DownloadPictures;

namespace DownloadPicturesGUI
{
    public partial class FormMain : Form
    {
        DownloadPictures.DownloadPictures dp=new DownloadPictures.DownloadPictures();
        List<string> SelectedItems = new List<string>();
        List<string> Adresses = new List<string>();

        const int listWidth = 100;
        const int listHeight = 80;

        public FormMain()
        {
            InitializeComponent();

            lblTop.Text = "URLを入力し、「URLから画像を取得する」ボタンを押してください。\nダウンロードしたい画像を選択し、「ダウンロードする」ボタンを押してください。";

            imageList.ImageSize = new Size(listWidth, listHeight);
            listView.LargeImageList = imageList;

            this.MinimumSize = new Size(600, 600);

            this.Text = "DownloadPictures";
        }

        private void btnGetPictures_Click(object sender, EventArgs e)
        {

            btnGetPictures.Enabled = false;
            btnSelectAll.Enabled = false;
            btnSelectFolder.Enabled = false;
            btnStartDownload.Enabled = false;
            listView.Enabled = false;
            tbUrl.Enabled = false;
            tbFolder.Enabled = false;

            lblProgress.Text = "Webページから画像を取得中";

            listView.Clear();
            imageList.Images.Clear();

            Adresses = dp.GetPictures(tbUrl.Text);

            WebClient wc = new WebClient();
            Stream stream;

            for (int i = 0; i < Adresses.Count; i++)
            {
                string url = Adresses[i];
                stream = wc.OpenRead(url);
                Image image = Image.FromStream(stream);
                Image thumbnail = createThumbnail(image, listWidth, listHeight);

                imageList.Images.Add(thumbnail);

                listView.Items.Add(url, i);
                listView.Items[i].Checked = true;
                stream.Close();
            }

            selectallpictures();

            btnGetPictures.Enabled = true;
            btnSelectAll.Enabled = true;
            btnSelectFolder.Enabled = true;
            btnStartDownload.Enabled = true;
            listView.Enabled = true;
            tbUrl.Enabled = true;
            tbFolder.Enabled = true;

            lblProgress.Text = "Progress";
        }

        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            SelectedItems.Clear();

            btnGetPictures.Enabled = false;
            btnSelectAll.Enabled = false;
            btnSelectFolder.Enabled = false;
            btnStartDownload.Enabled = false;
            listView.Enabled = false;
            tbUrl.Enabled = false;
            tbFolder.Enabled = false;

            lblProgress.Text = "画像をダウンロード中";

            for (int i = 0; i < listView.CheckedItems.Count; i++)
            {
                SelectedItems.Add(listView.CheckedItems[i].Text);
            }

            bwDownload.RunWorkerAsync();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "フォルダ選択";
            fbd.RootFolder = Environment.SpecialFolder.MyDocuments;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbFolder.Text = fbd.SelectedPath;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            selectallpictures();
        }

        private void selectallpictures()
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                listView.Items[i].Selected = true;
            }

            listView.Select();
        }

        private void bwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            dp.StartDownload(SelectedItems, tbFolder.Text);
        }

        private void bwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("ダウンロードが完了しました。", "Completed");

            btnGetPictures.Enabled = true;
            btnSelectAll.Enabled = true;
            btnSelectFolder.Enabled = true;
            btnStartDownload.Enabled = true;
            listView.Enabled = true;
            tbUrl.Enabled = true;
            tbFolder.Enabled = true;

            lblProgress.Text = "Progress";
        }

        Image createThumbnail(Image image, int width, int height)
        {
            Bitmap canvas = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(canvas);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

            float fw = (float)width / (float)image.Width;
            float fh = (float)height / (float)image.Height;

            float scale = Math.Min(fw, fh);
            fw = image.Width * scale;
            fh = image.Height * scale;

            g.DrawImage(image, (width - fw) / 2, (height - fh) / 2, fw, fh);
            g.Dispose();

            return canvas;
        }
    }
}
