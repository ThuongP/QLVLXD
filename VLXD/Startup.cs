using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VLXD.Models;
[assembly: OwinStartupAttribute(typeof(VLXD.Startup))]
namespace VLXD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
          //  CreateRoleuser();

        }
        private void CreateRoleuser()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                role = new IdentityRole();
                role.Name = "WebMaster";
                roleManager.Create(role);
                //tao all user
                var user = new ApplicationUser();
                user.UserName = "admin@qlvlxd.com";
                user.Email = "admin@qlvlxd.com";
                var check = userManager.Create(user, "123123");
                if (check.Succeeded)
                    userManager.AddToRole(user.Id, "Admin");

                user = new ApplicationUser();
                user.UserName = "master1@qlvlxd.com";
                user.Email = "master1@qlvlxd.com";
                 check = userManager.Create(user, "123123");
                if (check.Succeeded)
                    userManager.AddToRole(user.Id, "WebMaster");

                user = new ApplicationUser();
                user.UserName = "master2@qlvlxd.com";
                user.Email = "master2@qlvlxd.com";
                check = userManager.Create(user, "123123");
                if (check.Succeeded)
                    userManager.AddToRole(user.Id, "WebMaster");



            }
        }
    }
}
