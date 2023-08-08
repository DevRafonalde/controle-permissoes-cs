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
var indexSelectPermissao = 0;
function removerPerfil(id) {
    document.getElementById('select-' + id).remove();
    indexSelectPerfil--;
}

function removerPermissao(id) {
    document.getElementById('select-' + id).remove();
    indexSelectPermissao--;
}

$(document).ready(function () {
    var listaPerfisSelecionados = [];
    var listaPermissoesSelecionadas = [];

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

    function preencherPerfis(qntdPerfis) {
        for (var i = 0; i < qntdPerfis; i++) {
            carregarListaDePerfis();
        }
    }

    $("#adicionar-perfil").click(function () {
        carregarListaDePerfis()
    });

    $('#formulario-cadastro-usuario').submit(function (event) {
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
                window.location.replace("https://localhost:7225/Usuario/ListagemEspecifica/" + response);
            },
            error: function (error) {
                console.log("Erro na requisição: ", error);
            }
        });
    });

    $('#formulario-edicao-usuario').submit(function (event) {
        event.preventDefault();

        var listaPerfisSelect = document.getElementsByClassName("perfil-selecionado");

        for (var i = 0; i < listaPerfisSelect.length; i++) {
            listaPerfisSelecionados.push(listaPerfisSelect[i].value);
        }

        var modeloCadastroUsuarioPerfil = {
            Usuario: {
                Id: $('#id').val(),
                NomeCompleto: $('#nome-completo').val(),
                NomeAmigavel: $('#nome-amigavel').val(),
                NomeUser: $('#nome-user').val(),
                SenhaUser: $('#senha').val(),
                Observacao: $('#observacao').val(),
                CaixaVirtual: $('#caixa-virtual').is(":checked"),
                Ativo: $('#ativo').is(":checked")
            },
            PerfisSelecionadosIds: listaPerfisSelecionados
        };

        $.ajax({
            type: "POST",
            url: "/Usuario/EditarBanco",
            data: JSON.stringify(modeloCadastroUsuarioPerfil),
            contentType: "application/json",
            success: function (response) {
                console.log("Requisição bem sucedida: ", response);
                window.location.replace("https://localhost:7225/Usuario/ListagemEspecifica/" + response);
            },
            error: function (error) {
                console.log("Erro na requisição: ", error);
            }
        });
    });

    function carregarListaDePermissoes() {
        $.ajax({
            type: "GET",
            url: "/Perfil/GetTodasPermissoes",
            success: function (permissoes) {
                var selectOptions = permissoes.map(function (permissao) {
                    return '<option value="' + permissao.id + '">' + permissao.nome + '</option>';
                });
                var novoSelect = '<tr id="select-' + indexSelectPermissao + '">'
                    + '<th scope="row">' + (indexSelectPermissao + 1) + '</th>'
                    + '<td>'
                    + '<select class="permissao-selecionada form-select">'
                    + '<option selected disabled value="">Selecione uma permissão...</option>'
                    + selectOptions.join('')
                    + '</select>'
                    + '</td>'
                    + '<td>'
                    + '<a class="btn btn-danger" onclick="removerPermissao(' + indexSelectPermissao + ');">Remover Permissão</a>'
                    + '</td>'
                    + '</tr>';

                $("#selecao-permissoes").append(novoSelect);
                indexSelectPermissao++;
            },
            error: function (error) {
                console.error("Erro ao carregar perfis:", error);
            }
        });
    }

    $("#adicionar-permissao").click(function () {
        carregarListaDePermissoes()
    });

    $('#formulario-cadastro-perfil').submit(function (event) {
        event.preventDefault();

        var listaPermissoesSelect = document.getElementsByClassName("permissao-selecionada");

        for (var i = 0; i < listaPermissoesSelect.length; i++) {
            listaPermissoesSelecionadas.push(listaPermissoesSelect[i].value);
        }

        var modeloCadastroPerfilPermissao = {
            Perfil: {
                Nome: $('#nome').val(),
                Descricao: $('#descricao').val(),
                SistemaId: $('#sistema-id').val()
            },
            PermissoesSelecionadasIds: listaPermissoesSelecionadas
        };

        $.ajax({
            type: "POST",
            url: "/Perfil/Cadastrar",
            data: JSON.stringify(modeloCadastroPerfilPermissao),
            contentType: "application/json",
            success: function (response) {
                console.log("Requisição bem sucedida: ", response);
                window.location.replace("https://localhost:7225/Perfil/ListagemEspecifica/" + response);
            },
            error: function (error) {
                console.log("Erro na requisição: ", error);
            }
        });
    });

    $('#formulario-edicao-perfil').submit(function (event) {
        event.preventDefault();

        var listaPermissoesSelect = document.getElementsByClassName("permissao-selecionada");

        for (var i = 0; i < listaPermissoesSelect.length; i++) {
            listaPermissoesSelecionadas.push(listaPermissoesSelect[i].value);
        }

        var modeloCadastroPerfilPermissao = {
            Perfil: {
                Id: $('#id').val(),
                Nome: $('#nome').val(),
                Descricao: $('#descricao').val(),
                SistemaId: $('#sistema-id').val()
            },
            PermissoesSelecionadasIds: listaPermissoesSelecionadas
        };

        $.ajax({
            type: "POST",
            url: "/Perfil/EditarBanco",
            data: JSON.stringify(modeloCadastroPerfilPermissao),
            contentType: "application/json",
            success: function (response) {
                console.log("Requisição bem sucedida: ", response);
                window.location.replace("https://localhost:7225/Perfil/ListagemEspecifica/" + response);
            },
            error: function (error) {
                console.log("Erro na requisição: ", error);
            }
        });
    });
});