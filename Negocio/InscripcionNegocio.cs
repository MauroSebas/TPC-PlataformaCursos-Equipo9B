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
        public int CrearInscripcion(int idUsuario, int idCurso)
        {   
 
            Inscripcion nuevaInscripcion = new Inscripcion();
            InscripcionDatos datos = new InscripcionDatos();
            CursoNegocio negocio = new CursoNegocio();
            Curso seleccionado = new Curso();
            
            seleccionado = negocio.BuscarCurso(idCurso);

            //Validar si el curso esta publicado
            nuevaInscripcion.CursoID = idCurso;

            //Validar si el Usuario esta ok
            nuevaInscripcion.UsuarioID = idUsuario;

            //Validar FechaInscripcion y FechaExpiracion
            DateTime fechaInscripcion = DateTime.Today;
            nuevaInscripcion.FechaInscripcion = fechaInscripcion;

            int cantDiasAccesoCurso = seleccionado.DuracionAccesoDias;

            if (cantDiasAccesoCurso > 0)
            {
                DateTime fechaExpiracionInscripcion = fechaInscripcion.AddDays(cantDiasAccesoCurso);

                nuevaInscripcion.FechaExpiracion = fechaExpiracionInscripcion;

            }
            else
            {
                nuevaInscripcion.FechaExpiracion = null;
            }

            //Validar Estado
            nuevaInscripcion.Estado = "Pendiente";


            return datos.AltaInscripcion(nuevaInscripcion);

        }

        public Inscripcion ObtenerInscripcion(int idUsuario, int idCurso)
        {
            InscripcionDatos datos = new InscripcionDatos();
            Inscripcion seleccionada = new Inscripcion();

            //Validaciones a idUsuario e idCurso
            if (idUsuario <= 0 || idCurso <= 0) throw new Exception("IDs inválidos");

            seleccionada = datos.BuscarInscripcion(idUsuario, idCurso);

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
