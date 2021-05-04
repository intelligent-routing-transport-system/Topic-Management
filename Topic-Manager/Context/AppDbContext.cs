using Topic_Manager.Model;
using Microsoft.EntityFrameworkCore;

namespace Topic_Manager.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Sensor> EventSensors { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
