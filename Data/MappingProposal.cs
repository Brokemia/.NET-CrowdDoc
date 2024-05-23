namespace XMLDocCrowdSourcer.Data {
    public class MappingProposal {
        public Guid Id { get; set; }

        public required virtual ApplicationUser Author { get; set; }

        public required virtual Mapping Mapping { get; set; }
        
        public required string? ProposedValue { get; set; }
    }
}
