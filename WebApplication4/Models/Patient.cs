using WebApplication4.Data;


namespace WebApplication4.Models;

public class Patient
{
    public string[] Records { get; set; }
    public ApplicationUser User { get; set; }
    
}