using Dominio; // <-- ¡Necesitamos esto para "ver" la clase Usuario!
using System;
using System.Web.UI;

namespace Vistas
{
    public partial class AlumnoMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // --- ETAPA 2.A: EL "PATOVICA" (El Guardia de Seguridad) ---
            if (Session["Usuario"] == null)
            {
                // ¡"Mochila" vacía! ¡AFUERA!
                Response.Redirect("~/Auth/Loguin.aspx?error=DebeLoguearse", false);
                return; // ¡Importante! Frenamos la carga de la página.
            }

            // --- ETAPA 2.B: POBLAR EL PANEL (Darle "Vida") ---
            // Si llegamos acá, es porque el usuario SÍ está logueado.
            if (!IsPostBack) // Solo lo hacemos la primera vez que carga
            {
                // "Desarmamos la mochila" (Casteamos el objeto de la Session)
                Usuario userLogueado = (Usuario)Session["Usuario"];

                // --- ¡¡ARREGLO 1 (TU BUG DE LA "MANITO")!! ---
                // Le damos una imagen al logo de la esquina para que no sea un link vacío.
                // (Podrías cargar esto desde la BBDD de Configuración más adelante)
                


                // --- ¡¡ARREGLO 2 (TU LÓGICA DE NOMBRE vs. EMAIL)!! ---
                if (userLogueado.Perfil != null)
                {
                    string nombreParaMostrar;
                    string inicialParaAvatar;

                    // ¡TU LÓGICA! ¿Tiene Nombre Y Apellido?
                    // (Usamos IsNullOrWhiteSpace que es más seguro que IsNullOrEmpty)
                    if (!string.IsNullOrWhiteSpace(userLogueado.Perfil.Nombre) &&
                        !string.IsNullOrWhiteSpace(userLogueado.Perfil.Apellido))
                    {
                        // SÍ TIENE: Usamos "Pepito Rogelio" (NombreCompleto)
                        nombreParaMostrar = userLogueado.Perfil.NombreCompleto;
                        // Y usamos la inicial del Nombre ("P")
                        inicialParaAvatar = userLogueado.Perfil.Nombre[0].ToString();
                    }
                    else
                    {
                        // NO TIENE: Usamos el email "wxyz.25"
                        nombreParaMostrar = userLogueado.Email.Split('@')[0];
                        // Y usamos la inicial del Email ("w")
                        inicialParaAvatar = userLogueado.Email[0].ToString();
                    }

                    // Seteamos el Texto del Dropdown
                    litNombreUsuario.Text = nombreParaMostrar;

                    // Seteamos el Avatar (la Foto de Perfil)
                    if (string.IsNullOrEmpty(userLogueado.Perfil.UrlFotoPerfil))
                    {
                        // No tiene foto, usamos la inicial que calculamos
                        imgAvatar.ImageUrl = $"https://placehold.co/32x32/0d6efd/FFFFFF?text={inicialParaAvatar.ToUpper()}";
                    }
                    else
                    {
                        // Si SÍ tiene foto, la mostramos
                        imgAvatar.ImageUrl = userLogueado.Perfil.UrlFotoPerfil;
                    }
                }
                else
                {
                    // Fallback MUY RARO (si el Perfil es nulo por algún motivo)
                    litNombreUsuario.Text = userLogueado.Email.Split('@')[0];
                    imgAvatar.ImageUrl = $"https://placehold.co/32x32/888/FFFFFF?text={userLogueado.Email[0].ToString().ToUpper()}";
                }
            }
        }

        /// <summary>
        // --- ETAPA 2.C: EL LOGOUT ---
        // Este método es llamado por AMBOS botones (Header y Sidebar)
        /// </summary>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // 1. "Tiramos la mochila" (Matamos la Session)
            Session.Abandon();

            // 2. Lo mandamos a la calle (Home)
            Response.Redirect("~/Home.aspx");
        }
    }
}