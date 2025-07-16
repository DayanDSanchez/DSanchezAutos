using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Version
    {
        public int IdVersion { get; set; }
        public string Nombre { get; set; }
        public ML.Modelo Modelo { get; set; }
        public List<object> Versiones { get; set; }
    }
}
