using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HomeCon.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    
    public class ApplicationUser : IdentityUser
    {
        public Model.Client Client { get; set; } = new Model.Client();
        public override string Id { get => Client.Id; set => Client.Id = value; }
        public override string Email { get => Client.Email; set => Client.Email = value; }
        public override string UserName { get => Client.Name; set => Client.Name = value; }
        public override string SecurityStamp { get => Client.SecurityStamp; set => Client.SecurityStamp = value; }
        public override string ConcurrencyStamp { get => Client.ConcurrencyStamp; set => Client.ConcurrencyStamp = value; }
        public override int AccessFailedCount { get => Client.AccessFailedCount; set => Client.AccessFailedCount = value; }
        public override bool EmailConfirmed { get => Client.EmailConfirmed; set => Client.EmailConfirmed = value; }
        public override string PasswordHash { get => Client.Password; set => Client.Password = value; }

        public ApplicationUser()
        {
            Client = new Model.Client();
        }
        public ApplicationUser(Model.Client client)
        {
            Client = client;
        }
    }

}
