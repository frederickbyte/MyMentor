using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMentor.API.DTOs;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMentorController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public StudentMentorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{studentUserId}/mentors")]
        public ActionResult<IEnumerable<UserInfoDTO>> GetMentorsForStudent(string studentUserId)
        {
            return _unitOfWork.StudentMentors.GetMentorsForStudent(studentUserId)
                .Select(sm => new UserInfoDTO(sm.Mentor)).ToList();
        }

        [HttpGet("{studentUserId}/mentoringRequests")]
        public ActionResult<IEnumerable<UserInfoDTO>> GetRequestedMentorsForStudent(string studentUserId)
        {
            return _unitOfWork.StudentMentors.GetRequestedMentorsForStudent(studentUserId)
                .Select(sm => new UserInfoDTO(sm.Mentor)).ToList();
        }

        [HttpGet("{mentorUserId}/mentorshipRequests")]
        public ActionResult<IEnumerable<UserInfoDTO>> GetMentorRequestsForMentor(string mentorUserId)
        {
            return _unitOfWork.StudentMentors.GetMentorRequestsForMentor(mentorUserId)
                .Select(sm => new UserInfoDTO(sm.Student)).ToList();
        }

        [HttpGet("{mentorUserId}/mentorships")]
        public ActionResult<IEnumerable<UserInfoDTO>> GetMentorshipsForMentor(string mentorUserId)
        {
            return _unitOfWork.StudentMentors.GetMentorshipsForMentor(mentorUserId)
                .Select(sm => new UserInfoDTO(sm.Student)).ToList();
        }

        [HttpGet("{mentorUserId}/acceptMentorship/{studentUserId}")]
        public void AcceptMentorship(string mentorUserId, string studentUserId)
        {
            _unitOfWork.StudentMentors.AcceptMentorship(mentorUserId, studentUserId);
        }


        [HttpGet("{mentorUserId}/requestMentorship/{studentUserId}")]
        public ActionResult<UserInfoDTO> RequestMentorship(string mentorUserId, string studentUserId)
        {
            var mentorship = _unitOfWork.StudentMentors.RequestMentorship(mentorUserId, studentUserId);
            return new UserInfoDTO(mentorship.Mentor);
        }

        [HttpGet("{academicInterestId}/getMentors")]
        public ActionResult<List<UserInfoDTO>> GetMentorsForAcademicInterest(Guid academicInterestId)
        {
            var mentors = _unitOfWork.Teachers.MatchInterest(academicInterestId).
                Select(t => new UserInfoDTO(t.User)).ToList();
            return mentors;
        }

    }
}