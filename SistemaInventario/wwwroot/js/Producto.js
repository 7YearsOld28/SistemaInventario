﻿var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Admin/Productos/ObtenerTodos"
        },
        "columns": [
            { "data": "numeroSerie", "width": "15%" },
            {
                "data": "descripcion", "width": "25%"
            },
            {
                "data": "categoria.nombre", "width": "15%"
            },
            {
                "data": "marca.nombre", "width": "15%"
            },
            {
                "data": "precio", "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Productos/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer"><i class="fas fa-edit"></i></a>
                            <a onclick = Delete("/Admin/Productos/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer"><i class="fas fa-trash"></i></a>
                        </div>`;
                }, "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Está Seguro que quiere eliminar el Producto?",
        text: "Este registro no se prodrá recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}