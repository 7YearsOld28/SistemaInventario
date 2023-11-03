var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Admin/Usuarios/ObtenerTodos"
        },
        "columns": [
            { "data": "userName", "width": "10%" },
            { "data": "nombres", "width": "10%" },
            { "data": "apellidos", "width": "10%" },
            { "data": "email", "width": "10%" },
            { "data": "phoneNumber", "width": "10%" },
            { "data": "role", "width": "10%" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    var hoy = new Date().getTime();
                    var bloqueo = new Date(data.lockoutEnd).getTime();
                    if (bloqueo > hoy) {
                        //Este Usuario está bloqueado
                        return `
                        <div class="text-center">
                            <a onclick = BloquearDesbloquear('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                                <i class="fas fa-lock-open"></i> Desbloquear
                            </a>
                        </div>`;
                    }
                    else
                    {
                        return `
                        <div class="text-center">
                            <a onclick = BloquearDesbloquear('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:150px;">
                                <i class="fas fa-lock"></i> Bloquear
                            </a>
                        </div>`;
                    }
                   
                }, "width": "20%"
            }
        ]
    });
}

function BloquearDesbloquear(id) {
            $.ajax({
                type: "POST",
                url: '/Admin/Usuarios/BloquearDesbloquear',
                data: JSON.stringify(id),
                contentType: "application/json",
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