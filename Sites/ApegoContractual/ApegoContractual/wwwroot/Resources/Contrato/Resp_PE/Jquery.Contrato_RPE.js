$(document).ready(function () {
    LaunchLoader(true);
    $('#tbl_contrato_RPE').DataTable({

        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            //{ "width": "10%", "targets": 0 },
            //{ "width": "30%", "targets": 4 },
            //{ "width": "10%", "targets": 5 },
        ],
    })
    obtenerListado_RPE("1");
});

function obtenerListado_RPE(estatus) {
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
                fila.push("<div align='center'><button class= 'btn btn-default' title = 'Ir a plan de entrega' onclick =\"IrPlanEntrega('" + data[i].id + "');\"><i class='glyphicon glyphicon-indent-right'></i></button> <button class='btn btn-warning' title='Ver Acuerdo' onclick=\"ModalVerAcuerdosPE('" + data[i].id +"');\"><i class='glyphicon glyphicon-thumbs-up'></i></button></div>");
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

        var table = $('#tbl_contrato_RPE').DataTable();

        table.destroy();

        $('#tbl_contrato_RPE').DataTable({
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

function ModalVerAcuerdosPE(idcontrato) {
    $('#idAcuerdosPE').val(idcontrato);
    $('#ModalVerAcuerdosPE').modal('show');
    $('#lista_doctos').html('');

    GetAcuerdosPE(idcontrato);
}

function GetAcuerdosPE(idcontrato) {
    var usuario = $('#HDidUsuario').val();
    var contrato = idcontrato;
    $.get($("#EndPointAC").val() + "SerContrato/Get/ListadoAcuerdos/" + usuario + '/' + contrato, function (data, status) {
        var NombreResponsable = null;;
        var Arreglo_arreglos_file = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            NombreResponsable = data[i].responsable;
            Interno.push(data[i].tipo_acuerdo);
            Interno.push(NombreResponsable);
            Interno.push(data[i].acuerdo);

            fecha = data[i].fecha_registro.split('T');
            Interno.push(fecha[0]);

            fecha = data[i].fecha_compromiso.split('T');
            Interno.push(fecha[0]);

            fecha = data[i].fecha_cierre.split('T');
            Interno.push(fecha[0]);

            Interno.push(data[i].estatus_acuerdo);
            Interno.push(data[i].comentario);
            Interno.push(("<button class='btn btn-primary' title='Editar' onclick=\"muestraModalEditar('" + data[i].id + '\',\'' + contrato + "');\"><i class='fa fa-edit'></i></button>"));


            Arreglo_arreglos_file.push(Interno);

        }

        var table = $('#tbl_Acuerdos_PE').DataTable();
        table.destroy();
        $('#tbl_Acuerdos_PE').DataTable({

            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos_file,
            columns: [
                { title: "Tipo Acuerdo" },
                { title: "Responsable" },
                { title: "Acuerdo" },
                { title: "Fecha de registro" },
                { title: "Fecha de compromiso" },
                { title: "Fecha de cierre" },
                { title: "Estatus" },
                { title: "Comentarios" },
                { title: "Acciones" }
            ],

        });
        LaunchLoader(false);

    });


}

function muestraModalEditar(id, contrato) {
    GetSancionesPE(contrato);

    GetAcuerdosP_E();
    $('#idAcuerdo').val(id);
    $('#Modal_EditarAcuerdo').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_EditarAcuerdo').modal('show');

    $.get($('#EndPointAC').val() + "SerAcuerdos/Get/AcuerdoEditRC/" + id + "/" + contrato, function (data, status) {
        $('#TipoA_update > option[value="' + data.tipo_acuerdo_id + '"]').attr("selected", "selected");
        $(".TipoA_update").prop('disabled', true);

        $('#ResponsableA_update > option[value="' + data.responsable_id + '"]').attr("selected", "selected");
        $(".ResponsableA_update").prop('disabled', true);

        $('#txt_Acuerdo_update').val(data.acuerdo);
        $(".txt_Acuerdo_update").prop('disabled', true);

        fecha = data.fecha_registro.split('T');
        $('#txt_FechaRegistro_update').val(fecha[0]);
        $(".txt_FechaRegistro_update").prop('disabled', true);

        fecha1 = data.fecha_compromiso.split('T');
        $('#txt_FechaCompromiso_update').val(fecha1[0]);
        $(".txt_FechaCompromiso_update").prop('disabled', true);

        fecha2 = data.fecha_cierre.split('T');
        $('#txt_FechaCierre_update').val(fecha2[0]);
        $(".txt_FechaCierre_update").prop('disabled', true);

        $('#cmbEstatusAcuerdo_update').val(data.estatus_acuerdo);
        $('#txt_ComentarioAcuerdo_update').val(data.comentario);

    });
    //    $('#Partial_Modal_EditarAcuerdo').html(data)
    //        fechasIni();
    //    $('#Modal_EditarAcuerdo').modal('show');
    //}).fail(function (response) {
    //    alert(response.statusCode + response.statusText + response.resp);
    localStorage.setItem('Contrato', contrato);

}

function GetSancionesPE(id) {
    idContrato = id;
    $.get($('#EndPointAC').val() + "SerRespApego/Get/Dropdown/" + idContrato, function (data, status) {
        var body = "<option disabled selected value='-1'>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#ResponsableA').html(body);
        $('#ResponsableA_update').html(body);
    });
    return;
}

function GetAcuerdosP_E() {

    $.get($('#EndPointAC').val() + "SerAcuerdos/Get/DropDown/Tipos", function (data, status) {
        var body = "<option disabled selected value='-1'>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#TipoA').html(body);
        $('#TipoA_update').html(body);
    });
    return;
}

function IrPlanEntrega(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        return window.location.href = "/PlanDeEntrega/Lista/" + id;
    });
}

function filtrar_contrato(estatus) {
    localStorage.setItem('estatusContrato', estatus);
 obtenerListado_RPE(estatus);
}

function CerrarModalAcuerdosPE() {
    $('#idAcuerdosPE').val('');
    $('#ModalVerAcuerdosPE').modal('hide');
    $('#lista_doctos').html('');
}

function CerrarModalAcuerdosPEUpdate() {
    $('#idAcuerdo').val('');
    $('#Modal_EditarAcuerdo').modal('hide');
    $('#lista_doctos').html('');
}

function UpdateAcuerdoPE() {
    var Validacion = ValidarAcuerdoUpdate();
    var contrato = localStorage.getItem('Contrato');
    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);

    }
    else {
        var OBJ_AcuerdoForm = AcuerdoClassPE;
        OBJ_AcuerdoForm.p_opt = 3;
        OBJ_AcuerdoForm.p_id = $('#idAcuerdo').val();
        OBJ_AcuerdoForm.p_tbl_contrato_id = contrato;
        OBJ_AcuerdoForm.p_tbl_contrato_servidor_resp_id = $('#ResponsableA_update').val();
        OBJ_AcuerdoForm.p_tbl_tipo_acuerdo_id = $('#TipoA_update').val();
        OBJ_AcuerdoForm.p_acuerdo = $('#txt_Acuerdo_update').val();
        OBJ_AcuerdoForm.p_fecha_registro = $('#txt_FechaRegistro_update').val();
        OBJ_AcuerdoForm.p_fecha_compromiso = $('#txt_FechaCompromiso_update').val();
        OBJ_AcuerdoForm.p_fecha_cierre = $('#txt_FechaCierre_update').val();
        OBJ_AcuerdoForm.p_estatus_acuerdo = $('#cmbEstatusAcuerdo_update').val();
        OBJ_AcuerdoForm.p_comentario = $('#txt_ComentarioAcuerdo_update').val();
        //OBJ_AcuerdoForm.Inclusion = date;
        OBJ_AcuerdoForm.p_estatus = 1;

        //var form_data = new FormData();
        //form_data.append('AcuerdoForm', JSON.stringify(OBJ_AcuerdoForm));

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_AcuerdoForm),
            type: 'put',

            success: function (data) {
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", "El registro se actualizó correctamente");
                    CerrarModalAcuerdosPEUpdate();
                    GetAcuerdosPE(contrato);
                    $('#Modal_EditarAcuerdo').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            //processData: false,
            //type: 'POST',
            url: ($('#EndPointAC').val() + "SerAcuerdos/update")

        })
    }
}

function ValidarAcuerdoUpdate() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_Acuerdo_update').val() == '') {
        Response.Texto = 'Debe agregar un Acuerdo';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Acuerdo_update').val(), 'Acuerdo') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Acuerdo"';
        Response.Bit = true;
        return Response;
    }

    if ($('#txt_ComentarioAcuerdo_update').val() == '') {
        Response.Texto = 'Debe agregar un Comentario';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_ComentarioAcuerdo_update').val(), 'Comentario') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Comentario"';
        Response.Bit = true;
        return Response;
    }

    if ($('#drop_Responsables_update').val() == '') {
        Response.Texto = 'Debe seleccionar un responsable';
        Response.Bit = true;
        return Response;
    }
    if ($('#drop_TiposAcuerdo_update').val() == '') {
        Response.Texto = 'Debe seleccionar un tipo de acuerdo';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmbEstatusAcuerdo_update').val() == '') {
        Response.Texto = 'Debe seleccionar un estatus';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaRegistro_update').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de registro';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaCompromiso_update').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de compromiso';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaCierre_update').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de cierre';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

var AcuerdoClassPE = {
    p_opt: null,
    p_id: null,
    p_tbl_contrato_id: null,
    p_tbl_contrato_servidor_resp_id: null,
    p_tbl_tipo_acuerdo_id: null,
    p_acuerdo: null,
    p_fecha_registro: null,
    p_fecha_compromiso: null,
    p_fecha_cierre: null,
    p_estatus_acuerdo: null,
    p_comentario: null,
    //Inclusion: null,
    p_estatus: null
}