using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace XMLDocCrowdSourcer.Components.Pages.Project {
    public class ProjectManagerHandler : AuthorizationHandler<ProjectRequirement, Data.Project> {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       ProjectRequirement requirement,
                                                       Data.Project project) {
            if (!requirement.AllowManagers) {
                return Task.CompletedTask;
            }

            if (context.User.Claims.Any(
                c => c.Type == ClaimTypes.NameIdentifier
                && project.Managers.Any(r => r.Id == c.Value)
            )) {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
