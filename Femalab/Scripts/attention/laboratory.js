
var postData = [
    
]

var arrProducts = [

]

function clearInputs() {

    if ($("#Age").val() == "0") $("#Age").val("") 
    if ($("#Weight").val() == "0.00") $("#Weight").val("")
    if ($("#Size").val() == "0.00") $("#Size").val("")
    if ($("#Height").val() == "0.00") $("#Height").val("")

    if ($('#Patient_BirthDate').val() == '0001-01-01') {
        document.getElementById('Patient_BirthDate').valueAsDate = new Date();
    } else {
        const birthday = new Date($('#Patient_BirthDate').val());
        $("#Age").val(_calculateAge(birthday))
    }


    

}

function addPosData() {

   
    $($("#tbProductBody").children()).each(function (index,element) {
        
        let id = $(element).data("id")
        let price = $(element).children()[4].innerHTML
        let discount = $(element).children()[5].innerHTML
        let _import = $(element).children()[6].innerHTML

        postData.push({ ProductId: id, Quantity: 1, Price: price, Discount: discount, Import: _import })
    })

}

function getAllProducts() {
    $.ajax({
        type: "POST",
        url: "../Attention/GetProductAll",
        success: function (data) { 

            arrProducts = data;

            let optionsExamCode = {
                data: data,
                getValue: "Code",
                list: {
                    match: {
                        enabled: true
                    },
                    onChooseEvent: function () {
                        const product = $("#txtCodeProduct").getSelectedItemData()
                        const Description = product.Description;
                        const Specialty = product.Specialty;
                        const Price = product.Price.toFixed(2);
                        $("#txtCodeProduct").data("id", product.Id)
                        $("#txtProduct").val(Description).trigger("change");
                        $("#txtTypeProduct").val(Specialty).trigger("change");
                        $("#txtPriceProduct").val(Price).trigger("change");
                        $("#txtDiscountProduct").val("0.00").trigger("change");
                        $("#txtImportProduct").val(Price).trigger("change");
                    }
                },
            };

            let optionsExams = {
                data: data,
                getValue: "Description",
                list: {
                    match: {
                        enabled: true
                    },
                    onChooseEvent: function () {
                        let product = $("#txtProduct").getSelectedItemData()
                        let Code = product.Code;
                        let Specialty = product.Specialty;
                        let Price = product.Price.toFixed(2);
                        $("#txtCodeProduct").attr('data-id', product.Id).trigger("change");
                        $("#txtCodeProduct").val(Code).trigger("change");
                        $("#txtTypeProduct").val(Specialty).trigger("change");
                        $("#txtPriceProduct").val(Price).trigger("change");
                        $("#txtDiscountProduct").val("0.00").trigger("change");
                        $("#txtImportProduct").val(Price).trigger("change");
                    }
                },
            };

            $("#txtCodeProduct").easyAutocomplete(optionsExamCode);
            $("#txtProduct").easyAutocomplete(optionsExams);

            $(".easy-autocomplete").removeAttr("style");
        }
    });
}

function addService() {
    $('.addService').click(function () {
        
        let id = $(this).attr('data-id')

        let obj = postData.find(x => x.ProductId == id);
        if (obj == undefined) {

            obj = arrProducts.find(x => x.Id == id);
            let code = obj.Code
            let exam = obj.Description
            let type = obj.Specialty
            let price = obj.Price.toFixed(2)
            let discount = obj.Discount.toFixed(2)
            let _import = obj.Import.toFixed(2)

            postData.push({ ProductId: id, Quantity: 1, Price: price, Discount: discount, Import: _import })


            $("#tbProductBody").append(`\
                        <tr id="tr-${id}" data-id="${id}">\                
                            <td> ${code} </td>\
                            <td> ${exam} </td>\
                            <td> ${type} </td>\
                            <td> 1 </td>\
                            <td> ${price} </td>\
                            <td> ${discount} </td>\
                            <td> ${_import} </td>\
                            <td>\
                               <button title="Eliminar" data-id="${id}" class="btn btn-danger my-sm-1 removeProduct"> <i class="fas fa-trash"></i></button>\
                            </td>\
                        </tr>\
                        `)

            removeProduct()

        }

    })
}

function addProduct() {
    $('.addProduct').click(function () {
        let id = $("#txtCodeProduct").attr('data-id')

        let obj = postData.find(x => x.ProductId == id);
        if (obj == undefined) {
       
            let code = $("#txtCodeProduct").val()
            let exam = $("#txtProduct").val()
            let type = $("#txtTypeProduct").val()
            let price = $("#txtPriceProduct").val()
            let discount = $("#txtDiscountProduct").val()
            let _import = $("#txtImportProduct").val()

            postData.push({ ProductId: id, Quantity: 1, Price: price, Discount: discount, Import: _import})


            $("#tbProductBody").append(`\
                        <tr id="tr-${id}" data-id="${id}">\                
                            <td> ${code} </td>\
                            <td> ${exam} </td>\
                            <td> ${type} </td>\
                            <td> 1 </td>\
                            <td> ${price} </td>\
                            <td> ${discount} </td>\
                            <td> ${_import} </td>\
                            <td>\
                               <button title="Eliminar" data-id="${id}" class="btn btn-danger my-sm-1 removeProduct"> <i class="fas fa-trash"></i></button>\
                            </td>\
                        </tr>\
                        `)

            removeProduct()

            $("#txtCodeProduct").val("")
            $("#txtProduct").val("")
            $("#txtTypeProduct").val("")
            $("#txtPriceProduct").val("0.00")
            $("#txtDiscountProduct").val("0.00")
            $("#txtImportProduct").val("0.00")
        }

    })
}

function removeProduct() {
    $('.removeProduct').click(function () {

        
        let tr = $(this).parent().parent()
        let id = $(tr).attr('data-id')

        for (var i = 0; i < postData.length; i++) {
            if (postData[i].ProductId == id) {
                postData.splice(i, 1);
                break
            }
        }

        $(tr).remove()
    })
}

function addExist(Id) {
    let _id = postData.find(x => x.ProductId === Id).Id;
    return _id
}

$("#Patient_Document").focusout(function (e) {
    getPerson()
});

$("#Patient_BirthDate").focusout(function (e) {
    clearInputs()
});

$("#txtDiscountProduct").focusout(function (e) {
    var price = $("#txtPriceProduct").val()
    var discount = $("#txtDiscountProduct").val()

    var price_discount = price * discount
    var _import = price - price_discount
    $("#txtImportProduct").val(_import.toFixed(2))
    //var price = $("#txtDiscountProduct").val()

});

function getPerson() {

    var posting = $.post("../Attention/GetPerson", { Id: $("#Patient_Document").val() });

    posting
        .done(function (data) {
            
            $("#PatientId").val(data.ID)
            $("#Patient_Id").val(data.ID)
            $("#Patient_DocumentType").val("01")
            $("#Patient_FirstName").val(`${data.NOMBRES}`)
            $("#Patient_LastName").val(`${data.APE_PATERNO} ${data.APE_MATERNO}`)
            $("#Patient_Gender").val(`${(data.SEXO == '2') ? 'F' : 'M'}`)
            $("#Patient_Address").val(`${data.Address}`)
            $("#Patient_Email").val(`${data.Email}`)
            $("#Patient_Phone").val(`${data.Phone}`)
            $("#Weight").val(`${data.Weight}`)
            $("#Size").val(`${data.Size}`)

            const ano = data.FECHA_NACIMIENTO.substring(0, 4);
            const mes = data.FECHA_NACIMIENTO.substring(4, 6);
            const dia = data.FECHA_NACIMIENTO.substring(6, 8);

            var today = ano + "-" + mes + "-" + dia;

            
            //$('#Patient_BirthDate').attr("value", today);
            $('#Patient_BirthDate').val(today);
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
            $("#Patient_Address").val("")
            $("#Patient_Email").val("")
            $("#Patient_Phone").val("")
            $("#Weight").val("")
            $("#Size").val("")

            let today = new Date();
            const dd = ("0" + (today.getDate())).slice(-2);
            const mm = ("0" + (today.getMonth() + 1)).slice(-2);
            const yyyy = today.getFullYear();
            today = yyyy + '-' + mm + '-' + dd;

            $('#Patient_BirthDate').val(today);
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
            //'Patient.DocumentType': {
            //    valueNotEquals: "00"
            //},
            //'Patient.Document': {
            //    required: true
            //},
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
            //'Patient.DocumentType': {
            //    valueNotEquals: 'Por favor, seleccione una tipo de documento'
            //},
            //'Patient.Document': {
            //    required: 'Por favor, ingrese un documento'
            //},
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

            if ($("#Age").val() == "") $("#Age").val("0")
            if ($("#Weight").val() == "") $("#Weight").val("0.00")
            if ($("#Size").val() == "") $("#Size").val("0.00")
            if ($("#Height").val() == "") $("#Height").val("0.00")

            let i;
            for (i = 0; i < postData.length; i++) {
                createElement(i, postData[i].ProductId, postData[i].Quantity, postData[i].Price, postData[i].Discount , postData[i].Import)
            }

            var posting = $.post("../Attention/Laboratory", $(form).serialize() );
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

                        const { Id, Document, LastName, FirstName, TypeTag, Type, Gender, Age, Weight, Size } = data
                        const tr = document.getElementById(`tr-${Id}`)
                        //const _type = tr.children[2].children[0].children[0].classList

                        tr.children[0].innerHTML = Document
                        tr.children[1].innerHTML = LastName + ' ' + FirstName
                        tr.children[2].innerHTML = (Gender == 'M') ? '<i class="fas fa-mars fa-2x text-primary"></i>' : '<i class="fas fa-2x fa-venus text-danger"></i>'
                        tr.children[3].innerHTML = Age
                        tr.children[4].innerHTML = Weight
                        tr.children[5].innerHTML = Size
                        //tr.children[2].children[0].children[0].innerHTML = Type
                        //_type.remove(_type[1])
                        //_type.add(`badge-${TypeTag}`)

                        notifier.info('Atención actualizada satisfactoriamente.', 'Atención Actualizada!');
                        break
                    }
                }
            });
            return false;
        }
    });

    clearInputs()
    getAllProducts()
    addProduct()
    addService()
    removeProduct()
    addPosData()

});


function createElement(Index, _ProductId, _Quantity, _Price, _Discount, _Import) {
       
    var ProductId = document.createElement("input");
    ProductId.setAttribute("type", "hidden");
    ProductId.setAttribute("name", `AttentionDetails[${Index}].ProductId`);
    ProductId.setAttribute("value", `${_ProductId}`)

    var Quantity = document.createElement("input");
    Quantity.setAttribute("type", "hidden");
    Quantity.setAttribute("name", `AttentionDetails[${Index}].Quantity`);
    Quantity.setAttribute("value", `${_Quantity}`)

    var Price = document.createElement("input");
    Price.setAttribute("type", "hidden");
    Price.setAttribute("name", `AttentionDetails[${Index}].Price`);
    Price.setAttribute("value", `${_Price}`)

    var Discount = document.createElement("input");
    Discount.setAttribute("type", "hidden");
    Discount.setAttribute("name", `AttentionDetails[${Index}].Discount`);
    Discount.setAttribute("value", `${_Discount}`)

    var Import = document.createElement("input");
    Import.setAttribute("type", "hidden");
    Import.setAttribute("name", `AttentionDetails[${Index}].Import`);
    Import.setAttribute("value", `${_Import}`)

    document.getElementById("divAttentionDetails").appendChild(ProductId);
    document.getElementById("divAttentionDetails").appendChild(Quantity);
    document.getElementById("divAttentionDetails").appendChild(Price);
    document.getElementById("divAttentionDetails").appendChild(Discount);
    document.getElementById("divAttentionDetails").appendChild(Import);


}