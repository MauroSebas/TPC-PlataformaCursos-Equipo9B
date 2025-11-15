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

        public int agregarCurso(Curso nuevo)
        {
            CursoDatos datos = new CursoDatos();

            int idGenerado = datos.agregarCursoConSP(nuevo);

            //VAlidaciones
            return idGenerado;

        }


        public List<Curso>  listarCursos()
        {
            List<Curso> listaCursos = new List<Curso>();
            CursoDatos datos = new CursoDatos();

            listaCursos = datos.listarCursoConSP();

            //Hacer algun tipo de validacion

            return listaCursos;
        }

        public Curso BuscarCurso(int id)
        {
            List<Curso> lista = new List<Curso>();
            CursoDatos datos = new CursoDatos();
            Curso seleccionado = new Curso();

            seleccionado = datos.BuscarCursoPorId(id);

            //Validaciones 
            return seleccionado;

        }

        public int modificarCurso(Curso curso)
        {
            CursoDatos datos = new CursoDatos();
            int id = datos.modificarCursoConSP(curso);

            //VAlidaciones
            if (id == curso.Id)
            {
                return id;
            }
            else
            {
                return -1;
            }
        }

        public void eliminarCursoLogico(int id)
        {
            CursoDatos datos = new CursoDatos();
            datos.eliminarCursoSP(id);   
            //Validaciones 

        }


    }
}
