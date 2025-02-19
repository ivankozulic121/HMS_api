namespace WebApplication4.Models;

public class Appointment
{
    public int Id { get; set; }
    public Doctor Doctor { get; set; } 
    public Patient Patient { get; set; }
    public DateTime AppointmentTime { get; set; }
}