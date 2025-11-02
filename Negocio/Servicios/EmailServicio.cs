using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // Necesario para ConfigurationManager (leer Web.config)
using MailKit.Net.Smtp;    // Para la conexión SMTP

namespace Negocio.Servicios
{
    public class EmailServicio
    {
        // Propiedades privadas para almacenar los datos del Web.config
        private string servidorSMTP;
        private int puertoSMTP;
        private string usuarioSMTP;
        private string passwordSMTP;
        private string remitente;

        public EmailServicio()
        {
            // Leemos la configuración del Web.config al instanciar la clase
            servidorSMTP = ConfigurationManager.AppSettings["Email_SMTP_Server"];
            puertoSMTP = int.Parse(ConfigurationManager.AppSettings["Email_SMTP_Port"]);
            usuarioSMTP = ConfigurationManager.AppSettings["Email_User"];
            passwordSMTP = ConfigurationManager.AppSettings["Email_Password"];
            remitente = ConfigurationManager.AppSettings["Email_Remitente"];

            // NOTA: En un proyecto real, se deben validar que estos valores no sean null/vacíos.
        }

        /// <summary>
        /// Envía un correo electrónico a un destinatario específico.
        /// </summary>
        /// <param name="destinatario">Email del destinatario (ej. "usuario@mail.com").</param>
        /// <param name="asunto">Asunto del correo.</param>
        /// <param name="cuerpoHtml">Cuerpo del correo en formato HTML.</param>
        /// <returns>True si el envío fue exitoso, False si falló (por credenciales o conexión).</returns>
        public bool EnviarEmail(string destinatario, string asunto, string cuerpoHtml)
        {
            try
            {
                // 1. Crear el mensaje con MimeKit
                var email = new MimeMessage();
                // Usamos el remitente configurado y el email de autenticación
                email.From.Add(new MailboxAddress(remitente, usuarioSMTP));
                email.To.Add(MailboxAddress.Parse(destinatario));
                email.Subject = asunto;

                // 2. Definir el contenido (usando BodyBuilder para manejar HTML)
                var builder = new BodyBuilder { HtmlBody = cuerpoHtml };
                email.Body = builder.ToMessageBody();

                // 3. Conectar, Autenticar y Enviar con MailKit
                using (var clienteSmtp = new SmtpClient())
                {
                    // SecureSocketOptions.StartTls es el estándar para el puerto 587
                    clienteSmtp.Connect(servidorSMTP, puertoSMTP, SecureSocketOptions.StartTls);

                    // Autenticación con el usuario y la App Password de Gmail
                    clienteSmtp.Authenticate(usuarioSMTP, passwordSMTP);

                    clienteSmtp.Send(email);
                    clienteSmtp.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                // Deberías usar un sistema de logs para registrar este error ex 
                // (ej. si las credenciales de Gmail fallan).
                // throw ex; // Podrías relanzar la excepción si la capa superior necesita saber el error específico.
                return false; // Indicamos un fallo en el envío
            }
        }
    }
}

