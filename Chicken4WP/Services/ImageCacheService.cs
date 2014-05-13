using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Chicken4WP.Services
{
    public class ImageCacheService
    {
        private static Dictionary<string, BitmapImage> cachedImages = new Dictionary<string, BitmapImage>();

        public static BitmapImage GetImageFromUrl(string url)
        {
            if (cachedImages.ContainsKey(url))
            {
                return cachedImages[url];
            }
            var request = WebRequest.CreateHttp(url + "?random=" + DateTime.Now.Ticks.ToString("x"));
            var task = Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                null);
            return task.ContinueWith(t => DownloadImage(t.Result, url)).Result;
        }

        private static BitmapImage DownloadImage(WebResponse response, string url)
        {
            using (Stream stream = response.GetResponseStream())
            {
                var image = new BitmapImage();
                image.SetSource(stream);
                cachedImages.Add(url, image);
            }
            response.Close();
            return cachedImages[url];
        }
    }
}
