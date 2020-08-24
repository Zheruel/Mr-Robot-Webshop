﻿using System;
using System.Collections.Generic;

namespace MrRobotWebshop.Models
{
    public partial class WebshopUser
    {
        public WebshopUser()
        {
            Receipt = new HashSet<Receipt>();
        }

        public int WebshopUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<Receipt> Receipt { get; set; }
    }
}
