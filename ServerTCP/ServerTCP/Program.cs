using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ServerTCP
{
    class Program
    {
        public static void Main()
        {
            TcpListener ascoltatore = new TcpListener(IPAddress.Any, 56000);
            string richiesta = string.Empty;
            ascoltatore.Start();
            while (true)
            {
                Console.WriteLine("In attesa di una connessione... ");
                TcpClient client = ascoltatore.AcceptTcpClient();
                TcpClient client2 = ascoltatore.AcceptTcpClient();
                Console.WriteLine("Connesso!");
                NetworkStream stream = client.GetStream();
                StreamReader leggi = new StreamReader(client.GetStream());
                StreamWriter scrivi = new StreamWriter(client.GetStream());
                try
                {
                    byte[] buffer = new byte[256];
                    stream.Read(buffer, 0, buffer.Length);
                    int cont = 0;
                    foreach (byte b in buffer)
                        if (b != 0)
                            cont++;
                    richiesta = Encoding.ASCII.GetString(buffer, 0, cont);
                    Console.WriteLine("Richiesta ricevuta: " + richiesta);
                    scrivi.WriteLine(richiesta);
                    scrivi.Flush();
                }
                catch
                {
                    Console.WriteLine("Qualcosa è andato storto");
                }
                if (richiesta == "TRMN")
                    client.Close();
            }

            Console.WriteLine("\nPremi un tasto qualsiasi per continuare...");
            Console.Read();
        }
    }
}
