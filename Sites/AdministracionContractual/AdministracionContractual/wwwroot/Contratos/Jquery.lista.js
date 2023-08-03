$(document).ready(function () {
    LaunchLoader(true);
    $('#tbl_contratos_CORE').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            //{ "width": "25%", "targets": 0 },
            //{ "width": "30%", "targets": 1 },
            //{ "width": "10%", "targets": 2 },
            //{ "width": "10%", "targets": 3 },
            //{ "width": "15%", "targets": 4 },
            //{ "width": "10%", "targets": 5 }
        ],
    });
    Get_Lista_Contrato("1");
})

function Get_Lista_Contrato(estatus) {
    var _localEstatus = localStorage.getItem('estatusContrato');
    if (_localEstatus != null) {
        estatus = _localEstatus;
    }

    var idDependencia = $('#HDidDependencia').val();
    $.get($('#EndPointAdmon').val() + "Contratos/Get/Lista/" + idDependencia, function (data, status) {
        var listado = [];

        if ($('#Id_Proyecto').val() == '00000000-0000-0000-0000-000000000000') {
            for (var i = 0; i <= data.length - 1; i++) {
                var fila = [];
                var fechaInicio = data[i].fecha_Iinicio.substring(0, data[i].fecha_Iinicio.indexOf('T'));
                var fechaFin = data[i].fecha_fin.substring(0, data[i].fecha_fin.indexOf('T'));

                fila.push(data[i].numero);
                fila.push(data[i].nombre == null ? "No Asignada" : data[i].nombre);
                fila.push(fechaInicio);
                fila.push(fechaFin);
                fila.push(data[i].tipo_contrato === null ? "No asignada" : data[i].tipo_contrato)
                if (data[i].estatus == true) {
                    fila.push('Abierto');
                    fila.push("<button class='btn btn-primary' title='Modificar contrato' onclick=\"Editar_Contrato('" + data[i].id + "');\"><i class='fa fa-edit'></i></button>");
                } else {
                    fila.push('Cerrado');
                    fila.push("");
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
                }
            }
        } else {

            for (var i = 0; i <= data.length - 1; i++) {
                if ($('#Id_Proyecto').val() === data[i].tbl_proyecto_id) {
                    var fila = [];
                    var fechaInicio = data[i].fecha_Iinicio.substring(0, data[i].fecha_Iinicio.indexOf('T'));
                    var fechaFin = data[i].fecha_fin.substring(0, data[i].fecha_fin.indexOf('T'));

                    fila.push(data[i].numero);
                    fila.push(data[i].nombre == null ? "No Asignada" : data[i].nombre);
                    fila.push(fechaInicio);
                    fila.push(fechaFin);
                    fila.push(data[i].tipo_contrato === null ? "No asignada" : data[i].tipo_contrato)

                    if (data[i].estatus == true) {
                        fila.push('Abierto');
                        fila.push("<button class='btn btn-primary' title='Modificar contrato' onclick=\"Editar_Contrato('" + data[i].id + "');\"><i class='fa fa-edit'></i></button>");
                    } else {
                        fila.push('Cerrado');
                        fila.push('');
                    }
                    //listado.push(fila);

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
                    }

                }
            }
        }

        var table = $('#tbl_contratos_CORE').DataTable();

        table.destroy();

        $('#tbl_contratos_CORE').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: listado,
            columns: [
                { title: "Número del contrato." },
                { title: "Nombre del contrato." },
                { title: "fecha inicio." },
                { title: "Fecha fin." },
                { title: "Tipo de contrato." },
                { title: "Estatus." },
                { title: "Acciones." }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });
}

function filtrar_contrato(estatus) {
    localStorage.setItem('estatusContrato', estatus);
    Get_Lista_Contrato(estatus);
}

function Editar_Contrato(item) {
    window.location.href = window.location.origin + "/contratos/actualizacion/" + item;
}

$('#Add_Contrato').click(function () {
    window.location.href = "index";
})