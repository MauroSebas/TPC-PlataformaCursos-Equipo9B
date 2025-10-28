<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Vistas.Home" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<%-- Este es el placeholder para el <main>. Aquí va todo el contenido de tu página. --%>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- 1. SECCIÓN "HERO" (TRADUCIDA A BOOTSTRAP) --%>
    <div class="container my-5 py-5">
        <div class="row align-items-center g-5">
            <div class="col-lg-6 text-center text-lg-start">
                <h1 class="display-4 fw-bold lh-1 mb-3">Transforma tu futuro, una habilidad a la vez.</h1>
                <p class="lead">
                    Descubre cursos diseñados por expertos de la industria para impulsar tu carrera al siguiente nivel.
                </p>
                <div class="d-grid gap-2 d-md-flex justify-content-md-start">
                    <a href="#cursos" class="btn btn-primary btn-lg px-4 me-md-2">Explorar Cursos</a>
                </div>
            </div>
            <div class="col-lg-6">
                <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuCl1Cs_ED4qCV0rb-Ax5qA37yJG8GzelQ4f4WmALLuXFX2PHk1bTwETKRvifRP7Jlf8RNjVMyl1zwLhpAvdFFjOO2_2jZC_pTab_tnYRHL_Lpu47seYeuVOuQMSbRriTjN-1jWBvhmGhcVW16VPNKY8vTj1gu0mblebNT5WleKKEHaCwcLmLZ6JoO94jI-ZRlZy9UEEYr-aSwvywBKxLMWSxm67m1LmFCLSR8BWlts9icCI3fZoPJ8qNJ1o1LhxuLIkvLnYYpyDf1g" class="d-block mx-lg-auto img-fluid rounded-3 shadow-sm" alt="Person working on a laptop" loading="lazy">
            </div>
        </div>
    </div>

   
    <div class="bg-body-tertiary py-5" id="cursos">
        <div class="container">
            
            <div class="text-center mb-5">
                <h2 class="display-5 fw-bold">Nuestros Cursos</h2>
                <p class="lead text-muted">Explora nuestra selección de cursos y encuentra el perfecto para ti.</p>
            </div>

            <%-- Filtros de categoría --%>
            <ul class="nav nav-pills justify-content-center mb-4">
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="#">Todos</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Frontend</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Backend</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Bases de Datos</a>
                </li>
            </ul>

            <%-- Grilla de Cursos --%>
            <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-3 g-4">
                
                <%-- Card de Curso 1 --%>
                <div class="col">
                    <asp:HyperLink NavigateUrl="~/CursoDetalle.aspx" CssClass="text-decoration-none" runat="server">
                        <div class="card h-100 shadow-sm border-0 card-curso">
                            <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuDulIP4_kSCnje-KB9DatVdv8wPFlj4nj8JrfavWVPWGLgpiH6dThazngpNa7lV4snXrFCtVeVkfyZYOdXkOEnshoW9d7BZSvIgII5WrcXobFro2aAYe--57hOQQ3rsco-rONTuUu2IO4mPyg1MCb9GRchQKbhfCkLzNH217DYkMEjjPxHmR9aZY7hSZclX2kefcij_s6TNp6KXtGj-6y80oenxrxo9W_U_2BeQYAAqWSdoUUMIRukKHUG06Z5CgtEQRAvcaDHyfTA" class="card-img-top" alt="ReactJS logo">
                            <div class="card-body p-5 d-flex flex-column">
                                <p class="text-primary fw-semibold small">Frontend</p>
                                <h5 class="card-title fw-bold mb-auto">Introducción a React.js</h5>
                                <p class="text-muted mt-3 mb-0">$49.99 - Acceso Ilimitado</p>
                            </div>
                        </div>
                    </asp:HyperLink>
                </div>

                <%-- Card de Curso 2 --%>
                <div class="col">
                    <asp:HyperLink NavigateUrl="~/CursoDetalle.aspx" CssClass="text-decoration-none" runat="server">
                        <div class="card h-100 shadow-sm border-0 card-curso">
                            <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuCpoXWpa_YIZIQWba3SpJXfA7H_eyKAiNXJBMsl4PcFBD1c-UYaQKfu5LvN3h79C1OZPGWHh1RT4CM9lTsrJAd0pT6iWiuuH06RNKULZZ8jOvK8oX1OrtWK-9T36lHUSDhZUa-RHfVdPitmZTesoC1zXhecFwkVrlARTgjaA2DXv4ivDhLzRM0H2JXqae_4e5H1NbykC3dlIOeG4Ine9v4Ru4bNTZsqPnP8nao129UnWkvGZtR64hQOZlVJDqrhZaHBwhJBuo95o5c" class="card-img-top" alt="NodeJS code">
                            <div class="card-body p-4 d-flex flex-column">
                                <p class="text-primary fw-semibold small">Backend</p>
                                <h5 class="card-title fw-bold mb-auto">Node.js para Principiantes</h5>
                                <p class="text-muted mt-3 mb-0">Gratis - Acceso por 90 días</p>
                            </div>
                        </div>
                    </asp:HyperLink>
                </div>

                <%-- Card de Curso 3 --%>
                <div class="col">
                    <asp:HyperLink NavigateUrl="~/CursoDetalle.aspx" CssClass="text-decoration-none" runat="server">
                        <div class="card h-100 shadow-sm border-0 card-curso">
                            <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuDCpZ-suFSDi-_XOK7d7sE4qDvlBF2Ap7LgBuVXM9LXRET0TfdfsSSCxtTJreU9AEfChVZXOpoFoWlZhpqnGnzbVvf0jdJUYf2MwZ6MkXXi4g2YehXSrkt2KWd8lT-0mOYB444V_cFrqpagCXvnrfh4BsBxZ1F-YuyWiT6ibHhXQ5TWvrXgww67VbaDPA9LX36KvaGEnlqs7XMESRksNuGq6eNll59k55CLNsgVIwW6LBwx6heatjUP8u_fQBz6SGBaec9x5GK58dQ" class="card-img-top" alt="Database diagram">
                            <div class="card-body p-4 d-flex flex-column">
                                <p class="text-primary fw-semibold small">Bases de Datos</p>
                                <h5 class="card-title fw-bold mb-auto">SQL Esencial</h5>
                                <p class="text-muted mt-3 mb-0">$29.99 - Acceso Ilimitado</p>
                            </div>
                        </div>
                    </asp:HyperLink>
                </div>

                <%-- Card de Curso 4 --%>
                <div class="col">
                    <asp:HyperLink NavigateUrl="~/CursoDetalle.aspx" CssClass="text-decoration-none" runat="server">
                        <div class="card h-100 shadow-sm border-0 card-curso">
                            <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuBRP56WEZCwqIYw2Mg5yCK5QtnmBM0L-mCnrufk3yNdErXgFGCMY24o1fusAi7AjhQBVXOW6DC_Gx-PcSN0t_wU9qkWD2U3jrLTjRRT3u1Z1L47eo2UVukJk54bPnP2Z4TByRU0knFXcNjVZ-x6fyvvivRvNcaVEgHzFM4AhujY0K2Oc3v9-ipLA7MHPjzO9ArwAeXHiREtvPsTEVsbd4YI2r8mTqITYNs81x3TXXkx26uZhb-CByIGeD8a0fGQl_a_GNfxuDkBpww" class="card-img-top" alt="VueJS code">
                            <div class="card-body p-4 d-flex flex-column">
                                <p class="text-primary fw-semibold small">Frontend</p>
                                <h5 class="card-title fw-bold mb-auto">Desarrollo con Vue 3</h5>
                                <p class="text-muted mt-3 mb-0">$49.99 - Acceso Ilimitado</p>
                            </div>
                        </div>
                    </asp:HyperLink>
                </div>

                <%-- Card de Curso 5 --%>
                <div class="col">
                    <asp:HyperLink NavigateUrl="~/CursoDetalle.aspx" CssClass="text-decoration-none" runat="server">
                        <div class="card h-100 shadow-sm border-0 card-curso">
                            <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuAs7_l3rR4sLNlNNAPmX4dTXfvf84dPjSmORdRYcK7KzJP4cS3oOXFdBO48q1um0h4o1mQu7yWyq6az4807vcTW6ht2I9orqXXK7aZ8X-7zIGkiPtJ4LY9AOvewlFlgVAGT8sAePU8bdHYXbycFsFcYeXjwGV3xwZYaC1Nadj3MNeWf4hC3AM3PwovH_05xczPA1Q_sh1zy2uXYY82UP4NPTcbIBU6wHeym_Of8BPScCcBtonYsbLZC6UOck1_f40nh5aCGt4fnLxg" class="card-img-top" alt="JavaScript code">
                            <div class="card-body p-4 d-flex flex-column">
                                <p class="text-primary fw-semibold small">Backend</p>
                                <h5 class="card-title fw-bold mb-auto">APIs con Express</h5>
                                <p class="text-muted mt-3 mb-0">$39.99 - Acceso Ilimitado</p>
                            </div>
                        </div>
                    </asp:HyperLink>
                </div>

                <%-- Card de Curso 6 --%>
                <div class="col">
                    <asp:HyperLink NavigateUrl="~/CursoDetalle.aspx" CssClass="text-decoration-none" runat="server">
                        <div class="card h-100 shadow-sm border-0 card-curso">
                            <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuDNTn4km7unVgAZx4V8QSC6JUd6SLJjtwVKjS0dNP3Yg4Ge0HCILRYKr8wDjnxWWpn2IqPA_j8suYDUqL3OESvPwNhXxGTheRmv_j0fvdIxWelBebgsGfg4aILxgqVFUX_yTO2jgT8I8EQ2noHhlwHwAYg_8Qiy-sWp8YChVGfbv_TSpP8aLktCHRw3ex1eNeAjWKZO9o6eKLrWSo8ShsFN5tSym40lp32f_0Nh1tWN4LXusbePXhYnnLmiCgsZD9goYaKsmFXvVjE" class="card-img-top" alt="Collaboration">
                            <div class="card-body p-4 d-flex flex-column">
                                <p class="text-primary fw-semibold small">Bases de Datos</p>
                                <h5 class="card-title fw-bold mb-auto">Modelado de Datos NoSQL</h5>
                                <p class="text-muted mt-3 mb-0">Gratis - Acceso por 90 días</p>
                            </div>
                        </div>
                    </asp:HyperLink>
                </div>

            </div> <%-- Fin del .row --%>
        </div> <%-- Fin del .container --%>
    </div> <%-- Fin del .bg-body-tertiary --%>

</asp:Content>