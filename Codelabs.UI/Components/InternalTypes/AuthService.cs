using Codelabs.Core;
using System.Security.Claims;

namespace Codelabs.UI.Components.InternalTypes
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthorized()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return user.Identity.IsAuthenticated;
        }

        public RoleType? GetUserRole()
        {
            if (IsAuthorized())
            {
                var user = _httpContextAccessor.HttpContext.User;
                var role = (RoleType)int.Parse(user.FindFirst(ClaimTypes.Role)?.Value);
                return role;
            }
            else
            {
                return null;
            }
        }

        public int? GetUserID()
        {
            if (IsAuthorized())
            {
                var user = _httpContextAccessor.HttpContext.User;
                var ID = int.Parse(user.FindFirst(ClaimTypes.Name)?.Value);
                return ID;
            }
            else
            {
                return null;
            }
        }
    }
}
