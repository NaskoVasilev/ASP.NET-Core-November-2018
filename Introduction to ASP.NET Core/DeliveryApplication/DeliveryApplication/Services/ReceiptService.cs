using DeliveryApplication.Data;
using DeliveryApplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DeliveryApplication.Services
{
    public class ReceiptService
    {
        private readonly DeliveryApplicationDbContext context;

        public ReceiptService(DeliveryApplicationDbContext context)
        {
            this.context = context;
        }

        public ReceiptViewModel[] GetRecepitsById(string id)
        {
            return this.context.Receipts
                .Include(r => r.Recipient)
                .Where(r => r.RecipientId == id)
                .Select(r => new ReceiptViewModel
                {
                    Id = r.Id,
                    Fee = r.Fee,
                    IssuedOn = r.IssuedOn,
                    Recipient = r.Recipient.UserName
                })
                .ToArray();
        }

        public ReceiptInfoViewModel GetReceiptById(int id)
        {
            var receipt = context
                .Receipts
                .Include(r => r.Recipient)
                .Include(r => r.Package)
                .FirstOrDefault(r => r.Id == id);

            var model = new ReceiptInfoViewModel()
            {
                Number = receipt.Id,
                Total = receipt.Fee,
                DeliveryAddress = receipt.Package.ShippingAddress,
                PackageWeight = receipt.Package.Weight,
                PackageDescription = receipt.Package.Description,
                Recipient = receipt.Recipient.UserName,
                IssuedOn = receipt.IssuedOn
            };

            return model;
        }
    }
}
