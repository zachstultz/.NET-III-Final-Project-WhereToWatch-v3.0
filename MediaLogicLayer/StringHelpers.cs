using System.Security.Cryptography;
using System.Text;

namespace MediaLogicLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The string helpers.
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Builds the SHA256Value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A string.</returns>
        public static string Sha256Value(this string source)
        {
            // create a byte array - cryptography is byte oriented
            byte[] data;

            // create a .NET hash provider object
            using (var sha256Hash = SHA256.Create())
            {
                // hash the source
                data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // now to build the result string
            var s = new StringBuilder();

            // loop through the byte array
            foreach (var t in data) s.Append(t.ToString("x2"));

            // convert the string builder to a string
            var result = s.ToString();

            return result;
        }
    }
}