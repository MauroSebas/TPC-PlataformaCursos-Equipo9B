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
                // 1. Leemos el token que viene en la URL
                string token = Request.QueryString["token"];

                if (string.IsNullOrEmpty(token))
                {
                    MostrarMensajeError("Enlace inválido.", "No se proporcionó un token de activación.");
                    return;
                }

                try
                {
                    // 2. ¡Llamamos a la BLL (Capa de Negocio) que ya creamos!
                    // Esta es la BLL que valida y consume el token.
                    UsuarioTokenNegocio tokenNegocio = new UsuarioTokenNegocio();

                    // 3. Validamos el token (Este método lanza excepción si falla)
                    int usuarioID = tokenNegocio.ValidarToken(token, TipoTokenEnum.ActivacionCuenta);

                    // 4. Si ValidarToken NO tiró excepción, ¡el token era válido!
                    //    Ahora activamos al usuario.
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    usuarioNegocio.CambiarEstadoCuenta(usuarioID, (int)EstadoCuentaEnum.Activo);

                    // 5. Mostramos el mensaje de éxito
                    MostrarMensajeExito("¡Cuenta Activada!", "Tu email ha sido verificado correctamente. Ya podés iniciar sesión.");
                }
                catch (Exception ex)
                {
                    // 6. Si la BLL (ValidarToken) tiró una excepción...
                    //    (ej. "El enlace ha expirado" o "El enlace no es válido")
                    //    ¡La atrapamos y se la mostramos al usuario!
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
            hlLogin.Visible = true; // Mostramos el botón para ir al Login
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
