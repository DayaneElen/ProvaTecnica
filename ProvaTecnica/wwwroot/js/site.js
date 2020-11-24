// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#NomeUsuario").addClass("form-control")
})

$("#Create").submit(function (event) {
    let nomeUsuario = $('#NomeUsuario').val();
    if (nomeUsuario == '') {
        $('#NomeUsuario').focus();
        swal('Aviso', 'É necessário preencher o nome de usuário', 'warning');
        event.preventDefault();
    }
});

$("#Report").submit(function (event) {
    let nomeUsuario = $('#Usuarios option:selected').val();
});