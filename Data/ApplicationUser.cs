using Microsoft.AspNetCore.Identity;

namespace XMLDocCrowdSourcer.Data {
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser {
        public List<ProjectUser> ProjectRoles { get; set; } = [];
    }

}
