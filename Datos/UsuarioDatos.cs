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
                datos.setearConsulta("INSERT INTO Usuario (Email, PasswordHash, RolID, EstadoCuentaID, EstaActivo) " +
                                     "OUTPUT INSERTED.UsuarioID " +
                                     "VALUES (@Email, @PasswordHash, @RolID, @EstadoCuentaID, @EstaActivo)");

                
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Email", nuevo.Email);
                datos.Comando.Parameters.AddWithValue("@PasswordHash", nuevo.PasswordHash);
                datos.Comando.Parameters.AddWithValue("@RolID", nuevo.RolID);
                datos.Comando.Parameters.AddWithValue("@EstadoCuentaID", nuevo.EstadoCuentaID);
                datos.Comando.Parameters.AddWithValue("@EstaActivo", nuevo.EstaActivo);               
              
                int idGenerado = datos.ejecutarAccionScalar();
                return idGenerado;
            }
            catch (Exception ex)
            {
                // tengo que loguear el error acá -- No olvidarme.             
                return -1; // Indica error
            }
            finally
            {
                datos.cerrarConexion(); 
            }
        }
        public Usuario BuscarPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT UsuarioID, Email, PasswordHash, RolID, EstadoCuentaID, EstaActivo " +
                                     "FROM Usuario WHERE Email = @Email");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Email", email);

                datos.ejecutarLectura(); 

                if (datos.Lector.Read()) 
                {
                    Usuario user = new Usuario();
                    user.UsuarioID = (int)datos.Lector["UsuarioID"];
                    user.Email = (string)datos.Lector["Email"];
                    // PasswordHash, puede ser DBNull -- Para futuro agregar loguin externos como Gmail.
                    user.PasswordHash = datos.Lector["PasswordHash"] is DBNull ? null : (string)datos.Lector["PasswordHash"];
                    user.RolID = (int)datos.Lector["RolID"];
                    user.EstadoCuentaID = (int)datos.Lector["EstadoCuentaID"];
                    user.EstaActivo = (bool)datos.Lector["EstaActivo"];

                    return user; 
                }

                return null; 
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