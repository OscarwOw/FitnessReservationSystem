using System.ComponentModel.DataAnnotations;

namespace FitnessReservationSystem.Dto.UserDtos
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
