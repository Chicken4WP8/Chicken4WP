using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chicken4WP.Services;
using ImageTools;
using ImageTools.IO;
using ImageTools.IO.Bmp;
using ImageTools.IO.Gif;
using ImageTools.IO.Png;

namespace Chicken4WP.Controls
{
    [TemplatePart(Name = ElementImageBrushName, Type = typeof(ImageBrush))]
    public class ThemedImage : Control
    {
        private const string ElementImageBrushName = "ImageBrush";
        private Image ElementImageBrush;
        private ImageCacheService imageCacheService;

        public ThemedImage()
        {
            Decoders.AddDecoder<BmpDecoder>();
            Decoders.AddDecoder<PngDecoder>();
            Decoders.AddDecoder<GifDecoder>();

            DefaultStyleKey = typeof(ThemedImage);
            this.imageCacheService = (Application.Current.Resources["bootstrapper"] as AppBootstrapper).Container
                .GetInstance(typeof(ImageCacheService), null) as ImageCacheService;
        }

        #region public ImageSource Source
        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register("ImageUrl", typeof(string), typeof(ThemedImage), new PropertyMetadata(SourceUrlChanged));

        public string ImageUrl
        {
            get
            {
                return (string)GetValue(ImageUrlProperty);
            }
            set
            {
                SetValue(ImageUrlProperty, value);
            }
        }

        private static void SourceUrlChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var image = sender as ThemedImage;
            if (e.OldValue != null)
                image.ApplySource();
            image.SourceUrlChanged();
        }

        private void SourceUrlChanged()
        {
            if (!string.IsNullOrEmpty(ImageUrl))
                imageCacheService.SetImageStream(ImageUrl, SetImageSource);
        }

        private void SetImageSource(byte[] data)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
                () =>
                {
                    if (data == null)
                        return;
                    #region jpeg/png
                    try
                    {
                        using (var memStream = new MemoryStream(data))
                        {
                            memStream.Position = 0;
                            var bitmapImage = new BitmapImage();
                            bitmapImage.SetSource(memStream);
                            ApplySource(bitmapImage);
                        }
                    }
                    #endregion
                    #region others
                    catch (Exception exception)
                    {
                        Debug.WriteLine("set gif image. length: {0}", data.Length);
                        var memStream = new MemoryStream(data);
                        memStream.Position = 0;
                        var gifImage = new ExtendedImage();
                        gifImage.SetSource(memStream);
                        gifImage.LoadingCompleted += ExtendedImageLoadCompleted;
                    }
                    #endregion
                });
        }

        private void ExtendedImageLoadCompleted(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
                () =>
                {
                    try
                    {
                        var gifImage = sender as ExtendedImage;
                        var writeableBitmap = gifImage.ToBitmap();
                        using (var stream = new MemoryStream())
                        {
                            writeableBitmap.SaveJpeg(stream, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight, 0, 100);
                            ApplySource(writeableBitmap);
                            byte[] bytes = stream.ToArray();
                            ImageCacheService.AddImageCache(ImageUrl, bytes);
                        }
                        gifImage.LoadingCompleted -= ExtendedImageLoadCompleted;
                    }
                    catch (Exception exception)
                    {
                    }
                });
        }

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(ThemedImage), null);

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ElementImageBrush = GetTemplateChild(ElementImageBrushName) as Image;
            ApplySource();
        }

        private void ApplySource(ImageSource image = null)
        {
            if (ElementImageBrush != null)
            {
                ElementImageBrush.Source = image == null ? Source : image;
                ElementImageBrush.Stretch = Stretch.Fill;
            }
        }
    }
}
