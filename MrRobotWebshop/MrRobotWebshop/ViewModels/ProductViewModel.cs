using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrRobotWebshop.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
