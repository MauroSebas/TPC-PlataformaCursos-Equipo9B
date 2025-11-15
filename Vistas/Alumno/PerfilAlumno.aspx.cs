using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Alumno
{
    public partial class PerfilAlumno1 : System.Web.UI.Page
    {

        private Usuario UsuarioLogueado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Auth/Loguin.aspx", false);
                return;
            }
            UsuarioLogueado = (Usuario)Session["Usuario"];
            if (!IsPostBack)
            {
                CargarDatosDelUsuario();
            }
        }

        private void CargarDatosDelUsuario()
        {
            try
            {
                // Poblamos los campos principales
                txtNombre.Text = UsuarioLogueado.Perfil.Nombre;
                txtApellido.Text = UsuarioLogueado.Perfil.Apellido;
                txtLocalidad.Text = UsuarioLogueado.Perfil.Localidad;
                txtEmail.Text = UsuarioLogueado.Email;

                // Poblamos los datos "decorativos"
                litNombreUsuario.Text = string.IsNullOrWhiteSpace(UsuarioLogueado.Perfil.NombreCompleto)
                                        ? UsuarioLogueado.Email.Split('@')[0]
                                        : UsuarioLogueado.Perfil.NombreCompleto;

                if (string.IsNullOrEmpty(UsuarioLogueado.Perfil.UrlFotoPerfil))
                {
                    // (Asegurate que esta imagen default exista)
                    imgAvatar.ImageUrl = ResolveUrl("~/Assets/img/avatar_default.png");
                }
                else
                {
                    imgAvatar.ImageUrl = ResolveUrl(UsuarioLogueado.Perfil.UrlFotoPerfil);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar tus datos: {ex.Message}", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validamos solo el GRUPO 1
            Page.Validate("DatosPersonales");
            if (!Page.IsValid) return;

            try
            {
                PerfilNegocio negocio = new PerfilNegocio();
                Perfil perfilActualizado = UsuarioLogueado.Perfil;

                perfilActualizado.Nombre = txtNombre.Text.Trim();
                perfilActualizado.Apellido = txtApellido.Text.Trim();
                perfilActualizado.Localidad = txtLocalidad.Text.Trim();

                // (Lógica del Avatar iría acá)

                negocio.ActualizarPerfil(perfilActualizado);

                UsuarioLogueado.Perfil = perfilActualizado;
                Session["Usuario"] = UsuarioLogueado;

                CargarDatosDelUsuario(); // Recargamos para ver el nombre nuevo
                MostrarMensaje("¡Tus datos personales se actualizaron con éxito!");
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar tus datos: {ex.Message}", true);
            }
        }

        // --- Eventos del Panel de Contraseña ---
        protected void btnMostrarPanelPassword_Click(object sender, EventArgs e)
        {
            pnlCambiarPassword.Visible = true;
            pnlCambiarEmail.Visible = false;
        }

        protected void btnConfirmarPassword_Click(object sender, EventArgs e)
        {
            // Validamos solo el GRUPO 3
            Page.Validate("CambiarPassword");
            if (!Page.IsValid) return;

            try
            {
                // (El CompareValidator ya chequeó que las nuevas coincidan)
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.ActualizarPassword(
                    UsuarioLogueado.UsuarioID,
                    txtPassActual.Text,
                    txtPassNueva.Text
                );

                pnlCambiarPassword.Visible = false;
                MostrarMensaje("¡Tu contraseña se cambió con éxito!");
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cambiar la contraseña: {ex.Message}", true);
            }
        }

        protected void btnCancelarPassword_Click(object sender, EventArgs e)
        {
            pnlCambiarPassword.Visible = false;
        }

        // --- Eventos del Panel de Email ---
        protected void btnMostrarPanelEmail_Click(object sender, EventArgs e)
        {
            pnlCambiarEmail.Visible = true;
            pnlCambiarPassword.Visible = false;
            txtNuevoEmail.Text = "";
            txtPassConfirmarEmail.Text = "";
        }

        protected void btnConfirmarEmail_Click(object sender, EventArgs e)
        {
            // Validamos solo el GRUPO 2
            Page.Validate("CambiarEmail");
            if (!Page.IsValid) return;

            string nuevoEmail = txtNuevoEmail.Text.Trim();
            string passwordActual = txtPassConfirmarEmail.Text;

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                // 1. RE-AUTENTICACIÓN
                var usuarioValidado = negocio.ValidarLogin(
                    UsuarioLogueado.Email,
                    passwordActual
                );

                if (usuarioValidado == null)
                {
                    MostrarMensaje("Tu contraseña actual es incorrecta.", true);
                    return;
                }

                // 2. CAMBIO DE EMAIL
                negocio.CambiarEmail(UsuarioLogueado.UsuarioID, nuevoEmail);

                // 3. ÉXITO
                UsuarioLogueado.Email = nuevoEmail;
                Session["Usuario"] = UsuarioLogueado;

                pnlCambiarEmail.Visible = false;
                CargarDatosDelUsuario(); // Recargamos para ver el email nuevo
                MostrarMensaje("¡Email actualizado con éxito!");
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cambiar el email: {ex.Message}", true);
            }
        }

        protected void btnCancelarEmail_Click(object sender, EventArgs e)
        {
            pnlCambiarEmail.Visible = false;
        }

        // --- HELPER DE MENSAJES ---
        private void MostrarMensaje(string mensaje, bool esError = false)
        {
            // (Acá podés mejorarlo para que use un Panel de Bootstrap)
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", script, true);
        }

    }
}
