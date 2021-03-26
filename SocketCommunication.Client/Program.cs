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
        private static byte[] result = new byte[1024];
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(Ip), Port));
                Console.WriteLine("Connected");
            }
            catch
            {
                Console.WriteLine("Connect Failed");
                return;
            }
            int receiveLength = clientSocket.Receive(result);
            Console.WriteLine(Encoding.ASCII.GetString(result, 0, receiveLength));
            Console.ReadLine();
        }
    }
}
