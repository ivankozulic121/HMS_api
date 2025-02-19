namespace WebApplication4.Models;

public class Record
{
    public int Id { get; set; }
    public string RecordName { get; set; }
    
    public List<Patient> Patients { get; set; }
    
}