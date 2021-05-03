using Microsoft.EntityFrameworkCore;
using SchoolSystem.WebApplication.Entities;

namespace SchoolSystem.WebApplication
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }
        
        public virtual DbSet<StudentEntity> Students { get; set; }
    }
}