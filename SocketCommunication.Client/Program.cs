using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketCommunication.Client
{
    class Program
    {
        private const string Ip = "127.0.0.1";
        private const int Port = 8900;
        private static byte[] result = new byte[1024];
        private static Socket _clientSocket = null;
        static void Main(string[] args)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(Ip), Port));
                Console.WriteLine("Connected");
                while (true)
                {
                    var receiveLength = _clientSocket.Receive(result);
                    Console.WriteLine(Encoding.UTF8.GetString(result, 0, receiveLength));
                }
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Connect Failed");
                return;
            }
            finally
            {
                _clientSocket?.Close();
            }
        }

        private static void MsgRec()
        {

        }
    }
}
