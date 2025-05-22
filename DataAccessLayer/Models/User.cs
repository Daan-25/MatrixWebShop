namespace DataAccessLayer.Models;

public class User
{
    public int Id { get; set; }
    public string Gebruikersnaam { get; set; }
    public string Wachtwoord { get; set; }
    
    public int? CustomerId { get; set; }
    
    public Customer? Customer { get; set; }
}