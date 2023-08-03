var LONGITUD_100 = 100;
var URL_AGREGAR_PLAN_MONITOREO = "/Request/PlanMonitoreo/Add";
var URL_OBTENER_UBICACIONES_PLAN_ENTREGA = "/Request/PlanMonitoreo/ObtenerUbicacionesPorPlanEntrega/";
var SE_REALIZO_SELECCION = false;
var PORCENTAJE_TOTAL = 100;


$(function () {

    obtenerResponsables();
    obtenerPlanesEntrega();
    obtenerEstados();


    ValidarPorcentaje();

    


    $('.fechas').datetimepicker({   
                   format: 'YYYY-MM-DD'         
        
    });

    $("#planentrega").change(function () {
        var propMostrar = "none";

        if (this.value != '')
            propMostrar = "block";

        $("#panelUbicaciones").css("display", propMostrar);

        var table = $('#tbl_ubicaciones').DataTable();

        table
            .clear()
            .draw();
    });

    $("#btnGuardar").click(function () {
        guardar();
    });

    $("#btnCancelar").click(function () {
        history.back()
    });

    $("#btnSeleccionar").click(function () {
        seleccionar();
    });


   

});





function ValidarPorcentaje()
{

    

    $('#txtUbicaciones').keypress(function (event)
    {
        var keycode = (event.keyCode ? event.keyCode : event.which);
       
        if (event.keyCode > 47 && event.keyCode < 58)
        {
            if ($(this).val().length < 2) { return true; } else { $(this).val(""); return true; }
        }
        else { event.preventDefault(); return false; }
       
    });
    $('#txtUbicaciones').change(function () {

        if ($(this).val() < 0) 
        {
            $(this).val("100");
        }
        if ($(this).val() > 100)
        {
            $(this).val("0");
        }

    });

}

function obtenerEstados() {
    $.get($("#EndPointAC").val() + 'Operaciones/PM/Get/Estado', function (data, status) {
        var Body = "<option value='' selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].descripcion + "</option>";
        }
        $('#estado').html(Body);
    });
}

function obtenerPlanesEntrega() {
    var idCon = $('#hdnIdContrato').val();

    $.get($("#EndPointAC").val() +'Operaciones/PE/Get/PE/confirmados/' + idCon, function (data, status) {

         var Body = "<option value='' selected>Selecciona una opción</option>";
         for (var i = 0; i <= data.length - 1; i++) {
             Body = Body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
         }
         $('#planentrega').html(Body);
         


    });
}

function obtenerResponsables() {
    var idCon = $('#hdnIdContrato').val();

    $.get($("#EndPointAC").val() + "SerServidorPublico/Get/sigla/EPM/Contrato/" + idCon, function (data, status) {
        var Body = "<option value='' selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].tbl_contrato_servidor_resp_id + "'>" + data[i].nombrecompleto + "</option>";
        }
        $('#responsable').html(Body);
    }, 'json'); 
}

function seleccionar() {
    var idPlanEntrega = $("#planentrega").val();
    var pjUbicaciones = $("#txtUbicaciones").val();

    //if (idPlanEntrega == '') {

    //    ErrorSA('Error', "Seleccione un plan de entrega.");

    //    return false;
    //}

    if (pjUbicaciones == '') {
        pjUbicaciones = PORCENTAJE_TOTAL;
    }

    //$.get($("#EndPointAC").val() +"UbicacionesCatalog/Get/PE/"+ idPlanEntrega + "/" + pjUbicaciones, function (data, status) {
    $.get($("#EndPointAC").val() + "UbicacionesCatalog/Get/PE/" + idPlanEntrega, function (data, status) {
        var listado = [];
        var listadoUbicaciones_PM = [];

        if (data == undefined) {
            ErrorSA("Ubicaciones", "No hay ubicaciones disponibles");
        }
        else {

            if (data.length > 0) {
                SE_REALIZO_SELECCION = true;
            }

            for (var i = 0; i <= data.length - 1; i++) {
                var item = [];

                item.push(data[i].tbl_ubicacion_clave);
                item.push(data[i].tbl_ubicacion_unidad);
                item.push(data[i].tbl_contrato_servidor_resp_str);
                item.push(data[i].total_productos);

                listado.push(item);


                ////// Se genera objeto de ubicaciones para guardar el plan de monitoreo
                var ubicacion_ = ubicacion;

                ubicacion_.p_opt = 2;
                ubicacion_.p_id = CreateGuid();
                ubicacion_.p_tbl_ubicacion_plan_entrega_id = data[i].tbl_ubicacion_plan_entrega_id;

                listadoUbicaciones_PM.push(ubicacion);
                /////

            }

            $("#lstUbicaciones").val(JSON.stringify(listadoUbicaciones_PM));

            var table = $('#tbl_ubicaciones').DataTable();

            table.destroy();

            $('#tbl_ubicaciones').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
                },
                data: listado,
                columns: [
                    { title: "Clave de la unidad" },
                    { title: "Nombre de la unidad" },
                    { title: "Responsable administrativo" },
                    { title: "Total" }
                ],
                columnDefs: [
                    {
                        targets: -1,
                        className: 'dt-body-center'
                    }]
            });
        }
    });
}

function guardar() {
    var evaluacion = validarEntidad();

    if (evaluacion.Bit) {
        ErrorSA('Hay un error en los datos de entrada', evaluacion.Texto);
        return;
    }
    console.log(evaluacion.objeto);
    $.ajax({
        dataType: 'json',
        url: $("#EndPointAC").val() + 'Operaciones/PM/Add',
        //url: 'https://localhost:44359/PlanMonitoreo/Add',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(evaluacion.objeto),
        type: 'post',
        success: function (data) {
            if (!data.Bit) {

                SuccessSA('', '');

                $(".campo-formulario").val('');
                $("#panelUbicaciones").css("display", "none");
            }
        },
        error: function (data) {

            Swal.fire({
                type: 'error',
                title: 'Error al guardar el plan de monitoreo',
                text: data
            });
        }
    });
}

function validarEntidad() {
    

    var Response = { Texto: '', Bit: true, objeto: null };

    var strFechaEjecucion = $('#txtFechaEjecucion').val();
    var strPeriodo = $('#txtPeriodo').val();
    var idResponsable = $('#responsable').val();
    var idEstado = $('#estado').val();
    var idPlanEntrega = $('#planentrega').val();

    var lblFechaEjecucion = $("#lblFechaEjecucion").html();
    var lblPeriodo = $("#lblPeriodo").html();
    var lblResponsable = $("#lblResponsable").html();
    var lblEstado = $("#lblEstado").html();
    var lblPlanEntrega = $("#lblPlanEntrega").html();

	if (parseInt($('#txtUbicaciones').val()) > 100) {
		Response.Texto = 'El porcentaje de ubicaciones no debe ser mayor a 100';
		Response.Bit = true;
		return Response;
	}
    
    if ($.trim(strFechaEjecucion) == '') {
        Response.Texto = 'El campo "' + lblFechaEjecucion + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if ($.trim(strPeriodo) == '') {
        Response.Texto = 'El campo "' + lblPeriodo + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (idResponsable == '') {
        Response.Texto = 'El campo "' + lblResponsable + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (idEstado == '') {
        Response.Texto = 'El campo "' + lblEstado + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtUbicaciones').val() == '') {
        Response.Texto = 'El campo "Porcentaje de ubicaciones" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (idPlanEntrega == '') {
        Response.Texto = 'El campo "' + lblPlanEntrega + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strPeriodo, LONGITUD_100)) {
        Response.Texto = generarMensajeValidacionLongitud(lblPeriodo, LONGITUD_100);
        Response.Bit = true;
        return Response;
    }

    if (!SE_REALIZO_SELECCION) {
        Response.Texto = 'La selección aleatoria de ubicaciones es requerida.';
        Response.Bit = true;
        return Response;
    }

    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());


    var plan_detalle = plan_monitoreo;

    plan_detalle.p_opt = 2;
    plan_detalle.p_id = CreateGuid();
    plan_detalle.p_tbl_plan_entrega_id = idPlanEntrega;
    plan_detalle.p_tbl_plan_monitoreo_estado_id = idEstado;
    plan_detalle.p_tbl_contrato_servidor_resp_id = idResponsable;
    plan_detalle.p_periodo = strPeriodo;
    plan_detalle.p_ejecucion = strFechaEjecucion;
    plan_detalle.p_inclusion = date;
    p_activo = 1;

    var Plan = clase;
    Plan._plan_monitoreo = plan_detalle;
    Plan.ubicaciones = JSON.parse($("#lstUbicaciones").val());


    //OBJ.FechaEjecucion = strFechaEjecucion;
    //OBJ.Periodo = strPeriodo;
    //OBJ.tbl_RespApego_Contrato_ac_id = idResponsable;
    //OBJ.Id_Estado = idEstado;
    //OBJ.tbl_PlanEntregaDetalle_ac_id = idPlanEntrega;
    //OBJ.TBLENT_CONTRATO_id = $('#hdnIdContrato').val();
    //OBJ.lstUbicaciones = JSON.parse($("#lstUbicaciones").val());

    Response.Texto = '';
    Response.Bit = false;
    Response.objeto = Plan;

    return Response;
}

function generarMensajeValidacionLongitud(lblCampoValidar, longitudCadena) {
    return 'La longitud del campo "' + lblCampoValidar + '" debe ser menor o igual a ' + longitudCadena + ' caracteres.';
}

//var clase = {
//    Id: '00000000-0000-0000-0000-000000000000',
//    FechaEjecucion: null,
//    Periodo: '',
//    tbl_RespApego_Contrato_ac_id: '0',
//    Id_Estado: '0',
//    tbl_PlanEntregaDetalle_ac_id: '0',
//    TBLENT_CONTRATO_id: '0',
//    inclusion: '',
//    lstUbicaciones: ''
//};

var clase = {
    _plan_monitoreo: null,
    ubicaciones: null
};


var plan_monitoreo = {
  p_opt: 0,
  p_id: "00000000-0000-0000-0000-000000000000",
  p_tbl_plan_entrega_id: "00000000-0000-0000-0000-000000000000",
  p_tbl_plan_monitoreo_estado_id: "00000000-0000-0000-0000-000000000000",
  p_tbl_contrato_servidor_resp_id: "00000000-0000-0000-0000-000000000000",
  p_periodo: null,
  p_ejecucion: null,
  p_inclusion: null,
  p_activo: 0
}

var ubicacion = {
    p_opt: 0,
    p_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_plan_monitoreo_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_ubicacion_plan_entrega_id: '00000000-0000-0000-0000-000000000000'
};



function CreateGuid() {
    function _p8(s) {
        var p = (Math.random().toString(16) + "000000000").substr(2, 8);
        return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
    }
    return _p8() + _p8(true) + _p8(true) + _p8();
}