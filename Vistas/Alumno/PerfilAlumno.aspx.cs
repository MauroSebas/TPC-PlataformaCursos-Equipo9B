using Dominio;
using Dominio.Enums;
using Negocio;
using Negocio.Seguridad;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
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

            pnlMensajeGlobal.Visible = false;

            if (!IsPostBack)
            {
                CargarDatosDelUsuario();
            }
            if (Session["PerfilMensaje"] != null)
            {
                MostrarMensajeGlobal(Session["PerfilMensaje"].ToString());
                Session["PerfilMensaje"] = null;
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
            if (!Page.IsValid) return;

            try
            {
                PerfilNegocio negocio = new PerfilNegocio();
                Perfil perfilActualizado = UsuarioLogueado.Perfil;
                perfilActualizado.Nombre = txtNombre.Text.Trim();
                perfilActualizado.Apellido = txtApellido.Text.Trim();
                perfilActualizado.Localidad = txtLocalidad.Text.Trim();
                negocio.ActualizarPerfil(perfilActualizado);
                UsuarioLogueado.Perfil = perfilActualizado;
                Session["Usuario"] = UsuarioLogueado;
                Session["PerfilMensaje"] = "¡Tus datos personales se actualizaron con éxito!";
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al guardar tus datos: {ex.Message}", true);
            }
        }

        protected void btnConfirmarAvatar_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUploadModal.HasFile)
                {
                    PerfilNegocio negocio = new PerfilNegocio();
                    Perfil perfilActualizado = UsuarioLogueado.Perfil;
                    string extension = Path.GetExtension(fileUploadModal.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        MostrarMensajeGlobal("Epa, solo podés subir fotos .jpg o .png", true);
                        return;
                    }
                    string nombreArchivo = $"{UsuarioLogueado.UsuarioID}{extension}";
                    string rutaVirtual = $"~/Assets/Avatares/{nombreArchivo}";
                    string rutaFisica = Server.MapPath(rutaVirtual);
                    fileUploadModal.SaveAs(rutaFisica);
                    perfilActualizado.UrlFotoPerfil = rutaVirtual;
                    negocio.ActualizarPerfil(perfilActualizado);
                    UsuarioLogueado.Perfil = perfilActualizado;
                    Session["Usuario"] = UsuarioLogueado;
                }
                else
                {
                    MostrarMensajeGlobal("No seleccionaste ninguna foto.", true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al subir la foto: {ex.Message}", true);
            }
        }

        // --- Eventos del Panel de Contraseña ---
        protected void btnMostrarPanelPassword_Click(object sender, EventArgs e)
        {
            pnlCambiarPassword.Visible = true;
            pnlErrorPassword.Visible = false;
            pnlCambiarEmail.Visible = false;
            pnlVerificarToken.Visible = false; 
            updPassword.Update();
            updEmail.Update();
        }

        protected void btnConfirmarPassword_Click(object sender, EventArgs e)
        {
            Page.Validate("CambiarPassword");
            if (!Page.IsValid)
            {
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
                updPassword.Update();
                MostrarMensajeGlobal("¡Tu contraseña se cambió con éxito!");
            }
            catch (Exception ex)
            {
                MostrarErrorEnPanel(pnlErrorPassword, litErrorPassword, ex.Message);
                txtPassActual.Attributes.Add("value", txtPassActual.Text);
            }
        }

        protected void btnCancelarPassword_Click(object sender, EventArgs e)
        {
            pnlCambiarPassword.Visible = false;
        }

        
        // --- Eventos del Panel de Email ---
        protected void btnMostrarPanelEmail_Click(object sender, EventArgs e)
        {
            // PASO 1: Mostramos el panel de pedir datos
            pnlCambiarEmail.Visible = true;
            pnlErrorEmail.Visible = false;
            txtNuevoEmail.Text = "";
            txtPassConfirmarEmail.Text = "";

            // Ocultamos los otros paneles
            pnlCambiarPassword.Visible = false;
            pnlVerificarToken.Visible = false;

            // Actualizamos los corralitos
            updEmail.Update();
            updPassword.Update();
        }
       
        protected void btnEnviarToken_Click(object sender, EventArgs e)
        {
            Page.Validate("CambiarEmail");
            if (!Page.IsValid)
            {
                txtPassConfirmarEmail.Attributes.Add("value", txtPassConfirmarEmail.Text);
                return;
            }

            string nuevoEmail = txtNuevoEmail.Text.Trim();
            string passwordActual = txtPassConfirmarEmail.Text;

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                if (negocio.ValidarLogin(UsuarioLogueado.Email, passwordActual) == null)
                {
                    MostrarErrorEnPanel(pnlErrorEmail, litErrorEmail, "Tu contraseña actual es incorrecta.");
                    txtPassConfirmarEmail.Attributes.Add("value", txtPassConfirmarEmail.Text);
                    return;
                }

                if (negocio.BuscarPorEmail(nuevoEmail) != null)
                {
                    MostrarErrorEnPanel(pnlErrorEmail, litErrorEmail, "Ese email ya está en uso por otra cuenta.");
                    txtPassConfirmarEmail.Attributes.Add("value", txtPassConfirmarEmail.Text);
                    return;
                }

                UsuarioTokenNegocio tokenNegocio = new UsuarioTokenNegocio();
                string token = tokenNegocio.GenerarToken(
                    UsuarioLogueado.UsuarioID,
                    TipoTokenEnum.CambioEmail
                );

                Session["EmailPendiente"] = nuevoEmail;

                EmailServicio emailServicio = new EmailServicio();
                var reemplazos = new Dictionary<string, string>
                {
                    { "{{NOMBRE_USUARIO}}", UsuarioLogueado.Perfil.Nombre ?? UsuarioLogueado.Email },
                    { "{{TOKEN_VERIFICACION}}", token }
                };

                emailServicio.EnviarTemplateEmail(
                    nuevoEmail,
                    "Verifica tu nuevo email",
                    "VerificarNuevoEmail.html",
                    reemplazos
                );

                pnlCambiarEmail.Visible = false;
                pnlVerificarToken.Visible = true;
                litEmailPendiente.Text = nuevoEmail;
            }
            catch (Exception ex)
            {
                MostrarErrorEnPanel(pnlErrorEmail, litErrorEmail, ex.Message);
                txtPassConfirmarEmail.Attributes.Add("value", txtPassConfirmarEmail.Text);
            }
        }

        protected void btnCancelarEmail_Click(object sender, EventArgs e)
        {
            pnlCambiarEmail.Visible = false;
        }

     
        protected void btnConfirmarToken_Click(object sender, EventArgs e)
        {
            Page.Validate("VerificarToken");
            if (!Page.IsValid) return;

            string tokenIngresado = txtToken.Text.Trim();
            string emailGuardado = Session["EmailPendiente"]?.ToString();

            try
            {
                if (string.IsNullOrEmpty(emailGuardado))
                {
                    MostrarErrorEnPanel(pnlErrorToken, litErrorToken, "Tu sesión expiró. Volvé a empezar.");
                    btnMostrarPanelEmail_Click(null, null);
                    return;
                }

                UsuarioTokenNegocio tokenNegocio = new UsuarioTokenNegocio();
                int usuarioIDValidado = tokenNegocio.ValidarToken(
                    tokenIngresado,
                    TipoTokenEnum.CambioEmail
                );

                if (usuarioIDValidado != UsuarioLogueado.UsuarioID)
                {
                    throw new Exception("El token no corresponde a este usuario.");
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.CambiarEmail(UsuarioLogueado.UsuarioID, emailGuardado);
                
                UsuarioLogueado.Email = emailGuardado;
                Session["Usuario"] = UsuarioLogueado;
                Session["EmailPendiente"] = null; 

                
                Session["PerfilMensaje"] = "¡Tu email se actualizó con éxito!";

               
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                MostrarErrorEnPanel(pnlErrorToken, litErrorToken, ex.Message);
            }
        }

        protected void btnCancelarVerificacion_Click(object sender, EventArgs e)
        {
            pnlVerificarToken.Visible = false;
            Session["EmailPendiente"] = null;
        }


        // --- HELPER DE MENSAJES ---
        private void MostrarErrorEnPanel(Panel pnlError, Literal litError, string mensaje)
        {
            pnlError.Visible = true;
            litError.Text = mensaje;
            pnlMensajeGlobal.Visible = false;
            updMensajeGlobal.Update();
        }

        private void MostrarMensajeGlobal(string mensaje, bool esError = false)
        {
            pnlMensajeGlobal.Visible = true;
            litMensajeGlobal.Text = mensaje;
            pnlMensajeGlobal.CssClass = esError ? "alert alert-danger" : "alert alert-success";
            updMensajeGlobal.Update();
        }

    }
}



