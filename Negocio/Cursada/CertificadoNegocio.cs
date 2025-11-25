using System;
using System.Collections.Generic;
using Datos;
using Dominio;

namespace Negocio
{
    public class CertificadoNegocio
    {
        private CertificadoDatos datos = new CertificadoDatos();

        // Este método lo llama el Admin cuando aprueba la entrega
        public void GenerarCertificado(int idInscripcion, string urlReal)
        {
            // Validamos que llegue algo
            if (string.IsNullOrEmpty(urlReal))
            {
                throw new Exception("La URL del certificado no puede estar vacía.");
            }

            Certificado nuevo = new Certificado();
            nuevo.InscripcionId = idInscripcion;
            nuevo.UrlArchivo = urlReal;

            datos.Generar(nuevo);
        }

        // Este método lo usa el Alumno para ver sus logros
        public List<Certificado> ListarMisCertificados(int idUsuario)
        {
            return datos.ListarPorUsuario(idUsuario);
        }
    }
}