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
        public void CrearInscripcion(int idUsuario, int idCurso, DateTime fechaExpiracion)
        {   
 
            Inscripcion nuevaInscripcion = new Inscripcion();
            InscripcionDatos datos = new InscripcionDatos();

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

        public Inscripcion ObtenerInscripcion(int idUsuario, int idCurso)
        {
            InscripcionDatos datos = new InscripcionDatos();
            Inscripcion seleccionada = new Inscripcion();

            //Validaciones a idUsuario e idCurso

            seleccionada = datos.BuscarInscripcion(idUsuario, idCurso);

            if ( seleccionada is null)
            {
                throw new Exception("No se encontró ninguna inscripcion para Usuario y Curso solicitados.");
            }

            return seleccionada; 
        
        }

        public List<Inscripcion> listarPorUsuario(int idUsuario)
        {
            InscripcionDatos datos = new InscripcionDatos();
            List<Inscripcion> lista = new List<Inscripcion>();

            lista = datos.ListarUsuariosInscripcion(idUsuario);
            
            return lista;
        }
    }
}
