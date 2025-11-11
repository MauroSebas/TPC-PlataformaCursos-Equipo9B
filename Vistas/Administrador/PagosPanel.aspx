<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="PagosPanel.aspx.cs" Inherits="Vistas.Aministrador.PagosPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="d-flex">
    
    <main class="flex-grow-1 p-4 p-lg-5">
        
        <div class="container-xl d-flex flex-column gap-4">
            
            <header>
                <h1 class="h1 fw-bolder mb-0">Aprobaciones de Pagos Manuales</h1>
            </header>

            <div class="card p-3 rounded-3">
                <div class="row g-3 align-items-center">
                    <div class="col-md-6">
                        <div class="input-group">
                            <span class="input-group-text bg-body-tertiary border-0">
                                <span class="material-symbols-outlined fs-5">search</span>
                            </span>
                            <input type="text" class="form-control bg-body-tertiary border-0" placeholder="Buscar por email de alumno o nombre del curso...">
                        </div>
                    </div>
                    
                    <div class="col-md-6 d-flex justify-content-md-end gap-3">
                        <button class="btn btn-light border d-flex align-items-center gap-2 small">
                            <span>Estado: Todos</span>
                            <span class="material-symbols-outlined fs-6 text-body-secondary">expand_more</span>
                        </button>
                        <button class="btn btn-light border d-flex align-items-center gap-2 small">
                            <span>Fecha: Recientes</span>
                            <span class="material-symbols-outlined fs-6 text-body-secondary">expand_more</span>
                        </button>
                    </div>
                </div>
            </div>

            <div class="card rounded-3 border-0">
                <div class="table-responsive">
                    <table class="table table-hover align-middle mb-0">
                        <thead class="table-light text-body-secondary text-uppercase small">
                            <tr>
                                <th scope="col" class="px-4 py-3" style="width: 1rem;">
                                    <input class="form-check-input" type="checkbox"/>
                                </th>
                                <th scope="col" class="px-4 py-3">Email Alumno</th>
                                <th scope="col" class="px-4 py-3">Nombre del Curso</th>
                                <th scope="col" class="px-4 py-3">Monto</th>
                                <th scope="col" class="px-4 py-3">Método de Pago</th>
                                <th scope="col" class="px-4 py-3">Fecha</th>
                                <th scope="col" class="px-4 py-3">Estado</th>
                                <th scope="col" class="px-4 py-3">Acción</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="px-4 py-3"><input class="form-check-input" type="checkbox"/></td>
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">student.one@example.com</td>
                                <td class="px-4 py-3 text-nowrap">Advanced UX Design Principles</td>
                                <td class="px-4 py-3 text-nowrap">$199.00</td>
                                <td class="px-4 py-3 text-nowrap">Transferencia Bancaria</td>
                                <td class="px-4 py-3 text-nowrap">26 Oct, 2023</td>
                                <td class="px-4 py-3 text-nowrap">
                                    <span class="badge rounded-pill bg-warning-subtle text-warning-emphasis fw-medium">Pendiente</span>
                                </td>
                                <td class="px-4 py-3 text-nowrap small">
                                    <button class="btn btn-success-subtle btn-sm fw-bold">APROBAR</button>
                                    <button class="btn btn-danger-subtle btn-sm fw-bold">RECHAZAR</button>
                                </td>
                            </tr>
                            <tr>
                                <td class="px-4 py-3"><input class="form-check-input" type="checkbox"/></td>
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">student.two@example.com</td>
                                <td class="px-4 py-3 text-nowrap">Introduction to Python</td>
                                <td class="px-4 py-3 text-nowrap">$99.00</td>
                                <td class="px-4 py-3 text-nowrap">GPay Manual</td>
                                <td class="px-4 py-3 text-nowrap">25 Oct, 2023</td>
                                <td class="px-4 py-3 text-nowrap">
                                    <span class="badge rounded-pill bg-warning-subtle text-warning-emphasis fw-medium">Pendiente</span>
                                </td>
                                <td class="px-4 py-3 text-nowrap small">
                                    <button class="btn btn-success-subtle btn-sm fw-bold">APROBAR</button>
                                    <button class="btn btn-danger-subtle btn-sm fw-bold">RECHAZAR</button>
                                </td>
                            </tr>
                            <tr>
                                <td class="px-4 py-3"><input class="form-check-input" type="checkbox"/></td>
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">student.three@example.com</td>
                                <td class="px-4 py-3 text-nowrap">Digital Marketing Masterclass</td>
                                <td class="px-4 py-3 text-nowrap">$249.00</td>
                                <td class="px-4 py-3 text-nowrap">Transferencia Bancaria</td>
                                <td class="px-4 py-3 text-nowrap">25 Oct, 2023</td>
                                <td class="px-4 py-3 text-nowrap">
                                    <span class="badge rounded-pill bg-success-subtle text-success-emphasis fw-medium">Aprobado</span>
                                </td>
                                <td class="px-4 py-3 text-nowrap text-body-secondary small">Procesado</td>
                            </tr>
                            <tr>
                                <td class="px-4 py-3"><input class="form-check-input" type="checkbox"/></td>
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">student.four@example.com</td>
                                <td class="px-4 py-3 text-nowrap">Data Science Bootcamp</td>
                                <td class="px-4 py-3 text-nowrap">$499.00</td>
                                <td class="px-4 py-3 text-nowrap">Transferencia Bancaria</td>
                                <td class="px-4 py-3 text-nowrap">24 Oct, 2023</td>
                                <td class="px-4 py-3 text-nowrap">
                                    <span class="badge rounded-pill bg-warning-subtle text-warning-emphasis fw-medium">Pendiente</span>
                                </td>
                                <td class="px-4 py-3 text-nowrap small">
                                    <button class="btn btn-success-subtle btn-sm fw-bold">APROBAR</button>
                                    <button class="btn btn-danger-subtle btn-sm fw-bold">RECHAZAR</button>
                                </td>
                            </tr>
                            <tr>
                                <td class="px-4 py-3"><input class="form-check-input" type="checkbox"/></td>
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">student.five@example.com</td>
                                <td class="px-4 py-3 text-nowrap">React for Beginners</td>
                                <td class="px-4 py-3 text-nowrap">$149.00</td>
                                <td class="px-4 py-3 text-nowrap">GPay Manual</td>
                                <td class="px-4 py-3 text-nowrap">23 Oct, 2023</td>
                                <td class="px-4 py-3 text-nowrap">
                                    <span class="badge rounded-pill bg-danger-subtle text-danger-emphasis fw-medium">Rechazado</span>
                                </td>
                                <td class="px-4 py-3 text-nowrap text-body-secondary small">Procesado</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                
                <nav aria-label="Navegación de la tabla" class="d-flex flex-wrap justify-content-between align-items-center gap-3 p-4 border-top">
                    <span class="small text-body-secondary">
                        Mostrando <span class="fw-semibold text-body-emphasis">1</span> a <span class="fw-semibold text-body-emphasis">5</span> de <span class="fw-semibold text-body-emphasis">20</span> resultados
                    </span>
                    <div class="btn-group" role="group">
                        <button type="button" class="btn btn-light border" disabled>
                            <span class="material-symbols-outlined fs-6">chevron_left</span>
                        </button>
                        <button type="button" class="btn btn-light border">
                            <span class="material-symbols-outlined fs-6">chevron_right</span>
                        </button>
                    </div>
                </nav>
            </div>
            
        </div>
    </main>
</div>

</asp:Content>
