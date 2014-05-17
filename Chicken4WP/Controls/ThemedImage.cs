using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chicken4WP.Services;
using ImageTools;

namespace Chicken4WP.Controls
{
    [TemplatePart(Name = ElementImageBrushName, Type = typeof(ImageBrush))]
    public class ThemedImage : Control
    {
        private const string ElementImageBrushName = "ImageBrush";
        private Image ElementImageBrush;

        public ThemedImage()
        {
            DefaultStyleKey = typeof(ThemedImage);
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
            (sender as ThemedImage).SourceUrlChanged();
        }

        private void SourceUrlChanged()
        {
            ImageCacheService.SetImageStream(ImageUrl, SetImageSource);
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
                            this.Source = bitmapImage;
                        }
                    }
                    #endregion
                    #region others
                    catch
                    {
                        var memStream = new MemoryStream(data);
                        memStream.Position = 0;
                        var gifImage = new ExtendedImage();
                        gifImage.SetSource(memStream);
                        this.Source = gifImage.ToBitmap();
                    }
                    #endregion
                });
        }

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(ThemedImage), new PropertyMetadata(SourceChanged));

        private static void SourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as ThemedImage).ApplySource();
        }
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ElementImageBrush = GetTemplateChild(ElementImageBrushName) as Image;
            ApplySource();
        }

        private void ApplySource()
        {
            if (ElementImageBrush != null)
            {
                ElementImageBrush.Source = Source;
            }
        }
    }
}
