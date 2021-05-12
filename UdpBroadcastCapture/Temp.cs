using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpBroadcastCapture
{
    class Temp
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _temperatur;
        public string temperatur
        {
            get { return _temperatur; }
            set { _temperatur = value; }
        }

        public Temp(string Temp, int Id)
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
