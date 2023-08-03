$(document).ready(function () {
    GetInstancias();
    $(".pcr-button").css("width", '50%'); 
    
    $(".exist").each(function () {
        $(this).hide();
    });
    $('#logo_min').hide();
    $('#logo_ini').hide();

});
var con = $("#EndPointAdmon").val();
//var con = "https://localhost:44359/";

function GetInstancias() {
    $.get(con + "Instancia/Get_Drop", function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#Instancia').html(body); 
    });
    return;
}
function muestraModalAgregarInstancia() {
    $('.Clean').val('');
    $('#Gob_fed').prop("checked", false);
    $('#ModalAgregarInstancia').modal('show');
}

$("#GuardarInstancia").click(function () {
    AddInstancia();
});
$("#GuardaInstancia_cambios").click(function () {
    EditInstancia();   
});

function AddInstancia() {
    var Validacion = ValidarIns();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_ins_class;
        OBJ_Form.nombre = $('#Instancia_nom').val();
        OBJ_Form.nombre_corto = $('#Nom_cort').val();
        var i = $('#Gob_fed').is(":checked") ? 1 : 0;
        OBJ_Form.gob_fed = i;

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
                    SuccessSA("Operación exitosa", data_b[0].msg);
                    GetInstancias();
                    $('#ModalAgregarInstancia').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Instancia/Add")
        })
    }
}

function EditInstancia() {
    var Validacion = ValidarInst_Ed();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_ins_class;
        OBJ_Form.id = $('#Instancia').val();
        OBJ_Form.copyright = $('#Copyright').val();
        OBJ_Form.hex_col_header = pickr.getSelectedColor().toHEXA().toString();
        OBJ_Form.hex_col_sidebar = pickr2.getSelectedColor().toHEXA().toString();
        OBJ_Form.hex_background = pickr3.getSelectedColor().toHEXA().toString();
        OBJ_Form.hex_textcolor = pickr4.getSelectedColor().toHEXA().toString();

        var form_data_file = new FormData();
        var file_data = $('#input_b1').prop('files')[0];
        form_data_file.append('file', file_data);

        var form_data_fileb = new FormData();
        var file_datab = $('#input_b2').prop('files')[0];
        form_data_fileb.append('file', file_datab);        

        if (file_data != undefined) {
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
                    var token_r = data.replace(/[ '"]+/g, '');
                   OBJ_Form.token_logo_mini = token_r;

                },
                error: function (data) {
                    var objresponse = JSON.parse(data);
                    ErrorSA('', objresponse);
                }
            });
        }
        if (file_datab != undefined) {
            $.ajax({
                url: $("#EndPointFileAQ").val() + 'Upload/',
                dataType: 'text',
                cache: false,
                contentType: false,
                processData: false,
                data: form_data_fileb,
                type: 'POST',
                async: false,
                success: function (data) {
                    var token_f = data.replace(/[ '"]+/g, '');
                    OBJ_Form.token_logo_inicio = token_f;

                },
                error: function (data) {
                    var objresponse = JSON.parse(data);
                    ErrorSA('', objresponse);
                }
            });
        }

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'put',

            success: function (data) {
                var data_b = $.parseJSON(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", data_b[0].msg);
                    window.location.reload();
                    //$('#ModalEditarDependencia').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Instancia/Update")
        })
    }
}


function ValidarIns() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#Instancia_nom').val() == '') {
        Response.Texto = 'Debe agregar un nombre de instancia';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Instancia_nom').val(), 'Instancia_nom') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Instancia"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Nom_cort').val() == '') {
        Response.Texto = 'Debe agregar una nombre corto de instancia';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Nom_cort').val(), 'Nom_cort') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Nombre corto"';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
function ValidarInst_Ed() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#Instancia').val() == null) {
        Response.Texto = 'Debe seleccionar una instancia';
        Response.Bit = true;
        return Response;
    }
    if ($('#Copyright').val() == '') {
        Response.Texto = 'Debe ingresar un copyright';
        Response.Bit = true;
        return Response;
    }
    if ($('#logo_min').attr('src') == '') {
        if ($('#input_b1').val() == '') {
            Response.Texto = 'Debe seleccionar un logo miniatura';
            Response.Bit = true;
            return Response;
        }
    }
    if ($('#logo_ini').attr('src') == '') {
        if ($('#input_b2').val() == '') {
            Response.Texto = 'Debe seleccionar una imagen de inicio';
            Response.Bit = true;
            return Response;
        }
    }
    //if ($('#input_b2').val() == '') {
    //    Response.Texto = 'Debe seleccionar un logo miniatura';
    //    Response.Bit = true;
    //    return Response;
    //}

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

$("#Instancia").on("change", function () {
    GetInstancia($("#Instancia").val());
    $(".exist").each(function () {
        $(this).show();
    });
});

function GetInstancia(id_ins) {
    $.get(con + "Instancia/Get/" + id_ins, function (data, status) {
        if (data !== null) {
            //for (var i = 0; i <= data.length - 1; i++) {
                $('#Copyright').val(data.copyright);
                pickr.setColor(data.hex_col_header);
                pickr2.setColor(data.hex_col_sidebar);
                pickr3.setColor(data.hex_background);
                pickr4.setColor(data.hex_textcolor);

            if (data.token_logo_mini != null) {
                getURL_min(data.token_logo_mini);                
            } else {
                $('#logo_min').attr('src', '');
                $('#logo_min').hide();
            }
            if (data.token_logo_inicio != null) {
                getURL_ini(data.token_logo_inicio);
            } else {
                $('#logo_ini').attr('src', '');
                $('#logo_ini').hide();
            }
            //}
        }
    });
}

function getURL_min(token_) {
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/1";
    var URIENC = '';
    $.get(Uri, function (data, status) {
        URIENC = data;
        RES = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
        $('#logo_min').attr('src', RES);
        $('#logo_min').show();
        return RES;
    });
}
function getURL_ini(token_) {
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/1";
    var URIENC = '';
    $.get(Uri, function (data, status) {
        URIENC = data;
        RES = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
        $('#logo_ini').attr('src', RES);
        $('#logo_ini').show();
        return RES;
    });
}

var tbl_ins_class = {
    opt: 0,
    id: null,
    nombre: '',
    nombre_corto: '',
    copyright: '',
    gob_fed: 0,
    token_logo_mini: '',
    token_logo_inicio: '',
    hex_col_header: '',
    hex_col_sidebar: '',
    hex_background: '',
    hex_textcolor: ''
}
//var valir1 = pickr.getSelectedColor().toHEXA().toString();
//var valir2 = pickr2.setColor('#FFC107');

