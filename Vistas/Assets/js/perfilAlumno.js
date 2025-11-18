// Esta función "revive" los ojos después de CUALQUIER carga AJAX
if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(inicializarOjos);
}

// Esta función se llama CADA VEZ que la página carga (normal o AJAX)
function inicializarOjos() {
    // Busca los 4 botones "ojo" por su ID de HTML (que le pasamos desde el ASPX)
    // y les asigna el evento.
    // Usamos selectores genéricos para no depender de ClientID
    togglePassword('#btnShowPassActual', 'input[id*="txtPassActual"]');
    togglePassword('#btnShowPassNueva', 'input[id*="txtPassNueva"]');
    togglePassword('#btnShowPassRepetir', 'input[id*="txtPassRepetir"]');
    togglePassword('#btnShowPassConfirmarEmail', 'input[id*="txtPassConfirmarEmail"]');
}

// --- Código para la "Precarga" de la Foto en el MODAL ---
function inicializarPrecargaAvatar() {
    // Busca los controles por ID
    var fileUploader = document.querySelector('input[id*="fileUploadModal"]');
    var imgPreview = document.getElementById('imgPrecargaAvatar');

    if (!fileUploader || !imgPreview) return;

    fileUploader.addEventListener('change', function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                imgPreview.src = e.target.result;
                imgPreview.style.display = 'block';
            };

            reader.readAsDataURL(this.files[0]);
        }
    });
}

// --- Ejecutamos todo al cargar la página ---
document.addEventListener('DOMContentLoaded', function () {
    inicializarOjos();
    inicializarPrecargaAvatar();
});