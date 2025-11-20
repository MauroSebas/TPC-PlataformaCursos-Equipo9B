using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class InscripcionNegocio
    {
        public InscripcionDatos datos { get; set;}

        public void CrearInscripcion(int idUsuario, int idCurso, DateTime fechaExpiracion)
        {   
            try
            {
                Inscripcion nuevaInscripcion = new Inscripcion();
               
                //Validar si el curso esta publicado
                nuevaInscripcion.CursoID = idCurso;
                
                //Validar si el Usuario esta ok
                nuevaInscripcion.UsuarioID = idUsuario;
                
                nuevaInscripcion.FechaInscripcion = DateTime.Today;
                nuevaInscripcion.FechaExpiracion = fechaExpiracion;
                
                //Los argumentos los obtengo previemente en el evento aceptar:
                //idUsuario = int.Parse( ddlCurso.SelectedValue )
                //idCurso = int.Parse( ddlCurso.SelectedValue )
                //fechaExpiracion=> calendario = nuevaInscripcion.CursoID;

                datos.AltaInscripcion(nuevaInscripcion);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public Inscripcion ObtenerInscripcion(int idUsuario, int idCurso)
        {
            try
            {
                //Validaciones a idUsuario e idCurso

                return datos.BuscarInscripcion(idUsuario,idCurso);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la Inscripcion.", ex);
            }
        }

        public List<Inscripcion> listarPorUsuario(int idUsuario)
        {
            List<Inscripcion> lista = new List<Inscripcion>();

            lista = datos.ListarUsuariosInscripcion(idUsuario);
            
            return lista;
        }
    }
}
