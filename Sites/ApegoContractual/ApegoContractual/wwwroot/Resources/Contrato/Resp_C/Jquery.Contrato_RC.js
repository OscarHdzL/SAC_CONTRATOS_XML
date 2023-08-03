$(document).ready(function () {
    LaunchLoader(true);
    $('#tbl_contrato_RC').DataTable({

        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            //{ "width": "10%", "targets": 0 },
            //{ "width": "25%", "targets": 1 },
            //{ "width": "20%", "targets": 5 },
            //{ "width": "10%", "targets": 6 },
        ],
    })
    obtenerListado_RC("1");
});

function obtenerListado_RC(estatus) {
    var _localEstatus = localStorage.getItem('estatusContrato');
    if (_localEstatus != null) {
        estatus = _localEstatus;
    }

    var idDependencia = $('#HDidDependencia').val();
    var idRol = $('#idRolUsuario').val();
    var idUser = $('#HDidUsuario').val();
    $.get($('#EndPointAC').val() + "SerContrato/Get/ListadoContratos/RolDependenciaUsuario/" + idRol + '/' + idDependencia + '/' + idUser, function (data, status) {
        var listado = [];

        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];

            var fechaInicio = data[i].fecha_Iinicio.substring(0, data[i].fecha_Iinicio.indexOf('T'));
            var fechaFin = data[i].fecha_fin.substring(0, data[i].fecha_fin.indexOf('T'));

            fila.push(data[i].numero);
            fila.push(data[i].nombre === null ? "No Asignada" : data[i].nombre);
            fila.push(fechaInicio);
            fila.push(fechaFin);
            fila.push(data[i].tipo_contrato === null ? "No asignada" : data[i].tipo_contrato)

            if (data[i].estatus == true) {
                fila.push('Abierto');
                fila.push("<div align='center'><button class='btn btn-secondary' title='Asignar producto(s) a contrato' onclick=\"ConPro('" + data[i].id + "');\"><i class='fa fa-wrench'></i></button> <button class='btn btn-warning' title='Auditoria de plan de entrega' onclick=\"asociarObligaciones('" + data[i].id + "');\"><i class='glyphicon glyphicon-list-alt'></i></button> <button class='btn btn-info' title='Asignar responsable de apego contractual' onclick=\"AsignarRespApego('" + data[i].id + "');\"><i class='fa fa-user'></i></button> <button class='btn btn-success' title='Cerrar contrato' onclick=\"CerrarContrato('" + data[i].id + '\',\'' + data[i].responsableapegocontractual + "');\"><i class='fa fa-check-square'></i></button> </i></button> <button class='btn btn-warning' title='Ver Acuerdo' onclick=\"ModalVerAcuerdosRC('" + data[i].id + "');\"><i class='glyphicon glyphicon-thumbs-up'></i></button></div>");
            } else {
                fila.push('Cerrado');
                fila.push("<div align='center'><button class='btn btn-secondary' title='Asignar producto(s) a contrato' onclick=\"ConPro('" + data[i].id + "');\"><i class='fa fa-wrench'></i></button> <button class='btn btn-warning' title='Auditoria de plan de entrega' onclick=\"asociarObligaciones('" + data[i].id + "');\"><i class='glyphicon glyphicon-list-alt'></i></button></div>");
            }
            //fila.push("<button class='btn-secondary' title='Ver obligaciones' onclick=\"VerObligaciones('" + data[i].id + "');\"><i class='fa fa-list'></i></button>");

            //fila.push("<button class='btn btn-danger' title='Ir a plan de entrega' onclick=\"IrPlanEntrega('" + data[i].id + "');\"><i class='fa fa-arrow-circle-right'></i></button>");


            fila.push("<button class='btn btn-info' title='Ver obligaciones' onclick=\"VerObligaciones('" + data[i].id + "');\"><i class='fa fa-list'></i></button><button class='btn btn-danger' title='Ir a plan de entrega' onclick=\"IrPlanEntrega('" + data[i].id + "');\"><i class='fa fa-arrow-circle-right'></i></button><button class='btn btn-info' title='Ir a reporte sanciones' onclick=\"IrReporteSanciones('" + data[i].id + "');\"><i class='fa fa-bar-chart'></i></button>");

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

        var table = $('#tbl_contrato_RC').DataTable();

        table.destroy();

        $('#tbl_contrato_RC').DataTable({
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
                { title: "Ir..." },
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

function GetAcuerdosRC(idcontrato) {
    var contrato = idcontrato;
    $.get($("#EndPointAC").val() + "SerContrato/Get/ListadoAcuerdosRC/" + contrato, function (data, status) {
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

        var table = $('#tbl_Acuerdos_RC').DataTable();
        table.destroy();
        $('#tbl_Acuerdos_RC').DataTable({

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
    GetSancionesRC(contrato);
    GetAcuerdosR_C();
    $('#idAcuerdoRC').val(id);
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

function GetSancionesRC(id) {
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

function GetAcuerdosR_C() {

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


function ModalVerAcuerdosRC(idContrato) {
    $('#idAcuerdosRC').val(idContrato);
    $('#ModalVerAcuerdosRC').modal('show');
    $('#lista_doctos').html('');

    GetAcuerdosRC(idContrato);
}

$("#btnCargaMasiva").click(function () {
    $('#modalCargaMasiva').modal({ backdrop: 'static', keyboard: false });
    $("#modalCargaMasiva").modal("show");
});

function ConPro(id) {
    return window.location.href = "/Contrato/ContratoProducto/" + id;
}
function asociarObligaciones(id) {
    window.location.href = "/Contrato/AsignacionResponsabilidades/" + id;
}


function AsignarRespApego(idContrato, idpersona) {
    LaunchLoader(true);
    //$("#Responsable_Apego").val(idpersona);
    //agregar colocar la dependencia del contrato
    console.log('colocar dependencia del contrato');
    $("#Contrato_id").val(idContrato);


    $('#modalRespApegoContrato').modal('show');

    //DropResponsablesApego(idContrato, idpersona);
    fillDependencia(idContrato, idpersona);
    LaunchLoader(false);


}

function fillDependencia(idContrato, idpersona) {
    var uri = $('#EndPointAC_Admon').val() + 'Contratos/GetContratoporid/' + idContrato;
    $.get(uri, function (data) {
        $("#dependenciaContrato").val(data[0].p_tbl_dependencia_id);
        DropResponsablesApego(idContrato, idpersona);
    });
}

function getResponsableApego() {

}
function CancelarAsignarRespApego() {
    $('#modalRespApegoContrato').modal('hide');
    $("#Responsable_Apego").val('');
    $("#Contrato_id").val();
    $("#dependenciaContrato").val('');
}

function closeModalRC() {
    $('#modalRespApegoContrato').modal('hide');
    $("#Responsable_Apego").val('');
    $("#Contrato_id").val();
}
function VerObligaciones(id) {
    $('#modalVerificacionContrato').modal('show');
    var listado = [];

    $.get($('#EndPointAC').val() + "SerVerificacion/Get/VerificacionXContrato/" + id, function (data, status) {
        console.log(data);
        console.log('obligaciones');
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            var fechaInicio = '';
            if (data[i].fecha_verificacion != null) {
                fechaInicio = data[i].fecha_verificacion.substring(0, data[i].fecha_verificacion.indexOf('T'));
            }
            fila.push(data[i].pregunta);
            fila.push(data[i].pregunta_personalizada);
            fila.push(fechaInicio);

            listado.push(fila);

        }

        var table = $('#tbl_verificacion_modal').DataTable();

        table.destroy();

        $('#tbl_verificacion_modal').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: listado,
            columns: [
                { title: "Pregunta", "width": "50%" },
                { title: "Pregunta Personalizada", "width": "40%" },
                { title: "Fecha verificación", "width": "10%" },
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

function CancelarVerObligaciones() {
    $('#modalVerificacionContrato').modal('hide');
}

function IrPlanEntrega(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        return window.location.href = "/PlanDeEntrega/Lista/" + id;
    });
}

function IrReporteSanciones(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        return window.location.href = "/NotificacionSanciones/ReporteListaSanciones/" + id;
    });
}


$("#btnRealizarCargaMasiva").click(function () {
    var fileUpload = $("#archivo").get(0);

    var formData = new FormData();
    formData.append('archivo', fileUpload.files[0]);

    $.ajax({
        url: "/Contrato/RealizarCargaMasiva",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            console.log(data);

            if (data.exitoso) {
                $("#modalCargaMasiva").modal("hide");
                SuccessSA();
            }
            else {
                ErrorSA("", data.excepcion);
            }

        },
        error: function () {

        }
    });
});

function DropResponsablesApego(idContrato, idpersona) {
    var idDependencia = $('#dependenciaContrato').val();
    var responsable = "";
    console.log('idpersona: ' + idpersona);


    var url = $("#EndPointAC").val() + "SerServidorPublico/Get/sigla/RAC/dependencia/" + idDependencia;
    $.get(url, function (data, status) {

        var Body = "<option value='0'>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {

            Body = Body + "<option value='" + data[i].tbl_rol_usuario_id + "'>" + data[i].nombrecompleto + "</option>";
        }
        $('#DropResponsableApego').html(Body);

        if (idpersona != "null") {
            $.ajax({
                async: false
            });

            var url = $("#EndPointAC").val() + "SerServidorPublico/Get/sigla/RAC/Contrato/" + idContrato;
            $.get(url, function (data, status) {
                $("#Responsable_Apego").val(data[0].tbl_contrato_servidor_resp_id);
                $('#DropResponsableApego').val(data[0].tbl_rol_usuario_id);
            }, 'json');

            $.ajax({
                async: true
            });
        }
    });


}

function guardarResponsableApego() {



    var id_resp = $("#Responsable_Apego").val();
    var Validacion = ValidarRespApego();
    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);

    }
    else {

        var d = new Date();
        var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

        var OBJ_RAC = ResponsablesACClass;
        OBJ_RAC.p_opt = 2;

        OBJ_RAC.p_tbl_contrato_id = $('#Contrato_id').val();
        OBJ_RAC.p_tbl_rol_usuario_id = $('#DropResponsableApego').val();
        OBJ_RAC.p_inclusion = date;
        OBJ_RAC.p_estatus = 1;


        var tipo_peticion = '';
        var URL = '';

        if (id_resp == null || id_resp == "null" || id_resp == "") {
            OBJ_RAC.p_id = '00000000-0000-0000-0000-000000000000';
            tipo_peticion = "post";
            URL = $("#EndPointAC").val() + "SerRespApego/add";
        } else {
            OBJ_RAC.p_id = id_resp;
            tipo_peticion = "put";
            URL = $("#EndPointAC").val() + "SerRespApego/update";


        }


        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_RAC),
            type: tipo_peticion,

            success: function (data) {
                var response = JSON.parse(data);
                if (response[0].cod == "success") {

                    function confirmar() {
                        return closeModalRC();
                    }
                    var Conf = eval(confirmar);
                    SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", Conf);

                    obtenerListado_RC("1");

                }
                else {
                    ErrorSA("", "No se pudo guardar el responsable");
                }
            },
            error: function () {

                ErrorSA("", "No se pudo guardar el responsable");
            },
            processData: false,
            url: URL

        })
    }


}

function ValidarRespApego() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#DropResponsableApego').val() == '' || $('#DropResponsableApego').val() == '0') {
        Response.Texto = 'Debe seleccionar un responsable';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function filtrar_contrato(estatus) {
    localStorage.setItem('estatusContrato', estatus);
    obtenerListado_RC(estatus);
}

function CerrarContrato(tbl_contrato_id) {
    $('#txtPregunta').html('');
    //var txtPregunta = '¿Está seguro que desea cerrar el contrato?';
    var txtPlanEntregaNombre = '';
    var txtPlanMonitoreoNombre = '';
    var txtConteo = '';
    var j = 0;
    var k = 0;
    $('#ModalCerrarContrato').modal('show');

    //Consultamos si hay planes de entrega sin ejecutar, si hay no se podrá cerrar el contrato. 
    $.get($("#EndPointAC").val() + "Operaciones/PE/Get/lista/tipo/contrato/id/" + tbl_contrato_id + "/" + $('#HDidUsuario').val(), function (data, status) {

        for (var i = 0; i <= data.length - 1; i++) {
            if (data[i].header.p_cumplio_pe == false && data[i].header.p_ejecucion == false) {
                j++;
                txtPlanEntregaNombre += data[i].header.p_identificador + '<br>';
            }
        }

        if (j >= 1) {
            txtConteo += '<br><br>'
            txtConteo += 'No se puede cerrar el contrato ya que tiene ' + j + ' planes de entrega sin confirmar <br><br>' + txtPlanEntregaNombre;
        }
        //else {
        //    txtConteo = '¿Está seguro que desea cerrar el contrato?'
        //}

        //Plan de monitoreo
        $.get($("#EndPointAC").val() + 'Operaciones/PM/Get/List/' + tbl_contrato_id, function (data, status) {
            if (data.length > 0) {
                for (var i = 0; i <= data.length - 1; i++) {

                    if (!data[i].ejecutado) {
                        k++;
                        txtPlanMonitoreoNombre += data[i].identificador + '<br>';
                    }

                }
                if (k >= 1) {
                    txtConteo += '<br><br>'
                    txtConteo += 'No se puede cerrar el contrato ya que tiene ' + k + ' planes de monitoreo sin confirmar <br><br>' + txtPlanMonitoreoNombre;
                }
                //else {
                //    txtConteo = '¿Está seguro que desea cerrar el contrato?'
                //}

            }

            if (j == 0 && k == 0) {
                txtConteo = '¿Está seguro que desea cerrar el contrato?'
                $(".success-call").removeClass('hidden');
            }


            $('#txtPregunta').html(txtConteo);

        });




    });


    $(".success-call").prop("onclick", null).off("click");
    $(".success-call").click(function () {
        //var bit = 0;

        //bit = estatus == false ? 0 : 1;
        $.post($("#EndPointAC").val() + "Plan/CerrarContrato/" + tbl_contrato_id, function (data, status) {
            $('#ModalCerrarContrato').modal('hide');

            obtenerListado_RC("1");
        });

    });
}

var ResponsablesACClass = {

    p_opt: null,
    p_id: null,
    p_tbl_contrato_id: null,
    p_inclusion: null,
    p_estatus: null,
    p_tbl_rol_usuario_id: null

}

function CerrarModalAcuerdosRC() {
    $('#idAcuerdosRC').val('');
    $('#ModalVerAcuerdosRC').modal('hide');
    $('#lista_doctos').html('');
}

function UpdateAcuerdoRC() {
    var Validacion = ValidarAcuerdoUpdate();
    var contrato = localStorage.getItem('Contrato');
    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);

    }
    else {
        var OBJ_AcuerdoForm = AcuerdoClassPE;
        OBJ_AcuerdoForm.p_opt = 3;
        OBJ_AcuerdoForm.p_id = $('#idAcuerdoRC').val();
        OBJ_AcuerdoForm.p_tbl_contrato_id = contrato;
        OBJ_AcuerdoForm.p_tbl_contrato_servidor_resp_id = $('#ResponsableA_update').val();
        OBJ_AcuerdoForm.p_tbl_tipo_acuerdo_id = $('#TipoA_update').val();
        OBJ_AcuerdoForm.p_acuerdo = $('#txt_Acuerdo_update').val();
        OBJ_AcuerdoForm.p_fecha_registro = $('#txt_FechaRegistro_update').val();
        OBJ_AcuerdoForm.p_fecha_compromiso = $('#txt_FechaCompromiso_update').val();
        OBJ_AcuerdoForm.p_fecha_cierre = $('#txt_FechaCierre_update').val();
        OBJ_AcuerdoForm.p_estatus_acuerdo = $('#cmbEstatusAcuerdo_update').val();
        OBJ_AcuerdoForm.p_comentario = $('#txt_ComentarioAcuerdo_update').val();
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
                    CerrarModalAcuerdosRCUpdate();
                    GetAcuerdosRC(contrato);
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

function CerrarModalAcuerdosRCUpdate() {
    $('#idAcuerdo').val('');
    $('#Modal_EditarAcuerdo').modal('hide');
    $('#lista_doctos').html('');
}