using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Libreria para manejo de archivos
using System.IO;

namespace Vistas
{
    public partial class ProcesoPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                CursoNegocio cursoNegocio = new CursoNegocio();
                Curso seleccionado = new Curso();

                string idCursoStr = Request.QueryString["idCurso"];

                //Validacion de IdCurso
                if (!int.TryParse(idCursoStr, out int idCurso))
                {
                    // Si el ID es inválido o no existe, volvemos al Home
                    Response.Redirect("~/Home.aspx");
                }


                //Validacion de Usuario logueado
                if (Session["Usuario"] == null)
                {
                    //Redirecciona al loguin guardando la direccion donde se encuentra utilizando el parametro en la url (?returnUrl)una querystring 
                    Response.Redirect("~/Auth/Loguin.aspx?returnUrl=" + Request.Url.PathAndQuery);
                    return;
                }



                //Carga de datos del Curso
                seleccionado = cursoNegocio.BuscarCurso(idCurso);

                if (seleccionado != null)
                {

                    lblTituloCurso.Text = seleccionado.Titulo;
                    lblPrecioCurso.Text = seleccionado.PrecioFormateado;
                    //lblSubtotal.Text = seleccionado.PrecioFormateado;
                    lblTotal.Text = seleccionado.PrecioFormateado;

                    // 2. LLENAR EL MODAL (La Pre-Carga)
                    // Como el modal es parte de la página, podemos escribir en él directamente.

                    lblNombreCursoModal.Text = seleccionado.Titulo; // Nombre en el modal
                    lblMontoModal.Text = seleccionado.PrecioFormateado; // Precio en el modal

                }
                else
                {
                    Response.Redirect("~/Home.aspx");
                }


            }


        }

        protected void btnEnviarComprobante_Click(object sender, EventArgs e)
        {

            try
            {
                //1. Recuperar el curso desde la url
                string idCursoStr = Request.QueryString["idCurso"];
                int idCurso;

                if (!int.TryParse(idCursoStr, out idCurso))
                {
                    throw new Exception("ID de curso inválido.");
                }


                CursoNegocio cursoNegocio = new CursoNegocio();
                Curso cursoAComprar = new Curso();

                cursoAComprar = cursoNegocio.BuscarCurso(idCurso);

                // 2. Validaciones de archivo
                if (!fuComprobante.HasFile)//Si el FileUpload esta vacio
                {
                    lblMensaje.Text = "Debes seleccionar el comprobante.";
                    lblMensaje.CssClass = "text-danger mt-2 d-block";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Reabrir", "abrirModalPago();", true);
                    return;
                }

                // 3. Guardar archivo
                string carpeta = Server.MapPath("~/Assets/Comprobantes/");// Busca donde guardar el archivo.
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);//Si no existe la carpeta la crea

                string extension = Path.GetExtension(fuComprobante.FileName);//Obtengo solo la extension del archivo

                string nombreArchivo = $"Pago_{DateTime.Now.Ticks}_{cursoAComprar.Id}{extension}";//Genero un nombre unico para el archivo

                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);//Combino para obtener la ruta (ruta relativa + nombre archivo)

                fuComprobante.SaveAs(rutaCompleta);//Guardo el comprobante.

                //4. Logica de Inscripcion y Pago
                Usuario usuarioLogueado = (Usuario)Session["Usuario"];
                InscripcionNegocio inscNegocio = new InscripcionNegocio();

                // Crear Inscripción
                int idInscripcion = inscNegocio.CrearInscripcion(usuarioLogueado.UsuarioID, cursoAComprar.Id);

                //Tengo que validar la creacion de la inscripcion

                // Crear Pago
                Pago nuevoPago = new Pago();

                nuevoPago.Monto = cursoAComprar.Precio; //El precio lo obtengo de la base de datois
                nuevoPago.UrlComprobante = "~/Assets/Comprobantes/" + nombreArchivo;

                nuevoPago.Inscripcion = new Inscripcion();
                nuevoPago.Inscripcion.Id = idInscripcion;// Asigno el id de la inscripcion en Pago

                PagoNegocio pagoNegocio = new PagoNegocio();
                pagoNegocio.RegistrarPagoAlumno(nuevoPago);

                //5. Abro el modal de comprobante recibido
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Exito", "abrirModalExito();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "abrirModalPago();", true);
            }
        }


        protected void btnEntendido_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Alumno/MisCursos.aspx");
        }
    }


}
