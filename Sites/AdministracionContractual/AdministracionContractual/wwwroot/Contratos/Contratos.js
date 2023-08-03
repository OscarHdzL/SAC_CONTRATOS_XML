
//////////////////////////////INIT////////////////////////////////////////////////////////
var listFiles = [];
var listTypesDocuments = [];
$(document).ready(function () {

    //INIT_Fechas();
    //INIT_Areas();
    //INIT_Firmantes();
    //INIT_Proveedores();
    //INIT_adicionales();
    datepickerFechas();
    setInterval('validateTimeline();validaBag();', 1000);
    Get_lista_tipo_documento();
    setTimeout(function () {
        GetproyectosPorDependencia();
        Gettipocontraato();

    }, 1000);



});

//////////////////////////////INIT////////////////////////////////////////////////////////

function Get_lista_tipo_documento() {
    const id_instancia = $("#HDidInstancia").val();
    $.get($("#EndPointAdmon").val() + "Catalogos/Get/TDocumento/" + id_instancia, (data, status) => {
        listTypesDocuments = data.map(data => ({ id: data.id, name: data.tipo_documento }));

        Body = "<option value='0' selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++)
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].tipo_documento + "</option>";

        $('#select-type-document').html(Body);
    });

}

formatNumberInput = (e) => e.value = formatNumber(e.value);

formatNumber = (e) => e.replace(/\D/g, "")
    .replace(/([0-9])([0-9]{2})$/, '$1.$2')
    .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");

function callDatosGenerales() {
    datepickerDatosGenerales();
    INIT_datosgenerales();


}

function callFechas_parcial() {
    $(".div_areas").hide();
    $(".div_datosgenerales").hide();
    $(".div_fechas").show();
    $(".div_firmantes").hide();
    $(".div_deductivas").hide();
    $(".div_proveedores").hide();
    INIT_Fechas();
}

function callAreas() {
    $(".div_areas").show();
    $(".div_datosgenerales").hide();
    $(".div_fechas").hide();
    $(".div_firmantes").hide();
    $(".div_deductivas").hide();
    $(".div_proveedores").hide();
    INIT_Areas();
}

function callFirmantes() {
    $(".div_areas").hide();
    $(".div_datosgenerales").hide();
    $(".div_fechas").hide();
    $(".div_firmantes").show();
    $(".div_deductivas").hide();
    $(".div_proveedores").hide();
    INIT_Firmantes();
}

function callProveedores() {
    $(".div_areas").hide();
    $(".div_datosgenerales").hide();
    $(".div_fechas").hide();
    $(".div_firmantes").hide();
    $(".div_deductivas").hide();
    $(".div_proveedores").show();
    INIT_Proveedores();
}


function callpenalizacion() {
    $(".div_areas").hide();
    $(".div_datosgenerales").hide();
    $(".div_fechas").hide();
    $(".div_firmantes").hide();
    $(".div_deductivas").show();
    $(".div_proveedores").hide();
    INIT_adicionales();
}

function validateTimeline() {
    //if ($('#objeto_contrato_temp').val() == '' && !$(".div_datosgenerales").is(":visible")) {
    //    $(".div_areas").hide();
    //    $(".div_datosgenerales").show();
    //    $(".div_fechas").hide();
    //    $(".div_firmantes").hide();
    //    $(".div_deductivas").hide();
    //    $(".div_proveedores").hide();
    //    callDatosGenerales();
    //}
}


function validaBag() {
    //console.log('validando');
    //debugger;
    if ($('#a1').val() == "1") {
        $('.tcre_2').removeClass('secondary');
        $('.tcre_2').addClass('primary');
        $('.tcre_1').removeClass('secondary');
        $('.tcre_1').addClass('primary');
    }
    if ($('#a2').val() == "1") {
        $('.tcre_3').removeClass('secondary');
        $('.tcre_3').addClass('primary');
    }
    if ($('#a3').val() == "1" > 0) {
        $('.tcre_4').removeClass('secondary');
        $('.tcre_4').addClass('primary');
    }
    if ($('#a4').val() == "1" > 0) {
        $('.tcre_5').removeClass('secondary');
        $('.tcre_5').addClass('primary');
    }
    if ($('#a5').val() == "1" > 0) {
        $('.tcre_6').removeClass('secondary');
        $('.tcre_6').addClass('primary');
    }
    if ($('#a6').val() == "1" > 0) {
        $('.tcre_7').removeClass('secondary');
        $('.tcre_7').addClass('primary');
    }

}

function click_bag(value) {

    $(".div_areas").hide();
    $(".div_datosgenerales").hide();
    $(".div_fechas").hide();
    $(".div_firmantes").hide();
    $(".div_deductivas").hide();
    $(".div_proveedores").hide();
    $(".div_deductivas").hide();
    $(".formalizacion_div").hide();

    if (value == '1') {
        $(".div_datosgenerales").show();
        callDatosGenerales();
    }
    else if (value == '2') {
        $(".div_fechas").show();
    }
    else if (value == '3') {
        $(".div_areas").show();
    }
    else if (value == '4') {
        $('#firmantes_tbl').DataTable();
        $(".div_firmantes").show();
    }
    else if (value == '5') {
        $(".div_proveedores").show();
    }
    else if (value == '6') {
        $(".div_deductivas").show();
    }
    else if (value == '7') {
        $(".formalizacion_div").show();
        $(".div_datosgenerales").hide();
    }

}

loadFile = () => {

    const idTypeDocument = $('#select-type-document').val();
    const file = $('#doc_formalizacion').prop('files')[0];

    if (file && idTypeDocument != 0) {
        const bytesToMegaBytes = bytes => bytes / (1024 ** 2);
        const sizeFile = bytesToMegaBytes(file.size);

        if (sizeFile < 26) {
            if (file.type == "application/pdf" || file.type == "application/vnd.openxmlformats-officedocument.wordprocessingml.document") {
                listFiles.push({
                    idTypeDocument: idTypeDocument,
                    file: file
                });


                $(`#select-type-document option[value='${idTypeDocument}']`).remove();
                const html = listFiles.map(data => `<tr>
                                            <td>${listTypesDocuments.find(type => type.id == data.idTypeDocument).name}</td>
                                            <td>${data.file.name}
                                                &nbsp
                                                <em class="fa fa-trash text-danger"  style="cursor: pointer" title="Eliminar documento" onclick="deleteFile('${idTypeDocument}')"></em>
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

deleteFile = (idTypeDocument) => {
    const typeDocument = listTypesDocuments.find(type => type.id == idTypeDocument);

    listFiles = listFiles.filter(file => file.idTypeDocument != idTypeDocument);

    const html = listFiles.map(data => `<tr>
                                            <td>${listTypesDocuments.find(type => type.id == data.idTypeDocument).name}</td>
                                            <td>${data.file.name}
                                                &nbsp
                                                <em class="fa fa-trash text-danger"  style="cursor: pointer" title="Eliminar documento" onclick="deleteFile('${data.idTypeDocument}')"></em>
                                                </td>
                                            </tr>`);

    $('#select-type-document').append($('<option>', { value: typeDocument.id, text: typeDocument.name }));

    $('#table-files-body').html(html);
}

function uploadfile_(contrato) {

    var response_ = '';
    listFiles.forEach(data => {

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
                var objresponse = JSON.parse(data);
                response_ = '';
            }
        });
    });

    return response_;

}

function save_contrato() {

    var uri = $('#EndPointAdmon').val() + 'Contratos/Fase/Alta/v2';
    var obj = JSON.parse($('#objeto_contrato_temp').val());
    obj.p_tbl_prioridad_id = $('#cmb_prioridad').val();
    obj.p_tbl_estatus_contrato_id = $('#cmb_estatus').val();
    obj.p_tbl_procedimiento_id = $('#cmb_procedimiento').val();
    obj.p_porc_max_penalizacion = $('#PMP').val();
    obj.p_porc_max_deductivas = $('#PMD').val();
    obj.p_porc_garantia = $('#PG').val();
    obj.p_monto_garantia = $('#MG').val();
    obj.p_activo = "1";

    obj.p_tbl_dependencia_id = $("#dependenciaContrato").val();
    obj.p_estructura_asignado_id = $("#areaAsignadaContrato").val();
    obj.p_tipo_estructura = parseInt($("#nivelAreaAsignadaContrato").val());

    //////////////////////////////////////////////////

    //debugger;

    var nx = { firmantes: JSON.parse($('#firmantes_hd').val()), Responsable: $('#responsables_hd').val().replace('#res_', ''), Contrato: "" }

    ///////////////////////////////////////////////////

    var proveedores = $('#proveedores__').val();

    //////////////////////////////////////////////////

    var Presupuestos_str = JSON.stringify(baseline);

    //////////////////////////////////////////////////

    var areas_v2 = JSON.stringify(baseline);


    var response_ = '';
    var form_data_file = new FormData();

    form_data_file.append('contrato', JSON.stringify(obj));
    form_data_file.append('rc', JSON.stringify(nx));
    form_data_file.append('ProvContr', proveedores);
    form_data_file.append('LstPres', Presupuestos_str);
    form_data_file.append('areas_v2', areas_v2);



    $.ajax({
        url: uri,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data_file,
        type: 'POST',
        async: false,
        success: function (data) {
            console.log(JSON.parse(data));
            var cotr = JSON.parse(data);
            var uplo__ = uploadfile_(cotr.msg);
            obj.p_token = uplo__;
            obj.p_id = data.msg;


            Swal.fire({
                type: 'success',
                title: 'Operación Completada',
                text: 'Contrato guardado con exito.'
            });



            window.location.href = "/contratos/Lista";
            //    error: function (data) {
            //    },
            //    processData: false,
            //    type: 'POST',
            //    url: uri  

            //});
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            response_ = '';
        }
    });
    return response_;
}


function save_contrato_actualiza() {
    var response_ = '';
    $('#Loaderformaliz').modal('show');

    setTimeout(function () {


        if ($('#estatus_act').val() == "0") {

            Swal.fire({
                type: 'error',
                title: 'Error en los datos de entrada',
                text: 'Corriga los datos faltantes.'
            });
            $('#Loaderformaliz').modal('hide');

            return false;
        }

        var uri = $('#EndPointAdmon').val() + 'Contratos/Fase/Update';
        var obj = JSON.parse($('#objeto_contrato_temp').val());
        console.log(obj);
        //debugger;
        obj.p_numero = $('#txt_numero_contratos').val();
        obj.p_objeto = $('#txt_objeto_contratos').val();
        obj.p_nombre = $('#txt_nombre_contrato_contratos').val();
        obj.p_tbl_proyecto_id = $('#cmb_proyecto_contratos').val();
        obj.p_id = $('#contrato_identifier').val();
        obj.p_tbl_prioridad_id = $('#cmb_prioridad').val();
        obj.p_tbl_estatus_contrato_id = $('#cmb_estatus').val();
        obj.p_tbl_procedimiento_id = $('#cmb_procedimiento').val();
        obj.p_porc_max_penalizacion = $('#PMP').val();
        obj.p_porc_max_deductivas = $('#PMD').val();
        obj.p_porc_garantia = $('#PG').val();
        obj.p_monto_garantia = $('#MG').val();
        obj.p_activo = "1";

        obj.p_tbl_dependencia_id = $("#dependenciaContrato").val();
        obj.p_estructura_asignado_id = $("#areaAsignadaContrato").val();
        obj.p_tipo_estructura = parseInt($("#nivelAreaAsignadaContrato").val());

        obj.p_tbl_tipo_contrato_id = $('#cmb_tipo_contrato_contratos').val();
        obj.p_ampliacion = "0";
        obj.p_requiere_renovacion = "0";
        obj.p_satisfactorio = "0";
        obj.p_presento_garantia = "0";
        obj.p_es_administradora = "0";
        obj.p_monto_max_sin_iva = $("#txt_maxsiniva_contratos").val().replaceAll(',', '');
        obj.p_monto_min_sin_iva = $("#txt_minsiniva_contratos").val().replaceAll(',', '');
        obj.p_necesidad = $("#cmb_tipo_contratacion_contratos").val();
        //debugger;

        obj.p_fecha_Iinicio = $("#txt_fecha_inicio_contrato_contratos").val();
        obj.p_fecha_fin = $("#txt_fecha_fin_contrato_contratos").val();

        //////////////////////////////////////////////////


        var nx = { firmantes: JSON.parse($('#firmantes_hd').val()), Responsable: $('#responsables_hd').val().replace('#res_', ''), Contrato: "" }

        ///////////////////////////////////////////////////

        var proveedores = $('#proveedores__').val();

        //////////////////////////////////////////////////

        var Presupuestos_str = JSON.stringify(baseline);

        //////////////////////////////////////////////////

        var areas_v2 = JSON.stringify(baseline);



        var form_data_file = new FormData();

        form_data_file.append('contrato', JSON.stringify(obj));
        form_data_file.append('rc', JSON.stringify(nx));
        form_data_file.append('ProvContr', proveedores);
        form_data_file.append('LstPres', Presupuestos_str);
        form_data_file.append('areas_v2', areas_v2);



        $.ajax({
            url: uri,
            dataType: 'text',
            cache: false,
            contentType: false,
            processData: false,
            data: form_data_file,
            type: 'POST',
            async: false,
            success: function (data) {
                console.log(JSON.parse(data));
                var cotr = JSON.parse(data);
                console.log("Eddy", cotr);
                var uplo__ = uploadfileActualizar_(cotr.msg);

                $('#btn_formaliz').hide();
                Swal.fire({
                    type: 'success',
                    title: 'Operación Completada',
                    text: 'Contrato guardado con exito.'
                });

                window.location.href = "/contratos/Lista";
            },
            error: function (data) {
                var objresponse = JSON.parse(data);
                response_ = '';
            }
        });
        return response_;
    }, 1000);






}

