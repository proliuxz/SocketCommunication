using SocketCommunication.Message;
using System;
using System.Collections.Generic;
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

        private static Socket _clientSocket;
        static void Main(string[] args)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            MessageReader msgReader = new MessageReader();
            try
            {
                _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(Ip), Port));
                Console.WriteLine("Connected");
                while (true)
                {
                    var receiveLength = _clientSocket.Receive(Result);
                    List<string> messages = msgReader.GetMessage(Result, receiveLength);
                    foreach (var message in messages)
                    {
                        Console.WriteLine(message);
                    }
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
    }
}
