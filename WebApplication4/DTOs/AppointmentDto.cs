namespace WebApplication4.DTOs;

public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; }
    
    public DateTime AppointmentTime { get; set; }
}