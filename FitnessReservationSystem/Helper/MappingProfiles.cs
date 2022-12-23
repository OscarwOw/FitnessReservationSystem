using AutoMapper;
using FitnessReservationSystem.Dto.CourseDtos;
using FitnessReservationSystem.Dto.LectureDtos;
using FitnessReservationSystem.Dto.ReservationDtos;
using FitnessReservationSystem.Dto.TagDtos;
using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Lecture, LectureDTO>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<Lecture, LectureDTOWithCapacity>();
        }
    }
}
