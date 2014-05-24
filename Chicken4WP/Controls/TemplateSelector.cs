using System.Windows;
using System.Windows.Controls;
using Chicken4WP.Models;

namespace Chicken4WP.Controls
{
    public abstract class TemplateSelector : ContentControl
    {
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            ContentTemplate = this.SelectTemplate(this, newContent);
        }
        protected virtual DataTemplate SelectTemplate(DependencyObject sender, object newValue)
        {
            return null;
        }
    }

    public class TweetTemplateSelector : TemplateSelector
    {
        public DataTemplate TweetTemplate { get; set; }

        public DataTemplate RetweetTemplate { get; set; }

        protected override DataTemplate SelectTemplate(DependencyObject sender, object newValue)
        {
            Tweet tweet = newValue as Tweet;
            if (tweet.RetweetStatus == null)
                return TweetTemplate;
            return RetweetTemplate;
        }
    }
}
