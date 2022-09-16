using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    internal class Cliente
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long Dni { get; set; }
        public Sexo Genero { get; set; }


        public Cliente()
        {
            Nombre = String.Empty;
            Apellido=String.Empty;
            Dni = 0;
            Genero = null;
        }

        public Cliente(string nombre, string apellido, long dni, Sexo genero)
        {
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Genero = genero;

        }
    }
}
