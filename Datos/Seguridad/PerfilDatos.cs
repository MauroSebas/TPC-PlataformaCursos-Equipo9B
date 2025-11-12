using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class PerfilDatos
    {
        public Perfil ObtenerPerfilPorUsuarioID(int usuarioID)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT PerfilID, UsuarioID, Nombre, Apellido, UrlFotoPerfil, Localidad 
                    FROM Perfil 
                    WHERE UsuarioID = @UsuarioID");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@UsuarioID", usuarioID);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Perfil perfil = new Perfil
                    {
                        PerfilID = (int)datos.Lector["PerfilID"],
                        UsuarioID = (int)datos.Lector["UsuarioID"],
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : null,
                        Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : null,
                        UrlFotoPerfil = datos.Lector["UrlFotoPerfil"] != DBNull.Value ? (string)datos.Lector["UrlFotoPerfil"] : null,
                        Localidad = datos.Lector["Localidad"] != DBNull.Value ? (string)datos.Lector["Localidad"] : null
                    };
                    return perfil;
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

        public void ActualizarPerfil(Perfil perfil)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Perfil 
                    SET Nombre = @Nombre, 
                        Apellido = @Apellido, 
                        UrlFotoPerfil = @UrlFotoPerfil, 
                        Localidad = @Localidad
                    WHERE UsuarioID = @UsuarioID");

                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Nombre", perfil.Nombre ?? (object)DBNull.Value);
                datos.Comando.Parameters.AddWithValue("@Apellido", perfil.Apellido ?? (object)DBNull.Value);
                datos.Comando.Parameters.AddWithValue("@UrlFotoPerfil", perfil.UrlFotoPerfil ?? (object)DBNull.Value);
                datos.Comando.Parameters.AddWithValue("@Localidad", perfil.Localidad ?? (object)DBNull.Value);
                datos.Comando.Parameters.AddWithValue("@UsuarioID", perfil.UsuarioID);

                datos.ejecutarAccion();
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

        public void InsertarPerfilVacio(int usuarioID)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Perfil (UsuarioID) VALUES (@UsuarioID)");
                datos.setearParametro("@UsuarioID", usuarioID);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
