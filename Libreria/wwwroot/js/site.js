function showAlert(type, message) {
    return Swal.fire({
        title: type === 'success' ? '¡Éxito!' : 'Error',
        text: message,
        icon: type,
        confirmButtonText: 'Aceptar'
    });
}

function handleFormSubmit(formSelector, redirectUrl) {
    $(document).on('submit', formSelector, function (event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response.success) {
                    showAlert('success', response.message).then(() => {
                        window.location.href = redirectUrl;
                    });
                } else {
                    showAlert('error', response.message);
                }
            },
            error: function (xhr, status, error) {
                showAlert('error', 'Ocurrió un problema al procesar el formulario: ' + error);
            }
        });
    });
}


function confirmDelete(url, element) {
    const token = $('input[name="__RequestVerificationToken"]').val();

    Swal.fire({
        title: '¿Estás seguro?',
        text: "¡No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                headers: {
                    'RequestVerificationToken': token
                },
                success: function (response) {
                    if (response.success) {
                        showAlert('success', 'El registro ha sido eliminado.');
                        $(element).closest('tr').remove();
                    } else {
                        showAlert('error', response.message);
                    }
                },
                error: function (xhr, status, error) {
                    showAlert('error', 'Ocurrió un problema al eliminar el registro: ' + error);
                }
            });
        }
    });
}

function createDataTable(id) {
    new DataTable('#' + id, {
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                text: '<i class="fi fi-ss-duplicate"></i>',
                className: 'botonCopiar btn'
            },
            {
                extend: 'csv',
                text: '<i class="fi fi-ss-file-csv"></i>',
                className: 'botonCsv btn'
            },
            {
                extend: 'excel',
                text: '<i class="fi fi-ss-file-excel"></i>',
                className: 'botonExcel btn'
            },
            {
                extend: 'pdf',
                text: '<i class="fi fi-ss-file-pdf"></i>',
                className: 'botonPdf btn',
                exportOptions: {
                    columns: ':not(:last-child)' // Excluye la última columna
                }
            },
            {
                extend: 'print',
                text: '<i class="fi fi-ss-print"></i>',
                className: 'botonPrint btn',
                exportOptions: {
                    columns: ':not(:last-child)' // Excluye la última columna
                }
            },
            {
                extend: 'colvis',
                text: 'Filtrar',
                className: 'botonColvis btn'
            }
        ],
        scrollX: true,
        initComplete: function (json, settings) {
            $(".dt-buttons").removeClass("dt-buttons");
            //agregar clase para añadir estilos
            $(".dt-button").addClass("botones");
            //Elimina la clase de los estilos por defecto de tadatables
            $(".dt-button").addClass("botones");
        }
    });
    // Integrar el buscador con DataTables 
    $('#searchInput').on('keyup', function () {
        table.search(this.value).draw();
    });
}
