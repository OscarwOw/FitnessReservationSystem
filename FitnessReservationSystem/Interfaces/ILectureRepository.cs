﻿using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ILectureRepository
    {
        public bool Add(int courseId,Lecture lecture);
        public Lecture GetLecture(int id);
        public IEnumerable<Lecture> GetAll();
        public int GetRegistrationCount(int id);
        public bool Update(Lecture lecture);
        public void Delete(int id);
    }
}
