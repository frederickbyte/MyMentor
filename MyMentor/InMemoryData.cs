using System;
using System.Collections.Generic;
using MyMentor.Accounts;

namespace MyMentor
{
    public static class InMemoryData
    {
        private static List<AcademicInterest> _academicInterests;

        public static List<AcademicInterest> AcademicInterests
        {
            get
            {
                if (_academicInterests == null)
                {
                    _academicInterests = new List<AcademicInterest>
                    {
                        new AcademicInterest {Id = new Guid("F559D18B-72D3-4D5F-A10C-2A08377F662B"), Name = "Architecture"},
                        new AcademicInterest {Id = new Guid("F5684086-5344-4419-91CD-3F52DF6C35BE"), Name = "Art"},
                        new AcademicInterest {Id = new Guid("70AA3710-BCA6-478F-8A18-2C065E3E3611"), Name = "Biology"},
                        new AcademicInterest {Id = new Guid("3D0B5B14-6CD0-4943-9FA7-FC8AA1AE9E83"), Name = "Chemistry"},
                        new AcademicInterest {Id = new Guid("A7DDD3DA-05E5-4D91-BDD8-3A5A3FC04F40"), Name = "Computer Science"},
                        new AcademicInterest {Id = new Guid("CD5EAC5F-3094-48BC-8EFA-D2EC9776B754"), Name = "Dance"},
                        new AcademicInterest {Id = new Guid("C3FB0D06-401C-4467-BFF4-03A86BEB22FD"), Name = "English"},
                        new AcademicInterest {Id = new Guid("A63A22FA-E066-47A5-BAFD-70185008DF0B"), Name = "Mathematics"},
                        new AcademicInterest {Id = new Guid("1C9AECC7-9533-458E-91FD-FA1198CBCEBA"), Name = "Music"},
                        new AcademicInterest {Id = new Guid("D61DC0E3-9604-4D81-A81E-64EB9213DFF0"), Name = "Physics"},
                        new AcademicInterest {Id = new Guid("ED814788-C1C3-4090-AF92-4CB27CF1E282"), Name = "Chemical Engineering"},
                        new AcademicInterest {Id = new Guid("420D4918-63DC-4A4E-81A7-895C6EC30EAE"), Name = "Civil Engineering"},
                        new AcademicInterest {Id = new Guid("AFBB4A6C-8ADF-46E7-872B-A9E8D3D99251"), Name = "Electrical Engineering"},
                        new AcademicInterest {Id = new Guid("694B6CF4-6BC4-4BC5-AB57-1F78FDECEDB1"), Name = "Mechanical Engineering"}
                    };
                }
                return _academicInterests;
            }
        }

        private static List<CommunityEvent> _communityEvents;

        public static List<CommunityEvent> CommunityEvents
        {
            get
            {
                if (_communityEvents == null)
                {
                    _communityEvents = new List<CommunityEvent>
                    {
                        new CommunityEvent{Title =
                            "Coffee after Thursday's class",
                        Description = "Come relax and reflect on the semester with some cold brew after class.",
                        Location = "PFT Main Lobby",
                        EventDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 5, 30, 00)
                        },
                        new CommunityEvent{Title =
                            "Group study for Video Game Design",
                        Description = "Studying for our final next week.",
                        Location = "PFT 1263",
                        EventDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 7, 12, 30, 00)
                        },
                        new CommunityEvent{Title =
                            "Code review for Java project",
                        Description = "Final code review before submitting our project",
                        Location = "Highland Coffee",
                        EventDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 6, 4, 15, 00)
                        },
                        new CommunityEvent{Title = "Final exam review",
                        Description = "Reviewing PPT slides and class notes.",
                        Location = "Panera Bread lobby area",
                        EventDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 9, 5, 00, 00)
                        }
                    };
                }
                return _communityEvents;
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

        public static Dictionary<int, string> DefaultAvatars = new Dictionary<int, string>
        {
            {1, "stock_male_1.png" },
            {2, "stock_female_3.png" },
            {3, "stock_male_5.png" },
            {4, "stock_male_2.png" },
            {5, "stock_female_4.png" },
            {6, "stock_male_6.png" },
            {7, "stock_female_1.png" },
            {8, "stock_male_3.png" },
            {9, "stock_female_3.png" },
            {10, "stock_male_1.png" },
            {11, "stock_female_4.png" },
            {12, "stock_male_4.png" }
        };
    }
}
