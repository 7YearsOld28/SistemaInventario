﻿var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Admin/Marcas/ObtenerTodos"
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    }
                    else {
                        return "Inactivo";
                    }
                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Marcas/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer"><i class="fas fa-edit"></i></a>
                            <a onclick = Delete("/Admin/Marcas/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer"><i class="fas fa-trash"></i></a>
                        </div>`;
                }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Está Seguro que quiere eliminar la Marca?",
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