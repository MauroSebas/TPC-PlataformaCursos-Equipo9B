using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio; 

namespace Datos.Contenido
{
    
    public class ExamenDatos
    {
        public void Guardar(Examen examen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Examen_Guardar");

                datos.setearParametro("@CursoID", examen.CursoId);

                datos.setearParametro("@UrlConsigna", examen.UrlConsigna);

                datos.ejecutarAccion();
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

        public Examen ObtenerPorCurso(int idCurso)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Examen_ObtenerPorCurso");
                datos.setearParametro("@CursoID", idCurso);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Examen aux = new Examen();

                   
                    aux.Id = (int)datos.Lector["ExamenID"];
                    aux.CursoId = (int)datos.Lector["CursoID"];

                   
                    if (!(datos.Lector["UrlConsigna"] is DBNull))
                        aux.UrlConsigna = (string)datos.Lector["UrlConsigna"];

                    aux.EstaActivo = (bool)datos.Lector["EstaActivo"];

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