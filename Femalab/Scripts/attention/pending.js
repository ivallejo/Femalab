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

    $('.invoice-attention').click(function () {
        var url = $(this).data('url')
        $.get(url, function (data) {
            $("#ModalAttentionAddOrEdit").html(data)
            $("#ModalAttentionAddOrEdit").modal('show')
        });
    });

    $('.receipt-attention').click(function () {
        var url = $(this).data('url')
        $.get(url, function (data) {
            
            win = window.open();
            win.document.write(data);

            var document_focus = false;
            $(document).ready(function () { win.window.print(); document_focus = true; });
            setInterval(function () { if (document_focus === true) { win.window.close(); } }, 300);

        });
    });
}

function getAllAttentions() {
    $.ajax({
        type: "POST",
        url: "../Attention/GetAllPending",
        data: { filtro: $('#cboFiltro').val() ,dateBegin: $('#dateBegin').val(), dateEnd: $('#dateEnd').val(), },
        success: function (data) {
            $('#dataTable').DataTable().clear();
            $('#dataTable').DataTable().destroy();
            $.each(data, function (i, item) {
                let createdDate = new Date(parseInt(item.CreatedDate.replace("/Date(", "").replace(")/", ""), 10))
                let formattedCreatedDate = createdDate.getFullYear() + "-" + pad((createdDate.getMonth() + 1), 2) + "-" + pad(createdDate.getDate(), 2) + " " + pad(createdDate.getHours(), 2) + ":" + pad(createdDate.getMinutes(), 2) + ":" + pad(createdDate.getSeconds(), 2)
                
                $("#tbAttentionBody").append(`\
                        <tr id="tr-${item.Id}">\                
                        <td> ${item.Total.toFixed(2)} </td>\                
                        <td> ${item.Pay.toFixed(2)} </td>\         
                        <td> ${(item.Total - item.Pay).toFixed(2)} </td>\
                        <td> ${item.Document} </td>\
                        <td> ${item.LastName + ' ' + item.FirstName} </td>\
                        <td style='text-align:center'> ${((item.Gender == 'M') ? '<i class="fas fa-mars fa-2x text-primary"></i>' : '<i class="fas fa-2x fa-venus text-danger"></i>')} </td>\
                        <td> ${item.Age} </td>\
                        <td> ${item.Weight} </td>\
                        <td> ${item.Size} </td>\                      
                        <td> ${formattedCreatedDate} </td>\
                        <td>\                            
                            <button title="Editar" data-id="${item.Id}" class="btn btn-warning my-sm-1 edit-attention" data-url="/Attention/${item.Action + '/' + item.Id}" > <i class="fas fa-pen fa-sm"></i></button>\
                            <button title="Recibo" data-id="${item.Id}" class="btn btn-success my-sm-1 receipt-attention" data-url="/Attention/InvoiceTest/${item.Id}" > <i class="fas fa-receipt fa-sm"></i></button>\
                            <button title="Pagar" data-id="${item.Id}" class="btn btn-primary my-sm-1 invoice-attention" data-url="/Attention/Invoice/${item.Id}" > <i class="fas fa-file-invoice fa-sm"></i></button>\
                        </td>\
                    </tr>\
                `)
            })
            addActionsAttention()   
            $('#dataTable').DataTable({
                order : [[7, 'desc']],
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

    document.getElementById('dateBegin').valueAsDate = new Date();
    document.getElementById('dateEnd').valueAsDate = new Date();

    $('.finpenfing').click(function () {
        getAllAttentions()
    });
    $("#cboFiltro").change(function () {
        getAllAttentions();
    });
    getAllAttentions()
    //addActionsAttention()

});






