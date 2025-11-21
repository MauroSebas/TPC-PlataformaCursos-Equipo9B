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
                datos.setearParametro("@UrlComprobante", nuevo.UrlComprobante);

                return datos.ejecutarAccionScalar();
            }
            catch(Exception)
            {
                throw new Exception("Error de conexion.");
            }
            finally
            {
                datos.cerrarConexion();
            }
            
        }
        
        public List<Pago> ListarPagosPendientesSP()
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConSP("sp_Pago_ListarPendientes");
                datos.ejecutarLectura();

                while( datos.Lector.Read())
                {
                    Pago aux = new Pago();
                    aux.Id = (int)datos.Lector["PagoID"];
                    aux.Monto = (decimal)datos.Lector["Monto"];
                    aux.MetodoPago = (string)datos.Lector["MetodoPago"];
                    aux.Estado = (string)datos.Lector["Estado"];

                    //Objetos anidados
                    aux.Inscripcion = new Inscripcion();
                    aux.Inscripcion.Id = (int)datos.Lector["InscripcionID"];

                    aux.Inscripcion.Usuario = new Usuario();
                    aux.Inscripcion.Usuario.Email = (string)datos.Lector["Email"];

                    aux.Inscripcion.Curso = new Curso();
                    aux.Inscripcion.Curso.Titulo = (string)datos.Lector["Titulo"];

                    if (!(datos.Lector["UrlComprobante"] is DBNull))//El campo es distinto del objeto DBnull
                        aux.UrlComprobante = (string)datos.Lector["UrlComprobante"];
                    
                    if( !(datos.Lector["FechaPago"] is DBNull) )
                        aux.FechaPago = (DateTime)datos.Lector["FechaPago"];

                    if (!(datos.Lector["Observaciones"] is DBNull))
                        aux.Observaciones = (string)datos.Lector["Observaciones"];

                    lista.Add(aux);
                }

                return lista;

            }
            catch (Exception)
            {
                throw new Exception("Error de conexion.");
            }
            finally
            {
                datos.cerrarConexion();
            }
            
        }

        public void AprobarPagoSP(int idPago)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Pago_ActualizarEstado");
                datos.setearParametro("@PagoID", idPago);
                datos.setearParametro("@Estado", "Aprobado");
                datos.setearParametro("@Observaciones", "");
               
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error de conexion");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
