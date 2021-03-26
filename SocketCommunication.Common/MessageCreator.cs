using System;
using System.Collections.Generic;
using System.Text;

namespace SocketCommunication.Common
{
    public static class MessageCreator
    {
        public static Message CreateOneMessage(int length)
        {
            string body = GetRndString(length);
            return new Message(body);
        }

        private static string GetRndString(int length)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null;

            var sb = new StringBuilder();
            sb.Append("0123456789");
            sb.Append("abcdefghijklmnopqrstuvwxyz");
            sb.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            for (int i = 0; i < length; i++)
                s += sb.ToString().Substring(r.Next(0, sb.Length - 1), 1);

            return s;
        }
    }
}
