﻿using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ILectureRepository
    {
        public void Add(Lecture lecture);
        public Lecture GetLecture(int id);
        public IEnumerable<Lecture> GetAll();
        public int GetRegistrationCount(int id);
        public void Update(Lecture lecture);
        public void Delete(int id);
    }
}
