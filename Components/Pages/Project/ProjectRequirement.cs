using Microsoft.AspNetCore.Authorization;

namespace XMLDocCrowdSourcer.Components.Pages.Project {
    public abstract class ProjectRequirement : IAuthorizationRequirement { }

    public class ProjectEditMappingsRequirement : ProjectRequirement { }
}
