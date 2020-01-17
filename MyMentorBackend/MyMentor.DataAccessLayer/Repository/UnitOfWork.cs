using System;
using System.Collections.Generic;
using System.Text;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Users
        private IUserRepository _users;
        public IUserRepository Users => _users ??= new UserRepository(_context);

        // Teachers
        ITeacherRepository _teachers;
        public ITeacherRepository Teachers => _teachers ??= new TeacherRepository(_context);

        IStudentRepository _students;
        public IStudentRepository Students => _students ??= new StudentRepository(_context);

        private IStudentMentorRepository _studentMentors;
        public IStudentMentorRepository StudentMentors => _studentMentors ??= new StudentMentorRepository(_context);

        private IUserInterestRepository _userInterests;
        public IUserInterestRepository UserInterests => _userInterests ??= new UserInterestRepository(_context);

        private IAcademicInterestRepository _academicInterests;
        public IAcademicInterestRepository AcademicInterests => _academicInterests ??=new AcademicInterestRepository(_context);

        private INotificationRepository _notifications;
        public INotificationRepository Notifications => _notifications ??= new NotificationRepository(_context);


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
