using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco
{
    internal class DBHelper
    {
        private SqlConnection conn;
        string CadenaConexion = @"Data Source=PC;Initial Catalog=Banco;Integrated Security=True";
        private static DBHelper instancia;
        public DBHelper()
        {
            conn = new SqlConnection(CadenaConexion);
        }

        public static DBHelper ObtenerInstancia()
        {
            if(instancia==null)
                instancia = new DBHelper();
            return instancia;
        }
        public DataTable ConsultarDB(string Procedimiento)
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();

            conn.Open();
            comando.Connection = conn;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = Procedimiento;
            tabla.Load(comando.ExecuteReader());
            conn.Close();

            return tabla;
        }
        public DataTable ConsultarDB(string Procedimiento, int id)
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();

            conn.Open();
            comando.Connection = conn;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = Procedimiento;
            comando.Parameters.AddWithValue("@IdCliente", id);
            tabla.Load(comando.ExecuteReader());
            conn.Close();

            return tabla;
        }


        public void CargarCliente(Cliente oCliente)
        {
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GRABAR_CLIENTE";
            cmd.Parameters.AddWithValue("@Nombre", oCliente.Nombre);
            cmd.Parameters.AddWithValue("@Dni", oCliente.Dni);
            cmd.Parameters.AddWithValue("@Sexo", oCliente.Genero.codSexo);
            cmd.ExecuteNonQuery();
            conn.Close();

        }


        public void CargarCuenta(Cuenta oCuenta)
        {
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GRABAR_CUENTA";
            cmd.Parameters.AddWithValue("@IdCliente", oCuenta.Cliente);
            cmd.Parameters.AddWithValue("@Cbu", oCuenta.Cbu);
            cmd.Parameters.AddWithValue("@UltimoMovimiento", oCuenta.UltimoMov);
            cmd.Parameters.AddWithValue(@"Saldo", oCuenta.Saldo);
            cmd.Parameters.AddWithValue(@"Tipo", oCuenta.TipoCuent.Tipo);
            cmd.Parameters.AddWithValue("@Activo", 1);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public int ProximoCliente()
        {
            try
            {
                conn.Open();
                SqlCommand comando = new SqlCommand();
                comando.Connection= conn;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "SP_PROXIMOID_CLIENTE";
                SqlParameter param = new SqlParameter("@NewID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery ();
                int ultimoId = param.Value.GetType() == typeof(int) ? (int)param.Value : 1;
                return ultimoId;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error al cargar los datos");
                return 0;
            }

            conn.Close();
            //SqlCommand cmd = new SqlCommand();
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.CommandText = "SP_PROXIMOID_CLIENTE";
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter param = new SqlParameter();
            //param.ParameterName = "@NewID";
            //param.DbType = DbType.Int32;
            //param.Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(param);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //return Convert.ToInt32(param.Value);
        }

        public int ProximaCuenta()
        {

            try
            {
                conn.Open();
                SqlCommand comando = new SqlCommand();
                comando.Connection = conn;
                comando.CommandType=CommandType.StoredProcedure;
                comando.CommandText = "SP_PROXIMOID_CUENTA";
                SqlParameter param=new SqlParameter("@NewID",SqlDbType.Int);
                param.Direction=ParameterDirection.Output;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery ();
                int ultimoId = param.Value.GetType() == typeof(int) ? (int)param.Value : 1;
                return ultimoId;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error al cargar los datos");
                return 0;
            }
            //SqlCommand cmd = new SqlCommand();
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.CommandText = "SP_PROXIMOID_CUENTA";
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter param = new SqlParameter();
            //param.ParameterName = "@NewID";
            //param.DbType = DbType.Int32;
            //param.Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(param);
            //cmd.ExecuteNonQuery();
            //conn.Close();

            //return Convert.ToInt32(param.Value);
            conn.Close();
        }

        public void BajaLogicaCuenta(int codCuenta)
        {
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection= conn;
            cmd.CommandText = "SP_BAJA_LOGICA_CUENTA";
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CodCuenta", codCuenta);
            cmd.ExecuteNonQuery();
            conn.Close();

        }



        public void ActualizarCuenta(double saldo, DateTime ultimoMov, int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SP_ACTUALIZAR_CUENTA";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cod", codigo);
            cmd.Parameters.AddWithValue("@Saldo", saldo);
            cmd.Parameters.AddWithValue("@UltimoMov", ultimoMov);
            cmd.ExecuteNonQuery();
            conn.Close();



        }
    }
}
