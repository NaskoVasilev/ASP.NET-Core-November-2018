using DeliveryApplication.Data;
using DeliveryApplication.Data.Models;
using DeliveryApplication.Data.Models.Enums;
using DeliveryApplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace DeliveryApplication.Services
{
    public class PackageService
    {
        private readonly DeliveryApplicationDbContext context;

        public PackageService(DeliveryApplicationDbContext context)
        {
            this.context = context;
        }

        public RecipientViewModel[] GetAllRecipients()
        {
            return context.Users
               .Select(u => new RecipientViewModel
               {
                   Id = u.Id,
                   Username = u.UserName
               })
               .ToArray();
        }

        public void Create(PackageViewModel model)
        {
            Package package = new Package()
            {
                Description = model.Description,
                RecipientId = model.RecipientId,
                Weight = model.Weight,
                ShippingAddress = model.ShippingAddress
            };

            context.Packages.Add(package);
            context.SaveChanges();
        } 

        public void SetDeliveyDate(int id)
        {
            Random random = new Random();
            int days = random.Next(20, 40);
            Package package = context.Packages.Find(id);
            package.EstimatedDeliveryDate = DateTime.Now.AddDays(days);
        }

        public PackageIndexViewModel[] GetPackagesByStatus(string statusName, string userId)
        {
            Status status = Enum.Parse<Status>(statusName);

            return context.Packages
                .Where(p => p.Status == status && p.RecipientId == userId)
                .Select(p => new PackageIndexViewModel()
                {
                    Id = p.Id,
                    Name = p.Description
                })
                .ToArray();
        }

        public PackageInfoViewModel[] PackagesByStatus(Status status)
        {
            return context.Packages
                .Include(x => x.Recipient)
                .Where(p => p.Status == status)
                .Select(p => new PackageInfoViewModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    EstimatedDeliveryDate = p.EstimatedDeliveryDate,
                    Weight = p.Weight,
                    Recipient = p.Recipient.UserName,
                    ShippingAddress = p.ShippingAddress
                })
                .ToArray();
        }

        public void GenerateReceipt(int id, int recipientId)
        {
            const decimal feeRate = 2.67M;
            Package package = context.Packages.Find(id);
            Receipt receipt = new Receipt()
            {
                RecipientId = package.RecipientId,
                PackageId = id,
                Fee = package.Weight * feeRate
            };

            context.Receipts.Add(receipt);
            context.SaveChanges();
        }

        public Package GetById(int id)
        {
            return context.Packages
                .Include(p => p.Recipient)
                .FirstOrDefault(x => x.Id == id);
        }

        public void UpdateStatus(int id, Status newStatus)
        {
            Package package = context.Packages.Find(id);
            package.Status = newStatus;
            context.SaveChanges();
        }

        public string NornalizeDeliveryDate(DateTime deliveryDate, Status status)
        {
            if(status == Status.Pending)
            {
                return "N\\A";
            }
            else if(status == Status.Shipped)
            {
                return deliveryDate.ToShortDateString();
            }
            else
            {
                return "Delivered";
            }
        }
    }
}
