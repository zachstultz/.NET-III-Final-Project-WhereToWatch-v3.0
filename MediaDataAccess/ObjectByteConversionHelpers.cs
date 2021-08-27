using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The object byte conversion helpers.
    /// </summary>
    public static class ObjectByteConversionHelpers
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Objects the to byte array.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Bytes the array to object.
        /// </summary>
        /// <param name="arrBytes">The arr bytes.</param>
        /// <returns>An Object.</returns>
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = binForm.Deserialize(memStream);

            return obj;
        }
    }
}