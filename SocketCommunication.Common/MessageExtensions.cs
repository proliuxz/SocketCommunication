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

        public static byte[] ToByteArray2(this Message msg, Message msg1)
        {
            byte[] header = msg.HeaderByte;
            byte[] length = BitConverter.GetBytes(msg.Body.Length + 8);
            byte[] body = Encoding.UTF8.GetBytes(msg.Body);

            byte[] header1 = msg1.HeaderByte;
            byte[] length1 = BitConverter.GetBytes(msg1.Body.Length + 8);
            byte[] body1 = Encoding.UTF8.GetBytes(msg1.Body);

            Console.WriteLine(BitConverter.ToString(header));
            Console.WriteLine(BitConverter.ToString(length));
            Console.WriteLine(BitConverter.ToString(body));
            Console.WriteLine(Encoding.UTF8.GetString(body));

            Console.WriteLine(BitConverter.ToString(header1));
            Console.WriteLine(BitConverter.ToString(length1));
            Console.WriteLine(BitConverter.ToString(body1));
            Console.WriteLine(Encoding.UTF8.GetString(body1));
            return header.Concat(length).Concat(body).Concat(header1).Concat(length1).Concat(body1).ToArray();
        }

        public static (byte[], byte[]) ToByteArray3(this Message msg, Message msg1)
        {
            byte[] header = msg.HeaderByte;
            byte[] length = BitConverter.GetBytes(msg.Body.Length + 8);
            byte[] body = Encoding.UTF8.GetBytes(msg.Body);

            byte[] header1 = msg1.HeaderByte;
            byte[] length1 = BitConverter.GetBytes(msg1.Body.Length + 8);
            byte[] body1 = Encoding.UTF8.GetBytes(msg1.Body);

            Console.WriteLine(BitConverter.ToString(header));
            Console.WriteLine(BitConverter.ToString(length));
            Console.WriteLine(BitConverter.ToString(body));
            Console.WriteLine(Encoding.UTF8.GetString(body));

            Console.WriteLine(BitConverter.ToString(header1));
            Console.WriteLine(BitConverter.ToString(length1));
            Console.WriteLine(BitConverter.ToString(body1));
            Console.WriteLine(Encoding.UTF8.GetString(body1));
            return (header.Concat(length).Concat(body).Concat(header1).ToArray(), length1.Concat(body1).ToArray());
        }

        public static string GetMessageBodyFormStream(this string str)
        {
            return null;
        }
    }
}
