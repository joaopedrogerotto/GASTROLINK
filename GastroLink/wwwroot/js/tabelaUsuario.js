function visualizarUsuario(idUsuario) {
    $.ajax({
        url: '/Usuario/VisualizarUsuario?idUsuario=' + idUsuario,
        method: 'GET',
        success: function (html) {
            $("#modalContainer").html(html);
            $("#visualizarUsuarioModal").modal('show');
        },
        error: function (xhr, status, error) {

        }
    })
}