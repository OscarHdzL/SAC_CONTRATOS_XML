$.extend($.fn.dataTable.defaults, {
    responsive: true
});

$(document).ready(function () {
    $('#tbl_PlanEntrega_ubicaciones').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
    });
    getUbicaciones_Plan();

});


function getUbicaciones_Plan() {
    var idPlanMon = $('#idPlanMonitoreo').val();


    $.get($("#EndPointAC").val() + 'Operaciones/PM/Get/Ubicaciones/Plan/' + idPlanMon, function (data, status) {
        if (data.length > 0) {
            var nombre = '';
            var Arreglo_arreglos = [];
            for (var i = 0; i <= data.length - 1; i++) {

                var Interno = [];
                Interno.push(data[i].clave);
                Interno.push(data[i].unidad);
                Interno.push(data[i].direccion);
                Interno.push('<button class="btn btn-success" type="button" onclick="evalMonitoreo(\'' + data[i].tbl_plan_monitoreo_id + '\',\'' + data[i].tbl_ubicacion_id + '\');">Evaluar plan de entrega</button>  <button style="display:none" id="btn_data[i].tbl_ubicacion_id" onclick="ConfirmPMfnc(\'' + data[i].tbl_plan_monitoreo_id + '\',\'' + data[i].tbl_ubicacion_id + '\');" class="btn btn-info" type="button">Confirmar plan Monitores</button>');
               //Interno.push('<button style="display:none" id="btn_data[i].tbl_ubicacion_id" onclick="ConfirmPMfnc(\'' + data[i].tbl_plan_monitoreo_id + '\',\'' + data[i].tbl_ubicacion_id + '\');" class="btn btn-info" type="button">Confirmar plan Monitores</button>');
                
                
                Arreglo_arreglos.push(Interno);
            }

            var table = $('#tbl_PlanEntrega_ubicaciones').DataTable();

            table.destroy();
            console.log(Arreglo_arreglos);
            $('#tbl_PlanEntrega_ubicaciones').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
                },
                data: Arreglo_arreglos,
                columns: [
                    { title: "Identificador" },
                    { title: "Tipo" },
                    { title: "Clave" },
                    { title: "Ejecutar PM" }


                ]
            });
            LaunchLoader(false);

        } else { LaunchLoader(false); }
    });


}



function execute(obligacion) {

    var bit = 0;




    $('#tbl_Obligaciones_ac_id').val(obligacion);
    if ($('#bag_c_' + obligacion).hasClass("push-Off") && !$('#bag_c_' + obligacion).hasClass("push-NotOk")) {
        $('#bag_c_' + obligacion).addClass("push-NotOk");
        bit = 0;
    }
    else {
        $('#bag_c_' + obligacion).removeClass("push-NotOk");
        $('#bag_c_' + obligacion).addClass("push-Off");
        bit = 1;
    }

    var idObligacion = $('#tbl_Obligaciones_ac_id').val();
    var idproducto = $('#tbl_ProdServ_ac_id').val();
    var idplan = $('#tbl_PlanEntrega_ac_id').val();


    $.post("/Request/PlanEntrega/Ejecucion/Productos/" + idObligacion + "/" + idproducto + "/" + idplan + "/" + bit, function (data, status) {

    });

}


function noCumplio(tbl_prodServ_ac_id, id) {

    var planEntrega = $('#tbl_PlanEntregaDetalle_ac_id').val();


    var idu = $('#txt_ubicaciones_pe').val();

    var route = "/Request/Ejecucion/PE/Ejecucion/Get/Productos/";
    $.get(route + $('#txt_ubicaciones_pe').val() + '/' + tbl_prodServ_ac_id, function (data, status) {

        var htmlbody = '';
        for (var i = 0; i <= data.length - 1; i++) {
            var body_int = "<li id='oblicont_" + data[i].id + "' onmouseover=\"GreenContainer_obli('" + data[i].id + "')\" onmouseout=\"NormalContainer_obli('" + data[i].id + "')\" class='ctmain list-group-item d-flex justify-content-between align-items-center'>"
                + data[i].Obligacion +

                "<span id='bag_c_" + data[i].id + "' onclick='execute(" + data[i].id + ")' class='push-Off badge badge-primary badge-pill'>No Cumplió</span>" +
                "</li>";
            htmlbody = htmlbody + body_int;
        }
        $('.listobligacion').html(htmlbody);

    });


    $.get("/Request/PlanEntrega/Ejecucion/Productos/Get/Estatus/PE/" + id, function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {
            $('#bag_c_' + data[i].tbl_Obligaciones_ac_id).addClass("push-NotOk");
            console.log(data[i].tbl_Obligaciones_ac_id);

        }
    });




}

function NormalContainer_obli(id) {
    $('#oblicont_' + id).removeClass('list-group-item-success');

}
function GreenContainer_obli(id) {
    $('#oblicont_' + id).addClass('list-group-item-success');
}






function AddHeader() {
    var route = "/Request/Ejecucion/PE/Header/Add/";
    $.post(route + $('#txt_ubicaciones_pe').val(), function (data, status) {
        if ((data != null) && (data != '')) {
            $('#txt_header').val(data);
            $('#btnMain').remove();
        }
    });
}




function AddDetail() {
    var route = "/Request/Ejecucion/PE/Header/Add/";
    $.post(route + $('#txt_ubicaciones_pe').val(), function (data, status) {
        if ((data != null) && (data != '')) {
            $('#txt_header').val(data);
            $('#btnMain').remove();
        }
    });
}



function eval1(tbl_PlanMonitoreo_ac_id, tbl_ubicaciones_ac_id) {
    $('#ModalEvalMonitoreo').modal('show');
    var route = '/Request/PlanMonitoreo/Get/List/Ubicaciones/' + tbl_PlanMonitoreo_ac_id + '/' + tbl_ubicaciones_ac_id;
    $.get(route, function (data, status) {
        var body = '';
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<li class='list-group-item' style='cursor:pointer'>" + data[i].Elemento + "</li>";

        }

        $('.monitoreo_panel').html(body);

    });

}


function evalMonitoreo(tbl_PlanMonitoreo_ac_id, tbl_ubicaciones_ac_id) {
    $('.Monitoreo_Obligaciones').html('');
    $('#ModalEvalMonitoreo').modal('show');
    var route = $("#EndPointAC").val() + 'Operaciones/PM/Get/Productos/Plan/ubicacion/' + tbl_PlanMonitoreo_ac_id + '/' + tbl_ubicaciones_ac_id;
    $.get(route, function (data, status) {
        var body = '';
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<li id='listBoos_" + data[i].tbl_producto_servicio_id + "' onclick=\"GetObligaciones('" + data[i].tbl_producto_servicio_id + "','" + tbl_ubicaciones_ac_id + "','" + tbl_PlanMonitoreo_ac_id + "')\" class='Z-List list-group-item' style='cursor:pointer'>" + data[i].elemento + "</li>";

        }

        $('.monitoreo_panel').html(body);

    });

}


function ConfirmPMfnc(tbl_PlanMonitoreo_ac_id, tbl_ubicaciones_ac_id) {
    $('#ModalEvalConfirmPM').modal('show');
    var estatus = false;
    $.get($("#EndPointAC").val() +"Operaciones/PM/Get/Obligaciones/Incumplidas/" + tbl_PlanMonitoreo_ac_id, function (data, status) {
        var body_mdl = '¿Está seguro que desea confirmar el Plan de Monitoreo? <br> <ul>';
        if (data.length > 0) {
            estatus = false;
            body_mdl = "<p style='color:red'>Existen Obligaciones incumplidas.</p>" + body_mdl;
            for (var i = 0; i <= data.length - 1; i++) {
                body_mdl = body_mdl + '<li>' + data[i].obligacion + '</li>';
            }
            body_mdl = body_mdl + ' </ul>';
            $('.contenidoBody').html(body_mdl);
        }
        else {

            $('.contenidoBody').html(body_mdl);
            estatus = true;
        }

    });
    $(".success-call").prop("onclick", null).off("click");
    $(".success-call").click(function () {
        var bit = 0;

        bit = estatus == false ? 0 : 1;
        $.post($("#EndPointAC").val() + "Plan/ConfirmarPM/" + tbl_PlanMonitoreo_ac_id , function (data, status) {
            $('#ModalEvalConfirmPM').modal('hide');
            //ValidasPlanesEntrega();
            GetPlanesMonitoreoFiles();

        });



    });
}


function GetObligaciones(tbl_producto_servicio_id, tbl_ubicaciones_ac_id, tbl_PlanMonitoreo_ac_id) {
    $('#TXT_PRODUCTOS').val(tbl_producto_servicio_id);
    $('#TXT_UBICACIONES').val(tbl_ubicaciones_ac_id);
    $('#TXT_IDPLANMONITOREO').val(tbl_PlanMonitoreo_ac_id);

    $("#btnGuardar").removeClass('hidden');

    //list-group-item list-group-item-success
    $('.Z-List').each(function () {
        $(this).removeClass("list-group-item-success");
    });
    $('#listBoos_' + tbl_producto_servicio_id).addClass("list-group-item-success");


    //$.get("/Request/PlanMonitoreo/Get/Obligaciones/" + tbl_PlanMonitoreo_ac_id + "/" + tbl_ubicaciones_ac_id + "/" + valor, function (data, status) {
    $.get($("#EndPointAC").val() + 'Operaciones/PM/Get/Obligaciones/Producto/Plan/ubicacion/' + tbl_PlanMonitoreo_ac_id + "/" + tbl_ubicaciones_ac_id + "/" + tbl_producto_servicio_id, function (data, status) {
        var body = '';
        
        for (var i = 0; i <= data.length - 1; i++) {

            //body = body + "<li class='list-group-item'><table style='width:100%;'><tr><td style='width:60%;text-align: center;'>" + data[i].obligacion + " </td><td  style='width:40%;text-align: center;'><a id='Oblg_" + data[i].tbl_link_obligacion_id + "' onclick='NoCumple_eval(\"" + data[i].tbl_link_obligacion_id + "\",\"" + data[i].tbl_plan_entrega_producto_id + "\")' class='Normal-eval' type='button' >No cumple</a> <a onclick='AdjuntarDoc(\"" + tbl_PlanMonitoreo_ac_id + "\",\"" + tbl_ubicaciones_ac_id + "\",\"" + data[i].tbl_obligacion_id + "\",\"" + data[i].tbl_link_obligacion_id + "\")' class='fa fa-arrow-circle-up' type='button' title='Adjuntar Archivo'></a> <a id='Descarga_" + data[i].tbl_link_obligacion_id + "' onclick='Dowload(\"" + data[i].token_obligacion + "\")' style='color: #4CAF50;' class='fa fa-arrow-circle-down' type='button' title='Descargar archivo'></a></td></tr></table></li>";
            body = body + "<li class='list-group-item'>" +
                "<table style='width:100%;'>" +
                "<tr>" +
                "<td style='width:60%;text-align: center;'>" + data[i].obligacion + " </td>" +
                "<td  style='width:40%;text-align: center;'>" +
                //"<a id='Oblg_" + data[i].tbl_link_obligacion_id + "' onclick='NoCumple_eval(\"" + data[i].tbl_link_obligacion_id + "\",\"" + data[i].tbl_plan_entrega_producto_id + "\")' class='Normal-eval' type='button' >No cumple</a> " +
                "<select name='select' id='Oblg|" + data[i].tbl_link_obligacion_id + "|" + data[i].tbl_plan_entrega_producto_id + "' class='push-Off badge badge-primary badge-pill' style='margin-right: 30px;'>" +
                "<option value = '1'>Si Cumplió</option>" +
                "<option value = '2' selected>No cumplió</option>" +
                "</select>" +
                "<a onclick='AdjuntarDoc(\"" + tbl_PlanMonitoreo_ac_id + "\",\"" + tbl_ubicaciones_ac_id + "\",\"" + data[i].tbl_obligacion_id + "\",\"" + data[i].tbl_link_obligacion_id + "\")' class='fa fa-arrow-circle-up' type='button' title='Adjuntar Archivo'></a> " +
                "<a id='Descarga_" + data[i].tbl_link_obligacion_id + "' onclick='Dowload(\"" + data[i].token_obligacion + "\")' style='color: #4CAF50;' class='fa fa-arrow-circle-down' type='button' title='Descargar archivo'></a>" +
                "</td>" +
                "</tr>" +
                "</table>" +
                "</li>";

        }

        $('.Monitoreo_Obligaciones').html(body);
        $.get($("#EndPointAC").val() + 'Operaciones/PM/Get/Obligaciones/Nocumple/' + tbl_PlanMonitoreo_ac_id + "/"+ tbl_producto_servicio_id, function (data, status) {
            var _num_selects = document.getElementsByName('select').length;
                for (var j = 0; j <= data.length - 1; j++) {
                    for (var s = 0; s <= _num_selects - 1; s++) {
                        if (document.getElementsByName('select')[s].id === ("Oblg|" + data[j].tbl_link_obligacion_id + "|" + data[j].tbl_plan_entrega_producto_id) ) {
                            document.getElementById("Oblg|" + data[j].tbl_link_obligacion_id + "|" + data[j].tbl_plan_entrega_producto_id).value = "1";
                        }
                        
                    }
                }
            });
    });

}

function execute_btn_save() {


    var select_numbers = document.getElementsByName('select').length;
    var _link_obligacion;
    var _plan_entrega_producto_id;
    var _option;
    var _conact_link_option;

    for (var i = 0; i <= select_numbers - 1; i++) {
        _link_obligacion = (document.getElementsByName('select')[i].id).split('|')[1];
        _plan_entrega_producto_id = (document.getElementsByName('select')[i].id).split('|')[2];
        _option = document.getElementsByName('select')[i].value;

    //$.post("/Request/PlanMonitoreo/Add/Detail/" + tbl_Obligaciones_ac_id + "/" + $('#TXT_PRODUCTOS').val() + "/" + $('#TXT_IDPLANMONITOREO').val() + "/0", function (data, status) {

        $.post($("#EndPointAC").val() + "Plan/PM/" + _option + "/" +  _link_obligacion + "/" + _plan_entrega_producto_id, function (data, status) {

            if ((data != null) && (data != '')) {
                console.log(data);
            }

        });

        function Confirmacion() {
            return true;
        }
        var AccionSi = eval(Confirmacion);
        //LaunchLoader(true);
        SuccessSAAction("Operación exitosa", "Se actualizó correctamente", AccionSi);

    }


}

function Dowload(idObl) {

    if (idObl == null || idObl == "null") {
        ErrorSA("Error", "Esta obligacion no contiene ningun archivo adjunto");
    } else {
        getURL(idObl)
        modalVisualizacion();
    }
}

function AdjuntarDoc(plan_monitoreo,ubicacion,obligacion, link_obligacion) {
    $('#TXT_OBLIGACIONES').val(obligacion);
    $('#TXT_UBICACIONES').val(ubicacion);
    $('#TXT_IDPLANMONITOREO').val(plan_monitoreo);
    $('#id_tbl_link_obligacion').val(link_obligacion);
    $('#CargaDoc').modal({ backdrop: 'static', keyboard: false });
    $('#CargaDoc').modal('show');
}

function cerrarModalCargadeDoc() {
    $('#CargaDoc').modal('hide');
};


//Guarda el archivo de la obligación 
$('.GuardarDoc').click(function () {

    var obj_Doc_PM_Obl = SendDocPM;
    var countFiles = $("#DocObli").prop('files').length;

    obj_Doc_PM_Obl.tbl_plan_moniotoreo_id_ = $('#TXT_IDPLANMONITOREO').val();
    obj_Doc_PM_Obl.tbl_Ubicacion_id_ = $('#TXT_UBICACIONES').val();
    obj_Doc_PM_Obl.tbl_obligacion_id_ = $('#TXT_OBLIGACIONES').val();

    if (countFiles == 0) {
        ErrorSA('Error', "No se ha cargado ningún documento. ");
        return;
    } else if (countFiles > 0) {

        var form_data = new FormData();
        var file_data = $('#DocObli').prop('files')[0];
        form_data.append('file', file_data);

        $.ajax({
            url: $("#EndPointFileAC").val() + 'Upload/',
            dataType: 'text',
            cache: false,
            contentType: false,
            processData: false,
            data: form_data,
            type: 'POST',
            async: false,
            success: function (data) {
                var token = data.replace(/[ '"]+/g, '');
                obj_Doc_PM_Obl.token_ = token;
                console.log(token);
            },
            error: function (data) {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse);
            }
        });
    }
    console.log(JSON.stringify(obj_Doc_PM_Obl));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj_Doc_PM_Obl),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function Confirmacion() {
                    return cerrarModalCargadeDoc();
                }
                var AccionSi = eval(Confirmacion);
                SuccessSAAction("Operación exitosa", "El archivo se guardo correctamente", AccionSi);
                evalMonitoreo($('#TXT_IDPLANMONITOREO').val(), $('#TXT_UBICACIONES').val());
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAC").val() + 'Operaciones/PM/add/Doc/PM'

    });
})

function NoCumple_eval(tbl_link_obligacion_id, tbl_plan_entrega_producto_id) {

    $('#TXT_OBLIGACIONES').val(tbl_link_obligacion_id);
    $.post($("#EndPointAC").val() + 'Plan/PM/' + tbl_link_obligacion_id + "/" + tbl_plan_entrega_producto_id, function (data, status) {
        if ($('#Oblg_' + tbl_link_obligacion_id).hasClass("Normal-eval")) {
            $('#Oblg_' + tbl_link_obligacion_id).removeClass('Normal-eval');
            $('#Oblg_' + tbl_link_obligacion_id).addClass('NoCumplio-eval');
        }
        else {
            $('#Oblg_' + tbl_link_obligacion_id).removeClass('NoCumplio-eval');
            $('#Oblg_' + tbl_link_obligacion_id).addClass('Normal-eval');
        }
    });
}

function btn_ok() {
    //success-call

}

var SendDocPM = {
    id_: "00000000-0000-0000-0000-000000000000",
    tbl_plan_moniotoreo_id_: "00000000-0000-0000-0000-000000000000",
    tbl_Ubicacion_id_: "00000000-0000-0000-0000-000000000000",
    tbl_obligacion_id_: "00000000-0000-0000-0000-000000000000",
    token_: ""
};


