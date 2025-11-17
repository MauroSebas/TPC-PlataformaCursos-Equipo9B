using Dominio.Enums;
using Negocio;
using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Auth
{
    public partial class ActivarCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                string token = Request.QueryString["token"];

                if (string.IsNullOrEmpty(token))
                {
                    MostrarMensajeError("Enlace inválido.", "No se proporcionó un token de activación.");
                    return;
                }

                try
                {
                    
                    UsuarioTokenNegocio tokenNegocio = new UsuarioTokenNegocio();

                    int usuarioID = tokenNegocio.ValidarToken(token, TipoTokenEnum.ActivacionCuenta);

                   
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    usuarioNegocio.CambiarEstadoCuenta(usuarioID, (int)EstadoCuentaEnum.Activo);

                   
                    MostrarMensajeExito("¡Cuenta Activada!", "Tu email ha sido verificado correctamente. Ya podés iniciar sesión.");
                }
                catch (Exception ex)
                {
                    
                    MostrarMensajeError("Error de Activación", ex.Message);
                }
            }
        }

        // --- Métodos Helper para mostrar los mensajes ---

        private void MostrarMensajeExito(string titulo, string mensaje)
        {
            pnlMensaje.Visible = true;
            pnlMensaje.CssClass = "alert alert-success";
            litTitulo.Text = "✅ " + titulo;
            litMensaje.Text = mensaje;
            hlLogin.Visible = true; 
        }

        private void MostrarMensajeError(string titulo, string mensaje)
        {
            pnlMensaje.Visible = true;
            pnlMensaje.CssClass = "alert alert-danger";
            litTitulo.Text = "❌ " + titulo;
            litMensaje.Text = mensaje;
            hlLogin.Visible = false;
        }
    }
}
