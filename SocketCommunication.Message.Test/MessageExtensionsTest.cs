using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SocketCommunication.Message.Test
{
    public class MessageExtensionsTest
    {
        private Message msg;
        private string msgBody = "Test Body";
        [SetUp]
        public void Setup()
        {
            msg = new Message(msgBody);
        }

        [Test]
        public void TestMessageBytes()
        {
            byte[] header = msg.HeaderByte;
            byte[] length = BitConverter.GetBytes(msg.Body.Length + 8);
            byte[] body = Encoding.UTF8.GetBytes(msg.Body);
            byte[] expectedBytes = header.Concat(length).Concat(body).ToArray();
            Assert.AreEqual(expectedBytes, msg.ToByteArray());
        }
    }
}