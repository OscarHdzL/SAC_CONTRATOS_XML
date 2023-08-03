var URL_SERVICIO_BASE = URL_OBTENER_NOTIFICACIONES_SANCIONES = "";
var URL_PLANES_OBLIGACION =  "";

$.extend($.fn.dataTable.defaults, {
    responsive: true
});

function Periodo() {
    var date = $('#datetimepicker11').val();
    var elem = date.split('/');
    var mes = elem[0];
    var año = elem[1];
    return año + "-" + mes;
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
            { "width": "5%", "targets": 0 },
            { "width": "5%", "targets": 1 },
            { "width": "30%", "targets": 2 },
            { "width": "30%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            { "width": "5%", "targets": 6 },
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

    URL_PLANES_OBLIGACION =  URL_SERVICIO_BASE + 'NotificacionesSanciones/Get/planes/obligacion/'

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
    GetVistaContrato($('#ContratosNS').val());
    $('#listado_hid').show();
    $('#datetimepicker11').val('');
    $('#NotificacionSanciones').html('');
})
function GetVistaContrato(idContrato) {
    $('.Clean').val('');
    $('#Buscar').prop('disabled', false);
    $('#datetimepicker11').prop('disabled', false);
    $.get(URL_VISTACONTRATO + idContrato, function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {
            //$('#txt_numcon1').val(data.NumeroContrato);
            $('#txt_nomcont1').val(data[i].nombre);
            $('#txt_objcon1').val(data[i].objeto);
            $('#txt_monmax1').val(data[i].monto_max_sin_iva);
            $('#txt_monmin1').val(data[i].monto_min_sin_iva);
            var fechaIni = (data[i].fecha_Iinicio.split('T'));
            var fechaFin = (data[i].fecha_fin.split('T'));
            $('#txt_finicio1').val(fechaIni[0]);
            $('#txt_ffin1').val(fechaFin[0]);
        }
    });
}

$('#Buscar').click(function () {
    if ($('#datetimepicker11').val() === '') {
        return ErrorSA('Error en los datos de entrada', 'El campo "periodo" no puede estar vacío');
    }

    GetNotificacionSanciones();
});

function GetNotificacionSanciones() {
    var idContrato = $('#ContratosNS').val();
    var periodo = Periodo();

    $.get(URL_OBTENER_NOTIFICACIONES_SANCIONES + idContrato + "/Periodo/" + periodo, function (data) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {

            //var data1 = data[i].AreasResponsables
            //var data2 = data[i].Responsables;
            //var Areas = [];
            //var responsables = [];
            //for (var x = 0; x <= data1.length - 1; x++) {

            //    Areas.push(x + 1 + '.-' + " " + data1[x].TBLENT_AREA.AREA);

            //}
            //for (var z = 0; z <= data2.length - 1; z++) {

            //    responsables.push(z + 1 + '.-' + " " + data2[z].TBLENT_SERVIDOR_PUBLICO.NOMBRE + " " + data2[z].TBLENT_SERVIDOR_PUBLICO.AP_PATERNO + " " + data2[z].TBLENT_SERVIDOR_PUBLICO.AP_MATERNO);
            //}

            console.log(data)

            var NS = [];
            NS.push(i + 1);
            NS.push(data[i].sancion);
            //NS.push(Areas.toString().replace(/,/g, '</br>'));
            //NS.push(responsables.toString().replace(/,/g, '</br>'));
            NS.push(data[i].areas);
            NS.push(data[i].responsables);
            NS.push(data[i].obligacion);
            NS.push(data[i].clausula);
            //NS.push('<a onclick="btnListaPE(\'' + data[i].id_obligacion + '\')" class="fa fa-list btn btn-primary" title="Lista de PE"> </a> <a onclick="btnListaPM(\'' + data[i].id_obligacion + '\')" class="fa fa-line-chart btn btn-danger" title="Lista de PM"> </a>');
            NS.push('<a onclick="btnListaPE(\'' + data[i].id_obligacion + '\')" class="fa fa-list btn btn-primary" title="Lista de PE"> </a>');

            Arreglo_arreglos.push(NS);
        }

        var table = $('#NotificacionSanciones').DataTable();

        table.destroy();

        //console.log(Arreglo_arreglos);

        $('#NotificacionSanciones').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Tipo de sanción" },
                { title: "Área responsable" },
                { title: "Responsable" },
                { title: "Obligación incumplida" },
                { title: "Cláusula" },
                { title: "" }
            ]
        });

    });

}

function btnListaPE(item) {
    $('#ListaPE').modal({ backdrop: 'static', keyboard: false });
    $('#ListaPE').modal('show');
    var periodo = Periodo();
    GetListaPE(item, periodo);
}
function GetListaPE(item, periodo) {
    
    $.get(URL_PLANES_OBLIGACION + item + "/PE/" + periodo, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var ListaPE = [];
            ListaPE.push(i + 1);
            ListaPE.push(data[i].identificador);
            ListaPE.push(data[i].periodo);
            var fecha = data[i].fecha_ejecucion.split('T');
            ListaPE.push(fecha[0]);
            Arreglo_arreglos.push(ListaPE);
        }
        var table = $('#PlanEntregaNC').DataTable();
        table.destroy();
        console.log(Arreglo_arreglos);
        $('#PlanEntregaNC').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Nombre PE" },
                { title: "Periodo PE" },
                { title: "Fecha de ejecución PE" }
            ]
        });
    });
}



function btnListaPM(item) {
    $('#ListaPM').modal({ backdrop: 'static', keyboard: false });
    $('#ListaPM').modal('show');
    var periodo = Periodo();
    //GetListaPM(item, periodo);
}

function GetListaPM(idObl, periodo) {
    $.get("/Request/Sanciones/Get/PE/Incumplidos/" + idObl + "/" + periodo, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var ListaPE = [];
            ListaPE.push(i + 1);
            Arreglo_arreglos.push(ListaPE);
        }
        var table = $('#PlanMonitoreoNC').DataTable();
        table.destroy();
        console.log(Arreglo_arreglos);
        $('#PlanMonitoreoNC').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Tipo sanción" },
                { title: "Áreas involucradas" }
            ]
        });
    });
}