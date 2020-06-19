using System;
using Microsoft.Owin;
using Owin;
using ProjectH.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.h.Models;

[assembly: OwinStartup(typeof(ProjectH.Startup))]

namespace ProjectH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }
        private void CreateRoles()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                if (!roleManager.RoleExists("SuperAdmin"))
                {
                    role.Name = "SuperAdmin";
                    roleManager.Create(role);
                    AddUsers(role.Name);
                }
                if (!roleManager.RoleExists("Manager"))
                {
                    role.Name = "Manager";
                    roleManager.Create(role);
                    AddAnotherUsers(role.Name);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private void AddAnotherUsers(string roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();

            user.UserName = "Asim";
            user.Email = "asim@gmail.com";
            string password = "Asim123@#";

            var status = UserManager.Create(user, password);

            if (status.Succeeded)
            {
                UserManager.AddToRole(user.Id, roleName);
            }
        }

        private void AddUsers(string roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();

            user.UserName = "manager@gmail.com";
            user.Email = "manager@gmail.com";
            string password = "Manager123@#";

            var status = UserManager.Create(user, password);

            if (status.Succeeded)
            {
                UserManager.AddToRole(user.Id, roleName);
            }
        }
    }
}

