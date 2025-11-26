# 🎓 **DevCore Academy - Plataforma LMS**

### **Trabajo Práctico Final — Programación III**  
**Tecnología:** ASP.NET WebForms + ADO.NET  

---

## 📋 **Descripción del Proyecto**

**DevCore Academy** es una plataforma integral de gestión de cursos online (LMS) diseñada para conectar a estudiantes con contenido educativo de calidad.  
El sistema administra todo el ciclo de vida educativo: **publicación de cursos**, **venta**, **cursado**, **evaluación** y **certificación**.

Cuenta con dos roles bien definidos:

- **Administrador**
- **Alumno**

Incluye un flujo de negocio robusto: validación manual de pagos, entrega y corrección de exámenes, y generación de certificados PDF.

---

## 🚀 **Funcionalidades Principales**

---

### 👨‍🎓 **Módulo Alumno**

- **Catálogo de Cursos:** Exploración con filtros por categoría y detalle completo.  
- **Carrito de Compras:** Permite múltiples inscripciones.  
- **Pagos:** Subida de comprobantes para validación.  
- **Aula Virtual:**  
  - Visualización de clases (video y archivos).  
  - Barra de progreso en tiempo real.  
  - Navegación secuencial de lecciones.  
- **Evaluación:** Entrega de trabajos prácticos o exámenes finales (Google Drive / GitHub).  
- **Certificación:** Descarga de diplomas en PDF tras aprobación.  

---

### 👮‍♂️ **Módulo Administrador**

- **Dashboard:** Métricas en tiempo real de ingresos, alumnos y cursos.  
- **Gestión de Cursos (ABM):** Alta de cursos, módulos, lecciones y asignación de exámenes.  
- **Gestión de Pagos:** Aprobación y rechazo de comprobantes.  
- **Inscripciones:** Anulación y reactivación.  
- **Correcciones:** Bandeja de entrada de entregas.  
- **Feedback:** Devolución al alumno.  
- **Certificados:** Subida manual del certificado digital.  
- **Reportes:** Historial completo de transacciones y entregas.  

---

## 🛠️ **Stack Tecnológico**

El proyecto fue construido siguiendo una arquitectura **N-Tier**, asegurando escalabilidad y mantenimiento.

| **Capa / Área**        | **Tecnologías** |
|------------------------|------------------|
| **Frontend**           | ASP.NET WebForms (.aspx), HTML5, CSS3, Bootstrap 5.3 |
| **Backend**            | C# (.NET Framework 4.8) |
| **Base de Datos**      | SQL Server 2019+, T-SQL (Stored Procedures) |
| **Acceso a Datos**     | ADO.NET (Desconectado y Conectado) |
| **Scripting**          | JavaScript (Modales y UI dinámica) |
| **Control de Versiones** | Git & GitHub |

---

## ⚙️ **Instalación y Puesta en Marcha**

### **1. Base de Datos**

1. Abrí **SQL Server Management Studio (SSMS)**.  
2. Ejecutá el script:  
   **`PlataformaCursosDB`**  
   Este script crea toda la DB: tablas, SPs y datos de prueba.

---

### **2. Configuración de Conexión**

1. Abrí el proyecto en **Visual Studio 2022**.  
2. Buscá el archivo **Web.config**.  
3. Modificá la connection string:

```xml
<connectionStrings>
  <add name="CadenaConexion"
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=PlataformaCursosDB;Integrated Security=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>

### **3. Configuración de Email (Opcional)**

Para que funcionen los correos de bienvenida y recuperación:

1. Abrí **EmailService.cs**.  
2. Configurá tus credenciales SMTP  
   *(Gmail App Password / Mailtrap)*.

---

## 🔑 **Credenciales de Acceso (Demo)**

| **Rol**          | **Email**           | **Contraseña** |
|------------------|---------------------|----------------|
| **Administrador** | admin@admin.com     | admin          |
| **Alumno**        | alumno@demo.com     | 1234           |

---

## ✒️ **Autores**

- **Sebastián Duarte** — Desarrollo Fullstack  
- **Mauro Arias** — Desarrollo Fullstack  

Proyecto desarrollado para la **Tecnicatura en Programación — UTN**.

