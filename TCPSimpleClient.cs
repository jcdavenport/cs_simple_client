using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System;              // For String, Int32, Console, ArgumentException
//using System.Text;         // For Encoding
using System.IO;           // For IOException
using System.Net.Sockets;  // For TcpClient, NetworkStream, SocketException

namespace TCPSimpleClient
{
    class TCPSimpleClient
    {
        static void Main()
        {

            // Get Server name or IP address
            String server = "";
            Console.Write("\nEnter Server Address (Example: 129.165.23.141):");
            server = Console.ReadLine().Trim();

            // Get Port Number of Server
            Int32 servPort = 0;
            Console.Write("\nEnter Port Number of Server (Example: 7):");
            servPort = Convert.ToInt32(Console.ReadLine().Trim());

            // Get text to send
            String TextToSend = "";
            Console.Write("\nEnter Text to Send (Example: Hello):");
            TextToSend = Console.ReadLine().Trim();

            // Convert input String to bytes 
            byte[] byteBuffer = Encoding.ASCII.GetBytes(TextToSend);

            byte[] rcvBuffer = new byte[500]; //Receive buffer
            int bytesReceived; //Received byte count

            

            TcpClient client = new TcpClient(server, servPort); // Create socket that is connected to server on specified port
            NetworkStream netStream = client.GetStream();

            try
            {


                Console.WriteLine("Connected to server... sending the string:" + TextToSend);

                netStream.Write(byteBuffer, 0, byteBuffer.Length);

                Console.WriteLine("Sent {0} bytes to server... ", byteBuffer.Length);

                bytesReceived = netStream.Read(rcvBuffer, 0, rcvBuffer.Length);

                string ReceivedString = Encoding.ASCII.GetString(rcvBuffer);

                Console.WriteLine("\n\nEcho from server: " + ReceivedString);

                Console.WriteLine("Received {0} bytes from server... ", bytesReceived);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                netStream.Close();
                client.Close();
            }
            Console.Write("\nPress the Enter key to terminate the Simple Web Client Program \n\n");
            Console.ReadLine();
        }
    }
}
