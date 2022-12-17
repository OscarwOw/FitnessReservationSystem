

using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace FitnessReservationSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Reservation> Reservations { get; set; }           
    }
}
