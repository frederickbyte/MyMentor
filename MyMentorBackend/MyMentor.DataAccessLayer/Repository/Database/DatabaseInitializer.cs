using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyMentor.DataAccessLayer.Auth;
using MyMentor.DataAccessLayer.Auth.Interfaces;
using MyMentor.DataAccessLayer.Models;

namespace MyMentor.DataAccessLayer.Repository.Database
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }




    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
        }
        


        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            // Add academic interests if none exist
            if (!await _context.AcademicInterests.AnyAsync())
            {
                var academicInterests = new List<AcademicInterest>
                {
                    new AcademicInterest { Id = AcademicInterestIdentifiers.Architecture, Name = "Architecture" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.Art, Name = "Art" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.Biology, Name = "Biology" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.ChemicalEngineering, Name = "Chemical Engineering" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.Chemistry, Name = "Chemistry" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.CivilEngineering, Name = "Civil Engineering" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.ComputerScience, Name = "Computer Science" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.Dance, Name = "Dance" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.ElectricalEngineering, Name = "Electrical Engineering" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.English, Name = "English" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.History, Name = "History" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.Mathematics, Name = "" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.MechanicalEngineering, Name = "Mechanical Engineering" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.Music, Name = "Music" },
                    new AcademicInterest { Id = AcademicInterestIdentifiers.Physics, Name = "Physics" },
                };

                _context.AcademicInterests.AddRange(academicInterests);
                await _context.SaveChangesAsync();
            }



            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Generating default accounts");

                const string teacherRole = "Teacher";
                const string studentRole = "Student";

                await EnsureRoleAsync(teacherRole, "Teacher", new string[] { });
                await EnsureRoleAsync(studentRole, "Student", new string[] { });

                // Students
                await CreateUserAsync(StudentIdentifiers.DonaldBoudreaux, "dboudreaux", "p@ssW0rd", "Donald Boudreaux", "dboudreaux@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.JamesArmstrong, "jarmstrong", "p@ssW0rd", "James Armstrong", "jarmstrong@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.JohnSingleton, "jsingleton", "p@ssW0rd", "John Singleton", "jsingleton@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.MarissaThomas, "mthomas", "p@ssW0rd", "Marissa Thomas", "mthomas@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.MarkReynolds, "mreynolds", "p@ssW0rd", "Mark Reynolds", "mreynolds@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.MelissaSmith, "msmith", "p@ssW0rd", "Melissa Smith", "dboudreaux@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.NikolaTesla, "ntesla", "p@ssW0rd", "NikolaTesla", "msmith@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.SamanthaLittleton, "slittleton", "p@ssW0rd", "Samantha Littleton", "slittleton@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.SarahJohnson, "sjohnson", "p@ssW0rd", "Sarah Johnson", "sjohnson@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });
                await CreateUserAsync(StudentIdentifiers.WilliamMcDonald, "wmcDonald", "p@ssW0rd", "William McDonald", "wmcDonald@mymentor.com", "+1 (123) 000-0000", new string[] { studentRole });


                // Teachers
                await CreateUserAsync(TeacherIdentifiers.AnasMahmoud, "amahmoud", "p@ssW0rd", "Anas Mahmoud", "amahmoud@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.ChristopherThibodeaux, "cthibodeaux", "p@ssW0rd", "Christopher Thibodeaux", "cthibodeaux@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.GoldenGRichard, "gRichard", "p@ssW0rd", "Golden G. Richard", "gRichard@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.JeremyDaniels, "jdaniels", "p@ssW0rd", "Jeremy Daniels", "jdaniels@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.MatthewSingleton, "msingleton", "p@ssW0rd", "Matthew Singleton", "msingleton@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.NickVillanueva, "nvillanueva", "p@ssW0rd", "Nick Villanueva", "nvillanueva@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.NickiReynolds, "nreynolds", "p@ssW0rd", "Nicki Reynolds", "nreynolds@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.PattiAymond, "paymond", "p@ssW0rd", "Patti Aymond", "paymond@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.SarahMasters, "smasters", "p@ssW0rd", "Sarah Masters", "smasters@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });
                await CreateUserAsync(TeacherIdentifiers.SydneyThompson, "sthompson", "p@ssW0rd", "Sydney Thompson", "sthompson@mymentor.com", "+1 (123) 000-0000", new string[] { teacherRole });




                _logger.LogInformation("Account generation completed");
            }

            if (!await _context.UserInterests.AnyAsync())
            {
                var userInterests = new List<UserInterest>
                {
                    // Student Interests
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.NikolaTesla, AcademicInterestId = AcademicInterestIdentifiers.Chemistry},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.NikolaTesla, AcademicInterestId = AcademicInterestIdentifiers.ComputerScience},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.NikolaTesla, AcademicInterestId = AcademicInterestIdentifiers.Physics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.JohnSingleton, AcademicInterestId = AcademicInterestIdentifiers.Biology},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.JohnSingleton, AcademicInterestId = AcademicInterestIdentifiers.ComputerScience},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.JohnSingleton, AcademicInterestId = AcademicInterestIdentifiers.MechanicalEngineering},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MelissaSmith, AcademicInterestId = AcademicInterestIdentifiers.History},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MelissaSmith, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MelissaSmith, AcademicInterestId = AcademicInterestIdentifiers.Physics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.SarahJohnson, AcademicInterestId = AcademicInterestIdentifiers.Art},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.SarahJohnson, AcademicInterestId = AcademicInterestIdentifiers.History},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.SarahJohnson, AcademicInterestId = AcademicInterestIdentifiers.Music},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MarkReynolds, AcademicInterestId = AcademicInterestIdentifiers.Architecture},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MarkReynolds, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MarkReynolds, AcademicInterestId = AcademicInterestIdentifiers.Physics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MarissaThomas, AcademicInterestId = AcademicInterestIdentifiers.Art},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MarissaThomas, AcademicInterestId = AcademicInterestIdentifiers.History},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MarissaThomas, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.JamesArmstrong, AcademicInterestId = AcademicInterestIdentifiers.ChemicalEngineering},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.JamesArmstrong, AcademicInterestId = AcademicInterestIdentifiers.Chemistry},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.JamesArmstrong, AcademicInterestId = AcademicInterestIdentifiers.History},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.DonaldBoudreaux, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.DonaldBoudreaux, AcademicInterestId = AcademicInterestIdentifiers.Physics},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.DonaldBoudreaux, AcademicInterestId = AcademicInterestIdentifiers.Music},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.WilliamMcDonald, AcademicInterestId = AcademicInterestIdentifiers.ComputerScience},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.WilliamMcDonald, AcademicInterestId = AcademicInterestIdentifiers.English},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.WilliamMcDonald, AcademicInterestId = AcademicInterestIdentifiers.Physics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.SamanthaLittleton, AcademicInterestId = AcademicInterestIdentifiers.English},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.SamanthaLittleton, AcademicInterestId = AcademicInterestIdentifiers.History},
                    new UserInterest {Id = Guid.NewGuid(), UserId = StudentIdentifiers.SamanthaLittleton, AcademicInterestId = AcademicInterestIdentifiers.CivilEngineering},

                    // Teacher Interests
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.AnasMahmoud, AcademicInterestId = AcademicInterestIdentifiers.ComputerScience},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.AnasMahmoud, AcademicInterestId = AcademicInterestIdentifiers.MechanicalEngineering},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.AnasMahmoud, AcademicInterestId = AcademicInterestIdentifiers.Physics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.PattiAymond, AcademicInterestId = AcademicInterestIdentifiers.ComputerScience},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.PattiAymond, AcademicInterestId = AcademicInterestIdentifiers.CivilEngineering},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.PattiAymond, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.GoldenGRichard, AcademicInterestId = AcademicInterestIdentifiers.ComputerScience},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.GoldenGRichard, AcademicInterestId = AcademicInterestIdentifiers.CivilEngineering},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.GoldenGRichard, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.JeremyDaniels, AcademicInterestId = AcademicInterestIdentifiers.ComputerScience},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.JeremyDaniels, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.JeremyDaniels, AcademicInterestId = AcademicInterestIdentifiers.Physics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.SarahMasters, AcademicInterestId = AcademicInterestIdentifiers.Biology},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.SarahMasters, AcademicInterestId = AcademicInterestIdentifiers.English},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.ChristopherThibodeaux, AcademicInterestId = AcademicInterestIdentifiers.Biology},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.ChristopherThibodeaux, AcademicInterestId = AcademicInterestIdentifiers.CivilEngineering},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.ChristopherThibodeaux, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.NickVillanueva, AcademicInterestId = AcademicInterestIdentifiers.Chemistry},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.NickVillanueva, AcademicInterestId = AcademicInterestIdentifiers.History},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.NickiReynolds, AcademicInterestId = AcademicInterestIdentifiers.Architecture},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.NickiReynolds, AcademicInterestId = AcademicInterestIdentifiers.History},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.NickiReynolds, AcademicInterestId = AcademicInterestIdentifiers.Mathematics},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.MatthewSingleton, AcademicInterestId = AcademicInterestIdentifiers.ComputerScience},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.MatthewSingleton, AcademicInterestId = AcademicInterestIdentifiers.ChemicalEngineering},

                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.SydneyThompson, AcademicInterestId = AcademicInterestIdentifiers.Architecture},
                    new UserInterest {Id = Guid.NewGuid(), UserId = TeacherIdentifiers.SydneyThompson, AcademicInterestId = AcademicInterestIdentifiers.MechanicalEngineering},

                };
                _context.UserInterests.AddRange(userInterests);
                await _context.SaveChangesAsync();
            }

            if (!await _context.Students.AnyAsync())
            {
                var students = new List<Student>
                {
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.NikolaTesla, MajorId = AcademicInterestIdentifiers.ComputerScience, Grade = "Freshman", University = "LSU"},
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.JohnSingleton, MajorId = AcademicInterestIdentifiers.MechanicalEngineering, Grade = "Sophomore", University = "LSU"},
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MelissaSmith, MajorId = AcademicInterestIdentifiers.ComputerScience, Grade = "Freshman", University = "LSU"},
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.SarahJohnson, MajorId = AcademicInterestIdentifiers.Music,  Grade = "Senior", University = "LSU"},
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MarkReynolds, MajorId = AcademicInterestIdentifiers.ComputerScience ,  Grade = "Freshman", University = "LSU"},
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.MarissaThomas, MajorId = AcademicInterestIdentifiers.History ,  Grade = "Freshman", University = "LSU"},
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.JamesArmstrong, MajorId = AcademicInterestIdentifiers.ChemicalEngineering ,  Grade = "Sophomore", University = "LSU"},
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.DonaldBoudreaux, MajorId = AcademicInterestIdentifiers.Physics , Grade = "Sophomore", University = "LSU" },
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.WilliamMcDonald, MajorId = AcademicInterestIdentifiers.ComputerScience ,  Grade = "Freshman", University = "LSU"},
                    new Student {Id = Guid.NewGuid(), UserId = StudentIdentifiers.SamanthaLittleton, MajorId = AcademicInterestIdentifiers.English ,  Grade = "Freshman", University = "LSU"},
                };
                _context.Students.AddRange(students);
                await _context.SaveChangesAsync();
            }

            if (!await _context.Teachers.AnyAsync())
            {
                var teachers = new List<Teacher>
                {
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="PhD", UserId = TeacherIdentifiers.AnasMahmoud, DegreeId = AcademicInterestIdentifiers.ComputerScience, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="PhD", UserId = TeacherIdentifiers.PattiAymond, DegreeId = AcademicInterestIdentifiers.ComputerScience, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="PhD", UserId = TeacherIdentifiers.GoldenGRichard, DegreeId = AcademicInterestIdentifiers.ComputerScience, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="Masters", UserId = TeacherIdentifiers.JeremyDaniels, DegreeId = AcademicInterestIdentifiers.Mathematics, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="PhD", UserId = TeacherIdentifiers.SarahMasters, DegreeId = AcademicInterestIdentifiers.English, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="PhD", UserId = TeacherIdentifiers.ChristopherThibodeaux, DegreeId = AcademicInterestIdentifiers.Biology, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="Masters", UserId = TeacherIdentifiers.NickVillanueva, DegreeId = AcademicInterestIdentifiers.Chemistry, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="PhD", UserId = TeacherIdentifiers.NickiReynolds, DegreeId = AcademicInterestIdentifiers.History, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="Bachelors", UserId = TeacherIdentifiers.MatthewSingleton, DegreeId = AcademicInterestIdentifiers.ComputerScience, University = "LSU"},
                    new Teacher{Id = Guid.NewGuid(), DegreeLevel="PhD", UserId = TeacherIdentifiers.SydneyThompson, DegreeId = AcademicInterestIdentifiers.Architecture, University = "LSU"},
                };
                _context.Teachers.AddRange(teachers);
                await _context.SaveChangesAsync();
            }



            if (!await _context.StudentMentors.AnyAsync())
            {
                var studentMentors = new List<StudentMentor>
                {
                    new StudentMentor{ Id = Guid.NewGuid(), ApprovalStatus = "Approved", StudentId = StudentIdentifiers.NikolaTesla, MentorId = TeacherIdentifiers.AnasMahmoud},
                    new StudentMentor{ Id = Guid.NewGuid(), ApprovalStatus = "Approved", StudentId = StudentIdentifiers.NikolaTesla, MentorId = TeacherIdentifiers.PattiAymond},
                    new StudentMentor{ Id = Guid.NewGuid(), ApprovalStatus = "Approved", StudentId = StudentIdentifiers.NikolaTesla, MentorId = TeacherIdentifiers.MatthewSingleton},
                };
                _context.StudentMentors.AddRange(studentMentors);
                await _context.SaveChangesAsync();
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole(roleName, description);

                var result = await this._accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Succeeded)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");
            }
        }

        private async Task<ApplicationUser> CreateUserAsync(string id, string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Id = id,
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Succeeded)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");


            return applicationUser;
        }

    }

    public static class AcademicInterestIdentifiers
    {
        public static Guid Architecture = new Guid("F559D18B-72D3-4D5F-A10C-2A08377F662B");
        public static Guid Art = new Guid("F5684086-5344-4419-91CD-3F52DF6C35BE");
        public static Guid Biology = new Guid("70AA3710-BCA6-478F-8A18-2C065E3E3611");
        public static Guid Chemistry = new Guid("3D0B5B14-6CD0-4943-9FA7-FC8AA1AE9E83");
        public static Guid ComputerScience = new Guid("A7DDD3DA-05E5-4D91-BDD8-3A5A3FC04F40");
        public static Guid Dance = new Guid("CD5EAC5F-3094-48BC-8EFA-D2EC9776B754");
        public static Guid English = new Guid("C3FB0D06-401C-4467-BFF4-03A86BEB22FD");
        public static Guid History = new Guid("044FBF61-0280-44FC-BDE3-C18666FBB53D");
        public static Guid Mathematics = new Guid("A63A22FA-E066-47A5-BAFD-70185008DF0B");
        public static Guid Music = new Guid("1C9AECC7-9533-458E-91FD-FA1198CBCEBA");
        public static Guid Physics = new Guid("D61DC0E3-9604-4D81-A81E-64EB9213DFF0");
        public static Guid ChemicalEngineering = new Guid("ED814788-C1C3-4090-AF92-4CB27CF1E282");
        public static Guid CivilEngineering = new Guid("420D4918-63DC-4A4E-81A7-895C6EC30EAE");
        public static Guid ElectricalEngineering = new Guid("AFBB4A6C-8ADF-46E7-872B-A9E8D3D99251");
        public static Guid MechanicalEngineering = new Guid("694B6CF4-6BC4-4BC5-AB57-1F78FDECEDB1");
    }

    public static class StudentIdentifiers
    {
        public static string NikolaTesla = "EDB1574B-4CE2-40CE-BFD1-B4C2D5B78DFD";
        public static string JohnSingleton = "D0FB8E15-C9DF-4C3A-8D11-3596003C6BB8";
        public static string MelissaSmith = "D0A1BB18-EBD4-4E5B-BC07-C7A2B61570AE";
        public static string SarahJohnson = "8B175C71-9179-4875-B664-8996EF41FAB8";
        public static string MarkReynolds = "6E432B71-6580-4228-8695-E2AC955CE758";
        public static string MarissaThomas = "DA69EC46-6499-4CA0-9D4C-80F8EC29F929";
        public static string JamesArmstrong = "D9A09FE9-FCA4-4E67-8C30-1E5E46392D17";
        public static string DonaldBoudreaux = "BE4331F5-A2F9-4983-B806-BB5FB5F0E3C7";
        public static string WilliamMcDonald = "2D7794B1-9DC2-4C07-B26E-8109222EA305";
        public static string SamanthaLittleton = "61C48AFB-B7C0-4A94-AED5-5B921AE1736F";
    }

    public static class TeacherIdentifiers
    {
        public static string AnasMahmoud = "02949849-C53F-4DF7-A5FE-9FCAEA2345AD";
        public static string PattiAymond = "FF7927A4-DC53-44A8-9685-D007E0A8C0E2";
        public static string GoldenGRichard = "DD74FA3A-A38E-4EB5-BB3A-F68F5206268C";
        public static string JeremyDaniels = "64B54B74-E1C2-465C-981B-03452236A8E1";
        public static string SarahMasters = "95EC7D83-8BD7-43E4-A7AD-1587D8E4C636";
        public static string ChristopherThibodeaux = "4F9040BA-F034-49C1-8078-20B97E37B435";
        public static string NickVillanueva = "82FC7D07-3CE4-427F-935C-D2662E0B3F15";
        public static string NickiReynolds = "FC534089-DE3E-4394-A309-7EC79D9575F7";
        public static string MatthewSingleton = "E0B0CFCC-BC76-477F-9DF3-BAA7806BDC08";
        public static string SydneyThompson = "847DC204-950F-4F8C-8AF2-408A255BADC1";
    }
}
