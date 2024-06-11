using System.Xml.Linq;
using XMLDocCrowdSourcer.Data;

namespace XMLDocCrowdSourcer.Services {
    public interface IDocumentationParser {
        public XElement ParseElement(Mapping mapping);
    }
}
