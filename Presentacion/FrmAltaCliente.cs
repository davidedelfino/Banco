using System;
using System.CodeDom;
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
    public partial class FrmAltaCliente : Form
    {
        private DBHelper helper;
        Cliente oCliente;
        public FrmAltaCliente()
        {
            InitializeComponent();
            helper = DBHelper.ObtenerInstancia();
        }

        private void FrmAltaCliente_Load(object sender, EventArgs e)
        {
            CargarCboSexo();
            ProximoCliente();
        }

        private void CargarCboSexo()
        {
            DataTable tabla = helper.ConsultarDB("SP_SEXOS");
            if(tabla !=null)
            {
                cboSexo.DataSource = tabla;
                cboSexo.ValueMember = "cod_sexo";
                cboSexo.DisplayMember = "sexo";
            }
        }

        private bool ValidarCampos()
        {


            if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese Nombre");
                txtNombre.Focus();
                return false;
            }
            if(txtApellido.Text=="")
            {
                MessageBox.Show("Ingrese Apellido");
                txtApellido.Focus();
                return false;
            }
            if (cboSexo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione Sexo");
                cboSexo.Focus();
                return false;
            }
            if (txtDni.Text == "")
            {
                MessageBox.Show("Ingrese DNI");
                return false;
            }
            else
            {
                try
                {
                    Convert.ToDouble(txtDni.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("DNI debe contener sólo números");
                    txtDni.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                oCliente = new Cliente();
                oCliente.Dni = Convert.ToInt64(txtDni.Text);
                oCliente.Nombre = txtNombre.Text;
                oCliente.Apellido = txtApellido.Text;
                oCliente.Genero = new Sexo(Convert.ToInt32(cboSexo.SelectedValue));

                helper.CargarCliente(oCliente);

                MessageBox.Show("Cliente Cargado");
                txtNombre.Clear();
                txtDni.Clear();
                // ProximoCliente();
                return;
            }
        }

        private void ProximoCliente()
        {
            lblProximoCliente.Text += Convert.ToString(helper.ProximoCliente());
        }
    }
}
