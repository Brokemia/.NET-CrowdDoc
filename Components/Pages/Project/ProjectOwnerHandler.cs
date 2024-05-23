using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using XMLDocCrowdSourcer.Data;

namespace XMLDocCrowdSourcer.Components.Pages.Project {
    public class ProjectOwnerHandler : AuthorizationHandler<ProjectRequirement, Data.Project> {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       ProjectRequirement requirement,
                                                       Data.Project project) {
            // Not an owner
            if (!context.User.Claims.Any(
                c => c.Type == ClaimTypes.NameIdentifier
                && project.Owners.Any(r => r.Id == c.Value)
            )) {
                return Task.CompletedTask;
            }

            if (requirement is ProjectEditMappingsRequirement) {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
