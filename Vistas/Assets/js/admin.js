
(function () {
    'use strict';

    /**
     * Esta función encuentra la página actual en la URL
     * y le aplica la clase "active" al link correspondiente
     * en el menú lateral.
     */
    function activarMenuLateral() {

        // 1. Obtener la ruta de la página actual
        // Ej: "/Administrador/CursoPanel.aspx"
        const rutaActual = window.location.pathname;

        // 2. Buscar TODOS los links dentro de la <nav>
        const nav = document.querySelector('.sidebar nav');
        if (!nav) return; // Salir si no se encuentra la nav

        const navLinks = nav.querySelectorAll('a.nav-link');

        // 3. Recorrer cada link
        navLinks.forEach(link => {

            // Obtener el 'href' del link
            const linkHref = link.getAttribute('href');

            // 4. Comparar si la URL actual EMPIEZA con el href del link
            // Usamos startsWith para que funcione aunque haya parámetros
            if (rutaActual.startsWith(linkHref) && linkHref !== "#") {

                // ¡Coincidencia! Aplicamos las clases correctas
                link.classList.add('active'); // Lo pone azul
                link.classList.remove('text-body-secondary'); // Le quita el gris
            } else {

                // No es la página actual, nos aseguramos que no esté activa
                link.classList.remove('active');
                link.classList.add('text-body-secondary');
            }
        });
    }

    // 5. Ejecutar la función cuando el documento esté listo
    document.addEventListener('DOMContentLoaded', activarMenuLateral);

})();