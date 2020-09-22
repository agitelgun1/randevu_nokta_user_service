using System.Security.Cryptography;
using System.Text;

namespace UserService.Helper
{
    public static class CommonHelper
    {
        public static string GenerateSHA1(string plainTextString)
        {
            //create new instance of md5
            var sha1 = SHA1.Create();

            //convert the input text to array of bytes
            var hashData = sha1.ComputeHash(Encoding.Default.GetBytes(plainTextString));

            //create new instance of StringBuilder to save hashed data
            var returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }
    }
}