using System;
using System.Collections.Generic;

namespace MyMentor.Accounts
{
    public class User
    {
        public string UserId { get; set; }

        public string UserType { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string EducationLevel { get; set; }

        public string University { get; set; }

        public User() { }

        public User(string userId, string userType, string username, string password, string email, string educationLevel, string university)
        {
            UserId = userId;
            UserType = userType;
            Username = username;
            Password = password;
            Email = email;
            EducationLevel = educationLevel;
            University = university;
        }
    }

    public class AcademicInterest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public AcademicInterest()
        { }

        public AcademicInterest(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class UserInfo
    {
        public User User { get; set; }
        public List<AcademicInterest> AcademicInterests { get; set; }

        public UserInfo()
        { }

        public UserInfo(User user, List<AcademicInterest> academicInterests)
        {
            User = user;
            AcademicInterests = academicInterests;
        }
    }

    public class UserInfoWithAvatar
    {
        public UserInfo UserInfo { get; set; }
        public string AvatarImage { get; set; }
        public string AcademicInterestsAsString { get; set; }

        public UserInfoWithAvatar()
        { }

        public UserInfoWithAvatar(UserInfo userInfo, string avatarImage, string academicInterestsAsString)
        {
            UserInfo = userInfo;
            AvatarImage = avatarImage;
            AcademicInterestsAsString = academicInterestsAsString;
        }
    }
}
