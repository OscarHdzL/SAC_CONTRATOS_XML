$(document).ready(function () {

});
function GetAreasM() {
    var idDep = $('#IdDependencia').val();
    var idIns = $('#IdInstancia').val();

    $.get("/Request/Areas/Intancia/" + idIns + "/" + "Dependencia/" + idDep, function (data, status) {
        var Body = "<option value='' disabled selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].Value + "'>" + data[i].Text + "</option>";
        }
        $('.AreaM').html(Body);
    }, 'json');
}
function GetFuncionesDM() {
    $.get("/Request/Contrato/Funciones/GetLista", function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].Responsabilidad + "</option>";
        }
        $('.FuncionesM').html(Body);
    }, 'json');
}
//function GetFuncionesDM() {
//    var idcon = $('#IdContrato').val();
//    $.get("/Request/Contrato/Funciones/GetLista", function (data, status) {
//        var Body = "<option value='' disabled selected>Selecciona una opción</option>";
//        for (var i = 0; i <= data.length - 1; i++) {
//            Body = Body + "<option value='22'>Responsable de ejecución PE</option> <option value='21'>Responsable de ejecución PM</option>";
//        }
//        $('.FuncionesM').html(Body);
//    }, 'json');
//}
function GetPuestosM(idArea) {
    $.get("/Request/Contrato/Puestos/GetListaPuestos/" + idArea, function (data, status) {
        var Body = "<option value='' disabled selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].ID_PUESTO_PK + "'>" + data[i].PUESTO + "</option>";
        }
        $('.PuestoM').html(Body);
    }, 'json');
}

function GetFuncionariosM(idFun) {
    var rol2 = '';
    var idDep2 = $('#IdDependencia').val();
    $.get("/Request/Contrato/Funcionarios/rol/" + idFun, function (data, status) {
        for (var x = 0; x <= data.length - 1; x++) {
            rol2 = data[x].tbl_catRol_id;
        }
        var RolUser2 = rol2;
        $.get("/Request/Contrato/Funcionarios/GetListaFuncionarios/" + idDep2 + "/" + RolUser2, function (data, status) {
            var Body = "<option disabled selected>Selecciona una opción</option>";
            for (var i = 0; i <= data.length - 1; i++) {
                Body = Body + "<option value='" + data[i].ID_SERVIDOR_PUBLICO_PK + "'>" + data[i].NOMBRE + " " + data[i].AP_PATERNO + " " + data[i].AP_MATERNO + "</option>";
            }
            $('.FuncionarioM').html(Body);
        }, 'json');
    }, 'json');
}

//$('.FuncionesM').click(function () {
//    GetFuncionariosM($('.PuestoM').val())
//})
$('.AreaM').click(function () {
    $('#txtEmailM').val('');
    GetPuestosM($('.AreaM').val());
    GetFuncionariosM($('.PuestoM').val());
})
$('.PuestoM').click(function () {
    $('#txtEmailM').val('');
    GetFuncionariosM($('.PuestoM').val())
})
$('.FuncionarioM').click(function () {
    $('#txtEmailM').val('');
    var Email;
    var idP = $('.PuestoM').val();
    var idF = $('.FuncionarioM').val();
    $.get("/Request/Contrato/Funcionarios/GetListaFuncionariosEsp/" + idP + "/" + idF, function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {
            Email = data[i].EMAIL;
        }
        $('#txtEmailM').val(Email);
    }, 'json');
})

function cerrarMResp() {
    $('#ModalEditarResponsabilidades').modal('hide');
}
function ValidarM() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('.FuncionesM').val() == '') {
        Response.Texto = 'Debe seleccionar una Función';
        Response.Bit = true;
        return Response;
    }
    if ($('.AreaM').val() == '') {
        Response.Texto = 'Debe seleccionar un Área';
        Response.Bit = true;
        return Response;
    }
    if ($('.PuestoM').val() == '') {
        Response.Texto = 'Debe seleccionar un Puesto';
        Response.Bit = true;
        return Response;
    }
    if ($('.FuncionarioM').val() == '') {
        Response.Texto = 'Debe seleccionar un Funcionario';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtEmailM').val() == '') {
        Response.Texto = 'Debe Ingresar un Correo Electronico';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtEmailM').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Correo"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
function AddResponsabilidadesM() {
    var Validaciones = ValidarM();
    if (Validaciones.Bit) {
        return ErrorSA('Error en los datos de entrada', Validaciones.Texto);
    }

    //var d = new Date();
    //var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

    //var OBJ_S = ServidorPClassM;
    //var OBJ_P = PuestoSPClassM;
    //var OBJ_RAC = ResponsablesACClassM;
    //var OBJ_E = EmailM;

    //OBJ_S.id = '00000000-0000-0000-0000-000000000000';
    //OBJ_S.TBLENT_SERVIDOR_PUBLICO_id = $('#SerP').val();
    //OBJ_S.TBLENT_CONTRATO_id = $('#IdContrato').val();
    //OBJ_S.inclusion = date;
    //OBJ_S.estatus = 1;

    //OBJ_P.id = '00000000-0000-0000-0000-000000000000';
    //OBJ_P.tbl_responsabilidades_ac_id = $('#puesto').val();
    //OBJ_P.TBLENT_CONTRATO_id = $('#IdContrato').val();
    //OBJ_P.inclusion = date;
    //OBJ_P.estatus = 1;

    //OBJ_RAC.id = $('#IdResponsabilidad').val();
    //OBJ_RAC.TBLENT_CONTRATO_id = $('#IdContrato').val();
    //OBJ_RAC.tbl_RespApego_Puesto_ac_id = '00000000-0000-0000-0000-000000000000';
    //OBJ_RAC.tbl_RespApego_SerPub_ac_id = '00000000-0000-0000-0000-000000000000';
    //OBJ_RAC.inclusion = date;
    //OBJ_RAC.estatus = 1;

    //OBJ_E.Email = $('#txtEmailM').val();

    //var form_data = new FormData();
    //form_data.append('ServidorForm', JSON.stringify(OBJ_S));
    //form_data.append('PuestoForm', JSON.stringify(OBJ_P));
    //form_data.append('ResponsableForm', JSON.stringify(OBJ_RAC));
    //form_data.append('EmailForm', JSON.stringify(OBJ_E));

    //$.ajax({
    //    dataType: 'text',
    //    cache: false,
    //    contentType: false,
    //    processData: false,
    //    data: form_data,
    //    type: 'post',

    //    success: function (data) {
    //        var objresponse = JSON.parse(data);
    //        if (!objresponse.Bit) {
    //            function confirm() {
    //                return cerrarMResp();
    //            }
    //            var si = eval(confirm);
    //            SuccessSAAction("Operación exitosa", "El registro se modifico correctamente", si);
    //            $('#SerP').val('');
    //            $('#puesto').val('');
    //            $('#IdResponsabilidad').val('');
    //            GetResponsabilidadesAC();
    //        }
    //        else {
    //            ErrorSA("", objresponse.Descripcion);
    //        }
    //    },
    //    error: function () {
    //        var objresponse = JSON.parse(data);
    //        ErrorSA('', objresponse.Descripcion)
    //    },
    //    processData: false,
    //    type: 'POST',
    //    url: $("#EndPointAC").val() + 'update/email/' + $('#SerP').val(idpersona) +'/'+ $('#txtEmailM').val()

    //})

    $.post($("#EndPointAC").val() + 'SerResponsabilidad/update/email/' + $('#SerP').val() + '/' + $('#txtEmailM').val(), function (data, status) {
        if (data != null || data == undefined || data == "") {

            function confirm() {
                return cerrarMResp();
            }
            var si = eval(confirm);
            SuccessSAAction("Operación exitosa", "El registro se modifico correctamente", si);
            $('#SerP').val('');
            $('#puesto').val('');
            $('#IdResponsabilidad').val('');
            GetResponsabilidadesAC();


        } else {
            ErrorSA("", objresponse.Descripcion);
        }

    }, 'json');


}

$('#EditRAC').click(function () {
    function Confirmacion() {
        return AddResponsabilidadesM();
    }
    var AccionSi = eval(Confirmacion);
    function Negacion() {
        return $('#ModalEditarResponsabilidades').modal('hide');
    }
    var AccionNo = eval(Negacion)
    QuestionSA('¡Usted está a punto de modificar un registro...!', '¿En verdad desea continuar? ', 'Si, Continuar', 'No, Cancelar', AccionSi, AccionNo)
})

$('#CerrarM').click(function () {
    $('#SerP').val('');
    $('#puesto').val('');
    $('#IdResponsabilidad').val('');
})
/****************************************************************************************************************************************************/
var ServidorPClassM = {
    id: null,
    TBLENT_SERVIDOR_PUBLICO_id: null,
    TBLENT_CONTRATO_id: null,
    inclusion: null,
    estatus: null,
}
var PuestoSPClassM = {
    id: null,
    tbl_responsabilidades_ac_id: null,
    TBLENT_CONTRATO_id: null,
    inclusion: null,
    estatus: null,
}
var ResponsablesACClassM = {
    id: null,
    TBLENT_CONTRATO_id: null,
    tbl_RespApego_Puesto_ac_id: null,
    tbl_RespApego_SerPub_ac_id: null,
    inclusion: null,
    estatus: null,
}
var EmailM = {
    Email: null,
}
/****************************************************************************************************************************************************/
