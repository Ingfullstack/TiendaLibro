let datatable;

$(document).ready(function() {
    CargarTablas();
});

const CargarTablas = () => {
    datatable = $("#tblDatos").DataTable({
        "ajax": {
            "url": "/Admin/Almacen/ObtenerTodos"
        },
        "columns": [
            { "data": "nombre"},
            { "data": "descripcion"},
            {
                "data": "estado",
                "render": function (estado) {
                    if (estado) {
                        return `<span class="badge text-bg-success">Activo</span>`;
                    } else {
                        return `<span class="badge text-bg-danger">Inactivo</span>`;
                    }
                }
            },
            {
                "data": "id",
                "render": function (id) {
                    return `
                        <div>
                            <a href="/Admin/Almacen/Editar/${id}" class="btn btn-primary btn-sm">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Eliminar("/Admin/Almacen/Eliminar/${id}") class="btn btn-danger btn-sm">
                                <i class="bi bi-trash3"></i>
                            </a>
                        </div>
                    `;
                }
            }
        ]
    });
}

const Eliminar = (url) => {

    Swal.fire({
        title: "Estas seguro?",
        text: "No podrás revertir esto!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#325d88",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, Eliminar!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}