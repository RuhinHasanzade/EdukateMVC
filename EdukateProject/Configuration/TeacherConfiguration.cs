using EdukateProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdukateProject.Configuration
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(t => t.Name).IsRequired().HasMaxLength(64);
            builder.Property(t => t.Surname).IsRequired().HasMaxLength(64);
        }
    }
}
