using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XMLDocCrowdSourcer.Data {
    // Either a namespace or parent type
    public class MappingGroup {
        public enum ObjectType {
            Namespace,
            Type,
            Field,
            Method,
            Property,
            Event
        }

        public Guid Id { get; set; }
        
        public required string Name { get; set; }

        public ObjectType Type { get; set; }

        public virtual Mapping? Mapping { get; set; }

        // Any children of this group
        [InverseProperty(nameof(ParentMappingGroup))]
        public virtual List<MappingGroup> Groups { get; set; } = [];

        // Number of elements in Groups, used for performance reasons in MappingGroupList
        [NotMapped]
        public int GroupsCount { get; set; } = -1;

        [Required]
        public required virtual Project Project { get; set; }

        // Only used if direct child of a Project
        public virtual Project? ParentProject { get; set; }

        // Only used if direct child of another MappingGroup
        public virtual MappingGroup? ParentMappingGroup { get; set; }
    }
}
