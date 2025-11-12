using Datos.Seguridad;
using Dominio.Enums;
using Dominio.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad
{
    public class UsuarioTokenNegocio
    {
        
        private readonly UsuarioTokenDatos datos = new UsuarioTokenDatos();
        public string GenerarToken(int usuarioID, TipoTokenEnum tipoToken)
        {
            try
            {
               
                string tokenString = Guid.NewGuid().ToString();
               
                UsuarioToken token = new UsuarioToken
                {
                    UsuarioID = usuarioID,
                    Token = tokenString,
                    TipoToken = (int)tipoToken, 

                    // 24hs de validez
                    FechaVencimiento = DateTime.Now.AddHours(24)
                };               
                
                datos.Insertar(token);
              
                return tokenString;
            }
            catch (Exception ex)
            {             
                throw new Exception("Error al generar el token en la capa de negocio.", ex);
            }
        }

        public int ValidarToken(string token, TipoTokenEnum tipoTokenEsperado)
        {
            try
            {
               
                UsuarioToken tokenEncontrado = datos.BuscarPorToken(token);

                
                if (tokenEncontrado == null)
                {                    
                    throw new Exception("El enlace de activación no es válido o no existe.");
                }

                
                // (Evita que un link de "Reset Password" se use para "Activar Cuenta")
                if (tokenEncontrado.TipoToken != (int)tipoTokenEsperado)
                {
                    throw new Exception("El enlace no corresponde a la operación solicitada.");
                }

                // 4. ¿Expiró?
                if (tokenEncontrado.FechaVencimiento < DateTime.Now)
                {
                    datos.Eliminar(tokenEncontrado.TokenID);
                    throw new Exception("El enlace ha expirado. Por favor, solicitá uno nuevo.");
                }

                //Si está todo ok, se elimina para que no pueda volver a usarse.
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

