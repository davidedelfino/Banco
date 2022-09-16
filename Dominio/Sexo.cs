using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    internal class Sexo
    {
        public int codSexo { get; set; }

        public Sexo()
        {
            codSexo = 0;
        }

        public Sexo(int cod)
        {
            codSexo = cod;

        }
    }
}
