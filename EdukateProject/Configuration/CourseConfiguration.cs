using EdukateProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdukateProject.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c=>c.Title).IsRequired().HasMaxLength(64);
            builder.Property(c => c.ImagePath).IsRequired();
            builder.ToTable(opt =>
            {
                opt.HasCheckConstraint("CK_Course_Rating", "[Rating] between 0 and 5");
            });
            builder.HasOne(t => t.Teacher).WithMany(c => c.Courses).HasForeignKey(c => c.TeacherId).HasPrincipalKey(t => t.Id).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
