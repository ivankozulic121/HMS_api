using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.DBContext;


public class DoctorContext: DbContext
{
    public DoctorContext(DbContextOptions<DoctorContext> options): base(options) { }
    
    public DbSet<Doctor> Doctors { get; set; }
}