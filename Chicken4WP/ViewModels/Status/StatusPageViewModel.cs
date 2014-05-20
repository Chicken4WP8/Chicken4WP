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
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;

namespace Chicken4WP.ViewModels.Status
{
    public class StatusPageViewModel : Conductor<Screen>.Collection.OneActive
    {
        private StatusDetailViewModel detail;
        private StatusRetweetViewModel retweet;

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
