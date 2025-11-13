using Datos;
using Dominio;
using Dominio.Enums;
using Dominio.Seguridad;
using System;

namespace Negocio.Seguridad
{
    public class UsuarioTokenNegocio
    {
        private readonly UsuarioTokenDatos datos = new UsuarioTokenDatos();
        private const int MINUTOS_COOLDOWN = 2; // <-- ¡LA REGLA DE NEGOCIO!

        /// <summary>
        /// Genera un nuevo token, PERO solo si pasó el tiempo de cooldown.
        /// </summary>
        public string GenerarToken(int usuarioID, TipoTokenEnum tipoToken)
        {
            try
            {
                // 1. REGLA DE NEGOCIO: ¿El usuario ya pidió uno hace poco?
                UsuarioToken ultimoToken = datos.ObtenerUltimoToken(usuarioID, tipoToken);

                if (ultimoToken != null)
                {
                    // Calculamos hace cuántos segundos lo pidió
                    var tiempoEspera = (DateTime.Now - ultimoToken.FechaCreacion).TotalMinutes;

                    if (tiempoEspera < MINUTOS_COOLDOWN)
                    {
                        // ¡¡AQUÍ ESTÁ TU PROTECCIÓN ANTI-BOT!!
                        int segundosRestantes = (int)((MINUTOS_COOLDOWN * 60) - (tiempoEspera * 60));
                        throw new Exception($"Ya te enviamos un correo. Por favor, esperá {segundosRestantes} segundos para volver a intentarlo.");
                    }
                }

                // 2. Si llegamos acá, o no tenía token, o ya pasó el cooldown.
                // Limpiamos los tokens viejos
                datos.EliminarTokensAnteriores(usuarioID, tipoToken);

                // 3. Generamos el nuevo
                string tokenString = Guid.NewGuid().ToString();
                UsuarioToken token = new UsuarioToken
                {
                    UsuarioID = usuarioID,
                    Token = tokenString,
                    TipoToken = (int)tipoToken,
                    FechaVencimiento = DateTime.Now.AddHours(24),
                    FechaCreacion = DateTime.Now // <-- ¡GUARDAMOS LA FECHA!
                };

                // 4. Guardamos
                datos.Insertar(token);
                return tokenString;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Por favor, esperá"))
                    throw ex; // Relanzamos el error de cooldown

                throw new Exception("Error al generar el token en la capa de negocio.", ex);
            }
        }

        /// <summary>
        /// Valida un token (GUID) que viene de un link.
        /// </summary>
        public int ValidarToken(string token, TipoTokenEnum tipoTokenEsperado)
        {
            try
            {
                UsuarioToken tokenEncontrado = datos.BuscarPorToken(token);

                if (tokenEncontrado == null)
                    throw new Exception("El enlace no es válido o no existe.");

                if (tokenEncontrado.TipoToken != (int)tipoTokenEsperado)
                    throw new Exception("El enlace no corresponde a la operación solicitada.");

                if (tokenEncontrado.FechaVencimiento < DateTime.Now)
                {
                    datos.Eliminar(tokenEncontrado.TokenID);
                    throw new Exception("El enlace ha expirado. Por favor, solicitá uno nuevo.");
                }

                // ¡VÁLIDO!
                datos.Eliminar(tokenEncontrado.TokenID);
                return tokenEncontrado.UsuarioID;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("El enlace"))
                    throw ex;

                throw new Exception("Error al validar el token en la capa de negocio.", ex);
            }
        }
    }
}