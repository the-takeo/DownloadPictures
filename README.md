DownloadPictures
================
### About this

指定したWebページ上の画像を取得し、一括ダウンロードします。
HTML解析してリンク取得しているだけなので、取得できない場合もあります。許してください。

### How to use

使用例:  

    string url = "WebページのURL";   
    string folder = "保存先フォルダのアドレス";

    DownloadPictures dp = new DownloadPictures();

    // 指定したURLのページ上に存在する画像のURLリスト
    List<string> pictures = dp.GetPictures(url);

    // 渡されたURLリストの画像を取得し、指定したフォルダにダウンロードします。
    dp.StartDownload(pictures, folder);

Bulk download the image on a Web page.
