using BaseProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Areas.Identity.Services
{
    public class PlainTextPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            switch (hashedPassword == providedPassword)
            {
                case true:
                    return PasswordVerificationResult.Success;
                default:
                    return PasswordVerificationResult.Failed;
            }
        }
    }
}
