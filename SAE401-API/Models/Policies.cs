using Microsoft.AspNetCore.Authorization;

namespace SAE401_API.Models
{
    public class Policies
    {
        public static AuthorizationPolicy LoginPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        }
    }
}
