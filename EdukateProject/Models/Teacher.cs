using EdukateProject.Models.Common;

namespace EdukateProject.Models
{
    public class Teacher  : BaseEntity 
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public ICollection<Course> Courses { get; set; } = [];
    }
}
