using Dominio.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dashboard
{
    public class DashboardVista
    {
        public MetricasDashboard Metricas { get; set; }
        public List<CursoPopular> CursosPopulares { get; set; }
        public List<UsuarioReciente> UsuariosRecientes { get; set; }

        public DashboardVista()
        {
            Metricas = new MetricasDashboard();
            CursosPopulares = new List<CursoPopular>();
            UsuariosRecientes = new List<UsuarioReciente>();
        }
    }
}
