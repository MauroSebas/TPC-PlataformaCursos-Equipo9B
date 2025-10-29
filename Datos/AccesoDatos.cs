using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Datos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlCommand Comando
        {
            get { return comando; }
        }
       

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            // Es mejor leer la cadena de conexión desde Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
            conexion = new SqlConnection(connectionString);
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion; 
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // --- NUEVO MÉTODO ---
        // Método para ejecutar INSERT, UPDATE, DELETE o consultas que devuelven un solo valor
        public int ejecutarAccionScalar()
        {
            comando.Connection = conexion; 
            try
            {
                conexion.Open();                
                return Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex; 
            }
           
        }
       


        public void cerrarConexion()
        {
            if (lector != null && !lector.IsClosed) 
                lector.Close();
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open) 
                conexion.Close();
        }
    }
}