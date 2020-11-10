using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoesStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int TotalPrice { get; set; }
        public string Date { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}