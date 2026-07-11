using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Milkman2.Features.LogIn
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CurrentUserService(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public bool IsAuthenticated
        {
            get
            {
                var authState = _authenticationStateProvider
                    .GetAuthenticationStateAsync()
                    .GetAwaiter()
                    .GetResult();
                return authState.User.Identity?.IsAuthenticated ?? false;
            }
        }

        public int? UserId
        {
            get
            {
                var authState = _authenticationStateProvider
                    .GetAuthenticationStateAsync()
                    .GetAwaiter()
                    .GetResult();

                var claim = authState.User.FindFirst(ClaimTypes.NameIdentifier);

                return claim == null ? null : int.Parse(claim.Value);
            }
        }

        public bool IsPreOrderApplicable
        {
            get
            {
                var authState = _authenticationStateProvider
                    .GetAuthenticationStateAsync()
                    .GetAwaiter()
                    .GetResult();

                var claim = authState.User.FindFirst(ClaimTypes.Role);

                return claim == null ? false : bool.Parse(claim.Value);
            }
        }
    }
}
