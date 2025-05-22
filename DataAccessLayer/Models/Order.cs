using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public string PaymentMethod { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; } = null!;

        public ICollection<Product> Products { get; } = new List<Product>();
        
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    
    public class OrderItem
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        
        public Order Order { get; set; }

        public int ProductId { get; set; }
        
        public Part Product { get; set; }

        public int Aantal { get; set; }
    }
}
