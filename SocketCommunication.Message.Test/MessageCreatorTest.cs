using System;
using NUnit.Framework;
using static SocketCommunication.Message.MessageCreator;

namespace SocketCommunication.Message.Test
{
    public class MessageCreatorTest
    {
        private int bodyLength1 = 10;
        private int bodyLength2 = 10;
        private int bodyLength3 = 20;

        private Message _msg1, _msg2, _msg3;
        [SetUp]
        public void Setup()
        {
            _msg1 = CreateOneMessage(bodyLength1);
            _msg2 = CreateOneMessage(bodyLength2);
            _msg3 = CreateOneMessage(bodyLength3);
        }

        [Test]
        public void TestMessageCreatorMessageHeader()
        {
            Assert.AreEqual(BitConverter.GetBytes(0x7E), _msg1.HeaderByte);
            Assert.AreEqual(BitConverter.GetBytes(0x7E), _msg2.HeaderByte);
            Assert.AreEqual(BitConverter.GetBytes(0x7E), _msg3.HeaderByte);
        }

        [Test]
        public void TestMessageCreatorMessageBody()
        {
            Assert.AreEqual(_msg1.Body.Length, _msg2.Body.Length);
            Assert.AreNotEqual(_msg1.Body, _msg2.Body);
            Assert.AreEqual(10, _msg1.Body.Length);
            Assert.AreEqual(20, _msg3.Body.Length);
        }
    }
}