﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoesStore.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}