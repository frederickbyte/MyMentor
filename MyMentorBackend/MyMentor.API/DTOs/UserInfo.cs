using MyMentor.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMentor.API.DTOs
{
    public class UserInfoDTO
    {

        public UserDTO User { get; set; }
        public List<AcademicInterestDTO> AcademicInterests { get; set; }

        public UserInfoDTO()
        { }

        public UserInfoDTO(ApplicationUser user)
        {
            User = new UserDTO(user);
            AcademicInterests = user.UserInterests.Select(ui => new AcademicInterestDTO(ui.AcademicInterest)).ToList();
        }
    }
}
