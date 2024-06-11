using System.ComponentModel.DataAnnotations.Schema;

namespace XMLDocCrowdSourcer.Data {
    public class ProjectUser {
        public Guid Id { get; set; }

        [InverseProperty(nameof(ApplicationUser.ProjectRoles))]
        public required ApplicationUser User { get; set; }

        [InverseProperty(nameof(Data.Project.UserRoles))]
        public required Project Project { get; set; }

        public required ProjectRole Role { get; set; }
    }
}
