
using BAL.Common;
using System.Security.Claims;

namespace Jalaun
{
    public class Middlewares
    {
    }
    public class LoginUserMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            if (context?.User?.Identity?.IsAuthenticated == true)
            {
                SessionHelper.IsAuthenticated = true;
                SessionHelper.UserMobile =
                    context.User.FindFirstValue(ClaimTypes.MobilePhone);

                SessionHelper.UserRole =
                    context.User.FindFirstValue(ClaimTypes.Role);

                SessionHelper.UserName =
                    context.User.FindFirstValue(ClaimTypes.Name);
               
                SessionHelper.UserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            //else
            //{
            //    context.Response.Redirect("/Home/Auth");
            //    return;
            //}

            await next(context);

        }

    }

    public static class CustomLoginMiddlewareExtension
    {
        public static IApplicationBuilder CustomeMiddleSetSession(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoginUserMiddleware>();
        }
    }
}
