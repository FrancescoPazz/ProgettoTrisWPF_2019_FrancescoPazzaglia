using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPServer
{
    public static void Main()
    {
        TcpListener server = null;
        try
        {
            int port = 56000;
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddress, port);
            server.Start();
            Byte[] bytes = new Byte[256];
            String data = null;
            while (true)
            {
                Console.Write("In attesa di una connessione... ");
                TcpClient client = server.AcceptTcpClient();
                TcpClient client2 = server.AcceptTcpClient();
                Console.WriteLine("Connesso!");

                data = null;
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Ricevuto: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Inviato: {0}", data);
                }

                // Shutdown and end connection
                client.Close();
                client2.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            // Stop listening for new clients.
            server.Stop();
        }

        Console.WriteLine("\nPremi un tasto qualsiasi per continuare...");
        Console.Read();
    }
}