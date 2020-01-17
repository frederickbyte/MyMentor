using System;
using MyMentor.DataAccessLayer.Models;

namespace MyMentor.API.DTOs
{
    public class AcademicInterestDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public AcademicInterestDTO()
        { }

        public AcademicInterestDTO(AcademicInterest academicInterest)
        {
            Id = academicInterest.Id;
            Name = academicInterest.Name;
        }
    }
}