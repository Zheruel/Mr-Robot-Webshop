using MrRobotWebshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrRobotWebshop.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<SubCategoryViewModel> SubCategories { get; set; }
    }
}
