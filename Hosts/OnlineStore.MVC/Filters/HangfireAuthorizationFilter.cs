using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace OnlineStore.MVC.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("Admin");
        }
    }
}
