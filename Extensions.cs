using Cecil.XmlDocNames;
using Mono.Cecil;
using System.Security.Claims;

namespace XMLDocCrowdSourcer {
    public static class Extensions {
        public static bool TryGetAuthenticatedId(this ClaimsPrincipal user, out string? userId) {
            var identity = user.Identities.FirstOrDefault(
                identity => identity.IsAuthenticated
                && identity.HasClaim(c => c.Type == ClaimTypes.NameIdentifier));

            userId = identity?.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            return identity != null;
        }

        // TODO temporary fix https://github.com/Tenacom/Cecil.XmlDocNames/issues/80
        public static string GetFixedXmlDocName(this MemberReference reference) {
            var name = reference.GetXmlDocName();
            return name.Replace("..", ".");
        }
    }
}
