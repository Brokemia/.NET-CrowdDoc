using Cecil.XmlDocNames;
using Mono.Cecil;
using System.Runtime.CompilerServices;
using XMLDocCrowdSourcer.Data;
using static XMLDocCrowdSourcer.Data.MappingGroup;

namespace XMLDocCrowdSourcer.Components.Pages.Project {
    public class ProjectBuilder {
        private class Node {
            public string Name { get; set; }

            public ObjectType Type { get; set; }

            public string? XmlDocId { get; set; }

            public Dictionary<string, Node> Children { get; set; } = [];

            public Node(string name, ObjectType type) {
                Name = name;
                Type = type;
            }
        }

        private string Name;
        
        private string AssemblyName;

        private Dictionary<string, Node> Nodes = [];

        public ProjectBuilder(string name, string assemblyName) {
            Name = name;
            AssemblyName = assemblyName;
        }

        private Node ResolveTypeNode(string path) {
            // Dots for namespace, forward slashes for nested types
            //var parts = path.Split('.').SelectMany(p => p.Split('/'));

            // Make namespace all one element, then type name, then any nested types
            var lastDot = path.LastIndexOf('.');
            var parts = (lastDot < 0 ? [path] : new string[] { path[..lastDot], path[(lastDot + 1)..] }).SelectMany(p => p.Split('/'));

            Node res = null!;
            var current = Nodes;
            bool isNamespace = lastDot >= 0;
            foreach (var part in parts) {
                if (!current.TryGetValue(part, out Node? value)) {
                    value = new Node(part, isNamespace ? ObjectType.Namespace : ObjectType.Type);
                    current[part] = value;
                }

                res = value;
                current = res.Children;
                // Only first part of path could be a namespace
                isNamespace = false;
            }

            return res;
        }

        private bool ShouldGenerateDoc(IMemberDefinition member) {
            if (!member.IsSpecialName) return true;
            if (member is MethodDefinition method) {
                // Allow constructors and operator overloads to be documented
                return method.IsConstructor || method.Name.StartsWith("op_");
            }

            return false;
        }

        private void AddMember(IMemberDefinition member, Node typeNode, ObjectType objectType) {
            if (!ShouldGenerateDoc(member)) {
                return;
            }
            if (member.CustomAttributes.Any(a => a.AttributeType.FullName == typeof(CompilerGeneratedAttribute).FullName)) {
                return;
            }

            // Idk when this would happen
            if (member is not MemberReference reference) {
                return;
            }

            var xmlDoc = reference.GetFixedXmlDocName();
            typeNode.Children.Add(xmlDoc, new Node(member.Name, objectType) {
                XmlDocId = xmlDoc
            });
        }

        // TODO eliminate isspecialname stuff?
        public void AddType(TypeDefinition type) {
            // Ignore compiler generated types
            if (type.CustomAttributes.Any(a => a.AttributeType.FullName == typeof(CompilerGeneratedAttribute).FullName)) {
                return;
            }

            //if ((type.Name.Contains("<") || type.Name.Contains(">")) || type.Name.Contains(">e__FixedBuffer")) {
            //    var i = 1;
            //}

            var typeNode = ResolveTypeNode(type.FullName);
            typeNode.XmlDocId = type.GetFixedXmlDocName();

            foreach (var nested in type.NestedTypes) {
                AddType(nested);
            }

            foreach (MethodDefinition methodDef in type.Methods) {
                AddMember(methodDef, typeNode, ObjectType.Method);
            }

            foreach (PropertyDefinition propertyDef in type.Properties) {
                AddMember(propertyDef, typeNode, ObjectType.Property);
            }

            foreach (FieldDefinition fieldDef in type.Fields) {
                AddMember(fieldDef, typeNode, ObjectType.Field);
            }

            foreach (EventDefinition eventDef in type.Events) {
                AddMember(eventDef, typeNode, ObjectType.Event);
            }
        }

        public void AddModule(ModuleDefinition module) {
            foreach (TypeDefinition type in module.Types) {
                if (type.Name == "<Module>") {
                    continue;
                }
                
                AddType(type);
            }
        }

        private MappingGroup BuildGroup(Node node, Data.Project project) {
            MappingGroup res = new() {
                Name = node.Name,
                Type = node.Type,
                Mapping = node.XmlDocId != null ? new() {
                    XmlDocId = node.XmlDocId
                } : null,
                Project = project
            };

            foreach (var child in node.Children.Values) {
                res.Groups.Add(BuildGroup(child, project));
            }

            return res;
        }

        public Data.Project Build() {
            Data.Project res = new() {
                Name = Name,
                AssemblyName = AssemblyName
            };

            foreach (var node in Nodes.Values) {
                res.Groups.Add(BuildGroup(node, res));
            }

            return res;
        }
    }
}
