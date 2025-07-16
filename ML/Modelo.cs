using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Modelo
    {
        public int IdModelo { get; set; }
        public string Nombre { get; set; }
        public ML.Marca Marca { get; set; } //Propiedad de navegacion
        public List<object> Modelos { get; set; }
    }
}
