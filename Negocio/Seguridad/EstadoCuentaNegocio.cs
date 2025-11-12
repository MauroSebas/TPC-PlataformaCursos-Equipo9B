using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EstadoCuentaNegocio
    {
        private readonly EstadoCuentaDatos datos = new EstadoCuentaDatos();

        public List<EstadoCuenta> ObtenerTodos()
        {
            try
            {
                return datos.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de estados de cuenta", ex);
            }
        }

        public EstadoCuenta ObtenerPorID(int id)
        {
            try
            {
                return datos.BuscarPorID(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el estado de cuenta por ID", ex);
            }
        }
    }
}
