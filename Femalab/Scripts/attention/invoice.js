var arrPagos = [

]

function addPay() {
    $('.addPay').click(function () {
        debugger
        let txtEfectivo = $('#txtEfectivo').val()
        let txtTarjeta = $('#txtTarjeta').val()

        if (txtEfectivo != '' && txtEfectivo != '0.00') {
            
            let price = parseFloat(txtEfectivo).toFixed(2)

            if (validateAddImporte(price)) {
                let type = 'Efectivo'
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
                let type = 'Tarjeta'

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
                $('#txtTarjeta').val(parseFloat(0).toFixed(2))
            }
        }
    })
}

function validateAddImporte(importe) {

    let totalInvoice = parseFloat($('#hddTotal').val())
    let totalPagos = arrPagos.reduce(function (a, b) {
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

    let totalInvoice = parseFloat($('#hddTotal').val())

    let totalPagos = arrPagos.reduce(function (a, b) {
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

        for (var i = 0; i < arrPagos.length; i++) {
            if (arrPagos[i].Type == type) {
                arrPagos.splice(i, 1);
                break
            }
        }

        $(tr).remove()
    })
}

$(document).ready(function () {

    //$('#card-pay').height($('#card-invoice').height())
    //setTimeout(function () { $('#card-pay').height($('#card-invoice').height()); }, 300);
    addPay()

});
    