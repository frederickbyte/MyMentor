using MyMentor.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyMentor.App.ConnectionProfile
{
    public class ConnectionProfileViewModel : MVVM.BaseViewModel
    {
        public ObservableCollection<AcademicInterest> AcademicInterests { get; set; }
        public ConnectionProfileViewModel()
        {
        }

        public ConnectionProfileViewModel(UserInfoWithAvatar userProfile)
        {
            AcademicInterests = new ObservableCollection<AcademicInterest>(userProfile.UserInfo.AcademicInterests);
            ProfileName = userProfile.UserInfo.User.Username;
            FullName = userProfile.UserInfo.User.FullName;
            IsMentor = userProfile.UserInfo.User.UserType.ToLower().Equals("teacher");
            IsStudent = !IsMentor;
            IsCurrentUserMentor = UserInfo.User.UserType.ToLower().Equals("teacher");
            IsCurrentUserStudent = !IsCurrentUserMentor;
            University = userProfile.UserInfo.User.University;
            Classification = userProfile.UserInfo.User.UserType;
            EducationLevel = userProfile.UserInfo.User.EducationLevel;
            AvatarImage = userProfile.AvatarImage;
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

        private bool _isCurrentUserStudent;
        public bool IsCurrentUserStudent
        {
            get { return _isCurrentUserStudent; }
            set
            {
                _isCurrentUserStudent = value;
                OnPropertyChanged("IsCurrentUserStudent");
            }
        }

        private bool _isCurrentUserMentor;
        public bool IsCurrentUserMentor
        {
            get { return _isCurrentUserMentor; }
            set
            {
                _isCurrentUserMentor = value;
                OnPropertyChanged("IsCurrentUserMentor");
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
    }
}
