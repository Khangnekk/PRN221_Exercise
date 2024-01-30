using ServerApp.Models;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

internal class Program
{
    static int numberOfClient = 0;

    static void Main(string[] args)
    {
        string host = "127.0.0.1";
        int port = 1500;
        Console.WriteLine("Server App");
        IPAddress localAddr = IPAddress.Parse(host);
        TcpListener server = new TcpListener(localAddr, port);
        server.Start();

        Console.WriteLine("******************");
        Console.WriteLine("Waiting...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("******************");
            Console.WriteLine($"Number of clients connected: {++numberOfClient}");
            Thread thread = new Thread(new ParameterizedThreadStart(ProcessClient));
            thread.Start(client);
        }
    }

    static void ProcessClient(object? obj)
    {



        try
        {
            if (obj is TcpClient)
            {
                TcpClient client = (TcpClient)obj;


                NetworkStream stream = client.GetStream();


                Thread receiveThread = new Thread(() => ReceiveData(client, stream));
                receiveThread.Start();

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            Console.WriteLine($"Number of clients connected: {--numberOfClient}");
        }
    }

    private static void ReceiveData(TcpClient client, NetworkStream stream)
    {
        while (true)
        {
            NorthwindContext context = new NorthwindContext();
            Category category = new Category();
            string id;
            var formatter = new BinaryFormatter();
            id = formatter.Deserialize(stream) as string;
            if (int.TryParse(id, out int result))
            {
                Console.WriteLine($"Received: CategoryID = {result} at {DateTime.Now}");

                // Logic: request -> response.
                category = context.Categories.FirstOrDefault(c => c.CategoryId == result);

                // Serialize and send the category object back to the client
                formatter.Serialize(stream, category);
                Console.WriteLine($"Sent: Category: {category.CategoryId} - {category.CategoryName}");
            }
            else
            {
                Console.WriteLine($"{id} cannot be converted to an integer.");
            }
        }
    }
}
