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

class Client
{
    public static async Task Main(string[] args)
    {
        TcpClient tcp_client = new TcpClient();
        await tcp_client.ConnectAsync(IPAddress.Parse("26.86.16.106"), 12345);
        Console.WriteLine("Connected\n");


        var stream = tcp_client.GetStream();

        while (true)
        {
            User user = new User();

            Console.Write("login: ");
            user.Login = Console.ReadLine();

            Console.Write("password: ");
            user.Password = Console.ReadLine();

            Console.Write("id: ");
            user.ID = Console.ReadLine();

            Console.WriteLine();


            string str = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            str += '\0';


            if (str != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);

                await stream.WriteAsync(bytes);
            }
        }
    }
}