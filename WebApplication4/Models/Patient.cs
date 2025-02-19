using WebApplication4.Data;


namespace WebApplication4.Models;

public class Patient
{
    public int Id { get; set; }
    public List<Record> Records { get; set; }
    
    public List<Appointment> Appointments { get; set; }
    public ApplicationUser User { get; set; }
    
}