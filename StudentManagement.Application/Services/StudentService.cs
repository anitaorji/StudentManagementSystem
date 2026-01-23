using StudentManagement.Application.DTOs.Student;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Interfaces;
using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return students.Select(student => new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Course = student.Course,
                Level = student.Level
            });
        }
        public async Task<StudentResponseDto?> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            
            if (student == null)
                throw new KeyNotFoundException("Student not found");

            return new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Course = student.Course,
                Level = student.Level,
            };
        }
        public async Task<StudentResponseDto> AddStudentAsync(CreateStudentDto dto)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Course = dto.Course,
                Level = dto.Level
            };
            await _studentRepository.AddStudentAsync(student);
            
            return new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Course = student.Course,
                Level = student.Level
            };
        }
        public async Task<StudentResponseDto> UpdateStudentByIdAsync(Guid id, UpdateStudentDto dto)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
                return null;

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;
            student.Course = dto.Course;
            student.Level = dto.Level;

            await _studentRepository.UpdateStudentAsync(student);
            return new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Course = student.Course,
                Level = student.Level
            };
            
        }
        public async Task<bool> DeleteStudentByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
                return false;

            await _studentRepository.DeleteStudentAsync(student);
            return true;
        }

    }
}
