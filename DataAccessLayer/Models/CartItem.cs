namespace DataAccessLayer.Models;

public class CartItem
{
    public Part Part { get; set; } = null!;
    public int Aantal { get; set; }
}