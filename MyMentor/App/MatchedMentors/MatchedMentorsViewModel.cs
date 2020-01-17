using MyMentor.Accounts;
using MyMentor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyMentor.App.MatchedMentors
{
    public class MatchedMentorsViewModel : MVVM.BaseViewModel
    {
        private RestService _restService;
        public ObservableCollection<UserInfoWithAvatar> Matches { get; set; }
        public ICommand AddConnectionCommand { get; set; }
        public ICommand ViewMentorProfileCommand { get; set; }
        public MatchedMentorsViewModel() { }

        public MatchedMentorsViewModel(List<UserInfo> mentors)
        {
            _restService = new RestService();
            IsStudent = UserInfo.User.UserType.ToLower().Equals("student");
            IsMentor = !IsStudent;
            Matches = new ObservableCollection<UserInfoWithAvatar>();
            LoadMatches(mentors);
            AddConnectionCommand = new Command<UserInfoWithAvatar>(AddConnection);
            ViewMentorProfileCommand = new Command<UserInfoWithAvatar>(ViewMentorProfile);

        }

        public Action<UserInfoWithAvatar> OnViewMentorProfile = delegate { };

        public Action<UserInfoWithAvatar> OnAddConnection = delegate { };

        public Action<UserInfoWithAvatar> OnAddStudent = delegate { };

        public void AddConnection(UserInfoWithAvatar mentor)
        {
            OnAddConnection(mentor);
        }

        public void ViewMentorProfile(UserInfoWithAvatar mentorToView)
        {
            OnViewMentorProfile(mentorToView);
        }

        public async void AddConnection(string connectionId)
        {
            await _restService.RequestMentorship(connectionId, UserInfo.User.UserId);
        }

        // Load the appropriate data for the logged in user (i.e. get students for mentors or get mentors for a student).
        public void LoadMatches(List<UserInfo> matches)
        {
            var avatarCounter = 1;
            foreach (var match in matches)
            {
                var avatarImageString = InMemoryData.DefaultAvatars[avatarCounter];
                //var academicInterestsAsSingleString = "";
                //foreach (var interest in match.AcademicInterests)
                //{
                //    if (!string.IsNullOrEmpty(interest.Name))
                //    {
                //        academicInterestsAsSingleString += interest.Name + " | ";
                //    }
                //}
                Matches.Add(new UserInfoWithAvatar(match, avatarImageString, match.AcademicInterests[1].Name));
                avatarCounter++;
            }
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
    }
}
