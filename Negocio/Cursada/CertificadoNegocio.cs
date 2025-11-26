using System;
using System.Collections.Generic;
using Datos;
using Datos.Cursada;
using Dominio;

namespace Negocio
{
    public class CertificadoNegocio
    {
        private Datos.Cursada.CertificadoDatos datos = new CertificadoDatos();

       
        public void GenerarCertificado(int idInscripcion, string urlReal)
        {
            
            if (string.IsNullOrEmpty(urlReal))
            {
                throw new Exception("La URL del certificado no puede estar vacía.");
            }

            Certificado nuevo = new Certificado();
            nuevo.InscripcionId = idInscripcion;
            nuevo.UrlArchivo = urlReal;

            datos.Generar(nuevo);
        }

       
        public List<Certificado> ListarMisCertificados(int idUsuario)
        {
            return datos.ListarPorUsuario(idUsuario);
        }
    }
}