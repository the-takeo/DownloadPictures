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

        public FormMain()
        {
            InitializeComponent();

            lblTop.Text = "URLを入力し、「URLから画像を取得する」ボタンを押してください。\nダウンロードしたい画像を選択し、「ダウンロードする」ボタンを押してください。";

            listView.LargeImageList = imageList;
            listView.SmallImageList = imageList;
        }

        private void btnGetPictures_Click(object sender, EventArgs e)
        {
            listView.Clear();
            imageList.Images.Clear();

            List<string> Adresses = dp.GetPictures(tbUrl.Text);

            WebClient wc = new WebClient();
            Stream stream;

            for (int i=0;i<Adresses.Count;i++)
            {
                string url = Adresses[i];
                stream = wc.OpenRead(url);
                Image image = Image.FromStream(stream);

                imageList.Images.Add(image);

                listView.Items.Add(url, i);
                stream.Close();
            }

            selectallpictures();
        }

        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            List<string> SelectedItems=new List<string>();

            for (int i = 0; i < listView.SelectedItems.Count; i++)
            {
                SelectedItems.Add(listView.SelectedItems[i].Text);
            }

            dp.StartDownload(SelectedItems, tbFolder.Text);
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
    }
}
