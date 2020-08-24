using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrRobotWebshop.ViewModels
{
    public class WebshopUserViewModel
    {
        public int WebshopUserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int ReceiptCount { get; set; }
    }
}
