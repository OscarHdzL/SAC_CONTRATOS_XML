var URL_SERVICIO_BASE = URL_OBTENER_NOTIFICACIONES_SANCIONES = "";
var URL_PLANES_OBLIGACION = "";

$.extend($.fn.dataTable.defaults, {
    responsive: true
});

function Periodo() {
    var date = $('#datetimepicker11').val();
    var elem = date.split('/');
    var mes = elem[0];
    var año = elem[1];
    return año + "-" + mes+"-01";
}

$(document).ready(function () {
    establecerRutasServicio();

    $('#datetimepicker10').datetimepicker({
        viewMode: 'years',
        format: 'MM/YYYY'
    });
    GetContratos();
    $('#idcontratoNS').val($('#ContratosNS').val());
    $('#NotificacionSanciones').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "15%", "targets": 0 },
            { "width": "15%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "15%", "targets": 4 },
            { "width": "15%", "targets": 5 },
            { "width": "10%", "targets": 6 },
        ],
    });
    $('#listado_hid').hide();
    $('#PlanEntregaNC').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
    });
    $('#PlanMonitoreoNC').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
    });
    setInterval('Redimension()', 500);
})

function establecerRutasServicio() {
    var idDep = $('#HDidDependencia').val();
    URL_SERVICIO_BASE = $("#EndPointAC").val();
    URL_OBTENER_NOTIFICACIONES_SANCIONES = URL_SERVICIO_BASE + "NotificacionesSanciones/Get/Notificacion/Contrato/";
    URL_GET_CONTRATOS = URL_SERVICIO_BASE + "SerContrato/Get/ListadoContratos/" + idDep;
    URL_VISTACONTRATO = URL_SERVICIO_BASE + "SerContrato/Get/VistaContrato/";

    URL_PLANES_OBLIGACION = URL_SERVICIO_BASE + 'NotificacionesSanciones/Get/planes/obligacion/'

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
function GetContratos() {
    $.get(URL_GET_CONTRATOS, function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].numero + "</option>";
        }
        $('#ContratosNS').html(body);
    });
    return;
}
$('#ContratosNS').change(function () {
    $('#listado_hid').show();
    $('#datetimepicker11').val('');
    $('#NotificacionSanciones').html('');
})

$('#Buscar').click(function () {
    if ($('#datetimepicker11').val() === '') {
        return ErrorSA('Error en los datos de entrada', 'El campo "periodo" no puede estar vacío');
    }

    GetNotificacionSanciones();
});

function GetNotificacionSanciones() {
    var idContrato = $('#idContrato').val();
    var periodo = Periodo();

    $.get($("#EndPointAC").val() +"SerObligacion/GetReporteSancionesAgrupado/Contrato/" + idContrato + "/Periodo/" + periodo, function (data) {
        var Arreglo_arreglos = [];
        console.log(data);

        for (var i = 0; i <= data.length - 1; i++) {
            var NS = [];
            NS.push(data[i].identificador);
            NS.push(data[i].periodo);
            NS.push(data[i].descripcion);
            var fecha = data[i].fecha_ejecucion != null ? data[i].fecha_ejecucion.slice(0, data[i].fecha_ejecucion.indexOf("T")) : '';
            var color = returnColor(data[i].dias_restantes_ejecucion);
            var lblfecha = '<label style="color:' + color + ' " >' + fecha + '</label>';
            NS.push(lblfecha);
            NS.push(data[i].tipo_entrega);
            var cumplio = data[i].cumplio_pe == true ? "Si" : "No";
            NS.push(cumplio);

            var ObjhtmlProductos = '<label style="display: none;" id="ArregloUbicaciones_' + data[i].tbl_plan_entrega_id + '">' + JSON.stringify(data[i].ubicaciones) + '</label>' + '<a onclick="btnListaUbicaciones(\'' + data[i].tbl_plan_entrega_id + '\')" class="fa fa-list btn btn-primary" title="Lista Ubicaciones"> </a>';
            NS.push(ObjhtmlProductos);

            Arreglo_arreglos.push(NS);
        }

        var table = $('#NotificacionSanciones').DataTable();

        table.destroy();

        $('#NotificacionSanciones').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "Identificador" },
                { title: "Periodo" },
                { title: "Descripción" },
                { title: "Fecha de Ejecución" },
                { title: "Tipo de entrega" },
                { title: "Cumplió PE" },
                { title: "Ver ubicaciones" }
            ]
        });

    });

}
function returnColor(numero_dias) {
    if (numero_dias > 15) {
        return 'black';
    } else if (numero_dias >= 0) {
        return 'orange';
    } else {
        return 'red';
    }
}

function btnListaUbicaciones(ubicacionesID) {
    console.log(ubicacionesID);
    var ubicaciones_html = JSON.parse($('#' + 'ArregloUbicaciones_' + ubicacionesID).html());
    console.log(ubicaciones_html);
    $('#ReporteUbicaciones').modal({ backdrop: 'static', keyboard: false });
    $('#ReporteUbicaciones').modal('show');
    var Arreglo_arreglos = [];

    for (var i = 0; i <= ubicaciones_html.length - 1; i++) {
        var ListaPE = [];
        ListaPE.push(ubicaciones_html[i].tbl_ubicacion_clave);
        ListaPE.push(ubicaciones_html[i].tbl_ubicacion_unidad);
        var ObjhtmlProductos = '<label style="display: none;" id="ArregloProductos_' + ubicaciones_html[i].tbl_ubicacion_id + '">' + JSON.stringify(ubicaciones_html[i].productos) + '</label>' + '<a onclick="btnListaProductos(\'' + ubicaciones_html[i].tbl_ubicacion_id + '\')" class="fa fa-list btn btn-primary" title="Lista Productos"> </a>';
        ListaPE.push(ObjhtmlProductos);

        Arreglo_arreglos.push(ListaPE);
    }

    var table = $('#tbl_ubicaciones_reporte').DataTable();
    table.destroy();
    console.log(Arreglo_arreglos);
    $('#tbl_ubicaciones_reporte').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        data: Arreglo_arreglos,
        columns: [
            { title: "Clave ubicación" },
            { title: "Unidad ubicación" },
            { title: "Ver productos" }
        ]
    });
}


function btnListaProductos(ProductosID) {
    console.log(ProductosID);
    var productos_html = JSON.parse($('#' + 'ArregloProductos_' + ProductosID).html());
    console.log(productos_html);
    $('#ReporteProductos').modal({ backdrop: 'static', keyboard: false });
    $('#ReporteProductos').modal('show');
    var Arreglo_arreglos = [];

    for (var i = 0; i <= productos_html.length - 1; i++) {
        var ListaPE = [];
        ListaPE.push(productos_html[i].producto_servicio_nombre);
        ListaPE.push(productos_html[i].producto_servicio_clave);
        ListaPE.push(productos_html[i].cantidad);
        var ObjhtmlProductos = '';
        console.log(productos_html[i].obligaciones);

        if (productos_html[i].obligaciones[0].tbl_link_obligacion_id != null) {
            ObjhtmlProductos = '<label style="display: none;" id="ArregloSanciones_' + productos_html[i].producto_servicio_id + '">' + JSON.stringify(productos_html[i].obligaciones) + '</label>' + '<a onclick="btnListaSanciones(\'' + productos_html[i].producto_servicio_id + '\')" class="fa fa-list btn btn-primary" title="Lista Productos"> </a>';
        } else {
            ObjhtmlProductos = 'Sin obligaciones';
        }
        
        ListaPE.push(ObjhtmlProductos);

        Arreglo_arreglos.push(ListaPE);
    }

    var table = $('#tbl_productos_reporte').DataTable();
    table.destroy();
    console.log(Arreglo_arreglos);
    $('#tbl_productos_reporte').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        data: Arreglo_arreglos,
        columns: [
            { title: "Nombre producto" },
            { title: "Clave" },
            { title: "Cantidad" },
            { title: "Obligaciones" }
        ]
    });
}

function btnListaSanciones(ProductosID) {
    console.log(ProductosID);
    var obligaciones_html = JSON.parse($('#' + 'ArregloSanciones_' + ProductosID).html());
    console.log(obligaciones_html);
    $('#ReporteObligaciones').modal({ backdrop: 'static', keyboard: false });
    $('#ReporteObligaciones').modal('show');
    var Arreglo_arreglos = [];

    for (var i = 0; i <= obligaciones_html.length - 1; i++) {
        var ListaPE = [];
        ListaPE.push(obligaciones_html[i].obligacion);
        ListaPE.push(obligaciones_html[i].clausula);
        ListaPE.push(obligaciones_html[i].comentarios);
        ListaPE.push(obligaciones_html[i].tipo_obligacion);
        ListaPE.push(obligaciones_html[i].sansion);
        ListaPE.push(obligaciones_html[i].tbl_periodo_periodo);
        ListaPE.push(obligaciones_html[i].tbl_tipo_prioridad_nombre);
        var cumplio = obligaciones_html[i].obligacion_cumplida > 0 ? 'Cumplida' : 'No cumplida';
        var color = returnObligacionColor(obligaciones_html[i].obligacion_cumplida);
        var lblStatus = '<label style="color:' + color + ' " >' + cumplio + '</label>';
        ListaPE.push(lblStatus);
        Arreglo_arreglos.push(ListaPE);
    }

    var table = $('#tbl_obligaciones_reporte').DataTable();
    table.destroy();
    console.log(Arreglo_arreglos);
    $('#tbl_obligaciones_reporte').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        data: Arreglo_arreglos,
        columns: [
            { title: "Obligación" },
            { title: "Cláusula" },
            { title: "Comentario" },
            { title: "Tipo obligación" },
            { title: "Sanción" },
            { title: "Periodo" },
            { title: "Prioridad" },
            { title: "Estatus" }
        ]
    });
}
function returnObligacionColor(cumplida) {
    if (cumplida > 0) {
        return 'green';
    } else {
        return 'red';
    }
}