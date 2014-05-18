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
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args == null || args.Count() != 2)
                return;

            DownloadPictures dp = new DownloadPictures();

            dp.GetPictures(args[0]);

            dp.StartDownload(args[1]);
        }
    }
}