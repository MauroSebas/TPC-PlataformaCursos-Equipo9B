using System;
using System.Collections.Generic;
using System.Configuration; 
using System.IO;
using System.Net; 
using System.Net.Mail; 
using System.Web; 

namespace Negocio.Servicios
{

    public class EmailServicio
    {
        // Propiedades de configuración
        private readonly string servidorSMTP;
        private readonly int puertoSMTP;
        private readonly string usuarioSMTP;
        private readonly string passwordSMTP;
        private readonly string remitenteNombre;

        public EmailServicio()
        {
            try
            {
               
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

            // 4. Llama al método para que lo envíe
            EnviarEmailInterno(emailDestino, asunto, cuerpoHtml);
        }

        private void EnviarEmailInterno(string emailDestino, string asunto, string cuerpoHtml)
        {
            try
            {
                // 1. Configuración del cliente SMTP nativo
                SmtpClient clienteSmtp = new SmtpClient(servidorSMTP, puertoSMTP);
                clienteSmtp.Credentials = new NetworkCredential(usuarioSMTP, passwordSMTP);
                clienteSmtp.EnableSsl = true; // Gmail requiere SSL
                clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network; // Método de envío

                // 2. Crear el mensaje nativo
                MailMessage email = new MailMessage();
                email.From = new MailAddress(usuarioSMTP, remitenteNombre);
                email.To.Add(emailDestino);
                email.Subject = asunto;
                email.Body = cuerpoHtml;
                email.IsBodyHtml = true;

                // 3. Enviar
                clienteSmtp.Send(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar el email: {ex.Message}", ex);
            }
        }
    }
}