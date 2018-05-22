using Fleck;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocket
{
    class Program
    {
        private static readonly ConcurrentDictionary<int, IWebSocketConnection> Connections = new ConcurrentDictionary<int, IWebSocketConnection>();

        static void Main(string[] args)
        {
            var port = args.FirstOrDefault() ?? "2356";
            var uri = string.Format("ws://127.0.0.1:{0}/", port);

            Console.WriteLine("WebSocketDemo.Server");
            Console.WriteLine("========================================");
            Console.WriteLine("Type a message, or an empty line to exit");
            Console.WriteLine("========================================");

            using (var server = new WebSocketServer(uri))
            {
                var idSeed = 0;
                server.Start(connection =>
                {
                    var id = Interlocked.Increment(ref idSeed);
                    connection.OnOpen = () => Write(id, "Open: {0}", id);
                    connection.OnClose = () => Write(id, "Close: {0}", id);
                    connection.OnError = ex => Write(-1, "Error: {0} - {1}", id, ex.Message);
                    connection.OnMessage = m => Write(id, "User {0}: {1}", id, m);
                    Connections.TryAdd(id, connection);
                });

                while (true)
                {
                    var line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;
                    Write(0, "Admin: {0}", line);
                }
            }
        }

        private static void Write(int id, string format, params object[] args)
        {
            var message = string.Format(format, args);
            if (id != -1)
            {
                foreach (var connectionPair in Connections)
                {
                    if (!connectionPair.Value.IsAvailable)
                        continue;

                    if (connectionPair.Key == id)
                        continue;

                    connectionPair.Value.Send(message).Wait();
                }
            }
            var consoleMessage = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), message);
            Console.WriteLine(consoleMessage);
        }
    }
}
