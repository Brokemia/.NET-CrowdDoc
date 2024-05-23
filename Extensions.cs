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
    }
}
