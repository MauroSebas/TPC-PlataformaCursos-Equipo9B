using Datos.Contenido;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contenido
{
    public class CursoObjetivoNegocio
    {
        private CursoObjetivoDatos datos = new CursoObjetivoDatos();

        public List<CursoObjetivo> Listar(int idCurso)
        {
            return datos.Listar(idCurso);
        }

        public void Agregar(CursoObjetivo nuevo)
        {
           
            if (nuevo.Curso == null || nuevo.Curso.Id <= 0)
                throw new Exception("El objetivo debe estar asociado a un curso válido.");

            if (string.IsNullOrWhiteSpace(nuevo.Descripcion))
                throw new Exception("La descripción del objetivo no puede estar vacía.");

            if (nuevo.Descripcion.Length > 300) 
                throw new Exception("La descripción es muy larga (máx 300 caracteres).");

            datos.Agregar(nuevo);
        }

        public void Eliminar(int idObjetivo)
        {
            datos.Eliminar(idObjetivo);
        }
    }
}
