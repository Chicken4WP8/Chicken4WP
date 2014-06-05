using Chicken4WP.Models;

namespace Chicken4WP.ViewModels.Profile
{
    public abstract class ProfileViewModelBase : PivotItemViewModelBase
    {
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        protected override void Initialize()
        {
            User = storageService.GetTempUser();
            if (CheckIsFollowingProtectedUser())
            {
                ProfileInitialize();
            }
        }

        protected override void RefreshData()
        {
            if (CheckIsFollowingProtectedUser())
            {
                ProfileRefreshData();
            }
        }

        protected override void LoadData()
        {
            if (CheckIsFollowingProtectedUser())
            {
                ProfileLoadData();
            }
        }

        protected virtual void ProfileInitialize()
        { }

        protected virtual void ProfileRefreshData()
        { }

        protected virtual void ProfileLoadData()
        { }

        protected bool CheckIsFollowingProtectedUser()
        {
            if (!User.IsFollowing && User.Id != App.CurrentUser.Id)
            {
                toastMessageService.HandleMessage(languageHelper.GetString("Toast_Msg_NotFollowPrivateUser", User.ScreenName));
                base.LoadDataCompleted();
                return false;
            }
            return true;
        }
    }
}
