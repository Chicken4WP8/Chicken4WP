﻿using System;
using Chicken4WP.Models;

namespace Chicken4WP.ViewModels.Profile
{
    public class UserProfileDetailViewModel : PivotItemViewModelBase
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

        private string profileImageBiggerUrl;
        public string ProfileImageBiggerUrl
        {
            get { return profileImageBiggerUrl; }
            set
            {
                profileImageBiggerUrl = value;
                NotifyOfPropertyChange(() => ProfileImageBiggerUrl);
            }
        }

        private string profileBannerUrl;
        public string ProfileBannerUrl
        {
            get { return profileBannerUrl; }
            set
            {
                profileBannerUrl = value;
                NotifyOfPropertyChange(() => ProfileBannerUrl);
            }
        }

        private string profileImageOriginalUrl;
        public string ProfileImageOriginalUrl
        {
            get { return profileImageOriginalUrl; }
            set
            {
                profileImageOriginalUrl = value;
                NotifyOfPropertyChange(() => ProfileImageOriginalUrl);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            RefreshData();
        }

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("ProfilePage_ProfileDetail_Header");
        }

        protected override void RefreshData()
        {
            User = storageService.GetTempUser();
            ProfileImageBiggerUrl = User.ProfileImageUrl.Replace("_normal", "_bigger");
            ProfileImageOriginalUrl = User.ProfileImageUrl.Replace("_normal", "");
            ProfileBannerUrl = User.UserProfileBannerUrl + "/web";
        }

        protected override void LoadData()
        {

        }
    }
}