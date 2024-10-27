using System.Net;
using System.Net.Sockets;
using System.Text;

Socket tcpSocket = new Socket(AddressFamily.InterNetwork,
                               SocketType.Stream,
                               ProtocolType.Tcp);

string url = "yandex.ru";
int port = 80;

try
{
    await tcpSocket.ConnectAsync(url, port);
    Console.WriteLine("Socket Connected");
    Console.WriteLine($"Local socket: {tcpSocket.LocalEndPoint}");
    Console.WriteLine($"Remote socket: {tcpSocket.RemoteEndPoint}");

    string message = $"GET / HTTP/1.1\r\nHost: {url}\r\nConnection: close\r\n\r\n";
    byte[] buffer = Encoding.UTF8.GetBytes(message);

    int bytes = await tcpSocket.SendAsync(buffer);

    Console.WriteLine($"we sended to server {bytes} bytes");

    byte[] responseBuffer = new byte[1024];
    StringBuilder builder = new();
    int responseSize;

    do
    {
        responseSize = await tcpSocket.ReceiveAsync(responseBuffer);
        builder.Append(Encoding.UTF8.GetString(responseBuffer));

    }while(responseSize > 0);
    

    Console.WriteLine(builder.ToString());
}
catch(Exception ex)
{
    Console.WriteLine($"Not connection: {ex.Message}");
}


await tcpSocket.DisconnectAsync(false);