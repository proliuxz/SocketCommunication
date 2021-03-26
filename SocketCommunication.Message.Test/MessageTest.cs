using System;
using NUnit.Framework;

namespace SocketCommunication.Message.Test
{
    public class MessageTest
    {
        private Message msg;
        private string msgBody = "Test Body";
        [SetUp]
        public void Setup()
        {
            msg = new Message(msgBody);
        }

        [Test]
        public void TestMessageContent()
        {
            Assert.AreEqual(0x7E, Message.Header);
            Assert.AreEqual(msgBody, msg.Body);
            Assert.AreEqual(BitConverter.GetBytes(0x7E), msg.HeaderByte);
        }
    }
}