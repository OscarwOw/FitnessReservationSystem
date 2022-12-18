using System.ComponentModel.DataAnnotations;

namespace FitnessReservationSystem.Dto.UserDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "User name is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "User name is required")]
        public string? Password { get; set; }
    }
}
