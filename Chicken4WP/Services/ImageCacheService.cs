using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Chicken4WP.Services.Interface;

namespace Chicken4WP.Services
{
    public class ImageCacheService
    {
        private readonly IStorageService storageService;

        public ImageCacheService(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public void SetImageStream(string imageUrl, Action<byte[]> callback)
        {
            #region if cached
            var data = storageService.GetCachedImage(imageUrl);
            if (data != null && data.Length != 0)
            {
                callback(data);
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
            using (Stream stream = response.GetResponseStream())
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                var data = memoryStream.ToArray();
                AddImageCache(url, data);
            }
            response.Close();
        }

        public void AddImageCache(string imageUrl, byte[] data)
        {
            storageService.AddCachedImage(imageUrl, data);
        }
    }
}
