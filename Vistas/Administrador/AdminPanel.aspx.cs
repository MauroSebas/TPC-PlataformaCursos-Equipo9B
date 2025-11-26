using Dominio.Comercial;
using Negocio.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Aministrador
{
    public partial class AdminPanel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDashboard();
            }
        }

        private void CargarDashboard()
        {
            try
            {
                DashboardNegocio negocio = new DashboardNegocio();
               
                var datosDashboard = negocio.ObtenerDashboardCompleto();

               
                litTotalCursos.Text = datosDashboard.Metricas.TotalCursos.ToString();
                litTotalAlumnos.Text = datosDashboard.Metricas.TotalAlumnos.ToString();
                litPendientes.Text = datosDashboard.Metricas.PagosPendientes.ToString();
                litBadgePendientes.Text = datosDashboard.Metricas.PagosPendientes.ToString();
                litIngresos.Text = datosDashboard.Metricas.IngresosTotales.ToString("C0", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"));

               
                repCursosPopulares.DataSource = datosDashboard.CursosPopulares;
                repCursosPopulares.DataBind();
                pnlSinCursos.Visible = datosDashboard.CursosPopulares.Count == 0;

               
                repUsuariosRecientes.DataSource = datosDashboard.UsuariosRecientes;
                repUsuariosRecientes.DataBind();
                pnlSinUsuarios.Visible = datosDashboard.UsuariosRecientes.Count == 0;
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}

