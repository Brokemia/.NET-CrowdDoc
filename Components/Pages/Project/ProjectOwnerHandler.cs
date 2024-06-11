using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using XMLDocCrowdSourcer.Data;

namespace XMLDocCrowdSourcer.Components.Pages.Project {
    public class ProjectOwnerHandler(IServiceScopeFactory scopeFactory) : AuthorizationHandler<ProjectRequirement, Data.Project> {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       ProjectRequirement requirement,
                                                       Data.Project project) {
            if (!requirement.AllowOwners) {
                return Task.CompletedTask;
            }

            // https://stackoverflow.com/a/48368934/6337971
            using var scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            db.Projects.Entry(project)
                .Collection(p => p.UserRoles)
                .Query()
                .Include(r => r.User)
                .Load();

            if (context.User.Claims.Any(
                c => c.Type == ClaimTypes.NameIdentifier
                && project.UserRoles.Any(r => r.Role == Data.ProjectRole.Owner && r.User.Id == c.Value)
            )) {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
