using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Configuracion
    {
        public int ConfiguracionID { get; set; }
        public string NombrePlataforma { get; set; }
        public string LogoUrl {get;set;}
        public string Slogan { get; set; }
        public string EmailContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string UrlFacebook { get; set; }
        public string UrlInstagram { get; set; }

    }
}
