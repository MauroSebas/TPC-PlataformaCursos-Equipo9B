using Dominio.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seguridad
{
    public class UsuarioTokenDatos
    {
        public void Insertar(UsuarioToken token)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM UsuarioTokens WHERE UsuarioID = @UsuarioID AND TipoToken = @TipoToken");
                datos.Comando.Parameters.AddWithValue("@UsuarioID", token.UsuarioID);
                datos.Comando.Parameters.AddWithValue("@TipoToken", token.TipoToken);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {               
                Console.WriteLine("Error al limpiar tokens viejos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            try
            {
                datos = new AccesoDatos(); 
                datos.setearConsulta("INSERT INTO UsuarioTokens (UsuarioID, Token, TipoToken, FechaVencimiento) VALUES (@UsuarioID, @Token, @TipoToken, @FechaVencimiento)");
                datos.Comando.Parameters.Clear(); 
                datos.Comando.Parameters.AddWithValue("@UsuarioID", token.UsuarioID);
                datos.Comando.Parameters.AddWithValue("@Token", token.Token);
                datos.Comando.Parameters.AddWithValue("@TipoToken", token.TipoToken);
                datos.Comando.Parameters.AddWithValue("@FechaVencimiento", token.FechaVencimiento);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {              
                throw new Exception("Error al insertar el nuevo token en la BBDD.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public UsuarioToken BuscarPorToken(string token)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT TokenID, UsuarioID, Token, TipoToken, FechaVencimiento FROM UsuarioTokens WHERE Token = @Token");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Token", token);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new UsuarioToken
                    {
                        TokenID = (int)datos.Lector["TokenID"],
                        UsuarioID = (int)datos.Lector["UsuarioID"],
                        Token = (string)datos.Lector["Token"],
                        TipoToken = (int)datos.Lector["TipoToken"],
                        FechaVencimiento = (DateTime)datos.Lector["FechaVencimiento"]
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el token en la BBDD.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int tokenID)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM UsuarioTokens WHERE TokenID = @ID");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", tokenID);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el token consumido.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
