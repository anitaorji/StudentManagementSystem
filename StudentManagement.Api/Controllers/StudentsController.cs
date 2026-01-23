using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Student;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentAsync()
        {
            var students = await _studentService.GetAllStudentsAsync();

            if (students == null)
            {
                return Ok("No information available");
            }
            else
            {
                return Ok(students);
            }
        }

        [HttpGet("{id}", Name = "GetStudentById")]
        public async Task<ActionResult<StudentResponseDto>> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(student);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> AddStudentAsync(CreateStudentDto dto)
        {
            var student = await _studentService.AddStudentAsync(dto);
            return Ok(student);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResponseDto>> UpdateStudentById(
            Guid id,
            UpdateStudentDto dto)
        {
            var updatedStudent = await _studentService.UpdateStudentByIdAsync(id, dto);
            return Ok(updatedStudent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var deleted = await _studentService.DeleteStudentByIdAsync(id);

            if (!deleted)
                return BadRequest();

            return Ok("Information Deleted");
        }
                
    }
}