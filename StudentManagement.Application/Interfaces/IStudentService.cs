using StudentManagement.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Interfaces
{
    public interface IStudentService
    {
        public Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync();
        public Task<StudentResponseDto?> GetStudentByIdAsync(Guid id);
        public Task<StudentResponseDto> AddStudentAsync(CreateStudentDto dto);
        public Task<StudentResponseDto> UpdateStudentByIdAsync(Guid id, UpdateStudentDto dto);
        public Task<bool> DeleteStudentByIdAsync(Guid id);

    }
}
