$(document).ready(function () { 
    //$('.table').DataTable({
    //    "language": {
    //        "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
    //    },
    //    "pageLength": 5,
    //    "lengthMenu": [5]
    //});
    $('#prov_a_inv_SCZ, #doc_adj_a_inv_SCZ').select2({
        theme: 'classic',
        language: 'es'
    });
});

var con = $('#EndPointAQ').val() + "SerSolicitud/";
var con_file = $('#EndPointFileAQ').val();
var tipo_dictamen = null;
var archivos_array_mail = [];

function Modalsinit(modal, id) {
    if (modal == 'RegistrarEstudio') {
        openREG(id)
    } else if (modal == 'RegistDictamen') {
        openREGDIC(id); 
    } else if (modal == 'SolicitarEstudio') {
        openSEM(id);
    }else if (modal == 'SolicitarCotizacion') {
        openSCG(id);        
    } else if (modal == 'Turnar_SDRM') {
        openTSDRM(id);
    }else if (modal == 'CargarCotizacion') {
        openRCG(id);
    } else if (modal == 'TurnarSolciitudAdq') {
        get_turnar_solicitud();
        openRSA(id);
        $('#G_R_S_A').show();
        $('#A_R_S_A').hide();
        $('#R_S_A').hide();
        $('#RegisSolicAdqu_div_solicitante').show();
        $('#registrar_sol_adq_IP').show();
    } else if (modal == 'AutorizarSolciitudAdq') {
        openRSA(id);
        $('#A_R_S_A').show();
        $('#G_R_S_A').hide();
        $('#R_S_A').hide();
        $('#RegisSolicAdqu_div_solicitante').hide();        
        setTimeout(function () {
            $('.del_archivo').prop('disabled', true);
        }, 500)
    } else if (modal == 'RegistrarSolciitudAdq') {
        openRSA(id);
        $('#R_S_A').show();
        $('#G_R_S_A').hide();
        $('#A_R_S_A').hide();
        $('#RegisSolicAdqu_div_solicitante').show();
        $('#registrar_sol_adq_IP').hide();
        $('#folio_sol_adq_RSA').prop('disabled', false);
    }

}

////////////////////////////////////////// registrar estidio de mercado
function openREG(id) {
    $.get(con + "Get_Solicitud_suficiencia_det/" + id, function (data, status) {
        if (data !== null) {
            $('#folio_sol_REM').val(data.num_solicitud);
            $('#descr_REM').val(data.nombre_bien_servicio);            
            $('#S_E_G').hide();
        }
    });
    getdrop_tipo_docto('tipo_docto_est_merc_REM');
    $('#RegistrarEstudio').modal('show'); 
    $('#id_solicitud_REM').val(id);
}
$("#R_E_G").click(function () {
    if ($('#file_REM').val() == '') {
        Swal.fire({
            type: 'warning',
            title: 'No seleccionó archivo anexo, favor de validar',
            text: 'Desea continuar con el anexo previamente cargado?',
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonText: 'Continuar'
        }).then((result) => {
            if (result.value) {
                add_estudio_mercado('EMTER');
            }
        })
    } else if ($('#tipo_docto_est_merc_REM').val() == null) {
        Swal.fire({
            type: 'warning',
            title: 'No seleccionó tipo de documento, favor de validar',
            text: 'Desea continuar con el anexo previamente cargado?',
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonText: 'Continuar'
        }).then((result) => {
            if (result.value) {
                add_estudio_mercado('EMTER');
            }
        })
    } else {
        LaunchLoader(true);
        upload_file_tobd_REM('EMTER');
    }
});
////////////////////////////////////////// registrar estidio de mercado

////////////////////////////////////////// Turnar a SDRM
function openTSDRM(id) {
    $.get(con + "Get_Solicitud_suficiencia_det/" + id, function (data, status) {
        if (data !== null) {
            $('#folio_sol_TSDRM').val(data.num_solicitud);
            $('#descr_TSDRM').val(data.nombre_bien_servicio);
        }
    });
    getdrop_tipo_docto('tipo_docto_est_merc_TSDRM');
    $('#TurnarA_SDRM').modal('show');
    $('#id_solicitud_TSDRM').val(id);
}
$("#S_E_G_TSDRM").click(function () {
    LaunchLoader(true);
    if ($('#file_TSDRM').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione archivo'
        })
        LaunchLoader(false);
        return;
    } else if ($('#tipo_docto_est_merc_TSDRM').val() == null) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione tipo de documento'
        })
        LaunchLoader(false);
        return;
    } 
    LaunchLoader(true);
    upload_file_tobd_TSDRM();
    
});
function upload_file_tobd_TSDRM() {
    var form_data_file = new FormData();
    var file_data = $('#file_TSDRM').prop('files')[0];
    var tipo_docto = $('#tipo_docto_est_merc_TSDRM').val();
    var nom_docto = file_data.name;
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
            archivos_to_bd_TSDRM(token, tipo_docto, nom_docto);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            LaunchLoader(false);
            ErrorSA('', objresponse);
        }
    });
}
function archivos_to_bd_TSDRM(token, tipo_docto, nom_docto) {
    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.token = token;
    OBJ_Form.tbl_tipo_documento_id = tipo_docto;
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud_TSDRM').val();
    OBJ_Form.nom_documento = nom_docto;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        async: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        url: (con + "Add_dcto_adj"),
        success: function (data) {
            updatesolic_valid('Turnar_SDRM', $('#id_solicitud_TSDRM').val(), 'null');
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}

////////////////////////////////////////// Turnar a SDRM

////////////////////////////////////////// solicitar estidio de mercado
function openSEM(id) {
    $.get(con + "Get_Solicitud_suficiencia_det/" + id, function (data, status) {
        if (data !== null) {
            $('#folio_sol_REM').val(data.num_solicitud);
            $('#descr_REM').val(data.nombre_bien_servicio);            
            $('#R_E_G').hide();
        }
    });
    getdrop_tipo_docto('tipo_docto_est_merc_REM');
    $('#RegistrarEstudio').modal('show');
    $('#id_solicitud_REM').val(id);
}
$("#S_E_G").click(function () {    
    if ($('#file_REM').val() == '') {
        Swal.fire({
            type: 'warning',
            title: 'No seleccionó archivo anexo, favor de validar',
            text: 'Desea continuar con el anexo previamente cargado?',
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonText: 'Continuar'
        }).then((result) => {
            if (result.value) {
                add_estudio_mercado('EMSLC');
            }
        })
    } else if ($('#tipo_docto_est_merc_REM').val() == null) {
        Swal.fire({
            type: 'warning',
            title: 'No seleccionó tipo de documento, favor de validar',
            text: 'Desea continuar con el anexo previamente cargado?',
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonText: 'Continuar'
        }).then((result) => {
            if (result.value) {
                add_estudio_mercado('EMSLC');
            }
        })
    } else {
        LaunchLoader(true);
        upload_file_tobd_REM('EMSLC');
    }
});
function upload_file_tobd_REM(sigla) {
    var form_data_file = new FormData();
    var file_data = $('#file_REM').prop('files')[0];
    var tipo_docto = $('#tipo_docto_est_merc_REM').val();
    var nom_docto = file_data.name;
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
            archivos_to_bd_REM(token, tipo_docto, nom_docto, sigla);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            LaunchLoader(false);
            ErrorSA('', objresponse);
        }
    });
}
function archivos_to_bd_REM(token, tipo_docto, nom_docto, sigla) {
    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.token = token;
    OBJ_Form.tbl_tipo_documento_id = tipo_docto;
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud_REM').val();
    OBJ_Form.nom_documento = nom_docto;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        //url: ($('#EndPointAQ').val() + "SerSolicitud/Add_dcto_adj")
        url: (con + "Add_dcto_adj"),
        success: function (data) {
            add_estudio_mercado(sigla);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}
function add_estudio_mercado(sigla) {
    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud_REM').val();
    OBJ_Form.fecha_evento_estudio = '0001-01-01T00:00:00';
    OBJ_Form.tbl_usuario_id = $('#HDidUsuario').val();
    OBJ_Form.sigla_estatus = sigla;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        url: (con + "add_estudio_mercado"),
        success: function (data) {
            LaunchLoader(false);
            window.location.href = "/Bandeja";
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}
////////////////////////////////////////// solicitar estidio de mercado


//////////////////////////////////////////  dictamen
function openREGDIC(id) {
    $.get(con + "Get_Solicitud_suficiencia_det/" + id, function (data, status) {
        if (data !== null) {
            $('#folio_sol_RDIC').val(data.num_solicitud);
            $('#descr_RDIC').val(data.nombre_bien_servicio);
        }
    });
    $.post(con + "update_sol_metodo/dictamen_descart/" + id +"/null", function (data) {
            if (data[0].cod == 'warning') {
                $('#solic_dict_RDIC').show();
                $('#guardar_solic_RDIC').show();
                $('#guardar_regi_RDIC').hide();
                $('#reg_dict_RDIC').hide();
                get_tipo_dictamen();
            } else if (data[0].cod == 'success') {
                $('#reg_dict_RDIC').show();
                $('#guardar_regi_RDIC').show();
                $('#guardar_solic_RDIC').hide();
                $('#solic_dict_RDIC').hide();
                getdrop_tipo_docto('tipo_docto_dictamen');
            }
    });
    $('#dictamen_modal').modal('show');
    $('#id_solicitud_RDIC').val(id);
}
$("#guardar_solic_RDIC").click(function () {
    if ($('#tipodict_RDIC').val() == null) {
        ErrorSA('', 'Seleccione el tipo de dictamen.');
    } else {
        add_dictamen_sol('ENDCT');
    }
});
$("#guardar_regi_RDIC").click(function () {
    if ($('#folio_dictamen_RDIC').val() == '') {
        ErrorSA('', 'Ingrese folio de dictamen.');
    } else if ($('#file_dictamen_RDIC').val() == '') {
        ErrorSA('', 'Seleccione archivo de dictamen.');
    } else if ($('#tipo_docto_dictamen').val() == null) {
        ErrorSA('', 'Seleccione el tipo de documento.');
    } else {
        upload_file_tobd_REGDIC('DCTRC');
    }
});
$("#tipodict_RDIC").change(function () {
    for (var i = 0; i <= tipo_dictamen.length - 1; i++) {
        if (tipo_dictamen[i].id == $("#tipodict_RDIC").val()) {
            $("#desc_tipo_dict_RDIC").val(tipo_dictamen[i].descripcion);
        }
    }
});
function add_dictamen_sol(sigla) {
    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud_RDIC').val();
    OBJ_Form.fecha_evento_dictamen = '0001-01-01T00:00:00';
    OBJ_Form.tbl_usuario_id = $('#HDidUsuario').val();
    OBJ_Form.tbl_tipo_dictamen_id = $('#tipodict_RDIC').val();
    OBJ_Form.folio_dictamen = $('#folio_dictamen_RDIC').val();
    OBJ_Form.sigla_estatus = sigla;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        url: (con + "add_dictamen_solicitud"),
        success: function (data) {
            LaunchLoader(false);
            window.location.href = "/Bandeja";
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}
function upload_file_tobd_REGDIC(sigla) {
    var form_data_file = new FormData();
    var file_data = $('#file_dictamen_RDIC').prop('files')[0];
    var tipo_docto = $('#tipo_docto_dictamen').val();
    var nom_docto = file_data.name;
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
            archivos_to_bd_REGDIC(token, tipo_docto, nom_docto, sigla);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            LaunchLoader(false);
            ErrorSA('', objresponse);
        }
    });
}
function archivos_to_bd_REGDIC(token, tipo_docto, nom_docto, sigla) {
    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.token = token;
    OBJ_Form.tbl_tipo_documento_id = tipo_docto;
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud_RDIC').val();
    OBJ_Form.nom_documento = nom_docto;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        //url: ($('#EndPointAQ').val() + "SerSolicitud/Add_dcto_adj")
        url: (con + "Add_dcto_adj"),
        success: function (data) {
            add_dictamen_sol(sigla);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}
//////////////////////////////////////////  dictamen

////////////////////////////////////////// Solicitar cotizacion
function openSCG(id) {
    get_drop_proveedores();
    $.get(con + "Get/" + id, function (data, status) {
        if (data !== null) {
            $('#folio_sol_SCZ').val(data.num_solicitud);
            $('#descr_SCZ').val(data.nombre_bien_servicio);
        }
    });
    //GetDocumentosRSA_SCZ(id);
    getdrop_tipo_docto('tipo_dcto_SCZ');
    get_drop_documentos_adjuntos_cotiz_solic(id);
    $('#SolicitarCotizacion').modal('show');
    $('#id_solicitud_docts_SCZ').val(id);
}

$("#S_C_G").click(function () {
    LaunchLoader(true);
    if ($('#prov_a_inv_SCZ').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione interlocutores'
        })
        LaunchLoader(false);
        return;
    }
    else if ($('#doc_adj_a_inv_SCZ').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione documentos para adjuntar al correo'
        })
        LaunchLoader(false);
        return;
    }
    prueba_solic_cotiz();
});
function prueba_solic_cotiz() {
    var data_list_docum = $('#doc_adj_a_inv_SCZ').val();
    for (var i = 0; i <= data_list_docum.length - 1; i++) {
        $.get(con_file + "GeneraUrl/" + data_list_docum[i] + "/" + $('#time_adjuntos').val(), function (data, status) {
            if (data !== null) {
                archivos_array_mail.push(con_file + "Viewer/" + data);
            }
        });
    }
    //console.log(archivos_array_mail);
    setTimeout(function () {
        solicitar_cotizacion();
    }, 500)
}
function solicitar_cotizacion() {
    var data_list_prov = $('#prov_a_inv_SCZ').val();

    var form_data = new FormData();
    form_data.append('OBJ_proveedores', JSON.stringify(data_list_prov));
    form_data.append('OBJ_archivos_adjuntos', JSON.stringify(archivos_array_mail));
    form_data.append('id_solicitud', $('#id_solicitud_docts_SCZ').val());

    $.ajax({
        url: ($('#EndPointAQ').val() + "Cotizaciones/Add"),
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data,
        type: 'post',
        async: false,
        success: function (data) {
            LaunchLoader(false);
            var objresponse = JSON.parse(data);
            if (objresponse != null) {
                Swal.fire({
                    type: 'success',
                    title: 'Cotización solicitada',
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "/Bandeja";
                    }
                });
            }

        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
        }
    });

}
$("#add_SCZ").click(function () {
    LaunchLoader(true);
    if ($('#carga_docts_SCZ').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Cargue un documento'
        })
        LaunchLoader(false);
        return;
    }
    else if ($('#tipo_dcto_SCZ').val() == null) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione tipo de documento'
        })
        LaunchLoader(false);
        return;
    }
    upload_file_tobd_SCZ();
});
function upload_file_tobd_SCZ() {
    var form_data_file = new FormData();
    var file_data = $('#carga_docts_SCZ').prop('files')[0];
    var tipo_docto = $('#tipo_dcto_SCZ').val();
    var nom_docto = file_data.name;
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
            archivos_to_bd_SCZ(token, tipo_docto, nom_docto);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            LaunchLoader(false);
            ErrorSA('', objresponse);
        }
    });
}
function archivos_to_bd_SCZ(token, tipo_docto, nom_docto) {
    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.token = token;
    OBJ_Form.tbl_tipo_documento_id = tipo_docto;
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud_docts_SCZ').val();
    OBJ_Form.nom_documento = nom_docto;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        url: (con + "Add_dcto_adj"),
        success: function (data) {
            $('#carga_docts_SCZ').val('');
            $('#tipo_dcto_SCZ').val('');
            $('#doc_adj_a_inv_SCZ').html('');
            LaunchLoader(false);
            get_drop_documentos_adjuntos_cotiz_solic($('#id_solicitud_docts_SCZ').val());
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}
////////////////////////////////////////// Solicitar cotizacion

////////////////////////////////////////// Cargar cotizacion
function openRCG(id) {
    get_drop_proveedores_CCZ();
    $.get(con + "Get/" + id, function (data, status) {
        if (data !== null) {
            $('#folio_sol_CCZ').val(data.num_solicitud);
            $('#descr_CCZ').val(data.nombre_bien_servicio);
        }
    });
    getdrop_tipo_docto('tipo_dcto_CCZ');
    get_documentos_cotizacion_sol(id);
    $('#CargarCotizacion').modal('show');
    //  $('#C_C_G').hide();
    $('#id_solicitud_docts_CCZ').val(id);
}
function get_documentos_cotizacion_sol(id) {
    $.get($('#EndPointAQ').val() + "Cotizaciones/Get_documentos_cotizacion/" + id, function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var InternoArr = [];
            InternoArr.push(data[i].razon_social);
            InternoArr.push(data[i].nombre_documento);
            InternoArr.push(data[i].tipo_documento);
            InternoArr.push("<button class='btn btn-primary' onclick=\"ViewDocto('" + data[i].token + "');\"><span class='glyphicon glyphicon-eye-open'></span></button> <button class='btn btn-danger del_archivo' id='del_archivo' onclick=\"del_archivo_CTZ('" + data[i].id + "/" + data[i].tbl_solicitud_documento_adjunto_id + "');\"><span class='glyphicon glyphicon-trash'></span></button>");
            Arreglo_arreglosdot.push(InternoArr);
        }

        var table = $('#tbl_docum_adj_CCZ').DataTable();

        table.destroy();

        $('#tbl_docum_adj_CCZ').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            "pageLength": 5,
            "lengthMenu": [5],
            data: Arreglo_arreglosdot,
            columns: [
                { title: "Proveedor" },
                { title: "Nombre documento" },
                { title: "Tipo documento" },
                { title: "Acción" }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
        //getSolicitud();
    });
}
$("#C_C_G").click(function () {
    var estatus_CCG = null;
    var chk_compl = $('#chkbx_CCZ').is(":checked") ? 1 : 0;
    if (chk_compl == 1) {
        estatus_CCG = 'ENESM';
    } else if (chk_compl == 0){
        estatus_CCG = 'CTREC';
    }
    Swal.fire({
        type: 'question',
        title: '¿Cargar cotización?',
        confirmButtonText: 'Cargar',
        confirmButtonClass: 'btn btn-success'
    }).then((result) => {
        if (result.value) {
            //guardar_cotizacion(estatus_CCG);
            updatesolic_valid('Cargar_cotizacion', $('#id_solicitud_docts_CCZ').val(), estatus_CCG);
        }
    })
});
$("#Add_docto_CCZ").click(function () {
    $('#CargarCotizacion').modal('hide');
    LaunchLoader(true);
    if ($('#proveedor_CCZ').val() == null) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione interlocutor'
        }).then((result) => {
            if (result.value) {
                $('#CargarCotizacion').modal('show');
            }
        })
        LaunchLoader(false);        
        return;
    }
    else if ($('#file_CCZ').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione archivo'
        }).then((result) => {
            if (result.value) {
                $('#CargarCotizacion').modal('show');
            }
        })
        LaunchLoader(false);
        return;
    } else if ($('#tipo_dcto_CCZ').val() == null) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione tipo de documento'
        })
        LaunchLoader(false);
        return;
    }
    upload_file_tobd_CCZ();
});
function upload_file_tobd_CCZ() {
    var form_data_file = new FormData();
    var file_data = $('#file_CCZ').prop('files')[0];
    var tipo_docto = $('#tipo_dcto_CCZ').val();
    var nom_docto = file_data.name;
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
            archivos_to_bd_CCZ(token, tipo_docto, nom_docto);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            LaunchLoader(false);
            ErrorSA('', objresponse);
        }
    });
}
function archivos_to_bd_CCZ(token, tipo_docto, nom_docto) {
    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.token = token;
    OBJ_Form.tbl_tipo_documento_id = tipo_docto;
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud_docts_CCZ').val();
    OBJ_Form.nom_documento = nom_docto;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        async: false,
        url: (con + "Add_dcto_adj"),
        success: function (data) {
            var objresponse = JSON.parse(data);
            $('#file_CCZ').val('');
            $('#tipo_dcto_CCZ').val('');
            add_cotizacion_CCZ(objresponse[0].msg, nom_docto, tipo_docto);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}
function add_cotizacion_CCZ(id_docto, nom_docto, tipo_docto) {
    var OBJ_cotizacion_sol = {}
        OBJ_cotizacion_sol.p_opt = 0,
        OBJ_cotizacion_sol.p_id = '',
        OBJ_cotizacion_sol.p_tbl_proveedor_id = $('#proveedor_CCZ').val(),
        OBJ_cotizacion_sol.p_tbl_solicitud_documento_adjunto_id = id_docto,
        OBJ_cotizacion_sol.p_tbl_tipo_documento_id = tipo_docto,
        OBJ_cotizacion_sol.p_tbl_solicitud_id = $('#id_solicitud_docts_CCZ').val(),
        OBJ_cotizacion_sol.p_nom_documento = nom_docto    

    var form_data = new FormData();
    form_data.append('OBJ_cotizacion_sol', JSON.stringify(OBJ_cotizacion_sol));

    $.ajax({
        url: ($('#EndPointAQ').val() + "Cotizaciones/Add_cotizacion"),
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data,
        type: 'post',
        async: false,
        success: function (data) {
            LaunchLoader(false);
            var objresponse = JSON.parse(data);
            if (objresponse != null) {
                Swal.fire({
                    type: 'success',
                    title: 'Cotización solicitada',
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        $('#proveedor_CCZ').val('');
                        get_documentos_cotizacion_sol($('#id_solicitud_docts_CCZ').val());
                        $('#CargarCotizacion').modal('show');
                        //window.location.href = "/Bandeja";
                    }
                });
            }

        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
        }
    });
}
function del_archivo_CTZ(id) {
    Swal.fire({
        type: 'question',
        title: '¿Eliminar archivo?',
        confirmButtonText: 'Eliminar',
        confirmButtonClass: 'btn btn-danger'
    }).then((result) => {
        if (result.value) {
            del_archivo_CTZ_docto_adj_sol(id);
        }
    })
}
function del_archivo_CTZ_docto_adj_sol(id) {
    var id_s_dcts = id.split('/');
    console.log(id_s_dcts[0]);
    console.log(id_s_dcts[1]);
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        type: 'post',
        async: false,
        url: (con + "Delete_dcto_adj/" + id_s_dcts[1]),
        success: function (data) {
            var data_b = $.parseJSON(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                Delete_cotizacion(id_s_dcts[0]);
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    });
}
function Delete_cotizacion(id) {
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        type: 'post',
        async: false,
        url: ($('#EndPointAQ').val() + "Cotizaciones/Delete_cotizacion/" + id),
        success: function (data) {
            var data_b = $.parseJSON(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", data_b[0].msg);
                get_documentos_cotizacion_sol($('#id_solicitud_docts_CCZ').val());
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    });
}

////////////////////////////////////////// Cargar cotizacion

////////////////////////////////////////// Modulo solicitud de adquisiciones
function openRSA(id) {  
    $.get(con + "Get_Solicitud_suficiencia_det/" + id, function (data, status) {
        if (data !== null) {
            $('#folio_sol_RSA').val(data.num_solicitud);
            $('#folio_sol_adq_RSA').val(data.folio_solicitud_adq);
        }
    });
    GetDocumentosRSA_SCZ(id);
    getdrop_tipo_docto('tipo_dcto_RSA');
    $('#id_solicitud_docts').val(id);
    $('#RegisSolicAdqu').modal('show');
    
}
$("#R_S_A").click(function () {
    LaunchLoader(true);
    if ($('#folio_sol_adq_RSA').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Ingrese un folio de solicitud de adquisición'
        })
        LaunchLoader(false);
        return;
    }
    updatesolic_valid('Registro_solic_adq_IP', $('#id_solicitud_docts').val(), $('#folio_sol_adq_RSA').val());
});
$("#A_R_S_A").click(function () {
    LaunchLoader(true);
    updatesolic_valid('Autorizar_solic_adq_IP', $('#id_solicitud_docts').val(), 'null');
});
$("#G_R_S_A").click(function () {
    LaunchLoader(true);
    if ($('#turnar_solic_RSA').val() == null) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione area a turnar'
        })
        LaunchLoader(false);
        return;
    }
    updatesolic_valid('Turnar_solic_adq_IP', $('#id_solicitud_docts').val(), $('#turnar_solic_RSA').val());
});
$("#add_RSA").click(function () {
    LaunchLoader(true);
    if ($('#carga_docts_RSA').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Cargue un documento'
        })
        LaunchLoader(false);
        return;
    }
    else if ($('#tipo_dcto_RSA').val() == null) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'Seleccione tipo de documento'
        })
        LaunchLoader(false);
        return;
    }    
    upload_file_tobd_RSA();
});
function upload_file_tobd_RSA() {
    var form_data_file = new FormData();
    var file_data = $('#carga_docts_RSA').prop('files')[0];
    var tipo_docto = $('#tipo_dcto_RSA').val();
    var nom_docto = file_data.name;
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
            archivos_to_bd_RSA(token, tipo_docto, nom_docto);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            LaunchLoader(false);
            ErrorSA('', objresponse);
        }
    });
}
function archivos_to_bd_RSA(token, tipo_docto, nom_docto) {
    var OBJ_Form = {};
    OBJ_Form.id = null;
    OBJ_Form.token = token;
    OBJ_Form.tbl_tipo_documento_id = tipo_docto;
    OBJ_Form.tbl_solicitud_id = $('#id_solicitud_docts').val();
    OBJ_Form.nom_documento = nom_docto;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',
        url: (con + "Add_dcto_adj"),
        success: function (data) {
            $('#carga_docts_RSA').val('');
            $('#tipo_dcto_RSA').val('');
            LaunchLoader(false);
            GetDocumentosRSA_SCZ($('#id_solicitud_docts').val());
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}
////////////////////////////////////////// Modulo solicitud de adquisiciones

function updateSolic(parametro, id_sol) {
    Swal.fire({
        type: 'question',
        title: 'Actualizar estatus de solicitud?',
        confirmButtonText: 'Actualizar',
        confirmButtonClass: 'btn btn-success btn-lg'
    }).then((result) => {
        if (result.value) {
            updatesolic_valid(parametro, id_sol, 'null');
        }
    })
}
function updatesolic_valid(parametro, id_sol, variable) {

    // para actualizar solicitud
    $.ajax({
        url: (con + "update_sol_metodo/" + parametro + "/" + id_sol + "/" + variable),
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: new FormData(),
        type: 'POST',
        async: false,
        success: function (data) {
            LaunchLoader(false);
            var objresponse = JSON.parse(data);
            if (objresponse != null) {
                Swal.fire({
                    type: 'success',
                    title: 'Solicitud actualizada',
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "/Bandeja";
                    }
                });
            }

        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
        }
    });
}

function GetDocumentosRSA_SCZ(id) {
    $.get(con + "Get_Documentos_Solicitud/" + id, function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var InternoArr = [];
            InternoArr.push(data[i].nombre_documento);
            InternoArr.push(data[i].tipo_documento);
            InternoArr.push("<button class='btn btn-primary' onclick=\"ViewDocto('" + data[i].token + "');\"><span class='glyphicon glyphicon-eye-open'></span></button> <button class='btn btn-danger del_archivo' id='del_archivo' onclick=\"del_archivo('" + data[i].id + "');\"><span class='glyphicon glyphicon-trash'></span></button>");
            Arreglo_arreglosdot.push(InternoArr);
        }

        var table = $('#tbl_docum_adj_RSA').DataTable();

        table.destroy();

        $('#tbl_docum_adj_RSA').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            "pageLength": 5,
            "lengthMenu": [5],
            data: Arreglo_arreglosdot,
            columns: [
                { title: "Nom. documento"},
                { title: "Tipo de documento" },
                { title: "Acción" }
            ],
            columnDefs: [
                { width: "40%", targets: 0},
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
        //getSolicitud();
    });
}

function getdrop_tipo_docto(drop) {
    var instancia = $('#HDidInstancia').val();	
    $.get(con + "Get_lista_tipo_documento/" + instancia, function (data, status) {
        $('#'+drop+'').html('<option value="" selected disabled >Seleccione... </option>');
        for (var i = 0; i <= data.length - 1; i++) {
            var item = "<option value='" + data[i].id + "'>" + data[i].tipo_documento + "</option>";
            $('#' + drop + '').append(item);
        }        
    });

}
function get_tipo_dictamen() {
    var dependencia = $('#HDidDependencia').val();
    $.get(con + "Get_tipo_dictamen/" + dependencia, function (data, status) {
        $('#tipodict_RDIC').html('<option value="" selected disabled >Seleccione... </option>');
        for (var i = 0; i <= data.length - 1; i++) {
            var item = "<option value='" + data[i].id + "'>" + data[i].tipo_dictamen + "</option>";
            $('#tipodict_RDIC').append(item);
        }
        tipo_dictamen = data;
    });
}
function get_turnar_solicitud() {
    var dependencia = $('#HDidDependencia').val();
    $.get(con + "Get/area_turnar/Dropdown/" + dependencia, function (data, status) {
        $('#turnar_solic_RSA').html('<option value="" selected disabled >Seleccione... </option>');
        for (var i = 0; i <= data.length - 1; i++) {
            var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
            $('#turnar_solic_RSA').append(item);
        }        
    });
}
function get_drop_proveedores() {
    $.get($('#EndPointAQ').val() + "SerLicitante/Get/Proveedores/" + $('#HDidInstancia').val(), function (data, status) {
        //$('#prov_a_inv_SCZ').html('<option value="" selected disabled >Seleccione... </option>');
        for (var i = 0; i <= data.length - 1; i++) {
            var item = "<option value='" + data[i].rfc + "'>" + data[i].razon_social + "</option>";
            $('#prov_a_inv_SCZ').append(item);
        }
    });
}
function get_drop_proveedores_CCZ() {
    $.get($('#EndPointAQ').val() + "SerLicitante/Get/Proveedores/" + $('#HDidInstancia').val(), function (data, status) {
        //$('#prov_a_inv_SCZ').html('<option value="" selected disabled >Seleccione... </option>');
        for (var i = 0; i <= data.length - 1; i++) {
            var item = "<option value='" + data[i].id + "'>" + data[i].razon_social + "</option>";
            $('#proveedor_CCZ').append(item);
        }
    });
}
function get_drop_documentos_adjuntos_cotiz_solic(id) {

    $.get(con + "Get_Documentos_Solicitud/" + id, function (data, status) {
        //$('#prov_a_inv_SCZ').html('<option value="" selected disabled >Seleccione... </option>');
        for (var i = 0; i <= data.length - 1; i++) {
            var item = "<option value='" + data[i].token + "'>" + data[i].nombre_documento + "</option>";
            $('#doc_adj_a_inv_SCZ').append(item);
        }
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
function del_archivo(id) {
    function Confirmacion() {
        return del_file_tobd(id);
    }
    var AccionSi = eval(Confirmacion);
    function Negacion() {
        return;
    }
    var AccionNo = eval(Negacion)
    QuestionSA('¡Usted está a punto de eliminar un registro permanentemente...!', '¿En verdad desea continuar? ', 'Si, Continuar', 'No, Cancelar', AccionSi, AccionNo)
}
function del_file_tobd(id) {
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        type: 'post',
        //url: ($('#EndPointAQ').val() + "SerSolicitud/Add_dcto_adj")
        url: (con + "Delete_dcto_adj/" + id),
        success: function (data) {
            var data_b = $.parseJSON(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA('Operación exitosa', 'archivo eliminado');
               GetDocumentosRSA_SCZ($('#id_solicitud_docts').val());                
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    })
}
