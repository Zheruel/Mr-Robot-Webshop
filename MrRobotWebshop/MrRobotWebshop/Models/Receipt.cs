using System;
using System.Collections.Generic;

namespace MrRobotWebshop.Models
{
    public partial class Receipt
    {
        public Receipt()
        {
            ProductInfo = new HashSet<ProductInfo>();
        }

        public int ReceiptId { get; set; }
        public string Status { get; set; }
        public string FinalPrice { get; set; }
        public int WebshopUserId { get; set; }

        public WebshopUser WebshopUser { get; set; }
        public ICollection<ProductInfo> ProductInfo { get; set; }
    }
}
