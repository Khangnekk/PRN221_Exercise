using ClientApp.Models;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

internal class Program
{
    private static void Main(string[] args)
    {
        string server = "127.0.0.1";
        int port = 1500;
        ConnectServer(server, port);
    }

    public static void ConnectServer(string server, int port)
    {
        string request;
        Category category = new Category();

        TcpClient client = new TcpClient(server, port);
        //NetworkStream stream = client.GetStream();

        try
        {
            while (true)
            {
                Console.WriteLine("Enter CategoryID (or press Enter to exit): ");
                request = Console.ReadLine();
                if (string.IsNullOrEmpty(request)) break;

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(request);

                using (NetworkStream stream = client.GetStream())
                {
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine($"Sent: {request}");

                    var formatter = new BinaryFormatter();
                    category = formatter.Deserialize(stream) as Category;

                    // Check if the category is not null before displaying information
                    if (category != null)
                    {
                        Console.WriteLine($"Received: CategoryID = {category.CategoryId}, Name = {category.CategoryName}");
                    }
                    else
                    {
                        Console.WriteLine($"Category with ID {request} not found.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            client.Close();
        }
    }

}
