using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

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

            // Ocultamos el panel de mensajes GLOBAL en cada carga
            pnlMensajeGlobal.Visible = false;

            if (!IsPostBack)
            {
                CargarDatosDelUsuario();
            }
        }

        private void CargarDatosDelUsuario()
        {
            try
            {
                txtNombre.Text = UsuarioLogueado.Perfil.Nombre;
                txtApellido.Text = UsuarioLogueado.Perfil.Apellido;
                txtLocalidad.Text = UsuarioLogueado.Perfil.Localidad;
                txtEmail.Text = UsuarioLogueado.Email;

                litNombreUsuario.Text = string.IsNullOrWhiteSpace(UsuarioLogueado.Perfil.NombreCompleto)
                                        ? UsuarioLogueado.Email.Split('@')[0]
                                        : UsuarioLogueado.Perfil.NombreCompleto;

                if (string.IsNullOrEmpty(UsuarioLogueado.Perfil.UrlFotoPerfil))
                {
                    imgAvatar.ImageUrl = ResolveUrl("~/Assets/img/avatar_default.png");
                }
                else
                {
                    imgAvatar.ImageUrl = ResolveUrl(UsuarioLogueado.Perfil.UrlFotoPerfil);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cargar tus datos: {ex.Message}", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate("DatosPersonales");
            // Si la validación (largo, etc.) falla, los validadores se mostrarán solos
            // gracias al UpdatePanel. No necesitamos hacer nada más.
            if (!Page.IsValid) return;

            try
            {
                PerfilNegocio negocio = new PerfilNegocio();
                Perfil perfilActualizado = UsuarioLogueado.Perfil;

                perfilActualizado.Nombre = txtNombre.Text.Trim();
                perfilActualizado.Apellido = txtApellido.Text.Trim();
                perfilActualizado.Localidad = txtLocalidad.Text.Trim();

                if (fileUploadAvatar.HasFile)
                {
                    // ... (lógica de la foto) ...
                    string extension = Path.GetExtension(fileUploadAvatar.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        // ¡¡CAMBIO!! Usamos el panel de mensajes global
                        MostrarMensajeGlobal("Solo podés subir fotos .jpg o .png", true);
                        return;
                    }
                    string nombreArchivo = $"{UsuarioLogueado.UsuarioID}{extension}";
                    string rutaVirtual = $"~/Assets/Avatares/{nombreArchivo}";
                    string rutaFisica = Server.MapPath(rutaVirtual);
                    fileUploadAvatar.SaveAs(rutaFisica);
                    perfilActualizado.UrlFotoPerfil = rutaVirtual;
                }

                negocio.ActualizarPerfil(perfilActualizado);

                UsuarioLogueado.Perfil = perfilActualizado;
                Session["Usuario"] = UsuarioLogueado;

                CargarDatosDelUsuario();
                MostrarMensajeGlobal("¡Tus datos personales se actualizaron con éxito!");
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al guardar tus datos: {ex.Message}", true);
            }
        }

        // --- Eventos del Panel de Contraseña ---

        // ¡¡ARREGLO DE BUG 2!! (Paneles simultáneos)
        protected void btnMostrarPanelPassword_Click(object sender, EventArgs e)
        {
            // Mostramos este panel
            pnlCambiarPassword.Visible = true;
            pnlErrorPassword.Visible = false; // Ocultamos errores viejos

            // Ocultamos el OTRO panel
            pnlCambiarEmail.Visible = false;

            // Forzamos la actualización de AMBOS corralitos
            updPassword.Update();
            updEmail.Update();
        }

        protected void btnConfirmarPassword_Click(object sender, EventArgs e)
        {
            Page.Validate("CambiarPassword");
            // Si la validación (largo, mayúscula, etc.) falla,
            // el UpdatePanel se refresca solo y muestra los errores.
            // ¡¡PERO TENEMOS QUE ARREGLAR EL BUG DE QUE SE BORRA LA PASS ACTUAL!!
            if (!Page.IsValid)
            {
                // ¡¡ARREGLO DE BUG 2!! (No limpiar pass)
                txtPassActual.Attributes.Add("value", txtPassActual.Text);
                return;
            }

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.ActualizarPassword(
                    UsuarioLogueado.UsuarioID,
                    txtPassActual.Text,
                    txtPassNueva.Text
                );

                pnlCambiarPassword.Visible = false;
                updPassword.Update(); // Forzamos el cierre del panel
                MostrarMensajeGlobal("¡Tu contraseña se cambió con éxito!");
            }
            catch (Exception ex) // Esto agarra el "La contraseña actual es incorrecta"
            {
                // ¡¡ARREGLO DE BUG 1!! (Error local)
                MostrarErrorEnPanel(pnlErrorPassword, litErrorPassword, ex.Message);

                // ¡¡ARREGLO DE BUG 2!! (No limpiar pass)
                txtPassActual.Attributes.Add("value", txtPassActual.Text);
            }
        }

        protected void btnCancelarPassword_Click(object sender, EventArgs e)
        {
            pnlCambiarPassword.Visible = false;
        }

        // --- Eventos del Panel de Email ---

        // ¡¡ARREGLO DE BUG 2!! (Paneles simultáneos)
        protected void btnMostrarPanelEmail_Click(object sender, EventArgs e)
        {
            // Mostramos este panel
            pnlCambiarEmail.Visible = true;
            pnlErrorEmail.Visible = false; // Ocultamos errores viejos
            txtNuevoEmail.Text = "";
            txtPassConfirmarEmail.Text = "";

            // Ocultamos el OTRO panel
            pnlCambiarPassword.Visible = false;

            // Forzamos la actualización de AMBOS corralitos
            updEmail.Update();
            updPassword.Update();
        }

        protected void btnConfirmarEmail_Click(object sender, EventArgs e)
        {
            Page.Validate("CambiarEmail");
            if (!Page.IsValid)
            {
                // ¡¡ARREGLO DE BUG 2!! (No limpiar pass)
                txtPassConfirmarEmail.Attributes.Add("value", txtPassConfirmarEmail.Text);
                return;
            }

            string nuevoEmail = txtNuevoEmail.Text.Trim();
            string passwordActual = txtPassConfirmarEmail.Text;

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                var usuarioValidado = negocio.ValidarLogin(
                    UsuarioLogueado.Email,
                    passwordActual
                );

                if (usuarioValidado == null)
                {
                    // ¡¡ARREGLO DE BUG 1!! (Error local)
                    MostrarErrorEnPanel(pnlErrorEmail, litErrorEmail, "Tu contraseña actual es incorrecta.");

                    // ¡¡ARREGLO DE BUG 2!! (No limpiar pass)
                    txtPassConfirmarEmail.Attributes.Add("value", txtPassConfirmarEmail.Text);
                    return;
                }

                negocio.CambiarEmail(UsuarioLogueado.UsuarioID, nuevoEmail);

                UsuarioLogueado.Email = nuevoEmail;
                Session["Usuario"] = UsuarioLogueado;

                pnlCambiarEmail.Visible = false;
                CargarDatosDelUsuario();
                MostrarMensajeGlobal("¡Email actualizado con éxito!");
            }
            catch (Exception ex) // Esto agarra "El email ya existe"
            {
                // ¡¡ARREGLO DE BUG 1!! (Error local)
                MostrarErrorEnPanel(pnlErrorEmail, litErrorEmail, ex.Message);

                // ¡¡ARREGLO DE BUG 2!! (No limpiar pass)
                txtPassConfirmarEmail.Attributes.Add("value", txtPassConfirmarEmail.Text);
            }
        }

        protected void btnCancelarEmail_Click(object sender, EventArgs e)
        {
            pnlCambiarEmail.Visible = false;
        }

        // --- HELPER DE MENSAJES ---

        // ¡¡NUEVO!! Helper para errores DENTRO de los paneles
        private void MostrarErrorEnPanel(Panel pnlError, Literal litError, string mensaje)
        {
            pnlError.Visible = true;
            litError.Text = mensaje;
            // Ocultamos el panel de éxito global, por las dudas
            pnlMensajeGlobal.Visible = false;
            updMensajeGlobal.Update();
        }

        // Helper para mensajes GLOBALES (Arriba de todo)
        private void MostrarMensajeGlobal(string mensaje, bool esError = false)
        {
            pnlMensajeGlobal.Visible = true;
            litMensajeGlobal.Text = mensaje;
            pnlMensajeGlobal.CssClass = esError ? "alert alert-danger" : "alert alert-success";

            // Forzamos la actualización del panel global
            updMensajeGlobal.Update();
        }
    }
}



