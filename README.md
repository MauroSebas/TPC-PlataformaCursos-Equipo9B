🎓 DevCore Academy - Plataforma LMS

Trabajo Práctico Final > Materia: Programación III

Tecnología: ASP.NET WebForms + ADO.NET

📋 Descripción del Proyecto

DevCore Academy es una plataforma integral de gestión de cursos online (LMS) diseñada para conectar a estudiantes con contenido educativo de calidad. El sistema administra todo el ciclo de vida educativo: desde la publicación de cursos y la venta, hasta el cursado, evaluación y certificación.

La aplicación cuenta con dos roles bien definidos (Administrador y Alumno) y un flujo de negocio robusto que incluye validación de pagos manuales y corrección de exámenes.

🚀 Funcionalidades Principales

👨‍🎓 Módulo Alumno

Catálogo de Cursos: Exploración de cursos con filtros por categoría y detalle completo.

Carrito de Compras: Gestión de inscripciones múltiples.

Pagos: Subida de comprobantes de transferencia para validación.

Aula Virtual:

Visualización de clases (Video y Archivos).

Barra de progreso en tiempo real.

Navegación secuencial de lecciones.

Evaluación: Entrega de trabajos prácticos/exámenes finales (Google Drive/GitHub).

Certificación: Descarga de diplomas en PDF tras la aprobación.

👮‍♂️ Módulo Administrador

Dashboard: Métricas en tiempo real de ingresos, alumnos y cursos.

Gestión de Cursos (ABM): Alta de cursos, módulos, lecciones y asignación de exámenes.

Gestión de Pagos: Panel para aprobar o rechazar comprobantes de pago.

Lógica de anulación y reactivación de inscripciones.

Correcciones: Bandeja de entrada de exámenes entregados.

Devolución (Feedback) al alumno.

Subida manual del Certificado digital.

Reportes: Historial completo de transacciones y entregas.

🛠️ Stack Tecnológico

El proyecto fue construido siguiendo una arquitectura en capas (N-Tier) para garantizar escalabilidad y mantenimiento.

Capa / Área

Tecnologías

Frontend

ASP.NET WebForms (.aspx), HTML5, CSS3, Bootstrap 5.3

Backend

C# (.NET Framework 4.8)

Base de Datos

SQL Server 2019+, T-SQL (Stored Procedures)

Acceso a Datos

ADO.NET (Desconectado y Conectado)

Scripting

JavaScript (Manejo de Modales y UI dinámica)

Control de Versiones

Git & GitHub

⚙️ Instalación y Puesta en Marcha

Sigue estos pasos para levantar el proyecto en tu entorno local:

1. Base de Datos

Abre SQL Server Management Studio (SSMS).

Ejecuta el script BD/DB_Completa_Final.sql incluido en este repositorio.

Este script crea la DB, las tablas, los procedimientos almacenados y carga datos de prueba.

2. Configuración de Conexión

Abre el proyecto en Visual Studio 2022.

Ubica el archivo Web.config.

Modifica la cadena de conexión CadenaConexion para que apunte a tu instancia local:

<connectionStrings>
  <add name="CadenaConexion" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=PlataformaCursosDB;Integrated Security=True;" providerName="System.Data.SqlClient" />
</connectionStrings>


3. Configuración de Email (Opcional)

Para que funcionen los correos de bienvenida y recuperación de clave:

Ve a la clase EmailService.cs.

Configura tus credenciales SMTP (Gmail App Password o Mailtrap).

🔑 Credenciales de Acceso (Demo)

El script de base de datos incluye usuarios precargados para probar el sistema inmediatamente.

Rol

Email

Contraseña

Administrador

admin@admin.com

admin

Alumno

alumno@demo.com

1234

📸 Capturas de Pantalla


✒️ Autores

Duarte Sebastián - Desarrollo Fullstack

Arias Mauro - Desarrollo Fullstack

Proyecto desarrollado para la Tecnicatura en Programación - UTN.
