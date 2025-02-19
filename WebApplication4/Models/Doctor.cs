using WebApplication4.Data;

namespace WebApplication4.Models;

public class Doctor
{   
    public int Id { get; set; }
    
    public ICollection<Appointment> Appointments { get; set; }
    public ApplicationUser User { get; set; }
    
}