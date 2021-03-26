using SocketCommunication.Message;
using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File($"log{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}.txt")
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            CancellationTokenSource cts = new CancellationTokenSource();
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Parse(Ip), Port));
            _serverSocket.Listen(Backlog);
            try
            {
                Log.Information("Server Started");
                Task task = Task.Run(() => ListenClientConnectAsync(cts.Token));
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {
                cts.Cancel();
                _serverSocket.Shutdown(SocketShutdown.Both);
                _serverSocket.Close();
            }
        }

        static async Task ListenClientConnectAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Socket clientSocket = _serverSocket.Accept();
                Log.Information("Client Connected");
                await Task.Delay(100);
                Task task = Task.Run(() => SendMsgAsync(ct, clientSocket));
            }
        }

        static async Task SendMsgAsync(CancellationToken ct, Socket clientSocket)
        {
            while (!ct.IsCancellationRequested)
            {
                for (int i = 0; i < 100; i++)
                {
                    var msg = MessageCreator.CreateOneMessage(1016);
                    clientSocket.Send(msg.ToByteArray());
                    Log.Information(msg.Body);
                }
                await Task.Delay(5000);
            }
        }
    }
}
