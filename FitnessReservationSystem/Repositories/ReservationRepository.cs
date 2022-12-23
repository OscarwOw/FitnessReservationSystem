using FitnessReservationSystem.Data;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessReservationSystem.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DatabaseContext _databaseContext;
        public ReservationRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add(int lectureId,Reservation reservation)
        {
            var lecture = _databaseContext.Lectures.Where(c => c.Id == lectureId).FirstOrDefault();
            if (lecture == null)
            {
                return false;
            }
            reservation.Lecture = lecture;
            reservation.User = _databaseContext.ApplicationUsers.Where(c => c.Email == "user@example.com").FirstOrDefault();
            _databaseContext.Add(reservation);
            _databaseContext.SaveChanges();
            return true;
        
        }
        public ICollection<Reservation> GetAll()
        {
            return _databaseContext.Reservations.ToList();
        }
        public Reservation GetReservation(int id)
        {
            return _databaseContext.Reservations.AsNoTracking().Where(e => e.Id == id).FirstOrDefault();
        }
        public Reservation GetReservationByName(string name)
        {
            return _databaseContext.Reservations.Where(e => e.Name == name).FirstOrDefault();
        }
       
        public bool Update(Reservation reservation)
        {
            
            _databaseContext.Update(reservation);
            _databaseContext.SaveChanges();
            return true;
        }

        public bool CheckIfExists(int LectureId, string mail)
        {
            Reservation reservation = _databaseContext.Reservations.Where(e => e.Lecture.Id == LectureId)           //List of reservations from exact lecture
                                                                   .Where(c => c.Mail == mail).FirstOrDefault();    //mail reservation combo
            if (reservation == null)
            {
                return false;
            }
            return true;
        }
        public bool Delete(Reservation reservation)
        {
            
            _databaseContext.Remove(reservation);
            _databaseContext.SaveChanges();
            return true;
        }
    }
}
