using Microsoft.AspNetCore.Identity;
using System;

namespace DeliveryApplication.Data.Models
{
    public class Receipt
    {
        public Receipt()
        {
            this.IssuedOn = DateTime.Now;
        }

        public int Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientId { get; set; }
        public IdentityUser Recipient { get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }
    }
}
