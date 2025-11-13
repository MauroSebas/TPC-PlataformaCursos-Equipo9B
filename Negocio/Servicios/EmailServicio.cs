using System;
using System.Collections.Generic;
using System.Configuration; 
using System.IO;            
using System.Web;           
using MailKit.Net.Smtp;   
using MailKit.Security;   
using MimeKit;            

namespace Negocio.Servicios
{
    
    public class EmailServicio
    {
        // Propiedades  Web.config
        private readonly string servidorSMTP;
        private readonly int puertoSMTP;
        private readonly string usuarioSMTP;
        private readonly string passwordSMTP;
        private readonly string remitenteNombre;

        public EmailServicio()
        {
            try
            {
                // Lee la configuración 
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
                
                throw new Exception("Error fatal al configurar EmailServicio. Revisa el Web.config.", ex);
            }
        }

        
        /// <param name="emailDestino">El email del destinatario.</param>
        /// <param name="asunto">El asunto del correo.</param>
        /// <param name="nombreTemplate">El nombre del archivo (ej. "ActivacionCuenta.html").</param>
        /// <param name="reemplazos">Un diccionario con los placeholders y sus valores.</param>
        public void EnviarTemplateEmail(string emailDestino, string asunto, string nombreTemplate, Dictionary<string, string> reemplazos)
        {
            string cuerpoHtml;
            try
            {
                // 1. Obtiene la ruta física del template
               
                string templatePath = HttpContext.Current.Server.MapPath($"~/EmailTemplates/{nombreTemplate}");

                // 2. Lee todo el archivo HTML
                cuerpoHtml = File.ReadAllText(templatePath);

                // 3. Reemplaza los placeholders 
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

            // 4. Llama al método  para que lo envíe
            EnviarEmailInterno(emailDestino, asunto, cuerpoHtml);
        }


       
        private void EnviarEmailInterno(string emailDestino, string asunto, string cuerpoHtml)
        {
            try
            {
                // 1. Crear el mensaje 
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(remitenteNombre, usuarioSMTP));
                email.To.Add(MailboxAddress.Parse(emailDestino));
                email.Subject = asunto;

                // 2. Definir el contenido (El cuerpo HTML)
                var builder = new BodyBuilder { HtmlBody = cuerpoHtml };
                email.Body = builder.ToMessageBody();

                // 3. Conectar, Autenticar y Enviar 
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
                
                throw new Exception($"Error al enviar el email: {ex.Message}", ex);
            }
        }
    }
}