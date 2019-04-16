$(document).ready(function () {

    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg !== value;
    }, "Value must not equal arg.");


    $('#AddOrEditFormID').validate({       
        errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
        errorElement: 'div',
        errorPlacement: function (error, e) {
            e.parents('.form-group > div').append(error);
        },
        highlight: function (e) {

            $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
            $(e).closest('.help-block').remove();
        },
        success: function (e) {
            e.closest('.form-group').removeClass('has-success has-error');
            e.closest('.help-block').remove();
        },
        rules: {
            'FirstName': {
                required: true
            },
            'LastName': {
                required: true
            },
            'DocumentType': {
                valueNotEquals: "00"
            },
            'Document': {
                required: true
            },
            'Gender': {
                valueNotEquals: "0"
            },
            'BirthDate': {
                required: true
            }
        },
        messages: {
            'FirstName': {
                required: 'Por favor, ingrese un nombre'
            },
            'LastName': {
                required: 'Por favor, ingrese un apellido'
            },
            'DocumentType': {
                valueNotEquals: 'Por favor, seleccione una tipo de documento'
            },
            'Document': {
                required: 'Por favor, ingrese un documento'
            },
            'Gender': {
                valueNotEquals: 'Por favor, seleccione un género'
            },
            'BirthDate': {
                required: 'Por favor, ingrese una fecha de nacimiento'
            }
        },
        submitHandler: function (form) {

            var posting = $.post("../Patient/AddOrEdit", $(form).serialize());
            posting.done(function (data) {
                getAllPatients()
                $("#ModalPatientAddOrEdit").modal('hide')
                //Swal.fire(
                //    'Paciente Registrado!',
                //    'Paciente registrado satisfactoriamente.'
                //)
                let notifier = new Notifier();
                notifier.success('Paciente registrado satisfactoriamente.','Paciente Registrado!');
            });
          
            return false; 
        }
    });
});

