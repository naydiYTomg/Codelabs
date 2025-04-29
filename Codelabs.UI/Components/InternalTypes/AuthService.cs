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

        public async Task<bool> IsAuthorized()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return user.Identity.IsAuthenticated;
        }

        public async Task<RoleType?> GetUserRole()
        {
            if (await IsAuthorized())
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

        public async Task<int?> GetUserID()
        {
            if (await IsAuthorized())
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
