using System.ComponentModel.DataAnnotations.Schema;

namespace XMLDocCrowdSourcer.Data {
    public class Project {
        public Guid Id { get; set; }
        
        public required string Name { get; set; }

        public required string AssemblyName { get; set; }

        [InverseProperty(nameof(MappingGroup.ParentProject))]
        public List<MappingGroup> Groups { get; set; } = [];

        public List<ProjectUser> UserRoles { get; set; } = [];
    }
}
