using EdukateProject.Models.Common;

namespace EdukateProject.Models
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }
    }
}
