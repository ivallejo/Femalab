function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function addActionsAttention() {

    $('.addattention').click(function () {
        var url = $(this).data('url')
        $.get(url, function (data) {
            $("#ModalAttentionAddOrEdit").html(data)
            $("#ModalAttentionAddOrEdit").modal('show')
        });
    });

    $('.details-attention').click(function () {
        var url = $(this).data('url')
        $.get(url, function (data) {
            $("#ModalAttentionAddOrEdit").html(data)
            $("#ModalAttentionAddOrEdit").modal('show')
            $("#btnGuardar").hide()
        });
    });

    $('.edit-attention').click(function () {
        var url = $(this).data('url')
        $.get(url, function (data) {
            $("#ModalAttentionAddOrEdit").html(data)
            $("#ModalAttentionAddOrEdit").modal('show')           
        });
    });

}

function getAllAttentions() {
    $.ajax({
        type: "POST",
        url: "../Attention/GetAll",
        success: function (data) {
            $('#dataTable').DataTable().destroy();
            $.each(data, function (i, item) {
                let createdDate = new Date(parseInt(item.CreatedDate.replace("/Date(", "").replace(")/", ""), 10))
                let formattedCreatedDate = createdDate.getFullYear() + "-" + pad((createdDate.getMonth() + 1), 2) + "-" + pad(createdDate.getDate(), 2) + " " + pad(createdDate.getHours(), 2) + ":" + pad(createdDate.getMinutes(), 2) + ":" + pad(createdDate.getSeconds(), 2)

                $("#tbAttentionBody").append(`\
                    <tr id="tr-${item.Id}">\                
                        <td class="border-left-${item.CategoryTag}"> ${item.Document} </td>\
                        <td> ${item.LastName + ' ' + item.FirstName}</td>\
                        <td> <h5><span class="badge badge-${item.TypeTag}">${item.Type}</span></h5> </td>\
                        <td> ${formattedCreatedDate} </td>\
                        <td>\
                            <button title="Detalle" data-id="${item.Id}" class="btn btn-success my-sm-1 details-attention" data-url="/Attention/${item.Action + '/' + item.Id}" > <i class="fas fa-info-circle"></i></button>\
                            <button title="Editar" data-id="${item.Id}" class="btn btn-warning my-sm-1 edit-attention" data-url="/Attention/${item.Action + '/' + item.Id}" > <i class="fas fa-user-edit"></i></button>\
                            <button title="Eliminar" data-id="${item.Id}" class="btn btn-danger my-sm-1 remove-attention" data-url="/Attention/Remove/${item.Id}"> <i class="fas fa-trash"></i></button>\
                        </td>\
                    </tr>\
                `)
            })
            addActionsAttention()   
            $('#dataTable').DataTable({
                order : [[3, 'desc']],
                language : {                    
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
    getAllAttentions()
    //addActionsAttention()

});






