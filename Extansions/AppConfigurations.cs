using BasetApi.Models;
using BasetApi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace BasetApi.Extansions
{
    public static class AppConfigurations
    {
        public static async Task ConfigureAdminUser(this IApplicationBuilder app)
        {
            const string AdminUser = "Admin";
            const string AdminPassword = "Admin12345+";

            UserManager<User> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<User>>();

            RoleManager<Role> roleManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<Role>>();

            User user = await userManager.FindByNameAsync(AdminUser);

            if (user is null)
            {
                user = new User()
                {
                    Email = "mcelxan@gmail.com",
                    PhoneNumber = "0705450755",
                    UserName = AdminUser,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, AdminPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Admin User Tapılmadı");
                }

                var roleResult = await userManager.AddToRolesAsync(user,
       await roleManager
           .Roles
           .Select(r => r.Name)
           .ToListAsync()
   );


                if (!roleResult.Succeeded)
                {
                    throw new Exception("Xeta!...");
                }
            }
        }

        public static void ConfigureSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Baset API v1");
            c.RoutePrefix = "swagger";
            c.DocExpansion(DocExpansion.None);
        });

        }
    }
}