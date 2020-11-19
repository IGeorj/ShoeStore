using System.ComponentModel.DataAnnotations;

namespace ShoesStore.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }

    }
}