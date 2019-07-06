DownloadPictures
================
### About this

Bulk download the image on a Web page.

### How to use

Example:  

    string url = "Page's URL";   
    string folder = "Local folder address";

    DownloadPictures dp = new DownloadPictures();

    // URL list of Pictures.
    List<string> pictures = dp.GetPictures(url);

    // Download to local folder.
    dp.StartDownload(pictures, folder);
