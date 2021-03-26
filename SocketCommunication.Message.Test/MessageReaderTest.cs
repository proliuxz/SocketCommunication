using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SocketCommunication.Message.Test
{
    public class MessageReaderTest
    {
        private Message _msg1;
        private Message _msg2;
        private int _bodyLength1 = 20;
        private int _bodyLength2 = 30;
        private MessageReader _msgReader;
        private byte[] _header1;
        private byte[] _header2;
        private byte[] _length1;
        private byte[] _length2;
        private byte[] _body1;
        private byte[] _body2;

        [SetUp]
        public void Setup()
        {
            _msg1 = MessageCreator.CreateOneMessage(_bodyLength1);
            _msg2 = MessageCreator.CreateOneMessage(_bodyLength2);
            _msgReader = new MessageReader();

            _header1 = _msg1.HeaderByte;
            _length1 = BitConverter.GetBytes(_msg1.Body.Length + 8);
            _body1 = Encoding.UTF8.GetBytes(_msg1.Body);

            _header2 = _msg2.HeaderByte;
            _length2 = BitConverter.GetBytes(_msg2.Body.Length + 8);
            _body2 = Encoding.UTF8.GetBytes(_msg2.Body);
        }

        [Test]
        public void ReadOneMessage()
        {
            List<string> exceptedMessages1 = _msgReader.GetMessage(_msg1.ToByteArray(), _bodyLength1 + 8);
            List<string> exceptedMessages2 = _msgReader.GetMessage(_msg2.ToByteArray(), _bodyLength2 + 8);
            Assert.AreEqual(1, exceptedMessages1.Count);
            Assert.AreEqual(1, exceptedMessages2.Count);
            Assert.AreEqual(_msg1.Body, exceptedMessages1.First());
            Assert.AreEqual(_msg2.Body, exceptedMessages2.First());
        }

        [Test]
        public void ReadTwoMessagesAndSolveStickyPackages()
        {
            byte[] data = _header1.Concat(_length1).Concat(_body1).Concat(_header2).Concat(_length2).Concat(_body2).ToArray();
            List<string> exceptedMessages = _msgReader.GetMessage(data, _bodyLength1 + _bodyLength2 + 16);
            Assert.AreEqual(2, exceptedMessages.Count);
            Assert.AreEqual(_msg1.Body, exceptedMessages[0]);
            Assert.AreEqual(_msg2.Body, exceptedMessages[1]);
        }

        [Test]
        public void ReadTwoMessagesAndSolveNonsequencePackages()
        {
            byte[] data1 = _header1.Concat(_length1).Concat(_body1).Concat(_header2).ToArray();
            byte[] data2 = _length2.Concat(_body2).ToArray();
            List<string> exceptedMessages1 = _msgReader.GetMessage(data1, _bodyLength1 + 12);
            List<string> exceptedMessages2 = _msgReader.GetMessage(data2, _bodyLength2 + 4);
            Assert.AreEqual(1, exceptedMessages1.Count);
            Assert.AreEqual(1, exceptedMessages2.Count);
            Assert.AreEqual(_msg1.Body, exceptedMessages1.First());
            Assert.AreEqual(_msg2.Body, exceptedMessages2.First());
        }

        [Test]
        public void ReadTwoMessagesAndSolveUncompletePackage()
        {
            byte[] data = _body1.Concat(_header2).Concat(_length2).Concat(_body2).ToArray();
            List<string> exceptedMessages = _msgReader.GetMessage(data, _bodyLength1 + _bodyLength2 + 8);
            Assert.AreEqual(1, exceptedMessages.Count);
            Assert.AreEqual(_msg2.Body, exceptedMessages.First());
        }
    }
}