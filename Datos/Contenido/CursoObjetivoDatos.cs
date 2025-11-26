using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Contenido
{
    public class CursoObjetivoDatos
    {
        
        public List<CursoObjetivo> Listar(int idCurso)
        {
            List<CursoObjetivo> lista = new List<CursoObjetivo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_CursoObjetivo_Listar");
                datos.setearParametro("@CursoID", idCurso);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    CursoObjetivo aux = new CursoObjetivo();
                    aux.Id = (int)datos.Lector["ObjetivoID"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                   

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

       
        public void Agregar(CursoObjetivo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_CursoObjetivo_Alta");               
                datos.setearParametro("@CursoID", nuevo.Curso.Id);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
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

       
        public void Eliminar(int idObjetivo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_CursoObjetivo_Eliminar");
                datos.setearParametro("@ObjetivoID", idObjetivo);
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
    }
}

