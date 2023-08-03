$(document).ready(function () {
    LaunchLoader(true);
    var bagControl_js = [];
    $('#BagsControlResp').val(JSON.stringify(bagControl_js));
    obtenerObligaciones2();
    //GetDependencias();
    //GetDependenciasContrato();
});




function TriggerTable() {
    try {
        var table = $('#ObligacionesTbl').DataTable();
        $('#ObligacionesTbl tbody').on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('.selected').find('td:not(:empty):first').trigger('click')
            }
        });

    }
    catch{ }
}

function Callriesgos(link_obligacion, obligacion) {
    $('#Riesgosmdl').modal('show');
    $('#idObligacionVPMtzR').val(link_obligacion);
    setInterval('Redimension()', 500);
    GetMatriz(obligacion);
    $('#idObli').val(obligacion);
    GetResponsables1();
    GetCatRespuesta();
    GetProbabilidad();
    GetImpacto();
    GetTipoRespuesta();
}



///cmb_sancion_index
$(".cmb_sancion_index").change(function () {
    var Sancion = $(".cmb_sancion_index option:selected").text().toLowerCase();
    if (Sancion != "") {
        $('#tipoSan').text('Indique el porcentaje de la sanción');
        $('.DivTipoSan').html('<input type="number" class="form-control cln" id ="PorcentajeSan" onKeyPress="if(this.value.length==3) return false;" min="0" > <span class="input-group-addon">%</span>')
    }
    else {
        $('#tipoSan').text('');
        $('.DivTipoSan').html('')
    }
});

function Separadores() {
    $('#MontoSan').keyup(function (event) {
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

function LaunchModalObligaciones() {
    var numcont = $('#txt_numcon').val();
    $('#titulo_hd').html('Alta de obligaciones Contrato: ' + numcont);

    $('#AltaObligaciones').modal('show');
    GetDependenciasContrato();
    GetSanciones();
    GetPrioridad();
    GetPeriodos();
    GetResponsables1($('#idContrato').val());
    GetSelectiveAreas();
    
    GetProductoServicio();    
    GetTipoObligacion();
    GetTipoAplicacion();

    var bagControl_js = [];
    $('#BagsControlResp').val(JSON.stringify(bagControl_js));

    $('#Cont-bag_Resp').html('');
    $('#Cont-bag').html('');
    $('#tipoSan').text('');
    $('.DivTipoSan').html('')

}

function LaunchModalModificaObligaciones(idlinkk, idObligacion) {
    var idLink = idlinkk;
    var idObl = idObligacion;
    GetDependenciasContratoM();
    var numcont = $('#txt_numcon').val();
    $('#titulo_hd').html('Modificacion de obligaciones Contrato: ' + numcont);
    $('#ModificarObligacion').modal('show');
    ModificarObl(idLink, idObl);
    


    var bagControl_js = [];
    $('#BagsControlResp').val(JSON.stringify(bagControl_js));

    $('#Cont-bag_Resp').html('');
    $('#Cont-bag').html('');
    $('#tipoSan').text('');
    $('.DivTipoSan').html('')

}

function GetTipoAplicacion() {
    $.get($('#EndPointAC').val() + "SerObligacion/Get/TipoAplicacion", function (data, status) {
        var body = "<option value='-1'>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].tipo_aplicacion + "</option>";
        }
        $('.txt_aplicacion').html(body);
    });
    return;
}

//Obtiene un true si la obligacion que se quiere eliminar esta ligada a un plan de entrega a traves de los productos
function VerificarObligacion(IdObligacion) {
    $.get($('#EndPointAC').val() + "SerObligacion/verificar/" + IdObligacion, function (data, status) {
        var estado =  data.estado;    

        if (estado == false) {

        $.ajax({
            dataType: 'text',
            type: 'delete',
            async: false,

            success: function (data) {
                var obj = JSON.parse(data);
                if (obj.response[0].cod == "success") {
                    obtenerObligaciones2();
                    SuccessSA('', 'La obligación se inhabilitó correctamente');
                }
                else {
                    ErrorSA("", obj.response[0].msg);
                }
            },
            error: function (data) {
                var obj = JSON.parse(data);
                ErrorSA('', obj.response[0].msg)
            },
            url: $('#EndPointAC').val() + "SerObligacion/delete/" + IdObligacion
           // url: "https://localhost:44359/obligaciones/Delete/" + IdObligacion
        })

        } else if (estado == true) {
        ErrorSA('', "La obligacion que intenta eliminar esta vinculada a un producto registrado en planes de entrega")

    }



    });
}


function GetSanciones() {
    $.get($('#EndPointAC').val() + "SerSancion/Get", function (data, status) {
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].sansion + "</option>";
        }
        $('.cmb_sancion_index').html(body);
    });
    return;
}

function GetPrioridad() {
    
    $.get($("#EndPointAC_Admon").val() + "Catalogos/Get/TPrioridad/" + $('#HDidInstancia').val(), function (data, status) {
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].tipo_prioridad + "</option>";
        }
        $('.cmb_prioridad_index').html(body);
    });
    return;
}

function GetPeriodos() {
    $.get($('#EndPointAC').val() + "SerObligacion/Get/Periodos", function (data, status) {
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].periodo + "</option>";
        }
        $('.cmb_periodo_index').html(body);
    });
    return;
}
function GetResponsables1(contrato) {

    $.get($('#EndPointAC').val() + "SerServidorPublico/Get/sigla/All/Contrato/" + contrato, function (data, status) {
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            var obj = data[i];
            body = body + "<option value='" + obj.tbl_rol_usuario_id + "'>" + obj.nombrecompleto + "</option>";
        }
        $('.cmb_responsable').html(body);
        $("#cmb_responsable").prop('disabled', true);
    });
    return;

}
function GetProductoServicio() {

    $.get($('#EndPointAC').val() + "SerContratoProductos/Get/Dropdown/" + $('#idContrato').val(), function (data, status) {
       
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#cmb_ProdServ_index').html(body);
    });
    return;
}

function GetSelectiveAreas() {
    $.get($('#EndPointAC').val() + "SerAreas/Get/DropDown/" + $('#HDidDependencia').val(), function (data, status) {
        var body = "<option value='-1'>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#cmb_areas_idex').html(body);
    });
    return;
}

function GetDependenciasContrato() {
    var contratoid = $('#idContrato').val();
    $.get($('#EndPointAC_Admon').val() + "Contratos/GetContratoporid/"+contratoid, function (data, status) {
        var iddependencia;
        for (var i = 0; i <= data.length - 1; i++) {
            iddependencia = data[i].p_tbl_dependencia_id;
        }     
        GetDependencias(iddependencia);
    });
    return;
}

function GetDependencias(iddependencia) {
    var instancia = iddependencia;
    $.get($('#EndPointAC').val() + "SerDependencia/Get/Dropdown/" + instancia, function (data, status) {
        $('#InstanciaObj').val(JSON.stringify(data));
        var body = "<option value='-1'>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('.cmb_lstdep_index').html(body);
        $('.cmb_lstdep_index > option[value="' + iddependencia + '"]').attr("selected", "selected");
        $("#cmb_lstdep_index").prop('disabled', true);
    });
    return;
}

//function GetDependencias() {

//    $.get($('#EndPointAC').val() + "SerDependencia/Get/Dropdown/" + $('#HDidInstancia').val(), function (data, status) {
//        $('#InstanciaObj').val(JSON.stringify(data));
//        var body = "<option value='-1'>Seleccione...</option>";
//        for (var i = 0; i <= data.length - 1; i++) {
//            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
//        }
//        $('.cmb_lstdep_index').html(body);
//        $('#cmb_lstdep_index > option[value="' + $('#HDidDependencia').val() + '"]').attr("selected", "selected");        
//        $("#cmb_lstdep_index").prop('disabled', true);
//    });
//    return;
//}

function GetTipoObligacion() {
    $.get($('#EndPointAC').val() + "SerObligacion/Get/Tipooblig", function (data, status) {  
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].tipo_obligacion + "</option>";
        }
        $('#cmb_TipoObligacion').html(body);
    });
    return;
}

var Entidad = {
    tbl_obligacion: {
        p_opt: 0,
        p_id: "00000000-0000-0000-0000-000000000000",
        p_clausula: "",
        p_nivel_servicio: 0,
        p_forma_aplicacion: "",
        p_comentarios: "",
        p_obligacion: "",
        p_monto: 0.0,
        p_porcentaje: 0,
        p_tbl_tipo_prioridad_id:""
    },
    tbl_link_obligacion: {
        p_opt: 0,
        p_id: "00000000-0000-0000-0000-000000000000",
        p_tbl_obligacion_id: "00000000-0000-0000-0000-000000000000",
        p_tbl_contrato_id: "00000000-0000-0000-0000-000000000000",
        p_tbl_tipo_obligacion_id: "00000000-0000-0000-0000-000000000000",
        p_tbl_sancion_obligacion_id: "00000000-0000-0000-0000-000000000000",
        p_tbl_periodo_id: "00000000-0000-0000-0000-000000000000",
        p_tbl_producto_servicio_id: "00000000-0000-0000-0000-000000000000",
        p_estatus: 1,
        p_tbl_tipo_aplicacion_id: "00000000-0000-0000-0000-000000000000",
        p_str_areas: "",
        p_str_responsables: "",
    },
    tbl_area_obligacion: {
        p_id_obligacion_id: "00000000-0000-0000-0000-000000000000",
        p_str_areas:""
    },
    tbl_responsable_obligacion: {
        p_id_obligacion_id: "00000000-0000-0000-0000-000000000000",
        p_str_responsables:""
    }
}

function getEntidad() {

    var EntidadObj = Entidad;
    //Primera entidad
    EntidadObj.tbl_obligacion.p_clausula = $('.txt_clausula').val();
    EntidadObj.tbl_obligacion.p_nivel_servicio = 1;
   //EntidadObj.tbl_obligacion.p_forma_aplicacion = $('.cmb_sancion_index').val();
    EntidadObj.tbl_obligacion.p_comentarios = $('.txt_comentarios').val();
    EntidadObj.tbl_obligacion.p_obligacion = $('.txt_oblNombre').val();

    EntidadObj.tbl_obligacion.p_tbl_tipo_prioridad_id = $('.cmb_prioridad_index').val();

    if ($('.cmb_sancion_index').val() != "") {
        EntidadObj.tbl_obligacion.p_porcentaje = $('#PorcentajeSan').val();
    }
    //segunda entidad
    EntidadObj.tbl_link_obligacion.p_tbl_contrato_id = $('#idContrato').val();
    EntidadObj.tbl_link_obligacion.p_tbl_tipo_obligacion_id = $('.cmb_TipoObligacion').val();

    EntidadObj.tbl_link_obligacion.p_tbl_sancion_obligacion_id = $('.cmb_sancion_index').val();
    EntidadObj.tbl_link_obligacion.p_tbl_periodo_id = $('.cmb_periodo_index').val();
    EntidadObj.tbl_link_obligacion.p_tbl_producto_servicio_id = $(".cmb_ProdServ_index").val();

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
function cerrarMdl() {
    return $('#AltaObligaciones').modal('hide');
}



function ValidarAddObl() {

    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('.txt_clausula').val() == '') {
        Response.Texto = 'Debe ingresar una cláusula';
        Response.Bit = true;
        return Response;
    }
    if ($('.txt_oblNombre').val() == '') {
        Response.Texto = 'Debe ingresar un nombre de la obligación';
        Response.Bit = true;
        return Response;
    }
    
    if ($('.cmb_sancion_index').val() == '') {
        Response.Texto = 'Debe seleccionar un tipo de sanción';
        Response.Bit = true;
        return Response;
    }
    var tipo_sa = $('.cmb_sancion_index').val();
    if (tipo_sa != '') {
        if ($('#PorcentajeSan').val() == '') {
            Response.Texto = 'Debe ingresar un porcentaje de la sanción';
            Response.Bit = true;
            return Response;
        }
    }
    if ($('.txt_comentarios').val() == '') {
        Response.Texto = 'Debe ingresar un comentario';
        Response.Bit = true;
        return Response;
    }
     
    if ($('.cmb_periodo_index').val() == '') {
        Response.Texto = 'Debe seleccionar un período';
        Response.Bit = true;
        return Response;
    }
    if ($('.cmb_ProdServ_index').val() == '') {
        Response.Texto = 'Debe seleccionar un producto/servicio';
        Response.Bit = true;
        return Response;
    }
    if (($('.cmb_TipoObligacion').val() == '')) {
        Response.Texto = 'Debe seleccionar un tipo de obligación';
        Response.Bit = true;
        return Response;
    }
    if (($('.cmb_prioridad_index').val() == '')) {
        Response.Texto = 'Debe seleccionar una prioridad';
        Response.Bit = true;
        return Response;
    }
    

    if (!$.trim($('#Cont-bag_Resp').html())) {
        Response.Texto = 'Debe seleccionar al menos un responsable';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function addObligacion_se() {
    var Validacion = ValidarAddObl();
    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    var obj = getEntidad();
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj),
        type: 'post',
        success: function (data) {
            function conf() {
                return cerrarMdl();
            }
            var Si = eval(conf);
            SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", Si);
            obtenerObligaciones2();
            $('.cln').val('');
        },
        error: function (data) {

            ErrorSA('', 'No se pudo realizar el registro')
        },
        processData: false,
        type: 'POST',
        url: ($('#EndPointAC').val() + "SerObligacion/Add")
        
    });

}


$('.cerrar').click(function () {
    $('.cln').val('');
    $(".TipoAplicacion").hide();
    $('#tipoSan').text('');
    $('.DivTipoSan').html('');
})

function obtenerObligaciones2()
{
    $.get($('#EndPointAC').val() + "SerObligacion/Contrato/List/Detalle/" + $('#idContrato').val(), function (data, status)
    {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++)
        {
            var Interno = [];
            var Areasl = "";
            var Responsablesl = "";                                             
            Interno.push(data[i].obligacion.clausula);
            Interno.push(data[i].obligacion.tipo_obligacion); 

            if (data[i].areas != null) {
                for (var j = 0; j <= data[i].areas.length - 1; j++) {
                    Areasl = Areasl + '<p>' + data[i].areas[j].text + '</p>';
                }
            }
            else { Areasl = ''; }
            //Interno.push(Areasl);

            if (data[i].responsables != null) {
                for (var k = 0; k <= data[i].responsables.length - 1; k++) {
                    Responsablesl = Responsablesl + '<p>' + data[i].responsables[k].text + '</p>';
                }
            }
            else { Responsablesl = ''; }
            Interno.push(Responsablesl);

            //Interno.push(data[i].obligacion.nivel_servicio);
            Interno.push(data[i].obligacion.sansion);
            //Interno.push(data[i].obligacion.forma_aplicacion);
            Interno.push(data[i].obligacion.periodo);
            Interno.push(data[i].obligacion.tbl_tipo_prioridad_nombre??"");
            Interno.push(data[i].obligacion.comentarios);         
            Interno.push("<button onclick=\"Callriesgos('" + data[i].obligacion.tbl_link_obligacion_id + "','" + data[i].obligacion.tbl_obligacion_id + "')\" class='btn btn-warning' title='Asignar Riesgos'><i class='fa fa-warning'></i></button> <button class='btn btn-danger' title='Eliminar obligación' onclick=\"IrObligacionInvalid('" + data[i].obligacion.tbl_obligacion_id + "','" + data[i].obligacion.tbl_link_obligacion_id + "');\"><i class='fa fa-trash'></i></button> <button class='btn btn-primary' title='Editar obligación' onclick=\"LaunchModalModificaObligaciones('" + data[i].obligacion.tbl_link_obligacion_id + "','" + data[i].obligacion.tbl_obligacion_id + "');\"><i class='fa fa-edit'></i></button>");
            Arreglo_arreglos.push(Interno);        
        }
        var table = $('#ObligacionesTbl').DataTable();
        table.destroy();
        console.log(Arreglo_arreglos);
        $('#ObligacionesTbl').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "Cláusula" },
                { title: "Tipo obligación" },
                // { title: "Áreas" },
                { title: "Responsables" },
                // { title: "Nivel de servicio" },
                { title: "Sanción" },
                // { title: "Aplicación" },
                { title: "Periodo" },
                { title: "Prioridad" },
                { title: "Comentarios" },
                { title: "Acciones" }
            ]
        });
        LaunchLoader(false);
    });
}

/********************************************************************************************************/
$('.cerrarModal').click(function () {
    $('#idLinkObl').val('');
    $('#idObl').val('');
    GetSancionesM();
    $('.clean').val('');

    $('.Bag-Obligaciones').remove();
})

function ModificarObl(idLink, idObl) {
    $.get($('#EndPointAC').val() + "SerObligacion/Obligacion/"+ idObl, function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {

            $('.txt_clausulaM').val(data[i].clausula);
            $('.txt_oblNombreM').val(data[i].obligacion);
            $('.txt_LvServicioM').val(data[i].nivel_servicio);

            $('.cmb_sancion_indexM > option[value="' + data[i].tbl_sancion_obligacion_id + '"]').attr("selected", "selected");
            //$('.cmb_sancion_indexM').val(data[i].tbl_sancion_obligacion_id);

            if ($('.cmb_sancion_indexM').val() != '') {

                $('#tipoSanM').text('Indique el porcentaje de la sanción');
                $('.DivTipoSanM').html('<input type="text" class="form-control" disabled id ="PorcentajeSanM"> <span class="input-group-addon">%</span>')
                $('#PorcentajeSanM').val(data[i].porcentaje);
            }
            //else if ($('.cmb_sancion_indexM').val() == 'e6aa31d1-37fd-11ea-82d7-00155d1b3502') {
            //    $('#tipoSanM').text('Indique el monto de la sanción');
            //    $('.DivTipoSanM').html('<span class="input-group-addon">$</span> <input type="text" class="form-control" id="MontoSanM">')
            //    $('#MontoSanM').val(data[i].monto);
            //    Separadores();
            //}
            else {
                $('#tipoSanM').text('');
                $('.DivTipoSanM').html('')
            }
            
            $('.txt_comentariosM').val(data[i].comentarios);
            $('.cmb_periodo_indexM').val(data[i].tbl_periodo_id);
            $('.cmb_ProdServ_indexM').val(data[i].tbl_producto_servicio_id);
            $('.cmb_TipoObligacionM').val(data[i].tbl_tipo_obligacion_id);
            if (data[i].tbl_tipo_prioridad_id == null) {
                $(".cmb_prioridad_indexM").prop('disabled', false);
            }
            else {
                $('.cmb_prioridad_indexM').val(data[i].tbl_tipo_prioridad_id);
                $(".cmb_prioridad_indexM").prop('disabled', true);
            }     
        }
    });
    $('#idLinkObl').val(idLink);
    $('#idObl').val(idObl);
    $('#ModificarObligacion').modal({ backdrop: 'static', keyboard: false });
    $('#ModificarObligacion').modal('show');


}
/********************************************************************************************************/

function confirmarEliminar(id, id_link) {
    $.ajax({
        async:false
    });
    
    VerificarObligacion(id);

    $.ajax({
        async: true
    });

    //if (verificar_Obligacion == false) {

    //    $.ajax({
    //        dataType: 'text',
    //        cache: false,
    //        contentType: false,
    //        processData: false,
    //        type: 'post',
    //        async: false,

    //        success: function (data) {
    //            var objresponse = JSON.parse(data);
    //            if (!objresponse.Bit) {
    //                obtenerObligaciones2();
    //                SuccessSA('', 'La obligación se inhabilitó correctamente');
    //            }
    //            else {
    //                ErrorSA("", objresponse.Descripcion);
    //            }
    //        },
    //        error: function () {
    //            var objresponse = JSON.parse(data);
    //            ErrorSA('', objresponse.Descripcion)
    //        },
    //        processData: false,
    //        type: 'POST',
    //        url: '/Request/Obligacionesvalidas/Get/' + id + '/' + id_link
    //        url: '/Request/Obligacionesvalidas/Get/' + id + '/' + id_link
    //    })

    //} else if (verificar_Obligacion == true) {
    //    ErrorSA('', "La obligacion que intenta eliminar esta vinculada a un producto registrado en planes de entrega")

    //}

    
   
    
}

function IrObligacionInvalid(id, id_link) {
    function Confirmacion() {
        return confirmarEliminar(id, id_link);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}


////////// Control Agregar Elementos
function addRespo_bag(control, contenedorjson, bagcontauiner) {
    if ($('.' + control).val() == '-1') {
        return;
    }
    var value = $('.' + control).val();
    var text = $("." + control + " option:selected").text();
    console.log(value);
    console.log(contenedorjson);

    if (!FindBagResp(value, contenedorjson)) {
        var bag = "<div id='id_" + value + "_resp'><br><a class='Bag-Obligaciones'> " + text + " <i onclick=\"RemoveBagResp('" + value + "','" + contenedorjson + "')\" class='fa fa-fw fa-remove over'></i></a></div>";
        var obj = {
            value: value,
            text: text
        }
        var array_obj = JSON.parse($('#' + contenedorjson).val());
        array_obj.push(obj);
        $('#' + contenedorjson).val(JSON.stringify(array_obj));
        $('#' + bagcontauiner).append(bag);
    }

}

function FindBagResp(value, contenedorjson) {
    var bit = false;
    var obj = JSON.parse($('#' + contenedorjson).val());
    for (var i = 0; i <= obj.length - 1; i++) {
        if (obj[i].value == value) {
            bit = true;
        }
    }
    return bit;
}

function RemoveBagResp(value, contenedorjson) {
    var obj = JSON.parse($('#' + contenedorjson).val());
    for (var i = 0; i <= obj.length - 1; i++) {
        if (obj[i].value == value) {
            obj[i] = null;
        }
    }
    var newobj = [];
    for (var i = 0; i <= obj.length - 1; i++) {
        if (obj[i] != null) {
            newobj.push(obj[i]);
        }
    }
    $('#id_' + value + '_resp').remove();
    $('#' + contenedorjson).val(JSON.stringify(newobj));

}
////////// Control Agregar Elementos
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

//function getEntidadUPd() {

//    var EntidadObj = Entidad;
//    //Primera entidad
//    EntidadObj.tbl_obligacion.p_id = $('#idObl').val();
//    EntidadObj.tbl_obligacion.p_clausula = $('.txt_clausulaM').val();
//    EntidadObj.tbl_obligacion.p_nivel_servicio = 1;
//    //EntidadObj.tbl_obligacion.p_forma_aplicacion = $('.cmb_sancion_index').val();
//    EntidadObj.tbl_obligacion.p_comentarios = $('.txt_comentariosM').val();
//    EntidadObj.tbl_obligacion.p_obligacion = $('.txt_oblNombreM').val();

//    EntidadObj.tbl_obligacion.p_tbl_tipo_prioridad_id = $('.cmb_prioridad_indexM').val();

//    if ($('.cmb_sancion_indexM').val() != "") {
//        EntidadObj.tbl_obligacion.p_porcentaje = $('#PorcentajeSanM').val();
//    }
//    //segunda entidad
//    EntidadObj.tbl_link_obligacion.p_id = $('#idLinkObl').val(),;
//    EntidadObj.tbl_link_obligacion.p_tbl_contrato_id = $('#idContrato').val();
//    EntidadObj.tbl_link_obligacion.p_tbl_tipo_obligacion_id = $('.cmb_TipoObligacionM').val();

//    EntidadObj.tbl_link_obligacion.p_tbl_sancion_obligacion_id = $('.cmb_sancion_indexM').val();
//    EntidadObj.tbl_link_obligacion.p_tbl_periodo_id = $('.cmb_periodo_indexM').val();
//    EntidadObj.tbl_link_obligacion.p_tbl_producto_servicio_id = $(".cmb_ProdServ_indexM").val();

//    // areas BagsControl
//    //var AreasObj = JSON.parse($('#BagsControl').val());
//    //for (var i = 0; i <= AreasObj.length - 1; i++) {
//    //    EntidadObj.tbl_link_obligacion.p_str_areas = (AreasObj[i].value);
//    //}

//    // responsables $('#BagsControlResp').val()
//    var Responsablesobj = JSON.parse($('#BagsControlResp').val());
//    var ids = [];
//    for (var i = 0; i <= Responsablesobj.length - 1; i++) {
//        ids.push(Responsablesobj[i].value);
//    }
//    EntidadObj.tbl_link_obligacion.p_str_responsables = (ids.toString());
//    return EntidadObj;
//}
//Fin de la validación































