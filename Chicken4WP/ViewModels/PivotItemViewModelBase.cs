//using System;
//using System.Globalization;
//using System.Linq;
//using System.Windows;
//using System.Windows.Media.Imaging;
//using Caliburn.Micro;
//using Chicken4WP.Entities;
//using Chicken4WP.Services;
//using Chicken4WP.Services.Interface;

//namespace Chicken4WP.ViewModels
//{
//    //public abstract class PivotItemViewModelBase : Screen, IHandle<CultureInfo>
//    //{
//    //    protected readonly IEventAggregator eventAggregator;
//    //    protected readonly ToastMessageService toastMessageService;
//    //    protected readonly ITweetService tweetService;

//    //    protected BitmapImage defaultImage = new BitmapImage(new Uri("/Images/dark/cat.png", UriKind.Relative));

//    //    public PivotItemViewModelBase(ToastMessageService toastMessageService, IEventAggregator eventAggregator)
//    //    {
//    //        this.toastMessageService = toastMessageService;
//    //        this.eventAggregator = eventAggregator;
//    //        eventAggregator.Subscribe(this);
//    //        var proxy = ChickenDataContext.Instance.Settings.SingleOrDefault(s => s.Type == SettingType.ProxySetting && s.IsInUsed);
//    //        this.tweetService = (Application.Current.Resources["bootstrapper"] as AppBootstrapper).Container
//    //            .GetInstance(typeof(ITweetService), proxy.Name) as ITweetService;
//    //        SetLanguage();
//    //    }

//    //    public void Handle(CultureInfo message)
//    //    {
//    //        SetLanguage();
//    //    }

//    //    protected abstract void SetLanguage();
//    //}
//}
