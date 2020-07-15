using System;
using System.Collections.Generic;

namespace MrRobotWebshop.Models
{
    public partial class WebshopUser
    {
        public int WebshopUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
