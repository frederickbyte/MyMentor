using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MyMentor.Accounts;
using Xamarin.Forms;
using User = MyMentor.Models.User;

namespace MyMentor.App.Notifications
{
    public class NotificationsViewModel : MVVM.BaseViewModel
    {
        public ObservableCollection<UserInfoWithAvatar> NotificationsForUser { get; set; }
        public ICommand RespondCommand { get; set; }

        public NotificationsViewModel()
        {
        }

        public NotificationsViewModel(List<UserInfo> notifications)
        {
            LoadNotifications(notifications);
            DoNotificationsExist = notifications != null && notifications.Count > 0;
            AreNotificationsEmpty = !DoNotificationsExist;
            RespondCommand = new Command<UserInfoWithAvatar>(RespondToNotification);
        }

        public void DismissNotification(string notificationUserId)
        {
            var notificationInList = NotificationsForUser.FirstOrDefault(n => n.UserInfo.User.UserId == notificationUserId);
            if (notificationInList != null)
            {
                NotificationsForUser.Remove(notificationInList);
            }
        }

        public Action<string, string> OnRespondToNotification = delegate { };

        public void RespondToNotification(UserInfoWithAvatar notificationUserInfo)
        {
            OnRespondToNotification(notificationUserInfo.UserInfo.User.UserId, notificationUserInfo.UserInfo.User.Username);
        }

        private void LoadNotifications(List<UserInfo> notifications)
        {
            NotificationsForUser = new ObservableCollection<UserInfoWithAvatar>();
            if (notifications != null && notifications.Count > 0)
            {
                var avatarCounter = 1;
                foreach (var notification in notifications)
                {
                    var avatarImageString = InMemoryData.DefaultAvatars[avatarCounter];
                    var academicInterestsAsSingleString = "";
                    // find last item in list so we can append commas when necessary
                    UserInfo last = notifications.Last();
                    foreach (var interest in notification.AcademicInterests)
                    {
                        if (!string.IsNullOrEmpty(interest.Name))
                        {
                            academicInterestsAsSingleString += interest.Name + " | ";
                        }
                    }
                    NotificationsForUser.Add(new UserInfoWithAvatar(notification, avatarImageString, academicInterestsAsSingleString));
                    avatarCounter++;
                }
            }
        }

        public Action OnDismissNotification = delegate { };


    private bool _doNotificationsExist;
        public bool DoNotificationsExist
        {
            get { return _doNotificationsExist; }
            set
            {
                _doNotificationsExist = value;
                OnPropertyChanged("DoNotificationsExist");
            }
        }

        private bool _areNotificationsEmpty;
        public bool AreNotificationsEmpty
        {
            get { return _areNotificationsEmpty; }
            set
            {
                _areNotificationsEmpty = value;
                OnPropertyChanged("AreNotificationsEmpty");
            }
        }
    }
}
