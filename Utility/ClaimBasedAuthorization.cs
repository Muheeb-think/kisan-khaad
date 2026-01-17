using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace UitilityClasses
{
    public static class ClaimBasedAccessAuthorization 
    {
        public static void AddUpdateClaim(this HttpContextAccessor currentPrincipal, string key, string value)
        {
            HttpContextAccessor httpContextAccessor = currentPrincipal as HttpContextAccessor;
            var identity = currentPrincipal.HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            httpContextAccessor.HttpContext.User = principal;
            AuthenticationHttpContextExtensions.AuthenticateAsync(httpContextAccessor.HttpContext,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationHttpContextExtensions.SignInAsync(httpContextAccessor.HttpContext, principal);
        }

        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim.Value;
        }
    }
}
