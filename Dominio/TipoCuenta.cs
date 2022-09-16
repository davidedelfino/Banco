using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    internal class TipoCuenta
    {
        public int Tipo { get; set; }

        public TipoCuenta()
        {
            Tipo = -1;
        }

        public TipoCuenta(int tipo)
        {
            Tipo = tipo;
        }
    }
}
