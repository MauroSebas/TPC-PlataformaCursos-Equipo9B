using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public class RolNegocio
    {
        private readonly RolDatos rolDatos = new RolDatos();

        public List<Rol> ObtenerTodos()
        {
            try
            {
                return rolDatos.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de roles.", ex);
            }
        }

        public Rol ObtenerPorID(int id)
        {
            try
            {
                return rolDatos.BuscarPorID(id);

            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener el Usuario", ex);
            }
        }
    }
}
