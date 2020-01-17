using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MyMentor.Accounts;
using MyMentor.Services;
using Xamarin.Forms;

namespace MyMentor.App.Connections
{
    public class ConnectionsViewModel : MVVM.BaseViewModel
    {
        private RestService _restService;

        public ObservableCollection<UserInfoWithAvatar> Mentors { get; set; }

        public ObservableCollection<UserInfoWithAvatar> Students { get; set; }

        public string AvatarImage { get; set; }

        public ICommand ViewProfileCommand { get; set; }

        public ICommand ManageConnectionsCommand { get; set; }
        public ConnectionsViewModel()
        {
            _restService = new RestService();
            Mentors = new ObservableCollection<UserInfoWithAvatar>();
            Students = new ObservableCollection<UserInfoWithAvatar>();
            IsStudent = UserInfo.User.UserType.ToLower().Equals("student");
            IsMentor = !IsStudent;
            ViewProfileCommand = new Command<UserInfoWithAvatar>(ViewProfile);
            LoadData();
        }

        public Action<UserInfoWithAvatar> OnViewConnectionProfile = delegate { };

        public void ViewProfile(UserInfoWithAvatar connection)
        {
            OnViewConnectionProfile(connection);
        }
        // Load the appropriate data for the logged in user (i.e. get students for mentors or get mentors for a student).
        public void LoadData()
        {
            if (IsStudent)
            {
                LoadMentors();
            }
            else
            {
                LoadStudents();
            }
        }

        public async void LoadMentors()
        {
            var mentorsForStudent = await _restService.GetMentorsForStudent(UserInfo.User.UserId);
            var avatarCounter = 1;
            foreach (var mentor in mentorsForStudent)
            {
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
                Mentors.Add(new UserInfoWithAvatar(mentor, avatarImageString, academicInterestsAsSingleString));
                avatarCounter++;
            }
            //Mentors.Add(new UserInfoWithAvatar { Name = "John Smith", Type = "Teacher", Interests = "Biology", AvatarImage = "stock_male_1.png"});
            //Mentors.Add(new UserInfoWithAvatar { Name = "Mary Johnson", Type = "Student", Interests = "Computer Science, Cybersecurity", AvatarImage = "stock_female_1.png" });
            //Mentors.Add(new UserInfoWithAvatar { Name = "Sally Waltz", Type = "Teacher", Interests = "Computer Science, Databases", AvatarImage = "stock_female_4.png" });
            //Mentors.Add(new UserInfoWithAvatar { Name = "Willie Maze", Type = "Teacher", Interests = "History", AvatarImage = "stock_male_2.png" });
            //Mentors.Add(new UserInfoWithAvatar { Name = "Tyler Bennet", Type = "Student", Interests = "English", AvatarImage = "stock_male_3.png" });
            //Mentors.Add(new UserInfoWithAvatar { Name = "Jeremy Brown", Type = "Teacher", Interests = "Mechanical Engineering", AvatarImage = "stock_male_4.png" });
            //Mentors.Add(new UserInfoWithAvatar { Name = "Drew Maizie", Type = "Teacher", Interests = "Calculus", AvatarImage = "stock_male_5.png" });
            //Mentors.Add(new UserInfoWithAvatar { Name = "Walter White", Type = "Teacher", Interests = "Statistics", AvatarImage = "stock_male_6.png" });
        }

        // Sample student data for a mentor.
        public async void LoadStudents()
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
                Students.Add(new UserInfoWithAvatar(student, avatarImageString, academicInterestsAsSingleString));
                avatarCounter++;
            }
            //Students.Add(new UserInfoWithAvatar { Name = "Jeremy Boudreaux", Classification = "Undergraduate", Interests = "Biology", AvatarImage = "stock_male_1.png" });
            //Students.Add(new UserInfoWithAvatar { Name = "Samuel Walton", Classification = "Undergraduate", Interests = "Chemistry", AvatarImage = "stock_male_2.png" });
            //Students.Add(new UserInfoWithAvatar { Name = "Melissa Williams", Classification = "Undergraduate", Interests = "Computer Science", AvatarImage = "stock_female_1.png" });
            //Students.Add(new UserInfoWithAvatar { Name = "Sandra Connors", Classification = "Graduate", Interests = "Computer Science", AvatarImage = "stock_female_3.png" });
            //Students.Add(new UserInfoWithAvatar { Name = "Tyler St. Marie", Classification = "Undergraduate", Interests = "Computer Science", AvatarImage = "stock_male_3.png" });
            //Students.Add(new UserInfoWithAvatar { Name = "Jeremy Bordelon", Classification = "Graduate", Interests = "Mechanical Engineering", AvatarImage = "stock_male_4.png" });
            //Students.Add(new UserInfoWithAvatar { Name = "Daniel Hunter", Classification = "Undergraduate", Interests = "Calculus", AvatarImage = "stock_male_5.png" });
            //Students.Add(new UserInfoWithAvatar { Name = "Jessica Maler", Classification = "Graduate", Interests = "Statistics", AvatarImage = "stock_female_4.png" });
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
