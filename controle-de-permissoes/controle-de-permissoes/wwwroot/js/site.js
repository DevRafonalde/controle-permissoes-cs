// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Código para funcionamento do DataTable
setTraducaoDataTable('#table-permissoes')
setTraducaoDataTable('#table-perfis')
setTraducaoDataTable('#table-usuarios')
$('.btn-total-contatos').click(function () {
    var usuarioId = $(this).attr('usuario-id');
    var urlCompleta = '/Usuario/ListarContatosPorUsuario/' + usuarioId;
    console.log(urlCompleta);
    $.ajax({
        type: 'GET',
        url: urlCompleta,
        success: function (result) {
            $("#corpoModal").html(result)
            setTraducaoDataTable('#table-contatos-usuario');
        }
    })
})

function setTraducaoDataTable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}

// Código para funcionamento do envio dos formulários de cadastro

var indexSelectPerfil = 0;
function removerPerfil(id) {
    document.getElementById('select-' + id).remove();
    indexSelectPerfil--;
}

$(document).ready(function () {
    var listaPerfisSelecionados = [];

    function carregarListaDePerfis() {
        $.ajax({
            type: "GET",
            url: "/Usuario/GetTodosPerfis",
            success: function (perfis) {
                var selectOptions = perfis.map(function (perfil) {
                    return '<option value="' + perfil.id + '">' + perfil.nome + '</option>';
                });
                var novoSelect = '<tr id="select-' + indexSelectPerfil + '">'
                    + '<th scope="row">' + (indexSelectPerfil + 1) + '</th>'
                    + '<td>'
                    + '<select class="perfil-selecionado form-select">'
                    + '<option selected disabled value="">Selecione um perfil...</option>'
                    + selectOptions.join('')
                    + '</select>'
                    + '</td>'
                    + '<td>'
                    + '<a class="btn btn-danger" onclick="removerPerfil(' + indexSelectPerfil + ');">Remover Perfil</a>'
                    + '</td>'
                    + '</tr>';

                $("#selecao-perfis").append(novoSelect);
                indexSelectPerfil++;
            },
            error: function (error) {
                console.error("Erro ao carregar perfis:", error);
            }
        });
    }

    $("#adicionar-perfil").click(function () {
        carregarListaDePerfis()
    });

    $('#formulario-cadastro').submit(function (event) {
        event.preventDefault();

        var listaPerfisSelect = document.getElementsByClassName("perfil-selecionado");

        for (var i = 0; i < listaPerfisSelect.length; i++) {
            listaPerfisSelecionados.push(listaPerfisSelect[i].value);
        }

        var modeloCadastroUsuarioPerfil = {
            Usuario: {
                NomeCompleto: $('#nome-completo').val(),
                NomeAmigavel: $('#nome-amigavel').val(),
                NomeUser: $('#nome-user').val(),
                SenhaUser: $('#senha').val(),
                Observacao: $('#observacao').val(),
                CaixaVirtual: $('#caixa-virtual').is(":checked")
            },
            PerfisSelecionadosIds: listaPerfisSelecionados
        };

        $.ajax({
            type: "POST",
            url: "/Usuario/Cadastrar",
            data: JSON.stringify(modeloCadastroUsuarioPerfil),
            contentType: "application/json",
            success: function (response) {
                console.log("Requisição bem sucedida: ", response);
            },
            error: function (error) {
                console.log("Erro na requisição: ", error);
            }
        });
    });
});