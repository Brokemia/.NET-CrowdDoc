namespace XMLDocCrowdSourcer.Data {
    public class Mapping {
        public Guid Id { get; set; }
        
        public required string XmlDocId { get; set; }
        
        public string? Documentation { get; set; }
    }
}
