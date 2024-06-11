using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace XMLDocCrowdSourcer.Data {
    public enum ProjectRole {
        // Highest privilege to lowest
        Owner = 0,
        Manager = 1,
    }
}
