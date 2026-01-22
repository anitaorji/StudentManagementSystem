using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Course { get; set; }
        public string? Level { get; set; }
        public string Email { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        
    }
}
