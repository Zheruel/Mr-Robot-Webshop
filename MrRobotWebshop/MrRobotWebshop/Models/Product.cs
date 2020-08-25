using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MrRobotWebshop.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductInfo = new HashSet<ProductInfo>();
        }

        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }
        public ICollection<ProductInfo> ProductInfo { get; set; }
    }
}
