using System;
using System.Collections.Generic;
using System.Configuration; 
using System.IO;            
using System.Web;           
using MailKit.Net.Smtp;   // El "Camión de Correo" de MailKit
using MailKit.Security;   // Para SecureSocketOptions
using MimeKit;            // La "Carta" de MailKit

namespace Negocio.Servicios
{
    /// <summary>
    /// Servicio "Especialista" en envío de correos.
    /// Usa MailKit para la conexión y lee templates HTML.
    /// </summary>
    public class EmailServicio
    {
        // Propiedades privadas leídas desde Web.config
        private readonly string servidorSMTP;
        private readonly int puertoSMTP;
        private readonly string usuarioSMTP;
        private readonly string passwordSMTP;
        private readonly string remitenteNombre;

        /// <summary>
        /// Constructor: Lee la configuración desde el Web.config
        /// en el momento en que se crea el servicio.
        /// </summary>
        public EmailServicio()
        {
            try
            {
                // Leemos la configuración (Tarea 2)
                servidorSMTP = ConfigurationManager.AppSettings["Email_SMTP_Server"];
                puertoSMTP = int.Parse(ConfigurationManager.AppSettings["Email_SMTP_Port"]);
                usuarioSMTP = ConfigurationManager.AppSettings["Email_User"];
                passwordSMTP = ConfigurationManager.AppSettings["Email_Password"];
                remitenteNombre = ConfigurationManager.AppSettings["Email_Remitente"];

                if (string.IsNullOrEmpty(servidorSMTP) || string.IsNullOrEmpty(usuarioSMTP) || string.IsNullOrEmpty(passwordSMTP))
                {
                    throw new Exception("Faltan configuraciones de email en el Web.config.");
                }
            }
            catch (Exception ex)
            {
                // Si falta algo en Web.config o el puerto no es un nro, esto explota.
                throw new Exception("Error fatal al configurar EmailServicio. Revisa el Web.config.", ex);
            }
        }

        /// <summary>
        /// Método público "inteligente" que lee un template HTML, reemplaza
        /// los placeholders y lo envía.
        /// </summary>
        /// <param name="emailDestino">El email del destinatario.</param>
        /// <param name="asunto">El asunto del correo.</param>
        /// <param name="nombreTemplate">El nombre del archivo (ej. "ActivacionCuenta.html").</param>
        /// <param name="reemplazos">Un diccionario con los placeholders y sus valores.</param>
        public void EnviarTemplateEmail(string emailDestino, string asunto, string nombreTemplate, Dictionary<string, string> reemplazos)
        {
            string cuerpoHtml;
            try
            {
                // 1. Obtenemos la ruta física del template
                // ¡¡ESTA LÍNEA AHORA FUNCIONA!!
                string templatePath = HttpContext.Current.Server.MapPath($"~/EmailTemplates/{nombreTemplate}");

                // 2. Leemos todo el archivo HTML
                cuerpoHtml = File.ReadAllText(templatePath);

                // 3. Reemplazamos los placeholders (ej. {{NOMBRE_USUARIO}}, {{LINK_ACTIVACION}})
                foreach (var item in reemplazos)
                {
                    cuerpoHtml = cuerpoHtml.Replace(item.Key, item.Value);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception($"No se encontró el template de email: {nombreTemplate}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer o reemplazar los placeholders del template.", ex);
            }

            // 4. Llamamos al método "tonto" para que lo envíe
            EnviarEmailInterno(emailDestino, asunto, cuerpoHtml);
        }


        /// <summary>
        /// Método privado "tonto" que solo se conecta y envía el correo.
        /// Usa MailKit.
        /// </summary>
        private void EnviarEmailInterno(string emailDestino, string asunto, string cuerpoHtml)
        {
            try
            {
                // 1. Crear el mensaje (La "Carta")
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(remitenteNombre, usuarioSMTP));
                email.To.Add(MailboxAddress.Parse(emailDestino));
                email.Subject = asunto;

                // 2. Definir el contenido (El cuerpo HTML)
                var builder = new BodyBuilder { HtmlBody = cuerpoHtml };
                email.Body = builder.ToMessageBody();

                // 3. Conectar, Autenticar y Enviar (El "Camión")
                using (var clienteSmtp = new SmtpClient())
                {
                    clienteSmtp.Connect(servidorSMTP, puertoSMTP, SecureSocketOptions.StartTls);
                    clienteSmtp.Authenticate(usuarioSMTP, passwordSMTP);
                    clienteSmtp.Send(email);
                    clienteSmtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                // Si falla (credenciales mal puestas, sin internet), lanzamos la excepción
                throw new Exception($"Error al enviar el email: {ex.Message}", ex);
            }
        }
    }
}