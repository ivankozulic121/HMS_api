
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.DBContext;

public class PatientContext: DbContext
{
    public PatientContext(DbContextOptions<PatientContext> options) : base(options) {}
    
    public DbSet<Patient> Patients { get; set; }
}