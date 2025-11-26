using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class InscripcionNegocio
    {
        private InscripcionDatos datos = new InscripcionDatos();
        private PagoNegocio pagoNegocio = new PagoNegocio();

        public Inscripcion ObtenerInscripcionActiva(int idUsuario, int idCurso)
        {
            return datos.ObtenerActiva(idUsuario, idCurso);
        }

        public List<Inscripcion> ListarPorUsuario(int idUsuario)
        {
            try
            {
                return datos.ListarPorUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar inscripciones para el usuario.", ex);
            }
        }

        
        public int InscribirPago(int idUsuario, int idCurso, decimal monto, string urlComprobante)
        {
            
            if (string.IsNullOrEmpty(urlComprobante))
                throw new Exception("No se recibió el comprobante de pago.");

            if (ObtenerInscripcionActiva(idUsuario, idCurso) != null)
                throw new Exception("El usuario ya tiene una inscripción activa o pendiente.");

            CursoNegocio cNeg = new CursoNegocio();
            Curso curso = cNeg.BuscarCurso(idCurso);
            if (curso == null) throw new Exception("Curso no encontrado.");

            DateTime fechaInscripcion = DateTime.Today;
            bool tieneDuracionLimitada = curso.DuracionAccesoDias > 0;

            DateTime? fechaExpiracion = null;

            if (tieneDuracionLimitada)
            {
                fechaExpiracion = fechaInscripcion.AddDays(curso.DuracionAccesoDias);
            }


            Inscripcion nueva = new Inscripcion
            {
                Usuario = new Usuario { UsuarioID = idUsuario },
                Curso = new Curso { Id = idCurso },
                FechaInscripcion = fechaInscripcion,
                FechaExpiracion = fechaExpiracion,
                Estado = "Pendiente"
            };
            int idNuevaInscripcion = datos.AltaInscripcion(nueva);

           
            Pago pago = new Pago
            {
                Inscripcion = new Inscripcion { Id = idNuevaInscripcion },
                Monto = monto,
                MetodoPago = curso.ModalidadPago, 
                Estado = "Pendiente",
                UrlComprobante = urlComprobante, 
                FechaPago = DateTime.Now
            };

            
            pagoNegocio.RegistrarPago(pago);

            return idNuevaInscripcion;
        }

        public int InscribirGratuito(int idUsuario, int idCurso)
        {
            if (ObtenerInscripcionActiva(idUsuario, idCurso) != null)
                throw new Exception("El usuario ya tiene una inscripción activa para este curso.");

            CursoNegocio cNeg = new CursoNegocio();
            Curso curso = cNeg.BuscarCurso(idCurso);
            if (curso == null) throw new Exception("Curso no encontrado.");

            DateTime fechaInscripcion = DateTime.Today;
            DateTime? fechaExpiracion = null;
            if (curso.DuracionAccesoDias > 0)
                fechaExpiracion = fechaInscripcion.AddDays(curso.DuracionAccesoDias);

            Inscripcion nueva = new Inscripcion
            {
                Usuario = new Usuario { UsuarioID = idUsuario },
                Curso = new Curso { Id = idCurso },
                FechaInscripcion = fechaInscripcion,
                FechaExpiracion = fechaExpiracion,
                Estado = "Aprobado"
            };
            int idNuevaInscripcion = datos.AltaInscripcion(nueva);

            Pago pagoGratis = new Pago
            {
                Inscripcion = new Inscripcion { Id = idNuevaInscripcion },
                Monto = 0,
                MetodoPago = "Gratuito",
                Estado = "Aprobado"
            };

            
            pagoNegocio.RegistrarPago(pagoGratis);

            return idNuevaInscripcion;
        }

        public bool CursoTieneInscripciones(int idCurso)
        {
            int cantidad = datos.ContarInscripciones(idCurso);
            return cantidad > 0;
        }
    }

}


