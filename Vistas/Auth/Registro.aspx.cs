using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio; // <-- Agregá esto para usar la clase Usuario
using Negocio; // <-- Agregá esto para usar UsuarioNegocio

namespace Vistas
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            // 1. Verificar si las validaciones del lado del cliente pasaron
            if (!Page.IsValid)
            {
                // Si no pasó, los mensajes de error de los validadores ya se mostraron.
                // No hacemos nada más aquí.
                return;
            }

            // Si las validaciones básicas pasaron, intentamos registrar
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario nuevoUsuario = new Usuario();

                // 2. Tomar los datos del formulario
                nuevoUsuario.Email = txtEmailRegistro.Text.Trim(); // .Trim() quita espacios extras
                // IMPORTANTE: Pasamos la contraseña en texto plano.
                // La capa de Negocio se encargará de hashearla ANTES de guardarla.
                nuevoUsuario.PasswordHash = txtPasswordRegistro.Text;
                // Nota: No necesitamos NombreCompleto aquí si no lo guardamos en la tabla Usuarios.

                // 3. Llamar a la lógica de negocio para registrar
                bool registroExitoso = negocio.RegistrarUsuario(nuevoUsuario);

                // 4. Mostrar resultado y/o redirigir
                if (registroExitoso)
                {
                    // OPCIONAL: Mostrar un mensaje de éxito antes de redirigir
                    // litMensaje.Text = "<div class='alert alert-success'>¡Registro exitoso! Serás redirigido al login.</div>";
                    // Podrías agregar un pequeño delay aquí si quieres que se vea el mensaje

                    // Redirigir a la página de login (asegúrate que la ruta sea correcta)
                    Response.Redirect("~/Auth/Loguin.aspx", false); // false evita que la página actual siga procesando
                    Context.ApplicationInstance.CompleteRequest(); // Necesario después de Redirect con false
                }
                else
                {
                    // Mostrar mensaje de error (probablemente email duplicado)
                    litMensaje.Text = "<div class='alert alert-danger'>Error al registrar: El email ingresado ya existe.</div>";
                }
            }
            catch (Exception ex)
            {
                // Manejar errores inesperados (ej: fallo de conexión a DB)
                // Es buena práctica loggear el error ex en un archivo o sistema de logs
                litMensaje.Text = "<div class='alert alert-danger'>Ocurrió un error inesperado. Por favor, intenta de nuevo más tarde.</div>";
                // Considera Session["error"] = ex.ToString(); y redirigir a una página de error genérica.
            }
        }
        // --- FIN MÉTODO ---
    }
}