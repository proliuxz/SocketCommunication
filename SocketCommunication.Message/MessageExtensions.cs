using System;
using System.Linq;
using System.Text;

namespace SocketCommunication.Message
{
    public static class MessageExtensions
    {
        public static byte[] ToByteArray(this Message msg)
        {
            byte[] header = msg.HeaderByte;
            byte[] length = BitConverter.GetBytes(msg.Body.Length + 8);
            byte[] body = Encoding.UTF8.GetBytes(msg.Body);
            return header.Concat(length).Concat(body).ToArray();
        }
    }
}
