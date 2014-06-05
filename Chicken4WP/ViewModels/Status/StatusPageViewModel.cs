using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;

namespace Chicken4WP.ViewModels.Status
{
    public class StatusPageViewModel : Conductor<Screen>.Collection.OneActive
    {
        private StatusDetailViewModel detail;
        private StatusRetweetViewModel retweet;

        public string Random { get; set; }

        public StatusPageViewModel(StatusDetailViewModel detail, StatusRetweetViewModel retweet)
        {
            this.detail = detail;
            this.retweet = retweet;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(detail);
            Items.Add(retweet);

            AppBarConductor.Mixin(this);
        }
    }
}
