$(document).ready(function () {
    $('#tblCustomers').DataTable({
        "type": "GET",
        "dataType": "json",
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0",
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
    });
})

function AddEditCustomers(itemId) {
    var url = "/Customers/AddEditCustomers?itemId=" + itemId;
    if (itemId > 0)
        document.getElementById("title").innerHTML = "Delete Customer";
    else
        document.getElementById("title").innerHTML = "Add Customer";

    document.getElementById("backdrop-" + itemId).style.display = "block";
    document.getElementById("customerFormModel-" + itemId).style.display = "block";
    document.getElementById("customerFormModel-" + itemId).classList.add("show");
}

function onCloseModal(itemId) {
    document.getElementById("backdrop-" + itemId).style.display = "none";
    document.getElementById("customerFormModel-" + itemId).style.display = "none";
    document.getElementById("customerFormModel-" + itemId).classList.remove("show");
}

let DeleteCustomer = function (itemId) {
    debugger;
    let _id = itemId;
    Swal.fire({
        "icon": "warning",
        "title": "Do you want to delete item with Item Id: " + _id,
        "text": "¡Si no lo está puede cancelar la accíón!",
        "inputValue": _id,
        "showCancelButton": true,
        "confirmButtonColor": '#3085d6',
        "cancelButtonColor": '#d33',
        "cancelButtonText": 'Cancel',
        "confirmButtonText": 'Yes, delete Register!'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger;
            $.ajax({
                type: "POST",
                url: "/Customers/Delete/" + _id,
                success: function () {
                    Swal.fire({
                        "icon": "success",
                        "type": "success",
                        "title": "Registro Borrado Correctamente!!!",
                        "showConfirmButton": true,
                        "confirmButtonText": "Ok"
                    }).then(function (result) {
                        if (result.value) {
                            $("#myModal").modal("hide");
                            window.location.href = "/Customers/Index";
                        }
                    });
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            })
            window.location.href = "/Customers/Index";
        }
    })
}

// Add the following code if you want the name of the file appear on select
$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    $("#Imagen").val(fileName);
});