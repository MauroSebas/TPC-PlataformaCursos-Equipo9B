using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
     public class CursoDatos
    {
        public int agregarCursoConSP(Curso nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_AltaCurso");
                datos.setearParametro("@CategoriaID", nuevo.Categoria.Id);
                datos.setearParametro("@Titulo", nuevo.Titulo);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@UrlImagenPortada", string.IsNullOrEmpty(nuevo.UrlImagenPortada) ? (object)DBNull.Value : nuevo.UrlImagenPortada);
                datos.setearParametro("@ModalidadPago", nuevo.ModalidadPago);
                datos.setearParametro("@DuracionAccesoDias", nuevo.DuracionAccesoDias);
                datos.setearParametro("@Publicado", nuevo.Publicado);
                datos.setearParametro("@EstaActivo", nuevo.EstaActivo);
                datos.setearParametro("@NivelDificultad", nuevo.NivelDificultad);
                datos.setearParametro("@Idioma", nuevo.Idioma);
                datos.setearParametro("@ConCertificado", nuevo.ConCertificado);
                int idNuevo = datos.ejecutarAccionScalar();
                return idNuevo;
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                
                datos.cerrarConexion();
            }
        }
        public List<Curso> listarCursoConSP()
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
                    aux.Id = (int)datos.Lector["CursoID"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["UrlImagenPortada"] is DBNull))
                        aux.UrlImagenPortada = (string)datos.Lector["UrlImagenPortada"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ModalidadPago = (string)datos.Lector["ModalidadPago"];
                    aux.DuracionAccesoDias = (int)datos.Lector["DuracionAccesoDias"];
                    aux.Publicado = (bool)datos.Lector["Publicado"];
                    aux.EstaActivo = (bool)datos.Lector["EstaActivo"];
                    aux.NivelDificultad = (string)datos.Lector["NivelDificultad"];
                    aux.Idioma = (string)datos.Lector["Idioma"];
                    aux.ConCertificado = (bool)datos.Lector["ConCertificado"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["CategoriaID"];
                    aux.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }      
        public Curso BuscarCursoPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConSP("sp_Curso_BuscarPorID");
                datos.setearParametro("@ID", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read()) 
                {
                    Curso aux = new Curso();
                    aux.Id = (int)datos.Lector["CursoID"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["UrlImagenPortada"] is DBNull))
                        aux.UrlImagenPortada = (string)datos.Lector["UrlImagenPortada"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ModalidadPago = (string)datos.Lector["ModalidadPago"];
                    aux.DuracionAccesoDias = (int)datos.Lector["DuracionAccesoDias"];
                    aux.Publicado = (bool)datos.Lector["Publicado"];
                    aux.EstaActivo = (bool)datos.Lector["EstaActivo"];
                    aux.NivelDificultad = (string)datos.Lector["NivelDificultad"];
                    aux.Idioma = (string)datos.Lector["Idioma"];
                    aux.ConCertificado = (bool)datos.Lector["ConCertificado"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["CategoriaID"];
                    aux.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    return aux;
                }
                return null; 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }      
        public int modificarCursoConSP(Curso nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
               
                datos.setearConSP("sp_ModificarCurso");

                datos.setearParametro("@CursoID", nuevo.Id);
                datos.setearParametro("@CategoriaID", nuevo.Categoria.Id);
                datos.setearParametro("@Titulo", nuevo.Titulo);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@UrlImagenPortada", string.IsNullOrEmpty(nuevo.UrlImagenPortada) ? (object)DBNull.Value : nuevo.UrlImagenPortada);
                datos.setearParametro("@ModalidadPago", nuevo.ModalidadPago);
                datos.setearParametro("@DuracionAccesoDias", nuevo.DuracionAccesoDias);
                datos.setearParametro("@Publicado", nuevo.Publicado);
                datos.setearParametro("@EstaActivo", nuevo.EstaActivo);
                datos.setearParametro("@NivelDificultad", nuevo.NivelDificultad);
                datos.setearParametro("@Idioma", nuevo.Idioma);
                datos.setearParametro("@ConCertificado", nuevo.ConCertificado);

                datos.ejecutarAccion();

                
                return nuevo.Id;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarCursoSP(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_EliminacionLogicaCurso");
                datos.setearParametro("@CursoID", id);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                
                datos.cerrarConexion();
            }
        }
        public int ContarCursosPorCategoria(int categoriaId)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Curso_ContarPorCategoria");
                datos.setearParametro("@CategoriaID", categoriaId);
                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar cursos por categoría", ex);
            }
        }
        public List<Curso> filtrarCursosConSP(string titulo, int categoriaId)
        {
            List<Curso> lista = new List<Curso>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Curso_Filtrar"); 

               
                datos.setearParametro("@Titulo", string.IsNullOrEmpty(titulo) ? (object)DBNull.Value : titulo);
                datos.setearParametro("@CategoriaID", categoriaId);
                //datos.setearParametro("@Estado", estado);

                datos.ejecutarLectura(); 

                while (datos.Lector.Read())
                {
                    
                    Curso aux = new Curso();
                    aux.Id = (int)datos.Lector["CursoID"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["UrlImagenPortada"] is DBNull))
                        aux.UrlImagenPortada = (string)datos.Lector["UrlImagenPortada"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ModalidadPago = (string)datos.Lector["ModalidadPago"];
                    aux.DuracionAccesoDias = (int)datos.Lector["DuracionAccesoDias"];
                    aux.Publicado = (bool)datos.Lector["Publicado"];
                    aux.EstaActivo = (bool)datos.Lector["EstaActivo"];
                    aux.NivelDificultad = (string)datos.Lector["NivelDificultad"];
                    aux.Idioma = (string)datos.Lector["Idioma"];
                    aux.ConCertificado = (bool)datos.Lector["ConCertificado"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["CategoriaID"];
                    aux.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar cursos en Datos", ex);
            }
            finally
            {
                
                datos.cerrarConexion();
            }
        }
        public void CambiarPublicadoSP(int cursoId, bool publico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Curso_CambiarPublicado"); 
                datos.setearParametro("@CursoID", cursoId);
                datos.setearParametro("@Publicado", publico);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado de publicación en Datos", ex);
            }
        }
        public void ActualizarImagen(int idCurso, string nuevaUrl)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Curso_ActualizarImagen");
                datos.setearParametro("@CursoID", idCurso);
                datos.setearParametro("@UrlImagenPortada", nuevaUrl);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

