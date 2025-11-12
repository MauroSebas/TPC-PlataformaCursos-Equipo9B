using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CursoNegocio
    {
        public void agregarConSP(Curso nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_AltaCurso");
                datos.setearParametro("@CategoriaID", nuevo.Categoria.Id);
                datos.setearParametro("@Titulo", nuevo.Titulo);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@UrlImagenPortada", nuevo.UrlImagenPortada);
                datos.setearParametro("@ModalidadPago", nuevo.ModalidadPago);
                datos.setearParametro("@DuracionAccesoDias", nuevo.DuracionAccesoDias);
                datos.setearParametro("@Publicado", nuevo.Publicado);
                datos.setearParametro("@EstaActivo", nuevo.EstaActivo);
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


        public List<Curso> listarConSP()
        {
            List<Curso> lista = new List<Curso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConSP("sp_Curso_ListarActivos");
                datos.ejecutarLectura();
                
                while (datos.Lector.Read())
                {

                    Curso aux = new Curso();
                    Categoria auxCat = new Categoria();

                    aux.Id = (int)datos.Lector["CursoID"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["UrlImagenPortada"];
                    aux.ModalidadPago = (string)datos.Lector["ModalidadPago"];
                    aux.DuracionAccesoDias = (int)datos.Lector["DuracionAccesoDias"];
                    aux.Publicado = (bool)datos.Lector["Publicado"];
                    aux.EstaActivo = (bool)datos.Lector["NombreCategoria"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["CategoriaID"];
                    aux.Categoria.Nombre = (string)datos.Lector["CategoriaNombre"];


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

        public Curso BuscarPorId(int id)
        {
            List<Curso> lista = new List<Curso>();
            AccesoDatos datos = new AccesoDatos();
            Curso seleccionado = new Curso();

            try
            {
                lista = listarConSP();

                foreach (Curso curso in lista)
                {
                    if (curso.Id == id)
                    {
                        seleccionado = curso;
                    }
                }

                return seleccionado;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void modificarConSP(Curso nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConSP("sp_ModificarCategoria");
                datos.setearParametro("@CursoID", nuevo.Id);
                datos.setearParametro("@CategoriaID", nuevo.Categoria.Id);
                datos.setearParametro("@Titulo", nuevo.Titulo);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@UrlImagenPortada", nuevo.UrlImagenPortada);
                datos.setearParametro("@ModalidadPago", nuevo.ModalidadPago);
                datos.setearParametro("@DuracionAccesoDias", nuevo.DuracionAccesoDias);
                datos.setearParametro("@Publicado", nuevo.Publicado);
                datos.setearParametro("@EstaActivo", nuevo.EstaActivo);
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

        public void eliminarLogico(int id)
        {
                AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_EliminacionLogicaCurso");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
