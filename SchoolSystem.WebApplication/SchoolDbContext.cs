
using Microsoft.EntityFrameworkCore;
using SchoolSystem.WebApplication.Entities;

namespace SchoolSystem.WebApplication
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> dbContext) : base(dbContext)
        {
        }
        
        public virtual DbSet<UserEntity> Users { get; set; }
        
        public virtual DbSet<SubjectEntity> Subjects { get; set; }
        
        public virtual DbSet<LessonEntity> Lessons { get; set; }
    }
}