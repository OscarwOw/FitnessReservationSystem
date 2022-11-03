using FitnessReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessReservationSystem.Data
{
    public class DatabaseContext : DbContext
    {
        protected DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCourse> TagCourses { get; set; }

        
    }
}
