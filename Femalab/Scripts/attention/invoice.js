var arrPagos = [

]

function addPay() {
    $('.addPay').click(function () {
        let txtEfectivo = $('#txtEfectivo').val()
        let txtTarjeta = $('#txtTarjeta').val()

        if (txtEfectivo != '' && txtEfectivo != '0.00') {

            let price = parseFloat(txtEfectivo).toFixed(2)

            if (validateAddImporte(price)) {
                let type = 'E'
                arrPagos.push({ Type: type, Price: price })

                $("#tbPayBody").append(`\
                        <tr data-id="${type}">\                
                            <td> ${type} </td>\
                            <td> ${price} </td>\
                            <td>\
                               <button title="Eliminar" class="btn btn-danger my-sm-1 removePay"> <i class="fas fa-trash"></i></button>\
                            </td>\
                        </tr>\
                        `)

                removePay()
                $('#txtEfectivo').val(parseFloat(0).toFixed(2))
            }

        } else if (txtTarjeta != '' && txtTarjeta != '0.00') {

            let price = parseFloat(txtTarjeta).toFixed(2)
            if (validateAddImporte(price)) {
                let type = 'T'

                arrPagos.push({ Type: type, Price: price })

                $("#tbPayBody").append(`\
                        <tr data-id="${type}">\                
                            <td> ${type} </td>\
                            <td> ${price} </td>\
                            <td>\
                               <button type="button" title="Eliminar" class="btn btn-danger my-sm-1 removePay"> <i class="fas fa-trash"></i></button>\
                            </td>\
                        </tr>\
                        `)

                removePay()
                $('#txtTarjeta').val(parseFloat(0).toFixed(2))
            }
        }
    })
}

function validateAddImporte(importe) {
    
    var arrPayments = []
    var table = document.getElementById('tbPayBody');
    var rowLength = table.rows.length;

    for (var i = 0; i < rowLength; i += 1) {
        let price = parseFloat(table.rows[i].cells[1].innerHTML.trim()).toFixed(2)
        arrPayments.push({ Price: price })
    }

    let totalInvoice = parseFloat($('#hddTotal').val())
    let totalPagos = arrPayments.reduce(function (a, b) {
        return parseFloat(a) + parseFloat(b.Price);
    }, 0)
    totalPagos = totalPagos + parseFloat(importe)

    if (totalInvoice >= totalPagos) {
        return true
    } else {
        return false
    }

}

function validateImporteTotal() {
    
    var arrPayments = []
    var table = document.getElementById('tbPayBody');
    var rowLength = table.rows.length;

        for (var i = 0; i < rowLength; i += 1) {
            let price = parseFloat(table.rows[i].cells[1].innerHTML.trim()).toFixed(2)
            arrPayments.push({ Price: price })
        }

    let totalInvoice = parseFloat($('#hddTotal').val())

    let totalPagos = arrPayments.reduce(function (a, b) {
        return parseFloat(a) + parseFloat(b.Price);
    }, 0);

    if (totalInvoice == totalPagos) {
        $('.addPay').prop("disabled", true)
    } else {
        $('.addPay').prop("disabled", false)
    }

}

function removePay() {
    $('.removePay').click(function () {
        let tr = $(this).parent().parent()
        let type = $(tr).attr('data-id')
        let divType = $("#" + type)
        if (divType !== null) { $(divType.children()[4]).val(false) }
        for (var i = 0; i < arrPagos.length; i++) {
            if (arrPagos[i].Type == type) {
                arrPagos.splice(i, 1);
                break
            }
        }
        $(tr).remove()
    })
}

function ChangeInvoice() {
    $('#VoucherType').on('change', function () {
        var voucherType = $(this).val()
        $('#Series').empty();
        if (voucherType == '01') {
            $('#Series')
                .append($("<option></option>")
                    .attr("value", "01")
                    .text("F001"));
        } else {
            $("#Series").append('<option value="03">B001</option>');
        }
    });
}

function ChangeUbigeo() {

    $('#Customer_Department').on('change', function () {
        var department = $(this).val()
        $('#Customer_Province').empty();
        $('#Customer_District').empty();

        var posting = $.post("../Attention/GetProvince", { codeDepartment: department });
        posting
            .done(function (data) {
                $.each(data, function (key, registro) {
                    $("#Customer_Province").append('<option value=' + registro.Value + '>' + registro.Text + '</option>');
                });    
                $('#Customer_Province').change()
            })
            .fail(function () {
                const notifier = new Notifier();
                notifier.error('Departamento no encontrado.', 'Departamento no encontrado!');
            })
    });

    $('#Customer_Province').on('change', function () {
        var department = $('#Customer_Department').val() + $('#Customer_Province').val() 
        $('#Customer_District').empty();

        var posting = $.post("../Attention/GetDistrict", { codeDepartmentProvince: department });
        posting
            .done(function (data) {
                $.each(data, function (key, registro) {
                    $("#Customer_District").append('<option value=' + registro.Value + '>' + registro.Text + '</option>');
                });
            })
            .fail(function () {
                const notifier = new Notifier();
                notifier.error('Provincia no encontrada.', 'Departamento no encontrada!');
            })
    });
}

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
            'Customer.Document': {
                required: true
            },
            'Customer.FirstName': {
                required: true
            }
        },
        messages: {
            'Customer.Document': {
                required: 'Por favor, ingrese un documento'
            },
            'Customer.FirstName': {
                required: 'PPor favor, ingrese un nombre'
            }
        },
        submitHandler: function (form) {
            $(".loading").show()
            if ($("#Customer.Document").val() == "") $("#Customer.Document").val("")
            if ($("#Customer.FirstName").val() == "") $("#Customer.FirstName").val("")
            if ($("#Customer.Address").val() == "") $("#Customer.Address").val("")
            if ($("#Customer.Phone").val() == "") $("#Customer.Phone").val("")
            if ($("#Customer.Email").val() == "") $("#Customer.Email").val("")

            let invoiceId = $("#Id").val()
            let iPayments = $("#iPayments").val()
            let i;
            for (i = 0; i < arrPagos.length; i++) {
                createElement(iPayments, invoiceId, arrPagos[i].Type, arrPagos[i].Price)
                iPayments++;
            }

            var posting = $.post("../Attention/Invoice", $(form).serialize());
            posting.done(function (data) {
                $("#ModalAttentionAddOrEdit").modal('hide')
                let notifier = new Notifier();
                switch (data.response) {
                    case "0": {
                        notifier.success('Factura registrada satisfactoriamente.', 'Factura Registrada!');
                        notifier.error('Error al comunicarse con SUNAT.', 'Error!');
                        getAllAttentions()
                        break
                    }
                    case "1": {
                        notifier.success('Factura registrada satisfactoriamente.', 'Factura Registrada!');
                        getAllAttentions()
                        break
                    }
                    case "2": {
                        notifier.success('Factura actualizada satisfactoriamente.', 'Factura Actualizada!');
                        getAllAttentions()
                        break
                    }                      
                }
            });

            return false;
        }
    });

    removePay()
    addPay()
    ChangeInvoice()
    ChangeUbigeo()
});




function createElement(Index, _InvoiceId, _PaymentType, _Amount) {

    var InvoiceId = document.createElement("input");
    InvoiceId.setAttribute("type", "hidden");
    InvoiceId.setAttribute("name", `Payments[${Index}].InvoiceId`);
    InvoiceId.setAttribute("value", `${_InvoiceId}`)

    var PaymentType = document.createElement("input");
    PaymentType.setAttribute("type", "hidden");
    PaymentType.setAttribute("name", `Payments[${Index}].PaymentType`);
    PaymentType.setAttribute("value", `${_PaymentType}`)

    var Amount = document.createElement("input");
    Amount.setAttribute("type", "hidden");
    Amount.setAttribute("name", `Payments[${Index}].Amount`);
    Amount.setAttribute("value", `${_Amount}`)

    var State = document.createElement("input");
    State.setAttribute("type", "hidden");
    State.setAttribute("name", `Payments[${Index}].State`);
    State.setAttribute("value", "true")

    

    document.getElementById("divPayments").appendChild(InvoiceId);
    document.getElementById("divPayments").appendChild(PaymentType);
    document.getElementById("divPayments").appendChild(Amount);
    document.getElementById("divPayments").appendChild(State);
    
}
