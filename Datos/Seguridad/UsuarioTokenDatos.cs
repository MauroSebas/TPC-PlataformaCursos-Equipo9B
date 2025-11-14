using Dominio;
using Dominio.Enums; 
using Dominio.Seguridad;
using System;
using System.Data.SqlClient;

namespace Datos
{
    public class UsuarioTokenDatos
    {
        public void Insertar(UsuarioToken token)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConsulta(@"
                    INSERT INTO UsuarioTokens (UsuarioID, Token, TipoToken, FechaVencimiento, FechaCreacion) 
                    VALUES (@UsuarioID, @Token, @TipoToken, @FechaVencimiento, @FechaCreacion)");

                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@UsuarioID", token.UsuarioID);
                datos.Comando.Parameters.AddWithValue("@Token", token.Token);
                datos.Comando.Parameters.AddWithValue("@TipoToken", token.TipoToken);
                datos.Comando.Parameters.AddWithValue("@FechaVencimiento", token.FechaVencimiento);
                datos.Comando.Parameters.AddWithValue("@FechaCreacion", token.FechaCreacion); // <-- ¡NUEVA LÍNEA!

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
        public void EliminarTokensAnteriores(int usuarioID, TipoTokenEnum tipoToken)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM UsuarioTokens WHERE UsuarioID = @UsuarioID AND TipoToken = @TipoToken");
                datos.Comando.Parameters.AddWithValue("@UsuarioID", usuarioID);
                datos.Comando.Parameters.AddWithValue("@TipoToken", (int)tipoToken);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar tokens viejos.", ex);
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
                // Agregamos la nueva columna
                datos.setearConsulta("SELECT TokenID, UsuarioID, Token, TipoToken, FechaVencimiento, FechaCreacion FROM UsuarioTokens WHERE Token = @Token");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Token", token);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return MapearToken(datos.Lector); 
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
        public UsuarioToken ObtenerUltimoToken(int usuarioID, TipoTokenEnum tipoToken)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Ordena por fecha de creación DESC y toma el primero (TOP 1)
                datos.setearConsulta(@"
                    SELECT TOP 1 TokenID, UsuarioID, Token, TipoToken, FechaVencimiento, FechaCreacion 
                    FROM UsuarioTokens 
                    WHERE UsuarioID = @UsuarioID AND TipoToken = @TipoToken
                    ORDER BY FechaCreacion DESC");

                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@UsuarioID", usuarioID);
                datos.Comando.Parameters.AddWithValue("@TipoToken", (int)tipoToken);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return MapearToken(datos.Lector); 
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el último token del usuario.", ex);
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
        private UsuarioToken MapearToken(SqlDataReader lector)
        {
            return new UsuarioToken
            {
                TokenID = (int)lector["TokenID"],
                UsuarioID = (int)lector["UsuarioID"],
                Token = (string)lector["Token"],
                TipoToken = (int)lector["TipoToken"],
                FechaVencimiento = (DateTime)lector["FechaVencimiento"],
                FechaCreacion = (DateTime)lector["FechaCreacion"] 
            };
        }
    }
}