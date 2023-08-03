$(document).ready(function () {
    LaunchLoader(true);
    $('#ListadoPuntosEvaluar').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
    });
    //GetListaPuntosEvaluar($('#HDidDependencia').val(), $('#IdContrato').val());
    ObtenerContrato();
});

/***************************************************************Listado de puntos a evaluar**************************************************************/
function ObtenerContrato() {

    var uri = $('#EndPointAC_Admon').val() + 'Contratos/GetContratoporid/' + $('#IdContrato').val();
    $.get(uri, function (data){    
        $("#dependenciaContrato").val(data[0].p_tbl_dependencia_id);
        console.log('dependencia del contrato' + $('#dependenciaContrato').val());
        GetListaPuntosEvaluar($('#dependenciaContrato').val(), $('#IdContrato').val());

    });
}


function GetListaPuntosEvaluar(idDep, idCon) {

    $.get($('#EndPointAC').val() + "SerVerificacion/Get/Lista/" + idDep + "/" + idCon, function (data, status) {
        var si = '557b948a-3ed0-11ea-9fcf-00155d1b3502';//que pedo
        var no = '557e154a-3ed0-11ea-9fcf-00155d1b3502';
        var NA = '5581020a-3ed0-11ea-9fcf-00155d1b3502';
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            Interno.push("<textarea style='width:100% !important;border-style: none;background-color: transparent;width: 1148px;margin: 0px;height: 46px' disabled >" + data[i].pregunta + "</textarea>");
            var pregunta_p = null;
            pregunta_p = data[i].pregunta_personalizada ?? '';
            Interno.push('<textarea style="width:100% !important;border-style: none;background-color: transparent;width: 1148px;margin: 0px;height: 46px" id="pp_' + data[i].idpregunta + '" >' + pregunta_p + "</textarea>");

            if (data[i].tbl_estatus_verificacion_id == si) {
                Interno.push("<input type='checkbox' id='chk_" + data[i].idpregunta + "' name='chk_" + data[i].idpregunta + "' value='" + data[i].idpregunta + "' onchange='CambiarCheck(\"" + data[i].idpregunta + "\")'   class='pregunta_verificacion' checked>");
            } else {
                Interno.push("<input type='checkbox' id='chk_" + data[i].idpregunta + "' name='chk_" + data[i].idpregunta + "' value='" + data[i].idpregunta + "' onchange='CambiarCheck(\"" + data[i].idpregunta + "\")'   class='pregunta_verificacion'>");
            }
            if (data[i].fecha_verificacion == null) {
                Interno.push("<input type='date' id='date_" + data[i].idpregunta + "'    name='date_" + data[i].idpregunta + "'  onchange='ValidarFecha(\"" + data[i].idpregunta + "\")'  class='fecha_verificacion'  >");
            } else {
                var fecha = data[i].fecha_verificacion;
                Interno.push("<input type='date' id='date_" + data[i].idpregunta + "' name='date_" + data[i].idpregunta + "' value='" + fecha.slice(0, 10) + "'  onchange='ValidarFecha(\"" + data[i].idpregunta + "\")' class='fecha_verificacion' >");
            }

            Interno.push('<a id="btnAddLV_' + data[i].idpregunta + '" onclick="GuardarEvaluacion(\'' + data[i].idpregunta + '\', \'' + data[i].idverificacion + '\')" class="fa fa-edit btn btn-primary" title="Guardar cambios"> </a>');

            Arreglo_arreglos.push(Interno);
        }
        $('#ListadoPuntosEvaluar').DataTable().destroy();;

        console.log(Arreglo_arreglos);
        $('#ListadoPuntosEvaluar').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "Pregunta", "width": "40%" },
                { title: "Personalizado", "width": "40%" },
                { title: "Asignado", "width": "5%" },
                { title: "Fecha verificación", "width": "10%" },
                { title: "Acciones", "width": "5%" }

            ]
        });
        LaunchLoader(false);
    });

}

/***********************************************************Fin del Listado de puntos a evaluar**********************************************************/

/**********************************************************Funciones para agregar la evaluacion**********************************************************/

function GuardarEvaluacion(idpregunta, idverificacion) {
    AddEvaluacionContV2(idpregunta, idverificacion);
}

function AddEvaluacionContV2(idpregunta, idverificacion) {
    var OBJ_Evaluacion = VerificacionContModelo;
    var endpoint = "";
    OBJ_Evaluacion.p_opt = 2;
    OBJ_Evaluacion.p_id = idverificacion;
    OBJ_Evaluacion.p_tbl_contrato_id = $('#IdContrato').val();
    OBJ_Evaluacion.p_tbl_usuario_id = $('#HDidUsuario').val();
    OBJ_Evaluacion.p_tbl_pregunta_formulario_id = idpregunta;
    if ($("#chk_" + idpregunta + "").is(":checked") == true) {
        OBJ_Evaluacion.p_tbl_estatus_verificacion_id = '557b948a-3ed0-11ea-9fcf-00155d1b3502';
        endpoint = $('#EndPointAC').val() + "SerVerificacion/AddV2";
    } else {
        OBJ_Evaluacion.p_tbl_estatus_verificacion_id = '557e154a-3ed0-11ea-9fcf-00155d1b3502';
        endpoint = $('#EndPointAC').val() + "SerVerificacion/DeleteV2";
    }
    OBJ_Evaluacion.p_inclusion = "";
    OBJ_Evaluacion.p_fecha_verificacion = $("#date_" + idpregunta + "").val();
    OBJ_Evaluacion.p_pregunta_personalizada = $("#pp_" + idpregunta + "").val();
    if (OBJ_Evaluacion.p_fecha_verificacion == null || OBJ_Evaluacion.p_fecha_verificacion == '') {
        if ($("#chk_" + idpregunta + "").is(":checked") == true) {
            ErrorSA("Error", "Debe seleccionar una fecha de verificación");
            return;
        }
        //si es nula la fecha y no esta seleccionado la pregunta lo dejamos pasar
    }
    debugger;
    LaunchLoader(true);
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Evaluacion),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                LaunchLoader(false);
                SuccessSA("Operación exitosa", objresponse[0].msg);
                GetListaPuntosEvaluar($('#dependenciaContrato').val(), $('#IdContrato').val());
            }
            else {
                LaunchLoader(false);
                ErrorSA("", objresponse[0].msg);
            }
        },
        //error: function () {
        //    LaunchLoader(true);
        //    var objresponse = JSON.parse(data);
        //    ErrorSA('', objresponse.Descripcion)
        //},
        error: function (jqXHR, exception) {
            console.log(jqXHR);
            var responseT = JSON.parse(jqXHR.responseText);
            console.log(responseT.response[0].msg);
            if (responseT.response[0] != null) {
                ErrorSA('', responseT.response[0].msg);
            } else {
                ErrorSA('', 'Ocurrió un error al guardar el registro');
            }
            LaunchLoader(false);
        },
        url: (endpoint)

    })
}

function ValidarFecha(idDate) {
    //console.log('cambiando fecha del date');
    var fecha = $("#date_" + idDate + "").val();
    console.log(fecha);
    FechaValida(idDate);
}

function FechaValida(idInput) {
    var fecha = $("#date_" + idInput + "").val();
    var x = new Date(fecha);
    var y = new Date();
    console.log(x < y);
    if (x < y) {
        ErrorSA("Error en la fecha", "La fecha no puede ser menor al día actual");
        $("#date_" + idInput + "").val(null);
        return false;
    } else {
        return true;
    }
}
function CambiarCheck(idInput) {
    if ($("#chk_" + idInput + "").is(":checked") == true) {
        console.log('seleccionado');
    } else {
        console.log('no seleccionado');
        $("#date_" + idInput + "").val(null);
        $("#pp_" + idInput + "").val('');
    }
}

function addEvSi(idPreg, Estatus, idverif) {
    AddEvaluacionCont(idPreg, Estatus, idverif )
}

function addEvNo(idPreg, Estatus, idverif) {
    AddEvaluacionCont(idPreg, Estatus, idverif)
}

function addEvNA(idPreg, Estatus, idverif) {
    AddEvaluacionCont(idPreg, Estatus, idverif)
}

function AddEvaluacionCont(idPreg, Estatus, idverif) {
    var OBJ_Evaluacion = VerificacionContClass;
    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());
    OBJ_Evaluacion.p_opt = 2;
    //OBJ_Evaluacion.p_id = '00000000-0000-0000-0000-000000000000';
    OBJ_Evaluacion.p_id = idverif;
    OBJ_Evaluacion.p_tbl_contrato_id = $('#IdContrato').val();
    OBJ_Evaluacion.p_tbl_usuario_id = $('#HDidUsuario').val();
    OBJ_Evaluacion.p_tbl_pregunta_formulario_id = idPreg;
    OBJ_Evaluacion.p_tbl_estatus_verificacion_id = Estatus;
    OBJ_Evaluacion.p_inclusion = date;

    //var form_data = new FormData();
    //form_data.append('EvaluacionP', JSON.stringify(OBJ_Evaluacion));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Evaluacion),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                //SuccessSA("Operación exitosa", objresponse[0].msg);
                //GetListaPuntosEvaluar($('#HDidDependencia').val(), $('#IdContrato').val());
            }
            else {
                ErrorSA("", objresponse[0].msg);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        //processData: false,
        //type: 'POST',
        url: ($('#EndPointAC').val() + "SerVerificacion/Add")

    })
    //GetListaPuntosEvaluar($('#HDidDependencia').val(), $('#IdContrato').val());
}

/**********************************************************Funciones para agregar la evaluacion**********************************************************/

var VerificacionContClass = {
    p_opt: null,
    p_id: null,
    p_tbl_contrato_id: null,
    p_tbl_usuario_id: null,
    p_tbl_pregunta_formulario_id: null,
    p_tbl_estatus_verificacion_id: null,
    p_inclusion: null,
}

var VerificacionContModelo = {
    p_opt: null,
    p_id: null,
    p_tbl_contrato_id: null,
    p_tbl_usuario_id: null,
    p_tbl_pregunta_formulario_id: null,
    p_tbl_estatus_verificacion_id: null,
    p_inclusion: null,
    p_fecha_verificacion: null,
    p_pregunta_personalizada:null
}




