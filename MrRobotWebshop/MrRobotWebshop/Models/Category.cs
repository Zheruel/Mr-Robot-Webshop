using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MrRobotWebshop.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategory = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<SubCategory> SubCategory { get; set; }
    }
}
