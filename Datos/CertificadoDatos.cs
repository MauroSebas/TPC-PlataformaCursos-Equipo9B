using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class CertificadoDatos
    {
       public int AltaCertificado(Certificado nuevo)
       {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConSP("sp_Certificado_Alta");
                datos.setearParametro("@InscripcionID", nuevo.Inscripcion.Id);
                datos.setearParametro("@UrlArchivoCertificado", nuevo.UrlArchivoCertificado);
            
                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

       }
       
       public Certificado BuscarCertificadoPorInscripcion(int idInscripcion)
       {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConSP("sp_Certificado_BuscarPorInscripcion");
                datos.setearParametro("@InscripcionID",idInscripcion);
                datos.ejecutarLectura();
                
                if (datos.Lector.Read())
                {
                    Certificado aux = new Certificado();
                    aux.Id = (int)datos.Lector["CertificadoID"];
                    aux.Inscripcion = new Inscripcion();
                    aux.Inscripcion.Id = (int)datos.Lector["InscripcionID"];
                    aux.UrlArchivoCertificado = (string)datos.Lector["UrlArchivoCertificado"];
                    aux.FechaEmision = (DateTime)datos.Lector["FechaEmision"];

                    return aux;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }
}
