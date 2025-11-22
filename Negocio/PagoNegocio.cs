using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{   
    public class PagoNegocio
    {
        public int RegistrarPagoAlumno (Pago nuevoPago)
        {
            PagoDatos datos = new PagoDatos();
            //Para obtener el Monto congelado en el codebehind tomar el monto 
            //Desde CursoNegocio.BuscarCurso(id).
  
            if (string.IsNullOrEmpty(nuevoPago.UrlComprobante))
            {
                //Envio la excepcion a la capa de vistaPago
                throw new Exception("Es obligatorio subir un comprobante.");
            }

            return datos.RegistrarPago(nuevoPago);

        }

        public List<Pago> ListarPagosPendientes()
        {

            List<Pago> listaPagosPendientes = new List<Pago>();
            PagoDatos datos = new PagoDatos();

            return datos.ListarPagosPendientesSP();
        }

        public void AprobarPago(int idPago)
        {
            PagoDatos datos = new PagoDatos();
            datos.AprobarPagoSP(idPago);

            //Rechazar Pago
        }
    }
}
