$(document).ready(function () {
    LaunchLoader(true);
	$('#tbl_doc_adjs, #tbl_m_partidas').DataTable({
		"language": {
			"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
		},
		"pageLength": 5,
		"lengthMenu": [5]
    });
    GetDocumentos();
    Get_suficiencia_autorizada();
    GetSolic();
    setTimeout(function () {
        LaunchLoader(false);
    }, 1000)
});

var json_person = [];
var con = "https://localhost:44359/solicitud/";

$("#ver_pres").click(function () {
	$('#ModalVerPresupuesto').modal('show');
});

function GoBandeja() {
    window.location.href = "/Bandeja";
}

function GetDocumentos() {
    $.get(con + "Get_Documentos_Solicitud/" + $('#id_solicitud').val(), function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var InternoArr = [];
            InternoArr.push(data[i].nombre_documento);
            InternoArr.push(data[i].tipo_documento);
            //InternoArr.push("<button class='btn btn-info' onclick=\"ViewDocto('" + data[i].token + "');\">Ver...</button>");
            InternoArr.push("<button class='btn btn-primary' onclick=\"ViewDocto('" + data[i].token + "');\"><span class='glyphicon glyphicon-eye-open'></span></button>");
            Arreglo_arreglosdot.push(InternoArr);
        }

        var table = $('#tbl_doc_adjs').DataTable();

        table.destroy();

        $('#tbl_doc_adjs').DataTable({
            data: Arreglo_arreglosdot,
            columns: [
                { title: "Nom. documento" },
                { title: "Tipo de documento" },
                { title: "Acción" }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });

    });
}

function ViewDocto(token_) {
    var RES_ = '';
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/10";
    //alert(Uri);
    var URIENC = '';
    $.get(Uri, function (data, status) {
        //alert(data);
        URIENC = data;
        RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
        var SCRH = ((screen.height / 4) * 3) - 40;
        $('#viewer_window_iframe').css('height', SCRH);
        $('#viewer_window_iframe').attr('src', RES_);
        $('#viewer_window').modal('show');
        return RES_;
    });

}

$("#ver_pres").click(function () {
    get_table_m_partidas();
    $('#ModalVerPresupuesto').modal('show');
});

function get_table_m_partidas() {
    var Arreglo_arreglos = [];
    var monto_solic = 0;
    for (var i = 0; i <= json_person.length - 1; i++) {
        var Interno = [];
        Interno.push(json_person[i].p_capitulo + ' - ' + json_person[i].p_capitulo_des);
        Interno.push(json_person[i].des_personal);
        Interno.push(json_person[i].des_numero);
        Interno.push(json_person[i].monto_por_ejercer);
        Interno.push("-");
        //Interno.push("<button class='btn btn-primary' onclick=(edit_cap_gast(" + i + "))><i class='fa fa-edit'></i></button> ");
        monto_solic = monto_solic + parseInt(json_person[i].monto_por_ejercer);
        Arreglo_arreglos.push(Interno);
    }
    var table = $('#tbl_m_partidas').DataTable();
    table.destroy();
    $('#tbl_m_partidas').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "pageLength": 5,
        "lengthMenu": [5],
        data: Arreglo_arreglos,
        columns: [
            { title: "Capitulo gasto" },
            { title: "No. partida" },
            { title: "Descripción" },
            { title: "Monto" },
            { title: "Acción" }
        ],
        columnDefs: [
            {
                targets: '_all',
                className: 'dt-body-center',
                width: '50%', targets: 0,
                width: '5%', targets: 1,
                width: '30%', targets: 2,
                width: '10%', targets: 3,
                width: '5%', targets: 4,
            }]
    });
    $('#m_monto_solic').val(monto_solic);
}

function GetSolic() {
    $.get(con + "Get_Solicitud_suficiencia_det/" + $('#id_solicitud').val(), function (data, status) {
        if (data !== null) {
            var fecha_sol_suf = (data.inclusion).split('T');
            $('#fecha_sol_suf').val(fecha_sol_suf[0]);
            $('#folio_sol').val(data.num_solicitud);
            $('#tipo_solic').val(data.tipo_solicitud);
            $('#tipo_contrato').val(data.tipo_contrato_solicitud);
            var fecha_solicitud = (data.fecha_solicitud).split('T');
            $('#fecha_solicitud').val(fecha_solicitud[0]);
            $('#elaboro').val(data.elaboro);
            $('#dependencia, #m_dep').val(data.dependencia);
            $('#area_solic, #m_area').val(data.area);
            $('#proyecto').val(data.proyecto);
            $('#serv_bien').val(data.nombre_bien_servicio);
            $('#mon_autor').val(data.monto_autorizado);
            $('#solicitante').val(data.solicitante);
            json_person = jQuery.parseJSON(data.json_pres);
            if (data.visita_sitio == true) {
                $('#visita_sitio').prop("checked", true);
            } else if (data.visita_sitio == false) {
                $('#visita_sitio').prop("checked", false);
            }
        }
    });
}

function Get_suficiencia_autorizada() {
    $.get(con + "solicitud/Get_Solicitud_Est_Merc/" + $('#id_solicitud').val(), function (data, status) {
        if (data !== null) {
            var fecha_aut_sol_suf = (data.fecha_autorizacion_suf).split('T');
            $('#f_autrz').val(fecha_aut_sol_suf[0]);
            $('#foli_autrz').val(data.folio_autorizacion_suf);
            $('#autorizo').val(data.autorizo_suf);
            $('#fuen_financ').val(data.fuente_financiamiento_suf);
            $('#Comentarios').val(data.comentarios_suf);
            
        }
    });
}
