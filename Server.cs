using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


class User
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string ID { get; set; }
}

class Server
{
    public static async Task Main(string[] args)
    {
        TcpListener tcp_listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
        tcp_listener.Start();

        Console.WriteLine("Server started...");

        TcpClient tcp_client = await tcp_listener.AcceptTcpClientAsync();

        await Console.Out.WriteLineAsync("Client started...\n");


        var stream = tcp_client.GetStream();
        List<byte> bytes = new List<byte>();

        while (true)
        {
            int bytes_read = 0;

            while ((bytes_read = stream.ReadByte()) != '\0')
            {
                bytes.Add((byte)bytes_read);
            }

            string str = Encoding.UTF8.GetString(bytes.ToArray());

            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(str);

            await Console.Out.WriteLineAsync($"login: {user.Login}\npassword: {user.Password}\nid: {user.ID}\n");

            bytes.Clear();
        }
    }
}