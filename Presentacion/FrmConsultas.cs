using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco
{
    public partial class FrmConsultas : Form
    {
        private DBHelper helper;
        public FrmConsultas()
        {
            InitializeComponent();
            helper = DBHelper.ObtenerInstancia();
        }

        private void FrmConsultas_Load(object sender, EventArgs e)
        {
            CargarDgvClientes();
        }

        private void CargarDgvClientes()
        {
            dgvClientes.DataSource = helper.ConsultarDB("SP_CONSULTAR_CLIENTES");
            
        }

        private void CargarDgvCuentas()
        {
            int idCliente = (int)dgvClientes.CurrentRow.Cells[0].Value;
            dgvCuentas.DataSource = helper.ConsultarDB("SP_CONSULTAR_CUENTAS_CLIENTE",idCliente);
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarDgvCuentas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int codigo = (int)dgvCuentas.CurrentRow.Cells[0].Value;
            helper.BajaLogicaCuenta(codigo);
            CargarDgvCuentas();


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int codigo = Convert.ToInt32(dgvCuentas.CurrentRow.Cells[0].Value);
            double saldo = Convert.ToDouble(dgvCuentas.CurrentRow.Cells[3].Value);
            DateTime ultimoMov = (DateTime)dgvCuentas.CurrentRow.Cells[2].Value;
            helper.ActualizarCuenta(saldo,ultimoMov,codigo);
            CargarDgvCuentas();

        }
    }
}
