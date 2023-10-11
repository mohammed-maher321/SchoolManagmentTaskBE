using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SchoolManagmentSystem.Contract.Dto;
using SchoolManagmentSystem.Contract.IServices;
using SchoolManagmentSystem.Domain.QueryModels;

namespace SchoolManagmentSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoruseController : BaseController
    {
        private ICourseService courseService;
        public CoruseController(ICourseService courseService, IActionContextAccessor accessor)
          : base(accessor) =>
          this.courseService = courseService;


        [HttpPost]
        public async Task<IActionResult> GetAllCourses(CourseQueryModel courseQueryModel)
        {
            var response = await courseService.GetCourses(courseQueryModel);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> SaveCourse(CourseDto model)
        {
            var response = await courseService.SaveCourse(model);
            return Ok(response);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCourse(int id)
        {
            var response = await courseService.GetCourse(id);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var response = await courseService.DeleteCourse(id);
            return Ok(response);
        }
    }
}
