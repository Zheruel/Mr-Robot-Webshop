using System;
using System.Collections.Generic;

namespace MrRobotWebshop.Models
{
    public partial class ProductInfo
    {
        public int ProductInfoId { get; set; }
        public int? Amount { get; set; }
        public decimal? Discount { get; set; }
        public int ProductId { get; set; }
        public int ReceiptId { get; set; }

        public Product Product { get; set; }
        public Receipt Receipt { get; set; }
    }
}
