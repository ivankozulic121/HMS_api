using WebApplication4.Data;

namespace WebApplication4.Models;

public class Doctor
{
    public ApplicationUser User { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}