using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public sealed class ApplicationRole : IdentityRole<int>
    {
    }
}
