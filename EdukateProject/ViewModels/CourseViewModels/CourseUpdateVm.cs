using System.ComponentModel.DataAnnotations;

namespace EdukateProject.ViewModels.CourseViewModels
{
    public class CourseUpdateVm
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(0, 5)]
        public double Rating { get; set; }

        [Required]
        public IFormFile? Image { get; set; } 

        public int TeacherId { get; set; }
    }
}
