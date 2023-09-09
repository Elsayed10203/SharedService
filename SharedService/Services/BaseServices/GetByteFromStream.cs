using System;
using System.IO;

namespace  Services.BaseServices
{
    public static class GetByteFromStream
    {
        public static byte[] ReadFully(Stream input)
        {
            try
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {
            }
            return default;

        }
    }
}
