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
            'Code': {
                required: true
            },
            'CategoryId': {
                valueNotEquals: "-1"
            },
            'SpecialtyId': {
                valueNotEquals: "-1"
            },
            'Description': {
                required: true
            },
            'Price': {
                valueNotEquals: "0.00"
            }
        },
        messages: {
            'Code': {
                required: 'Por favor, ingrese un código'
            },
            'CategoryId': {
                required: 'Por favor, seleccione una categoría'
            },
            'SpecialtyId': {
                valueNotEquals: 'Por favor, seleccione una especialidad'
            },
            'Description': {
                required: 'Por favor, ingrese una descripción'
            },
            'Price': {
                valueNotEquals: 'Por favor, ingrese un precio'
            }
        },
        submitHandler: function (form) {

            var posting = $.post("../Product/AddOrEdit", $(form).serialize());
            posting.done(function (data) {
                getAllProducts()
                $("#ModalProductAddOrEdit").modal('hide')
                let notifier = new Notifier();
                notifier.success('Producto registrado satisfactoriamente.','Producto Registrado!');
            });
          
            return false; 
        }
    });
});

