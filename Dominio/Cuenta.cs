using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    internal class Cuenta
    {
        public long Cbu { get; set; }
        public double Saldo { get; set; }
        public DateTime UltimoMov { get; set; }
        public TipoCuenta TipoCuent { get; set; }
        //public Cliente Cliente { get; set; }
        public int Cliente { get; set; }
        

        public Cuenta()
        {
            Cbu = 0;
            Saldo = 0;
            UltimoMov = DateTime.Now;
            TipoCuent = null;
            //Cliente = null;
            Cliente = 0;

        }

        public Cuenta(long cbu, double saldo, DateTime ultimoMov, TipoCuenta tipo, int cliente) //Cliente cliente)
        {
            Cbu = cbu;
            Saldo = saldo;
            UltimoMov = ultimoMov;
            TipoCuent = tipo;
            //Cliente=cliente;
            Cliente = cliente;

        }
    }
}
