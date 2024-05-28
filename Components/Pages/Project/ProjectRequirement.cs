using Microsoft.AspNetCore.Authorization;

namespace XMLDocCrowdSourcer.Components.Pages.Project {
    public class ProjectRequirement : IAuthorizationRequirement {
        public bool AllowOwners { get; set; }

        public bool AllowManagers { get; set; }
    }
}
