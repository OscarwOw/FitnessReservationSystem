using AutoMapper;
using FitnessReservationSystem.Dto;
using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Course, CourseDTO>();
        }
    }
}
