var datatable 

$(document).ready(function () {
    loadDataTable()
});

function loadDaraTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url":"/Admin/Bodega/ObtenerTodos"
        },
        "columns": [
            {"data": "nombre", "width": "20%" },
            {"data": "descripcion", "width": "20%"},
            {"data": "estado", "width": "20%"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="Admin/Bodegas/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer"><i class="fas fa-edit"></i></a>
                            <a class="btn btn-danger text-white" style="cursor:pointer"><i class="fas fa-trash"></i></a>
                        </div>`;
                }, "width": "20%"
            }
        ]
    })
}