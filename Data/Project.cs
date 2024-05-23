using System.ComponentModel.DataAnnotations.Schema;

namespace XMLDocCrowdSourcer.Data {
    public class Project {
        public Guid Id { get; set; }
        
        public required string Name { get; set; }

        [InverseProperty(nameof(MappingGroup.ParentProject))]
        public virtual List<MappingGroup> Groups { get; set; } = [];

        public virtual List<ApplicationUser> Owners { get; set; } = [];

        public virtual List<ApplicationUser> Managers { get; set; } = [];
    }
}
