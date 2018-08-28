using Microsoft.AspNetCore.DataProtection;

namespace BaseProject.Cookies
{
    public class PlainTextDataProtectionProvider : IDataProtector
    {
        public IDataProtector CreateProtector(string purpose)
        {
            return this;
        }

        public byte[] Protect(byte[] plaintext)
        {
            return plaintext;
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return protectedData;

        }
    }
}
