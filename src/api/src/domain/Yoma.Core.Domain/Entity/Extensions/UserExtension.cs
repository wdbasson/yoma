using Yoma.Core.Domain.Entity.Models;

namespace Yoma.Core.Domain.Entity.Extensions
{
    public static class UserExtension
    {
        public static void SetDisplayName(this User user)
        {
            if (!string.IsNullOrEmpty(user.DisplayName)) return;
            user.DisplayName = string.Join(' ', new[] { user.FirstName, user.Surname }.Where(o => !string.IsNullOrEmpty(o)));
            if (string.IsNullOrEmpty(user.DisplayName)) user.DisplayName = null;
        }
    }
}
