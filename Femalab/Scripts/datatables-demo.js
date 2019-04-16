
function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function addActionsPatients() {
    $('.details-patient').click(function () {
        var url = $('#ModalPatientAddOrEdit').data('url') + '/' + $(this).data("id")
        $.get(url, function (data) {
            $("#ModalPatientAddOrEdit").html(data)
            $("#ModalPatientAddOrEdit").modal('show')
        });
    });
    $('.remove-patient').click(function () {
        
        Swal.fire({
            title: 'Esta Seguro de eliminar al paciente?',
            text: "No podr\u00E1 revertir la acci\u00F3n!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'S\u00ED, Eliminar!',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.value) {
                var url = $('#hddUrlRemovePatient').val()
                var posting = $.post(url, { id: $(this).data("id") });
                posting.done(function (data) {
                    GetAllPatients()
                    Swal.fire(
                        'Paciente Eliminado!',
                        'Paciente eliminado satisfactoriamente.'
                    )
                });
            }
        })
    });
}

function GetAllPatients() {

    $.ajax({
        type: "POST",
        url: "../Patient/GetAll",
        success: function (data) {

            $("#tbPatientBody").empty()

            $.each(data, function (i, item) {
                let birthDate = new Date(parseInt(item.BirthDate.replace("/Date(", "").replace(")/", ""), 10))
                let createdDate = new Date(parseInt(item.CreatedDate.replace("/Date(", "").replace(")/", ""), 10))
                let formattedBirthDate = birthDate.getFullYear() + "-" + pad((birthDate.getMonth() + 1), 2) + "-" + pad(birthDate.getDate(),2) + " "
                let formattedCreatedDate = createdDate.getFullYear() + "-" + pad((createdDate.getMonth() + 1), 2) + "-" + pad(createdDate.getDate(), 2) + " " + pad(createdDate.getHours(), 2) + ":" + pad(createdDate.getMinutes(), 2) + ":" + pad(createdDate.getSeconds(), 2)

                $("#tbPatientBody").append(`<tr><td> ${item.Document} </td><td> ${((item.Gender == 'M') ? '<i class="fas fa-mars"></i>' : '<i class="fas fa-venus"></i>')} </td><td> ${item.LastName + ' ' + item.FirstName} </td><td> ${formattedBirthDate} </td><td> ${formattedCreatedDate} </td><td><button title="Detalle" data-id="${item.Id}" class="btn btn-success mr-2 details-patient"> <i class="fas fa-info-circle"></i></button><button title="Editar" data-id="${item.Id}" class="btn btn-warning mr-2 details-patient"> <i class="fas fa-user-edit"></i></button><button title="Eliminar" data-id="${item.Id}" class="btn btn-danger mr-2 remove-patient"> <i class="fas fa-trash"></i></button></td></tr>`)                

            })

            addActionsPatients()

            $('#dataTable').DataTable()
        }
    });
}

// Call the dataTables jQuery plugin
$(document).ready(function() {
  
    GetAllPatients()

});

