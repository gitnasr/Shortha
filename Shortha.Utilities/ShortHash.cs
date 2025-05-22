using System.Security.Cryptography;
using System.Text;

namespace Shortha.Helpers
{
    public class ShortHash
    {
        private readonly string _characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private readonly int _hashLength = 5;
        private string GetRandomSeed()
        {
            return Guid.NewGuid().ToString();
        }
        public string GenerateHash(string input)
        {
            var CombinedInput = input + GetRandomSeed();

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(CombinedInput));
                var base64Hash = Convert.ToBase64String(hashBytes);
                var shortHash = new StringBuilder();
                foreach (var character in base64Hash)
                {
                    if (_characters.Contains(character))
                    {
                        shortHash.Append(character);
                    }
                    if (shortHash.Length >= _hashLength)
                    {
                        break;
                    }
                }
                return shortHash.ToString();
            }
        }
    }

}
