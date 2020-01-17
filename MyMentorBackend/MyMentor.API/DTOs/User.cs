using MyMentor.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMentor.API.DTOs
{
    public class UserDTO
    {
        public string UserId { get; set; }

        public string UserType { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string EducationLevel { get; set; }
        public string University { get; set; }
        public AcademicInterestDTO AcademicFieldOfStudy { get; set; }

        public UserDTO() { }

        public UserDTO(ApplicationUser applicationUser)
        {
            UserId = applicationUser.Id;
            UserType = "Teacher";
            Username = applicationUser.UserName;
            FullName = applicationUser.FullName;
            Password = applicationUser.PasswordHash;
            Email = applicationUser.Email;
            if (applicationUser.Student != null)
            {
                EducationLevel = applicationUser.Student.Grade;
                AcademicFieldOfStudy = new AcademicInterestDTO(applicationUser.Student.Major);
                University = applicationUser.Student.University;
                UserType = "Student";
            }
            else if (applicationUser.Teacher != null)
            {
                EducationLevel = applicationUser.Teacher.DegreeLevel;
                AcademicFieldOfStudy = new AcademicInterestDTO(applicationUser.Teacher.Degree);
                University = applicationUser.Teacher.University;
                UserType = "Teacher";
            }
            else
            {
                throw new NotSupportedException("Invalid User TYpe");
            }
        }
    }
}
