$(document).ready(function () {    
    $('#tbl_doc_adjs').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "pageLength": 5,
        "lengthMenu": [5]
    });
    GetDocumentos();
    GetSolic();
    getdrop_tipo_docto();
    //getFuente_finc();

    $('#fecha_solicitud, #fecha_sol_suf').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    $('#fecha_autorizacion').datetimepicker({
        defaultDate: new Date(),
        format: 'YYYY-MM-DD'
    });
});

var json_person = [];
var con = $('#EndPointAQ').val() + "SerSolicitud/";

$("#ver_pres").click(function () {
    get_table_m_partidas();
    $('#ModalVerPresupuesto').modal('show');
});

function GoBandeja() {
    window.location.href = "/Bandeja";
}

function getdrop_tipo_docto() {
    var instancia = $('#HDidInstancia').val();
    $.get(con + "Get_lista_tipo_documento/" + instancia,
        function (data, status) {
            $('#tipo_dcto').html('<option value="" selected disabled >Seleccione... </option>');
            for (var i = 0; i <= data.length - 1; i++) {
                var item = "<option value='" + data[i].id + "'>" + data[i].tipo_documento + "</option>";
                $('#tipo_dcto').append(item);
            }
        });
}

function GetDocumentos() {
    LaunchLoader(true);
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
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            "pageLength": 5,
            "lengthMenu": [5],
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
    LaunchLoader(false);
}

function getFuente_finc() {
    $.get(con + "Get/Fuente_financiamiento/Dropdown/" + $('#HDidDependencia').val(), function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#fuen_financ').html(body);
    });
}

function get_table_m_partidas() {
    var Arreglo_arreglos = [];
    var monto_solic = 0;
    for (var i = 0; i <= json_person.length - 1; i++) {
        var Interno = [];
        Interno.push(json_person[i].p_capitulo + ' - ' + json_person[i].p_capitulo_des);
        Interno.push(json_person[i].des_personal);
        Interno.push(json_person[i].des_numero);
        Interno.push(json_person[i].monto_por_ejercer);
        Interno.push("<button class='btn btn-primary' onclick=(edit_cap_gast(" + i + "))><i class='fa fa-edit'></i></button> ");
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
                className: 'dt-body-center'
            }]
    });
    $('#m_monto_solic').val(monto_solic);
}

function get_montodisp_area(dep_ed, area_ed, cap_ed) {
    $.get(con + "get_partidas_montos_area_unitario/" + dep_ed + "/" + area_ed + "/" + cap_ed, function (data, status) {
        if (data !== null) {
            for (var i = 0; i <= data.length - 1; i++) {
                var monto = (data[i].cod).split('.');
                $('#area').text(data[i].msg);
                $('#m_monto_disp').val(monto[0]);
            }
            LaunchLoader(false);
        }
    });
}

function edit_cap_gast(index) { 
    var dep_ed = json_person[index].dependencia;
    var area_ed = json_person[index].areaSeleccionada;
    var cap_ed = json_person[index].idcapgast;
    LaunchLoader(true);
    get_montodisp_area(dep_ed, area_ed, cap_ed);
    $('#m_monto_edit').val('');
    $('#index_monto').val(index);
    $('#modal_monto').modal('show');
}

$("#m_monto_ed").click(function () {
    if ($('#m_monto_edit').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Debe ingresar un monto'
        }).then((result) => {
            $('#modal_monto').modal('show');
        })
        return;
    } else if (parseInt($('#m_monto_edit').val()) > parseInt($('#m_monto_disp').val())) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Debe ingresar un monto menor o igual al monto disponible'
        }).then((result) => {
            $('#m_monto_edit').val('');
            $('#modal_monto').modal('show');
        })
        return;
    }
    var inx_ed = $('#index_monto').val();
    json_person[inx_ed].monto_por_ejercer = $('#m_monto_edit').val();
    //console.log(json_person);
    get_table_m_partidas();
});


$("#rechazar").click(function () {
    if ($('#Comentarios').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Para rechazar suficiencia, es requerido un comentario'
        })
        return;
    } else {
        add_sufuciencia('SFRCH', $('#Comentarios').val());
    }
});

$("#en_proceso").click(function () {
    add_sufuciencia('ENSFC', $('#Comentarios').val());
});

$("#autorizar").click(function () {
    var coment = '';
    var Validacion = validarsufic();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);    
    } else {
        if ($('#Comentarios').val() == '') {
            coment = 'Suficiencia autorizada';
        } else {
            coment = $('#Comentarios').val();
        }
        upload_acta();
        add_sufuciencia('SFCAT', coment);        
    }
});

function archivos_to_bd(token, nom_docto) {

    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.token = token;
    OBJ_Form.tbl_tipo_documento_id = $('#tipo_dcto').val();
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud').val();
    OBJ_Form.nom_documento = nom_docto;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        url: (con + "Add_dcto_adj")

    })
}

function upload_acta() {
    var form_data_file = new FormData();
    var file_data = $('#docto_suf').prop('files')[0];
    var filename = file_data.name;
    form_data_file.append('file', file_data);
    $.ajax({
        url: $("#EndPointFileAQ").val() + 'Upload/',
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data_file,
        type: 'POST',
        async: false,
        success: function (data) {
            var token = data.replace(/[ '"]+/g, '');
            archivos_to_bd(token,filename);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
        }
    });
}

$('#editar_presup').click(function () {
    function Confirmacion() {
        return editar_presupuesto();
    }
    var AccionSi = eval(Confirmacion);
    function Negacion() {
        return location.reload(true);
    }
    var AccionNo = eval(Negacion)
    QuestionSA('¡Usted está a punto de modificar un registro...!', '¿En verdad desea continuar? ', 'Si, Continuar', 'No, Cancelar', AccionSi, AccionNo)
})

function editar_presupuesto() {
    var OBJ_Form = {};
    OBJ_Form.p_opt = 3;
    OBJ_Form.p_id = null;
    OBJ_Form.p_tbl_solicitud_id = $('#id_solicitud').val();
    OBJ_Form.p_fecha_autorizacion = $('#fecha_autorizacion').val();
    OBJ_Form.p_folio_autorizacion = '';
    OBJ_Form.p_autorizo = '';
    OBJ_Form.p_tbl_fuente_financiamiento_id = $('#m_monto_solic').val();
    OBJ_Form.p_comentarios = JSON.stringify(json_person);
    OBJ_Form.p_sigla = '';

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',

        success: function (data) {
            var data_b = $.parseJSON(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                Swal.fire({
                    type: 'success',
                    title: 'Operación exitosa',
                    text: data_b[0].msg,
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        $('#ModalVerPresupuesto').modal('hide');
                        location.reload();
                    }
                });
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: (con + "add_suficiencia")

    })
}



function validarsufic() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#foli_autrz').val() == '') {
        Response.Texto = 'Debe ingresar un folio de autorización';
        Response.Bit = true;
        return Response;
    }
    if ($('#autorizo').val() == '') {
        Response.Texto = 'Debe ingresar autorizador';
        Response.Bit = true;
        return Response;
    }
    //if ($('#fuen_financ').val() == null) {
    //    Response.Texto = 'Debe seleccionar fuente de financiamiento';
    //    Response.Bit = true;
    //    return Response;
    //}
    if ($('#docto_suf').val() == '') {
        Response.Texto = 'Debe agregar un archivo de autorización de suficiencia';
        Response.Bit = true;
        return Response;
    } 
    if ($('#tipo_dcto').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de documento a cargar';
        Response.Bit = true;
        return Response;
    } 
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}