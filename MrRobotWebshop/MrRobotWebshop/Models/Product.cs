using System;
using System.Collections.Generic;

namespace MrRobotWebshop.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductInfo = new HashSet<ProductInfo>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }
        public ICollection<ProductInfo> ProductInfo { get; set; }
    }
}
