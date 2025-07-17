using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public bool Correct { get; set; }  //True o False
        public string ErrorMessage { get; set; } //Cual es el error CLIENTE/USUARIO
        public Exception exception { get; set; } //Error para el programador PROGRAMADOR
        public object Object { get; set; }  //GetById  -  1 GUARDAR UN REGISTRO
        public List<object> Objects { get; set; }  //GetAll GUARDA VARIOS REGISTROS
    }
}
