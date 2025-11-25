using Datos.Contenido; 
using Dominio;        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ExamenNegocio
    {
       
        public void Guardar(int idCurso, string urlConsigna)
        {
           
            if (string.IsNullOrEmpty(urlConsigna))
            {
                throw new Exception("La URL de la consigna es obligatoria.");
            }

           
            Examen nuevoExamen = new Examen();
            nuevoExamen.CursoId = idCurso;
            nuevoExamen.UrlConsigna = urlConsigna;
            nuevoExamen.EstaActivo = true; 

           
            ExamenDatos datos = new ExamenDatos();
            datos.Guardar(nuevoExamen);
        }

        
        public Examen ObtenerPorCurso(int idCurso)
        {
            ExamenDatos datos = new ExamenDatos();
            return datos.ObtenerPorCurso(idCurso);
        }
    }
}