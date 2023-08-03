$(document).ready(function () {
    LaunchLoader(true);
    $('#tbl_contrato_RAC').DataTable({

        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            //{ "width": "10%", "targets": 0 },
            //{ "width": "30%", "targets": 1 },
            //{ "width": "15%", "targets": 5 },
        ],
    })
    obtenerListado_RAC("1");
});

function obtenerListado_RAC(estatus) {
    var _localEstatus = localStorage.getItem('estatusContrato');
    if (_localEstatus != null) {
        estatus = _localEstatus;
    }

    var idDependencia = $('#HDidDependencia').val();
    var idRol = $('#idRolUsuario').val();
    var idUser = $('#HDidUsuario').val();
    //debugger;
    $.get($('#EndPointAC').val() + "SerContrato/Get/ListadoContratos/RolDependenciaUsuario/" + idRol + '/' + idDependencia + '/' + idUser, function (data, status) {
        var listado = [];

        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];

            var fechaInicio = data[i].fecha_Iinicio.substring(0, data[i].fecha_Iinicio.indexOf('T'));
            var fechaFin = data[i].fecha_fin.substring(0, data[i].fecha_fin.indexOf('T'));

            fila.push(data[i].numero);
            fila.push(data[i].nombre == null ? "No Asignada" : data[i].nombre);
            fila.push(fechaInicio);
            fila.push(fechaFin);
            fila.push(data[i].tipo_contrato == null ? "No asignada" : data[i].tipo_contrato)
            if (data[i].estatus == true) {
                fila.push('Abierto');
                fila.push("<div align='center'><button class='btn btn-primary' title='Ver expediente' onclick=\"verExpediente('" + data[i].id + "');\"><i class='fa fa-files-o'></i></button> <button class='btn btn-success' title='Ver obligaciones' onclick=\"IrObligaciones('" + data[i].id + "');\"><i class='fa fa-legal'></i></button></div>");
            } else {
                fila.push('Cerrado');
                fila.push('');
            }

            switch (estatus) {
                case "0":
                    if (data[i].estatus == false) {
                        listado.push(fila);
                    }
                    break;
                case "1":
                    if (data[i].estatus == true) {
                        listado.push(fila);
                    }
                    break;
                case "2":
                    listado.push(fila);
                    break;
                default:
                    if (data[i].estatus == true) {
                        listado.push(fila);
                    }
            }        }

        var table = $('#tbl_contrato_RAC').DataTable();

        table.destroy();

        $('#tbl_contrato_RAC').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: listado,
            columns: [
                { title: "No." },
                { title: "Nombre del contrato" },
                { title: "Inicio" },
                { title: "Fin" },
                { title: "Tipo de contrato" },
                { title: "Estatus" },
                { title: "Acciones" },
            ],
            destroy: true,

            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });
}
function verExpediente(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        return window.location.href = "/GestionExpedientes/ExpedientesContrato/" + id;
    });
}
function IrObligaciones(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        return window.location.href = "/DesgloseContrato/Index/" + id;
    });
}


function filtrar_contrato(estatus) {
    localStorage.setItem('estatusContrato', estatus);
   obtenerListado_RAC(estatus);
}