using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMentor.DataAccessLayer;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicInterestsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public AcademicInterestsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/AcademicInterests
        [HttpGet]
        public ActionResult<IEnumerable<AcademicInterest>> GetAcademicInterests()
        {
            return _unitOfWork.AcademicInterests.GetAll().ToList();
        }

        // GET: api/AcademicInterests/5
        [HttpGet("{id}")]
        public ActionResult<AcademicInterest> GetAcademicInterest(Guid id)
        {
            var academicInterest =  _unitOfWork.AcademicInterests.Get(id);

            if (academicInterest == null)
            {
                return NotFound();
            }

            return academicInterest;
        }

        // PUT: api/AcademicInterests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcademicInterest(Guid id, AcademicInterest academicInterest)
        {
            if (id != academicInterest.Id)
            {
                return BadRequest();
            }
            _unitOfWork.AcademicInterests.Add(academicInterest);

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicInterestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //// POST: api/AcademicInterests
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<AcademicInterest>> PostAcademicInterest(AcademicInterest academicInterest)
        //{
        //    _context.AcademicInterests.Add(academicInterest);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAcademicInterest", new { id = academicInterest.Id }, academicInterest);
        //}

        //// DELETE: api/AcademicInterests/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<AcademicInterest>> DeleteAcademicInterest(Guid id)
        //{
        //    var academicInterest = await _context.AcademicInterests.FindAsync(id);
        //    if (academicInterest == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.AcademicInterests.Remove(academicInterest);
        //    await _context.SaveChangesAsync();

        //    return academicInterest;
        //}

        private bool AcademicInterestExists(Guid id)
        {
            return _unitOfWork.AcademicInterests.GetSingleOrDefault(e => e.Id == id) != null;
        }
    }
}
