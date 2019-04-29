using Eventures.Areas.Administration.Models;
using System.Linq;
using System.Security.Principal;

namespace Eventures.Areas.Services.Contracts
{
    public interface IAdministrationService
    {
        IQueryable<AdminstrationUserViewModel> GetAllUsers();

        void PromoteUser(string username);

        void DemoteUser(string username);

        void SetIsAdminProperty(AdminstrationUserViewModel[] users);
    }
}
