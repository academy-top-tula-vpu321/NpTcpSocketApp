// SERVER

using System.Net;
using System.Net.Sockets;
using System.Text;

Socket tcpServer = new(AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.Tcp);

IPAddress address = IPAddress.Loopback;
int port = 5000;
IPEndPoint endPoint = new IPEndPoint(address, port);

tcpServer.Bind(endPoint);
Console.WriteLine($"My endpoint: {tcpServer.LocalEndPoint}");
tcpServer.Listen();

Console.WriteLine("Server starting");

Socket tcpClient = await tcpServer.AcceptAsync();

Console.WriteLine($"Client endpoint: {tcpClient.RemoteEndPoint}");

NetworkStream reader = new NetworkStream(tcpClient);

byte[] buffer = new byte[1024];
int size = await reader.ReadAsync(buffer);

Console.WriteLine($"Client sended to us: {Encoding.UTF8.GetString(buffer)}");