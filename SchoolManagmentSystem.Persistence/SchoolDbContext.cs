using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Domain.Entities;
using SchoolManagmentSystem.Persistence.Configurations;

namespace SchoolManagmentSystem.Persistence;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<StudentCourse> StudentCourses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentCourseConfiguration());



        OnModelCreatingPartial(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
