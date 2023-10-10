using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Domain.Entities;

namespace SchoolManagmentSystem.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();

        entity.HasIndex(e => e.Name);

        entity.Property(e => e.Name).HasMaxLength(50);

    }

}
