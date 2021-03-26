using System;

namespace SocketCommunication.Message
{
    public class Message
    {
        public Message(string body)
        {
            this.Body = body;
        }

        // Message Definition HEAD_DATA + Length + Body
        // 因Socket通信中协议选用TCP 不需要添加额外校验码 使用包头仅用于快速分割 0x7E在Socket会被转译 不会被使用
        public const int Header = 0x7E;
        public readonly byte[] HeaderByte = BitConverter.GetBytes(Header);
        public string Body { get; set; }
    }
}
