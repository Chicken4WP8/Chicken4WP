
using System.Collections.ObjectModel;
using Chicken4WP.Models;
namespace Chicken4WP.ViewModels.Home
{
    public class MentionViewModel : PivotItemViewModelBase
    {
        private ObservableCollection<Tweet> items;
        public ObservableCollection<Tweet> Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            if (Items == null)
            {
                tweetService.GetMentions(null,
                    tweets =>
                    {
                        Items = new ObservableCollection<Tweet>();
                        for (int i = 0; i < tweets.Count; i++)
                        {
                            Items.Insert(0, tweets[i]);
                        }
                    });
            }
        }
        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_Mention_Header");
        }

        protected override void RefreshData()
        {
            throw new System.NotImplementedException();
        }

        protected override void LoadData()
        {
            throw new System.NotImplementedException();
        }
    }
}
