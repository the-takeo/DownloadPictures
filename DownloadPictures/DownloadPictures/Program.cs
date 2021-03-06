﻿using System;
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
            if (args == null || args.Count() < 2)
                return;

            DownloadPictures dp = null;

            if (args.Count() == 3)
                dp = new DownloadPictures(args[2].Split(',').ToList());

            else
                dp = new DownloadPictures();

            dp.StartDownload(dp.GetPictures(args[0]), args[1]);
        }
    }
}