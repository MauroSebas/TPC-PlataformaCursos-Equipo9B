
//Función para hacer visible la contraseña en el Login y Registro de usuario en el campo de Contraseña.
function togglePassword(btnSelector, inputSelector) {
    const btn = document.querySelector(btnSelector);
    const input = document.querySelector(inputSelector);
    if (!btn || !input) return;

    btn.addEventListener('click', function () {
        if (input.type === 'password') {
            input.type = 'text';
            btn.innerHTML = '<i class="bi bi-eye-slash"></i>';
        } else {
            input.type = 'password';
            btn.innerHTML = '<i class="bi bi-eye"></i>';
        }
    });
}
