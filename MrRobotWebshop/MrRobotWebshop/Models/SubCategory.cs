using System;
using System.Collections.Generic;

namespace MrRobotWebshop.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Product = new HashSet<Product>();
        }

        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
