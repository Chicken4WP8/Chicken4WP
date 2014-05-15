using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;

namespace Chicken4WP.Services
{
    public class ImageCacheService
    {
        private static readonly object locker = new object();
        private const int MAX_CACHE_ITEM = 500;
        private static Dictionary<string, byte[]> imageCacheDic = new Dictionary<string, byte[]>(MAX_CACHE_ITEM);
        private static Random random = new Random();

        public static void SetImageStream(string imageUrl, Action<byte[]> callBack)
        {
            #region if cached
            if (imageCacheDic.ContainsKey(imageUrl)
                && imageCacheDic[imageUrl] != null)
            {
                callBack(imageCacheDic[imageUrl]);
                return;
            }
            #endregion
            #region add to pending work list
            var pendingwork = new PendingWork
            {
                ImageUrl = imageUrl,
                CallBack = callBack
            };
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += DownloadImage;
            worker.RunWorkerAsync(pendingwork);
            #endregion
        }

        private static void DownloadImage(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(random.Next(MAX_CACHE_ITEM));
            if (e.Cancel)
                return;
            var pendingwork = e.Argument as PendingWork;
            pendingwork.Request = WebRequest.CreateHttp(pendingwork.ImageUrl + "?random=" + DateTime.Now.Ticks.ToString("x"));
            pendingwork.Request.BeginGetResponse(DownloadImage, pendingwork);
        }

        private static void DownloadImage(IAsyncResult result)
        {
            var pendingwork = (PendingWork)result.AsyncState;
            try
            {
                var response = pendingwork.Request.EndGetResponse(result);
                using (Stream stream = response.GetResponseStream())
                {
                    var memoryStream = new MemoryStream();
                    stream.CopyTo(memoryStream);
                    var data = memoryStream.ToArray();
                    AddImageCache(pendingwork.ImageUrl, data);
                }
                response.Close();
                pendingwork.CallBack(imageCacheDic[pendingwork.ImageUrl]);
            }
            catch (Exception e)
            {
            }
        }

        private static void DownloadImageComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            (sender as BackgroundWorker).DoWork -= DownloadImage;
        }

        private static void AddImageCache(string imageUrl, byte[] data)
        {
            lock (locker)
            {
                imageCacheDic[imageUrl] = data;
            }
        }


        private class PendingWork
        {
            public HttpWebRequest Request { get; set; }

            public string ImageUrl { get; set; }

            public Action<byte[]> CallBack { get; set; }
        }
    }
}
