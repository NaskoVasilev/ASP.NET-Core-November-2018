using Eventures.MappingConfiguration.Contracts;
using Eventures.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Eventures.Areas.Administration.Models
{
    public class AdminstrationUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
