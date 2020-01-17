using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using MyMentor.Accounts;
using MyMentor.Services;
using Xamarin.Forms;

namespace MyMentor.App.Dashboard
{
    public class DashboardViewModel : MVVM.BaseViewModel
    {
        // Services
        private RestService _restService;
        // Notifications for the logged in user
        private List<UserInfo> _userNotifications;
        // Information of the logged in user
        public UserInfo UserInformation;
        public ICommand DashboardCommand { get; set; }
        public ICommand DirectMessagesCommand { get; set; }
        public ICommand ViewNotificationsCommand { get; set; }

        public Action<bool> OnFindMentorsClick = delegate { };
        public Action<List<UserInfo>> OnViewNotifications = delegate { };
        public Action<List<UserInfoWithAvatar>> OnViewDirectMessages = delegate { };

        public DashboardViewModel() { }
        public DashboardViewModel(UserInfo userInfo)
        {
            _restService = new RestService();
            _userNotifications = new List<UserInfo>();
            UserInformation = userInfo;
            IsStudent = UserInfo.User.UserType.ToLower().Equals("student");
            IsMentor = !IsStudent;
            ViewNotificationsCommand = new Command(ViewNotifications);
            DirectMessagesCommand = new Command(ViewDirectMessages);
            SetupNotificationTimer();
        }

        private bool _isStudent;
        public bool IsStudent
        {
            get { return _isStudent; }
            set
            {
                _isStudent = value;
                OnPropertyChanged("IsStudent");
            }
        }

        private bool _isMentor;
        public bool IsMentor
        {
            get { return _isMentor; }
            set
            {
                _isMentor = value;
                OnPropertyChanged("IsMentor");
            }
        }

        public void ViewNotifications()
        {
            OnViewNotifications(_userNotifications);
        }

        public async void ViewDirectMessages()
        {
            List<UserInfoWithAvatar> matchesForUser = new List<UserInfoWithAvatar>();
            if (IsStudent)
            {
                var mentorsForStudent = await _restService.GetMentorsForStudent(UserInfo.User.UserId);
                var avatarCounter = 1;
                foreach (var mentor in mentorsForStudent)
                {
                    if (mentor.User.UserId == "4F9040BA-F034-49C1-8078-20B97E37B435")
                    {
                        mentor.User.FullName = "Christopher T";
                    }
                    var avatarImageString = InMemoryData.DefaultAvatars[avatarCounter];
                    var academicInterestsAsSingleString = "";
                    var counter = 1;
                    foreach (var interest in mentor.AcademicInterests)
                    {
                        if (counter < 3)
                        {
                            if (!string.IsNullOrEmpty(interest.Name))
                            {
                                academicInterestsAsSingleString += interest.Name + " | ";
                            }
                            counter++;
                        }
                    }
                    matchesForUser.Add(new UserInfoWithAvatar(mentor, avatarImageString, academicInterestsAsSingleString));
                    avatarCounter++;
                }
            }
            else
            {
                var studentsForMentor = await _restService.GetStudentsForMentor(UserInfo.User.UserId);
                var avatarCounter = 1;
                foreach (var student in studentsForMentor)
                {
                    var avatarImageString = InMemoryData.DefaultAvatars[avatarCounter];
                    var academicInterestsAsSingleString = "";
                    foreach (var interest in student.AcademicInterests)
                    {
                        if (!string.IsNullOrEmpty(interest.Name))
                        {
                            academicInterestsAsSingleString += interest.Name + " | ";
                        }
                    }
                    matchesForUser.Add(new UserInfoWithAvatar(student, avatarImageString, academicInterestsAsSingleString));
                    avatarCounter++;
                }
            }
            OnViewDirectMessages(matchesForUser);
        }

        // Setup the polling timer for getting notifications.
        private void SetupNotificationTimer()
        {
            // Start a timer that runs after 10 seconds.
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Task.Factory.StartNew(async () =>
                {
                    // Do the actual request and wait for it to finish.
                    var notifications = await _restService.GetNotificationsForMentor(UserInfo.User.UserId);
                    // Switch back to the UI thread to update the UI
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // Update the UI
                        if (notifications.Any())
                        {
                            //// get list of current IDs to prevent adding duplicate notifications
                            //var notificationUserIds = _userNotifications.Select(notification => notification.User.UserId.ToLower()).ToList();
                            //// Add notifications to list, if any exist
                            //foreach (var notification in notifications)
                            //{
                            //    // Do not add duplicate notifications
                            //    if (_userNotifications.Count < 1 || !notificationUserIds.Contains(notification.User.UserId.ToLower()))
                            //    {
                            //        _userNotifications.Add(notification);
                            //    }

                            //}
                            _userNotifications.Clear();
                            _userNotifications.Add(notifications[0]);
                        }
                        // Now repeat by scheduling a new request
                        SetupNotificationTimer();
                    });
                });
                // Don't repeat the timer (we will start a new timer when the request is finished)
                return false;
            });
        }
    }
}
