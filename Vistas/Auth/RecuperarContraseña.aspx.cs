using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Auth
{
    public partial class RecuperarContraseña : System.Web.UI.Page
    {
        // Declara tus Paneles y Controles
        protected System.Web.UI.WebControls.Panel pnlEmailSolicitud;
        protected System.Web.UI.WebControls.Panel pnlEnvioConfirmado;
        protected System.Web.UI.WebControls.Panel pnlError;
        protected System.Web.UI.WebControls.Literal litErrorMessage;
        protected System.Web.UI.WebControls.TextBox txtEmailRecuperacion;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}