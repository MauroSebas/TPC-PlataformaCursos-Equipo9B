using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class EstadoCuentaDatos
    {
        public List<EstadoCuenta> ListarTodos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<EstadoCuenta> lista = new List<EstadoCuenta>();

            try
            {
                datos.setearConsulta("SELECT EstadoCuentaID, NombreEstado FROM EstadoCuenta ORDER BY EstadoCuentaID");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    EstadoCuenta estado = new EstadoCuenta
                    {
                        EstadoCuentaID = (int)datos.Lector["EstadoCuentaID"],
                        NombreEstado = (string)datos.Lector["NombreEstado"]
                    };
                    lista.Add(estado);
                }

                return lista;
            }
            catch (Exception)
            {
                throw; 
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public EstadoCuenta BuscarPorID(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT EstadoCuentaID, NombreEstado FROM EstadoCuenta WHERE EstadoCuentaID = @id");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new EstadoCuenta
                    {
                        EstadoCuentaID = (int)datos.Lector["EstadoCuentaID"],
                        NombreEstado = (string)datos.Lector["NombreEstado"]
                    };
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

