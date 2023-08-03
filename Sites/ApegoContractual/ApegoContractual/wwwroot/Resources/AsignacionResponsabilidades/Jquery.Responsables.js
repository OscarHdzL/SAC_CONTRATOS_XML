var responsables_global = '';

$.extend($.fn.dataTable.defaults, {
    responsive: true
});
$(document).ready(function () {
    LaunchLoader(true);
    $('#ResponsablesAC').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "1%", "targets": 0 },
            { "width": "1%", "targets": 1 },
            { "width": "30%", "targets": 2 },
            { "width": "30%", "targets": 3 },
            { "width": "30%", "targets": 4 },
            { "width": "8%", "targets": 5 },

            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
        ],
    });

    $('#Responsabilidades').DataTable();

    setInterval('Redimension()', 500);
    //GetResponsabilidadesAC();
    fillDependencia();
    $('.Puesto').prop("disable", true);
    $('.Funcionario').prop("disable", true);
    LaunchLoader(false);

});

function fillDependencia() {
    var uri = $('#EndPointAC_Admon').val() + 'Contratos/GetContratoporid/' + $('#IdContrato').val();
    $.get(uri, function (data) {
        $("#dependenciaContrato").val(data[0].p_tbl_dependencia_id);
        GetResponsabilidadesAC();
    });
}


function Redimension() {
    try {
        var tables = document.getElementsByTagName('table');
        for (var i = 0; i < tables.length; i++) {
            $('#' + tables[i].id + '').resize();
        }
    }
    catch (err) { }
}

$('#RegistroResponsabilidades').click(function () {
    $('#ModalAddFuncion').modal({ backdrop: 'static', keyboard: false });
    $('#ModalAddFuncion').modal('show');
})

//**********************************Llenado de los drop**************************************//
function GetAreas() {
    var idDep = $('#dependenciaContrato').val();
    var idIns = $('#IdInstancia').val();

    $.get("/Request/Areas/Intancia/" + idIns + "/" + "Dependencia/" + idDep, function (data, status) {

        var Body = "<option disabled selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].Value + "'>" + data[i].Text + "</option>";
        }
        $('.Area').html(Body);
    }, 'json');
}

function GetPuestos(idA) {
    $.get("/Request/Contrato/Puestos/GetListaPuestos/" + idA, function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].ID_PUESTO_PK + "'>" + data[i].PUESTO + "</option>";
        }
        $('.Puesto').html(Body);
    }, 'json');
}

function GetFuncionarios(idDep) {
    var idFun = $('.Funciones').val();
    var rol = '';

    //$.get("/Request/Contrato/Funcionarios/GetListaFuncionarios/" + idDep + "/" + RolUser, function (data, status) {
    var url = $("#EndPointAC").val() + "SerServidorPublico/Get/sigla/" + $('.Funciones').val() + "/dependencia/" + idDep;
    $.get(url, function (data, status) {
        responsables_global = data;

        var Body = "<option disabled selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].tbl_rol_usuario_id + "'>" + data[i].nombrecompleto + "</option>";
        }
        $('.Funcionario').html(Body);
    }, 'json');

}

$('.Funciones').change(function () {
    $('#txtEmail').val('');

    GetFuncionarios($('#dependenciaContrato').val())
    $('#txtEmail').val('');
})

$('.Funcionario').change(function () {
    var Email;
    var idF = $('.Funcionario').val();

    for (var i = 0; i <= responsables_global.length - 1; i++) {

        if (idF == responsables_global[i].tbl_rol_usuario_id) {
            Email = responsables_global[i].email;

            if (Email != null) {

                $('#txtEmail').val(Email);
                $('#txtEmail').prop('disabled', true);
            }
            else {
                $('#txtEmail').prop('disabled', false);
            }

        }

    }

})
//******************************Fin del Llenado de los drop**********************************//
//$('#AddFuncion, #EditFuncionF').click(function () {
//    AddFunciones();
//})
//function AddFunciones() {
//    var OBJ_Funcion = FuncionesClass;
//    var d = new Date();
//    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

//    if ($('#IdFuncion').val() == '') {

//        if ($('#txtFuncion').val() == '') {
//            $('#txtFuncion').val('');
//            return ErrorSA('Error en los datos de entrada', 'Debe completar el campo "Responsabilidades"');

//        }
//        else if (ValidaCadena($('#txtFuncion').val(), '') != '') {
//            $('#txtFuncion').val('');
//            return ErrorSA('Error en los datos de entrada', 'No se permiten caracteres especiales en el campo "Responsabilidades"');

//        }
//        OBJ_Funcion.id = '00000000-0000-0000-0000-000000000000';
//        OBJ_Funcion.Responsabilidad = $('#txtFuncion').val();

//    }
//    else {

//        if ($('#txtFuncionF').val() == '') {
//            $('#txtFuncionF').val('');
//            return ErrorSA('Error en los datos de entrada', 'Debe completar el campo "Responsabilidades"');

//        }
//        else if (ValidaCadena($('#txtFuncionF').val(), '') != '') {
//            $('#txtFuncionF').val('');
//            return ErrorSA('Error en los datos de entrada', 'No se permiten caracteres especiales en el campo "Responsabilidades"');

//        }


//        OBJ_Funcion.id = $('#IdFuncion').val();
//        OBJ_Funcion.Responsabilidad = $('#txtFuncionF').val();
//    }

//    OBJ_Funcion.Inclusion = date;
//    OBJ_Funcion.Estatus = 1;

//    var form_data = new FormData();
//    form_data.append('FuncionesForm', JSON.stringify(OBJ_Funcion));

//    $.ajax({
//        dataType: 'text',
//        cache: false,
//        contentType: false,
//        processData: false,
//        data: form_data,
//        type: 'post',

//        success: function (data) {
//            var objresponse = JSON.parse(data);
//            if (!objresponse.Bit) {
//                SuccessSA("Operación exitosa", "El registro se guardó correctamente");
//                $('#txtFuncion').val('');
//                GetFunciones();
//                $('#IdFuncion').val('');

//            }
//            else {
//                ErrorSA("", objresponse.Descripcion);
//            }

//        },
//        error: function (data) {
//            var objresponse = JSON.parse(data);
//            ErrorSA('', objresponse.Descripcion)
//        },
//        processData: false,
//        type: 'POST',
//        url: '/Request/Contrato/AddResponsabilidades'

//    })
//}

function btnEditF(id, Resp) {
    $('#ModalEditF').modal({ backdrop: 'static', keyboard: false });
    $('#ModalEditF').modal('show');

    $('#txtFuncionF').val(Resp);
    $('#IdFuncion').val(id);
}
function EliminarF(item) {

    //$.post("/Request/Contrato/Delete/" + item, function (data, status) {
    //    var objresponse = JSON.parse(data);
    //    if (status == 'success') {
    //        SuccessSA('', 'El registro se eliminó exitosamente');
    //        GetFunciones();
    //    }
    //    else {
    //        ErrorSA('', objresponse.Descripcion);
    //    }
    //});



    var OBJ_RAC_ = ResponsablesACClass;
    OBJ_RAC_.p_opt = 4;
    OBJ_RAC_.p_id = item;
    OBJ_RAC_.p_tbl_contrato_id = '00000000-0000-0000-0000-000000000000';
    OBJ_RAC_.p_tbl_rol_usuario_id = '00000000-0000-0000-0000-000000000000';
    OBJ_RAC_.p_inclusion = '2020-01-01';
    OBJ_RAC_.p_estatus = 0;


    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_RAC_),
        type: 'delete',
        url: $("#EndPointAC").val() + "SerRespApego/delete",

        success: function (data) {
            if (data != null) {
                SuccessSA('', 'El registro se eliminó exitosamente');
                GetFunciones();
            }
            else {
                ErrorSA('', "No se pudo eliminar");
            }
        },
        error: function (data) {

            ErrorSA('', objresponse.Descripcion)
        },

    });


}
function btnDeleteF(item) {
    function Confirmacion() {
        return EliminarF(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

var FuncionesClass = {
    p_opt: null,
    p_id: null,
    p_tbl_contrato_id: null,
    p_inclusion: null,
    p_estatus: null,
    p_tbl_rol_usuario_id: null
}




/**********************************************************************************************************************************************************************/
function Validar() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('.Funciones').val() == null) {
        Response.Texto = 'Debe seleccionar una responsabilidad';
        Response.Bit = true;
        return Response;
    }
    //if ($('.Area').val() == null) {
    //    Response.Texto = 'Debe seleccionar un Área';
    //    Response.Bit = true;
    //    return Response;
    //}
    //if ($('.Puesto').val() == null) {
    //    Response.Texto = 'Debe seleccionar un Puesto';
    //    Response.Bit = true;
    //    return Response;
    //}
    if ($('.Funcionario').val() == null) {
        Response.Texto = 'Debe seleccionar un Funcionario';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtEmail').val() == '') {
        Response.Texto = 'Debe Ingresar un Correo Electronico';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtEmail').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Correo"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
function AddResponsabilidades() {
    var Validaciones = Validar();

    if (Validaciones.Bit) {
        return ErrorSA('Error en los datos de entrada', Validaciones.Texto);
    }

    var idFuncionario = $('.Funcionario').val();

    var objBusqueda = $('#ResponsablesAC').DataTable().data().toArray();

    for (var indice in objBusqueda) {

        if (objBusqueda[indice][0] == idFuncionario) { //[4] es el id del producto
            ErrorSA('Error', 'El funcionario ya se registró anteriormente');
            return;
        }

    }

    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

    var OBJ_S = ServidorPClass;
    var OBJ_P = PuestoSPClass;
    var OBJ_RAC = ResponsablesACClass;
    var OBJ_E = Email;

    //OBJ_S.id = '00000000-0000-0000-0000-000000000000';
    //OBJ_S.TBLENT_SERVIDOR_PUBLICO_id = $('.Funcionario').val();
    //OBJ_S.TBLENT_CONTRATO_id = $('#IdContrato').val();
    //OBJ_S.inclusion = date;
    //OBJ_S.estatus = 1;

    //OBJ_P.id = '00000000-0000-0000-0000-000000000000';
    //OBJ_P.tbl_responsabilidades_ac_id = $('.Funciones').val();
    //OBJ_P.TBLENT_CONTRATO_id = $('#IdContrato').val();
    //OBJ_P.inclusion = date;
    //OBJ_P.estatus = 1;

    OBJ_RAC.p_opt = 2;
    OBJ_RAC.p_id = '00000000-0000-0000-0000-000000000000';
    OBJ_RAC.p_tbl_contrato_id = $('#IdContrato').val();
    OBJ_RAC.p_tbl_rol_usuario_id = $('.Funcionario').val();
    OBJ_RAC.p_inclusion = date;
    OBJ_RAC.p_estatus = 1;

    OBJ_E.Email = $('#txtEmail').val();

    //var form_data = new FormData();
    //form_data.append('ServidorForm', JSON.stringify(OBJ_S));
    //form_data.append('PuestoForm', JSON.stringify(OBJ_P));
    //form_data.append('ResponsableForm', JSON.stringify(OBJ_RAC));
    //form_data.append('EmailForm', JSON.stringify(OBJ_E));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_RAC),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", "El registro se guardó correctamente");
                $('#txtEmail').val('');
                $('#IdResponsabilidad').val('');
                $('.Funciones').val('');
                //GetFuncionesD();
                //$('.Funcionario').val('');
                $('#Funcionario').empty();
                GetResponsabilidadesAC();
                $('.Puesto').prop('disebled', false);
                $('.Funcionario').prop('disebled', false);
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        processData: false,
        type: 'POST',
        //url: '/Request/Contrato/AddResponsablesApego'
        url: $("#EndPointAC").val() + "SerRespApego/add"

    })
}
$('#AddResponsable').click(function () {
    AddResponsabilidades();
})

function GetResponsabilidadesAC(idCon) {
    var idCon = $('#IdContrato').val();

    $.get($("#EndPointAC").val() + "SerRespApego/Get/ResponsablesApego/Responsabilidad/" + idCon, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            Interno.push(data[i].tbl_rol_usuario_id);
            Interno.push(i + 1);
            Interno.push(data[i].funcionario);
            if (data[i].funcion == 'Responsable ejecutor del PE') {
                Interno.push('Supervisor de entregas');
            } else {
                Interno.push(data[i].funcion);
            }
            Interno.push(data[i].correo);
            Interno.push('<a onclick="btnEditResp(\'' + data[i].tbl_persona_id + '\',\'' + data[i].funcion + '\',\'' + data[i].correo + '\')" class="fa fa-pencil-square-o btn btn-primary" title="Modificar registro"> </a> <a onclick="btnDeleteResp(\'' + data[i].tbl_contrato_servidor_resp + '\')" class="fa fa-trash-o btn btn-danger" title="Eliminar registro"> </a>');
            Arreglo_arreglos.push(Interno);
        }
        var table = $('#ResponsablesAC').DataTable();

        table.destroy();
        console.log(Arreglo_arreglos);
        $('#ResponsablesAC').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "Id" },
                { title: "No." },
                { title: "Funcionario" },
                { title: "Funciones" },
                { title: "Correo" },
                { title: "Acciones" },
            ],
            order: [[1, "asc"]],
            columnDefs: [
                { "className": "dt-center", "targets": "_all" },
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },

            ],
            order: [[1, "asc"]],
        });
        LaunchLoader(false);
    });
}

function btnEditResp(idpersona, idFun, correo) {

    $('#ModalEditarResponsabilidades').modal({ backdrop: 'static', keyboard: false });
    $('#ModalEditarResponsabilidades').modal('show');
    $('#txtEmailM').val(correo);
    $('#puesto').val(idFun)
    //$('#SerP').val(idser);
    $('#SerP').val(idpersona);
    var idCon = $('#IdContrato').val();


}

function EliminarResp(item) {
    LaunchLoader(true);

    var OBJ_RAC_ = ResponsablesACClass;
    OBJ_RAC_.p_opt = 4;
    OBJ_RAC_.p_id = item;
    OBJ_RAC_.p_tbl_contrato_id = '00000000-0000-0000-0000-000000000000';
    OBJ_RAC_.p_tbl_rol_usuario_id = '00000000-0000-0000-0000-000000000000';
    OBJ_RAC_.p_inclusion = '2020-01-01';
    OBJ_RAC_.p_estatus = 0;


    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_RAC_),
        type: 'delete',
        url: $("#EndPointAC").val() + "SerRespApego/delete",

        success: function (data) {
            if (data != null) {
                SuccessSA('', 'El registro se eliminó exitosamente');
                GetResponsabilidadesAC();
            }
            else {
                ErrorSA('', "No se pudo eliminar");
            }
        },
        error: function (data) {

            ErrorSA('', data[0].msg)
        },

    });

    LaunchLoader(false);
}

function btnDeleteResp(item) {
    function Confirmacion() {
        return EliminarResp(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}


var ServidorPClass = {
    id: null,
    TBLENT_SERVIDOR_PUBLICO_id: null,
    TBLENT_CONTRATO_id: null,
    inclusion: null,
    estatus: null,
}
var PuestoSPClass = {
    id: null,
    tbl_responsabilidades_ac_id: null,
    TBLENT_CONTRATO_id: null,
    inclusion: null,
    estatus: null,
}
var ResponsablesACClass = {

    p_opt: null,
    p_id: null,
    p_tbl_contrato_id: null,
    p_inclusion: null,
    p_estatus: null,
    p_tbl_rol_usuario_id: null

}
var Email = {
    Email: null,
}
/**********************************************************************************************************************************************************************/
function CerrarMFun() {
    $('#ModalAddFuncion').modal('hide');
}
function Validar2() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txtFuncionF').val() == '') {
        Response.Texto = 'Debe ingresar una responsabilidad';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('#txtFuncionF').val(), '') != '') {
        Response.Texto = 'El campo "Función" no debe contener caracteres especiales';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
function AddResponsabilidad() {
    var Validaciones = Validar2();
    if (Validaciones.Bit) {
        return ErrorSA('Error en los datos de entrada', Validaciones.Texto);
    }

    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

    var OBJ_F = Responsabilidades;
    OBJ_F.id = '00000000-0000-0000-0000-000000000000';
    OBJ_F.Responsabilidad = $('#txtFuncionF').val();
    OBJ_F.TBLENT_CONTRATO_ID = $('#IdContrato').val();
    OBJ_F.Inclusion = date;
    OBJ_F.Estatus = 1;


    var form_data = new FormData();
    form_data.append('FuncionesForm', JSON.stringify(OBJ_F));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data,
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                function conf() {
                    GetFuncionesD();
                    return CerrarMFun();
                }
                var Si = eval(conf);
                SuccessSAAction("Operación exitosa", "El registro se guardó correctamente", Si);

            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        processData: false,
        type: 'POST',
        url: '/Request/Contrato/AddResponsabilidades'

    })
}
$('#GuardarFun').click(function () {
    AddResponsabilidad();
})
var Responsabilidades = {
    id: null,
    Responsabilidad: null,
    TBLENT_CONTRATO_ID: null,
    Inclusion: null,
    Estatus: null,
}



function AddResponsabilidadesM() {
    var Validaciones = ValidarM();
    if (Validaciones.Bit) {
        return ErrorSA('Error en los datos de entrada', Validaciones.Texto);
    }



    $.post($("#EndPointAC").val() + 'SerResponsabilidad/update/email/' + $('#SerP').val() + '/' + $('#txtEmailM').val(), function (data, status) {
        if (data != null) {

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



function ValidarM() {
    var Response = { Texto: '', Bit: true, objeto: null };

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

function cerrarMResp() {
    $('#ModalEditarResponsabilidades').modal('hide');
}