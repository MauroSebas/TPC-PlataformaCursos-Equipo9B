
function abrirModalPago() {
    var modalEl = document.getElementById('pagoModal');
    if (modalEl) {
        // getOrCreateInstance es un método de Bootstrap 5 que evita errores
        // si el modal ya estaba inicializado.
        var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
        modal.show();
    }
}

function abrirModalExito() {
    var modalEl = document.getElementById('modalConfirmacion');
    if (modalEl) {
        var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
        modal.show();
    }
}