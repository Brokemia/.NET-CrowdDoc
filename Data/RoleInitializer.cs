using Microsoft.AspNetCore.Identity;

namespace XMLDocCrowdSourcer.Data {
    public static class RoleInitializer {
        public static async Task InitializeAsync(IServiceProvider provider) {
            var scope = provider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            string[] roleNames = [ "SuperAdmin" ];
            
            foreach (var roleName in roleNames) {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                
                // Create the role and seed it to the database
                IdentityResult roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    
                if (!roleResult.Succeeded) {
                    throw new Exception($"Failed to create role {roleName}");
                }
            }
        }
    }
}
