using AutoMapper;
using AutoMapper.QueryableExtensions;
using Eventures.Areas.Administration.Models;
using Eventures.Areas.Services.Contracts;
using Eventures.Data;
using Eventures.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eventures.Areas.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly EventuresDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public AdministrationService(EventuresDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public void DemoteUser(string username)
        {
            var user = context.Users.FirstOrDefault(x => x.UserName == username);
            if(user != null)
            {
                userManager.RemoveFromRoleAsync(user, "Administrator").GetAwaiter().GetResult();
            }
        }

        public IQueryable<AdminstrationUserViewModel> GetAllUsers()
        {
            return context.Users
                .ProjectTo<AdminstrationUserViewModel>(mapper.ConfigurationProvider);
        }

        public void PromoteUser(string username)
        {
            var user = context.Users.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
            }
        }

        public void SetIsAdminProperty(AdminstrationUserViewModel[] users)
        {
            string adminRoleId = context.Roles.FirstOrDefault(x => x.Name == "Administrator")?.Id;
            foreach (var user in users)
            {
                user.IsAdmin = context.UserRoles.Any(x => x.RoleId == adminRoleId && x.UserId == user.Id);
            }
        }
    }
}
