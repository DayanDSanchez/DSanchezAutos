using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Auto
    {
        public int IdAuto { get; set; }
        public string Color { get; set; }
        public int Asientos { get; set; }
        public int Puertas { get; set; }
        public ML.Version Version { get; set; }
        public ML.AutoImagen AutoImagen { get; set; }
        public List<object> Autos { get; set; }
    }
}
