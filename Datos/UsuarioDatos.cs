using System;
using Dominio;

namespace Datos
{
    public class UsuarioDatos
    {
        public int InsertarNuevo(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Usuario (Email, PasswordHash, RolID, EstadoCuentaID, EstaActivo, UrlFotoPerfil) " +
                                     "OUTPUT INSERTED.UsuarioID " +
                                     "VALUES (@Email, @PasswordHash, @RolID, @EstadoCuentaID, @EstaActivo, @UrlFotoPerfil)");

                
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Email", nuevo.Email);
                datos.Comando.Parameters.AddWithValue("@PasswordHash", nuevo.PasswordHash);
                datos.Comando.Parameters.AddWithValue("@RolID", nuevo.RolID);
                datos.Comando.Parameters.AddWithValue("@EstadoCuentaID", nuevo.EstadoCuentaID);
                datos.Comando.Parameters.AddWithValue("@EstaActivo", nuevo.EstaActivo);
                datos.Comando.Parameters.AddWithValue("@UrlFotoPerfil", (object)nuevo.UrlFotoPerfil ?? DBNull.Value);

                // Llamamos al nuevo método público
                int idGenerado = datos.ejecutarAccionScalar();
                return idGenerado;
            }
            catch (Exception ex)
            {
                // Considera loggear el error ex aquí
                // throw ex; // O relanzar para manejo global
                return -1; // Indicar error
            }
            finally
            {
                datos.cerrarConexion(); // Fundamental cerrar siempre
            }
        }
        public Usuario BuscarPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT UsuarioID, Email, PasswordHash, RolID, EstadoCuentaID, EstaActivo, UrlFotoPerfil " +
                                     "FROM Usuario WHERE Email = @Email");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Email", email);

                datos.ejecutarLectura(); // Abre conexión y ejecuta ExecuteReader

                if (datos.Lector.Read()) // Si encontró una fila
                {
                    Usuario user = new Usuario();
                    user.UsuarioID = (int)datos.Lector["UsuarioID"];
                    user.Email = (string)datos.Lector["Email"];
                    // Ojo con PasswordHash, puede ser DBNull
                    user.PasswordHash = datos.Lector["PasswordHash"] is DBNull ? null : (string)datos.Lector["PasswordHash"];
                    user.RolID = (int)datos.Lector["RolID"];
                    user.EstadoCuentaID = (int)datos.Lector["EstadoCuentaID"];
                    user.EstaActivo = (bool)datos.Lector["EstaActivo"];
                    // Ojo con UrlFotoPerfil, puede ser DBNull
                    user.UrlFotoPerfil = datos.Lector["UrlFotoPerfil"] is DBNull ? null : (string)datos.Lector["UrlFotoPerfil"];

                    return user; // Devuelve el usuario encontrado
                }

                return null; // No encontró usuario con ese email
            }
            catch (Exception ex)
            {
                // Loggear error ex
                throw ex; // Relanzar para que Negocio se entere
            }
            finally
            {
                datos.cerrarConexion(); // Siempre cerrar
            }
        }
        
    }
}