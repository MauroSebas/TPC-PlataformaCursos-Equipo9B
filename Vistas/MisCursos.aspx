<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MisCursos.aspx.cs" Inherits="Vistas.MisCursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Título de la página --%>
    <div class="my-4 pt-4"> <%-- Margen vertical --%>
        <h1 class="display-5 fw-bold">Mis Cursos</h1>
        <p class="lead text-muted">¡Hola Julieta! Continúa tu aprendizaje donde lo dejaste.</p>
    </div>

    <%-- Grilla de Cursos del Usuario --%>
    <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-3 g-4 mb-5">

        <%-- Card Curso 1 (En Progreso) --%>
        <div class="col">
            <div class="card h-100 shadow-sm border-0">
                <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuDbgY0x6rkKwGKGqHRtCA-99CeInaKDYUMOLGL75PU37FhjjhJOS5IzopocFBWfrXlS_qujxT029wWNxg77Putw25Ft8RfWbjW7GHDHZOWzmR1_WBnnVcNzoq4JNkwgru5UiA4sxORWqTJ2Id6NUMgNNvPa-UQPuIsvAu5E2ocS57q3fu8c7bKM0tmULzCJ0jF4K7DFPWXfwT-9eIOyv_Mjrm-04IRMHgE6XBicn18Ov5Jj8m9VsW76My59duypKKM6jisZqMyqBrI" class="card-img-top" alt="Curso React">
                <div class="card-body p-4 d-flex flex-column">
                    <p class="text-muted small mb-1">Desarrollo Web</p>
                    <h5 class="card-title fw-bold">Introducción a React y Hooks Avanzados</h5>

                    <%-- Barra de Progreso --%>
                    <div class="mt-auto pt-3">
                        <div class="d-flex justify-content-between small mb-1">
                            <span class="text-muted">Progreso</span>
                            <span class="fw-medium">75%</span>
                        </div>
                        <div class="progress" role="progressbar" aria-label="Progreso del curso" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="height: 8px;">
                            <div class="progress-bar" style="width: 75%;"></div>
                        </div>
                        <%-- Botón --%>
                        <div class="d-grid mt-3">
                            <asp:HyperLink NavigateUrl="~/Aula.aspx" Text="Continuar Curso" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- Card Curso 2 (En Progreso) --%>
        <div class="col">
            <div class="card h-100 shadow-sm border-0">
                <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuCPDW7JNptuF8rW9smez3BfTIuQK7AGsrMe0nl4Agdv4kpP0QAUr2sISjkKIAW8C7r-qd7O01aVNTJC--9TAVoOSkgW86LKnKaNqQpxb5aRhixfNvU6gYwrOaz_w7_jwNj0-hmqbo2qfpfDlEUZb-Xs09VHD02cw4QWwMvkKF4gmWNjpXahUGAnspSE1eB7yogOUWa-3W4WmMpL8Bhte6aqlc5nE2n9HUdDME9rgxvrHB1FkxMEq_Iwmb37ANsDBbDaMdzdawai3eQ" class="card-img-top" alt="Curso Marketing">
                <div class="card-body p-4 d-flex flex-column">
                    <p class="text-muted small mb-1">Marketing Digital</p>
                    <h5 class="card-title fw-bold">Estrategias de Contenido para Redes Sociales</h5>
                    <div class="mt-auto pt-3">
                         <div class="d-flex justify-content-between small mb-1">
                            <span class="text-muted">Progreso</span>
                            <span class="fw-medium">25%</span>
                        </div>
                        <div class="progress" role="progressbar" aria-label="Progreso del curso" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100" style="height: 8px;">
                            <div class="progress-bar" style="width: 25%;"></div>
                        </div>
                        <div class="d-grid mt-3">
                            <asp:HyperLink NavigateUrl="~/Aula.aspx" Text="Continuar Curso" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- Card Curso 3 (Completado) --%>
        <div class="col">
            <div class="card h-100 shadow-sm border-0">
                <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuANRxN9YnyZbfZO56PzaI4xWJt9lBn8NgXEY8RcllJiHUrqPhlpfmc0Scg_a_Y-lyyWNGma2S6tzAKklS39TV8IFY7Oelg6rexLNC0dcD5eYaZCpwUvua10fE5ymhtCVwn4Lw7iQmkvXEns92dbSsE6k7HnvCatCecRq5wXNpytD8Cmrk0FDFCnhImpMYHptJDXjoDkrItr2xg8mAVmDyPd1OLs8xLXwQqLw6sHo9RilKchHQiYxvk9dcyHxMpc-B-hoKTS7uhhlx8" class="card-img-top" alt="Curso Diseño UI">
                 <div class="card-body p-4 d-flex flex-column">
                    <p class="text-muted small mb-1">Diseño UX/UI</p>
                    <h5 class="card-title fw-bold">Fundamentos del Diseño de Interfaces</h5>
                    <div class="mt-auto pt-3">
                         <div class="d-flex justify-content-between small mb-1">
                            <span class="text-muted">Progreso</span>
                            <span class="fw-medium text-success">100% Completado</span> <
                        </div>
                        <div class="progress" role="progressbar" aria-label="Progreso del curso" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="height: 8px;">
                            <div class="progress-bar bg-success" style="width: 100%;"></div> 
                        </div>
                         <%-- Botón "Ver Certificado" --%>
                        <div class="d-grid mt-3">
                            
                            <asp:HyperLink NavigateUrl="~/Aula.aspx" Text="Ver Certificado" CssClass="btn btn-outline-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

         <%-- Card Curso 4 (En Progreso) --%>
        <div class="col">
            <div class="card h-100 shadow-sm border-0">
                <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuBGVpzMnMsS1aiGK_P0UBpdGG4pyW_0iCh2rj9ewKJ2q6FspvChi8TscUU6SVC2vCj1bru_ELsVy8-_KVXdpS7feWqNaFY5n6twSU8GTupq1gVYpaeMO2FTuVcN8FDvb0BfcLGQzU__6b94LlCGEYuSVj6tVqnx4x2xqrgnAlchs4smr11BDS_xa358qYg_cg9znRwxbS44JpY6YHXbrSuvkiIpAzcWjub9PF0YtPd56ElJMx9sGlhDoQXRkbeZZlJeubKSXviPGlA" class="card-img-top" alt="Curso Python">
                 <div class="card-body p-4 d-flex flex-column">
                    <p class="text-muted small mb-1">Ciencia de Datos</p>
                    <h5 class="card-title fw-bold">Python para Análisis de Datos</h5>
                     <div class="mt-auto pt-3">
                         <div class="d-flex justify-content-between small mb-1">
                            <span class="text-muted">Progreso</span>
                            <span class="fw-medium">50%</span>
                        </div>
                        <div class="progress" role="progressbar" aria-label="Progreso del curso" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="height: 8px;">
                            <div class="progress-bar" style="width: 50%;"></div>
                        </div>
                        <div class="d-grid mt-3">
                           <asp:HyperLink NavigateUrl="~/Aula.aspx" Text="Continuar Curso" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div> 
</asp:Content>
