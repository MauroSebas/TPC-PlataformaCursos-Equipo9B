using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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
            StringBuilder consulta = new StringBuilder();

            consulta.Append(@"
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
                WHERE 1=1 ");

            // --- Armado Dinámico de Filtros  ---
            if (!string.IsNullOrEmpty(email))
            {
                consulta.Append(" AND U.Email LIKE @Email");
                datos.Comando.Parameters.AddWithValue("@Email", $"%{email}%");
            }
            if (!string.IsNullOrEmpty(nombre))
            {
                consulta.Append(" AND P.Nombre LIKE @Nombre");
                datos.Comando.Parameters.AddWithValue("@Nombre", $"%{nombre}%");
            }
            if (!string.IsNullOrEmpty(apellido))
            {
                consulta.Append(" AND P.Apellido LIKE @Apellido");
                datos.Comando.Parameters.AddWithValue("@Apellido", $"%{apellido}%");
            }
            if (rolId.HasValue)
            {
                consulta.Append(" AND U.RolID = @RolID");
                datos.Comando.Parameters.AddWithValue("@RolID", rolId.Value);
            }
            if (estadoCuentaId.HasValue)
            {
                consulta.Append(" AND U.EstadoCuentaID = @EstadoCuentaID");
                datos.Comando.Parameters.AddWithValue("@EstadoCuentaID", estadoCuentaId.Value);
            }
            if (estaActivo.HasValue)
            {
                consulta.Append(" AND U.EstaActivo = @EstaActivo");
                datos.Comando.Parameters.AddWithValue("@EstaActivo", estaActivo.Value);
            }

            // --- Filtros de Fechas  ---
            if (fechaCreacionDesde.HasValue)
            {
                consulta.Append(" AND U.FechaCreacion >= @FechaCreacionDesde");
                datos.Comando.Parameters.AddWithValue("@FechaCreacionDesde", fechaCreacionDesde.Value);
            }
            if (fechaCreacionHasta.HasValue)
            {
                consulta.Append(" AND U.FechaCreacion < @FechaCreacionHasta");
                datos.Comando.Parameters.AddWithValue("@FechaCreacionHasta", fechaCreacionHasta.Value.AddDays(1));
            }
            if (fechaLoginDesde.HasValue)
            {
                consulta.Append(" AND U.FechaUltimoLogin >= @FechaLoginDesde");
                datos.Comando.Parameters.AddWithValue("@FechaLoginDesde", fechaLoginDesde.Value);
            }
            if (fechaLoginHasta.HasValue)
            {
                consulta.Append(" AND U.FechaUltimoLogin < @FechaLoginHasta");
                datos.Comando.Parameters.AddWithValue("@FechaLoginHasta", fechaLoginHasta.Value.AddDays(1));
            }
            if (fechaBajaDesde.HasValue)
            {
                consulta.Append(" AND U.FechaBaja >= @FechaBajaDesde");
                datos.Comando.Parameters.AddWithValue("@FechaBajaDesde", fechaBajaDesde.Value);
            }
            if (fechaBajaHasta.HasValue)
            {
                consulta.Append(" AND U.FechaBaja < @FechaBajaHasta");
                datos.Comando.Parameters.AddWithValue("@FechaBajaHasta", fechaBajaHasta.Value.AddDays(1));
            }

            consulta.Append(" ORDER BY U.UsuarioID");

            try
            {
                datos.setearConsulta(consulta.ToString());
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add(MapearUsuarioCompleto(datos.Lector));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios en la capa de datos.", ex);
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
                // CONSULTA CORREGIDA
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
                // CONSULTA CORREGIDA
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