using SocketCommunication.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.Client
{
    class Program
    {
        private const string Ip = "127.0.0.1";
        private const int Port = 8900;
        private static readonly byte[] Result = new byte[1024];

        private static byte[] _buffer = new byte[2 * 1024 * 1024];
        private static int _offset;
        private static int _bufferLen;
        private static Socket _clientSocket;
        static void Main(string[] args)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(Ip), Port));
                Console.WriteLine("Connected");
                while (true)
                {
                    var receiveLength = _clientSocket.Receive(Result);
                    Array.Copy(Result, 0, _buffer, _bufferLen, receiveLength);
                    _bufferLen += receiveLength;
                    GetMessage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
            finally
            {
                _clientSocket?.Close();
            }
        }

        private static void GetMessage()
        {
            while (_bufferLen - _offset > 8)
            {
                int header = BitConverter.ToInt32(_buffer, _offset);
                if (header != Message.Header)
                {
                    _offset += 1;
                    continue;
                }

                int length = BitConverter.ToInt32(_buffer, _offset + 4);
                if (length < _bufferLen - _offset)
                    break;

                string str = Encoding.UTF8.GetString(_buffer, _offset + 8, length - 8);
                _offset += length;
            }

            byte[] nBuffer = new byte[2 * 1024 * 1024];
            int nBufferLen = _bufferLen - _offset;
            Array.Copy(_buffer, _offset, nBuffer, 0, nBufferLen);
            _buffer = nBuffer;
            _bufferLen = nBufferLen;
            _offset = 0;
        }
    }
}
