using Datos.Cursada;
using Dominio.Cursada; // <--- IMPORTANTE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Cursada
{
    public class EntregaNegocio
    {
        private EntregaDatos datos = new EntregaDatos();

        public void RegistrarEntrega(int idInscripcion, int idExamen, string urlResolucion)
        {
            if (string.IsNullOrEmpty(urlResolucion))
            {
                throw new Exception("Es obligatorio ingresar el link de la resolución.");
            }

            Entrega nueva = new Entrega();
            nueva.InscripcionId = idInscripcion;
            nueva.ExamenId = idExamen;
            nueva.UrlResolucion = urlResolucion;

            datos.Registrar(nueva);
        }

        public Entrega ObtenerUltimaEntrega(int idInscripcion)
        {
            return datos.ObtenerPorInscripcion(idInscripcion);
        }

        public List<Entrega> ListarPendientes()
        {
            return datos.ListarPendientes();
        }

        public void CorregirEntrega(int idEntrega, bool aprobado, string devolucion)
        {
            string estado = aprobado ? "Aprobado" : "Rechazado";

            if (string.IsNullOrEmpty(devolucion))
            {
                if (aprobado)
                    devolucion = "¡Excelente trabajo!";
                else
                    devolucion = "Debes revisar los contenidos y volver a entregar.";
            }

            datos.Corregir(idEntrega, estado, devolucion);
        }

        public List<Entrega> ListarEntregas(string estado)
        {
            if (estado == "Todos") return datos.ListarAdmin(null);
            return datos.ListarAdmin(estado);
        }
    }
}