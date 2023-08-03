$(document).ready(function () {
    AjusteTabla();
    getAcuerdos();
    //GetResponsables();
    //Inicializa campo fecha
    fechasIni();

});

function fechasIni(){
    $('.fechas').datetimepicker({
        format: 'YYYY-MM-DD'
    });
}

var idContrato = $('#idcontrato').val();

function AjusteTabla() {
    $('#tbl_Acuerdos').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "15%", "targets": 0 },
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 8 },
        ],
    });
}

function GetSanciones() {
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

function GetAcuerdos() {
    
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






function muestraModalAgregarAcuerdo() {

    $('#txt_FechaRegistro').data("DateTimePicker").clear();
    $('#txt_FechaCompromiso').data("DateTimePicker").clear();
    $('#txt_FechaCierre').data("DateTimePicker").clear();

    $('.Clean').val('');
    GetSanciones();
    GetAcuerdos();
    $('#ModalAgregarAcuerdo').modal('show');
}


function muestraModalEditar(id, contrato) {
    GetSanciones();
    GetAcuerdos();
    $('#idAcuerdo').val(id);
    $('#Modal_EditarAcuerdo').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_EditarAcuerdo').modal('show');

    $.get($('#EndPointAC').val() + "SerAcuerdos/Get/AcuerdoEditRC/" + id + "/" + contrato, function (data, status) {
        $('#TipoA_update > option[value="' + data.tipo_acuerdo_id + '"]').attr("selected", "selected");
        $('#ResponsableA_update > option[value="' + data.responsable_id + '"]').attr("selected", "selected");
        $('#txt_Acuerdo_update').val(data.acuerdo);

        fecha = data.fecha_registro.split('T');
        $('#txt_FechaRegistro_update').val(fecha[0]);        

        fecha1 = data.fecha_compromiso.split('T');
        $('#txt_FechaCompromiso_update').val(fecha1[0]);
        
        fecha2 = data.fecha_cierre.split('T');
        $('#txt_FechaCierre_update').val(fecha2[0]);
        
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

function getAcuerdos() {

    $.get($('#EndPointAC').val() + "SerAcuerdos/Get/" + idContrato, function (data, status) {
        var Arreglo_arreglos = [];
        var NombreResponsable = null;
        var fecha = null;
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
            Interno.push("<button class='btn btn-primary' title='Editar' onclick=\"muestraModalEditar('" + data[i].id + '\',\'' + idContrato + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar' onclick=\"DeleteItemAcuerdo('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");
            //Interno.push('<div align="center"><a class="btn btn-primary" title="Modificar registro" onclick="muestraModalEditar(\'' + data[i].id + '\')" ><span class="fa fa-edit"></span></a> <a class="btn btn-danger" title="Eliminar registro" onclick="DeleteItemAcuerdo(\'' + data[i].id + '\')" ><span class="fa fa-trash"></span></a></div>');
            Arreglo_arreglos.push(Interno);
        }

        var table = $('#tbl_Acuerdos').DataTable();

        table.destroy();
        //console.log(Arreglo_arreglos);
        $('#tbl_Acuerdos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "Tipo Acuerdo" },
                { title: "Responsable" },
                { title: "Acuerdo" },
                { title: "Fecha de registro" },
                { title: "Fecha de compromiso" },
                { title: "Fecha de cierre" },
                { title: "Estatus" },
                { title: "Comentarios" },
                { title: "Acción      " }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
    });
}


$("#GuardarAcuerdo").click(function () {
    AddAcuerdo();
});

function AddAcuerdo() {
    var Validacion = ValidarAcuerdo();
    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);

    }
    else {
        var OBJ_AcuerdoForm = AcuerdoClass;
        var d = new Date();
        var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());
        OBJ_AcuerdoForm.p_opt = 2;
        OBJ_AcuerdoForm.p_id = '00000000-0000-0000-0000-000000000000';
        OBJ_AcuerdoForm.p_tbl_contrato_id = $('#idcontrato').val();
        OBJ_AcuerdoForm.p_tbl_contrato_servidor_resp_id = $('#ResponsableA').val();
        OBJ_AcuerdoForm.p_tbl_tipo_acuerdo_id = $('#TipoA').val();
        OBJ_AcuerdoForm.p_acuerdo = $('#txt_Acuerdo').val();
        OBJ_AcuerdoForm.p_fecha_registro = $('#txt_FechaRegistro').val();
        OBJ_AcuerdoForm.p_fecha_compromiso = $('#txt_FechaCompromiso').val();
        OBJ_AcuerdoForm.p_fecha_cierre = $('#txt_FechaCierre').val();
        OBJ_AcuerdoForm.p_estatus_acuerdo = $('#cmbEstatusAcuerdo_').val();
        OBJ_AcuerdoForm.p_comentario = $('#txt_ComentarioAcuerdo').val();
        //OBJ_AcuerdoForm.Inclusion = date;
        OBJ_AcuerdoForm.p_estatus = 1;
        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_AcuerdoForm),
            type: 'post',

            success: function (data) {
                var objresponse = JSON.parse(data);
                if (objresponse[0].cod == "success") {
                    SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                    getAcuerdos();
                    $('#ModalAgregarAcuerdo').modal('hide');
                }
                else {
                    ErrorSA("", objresponse[0].msg);
                }
            },
            error: function (data) {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse[0].msg)
            },
            processData: false,
            type: 'POST',
            url: ($('#EndPointAC').val() + "SerAcuerdos/add")
            //url: "https://localhost:44359/" + "acuerdos/add"            

        })
    }
}


function elimina(item) {
        var OBJ_AcuerdoForm = AcuerdoClass;
        OBJ_AcuerdoForm.p_opt = 4;
        OBJ_AcuerdoForm.p_id = item; 
        OBJ_AcuerdoForm.p_estatus = 0;
        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_AcuerdoForm),
            type: 'delete',

            success: function (data) {
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", "El registro se eliminado correctamente");
                    getAcuerdos();
                    $('#ModalEditarAcuerdo').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: ($('#EndPointAC').val() + "SerAcuerdos/delete")
        })    
}



function DeleteItemAcuerdo(item) {
    function Confirmacion() {
        return elimina(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?", "Si", "No", AccionSi, "");
}



function ValidarAcuerdo() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#TipoA').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de acuerdo';
        Response.Bit = true;
        return Response;
    }
    if ($('#ResponsableA').val() == null) {
        Response.Texto = 'Debe seleccionar un responsable';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Acuerdo').val() == '') {
        Response.Texto = 'Debe agregar un acuerdo';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Acuerdo').val(), 'Acuerdo') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Acuerdo"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaRegistro').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de registro';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaCompromiso').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de compromiso';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaCierre').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de cierre';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmbEstatusAcuerdo_').val() == '') {
        Response.Texto = 'Debe seleccionar un estatus';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_ComentarioAcuerdo').val() == '') {
        Response.Texto = 'Debe agregar un comentario';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_ComentarioAcuerdo').val(), 'Comentario') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Comentario"';
        Response.Bit = true;
        return Response;
    }
    
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}



var AcuerdoClass = {
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