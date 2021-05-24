using Microsoft.EntityFrameworkCore;
using ScheduleApp.Data;

namespace ScheduleApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<StudentData> Students { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
    }
}