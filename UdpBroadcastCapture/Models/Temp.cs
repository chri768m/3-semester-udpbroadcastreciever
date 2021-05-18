using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpBroadcastCapture
{
    class Temp
    {
        public string Type = "Temperatur";
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _temperatur;
        public int temperatur
        {
            get { return _temperatur; }
            set { _temperatur = value; }
        }

        public Temp(int Temp, int Id)
        {
            _temperatur = Temp;
            id = Id;
        }
        public Temp()
        {


        }
        public override string ToString()
        {
            return id + " " + temperatur;
        }
    }
}
