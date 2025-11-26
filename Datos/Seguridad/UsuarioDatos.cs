using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos
{
    public class UsuarioDatos
    {
        public List<Usuario> Listar(
            string email = null, string nombre = null, string apellido = null,
            int? rolId = null, int? estadoCuentaId = null, bool? estaActivo = null,
            DateTime? fechaCreacionDesde = null, DateTime? fechaCreacionHasta = null,
            DateTime? fechaLoginDesde = null, DateTime? fechaLoginHasta = null,
            DateTime? fechaBajaDesde = null, DateTime? fechaBajaHasta = null
        )
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();

            try
            {
                datos.setearConSP("sp_Usuario_ListarConFiltros");

                // Filtros de Texto
                datos.setearParametro("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                datos.setearParametro("@Nombre", string.IsNullOrEmpty(nombre) ? (object)DBNull.Value : nombre);
                datos.setearParametro("@Apellido", string.IsNullOrEmpty(apellido) ? (object)DBNull.Value : apellido);

                // Filtros de IDs y Booleanos
                datos.setearParametro("@RolID", rolId.HasValue ? (object)rolId.Value : DBNull.Value);
                datos.setearParametro("@EstadoCuentaID", estadoCuentaId.HasValue ? (object)estadoCuentaId.Value : DBNull.Value);
                datos.setearParametro("@EstaActivo", estaActivo.HasValue ? (object)estaActivo.Value : DBNull.Value);

                // Filtros de Fechas
                datos.setearParametro("@FechaCreacionDesde", fechaCreacionDesde.HasValue ? (object)fechaCreacionDesde.Value : DBNull.Value);
                datos.setearParametro("@FechaCreacionHasta", fechaCreacionHasta.HasValue ? (object)fechaCreacionHasta.Value : DBNull.Value);

                datos.setearParametro("@FechaLoginDesde", fechaLoginDesde.HasValue ? (object)fechaLoginDesde.Value : DBNull.Value);
                datos.setearParametro("@FechaLoginHasta", fechaLoginHasta.HasValue ? (object)fechaLoginHasta.Value : DBNull.Value);

                datos.setearParametro("@FechaBajaDesde", fechaBajaDesde.HasValue ? (object)fechaBajaDesde.Value : DBNull.Value);
                datos.setearParametro("@FechaBajaHasta", fechaBajaHasta.HasValue ? (object)fechaBajaHasta.Value : DBNull.Value);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                   
                    lista.Add(MapearUsuarioCompleto(datos.Lector));
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios con filtros.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

       
        public Usuario BuscarPorID(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT 
                        U.UsuarioID, U.Email, U.PasswordHash, U.EstaActivo, 
                        U.FechaCreacion, U.FechaUltimoLogin, U.FechaBaja,
                        R.RolID, R.NombreRol,
                        E.EstadoCuentaID, E.NombreEstado,
                        P.PerfilID, P.Nombre, P.Apellido, P.UrlFotoPerfil, P.Localidad
                    FROM Usuario U
                    INNER JOIN Roles R ON U.RolID = R.RolID
                    INNER JOIN EstadosCuenta E ON U.EstadoCuentaID = E.EstadoCuentaID
                    INNER JOIN Perfil P ON U.UsuarioID = P.UsuarioID
                    WHERE U.UsuarioID = @ID");

                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return MapearUsuarioCompleto(datos.Lector);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario por ID.", ex);
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
                datos.setearConsulta(@"
                    SELECT 
                        U.UsuarioID, U.Email, U.PasswordHash, U.EstaActivo, 
                        U.FechaCreacion, U.FechaUltimoLogin, U.FechaBaja,
                        R.RolID, R.NombreRol,
                        E.EstadoCuentaID, E.NombreEstado,
                        P.PerfilID, P.Nombre, P.Apellido, P.UrlFotoPerfil, P.Localidad
                    FROM Usuario U
                    INNER JOIN Roles R ON U.RolID = R.RolID
                    INNER JOIN EstadosCuenta E ON U.EstadoCuentaID = E.EstadoCuentaID
                    INNER JOIN Perfil P ON U.UsuarioID = P.UsuarioID
                    WHERE U.Email = @Email");

                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return MapearUsuarioCompleto(datos.Lector);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario por Email.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        
        public int InsertarNuevo(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Usuario (
                        Email, PasswordHash, RolID, EstadoCuentaID, EstaActivo, FechaCreacion
                    ) 
                    OUTPUT INSERTED.UsuarioID 
                    VALUES (
                        @Email, @PasswordHash, @RolID, @EstadoCuentaID, @EstaActivo, @FechaCreacion
                    )");

                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Email", nuevo.Email);
                datos.Comando.Parameters.AddWithValue("@PasswordHash", (object)nuevo.PasswordHash ?? DBNull.Value);
                datos.Comando.Parameters.AddWithValue("@RolID", nuevo.RolID);
                datos.Comando.Parameters.AddWithValue("@EstadoCuentaID", nuevo.EstadoCuentaID);
                datos.Comando.Parameters.AddWithValue("@EstaActivo", nuevo.EstaActivo);
                datos.Comando.Parameters.AddWithValue("@FechaCreacion", nuevo.FechaCreacion);

                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar usuario en la capa de datos.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool Actualizar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    UPDATE Usuario SET 
                        Email = @Email, PasswordHash = @PasswordHash, RolID = @RolID, 
                        EstadoCuentaID = @EstadoCuentaID, EstaActivo = @EstaActivo,
                        FechaCreacion = @FechaCreacion, FechaUltimoLogin = @FechaUltimoLogin, 
                        FechaBaja = @FechaBaja
                    WHERE UsuarioID = @ID");

                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Email", usuario.Email);
                datos.Comando.Parameters.AddWithValue("@PasswordHash", (object)usuario.PasswordHash ?? DBNull.Value);
                datos.Comando.Parameters.AddWithValue("@RolID", usuario.RolID);
                datos.Comando.Parameters.AddWithValue("@EstadoCuentaID", usuario.EstadoCuentaID);
                datos.Comando.Parameters.AddWithValue("@EstaActivo", usuario.EstaActivo);
                datos.Comando.Parameters.AddWithValue("@FechaCreacion", usuario.FechaCreacion);
                datos.Comando.Parameters.AddWithValue("@FechaUltimoLogin", (object)usuario.FechaUltimoLogin ?? DBNull.Value);
                datos.Comando.Parameters.AddWithValue("@FechaBaja", (object)usuario.FechaBaja ?? DBNull.Value);
                datos.Comando.Parameters.AddWithValue("@ID", usuario.UsuarioID);

                int filas = datos.ejecutarAccion(true);
                return filas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario en la capa de datos.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        
        //MAPEO AUXILIAR 
        
        private Usuario MapearUsuarioCompleto(SqlDataReader lector)
        {
            Usuario user = new Usuario
            {
                UsuarioID = (int)lector["UsuarioID"],
                Email = (string)lector["Email"],
                PasswordHash = lector["PasswordHash"] is DBNull ? null : (string)lector["PasswordHash"],
                EstaActivo = (bool)lector["EstaActivo"],
                FechaCreacion = (DateTime)lector["FechaCreacion"],
                FechaUltimoLogin = lector["FechaUltimoLogin"] is DBNull ? null : (DateTime?)lector["FechaUltimoLogin"],
                FechaBaja = lector["FechaBaja"] is DBNull ? null : (DateTime?)lector["FechaBaja"],

                RolID = (int)lector["RolID"],
                Rol = new Rol
                {
                    RolID = (int)lector["RolID"],
                    NombreRol = (string)lector["NombreRol"]
                },

                EstadoCuentaID = (int)lector["EstadoCuentaID"],
                EstadoCuenta = new EstadoCuenta
                {
                    EstadoCuentaID = (int)lector["EstadoCuentaID"],
                    NombreEstado = (string)lector["NombreEstado"]
                },

                Perfil = new Perfil
                {
                    PerfilID = (int)lector["PerfilID"],
                    UsuarioID = (int)lector["UsuarioID"],
                    Nombre = lector["Nombre"] is DBNull ? null : (string)lector["Nombre"],
                    Apellido = lector["Apellido"] is DBNull ? null : (string)lector["Apellido"],
                    UrlFotoPerfil = lector["UrlFotoPerfil"] is DBNull ? null : (string)lector["UrlFotoPerfil"],
                    Localidad = lector["Localidad"] is DBNull ? null : (string)lector["Localidad"]
                }
            };
            return user;
        }
    }
}