using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ProtoBuf;

namespace DemoProtocol.Tools
{
   public class ProtoBufTool
    {
        public static byte[] Serialize<T>(T instance)
        {
            byte[] bytes = null;
            using(MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<T>(ms, instance);
                bytes = new byte[ms.Position];  // Initialize bytes length.
                var fullbyte = ms.GetBuffer();  // get bytes from memory stream.
                Array.Copy(fullbyte, bytes, bytes.Length);
            }
            return null;
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }
    }
}
