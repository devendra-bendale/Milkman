using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Features.LogIn
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(10)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(10)]
        public string Password { get; set; } = string.Empty;
    }

    public record LoggedInUser(int Id, string Name, bool IsPreOrderApplicable)
    {
        public Claim[] ToClaims() => [
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Name, Name),
            new Claim(ClaimTypes.Role, IsPreOrderApplicable.ToString())
            ];

        public static LoggedInUser? FromClaimsPrinciple(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated is true)
            {
                var id = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var name = principal.FindFirst(ClaimTypes.Name)!.Value;
                var isPreOrderApplicable = Convert.ToBoolean(principal.FindFirst(ClaimTypes.Role)!.Value);

                return new(id, name, isPreOrderApplicable);
            }
            return null;
        }
    }

    public record LoginResponseDto(LoggedInUser User, string Token);
}
