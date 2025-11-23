<%@ Page Title="" Language="C#" MasterPageFile="~/Alumno/Alumno.Master" AutoEventWireup="true" CodeBehind="MisPagos.aspx.cs" Inherits="Vistas.MisPagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="d-flex flex-column gap-4">
        
       

        <div class="card shadow-sm border-0">
            
            <div class="card-body p-0 table-responsive">
                
                <table class="table table-hover mb-0">
                    
                    <thead class="bg-light border-bottom">
                        <tr>
                            <th scope="col" class="text-muted fw-semibold small text-uppercase py-3" style="width: 40%;">Nombre del Curso</th>
                            <th scope="col" class="text-muted fw-semibold small text-uppercase py-3" style="width: 20%;">Fecha de Compra</th>
                            <th scope="col" class="text-muted fw-semibold small text-uppercase py-3" style="width: 20%;">Método de Pago</th>
                            <th scope="col" class="text-muted fw-semibold small text-uppercase py-3 text-end" style="width: 10%;">Monto Total</th>
                            <th scope="col" class="text-muted fw-semibold small text-uppercase py-3 text-end" style="width: 10%;">Acción</th>
                        </tr>
                    </thead>
                    
                    <tbody>
                        
                        <tr class="align-middle">
                            <td class="text-body-emphasis fw-medium small">Introducción a UI/UX</td>
                            <td class="text-muted small">15 de Ago, 2023</td>
                            <td class="text-muted small">Tarjeta (**** 1234)</td>
                            <td class="text-muted small text-end fw-medium">$49.99</td>
                            <td class="text-end">
                                <asp:LinkButton ID="btnFactura1" runat="server" CssClass="btn btn-sm btn-outline-secondary">
                                    <i class="bi bi-download me-1"></i> Factura
                                </asp:LinkButton>
                            </td>
                        </tr>

                        <tr class="align-middle">
                            <td class="text-body-emphasis fw-medium small">Marketing Digital Avanzado</td>
                            <td class="text-muted small">02 de Jul, 2023</td>
                            <td class="text-muted small">PayPal</td>
                            <td class="text-muted small text-end fw-medium">$99.99</td>
                            <td class="text-end">
                                <asp:LinkButton ID="btnFactura2" runat="server" CssClass="btn btn-sm btn-outline-secondary">
                                    <i class="bi bi-download me-1"></i> Factura
                                </asp:LinkButton>
                            </td>
                        </tr>

                        <tr class="align-middle">
                            <td class="text-body-emphasis fw-medium small">Fundamentos de Programación con Python</td>
                            <td class="text-muted small">18 de Jun, 2023</td>
                            <td class="text-muted small">Tarjeta (**** 5678)</td>
                            <td class="text-muted small text-end fw-medium">$75.00</td>
                            <td class="text-end">
                                <asp:LinkButton ID="btnFactura3" runat="server" CssClass="btn btn-sm btn-outline-secondary">
                                    <i class="bi bi-download me-1"></i> Factura
                                </asp:LinkButton>
                            </td>
                        </tr>
                        
                    </tbody>
                </table>
                
                <%--
                <div class="d-flex flex-column align-items-center justify-content-center text-center border-2 border-dashed border-secondary-subtle rounded-lg py-5 px-4 bg-light">
                    <div class="d-flex align-items-center justify-content-center bg-secondary-subtle text-primary rounded-circle mb-3" style="width: 50px; height: 50px;">
                        <i class="bi bi-receipt-long fs-4"></i>
                    </div>
                    <h5 class="text-body-emphasis fw-bold">Aún no has realizado ninguna compra</h5>
                    <p class="text-muted small mb-3">Los cursos que compres aparecerán aquí. ¡Explora nuestro catálogo para empezar a aprender!</p>
                    <asp:HyperLink ID="lnkVerCursos" runat="server" NavigateUrl="~/Home.aspx" CssClass="btn btn-primary fw-bold">
                        Ver Cursos
                        <i class="bi bi-arrow-right ms-2"></i>
                    </asp:HyperLink>
                </div>
                --%>

            </div>
            
        </div>
        
        <div class="d-flex justify-content-center p-3">
            <nav aria-label="Paginación de Pagos">
                <ul class="pagination mb-0">
                    <li class="page-item disabled">
                        <a class="page-link" href="#" aria-label="Anterior">
                            <i class="bi bi-chevron-left"></i>
                        </a>
                    </li>
                    <li class="page-item active"><a class="page-link" href="#">1</a></li>
                    <li class="page-item"><a class="page-link" href="#">2</a></li>
                    <li class="page-item"><a class="page-link" href="#">3</a></li>
                    <li class="page-item disabled"><span class="page-link">...</span></li>
                    <li class="page-item"><a class="page-link" href="#">10</a></li>
                    <li class="page-item">
                        <a class="page-link" href="#" aria-label="Siguiente">
                            <i class="bi bi-chevron-right"></i>
                        </a>
                    </li>
                </ul>
            </nav>
        </div>

    </div>
</asp:Content>
