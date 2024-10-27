// CLIENT

using System.Net.Sockets;
using System.Net;
using System.Text;

Socket tcpClient = new Socket(AddressFamily.InterNetwork,
                               SocketType.Stream,
                               ProtocolType.Tcp);


await tcpClient.ConnectAsync(IPAddress.Loopback, 5000);

Console.WriteLine($"My endpoint: {tcpClient.LocalEndPoint}");
Console.WriteLine($"Server endpoint: {tcpClient.RemoteEndPoint}");

NetworkStream writer = new NetworkStream(tcpClient);

var buffer = Encoding.UTF8.GetBytes("Hello world");

await writer.WriteAsync(buffer);

Console.WriteLine("Data write to server");