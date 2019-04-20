
function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function addActionsPatients() {

    $('#btnAddProduct').click(function () {
        var url = $('#ModalProductAddOrEdit').data('url')
        $.get(url, function (data) {
            $("#ModalProductAddOrEdit").html(data)
            $("#ModalProductAddOrEdit").modal('show')
        });
    });

    $('.details-patient').click(function () {
        var url = $('#ModalProductAddOrEdit').data('url') + '/' + $(this).data("id")
        $.get(url, function (data) {
            $("#ModalProductAddOrEdit").html(data)
            $("#ModalProductAddOrEdit").modal('show')
        });
    });
}

function getAllPatients() {

    $.ajax({
        type: "POST",
        url: "../Product/GetAll",
        success: function (data) {

            //$("#tbPatientBody").empty()
            $('#dataTable').DataTable().clear();
            $('#dataTable').DataTable().destroy();

            $.each(data, function (i, item) {
                let createdDate = new Date(parseInt(item.CreatedDate.replace("/Date(", "").replace(")/", ""), 10))
                let formattedCreatedDate = createdDate.getFullYear() + "-" + pad((createdDate.getMonth() + 1), 2) + "-" + pad(createdDate.getDate(), 2) + " " + pad(createdDate.getHours(), 2) + ":" + pad(createdDate.getMinutes(), 2) + ":" + pad(createdDate.getSeconds(), 2)

                $("#tbProductBody").append(`<tr><td style='text-align:center'> ${item.Code} </td><td> ${item.Description} </td><td> ${formattedBirthDate} </td><td> ${formattedCreatedDate} </td><td> <button title="Editar" data-id="${item.Id}" class="btn btn-warning mb-2 mr-2 details-patient"> <i class="fas fa-user-edit"></i></button><button title="Detalle" data-id="${item.Id}" class="btn btn-success mb-2 mr-2 details-patient"> <i class="fas fa-history"></i></button></td></tr>`)

            })

            addActionsPatients()

            $('#dataTable').DataTable({
                order: [[4, 'desc']],
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                }
            })
        }
    });
}



$(document).ready(function () {

    getAllPatients()

    addActionsPatients()

});

