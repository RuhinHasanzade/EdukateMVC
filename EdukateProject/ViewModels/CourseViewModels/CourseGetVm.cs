namespace EdukateProject.ViewModels.CourseViewModels
{
    public class CourseGetVm
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string TeacherSurname { get; set; } = string.Empty;



    }
}
