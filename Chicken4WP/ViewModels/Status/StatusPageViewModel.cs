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
    public class HomePageViewModel : Conductor<Screen>.Collection.OneActive
    {
        //private IndexViewModel index;
        //private MentionViewModel mention;
        //private DMViewModel dm;

        //public HomePageViewModel(IndexViewModel index, MentionViewModel mention, DMViewModel dm)
        //{
        //    this.index = index;
        //    this.mention = mention;
        //    this.dm = dm;
        //}

        protected override void OnInitialize()
        {
            base.OnInitialize();

            //Items.Add(index);
            //Items.Add(mention);
            //Items.Add(dm);

            AppBarConductor.Mixin(this);
        }
    }
}
