using MyMentor.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MyMentor.Accounts;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MyMentor.App.UserProfile
{
    public class UserProfileViewModel : MVVM.BaseViewModel
    {
        private RestService RestService;
        // List of logged in user's academic interests
        public ObservableCollection<AcademicInterest> AcademicInterests { get; set; }
        public ICommand AddAcademicInterestCommand { get; set; }
        public ICommand UpdateProfileCommand { get; set; }
        public UserProfileViewModel()
        {
            RestService = new RestService();
            AcademicInterests = new ObservableCollection<AcademicInterest>();
            IsStudent = UserInfo.User.UserType.ToLower().Equals("student");
            IsMentor = !IsStudent;
            ProfileName = UserInfo.User.Username;
            FullName = UserInfo.User.FullName;
            Classification = UserInfo.User.UserType;
            University = UserInfo.User.University;
            EducationLevel = UserInfo.User.EducationLevel;
            AssociationCount = 9;
            PostCount = 2;
            TotalMatchesCount = 10;
            AvatarImage = GetAvatarImageResource();
            LoadAcademicInterests();
            AddAcademicInterestCommand = new Command(OnAddAcademicInterestClick);
            UpdateProfileCommand = new Command(OnUpdateProfileClick);
        }

        public Action OnAddAcademicInterest = delegate { };
        public Action OnUpdateProfile = delegate { };

        private void OnAddAcademicInterestClick()
        {
            OnAddAcademicInterest();
        }

        public void AddAcademicInterest(string viewModelToAdd)
        {
            AcademicInterests.Add(new AcademicInterest { Id = Guid.NewGuid(), Name = viewModelToAdd });
        }

        private void OnUpdateProfileClick()
        {
            OnUpdateProfile();
        }

        public void UpdateProfileName(string newName)
        {
            ProfileName = newName;
        }

        private string GetAvatarImageResource()
        {
            return "stock_male_3.png";
        }

        public async void LoadAcademicInterests()
        {
            var items = await RestService.GetAcademicInterests();
            items.ForEach(ai => AcademicInterests.Add(ai));
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

        private string _profileName;
        public string ProfileName
        {
            get { return _profileName; }
            set
            {
                _profileName = value;
                OnPropertyChanged("ProfileName");
            }
        }


        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        private string _classification;
        public string Classification
        {
            get { return _classification; }
            set
            {
                _classification = value;
                OnPropertyChanged("Classification");
            }
        }

        private string _university;
        public string University
        {
            get { return _university; }
            set
            {
                _university = value;
                OnPropertyChanged("University");
            }
        }

        private string _educationLevel;
        public string EducationLevel
        {
            get { return _educationLevel; }
            set
            {
                _educationLevel = value;
                OnPropertyChanged("EducationLevel");
            }
        }

        private string _avatarImage;
        public string AvatarImage
        {
            get { return _avatarImage; }
            set
            {
                _avatarImage = value;
                OnPropertyChanged("AvatarImage");
            }
        }

        private int _associationCount;
        public int AssociationCount
        {
            get { return _associationCount; }
            set
            {
                _associationCount = value;
                OnPropertyChanged("AssociationCount");
            }
        }

        private int _postCount;
        public int PostCount
        {
            get { return _postCount; }
            set
            {
                _postCount = value;
                OnPropertyChanged("PostCount");
            }
        }

        private int _totalMatchesCount;
        public int TotalMatchesCount
        {
            get { return _totalMatchesCount; }
            set
            {
                _totalMatchesCount = value;
                OnPropertyChanged("TotalMatchesCount");
            }
        }
    }
}