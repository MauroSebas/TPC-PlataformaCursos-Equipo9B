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
        
        private PagoDatos datos = new PagoDatos();       
        public List<Pago> ListarPagosPendientes()
        {
            try
            {              
                return datos.ListarPagosPendientes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pagos pendientes.", ex);
            }
        }
        public int RegistrarPago(Pago nuevoPago)
        {
            if (nuevoPago.Inscripcion == null || nuevoPago.Inscripcion.Id <= 0)
                throw new Exception("El pago debe estar asociado a una inscripción válida.");
            
            if (nuevoPago.Monto <= 0 && nuevoPago.Estado != "Gratuito" && nuevoPago.Estado != "Aprobado")
                throw new Exception("El monto debe ser positivo para un pago pendiente.");
          
            if (nuevoPago.MetodoPago == "Transferencia" && string.IsNullOrEmpty(nuevoPago.UrlComprobante))
            {
                throw new Exception("Es obligatorio subir un comprobante para el pago por Transferencia.");
            }

            return datos.RegistrarPago(nuevoPago);
        }
        public void AprobarPago(int idPago, string observaciones)
        {           
            try
            {
                datos.ActualizarEstado(idPago, "Aprobado", observaciones);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al aprobar pago. Consulte la DB.", ex);
            }
        }      
        public void RechazarPago(int idPago, string observaciones)
        {           
            try
            {
                datos.ActualizarEstado(idPago, "Rechazado", observaciones);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al rechazar pago. Consulte la DB.", ex);
            }
        }
        public void ActualizarComprobante(int idInscripcion, string urlComprobante, string metodoPago, string estadoPago)
        {           
            datos.ActualizarComprobante(idInscripcion, urlComprobante, metodoPago, estadoPago);
        }
        public List<Pago> ListarPagos(string estado = null)
        {
            try
            {
                return datos.ListarAdmin(estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar el listado de pagos.", ex);
            }
        }

        public List<Pago> ListarPorUsuario(int idUsuario)
        {
            try
            {
                return datos.ListarPorUsuario(idUsuario);
            }
            catch (Exception ex) { throw ex; }

        public List<Pago> FiltrarPagos(string estado, string busqueda)
        {

            try
            {
                return datos.Filtrar(estado, busqueda);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar el listado de pagos.", ex);
            }


        }
    }
}
