$(document).ready(function () {
    LaunchLoader(true);
    GetSancionesM();
    GetProductoServicioM();
    GetPeriodosM();
    GetTipoObligacionM();
    GetPrioridadM();
   
});


///cmb_sancion_index
$(".cmb_sancion_indexM").change(function () {
    var Sancion = $(".cmb_sancion_indexM option:selected").text().toLowerCase();
    if (Sancion == "deductiva") {

        $('#tipoSanM').text('Indique el porcentaje de la sanción');
        $('.DivTipoSanM').html('<input type="text" class="form-control clean" id ="PorcentajeSanM"> <span class="input-group-addon">%</span>')
    }
    else if (Sancion == "convencional") {

        $('#tipoSanM').text('Indique el Monto de la sanción');
        $('.DivTipoSanM').html('<span class="input-group-addon">$</span> <input type="text" class="form-control clean" id="MontoSanM">')
        SeparadoresM();
    }
    else {

        $('#tipoSanM').text('');
        $('.DivTipoSanM').html('')
    }
});

//Auxiliares
function SeparadoresM() {
    $('#MontoSanM').keyup(function (event) {
        if (event.which >= 37 && event.which <= 40) {
            event.preventDefault();
        }

        $(this).val(function (index, value) {
            return value
                .replace(/\D/g, "")
                .replace(/([0-9])([0-9]{2})$/, '$1.$2')
                .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",")
                ;
        });
    });
}
//Auxiliares llenado de Drops
function GetSancionesM() {
    $.get($('#EndPointAC').val() + "SerSancion/Get", function (data, status) {
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].sansion + "</option>";
        }
        $('.cmb_sancion_indexM').html(body);
        $(".cmb_sancion_indexM").prop('disabled', true);
        
    });
    return;
}
function GetPeriodosM() {
    $.get($('#EndPointAC').val() + "SerObligacion/Get/Periodos", function (data, status) {
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].periodo + "</option>";
        }
        $('.cmb_periodo_indexM').html(body);
        $(".cmb_periodo_indexM").prop('disabled', true);
    });
    return;
}
function GetProductoServicioM() {

    $.get($('#EndPointAC').val() + "SerContratoProductos/Get/Dropdown/" + $('#idContrato').val(), function (data, status) {
        //$('#InstanciaObj').val(JSON.stringify(data));
        var body = "<option value='-1'>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('.cmb_ProdServ_indexM').html(body);
        $(".cmb_ProdServ_indexM").prop('disabled', true);
    });
    return;
}

function GetDependenciasContratoM() {
    var contratoid = $('#idContrato').val();
    $.get($('#EndPointAC_Admon').val() + "Contratos/GetContratoporid/" + contratoid, function (data, status) {
        //$('#InstanciaObj').val(JSON.stringify(data));
        var iddependencia;
        for (var i = 0; i <= data.length - 1; i++) {
            iddependencia = data[i].p_tbl_dependencia_id;
        }
        GetDependenciasM(iddependencia);
    });
    return;
}

function GetDependenciasM(iddependencia) {
    var instancia = iddependencia;
    $.get($('#EndPointAC').val() + "SerDependencia/Get/Dropdown/" + instancia, function (data, status) {
        $('#InstanciaObj').val(JSON.stringify(data));
        var body = "<option value='-1'>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('.cmb_lstdep_indexM').html(body);
        $('.cmb_lstdep_indexM > option[value="' + iddependencia + '"]').attr("selected", "selected");
        $(".cmb_lstdep_indexM").prop('disabled', true);
    });
    return;
}
function GetTipoObligacionM() {
    $.get($('#EndPointAC').val() + "SerObligacion/Get/Tipooblig", function (data, status) {
        var body = "<option value='-1'>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].tipo_obligacion + "</option>";
        }
        $('.cmb_TipoObligacionM').html(body);
        $(".cmb_TipoObligacionM").prop('disabled', true);
    });
    return;
}

function GetPrioridadM() {

    $.get($("#EndPointAC_Admon").val() + "Catalogos/Get/TPrioridad/" + $('#HDidInstancia').val(), function (data, status) {
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].tipo_prioridad + "</option>";
        }
        $('.cmb_prioridad_indexM').html(body);
        //$(".cmb_prioridad_indexM").prop('disabled', true);
    });
    return;
}

//Auxiliares fin

//validación
function ValidarOblupdate() {

    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('.txt_clausulaM').val() == '') {
        Response.Texto = 'Debe ingresar una clausula';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_oblNombreM').val() == '') {
        Response.Texto = 'Debe ingresar un nombre de la obligación';
        Response.Bit = true;
        return Response;
    }
    
    if ($('.cmb_sancion_indexM').val() == '') {
        Response.Texto = 'Debe seleccionar un tipo de sanción';
        Response.Bit = true;
        return Response;
    }
    if ($('.cmb_periodo_indexM').val() == '') {
        Response.Texto = 'Debe seleccionar un periodo';
        Response.Bit = true;
        return Response;
    }
    if ($('.cmb_ProdServ_indexM').val() == '') {
        Response.Texto = 'Debe seleccionar un producto/servicio';
        Response.Bit = true;
        return Response;
    }
    if ($('.cmb_TipoObligacionM').val() == '') {
        Response.Texto = 'Debe seleccionar un tipo de obligación';
        Response.Bit = true;
        return Response;
    }
    if (($('.cmb_prioridad_indexM').val() == '')) {
        Response.Texto = 'Debe seleccionar una prioridad';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
//Fin de la validación
function cerrarModalObl() {
    $('#ModificarObligacion').modal('hide');
}


$('.Update').click(function () {
    //ModificarOBL($('#idLinkObl').val(), $('#idObl').val());
    updateObligacion_se();
})

function updateObligacion_se() {
    var Validacion = ValidarOblupdate();
    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    var obj = getEntidadUPd();
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj),
        type: 'post',
        success: function (data) {
            function conf() {
            }
            var Si = eval(conf);
            SuccessSAAction("Operación exitosa", "El registro se actualizo correctamente", Si);
            obtenerObligaciones2();
            cerrarMdlUpd();
            $('.cln').val('');
        },
        error: function (data) {

            ErrorSA('', 'No se pudo realizar el registro')
        },
        processData: false,
        type: 'PUT',
        url: ($('#EndPointAC').val() + "SerObligacion/Put")

    });

}

function getEntidadUPd() {

    var EntidadObj = Entidad;
    //Primera entidad
    EntidadObj.tbl_obligacion.p_id = $('#idObl').val();
    EntidadObj.tbl_obligacion.p_clausula = $('.txt_clausulaM').val();
    EntidadObj.tbl_obligacion.p_nivel_servicio = 1;
    //EntidadObj.tbl_obligacion.p_forma_aplicacion = $('.cmb_sancion_index').val();
    EntidadObj.tbl_obligacion.p_comentarios = $('.txt_comentariosM').val();
    EntidadObj.tbl_obligacion.p_obligacion = $('.txt_oblNombreM').val();

    EntidadObj.tbl_obligacion.p_tbl_tipo_prioridad_id = $('.cmb_prioridad_indexM').val();

    if ($('.cmb_sancion_indexM').val() != "") {
        EntidadObj.tbl_obligacion.p_porcentaje = $('#PorcentajeSanM').val();
    }
    //segunda entidad
    EntidadObj.tbl_link_obligacion.p_id = $('#idLinkObl').val();
    EntidadObj.tbl_link_obligacion.p_tbl_contrato_id = $('#idContrato').val();
    EntidadObj.tbl_link_obligacion.p_tbl_tipo_obligacion_id = $('.cmb_TipoObligacionM').val();

    EntidadObj.tbl_link_obligacion.p_tbl_sancion_obligacion_id = $('.cmb_sancion_indexM').val();
    EntidadObj.tbl_link_obligacion.p_tbl_periodo_id = $('.cmb_periodo_indexM').val();
    EntidadObj.tbl_link_obligacion.p_tbl_producto_servicio_id = $(".cmb_ProdServ_indexM").val();

    // areas BagsControl
    //var AreasObj = JSON.parse($('#BagsControl').val());
    //for (var i = 0; i <= AreasObj.length - 1; i++) {
    //    EntidadObj.tbl_link_obligacion.p_str_areas = (AreasObj[i].value);
    //}

    // responsables $('#BagsControlResp').val()
    var Responsablesobj = JSON.parse($('#BagsControlResp').val());
    var ids = [];
    for (var i = 0; i <= Responsablesobj.length - 1; i++) {
        ids.push(Responsablesobj[i].value);
    }
    EntidadObj.tbl_link_obligacion.p_str_responsables = (ids.toString());
    return EntidadObj;
}

function cerrarMdlUpd() {
    return $('#ModificarObligacion').modal('hide');
}
0
//Entidad
var OBJ = {
    Obl: {
        id: 0,
        Clausula: "-",
        NivelServicio: 0,
        FormaAplicacion: "-",
        Comentarios: "-",
        Obligacion: null,
        Monto: null,
        Porcentaje: null,
        inclusion: "2019-11-05T10:29:51.607",
        tbl_tipo_prioridad_id: ""
    },
    LinkObl: {
        id: "00000000-0000-0000-0000-000000000000",
        tbl_Obligaciones_ac_id: 0,
        TBLENT_CONTRATO_id: "00000000-0000-0000-0000-000000000000",
        tbl_tipoObligacion_id: 0,
        tbl_SancionObli_ac_id: 0,
        tbl_Periodos_ac_id: 0,
        tbl_ProdServ_ac_id: 0,
        inclusion: "2019-11-09T12:22:48.877",
        Estatus: true,
        tbl_tipoAplicacion_ac_id: 0
    }
}
