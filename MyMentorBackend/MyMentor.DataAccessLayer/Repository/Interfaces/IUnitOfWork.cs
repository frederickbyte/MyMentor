namespace MyMentor.DataAccessLayer.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        IStudentMentorRepository StudentMentors { get; }
        IAcademicInterestRepository AcademicInterests { get; }
        IUserInterestRepository UserInterests { get; }
        INotificationRepository Notifications { get;  }
        int SaveChanges();
    }
}
