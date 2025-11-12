<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Vistas.Aministrador.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main class="flex-grow-1 p-4 p-lg-5">

        <div class="container-xl">

            <header class="d-flex flex-wrap justify-content-between align-items-center gap-4 mb-5">
                <div class="flex-column">
                    <h1 class="h2 fw-bold text-body-emphasis mb-0">Resumen del Panel</h1>
                    <p class="text-body-secondary fs-6 mt-1">Un vistazo general a las métricas clave de la plataforma.</p>
                </div>

                <div class="dropdown">
                    <button class="btn bg-body border d-flex align-items-center gap-2" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                        <span class="material-symbols-outlined fs-6">calendar_today</span>
                        <span class="d-none d-sm-inline">Últimos 30 días</span>
                        <span class="material-symbols-outlined fs-6">expand_more</span>
                    </button>
                    <ul class="dropdown-menu dropdown-menu-end">
                        <li><a class="dropdown-item" href="#">Últimos 7 días</a></li>
                        <li><a class="dropdown-item" href="#">Últimos 30 días</a></li>
                        <li><a class="dropdown-item" href="#">Este mes</a></li>
                    </ul>
                </div>
            </header>

            <div class="row g-4">

                <div class="col-12 col-md-6 col-lg-3">
                    <div class="card p-3 h-100 shadow-sm border-0 rounded-3">
                        <div class="card-body d-flex flex-column gap-2">
                            <div class="d-flex align-items-center justify-content-between text-body-secondary">
                                <h3 class="h6 mb-0">Ingresos Totales</h3>
                                <span class="material-symbols-outlined">payments</span>
                            </div>
                            <p class="h2 text-body-emphasis fw-bold my-1">$12,450</p>
                            <p class="text-success small fw-medium mb-0">+5.2% vs mes anterior</p>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-md-6 col-lg-3">
                    <div class="card p-3 h-100 shadow-sm border-0 rounded-3">
                        <div class="card-body d-flex flex-column gap-2">
                            <div class="d-flex align-items-center justify-content-between text-body-secondary">
                                <h3 class="h6 mb-0">Nuevas Inscripciones</h3>
                                <span class="material-symbols-outlined">person_add</span>
                            </div>
                            <p class="h2 text-body-emphasis fw-bold my-1">152</p>
                            <p class="text-success small fw-medium mb-0">+12% en los últimos 30 días</p>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-md-6 col-lg-3">
                    <div class="card p-3 h-100 shadow-sm border-0 rounded-3">
                        <div class="card-body d-flex flex-column gap-2">
                            <div class="d-flex align-items-center justify-content-between text-body-secondary">
                                <h3 class="h6 mb-0">Cursos Activos</h3>
                                <span class="material-symbols-outlined">play_circle</span>
                            </div>
                            <p class="h2 text-body-emphasis fw-bold my-1">34</p>
                            <p class="text-success small fw-medium mb-0">+2 cursos nuevos este mes</p>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-md-6 col-lg-3">
                    <div class="card p-3 h-100 bg-warning-subtle border-warning-subtle rounded-3">
                        <div class="card-body d-flex flex-column gap-2">
                            <div class="d-flex align-items-center justify-content-between text-warning-emphasis">
                                <h3 class="h6 mb-0">Pagos Pendientes</h3>
                                <span class="material-symbols-outlined">pending_actions</span>
                            </div>
                            <p class="h2 text-body-emphasis fw-bold my-1">$1,200</p>
                            <p class="text-warning-emphasis small fw-medium mb-0">3 pagos vencidos</p>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </main>

</asp:Content>
