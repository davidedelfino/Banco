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
    public partial class FrmAltaCuenta : Form
    {
        private DBHelper helper;
        Cuenta oCuenta;
        public FrmAltaCuenta()
        {
            InitializeComponent();
            helper = DBHelper.ObtenerInstancia();

        }

        private void FrmAltaCuenta_Load(object sender, EventArgs e)
        {
            CargarDgvClientes();
            CargarCboTiposCuentas();
            ProximaCuenta();
        }

        private void CargarDgvClientes()
        {
            dgvClientes.DataSource= helper.ConsultarDB("SP_CONSULTAR_CLIENTES");
            


        }

        private void CargarCuenta()
        {
            
        }

        private bool ValidarCampos()
        {
            if (txtCbu.Text == "")
            {
                MessageBox.Show("Ingrese CBU");
                txtCbu.Focus();
                return false;
            }
            else
            {
                try
                {
                    Convert.ToInt64(txtCbu.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("CBU sólo puede contener números");
                    txtCbu.Focus();
                    return false;
                }
            }
            if (cboTipoCuenta.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione Tipo de Cuenta");
                cboTipoCuenta.Focus();
                return false;
            }
            if (txtSaldo.Text == "")
            {
                MessageBox.Show("Ingrese Saldo");
                txtSaldo.Focus();
                return false;
            }
            else
            {
                try
                {
                    Convert.ToDouble(txtSaldo.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Saldo sólo acepta números");
                    txtSaldo.Focus();
                    return false;
                }
            }

            return true;
        }

        private void CargarCboTiposCuentas()
        {
            DataTable tabla = helper.ConsultarDB("SP_TIPOSCUENTAS");
            if (tabla != null)
            {
                cboTipoCuenta.DataSource = tabla;
                cboTipoCuenta.DisplayMember = "tipo";
                cboTipoCuenta.ValueMember = "cod_tipocuenta";
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                oCuenta = new Cuenta();
                oCuenta.Saldo = Convert.ToDouble(txtSaldo.Text);
                oCuenta.Cbu = Convert.ToInt64(txtCbu.Text);
                oCuenta.UltimoMov = dtpUltimoMov.Value;
                oCuenta.TipoCuent = new TipoCuenta(Convert.ToInt32(cboTipoCuenta.SelectedValue));
                oCuenta.Cliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);

                helper.CargarCuenta(oCuenta);
                MessageBox.Show("Cuenta cargada exitosamente");

                




            }

            
        }

        private void ProximaCuenta()
        {
            lblProximaCuenta.Text += Convert.ToString(helper.ProximaCuenta());
        }
    }
}
