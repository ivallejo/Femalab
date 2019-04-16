
var postData = [
    
]


function clearInputs() {
    $("#Age").val("")
    $("#Weight").val("")
    $("#Size").val("")
    $("#Height").val("")
}

function getAllProducts() {
    $.ajax({
        type: "POST",
        url: "../Attention/GetProductAll",
        success: function (data) { 

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
                    }
                },
            };

            $("#txtCodeProduct").easyAutocomplete(optionsExamCode);
            $("#txtProduct").easyAutocomplete(optionsExams);

            $(".easy-autocomplete").removeAttr("style");
        }
    });
}

function addProduct() {
    $('.addProduct').click(function () {
        let id = $("#txtCodeProduct").attr('data-id')

        let obj = postData.find(x => x.Id == id);
        if (obj == undefined) {
       
            let code = $("#txtCodeProduct").val()
            let exam = $("#txtProduct").val()
            let type = $("#txtTypeProduct").val()
            let price = $("#txtPriceProduct").val()

            postData.push({Id : id,Quantity : 1,Price : price})


            $("#tbProductBody").append(`\
                        <tr id="tr-${id}" data-id="${id}">\                
                            <td> ${code} </td>\
                            <td> ${exam} </td>\
                            <td> ${type} </td>\
                            <td> 1 </td>\
                            <td> ${price} </td>\
                            <td>\
                               <button title="Eliminar" data-id="${id}" class="btn btn-danger my-sm-1 removeProduct"> <i class="fas fa-trash"></i></button>\
                            </td>\
                        </tr>\
                        `)

            removeProduct()

        }

    })
}

function removeProduct() {
    $('.removeProduct').click(function () {

        
        let tr = $(this).parent().parent()
        let id = $(tr).attr('data-id')

        for (var i = 0; i < postData.length; i++) {
            if (postData[i].Id == id) {
                postData.splice(i, 1);
                break
            }
        }

        $(tr).remove()
    })
}


function addExist(Id) {
    let _id = postData.find(x => x.Id === Id).Id;
    return _id
}


$(document).ready(function () {
    clearInputs()
    getAllProducts()
    addProduct()
});
