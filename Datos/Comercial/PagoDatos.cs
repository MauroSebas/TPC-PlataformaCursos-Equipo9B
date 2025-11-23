using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class PagoDatos
    {        
        public int RegistrarPago(Pago nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Pago_Registrar");

                datos.setearParametro("@InscripcionID", nuevo.Inscripcion.Id);
                datos.setearParametro("@Monto", nuevo.Monto);
                datos.setearParametro("@MetodoPago", nuevo.MetodoPago);
                datos.setearParametro("@Estado", nuevo.Estado);

                datos.setearParametro("@UrlComprobante", string.IsNullOrEmpty(nuevo.UrlComprobante) ? (object)DBNull.Value : nuevo.UrlComprobante);
                datos.setearParametro("@FechaPago", nuevo.FechaPago.HasValue ? (object)nuevo.FechaPago.Value : DBNull.Value);
                datos.setearParametro("@Observaciones", string.IsNullOrEmpty(nuevo.Observaciones) ? (object)DBNull.Value : nuevo.Observaciones);

                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar pago en DB.", ex);
            }
            finally { datos.cerrarConexion(); }
        }        
        public List<Pago> ListarPagosPendientes()
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Pago_ListarPendientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearPago(datos));
                }
                return lista;
            }
            catch (Exception ex) { throw new Exception("Error al listar pagos pendientes.", ex); }
            finally { datos.cerrarConexion(); }
        }       
        public void ActualizarEstado(int idPago, string nuevoEstado, string observacionesAdmin)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Pago_ActualizarEstado");
                datos.setearParametro("@PagoID", idPago);                
                datos.setearParametro("@Estado", nuevoEstado);
                datos.setearParametro("@Observaciones", observacionesAdmin);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar estado de pago y registro.", ex);
            }
            finally { datos.cerrarConexion(); }
        } 
        public void ActualizarComprobante(int idInscripcion, string urlComprobante, string metodoPago, string estadoPago)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Pago_ActualizarComprobante");
                datos.setearParametro("@InscripcionID", idInscripcion);
                datos.setearParametro("@UrlComprobante", urlComprobante);
                datos.setearParametro("@MetodoPago", metodoPago);
                datos.setearParametro("@Estado", estadoPago);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar comprobante.", ex);
            }
            finally { datos.cerrarConexion(); }
        }

        public List<Pago> ListarAdmin(string estado = null)
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Pago_ListarAdmin");
                
                if (string.IsNullOrEmpty(estado))
                    datos.setearParametro("@Estado", DBNull.Value);
                else
                    datos.setearParametro("@Estado", estado);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {                    
                    lista.Add(MapearPago(datos));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pagos para admin.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private Pago MapearPago(AccesoDatos datos)
        {
            Pago aux = new Pago();

            aux.Id = (int)datos.Lector["PagoID"];
            aux.Monto = (decimal)datos.Lector["Monto"];
            aux.MetodoPago = (string)datos.Lector["MetodoPago"];
            aux.Estado = (string)datos.Lector["Estado"];

            if (!(datos.Lector["UrlComprobante"] is DBNull))
                aux.UrlComprobante = (string)datos.Lector["UrlComprobante"];
            if (!(datos.Lector["FechaPago"] is DBNull))
                aux.FechaPago = (DateTime)datos.Lector["FechaPago"];
            if (!(datos.Lector["Observaciones"] is DBNull))
                aux.Observaciones = (string)datos.Lector["Observaciones"];

            aux.Inscripcion = new Inscripcion();
            aux.Inscripcion.Id = (int)datos.Lector["InscripcionID"];

            aux.Inscripcion.Curso = new Curso();
            if (!(datos.Lector["Titulo"] is DBNull))
                aux.Inscripcion.Curso.Titulo = (string)datos.Lector["Titulo"];

            aux.Inscripcion.Usuario = new Usuario();
           
            if (!(datos.Lector["Email"] is DBNull))
                aux.Inscripcion.Usuario.Email = (string)datos.Lector["Email"];

            return aux;
        }
    }
}


