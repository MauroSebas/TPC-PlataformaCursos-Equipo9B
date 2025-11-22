using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Dominio;


namespace Negocio
{
    public class CertificadoNegocio
    {
        public int GuardarCertificado(Certificado certificado)
        {
            CertificadoDatos datos = new CertificadoDatos();
            
            if (certificado.Inscripcion == null || certificado.Inscripcion.Id == 0)
            {
                throw new Exception("Este certificado no posee inscripcion.");
            }
            
            if (string.IsNullOrEmpty(certificado.UrlArchivoCertificado))
            {
                throw new Exception("Se debe subir el archivo del certificado.");
            }

            return datos.AltaCertificado(certificado);
        }

        public Certificado ObtenerCertificado(int idInscripcion)
        {
            CertificadoDatos datos = new CertificadoDatos();
            Certificado certificado = new Certificado();

            return datos.BuscarCertificadoPorInscripcion(idInscripcion);
        }

    }
}
