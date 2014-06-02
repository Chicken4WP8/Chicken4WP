using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Chicken4WP.Services
{
    public class ImageCacheService
    {
        private static readonly object locker = new object();
        private const int MAX_CACHE_ITEM = 2000;
        private static Dictionary<string, byte[]> imageCacheDic = new Dictionary<string, byte[]>(MAX_CACHE_ITEM);
        
        public void SetImageStream(string imageUrl, Action<byte[]> callback)
        {
            #region if cached
            List<byte> data = new List<byte>();
            lock (locker)
            {
                if (imageCacheDic.ContainsKey(imageUrl) && imageCacheDic[imageUrl] != null)
                {
                    byte[] temp = new byte[imageCacheDic[imageUrl].Length];
                    imageCacheDic[imageUrl].CopyTo(temp, 0);
                    data.AddRange(temp);
                }
            }
            if (data.Count != 0)
            {
                callback(data.ToArray());
                return;
            }
            #endregion
            #region add to pending work list
            var request = WebRequest.CreateHttp(imageUrl + "?random=" + DateTime.Now.Ticks.ToString("x"));
            Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                null)
                .ContinueWith(
                t =>
                {
                    DownloadImage(t.Result, imageUrl);
                    SetImageStream(imageUrl, callback);
                });
            #endregion
        }

        private void DownloadImage(WebResponse response, string url)
        {
            if (imageCacheDic.ContainsKey(url) && imageCacheDic[url] != null)
                return;

            using (Stream stream = response.GetResponseStream())
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                var data = memoryStream.ToArray();
                AddImageCache(url, data);
            }
            response.Close();
        }

        public static void AddImageCache(string imageUrl, byte[] data)
        {
            lock (locker)
            {
                imageCacheDic[imageUrl] = data;
            }
        }
    }
}
