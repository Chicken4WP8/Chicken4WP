using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Chicken4WP.Models;

namespace Chicken4WP.Controls
{
    public class AutoRichTextBox : RichTextBox
    {
        private static Brush accentBrush = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

        public static DependencyProperty DataProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AutoRichTextBox), new PropertyMetadata(TweetDataPropertyChanged));

        public string Text
        {
            get { return GetValue(DataProperty) as string; }
            set { SetValue(DataProperty, value); }
        }

        public static DependencyProperty EntitiesProperty =
            DependencyProperty.Register("Entities", typeof(IList<EntityBase>), typeof(AutoRichTextBox), null);

        public IList<EntityBase> Entities
        {
            get { return GetValue(EntitiesProperty) as IList<EntityBase>; }
            set { SetValue(EntitiesProperty, value); }
        }

        private static void TweetDataPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as AutoRichTextBox;
            if (textBox == null || e.NewValue == null || textBox.Entities == null)
                return;
            textBox.AddEntities();
        }

        private void AddEntities()
        {
            this.Blocks.Clear();
            //string text = string.Empty;
            var paragraph = new Paragraph();
            #region none
            if (Entities.Count == 0)
            {
                paragraph.Inlines.Add(new Run
                {
                    Text = HttpUtility.HtmlDecode(Text)
                });
                this.Blocks.Add(paragraph);
            }
            #endregion
            #region add
            else
            {
                #region replace
                int index = 0;
                foreach (var entity in Entities.OrderBy(v => v.Index))
                {
                    #region starter
                    if (index < entity.Index)
                    {
                        paragraph.Inlines.Add(new Run
                        {
                            Text = HttpUtility.HtmlDecode(Text.Substring(index, entity.Index - index)),
                        });
                        index = entity.Index;
                    }
                    #endregion
                    var hyperlink = new Hyperlink();
                    hyperlink.TextDecorations = null;
                    hyperlink.Foreground = accentBrush;
                    #region entity
                    switch (entity.EntityType)
                    {
                        #region mention, hashtag
                        case EntityType.UserMention:
                        case EntityType.HashTag:
                            hyperlink.CommandParameter = entity;
                            hyperlink.Click += this.Hyperlink_Click;
                            hyperlink.Inlines.Add(entity.DisplayText);
                            break;
                        #endregion
                        #region media, url
                        case EntityType.Media:
                            var media = entity as MediaEntity;
                            hyperlink.NavigateUri = new Uri(media.MediaUrl, UriKind.Absolute);
                            hyperlink.TargetName = "_blank";
                            hyperlink.Inlines.Add(media.TruncatedUrl);
                            break;
                        case EntityType.Url:
                            var url = entity as UrlEntity;
                            hyperlink.NavigateUri = new Uri(url.ExpandedUrl, UriKind.Absolute);
                            hyperlink.TargetName = "_blank";
                            hyperlink.Inlines.Add(url.TruncatedUrl);
                            break;
                        #endregion
                    }
                    #endregion
                    paragraph.Inlines.Add(hyperlink);
                    index += entity.DisplayText.Length;
                }
                #region ender
                if (index < Text.Length)
                {
                    paragraph.Inlines.Add(new Run
                    {
                        Text = HttpUtility.HtmlDecode(Text.Substring(index, Text.Length - index)),
                    });
                }
                #endregion
                #endregion
                this.Blocks.Add(paragraph);
            }
            #endregion
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var hyperlink = sender as Hyperlink;
            var entity = hyperlink.CommandParameter as EntityBase;
            switch (entity.EntityType)
            {
                case EntityType.UserMention:
                    var mention = entity as UserMention;
                    User user = new User
                    {
                        Id = mention.Id,
                        ScreenName = mention.ScreenName,
                    };
                    //NavigationServiceManager.NavigateTo(Const.ProfilePage, user);
                    break;
                case EntityType.HashTag:
                    //var query = IsolatedStorageService.GetObject<string>(Const.SearchPage);
                    //if (entity.Text != query)
                    //    NavigationServiceManager.NavigateTo(Const.SearchPage, entity.Text);
                    break;
            }
        }
    }
}
