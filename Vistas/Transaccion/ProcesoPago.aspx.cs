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

                CursoNegocio negocio = new CursoNegocio();
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
                seleccionado = negocio.BuscarCurso(idCurso);

                if ( seleccionado != null)
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



        }



    }


    }
}