using System.Xml.Linq;
using XMLDocCrowdSourcer.Data;

namespace XMLDocCrowdSourcer.Services {
    public class DocumentationParser : IDocumentationParser {
        public XElement ParseElement(Mapping mapping) {
            return XElement.Parse($"<member name=\"{mapping.XmlDocId}\">{mapping.Documentation}</member>");
        }
    }
}
