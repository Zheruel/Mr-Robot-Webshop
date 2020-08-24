using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MrRobotWebshop.Models
{
    public partial class WebshopUser
    {
        public WebshopUser()
        {
            Receipt = new HashSet<Receipt>();
        }

        public int WebshopUserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Salt { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public ICollection<Receipt> Receipt { get; set; }
    }
}
