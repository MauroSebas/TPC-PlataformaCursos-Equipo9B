using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Datos
{
    public class AccesoDatos
    {
        public List <CategoriaCurso> listar()
        {
            List<CategoriaCurso> lista = new List<CategoriaCurso>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion. ConnectionString = "server=.\\SQLEXPRESS;database=PLATAFORMACURSO_DB;integrated security=true";


                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
