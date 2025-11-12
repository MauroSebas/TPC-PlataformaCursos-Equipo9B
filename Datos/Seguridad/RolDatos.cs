using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class RolDatos
    {
        public List<Rol> ListarTodos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Rol> lista = new List<Rol>();

            try
            {
                datos.setearConsulta("SELECT RolID, NombreRol FROM Rol ORDER BY RolID");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Rol rol = new Rol
                    {
                        RolID = (int)datos.Lector["RolID"],
                        NombreRol = (string)datos.Lector["NombreRol"]
                    };
                    lista.Add(rol);
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

        public Rol BuscarPorID(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT RolID, NombreRol FROM Rol WHERE RolID = @id");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Rol rol = new Rol
                    {
                        RolID = (int)datos.Lector["RolID"],
                        NombreRol = (string)datos.Lector["NombreRol"]
                    };
                    return rol;
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
