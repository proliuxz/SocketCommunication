using System;
using System.Collections.Generic;
using System.Text;

namespace SocketCommunication.Message
{
    public class MessageReader
    {
        private static byte[] _buffer = new byte[2 * 1024 * 1024];
        private static int _offset;
        private static int _bufferLen;

        public List<string> GetMessage(byte[] data, int receiveLength)
        {
            List<string> result = new List<string>();
            Array.Copy(data, 0, _buffer, _bufferLen, receiveLength);
            _bufferLen += receiveLength;
            while (_bufferLen - _offset > 8)
            {
                int header = BitConverter.ToInt32(_buffer, _offset);
                if (header != Message.Header)
                {
                    _offset += 1;
                    continue;
                }

                int length = BitConverter.ToInt32(_buffer, _offset + 4);
                if (length > _bufferLen - _offset)
                    break;

                string str = Encoding.UTF8.GetString(_buffer, _offset + 8, length - 8);
                result.Add(str);
                _offset += length;
            }

            byte[] nBuffer = new byte[2 * 1024 * 1024];
            int nBufferLen = _bufferLen - _offset;
            Array.Copy(_buffer, _offset, nBuffer, 0, nBufferLen);
            _buffer = nBuffer;
            _bufferLen = nBufferLen;
            _offset = 0;
            return result;
        }
    }
}
