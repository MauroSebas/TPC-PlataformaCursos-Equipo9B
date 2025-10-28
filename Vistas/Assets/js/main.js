(function () {
    'use strict';

    const mainNavbar = document.getElementById('main-navbar');
    const themeIcon = document.getElementById('theme-icon');

    const getStoredTheme = () => localStorage.getItem('theme');
    const setStoredTheme = theme => localStorage.setItem('theme', theme);

    const setTheme = theme => {
        if (theme === 'dark') {
            document.documentElement.setAttribute('data-bs-theme', 'dark');
            if (themeIcon) {
                // ANTES: themeIcon.classList.remove('bi-sun'); themeIcon.classList.add('bi-moon');
                // CORREGIDO: Muestra el SOL para cambiar a CLARO
                themeIcon.classList.remove('bi-moon');
                themeIcon.classList.add('bi-sun');
            }
            if (mainNavbar) {
                mainNavbar.classList.remove('bg-white', 'navbar-light');
                mainNavbar.classList.add('bg-dark', 'navbar-dark');
            }
        } else { // Light mode
            document.documentElement.setAttribute('data-bs-theme', 'light');
            if (themeIcon) {
                // ANTES: themeIcon.classList.remove('bi-moon'); themeIcon.classList.add('bi-sun');
                // CORREGIDO: Muestra la LUNA para cambiar a OSCURO
                themeIcon.classList.remove('bi-sun');
                themeIcon.classList.add('bi-moon');
            }
            if (mainNavbar) {
                mainNavbar.classList.remove('bg-dark', 'navbar-dark');
                mainNavbar.classList.add('bg-white', 'navbar-light');
            }
        }
    };

    // ... (El resto del código es igual) ...

    // 1. APLICAR TEMA AL CARGAR LA PÁGINA
    const storedTheme = getStoredTheme();
    // CORREGIDO: Default a 'light' si no hay nada guardado
    const preferredScheme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    const currentTheme = storedTheme ? storedTheme : preferredScheme;
    setTheme(currentTheme);

    // 2. AGREGAR EL CLICK AL BOTÓN
    const themeToggler = document.getElementById('theme-toggler');
    if (themeToggler) {
        themeToggler.addEventListener('click', (event) => {
            event.preventDefault();
            const currentTheme = document.documentElement.getAttribute('data-bs-theme');
            const newTheme = (currentTheme === 'light') ? 'dark' : 'light';
            setStoredTheme(newTheme);
            setTheme(newTheme);
        });
    }

})();