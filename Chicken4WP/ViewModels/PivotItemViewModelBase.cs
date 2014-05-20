using Chicken4WP.Entities;
using Chicken4WP.Services;
using Chicken4WP.Services.Interface;
using Caliburn.Micro;
using System.Globalization;

namespace Chicken4WP.ViewModels
{
    public abstract class PivotItemViewModelBase : ViewModelBase, IHandle<CultureInfo>
    {
        protected override void OnActivate()
        {
            base.OnActivate();
            SetLanguage();
        }

        public virtual void Handle(CultureInfo message)
        {
            SetLanguage();
        }

        protected abstract void SetLanguage();
    }
}
