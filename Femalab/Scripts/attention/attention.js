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
            'Patient.FirstName': {
                required: true
            },
            'Patient.LastName': {
                required: true
            },
            'Patient.DocumentType': {
                valueNotEquals: "00"
            },
            'Patient.Document': {
                required: true
            },
            'Patient.Gender': {
                valueNotEquals: "0"
            },
            'Patient.BirthDate': {
                required: true,
                valueNotEquals: "01/01/0001"
            },
            'AttentionTypeId': {
                valueNotEquals: "0"
            },
            'DoctorId': {
                valueNotEquals: "0"
            }
        },
        messages: {            
            'Patient.FirstName': {
                required: 'Por favor, ingrese un nombre'
            },
            'Patient.LastName': {
                required: 'Por favor, ingrese un apellido'
            },
            'Patient.DocumentType': {
                valueNotEquals: 'Por favor, seleccione una tipo de documento'
            },
            'Patient.Document': {
                required: 'Por favor, ingrese un documento'
            },
            'Patient.Gender': {
                valueNotEquals: 'Por favor, seleccione un género'
            },
            'Patient.BirthDate': {
                required: 'Por favor, ingrese una fecha de nacimiento',
                valueNotEquals: 'Por favor, ingrese una fecha de nacimiento'
            },
            'AttentionTypeId': {
                valueNotEquals: 'Por favor, seleccione un tipo de consulta'
            },
            'DoctorId': {
                valueNotEquals: 'Por favor, seleccione un médico'
            }
        },
        submitHandler: function (form) {            
            var posting = $.post("../Attention/Attention", $(form).serialize());
            posting.done(function (data) {                
                $("#ModalAttentionAddOrEdit").modal('hide')                
                let notifier = new Notifier();  
                switch (data.response) {
                    case 0: {
                        notifier.error('Comuniquese con el administrador.', 'Error!');
                        break
                    }
                    case 1: {
                        notifier.success('Atención registrada satisfactoriamente.', 'Atención Registrada!');
                        getAllAttentions()
                        break
                    }
                    case 2: {

                        const { Id, Document, LastName, FirstName, TypeTag, Type } = data
                        const tr = document.getElementById(`tr-${Id}`)                       
                        const _type = tr.children[2].children[0].children[0].classList
                        
                        tr.children[0].innerHTML = Document
                        tr.children[1].innerHTML = LastName + ' ' + FirstName                        
                        tr.children[2].children[0].children[0].innerHTML = Type
                        _type.remove(_type[1])
                        _type.add(`badge-${TypeTag}`)

                        notifier.info('Atención actualizada satisfactoriamente.', 'Atención Actualizada!');
                        break
                    }
                }                
            });
            return false;
        }
    });
});

