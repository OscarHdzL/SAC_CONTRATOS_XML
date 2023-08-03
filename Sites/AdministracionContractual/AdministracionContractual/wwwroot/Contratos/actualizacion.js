
document.onreadystatechange = function () {
    try {
        LaunchLoader(true);
    }
    catch (err) { }
    if (document.readyState == "complete") {

        $('.valDatGrales ,.valFechar_ ,.validateareas_btn , .firmantesupd_btn, .proveedoresupd_btn ,.dadicionales_btn').hide();

        LaunchLoader(true);
        setTimeout(function () {
            call_area_saved();
            call_proveedores_saved();

            //call_proveedores_saved
            setInterval('call_proveedores_saved();call_Firmantes_saved();validacion_completa_update()', 250);
            Get_lista_tipo_documento();
            $('#fromalizacion_ins').hide();
            $('#fromalizacion_doc').hide();
            $('#fromalizacion_doc_actualizar').show();

            $('#fromalizacion_upd').show();

            $(".btn-fomali").click(function () {
                $('#Loaderformaliz').modal('show');
                save_contrato_actualiza();
            });


        }, 2500);

        fill__();

    }
}


function call_Firmantes_saved() {
    if ($('#firmates_censo').val() == "0") {
        var tmp_ = [];

        $.get($("#EndPointAdmon").val() + 'Contratos/GET/Json/Responsable/' + $('#contrato_identifier').val(), function (data, status) {
            try {
                for (var i = 0; i <= data.firmantes.length - 1; i++) {
                    tmp_.push(data.firmantes[i]);
                    $('#fir_' + data.firmantes[i]).addClass('check-fir');
                }
                $('#res_' + data.responsable).addClass('check-fir');
                $('#firmantes_hd').val(JSON.stringify(tmp_));
                $('#responsables_hd').val(data.responsable);
                $('#firmates_censo').val("1");
            }
            catch {
                $('#firmates_censo').val("1");
            }
        }, 'json');

    }
    else {
        var objfirmantes = JSON.parse($('#firmantes_hd').val());
        for (var i = 0; i <= objfirmantes.length - 1; i++) {
            $('#fir_' + objfirmantes[i]).addClass('check-fir');

        }
        $('#res_' + $('#responsables_hd').val()).addClass('check-fir');

    }
}

Get_lista_tipo_documento = () => {
    const id_instancia = $("#HDidInstancia").val();
    $.get($("#EndPointAdmon").val() + "Catalogos/Get/TDocumento/" + id_instancia, (data, status) => {
        listTypesDocuments = data.map(data => ({ id: data.id, name: data.tipo_documento }));

        Body = "<option value='0' selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++)
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].tipo_documento + "</option>";

        $('#select-type-document').html(Body);

        DocumentsSave();
    }, 'json');
}

function DocumentsSave() {
    $.get($("#EndPointFileAQ").val() + 'ObtenerListaDocumentosContrato/' + $('#contrato_identifier').val(), function (data, status) {
        console.log(data);
        console.log(listTypesDocuments);
        listFiles = data.map(data => ({ uploaded: true, ...data }));

        listFiles.forEach(file => $(`#select-type-document option[value='${file.file_tbl_tipo_documento_id}']`).remove());

        const html = listFiles.map(data => `<tr>
                                            <td>${listTypesDocuments.find(type => type.id == data.file_tbl_tipo_documento_id)?.name}</td>
                                            <td>${data.file_name + '.' + data.file_extension}
                                                &nbsp
                                                <em class="fa fa-eye text-primary"  style="cursor: pointer" title="Visualizar archivo" onclick="getURL('${data.id}')"></em>
                                                &nbsp
                                                <em class="fa fa-trash text-danger"  style="cursor: pointer" title="Eliminar documento" onclick="deleteFileActualizar('${data.file_tbl_tipo_documento_id}',${data.uploaded})"></em>
                                            </td>
                                            </tr>`);

        $('#table-files-body').html(html);
    }, 'json');
}

loadFileActualizar = () => {

    const idTypeDocument = $('#select-type-document').val();
    const file = $('#doc_formalizacion').prop('files')[0];
    if (file && idTypeDocument != 0) {


        const bytesToMegaBytes = bytes => bytes / (1024 ** 2);
        const sizeFile = bytesToMegaBytes(file.size);

        if (sizeFile < 26) {
            if (file.type == "application/pdf" || file.type == "application/vnd.openxmlformats-officedocument.wordprocessingml.document") {

                listFiles.push({
                    uploaded: false,
                    idTypeDocument: idTypeDocument,
                    file: file
                });


                $(`#select-type-document option[value='${idTypeDocument}']`).remove();

                const html = listFiles.map(data => `<tr>
                                            <td>${data.uploaded ? listTypesDocuments.find(type => type.id == data.file_tbl_tipo_documento_id).name : listTypesDocuments.find(type => type.id == data.idTypeDocument).name}</td>
                                            <td>${data.uploaded ? data.file_name + '.' + data.file_extension : data.file.name}
                                                ${data.uploaded ? `
                                                &nbsp
                                                <em class="fa fa-eye text-primary"  style="cursor: pointer" title="Visualizar archivo" onclick="getURL('${data.id}')"></em>
                                                ` : ``}
                                                &nbsp
                                                <em class="fa fa-trash text-danger"  style="cursor: pointer" title="Eliminar documento" onclick="deleteFileActualizar('${data.uploaded ? data.file_tbl_tipo_documento_id : data.idTypeDocument}',${data.uploaded})"></em>
                                            </td>
                                            </tr>`);

                $('#table-files-body').html(html);
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Archivo de tipo invalido.',
                    text: 'El archivo debe ser .DOCX o .PDF'
                });
            }
        } else {
            Swal.fire({
                type: 'error',
                title: 'Archivo mayor a 25 MB.',
                text: 'El tamaño del archivo es: ' + sizeFile.toFixed().toString() + 'MB'
            });
        }

        $('#doc_formalizacion').val('');
        $('#select-type-document').val('0');
    }

}

deleteFileActualizar = (idTypeDocument, uploaded) => {

    const typeDocument = listTypesDocuments.find(type => type.id == idTypeDocument);
    const fileDeleted = listFiles.find(file => file.file_tbl_tipo_documento_id == idTypeDocument);

    listFiles = uploaded ? listFiles.filter(file => file.file_tbl_tipo_documento_id != idTypeDocument) : listFiles.filter(file => file.idTypeDocument != idTypeDocument);;

    const html = listFiles.map(data => `<tr>
                                            <td>${data.uploaded ? listTypesDocuments.find(type => type.id == data.file_tbl_tipo_documento_id)?.name : listTypesDocuments.find(type => type.id == data.idTypeDocument)?.name}</td>
                                            <td>${data.uploaded ? data.file_name + '.' + data.file_extension : data.file.name}
                                                ${data.uploaded ? `
                                                &nbsp
                                                <em class="fa fa-eye text-primary"  style="cursor: pointer" title="Visualizar archivo" onclick="getURL('${data.id}')"></em>
                                                ` : ``}
                                                &nbsp
                                                <em class="fa fa-trash text-danger"  style="cursor: pointer" title="Eliminar documento" onclick="deleteFileActualizar('${data.uploaded ? data.file_tbl_tipo_documento_id : data.idTypeDocument}',${data.uploaded})"></em>
                                            </td>
                                            </tr>`);

    if (typeDocument)
        $('#select-type-document').append($('<option>', { value: typeDocument.id, text: typeDocument.name }));

    $('#table-files-body').html(html);

    if (uploaded)
        $.get($("#EndPointFileAQ").val() + 'DeleteFileContract/' + fileDeleted.id, function (data, status) { }, 'json');

}

function uploadfileActualizar_(contrato) {
    //debugger;
    var response_ = '';
    listFiles.filter(file => file.uploaded == false).forEach(data => {
        //debugger;
        data.uploaded = true;
        var form_data_file = new FormData();
        form_data_file.append('file', data.file);

        $.ajax({
            url: $("#EndPointFileAQ").val() + 'Upload/DocType/' + data.idTypeDocument + '/' + contrato,
            dataType: 'text',
            cache: false,
            contentType: false,
            processData: false,
            data: form_data_file,
            type: 'POST',
            async: false,
            success: function (data) {
                //debugger;
                var token = data.replace(/[ '"]+/g, '');
                $.post($('#EndPointAdmon').val() + 'Contratos/Token',
                    {
                        p_id: contrato,
                        p_token: token
                    },
                    function (result) {

                        console.log(result);

                    });
                response_ = token;
            },
            error: function (data) {
                //debugger;
                var objresponse = JSON.parse(data);
                response_ = '';
            }
        });
    });

    return response_;

}

function call_proveedores_saved() {
    if ($('#prov_hidden').val() == '0') {
        $.get($("#EndPointAdmon").val() + 'Contratos/GET/Json/Proveedores/' + $('#contrato_identifier').val(), function (data, status) {
            var tmp = [];
            if (data.length == 1) {
                tmp.push(data[0]);
            }
            else {
                for (var i = 0; i <= data.length - 1; i++) {
                    tmp.push(data[i]);
                }
                //tmp = JSON.parse(data);
            }
            for (var i = 0; i <= tmp.length - 1; i++) {
                $('#fir_' + tmp[i]).addClass('check-fir');
            }
            $('#proveedores__').val(JSON.stringify(tmp));
            $('#prov_hidden').val('1');
            LaunchLoader(false);
        });
    }
    else {
        var obj_prvitera = JSON.parse($('#proveedores__').val());
        for (var i = 0; i <= obj_prvitera.length - 1; i++) {
            $('#fir_' + obj_prvitera[i]).addClass('check-fir');
        }
    }
}

function call_area_saved() {
    $.get($("#EndPointAdmon").val() + 'Contratos/GET/Json/' + $('#contrato_identifier').val(), function (data, status) {
        $('#JSONupdate').val(data);
        JustUpdate();
    });
}

function fill__() {

    click_bag(1);

    var uri = $('#EndPointAdmon').val() + 'Contratos/GetContratoporid/' + $('#contrato_identifier').val();
    $.get(uri, function (data) {
        $('#objeto_contrato_temp').val(JSON.stringify(data[0]));
        //debugger;
        var proyecto_delay = data[0].p_tbl_proyecto_id;
        var tipocontr_delay = data[0].p_tbl_tipo_contrato_id;

        $('#hd_tokengral').val(data[0].p_token);

        $('#cmb_tipo_contratacion_contratos').val(data[0].p_necesidad);
        $('#cmb_proyecto_contratos').val(data[0].p_tbl_proyecto_id);
        $('#txt_fecha_inicio_contratos').val(data[0].p_fecha_Iinicio.replace('T00:00:00', ''));
        $('#txt_fecha_final_contratos').val(data[0].p_fecha_fin.replace('T00:00:00', ''));

        $('#txt_numero_contratos').val(data[0].p_numero);
        $('#txt_nombre_contrato_contratos').val(data[0].p_nombre);
        $('#txt_objeto_contratos').val(data[0].p_objeto);
        $('#txt_fecha_registro_contratos').val(data[0].p_fecha_firma.replace('T00:00:00', ''));

        $('#cmb_tipo_contrato_contratos').val(data[0].p_tbl_tipo_contrato_id);
        $('#txt_minsiniva_contratos').val(formatNumber(data[0].p_monto_min_sin_iva.toString() + '.00'));
        $('#txt_maxsiniva_contratos').val(formatNumber(data[0].p_monto_max_sin_iva.toString() + '.00'));

        GetFechasProyecto();


        $('#txt_fecha_inicio_contrato_contratos').val(data[0].p_fecha_Iinicio.replace('T00:00:00', ''));
        $('#txt_fecha_fin_contrato_contratos').val(data[0].p_fecha_fin.replace('T00:00:00', ''));

        $("#dependenciaContrato").val(data[0].p_tbl_dependencia_id);
        $("#areaAsignadaContrato").val(data[0].p_estructura_asignado_id);
        $("#nivelAreaAsignadaContrato").val(data[0].p_tipo_estructura);
        catalogPresupuesto();
        INIT_Areas();
        INIT_Proveedores();
        INIT_Firmantes();

        INIT_adicionales();
        setTimeout(function () {
            $('#cmb_proyecto_contratos').val(proyecto_delay);
            $('#cmb_tipo_contrato_contratos').val(tipocontr_delay);
            $('#PMP').val(data[0].p_porc_max_penalizacion);
            $('#PMD').val(data[0].p_porc_max_deductivas);
            $('#PG').val(data[0].p_porc_garantia);
            $('#MG').val(formatNumber(data[0].p_monto_garantia.toFixed(2)));
            console.log('valor desde bd 1: ' + data[0].p_monto_garantia);
            console.log('valor desde bd 2: ' + data[0].p_monto_garantia.toFixed(2));
            console.log('valor desde bd 3: ' + formatNumber(data[0].p_monto_garantia.toFixed(2)));
            $('#cmb_procedimiento').val(data[0].p_tbl_procedimiento_id);
            $('#cmb_prioridad').val(data[0].p_tbl_prioridad_id);
            $('#cmb_estatus').val(data[0].p_tbl_estatus_contrato_id);
        }, 2000);


        $('#Token__').val(data[0].p_token);




    });
}


function getURL(idDocument) {
    //Limpiar viewer
    $('#viewer_window_iframe').attr('src', '');
    //


    var RES_ = '';
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrlDocXIdDoc/' + idDocument + "/10/" + $('#hd_tokengral').val();
    //alert(Uri);
    var URIENC = '';
    //debugger;
    $.get(Uri, function (data, status) {
        //alert(data);
        URIENC = data;
        RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
        //debugger;
        var SCRH = ((screen.height / 4) * 3) - 40;
        $('#viewer_window_iframe').css('height', SCRH);
        $('#viewer_window_iframe').attr('src', RES_);
        modalVisualizacion();
        return RES_;
    });

}

function modalVisualizacion() {
    $('#viewer_window').modal('show');
}