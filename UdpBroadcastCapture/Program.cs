using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static UdpBroadcastCapture.RESTClient;
using UdpBroadcastCapture.Models;

namespace UdpBroadcastCapture
{
    class Program
    {
        // https://msdn.microsoft.com/en-us/library/tst0kwb1(v=vs.110).aspx
        // IMPORTANT Windows firewall must be open on UDP port 7000
        // https://www.windowscentral.com/how-open-port-windows-firewall
        // Use the network MGV-xxx to capture from local IoT devices (fake or real)
        private const int Port = 8400 ;
        //private static readonly IPAddress IpAddress = IPAddress.Parse("192.168.5.137"); 
        // Listen for activity on all network interfaces
        // https://msdn.microsoft.com/en-us/library/system.net.ipaddress.ipv6any.aspx
        private const string BaseUri = "https://simonappservice.azurewebsites.net/api/";
        static void Main()
        {
            //vi laver Temp objekt 
            /*Console.WriteLine("giv en temperatur");
            string inputtemp = Console.ReadLine();
            Temp temp = new Temp(inputtemp, 3);
            Temp temp1 = Post <Temp, Temp>(BaseUri, temp).Result;
            Console.WriteLine(temp1);*/
            int _nextID = 1;
            //int[] IntMessage;
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, Port);
            using (UdpClient socket = new UdpClient(ipEndPoint))
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(0, 0);
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast {0}", socket.Client.LocalEndPoint);
                    byte[] datagramReceived = socket.Receive(ref remoteEndPoint);

                    //string message = Encoding.ASCII.GetString(datagramReceived, 0, datagramReceived.Length);
                    string message = Encoding.ASCII.GetString(datagramReceived);
                    Console.WriteLine("Receives {0} bytes from {1} port {2} message {3}", datagramReceived.Length,
                        remoteEndPoint.Address, remoteEndPoint.Port, message);

                    string[] txtSplit = message.Split(' ');
                    int[] intSplit = Array.ConvertAll(txtSplit, s => int.Parse(s));

                    //int IntMessage = Int32.Parse(message);
                    Measurement Meas = new Measurement { Temp = intSplit[0], Humi = intSplit[1], Stoej = intSplit[2], CO2 = intSplit[3], Id = _nextID };
                    //Temp _temp = new Temp {temperatur = intSplit[0], id = _nextID};
                    //Post<Temp, Temp>(BaseUri, _temp);
                    Put<Measurement, Measurement>(BaseUri + "Measurement/1", Meas);
                    //Put<Temp, Temp>(BaseUri + "Temp/1", _temp);            
                }
            }
        }

        // To parse data from the IoT devices (depends on the protocol)
        /*private static void Parse(string response)
        {
            string[] parts = response.Split(' ');
            foreach (string part in parts)
            {
                Console.WriteLine(part);
            }
            string temperatureLine = parts[6];
            string temperatureStr = temperatureLine.Substring(temperatureLine.IndexOf(": ") + 2);
            Console.WriteLine(temperatureStr);
        }*/


    }
}
