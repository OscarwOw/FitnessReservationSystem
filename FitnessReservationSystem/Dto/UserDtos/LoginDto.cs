using System.ComponentModel.DataAnnotations;

namespace FitnessReservationSystem.Dto.UserDtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
