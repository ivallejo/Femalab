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


$("#Patient_Document").focusout( function (e) {
    getPerson()
});

function getPerson() {    

    var posting = $.post("../Attention/GetPerson", { Id: $("#Patient_Document").val() } );

    posting
        .done(function (data) {

            $("#Patient_DocumentType").val("01")
            $("#Patient_FirstName").val(`${data.NOMBRES}`)
            $("#Patient_LastName").val(`${data.APE_PATERNO} ${data.APE_MATERNO}`)
            $("#Patient_Gender").val(`${(data.SEXO == '2') ? 'F' : 'M'}`)
            
            const ano = data.FECHA_NACIMIENTO.substring(0, 4);
            const mes = data.FECHA_NACIMIENTO.substring(4, 6);
            const dia = data.FECHA_NACIMIENTO.substring(6, 8);
                       
            var today = ano + "-" + mes + "-" + dia;

            $('#Patient_BirthDate').attr("value", today);

            const birthday = new Date($('#Patient_BirthDate').val());

            $("#Age").val(_calculateAge(birthday))

            const notifier = new Notifier();
            notifier.success('Paciente encontrado.', 'Paciente encontrado!');
        })
        .fail(function () {

            $("#Patient_DocumentType").val("01")
            $("#Patient_FirstName").val("")
            $("#Patient_LastName").val("")
            $("#Patient_Gender").val("0")

            let today = new Date();
            const dd = ("0" + (today.getDate())).slice(-2);
            const mm = ("0" + (today.getMonth() + 1)).slice(-2);
            const yyyy = today.getFullYear();
            today = yyyy + '-' + mm + '-' + dd;

            $('#Patient_BirthDate').attr("value", today);
            $("#Age").val("")

            const notifier = new Notifier();
            notifier.error('Paciente no encontrado.', 'Paciente no encontrado!');       
        })       
}

function _calculateAge(birthday) { // birthday is a date
    var ageDifMs = Date.now() - birthday.getTime();
    var ageDate = new Date(ageDifMs); // miliseconds from epoch
    return Math.abs(ageDate.getUTCFullYear() - 1970);
}
