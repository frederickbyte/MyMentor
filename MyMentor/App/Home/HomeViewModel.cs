using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyMentor.Services;
using System.Windows.Input;
using MyMentor.Accounts;
using Xamarin.Forms;
using System.Collections.Generic;

namespace MyMentor.App.Home
{
    public class HomeViewModel : MVVM.BaseViewModel
    {
        // Services
        private RestService _restService;
        public ICommand FindMentorsCommand { get; set; }

        public ICommand FindStudentsCommand { get; set; }

        public ObservableCollection<AcademicInterest> AcademicInterestsForPicker { get; set; }

        public HomeViewModel()
        {
            _restService = new RestService();
            IsStudent = UserInfo.User.UserType.ToLower().Equals("student");
            IsMentor = !IsStudent;
            AcademicInterestsForPicker = new ObservableCollection<AcademicInterest>(InMemoryData.AcademicInterests);
            FindMentorsCommand = new Command(FindMentors);
            FindStudentsCommand = new Command(FindStudents);
        }

        public Action<List<UserInfo>> OnFindMentors = delegate { };
        public Action<List<UserInfo>> OnFindStudents = delegate { };

        private async void FindMentors()
        {
            List<UserInfo> mentors = new List<UserInfo>();
            if (SelectedAcademicInterest.Id != null)
            {
                var results = await _restService.GetMentorsForAcademicInterest(SelectedAcademicInterest.Id);
                foreach (var result in results)
                {
                    mentors.Add(result);
                }
            }
            OnFindMentors(mentors);
        }

        public async void FindStudents()
        {
            List<UserInfo> students = new List<UserInfo>();
            if (SelectedAcademicInterest.Id != null)
            {
                var results = await _restService.GetMentorsForAcademicInterest(SelectedAcademicInterest.Id);
                foreach (var result in results)
                {
                    students.Add(result);
                }
            }
            OnFindStudents(students);
        }


        public async Task RequestMentorship(string mentorUserId)
        {
            var service = new RestService();
            var mentorshipRequest = await service.RequestMentorship(mentorUserId, UserInfo.User.UserId);
            if (mentorshipRequest.User != null)
            {
                // TODO: create pending notification
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

        private AcademicInterest _selectedAcademicInterest;
        public AcademicInterest SelectedAcademicInterest
        {
            get => _selectedAcademicInterest;
            set
            {
                _selectedAcademicInterest = value;
                OnPropertyChanged("SelectedAcademicInterest");
            }
        }
    }
}
