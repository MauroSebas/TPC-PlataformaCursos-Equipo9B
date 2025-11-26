using System;
using System.Collections.Generic;
using Dominio;

namespace Datos
{
    public class CertificadoDatos
    {
        
        public void Generar(Certificado nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Certificado_Generar");

                datos.setearParametro("@InscripcionID", nuevo.InscripcionId);
                datos.setearParametro("@UrlArchivo", nuevo.UrlArchivo);

                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public List<Certificado> ListarPorUsuario(int idUsuario)
        {
            List<Certificado> lista = new List<Certificado>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Certificado_ListarPorUsuario");
                datos.setearParametro("@UsuarioID", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Certificado aux = new Certificado();
                    aux.Id = (int)datos.Lector["CertificadoID"];
                    aux.FechaEmision = (DateTime)datos.Lector["FechaEmision"];
                    aux.UrlArchivo = (string)datos.Lector["UrlArchivoCertificado"];

                    aux.NombreCurso = (string)datos.Lector["NombreCurso"];

                    if (!(datos.Lector["UrlImagenCurso"] is DBNull))
                        aux.UrlImagenCurso = (string)datos.Lector["UrlImagenCurso"];

                    lista.Add(aux);
                }
                return lista;
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