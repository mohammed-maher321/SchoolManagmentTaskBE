using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SchoolManagmentSystem.Contract.Dto;
using SchoolManagmentSystem.Contract.IServices;
using SchoolManagmentSystem.Domain.QueryModels;

namespace SchoolManagmentSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentController : BaseController
    {
        private IStudentService studentService;
        public StudentController(IStudentService studentService, IActionContextAccessor accessor)
          : base(accessor) =>
          this.studentService = studentService;


        [HttpPost]
        public async Task<IActionResult> GetAllStudents(StudentQueryModel studentQueryModel)
        {
            var response = await studentService.GetStudents(studentQueryModel);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> SaveStudent(StudentDto model)
        {
            var response = await studentService.SaveStudent(model);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var response = await studentService.DeleteStudent(id);
            return Ok(response);
        }
    }
}
