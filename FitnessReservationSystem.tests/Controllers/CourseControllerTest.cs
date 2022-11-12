using AutoMapper;
using FakeItEasy;
using FitnessReservationSystem.Controllers;
using FitnessReservationSystem.Dto;
using FitnessReservationSystem.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservationSystem.tests.Controllers
{
    public class CourseControllerTest
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseControllerTest()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public void CourseController_GetCourses_returnOk()
        {
            //Arange
            var courses = A.Fake<ICollection<CourseDTO>>();
            var courseList = A.Fake<List<CourseDTO>>();
            A.CallTo(() => _mapper.Map<List<CourseDTO>>(courses)).Returns(courseList);
            var controller = new CourseController(_courseRepository, _mapper);

            //Act
            var result = controller.GetCourses();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
           
        }
    }
}
