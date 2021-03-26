using SocketCommunication.Message;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Serilog;
using Serilog.Events;

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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("log.txt")
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

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
                        Log.Information(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Console.ReadKey();
            }
            finally
            {
                _clientSocket?.Close();
            }
        }
    }
}
