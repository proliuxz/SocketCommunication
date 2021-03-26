using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.Common
{
    public static class MessageExtensions
    {
        public static byte[] ToByteArray(this Message msg)
        {
            byte[] header = msg.HeaderByte;
            byte[] length = BitConverter.GetBytes(msg.Body.Length + 8);
            byte[] body = Encoding.UTF8.GetBytes(msg.Body);
            Console.WriteLine(BitConverter.ToString(header));
            Console.WriteLine(BitConverter.ToString(length));
            Console.WriteLine(BitConverter.ToString(body));
            Console.WriteLine(Encoding.UTF8.GetString(body));
            return header.Concat(length).Concat(body).ToArray();
        }

        public static string GetMessageBodyFormStream(this string str)
        {
            return null;
        }
    }
}
