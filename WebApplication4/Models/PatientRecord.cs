namespace WebApplication4.Models;

public class PatientRecord
{
    public int Id { get; set; }
    
    public Record Record { get; set; }
    
    public Patient Patient { get; set; }
    
    public DateTime RecordTime { get; set; }
}