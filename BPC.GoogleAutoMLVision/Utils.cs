using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BPC.GoogleAutoMLVision
{
    class Utils
    {
        public static string StreamToBase64(Stream stream)
        {
            byte[] bytes;            
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(bytes);
        }
    }
}
