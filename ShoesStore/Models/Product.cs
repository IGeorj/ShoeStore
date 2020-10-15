using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
    }
}
