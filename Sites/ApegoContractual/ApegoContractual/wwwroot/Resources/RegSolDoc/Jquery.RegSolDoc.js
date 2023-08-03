var Responsables = null;
var datosparacorreo = null;

$(document).ready(function () {
    GetResponsables();
    GetSolicitud($('#idcontrato').val());

    $('#txt_fsolic').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    $('#txt_fentrega').datetimepicker({
        format: 'YYYY-MM-DD'
    });
});

function GetResponsables() {
    var idcon = $('#idcontrato').val();
    $.get($('#EndPointAC').val() + "SerServidorPublico/Get/sigla/All/Contrato/" + idcon, function (data, status) {
        Responsables = data;
        reestablecerResponsables();
    }, 'json');
}

function reestablecerResponsables() {
    console.log(Responsables);
    var Body = "<option disabled selected>Selecciona una opción</option>";
    for (var i = 0; i <= Responsables.length - 1; i++) {
        Body = Body + "<option value='" + Responsables[i].tbl_contrato_servidor_resp_id + "'>" + Responsables[i].nombre + " " + Responsables[i].ap_paterno + " " + Responsables[i].ap_materno +"</option>";
    }
    $('#nomresp').html(Body);
}

$('#AddRegistro').click(function () {
    $("#tituloM").html("Agregar nueva solicitud");
    $('#ModalNuevaSolicitud').modal({ backdrop: 'static', keyboard: false });
    $('#ModalNuevaSolicitud').modal('show');
    $('.Clean').val('');
    $('#IdMR').val('');
    GetResponsables();
})

$('#nomresp').change(function () {
    for (var i = 0; i <= Responsables.length - 1; i++) {
        if (Responsables[i].tbl_contrato_servidor_resp_id === $('#nomresp').val()) {
            $('#txtarearesp').val(Responsables[i].area);
            $('#txtcorreoresp').val(Responsables[i].email);
            $('#idarearesp').val(Responsables[i].tbl_area_id);

        }
    }
});



function Validar() {

    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txtnomdoc').val() == '') {
        Response.Texto = 'Debe agregar un nombre de documento';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtnomdoc').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Nombre Documento"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtarearesp').val() == '') {
        Response.Texto = 'Debe seleccionar un responsable';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_fsolic').val() == '') {
        Response.Texto = 'Debe agregar fecha de solicitud';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_fentrega').val() == '') {
        Response.Texto = 'Debe agregar fecha de entrega';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmbTipoEntrega').val() == '') {
        Response.Texto = 'Debe seleccionar un tipo de entrega';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtobs').val() == '') {
        Response.Texto = 'Debe agregar observaciones';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtobs').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Observaciones"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtcorrsol').val() == '') {
        Response.Texto = 'Debe agregar un correo electronico del solicitante';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

$('#AddNuevaS').click(function () {
    AddSolicitud();
})


function AddSolicitud() {
    var Validacion = Validar();
    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }           
    if ($('#IdMR').val() == '') {        
        NuevoReg();
    }
    else {        
        EditReg();
    };
}

function NuevoReg() {
    var d = new Date(); 
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());
    var OBJ_Solicitud = SolicitudClass;
    OBJ_Solicitud.p_id = '00000000-0000-0000-0000-000000000000';
    OBJ_Solicitud.p_opt = 2;
    OBJ_Solicitud.p_tbl_contrato_id = $('#idcontrato').val();
    OBJ_Solicitud.p_nombre_documento = $('#txtnomdoc').val();
    OBJ_Solicitud.p_tbl_contrato_servidor_resp_id = $('#nomresp').val();
    OBJ_Solicitud.p_fecha_solicitud = $('#txt_fsolic').val();
    OBJ_Solicitud.p_fecha_entrega = $('#txt_fentrega').val();
    OBJ_Solicitud.p_tipo_entregable = $('#cmbTipoEntrega').val();
    OBJ_Solicitud.p_observacion = $('#txtobs').val();
    OBJ_Solicitud.p_correo_solicitud = $('#txtcorrsol').val();
    OBJ_Solicitud.p_inclusion = date;
    OBJ_Solicitud.p_estatus = 1;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Solicitud),
        type: 'post',
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", "El registro se guardó correctamente");
                $('.Clean').val('');
                GetSolicitud($('#idcontrato').val());
                $('#ModalNuevaSolicitud').modal('hide');
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
        url: ($('#EndPointAC').val() + "SerRegSolDoc/add")
    })
}
function EditReg() {
    var d = new Date(); 
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());
    var OBJ_Solicitud = SolicitudClass;
    OBJ_Solicitud.p_id = $('#IdMR').val();
    OBJ_Solicitud.p_opt = 3;
    OBJ_Solicitud.p_tbl_contrato_id = $('#idcontrato').val();
    OBJ_Solicitud.p_nombre_documento = $('#txtnomdoc').val();
    OBJ_Solicitud.p_tbl_contrato_servidor_resp_id = $('#nomresp').val();
    OBJ_Solicitud.p_fecha_solicitud = $('#txt_fsolic').val();
    OBJ_Solicitud.p_fecha_entrega = $('#txt_fentrega').val();
    OBJ_Solicitud.p_tipo_entregable = $('#cmbTipoEntrega').val();
    OBJ_Solicitud.p_observacion = $('#txtobs').val();
    OBJ_Solicitud.p_correo_solicitud = $('#txtcorrsol').val();
    OBJ_Solicitud.p_inclusion = date;
    OBJ_Solicitud.p_estatus = 1;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Solicitud),
        type: 'put',
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", "El registro se actualizó correctamente");
                $('.Clean').val('');
                GetSolicitud($('#idcontrato').val());
                $('#ModalNuevaSolicitud').modal('hide');
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
        //type: 'PUT',
        url: ($('#EndPointAC').val() + "SerRegSolDoc/Put")
    })
}



$('#cerrarMMR').click(function () {
    $('.Clean').val('');
})

function GetSolicitud(idCon) {

    $.get($('#EndPointAC').val() + "SerRegSolDoc/Get/Lista/" + idCon, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            Interno.push(i + 1);
            Interno.push(data[i].nombre_documento);
            Interno.push(data[i].area);
            Interno.push(data[i].nombre_responsable);
            Interno.push(data[i].correo_responsable);
            var fechasol = (data[i].fecha_solicitud).split('T');
            var fechaent = (data[i].fecha_entrega).split('T');
            Interno.push(fechasol[0]);
            Interno.push(fechaent[0]);
            Interno.push(data[i].tipo_entregable);
            Interno.push(data[i].observacion);
            Interno.push(data[i].correo_solicitud);
            Interno.push('<a onclick="btnEnviarCorreo(\'' + data[i].id + '\')" class="fa fa-envelope btn btn-primary" title="Enviar correo"> </a> <a onclick="btnEditM(\'' + data[i].id + '\')" class="fa fa-pencil-square-o btn btn-primary" title="Modificar registro"> </a>');// <a onclick="btnDeleteM(\'' + data[i].id + '\')" class="fa fa-trash-o btn btn-danger" title="Eliminar registro"> </a>');
            Arreglo_arreglos.push(Interno);
        }



        var table = $('#tbl_SolicitudDoc').DataTable();

        table.destroy();
        console.log(Arreglo_arreglos);
        $('#tbl_SolicitudDoc').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "ID" },
                { title: "Nombre documento" },
                { title: "Área responsable" },
                { title: "Nombre del responsable" },
                { title: "Correo responsable" },
                { title: "Fecha solicitud" },
                { title: "Fecha entrega" },
                { title: "Tipo de entrega" },
                { title: "Observaciones / Comentarios" },
                { title: "Correo solicitante" },
                { title: "Acciones" }
            ]
        });

    });

}



function btnEditM(item) {
    //$('.Clean').val('');
    $('#IdMR').val(item);
    $("#tituloM").html("Modificar solicitud");
    //$('#AddNuevaS').text('Editar');
    $('#ModalNuevaSolicitud').modal({ backdrop: 'static', keyboard: false });
    $('#ModalNuevaSolicitud').modal('show');
    
    var idCon = $('#idcontrato').val();
    $.get($('#EndPointAC').val() + "SerRegSolDoc/Get/Solicitudedit/" + idCon + "/" + item, function (data, status) {
    //$.get("https://localhost:44359/regsoldoc/Get/Solicitudedit/" + idCon + "/" + item, function (data, status) {
        //for (var i = 0; i <= data.length - 1; i++) {
            $('#txtnomdoc').val(data.nombre_documento);
            $('#idarearesp').val(data.area_id_responsable);
            $('#txtarearesp').val(data.area_responsable);
            $('#txtcorreoresp').val(data.correo_responsable);
            var fechasol = (data.fecha_solicitud).split('T');
            $('#txt_fsolic').val(fechasol[0]);
            var fechaent = (data.fecha_entrega).split('T');
            $('#txt_fentrega').val(fechaent[0]);
            $('#cmbTipoEntrega').val(data.tipo_entregable);
            //$('#cmbTipoEntrega > option[value="' + data[i].tipo_entregable + '"]').attr("selected", "selected");
            $('#txtobs').val(data.observacion);
            $('#txtcorrsol').val(data.correo_solicitud);  
            
            reestablecerResponsables();
            $('#nomresp > option[value="' + data.tbl_contrato_servidor_resp_id + '"]').attr("selected", "selected");
        //}
    });

}


//function EliminarSol(item) {
//    $.post($('#EndPointAC').val() + "Request/SolRegDoc/Delete/" + item, function (data, status) {
//        var objresponse = JSON.parse(data);
//        if (status == 'success') {
//            SuccessSA('', 'El registro se eliminó exitosamente');
//            GetSolicitud($('#idcontrato').val());
//        }
//        else {
//            ErrorSA('', objresponse.Descripcion);
//        }
//    });
//}

//function btnDeleteM(item) {
//    function Confirmacion() {
//        return EliminarSol(item);
//    }
//    var AccionSi = eval(Confirmacion);
//    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
//}

var SolicitudClass = {
    p_opt: null,
    p_id: null,
    p_tbl_contrato_id: null,
    p_nombre_documento: null,
    //TBLENT_AREA_id: null,
    p_tbl_contrato_servidor_resp_id: null,
    p_fecha_solicitud: null,
    p_fecha_entrega: null,
    p_tipo_entregable: null,
    p_observacion: null,
    p_correo_solicitud: null,
    p_inclusion: null,
    p_estatus: null,
}


var SolicEmail = {
    body: null,
    email: null,
    asunto: null,
}

function btnEnviarCorreo(item) {
    function Confirmacion() {
        return enviarcorreo(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea enviar correo?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function enviarcorreo(item) {
    $('#IdMR').val(item);
    var idCon = $('#idcontrato').val();
    $.get($('#EndPointAC').val() + "SerRegSolDoc/Get/Solicitudedit/" + idCon + "/" + item, function (data, status) {
        //for (var i = 0; i <= data.length - 1; i++) {
            var correo = [];
            correo.push(data.correo_responsable);
            correo.push(data.correo_solicitud);

            var fechasol = (data.fecha_solicitud).split('T');
            var fechaent = (data.fecha_entrega).split('T');
                       
            var OBJ_Email = SolicEmail;
            OBJ_Email.body = 'Se solicita el registro del documento "' + data.nombre_documento + '", de tipo: ' + data.tipo_entregable + ' dirigido al responsable : ' + data.nombre_responsable + ', del área: ' + data.area_responsable + ', con fecha de solicitud: ' + fechasol[0] + ' y fecha de entrega: ' + fechaent[0] + '.';
            OBJ_Email.email = correo;
            OBJ_Email.asunto = 'Solicitud de Registro de Documento '


            $.ajax({
                dataType: 'text',
                cache: false,
                contentType: 'application/json',
                processData: false,
                data: JSON.stringify(OBJ_Email),
                type: 'post',

                success: function (data) {
                    SuccessSA("Operación exitosa", "Email enviado");
                },
                error: function () {
                    var objresponse = JSON.parse(data);
                    ErrorSA('', objresponse.Descripcion)
                },
                processData: false,
                type: 'POST',
                url: ($('#EndPointAC').val() + "SerEnvioCorreo/Send/correo")

            })

        //}
    });
}

