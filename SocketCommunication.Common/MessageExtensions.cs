using System;
using System.Collections.Generic;
using System.Text;

namespace SocketCommunication.Common
{
    public static class MessageExtensions
    {
        public static string ToStream(this Message msg)
        {
            var sb = new StringBuilder();
            sb.Append(msg.Header);
            sb.Append(msg.Length);
            sb.Append(msg.Body);
            return sb.ToString();
        }
    }
}
