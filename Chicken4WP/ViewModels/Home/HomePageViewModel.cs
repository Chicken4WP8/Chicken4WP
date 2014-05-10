using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;

namespace Chicken4WP.ViewModels.Home
{
    public class HomePageViewModel : Conductor<Screen>.Collection.OneActive
    {
        private IndexViewModel index;
        private MentionViewModel mention;
        private DMViewModel dm;

        public HomePageViewModel(IndexViewModel index, MentionViewModel mention,DMViewModel dm)
        {
            this.index = index;
            this.mention = mention;
            this.dm = dm;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(index);
            Items.Add(mention);
            Items.Add(dm);

            AppBarConductor.Mixin(this);
        }
    }
}