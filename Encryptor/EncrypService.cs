using Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Encryptor
{
    public class EncrypService : IEncrypService
    {
        string IEncrypService.EncrypText(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;

            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }

            return hashString;
        }
    }
}
