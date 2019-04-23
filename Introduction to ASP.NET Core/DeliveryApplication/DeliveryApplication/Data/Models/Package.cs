using DeliveryApplication.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace DeliveryApplication.Data.Models
{
    public class Package
    {
        private const int DeliverDays = 3;

        public Package()
        {
            Status = Status.Pending;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public Status Status{ get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public string RecipientId { get; set; }
        public IdentityUser Recipient { get; set; }
    }
}
