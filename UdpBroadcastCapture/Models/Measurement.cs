using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpBroadcastCapture.Models
{
    class Measurement
    {
        public int Temp { get; set; }
        public int Humi { get; set; }
        public int Id { get; set; }
        public int Stoej { get; set; }
        public int CO2 { get; set; }

        public Measurement(int temp, int humi,int stoej, int co2, int id)
        {
            Temp = temp;
            Humi = humi;
            Stoej = stoej;
            CO2 = co2;
            Id = id;
        }
        public Measurement()
        {

        }

        public override string ToString()
        {
            return Temp + " " + Humi + " " + Stoej;
        }
    }
}
