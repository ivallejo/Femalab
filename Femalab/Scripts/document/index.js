
function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function addActionsDocument() {

    $('.send-document').click(function () {
        $(".loading").show()
        var url = $(this).data('url')
        var id = $(this).data('id')
        $.post(url, { id: id} ,function (data) {
            getAllDocuments()
            $(".loading").hide()
        }).done(function () {
            alert("second success");
            $(".loading").hide()
        })
        .fail(function () {
            alert("error");
            $(".loading").hide()
        })
        .always(function () {
            alert("finished");
            $(".loading").hide()
        });

    });

}

function getAllDocuments() {
    $.ajax({
        type: "POST",
        url: "../Document/GetAll",
        data: { dateBegin: $('#dateBegin').val(), dateEnd: $('#dateEnd').val(), },
        success: function (data) {
            $('#dataTable').DataTable().clear();
            $('#dataTable').DataTable().destroy();
            $.each(data, function (i, item) {
                let createdDate = new Date(parseInt(item.CreatedDate.replace("/Date(", "").replace(")/", ""), 10))
                let paidDate = new Date(parseInt(item.PaidDate.replace("/Date(", "").replace(")/", ""), 10))
                let formattedCreatedDate = createdDate.getFullYear() + "-" + pad((createdDate.getMonth() + 1), 2) + "-" + pad(createdDate.getDate(), 2) + " " + pad(createdDate.getHours(), 2) + ":" + pad(createdDate.getMinutes(), 2) + ":" + pad(createdDate.getSeconds(), 2)
                let formattedPaidDate = paidDate.getFullYear() + "-" + pad((paidDate.getMonth() + 1), 2) + "-" + pad(paidDate.getDate(), 2) + " " + pad(paidDate.getHours(), 2) + ":" + pad(paidDate.getMinutes(), 2) + ":" + pad(paidDate.getSeconds(), 2)

                let strState;
                if (item.SunatState == 0) {
                    strState = '<span class="badge badge-warning">Pendiente</span>';
                } else if (item.SunatState == 1){
                    strState = '<span class="badge badge-success">Enviado</span>';
                } else if (item.SunatState == 2) {
                    strState = '<span class="badge badge-danger">Rechazado</span>';
                } else {
                    strState = '<span class="badge badge-warning">Pendiente</span>';
                }

                $("#tbDocumentBody").append(`\
                        <tr id="tr-${item.Id}">\   
                        <td style="width:40px;text-align:center"> ${item.SunatNumber} </td>\
                        <td style="width:80px;text-align:center"> <h5>${strState}</h5> </td>\
                        <td style="width:50px;text-align:center">\
                            <button title="Enviar a SUNAT" data-id="${item.Id}" class="btn btn-warning my-sm-1 send-document" data-url="/Document/Send" > <i class="fas fa-file-upload fa-sm"></i></button>\                                                    </td>\
                        </td>\                        
                        <td style="width:250px"> ${item.FirstName} </td>\
                        <td style="width:80px"> ${item.TotalValue.toFixed(2)} </td>\                            
                        <td style="width:90px"> ${formattedCreatedDate} </td>\
                        <td style="width:90px"> ${formattedPaidDate} </td>\
                        <td style="width:30px;text-align:center">\
                            <a title="Descargar PDF" class="btn btn-primary my-sm-1 invoice-pdf" href="${item.SunatPdf}"> <i class="fas fa-file-pdf fa-sm"></i></a>\
                        </td>\
                    </tr>\
                `)
            })
            addActionsDocument()
            $('#dataTable').DataTable({
                order: [[5, 'desc']],
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

    document.getElementById('dateBegin').valueAsDate = new Date();
    document.getElementById('dateEnd').valueAsDate = new Date();

    $('.finpenfing').click(function () {
        getAllDocuments()
    });
    
    getAllDocuments()

});

