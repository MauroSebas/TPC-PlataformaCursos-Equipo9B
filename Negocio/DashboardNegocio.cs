using Datos;
using Dominio.Comercial;
using Dominio.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Comercial
{
    public class DashboardNegocio
    {
        public DashboardVista ObtenerDashboardCompleto()
        {
            AccesoDatos datos = new AccesoDatos();
            DashboardVista dashboard = new DashboardVista();

            try
            {
                datos.setearConSP("sp_Dashboard_Completo");
                datos.ejecutarLectura();

               
                if (datos.Lector.Read())
                {
                    dashboard.Metricas.TotalCursos = (int)datos.Lector["TotalCursos"];
                    dashboard.Metricas.TotalAlumnos = (int)datos.Lector["TotalAlumnos"];
                    dashboard.Metricas.PagosPendientes = (int)datos.Lector["PagosPendientes"];
                    dashboard.Metricas.IngresosTotales = (decimal)datos.Lector["IngresosTotales"];
                }

               
                if (datos.Lector.NextResult())
                {
                    while (datos.Lector.Read())
                    {
                        dashboard.CursosPopulares.Add(new CursoPopular
                        {
                            Titulo = (string)datos.Lector["Titulo"],
                            Inscripciones = (int)datos.Lector["Inscripciones"]
                        });
                    }
                }

                
                if (datos.Lector.NextResult())
                {
                    while (datos.Lector.Read())
                    {
                        dashboard.UsuariosRecientes.Add(new UsuarioReciente
                        {
                            Nombre = datos.Lector["Nombre"] == DBNull.Value ? "" : (string)datos.Lector["Nombre"],
                            Apellido = datos.Lector["Apellido"] == DBNull.Value ? "" : (string)datos.Lector["Apellido"],
                            Email = datos.Lector["Email"] == DBNull.Value ? "" : (string)datos.Lector["Email"],
                            
                            FechaCreacion = (DateTime)datos.Lector["FechaCreacion"]
                        });
                    }
                }

                return dashboard;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar el dashboard completo", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

