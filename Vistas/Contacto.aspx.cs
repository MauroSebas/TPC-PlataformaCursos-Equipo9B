using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class Contacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                string emailSoporte = "soporte@learnify.com";
                string telefonoSoporte = "+34 900 123 456";

                
                linkEmail.Text = emailSoporte;
                linkEmail.NavigateUrl = "mailto:" + emailSoporte;

                
                linkTelefono.Text = telefonoSoporte;
                linkTelefono.NavigateUrl = "tel:" + telefonoSoporte;

                
            }

        }
    }
}