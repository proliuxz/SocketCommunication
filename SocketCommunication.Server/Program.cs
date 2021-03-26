using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketCommunication.Server
{
    class Program
    {
        private const string Ip = "127.0.0.1";
        private const int Port = 8900;
        private const int Backlog = 10;
        static Socket _serverSocket;

        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Parse(Ip), Port));
            _serverSocket.Listen(Backlog);

            try
            {
                Console.WriteLine("Server Started");
                ListenClientConnect(cts.Token).Wait();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                cts.Cancel();
                _serverSocket.Shutdown(SocketShutdown.Both);
                _serverSocket.Close();
            }
        }

        private static async Task ListenClientConnect(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Socket clientSocket = _serverSocket.Accept();
                Console.WriteLine("Client Connected");
                clientSocket.Send(Encoding.ASCII.GetBytes($"Hello! From {_serverSocket.LocalEndPoint}"));
                await Task.Delay(1000);
            }
        }
    }
}
