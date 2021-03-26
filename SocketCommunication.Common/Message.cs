﻿using System;

namespace SocketCommunication.Common
{
    public class Message
    {
        public Message(string body)
        {
            this.Body = body;
        }

        // Message Definition HEAD_DATA + Length + Body
        // 因Socket通信中协议选用TCP 不需要添加额外校验码 使用包头仅用于快速分割 0x7E在Socket会被转译 不会被使用
        public readonly int Header = 0x7E;
        public int Length => Body.Length + sizeof(int) * 2;
        public string Body { get; set; }
    }
}