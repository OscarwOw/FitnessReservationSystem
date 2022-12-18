using System.ComponentModel.DataAnnotations;

namespace FitnessReservationSystem.Dto.UserDtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "User name is required")]
        public string? Password { get; set; }
    }
}
