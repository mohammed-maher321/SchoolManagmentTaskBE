using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Domain.Entities;

namespace SchoolManagmentSystem.Persistence.Configurations;


public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();

        entity.HasIndex(e => e.StudentId);
        entity.HasIndex(e => e.CourseId);

        entity.HasOne(d => d.Student)
         .WithMany(p => p.StudentCourses)
         .HasForeignKey(d => d.StudentId)
         .OnDelete(DeleteBehavior.Restrict);


        entity.HasOne(d => d.Course)
         .WithMany(p => p.StudentCourses)
         .HasForeignKey(d => d.CourseId)
         .OnDelete(DeleteBehavior.Restrict);
    }

}