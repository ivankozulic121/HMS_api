using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.DBContext;

public class AppointmentContext: DbContext
{
    public AppointmentContext(DbContextOptions<AppointmentContext> options) : base(options) {}
    
    public DbSet<Appointment> Appointments { get; set; }
}