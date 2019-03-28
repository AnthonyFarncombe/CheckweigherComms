using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace CheckweigherComms
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 13003);
            server.Start();

            byte[] receiveBytes = new byte[ReceiveData.bytesLength];

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!\n");

                try
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        while (true)
                        {
                            int i = 0;
                            while (i < receiveBytes.Length)
                            {
                                i += stream.Read(receiveBytes, i, receiveBytes.Length - i);
                                if (i == 0 && client.Client.Poll(1, SelectMode.SelectRead))
                                {
                                    if (client.Available == 0)
                                    {
                                        client.Close();
                                        throw new IOException("Client disconnected");
                                    }
                                }
                            }

                            Console.WriteLine(DateTime.Now);
                            string hex = BitConverter.ToString(receiveBytes.Take(i).ToArray()).Replace("-", " ");
                            Console.WriteLine("Received bytes: {0}", hex);

                            ReceiveData receiveData = new ReceiveData(receiveBytes);
                            Console.WriteLine("Target Weight:\t{0}", receiveData.TargetWeight);
                            Console.WriteLine("Min Weight:\t{0}", receiveData.MinWeight);
                            Console.WriteLine("Max Weight:\t{0}", receiveData.MaxWeight);

                            SendData sendData = new SendData
                            {
                                Status = 1,
                                LiveWeight = 102,
                                TargetWeight = receiveData.TargetWeight,
                                MinWeight = receiveData.MinWeight,
                                MaxWeight = receiveData.MaxWeight
                            };

                            byte[] sendBytes = sendData.GetBytes();
                            stream.Write(sendBytes, 0, sendBytes.Length);
                            Console.WriteLine("Sent bytes: {0}\n", BitConverter.ToString(sendBytes).Replace("-", " "));
                        }
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Client disconnected");
                }
            }
        }
    }
}
