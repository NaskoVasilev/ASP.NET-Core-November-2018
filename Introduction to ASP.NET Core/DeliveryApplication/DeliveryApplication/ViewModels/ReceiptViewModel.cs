using System;

namespace DeliveryApplication.ViewModels
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Recipient { get; set; }
    }
}
