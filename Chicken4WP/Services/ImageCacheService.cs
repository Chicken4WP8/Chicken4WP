using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ImageTools.IO;
using ImageTools.IO.Bmp;
using ImageTools.IO.Gif;
using ImageTools.IO.Png;

namespace Chicken4WP.Services
{
    public class ImageCacheService
    {
        private static readonly object locker = new object();
        private const int MAX_CACHE_ITEM = 1000;
        private static Dictionary<string, byte[]> imageCacheDic = new Dictionary<string, byte[]>(MAX_CACHE_ITEM);

        static ImageCacheService()
        {
            Decoders.AddDecoder<BmpDecoder>();
            Decoders.AddDecoder<PngDecoder>();
            Decoders.AddDecoder<GifDecoder>();
        }

        public static void SetImageStream(string imageUrl, Action<byte[]> callback)
        {
            #region if cached
            if (imageCacheDic.ContainsKey(imageUrl)
                && imageCacheDic[imageUrl] != null)
            {
                callback(imageCacheDic[imageUrl]);
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

        private static void DownloadImage(WebResponse response, string url)
        {
            using (Stream stream = response.GetResponseStream())
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                var data = memoryStream.ToArray();
                AddImageCache(url, data);
            }
            response.Close();
        }

        private static void AddImageCache(string imageUrl, byte[] data)
        {
            lock (locker)
            {
                imageCacheDic[imageUrl] = data;
            }
        }
    }
}
