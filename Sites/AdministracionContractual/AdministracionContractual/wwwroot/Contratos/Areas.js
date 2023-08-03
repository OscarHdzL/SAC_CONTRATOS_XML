
var baseline = [];
var PresUsado;

function INIT_Areas() {
    baseline = [];
    setTimeout(function () {
        //catalogPresupuesto();
        getDependenciasAsignadasUsuario();
    }, 1000);
    //setInterval('convertbaseline()', 500);
}


function catalogPresupuesto() {
    var uri = $('#EndPointAdmon').val() + 'Contratos/Set/Presupuesto/' + $('#dependenciaContrato').val();
    $.get(uri, function (data, status) {
        $('#catalogo_gral_').val(JSON.stringify(data));
        PresUsado = data;
        StyleListbox();
    }, 'json');
}

function StyleListbox() {

    var obj = JSON.parse($('#catalogo_gral_').val());
    //identifier_areas

    var element = "";
    var body_cg = '';
    var recorrido = [];
    var recorrido_cg = [];
    for (var i = 0; i <= obj.length - 1; i++) {
        if (typeof recorrido.find(element => element == obj[i].area) === "undefined") {
            console.log(typeof recorrido.find(element => element == obj[i].area) === "undefined");
            element = element + "<a identifier='" + obj[i].tbl_area_id + "' class='list-area list-group-item' id='list-home-list'>" + obj[i].area + "</a> ";
            recorrido.push(obj[i].area);
        }
    }
    $('#identifier_areas').html(element);

    $(".list-area").each(function () {
        $(this).click(function () {
            obj = JSON.parse($('#catalogo_gral_').val());
            $('#identifier_CAP_GASTO').html('');
            var identifiere_ = $(this).attr('identifier');
            for (var i = 0; i <= obj.length - 1; i++) {
                $("[identifier='" + obj[i].tbl_area_id + "']").removeClass('active');
            }
            $(this).addClass('active');
            $('#areaSeleccionada').val(identifiere_);
            body_cg = '';
            for (var i = 0; i <= obj.length - 1; i++) {
                if (obj[i].tbl_area_id == $(this).attr('identifier')) {
                    body_cg = body_cg + "<a onclick = \"callaction_cg(" + obj[i].monto_disponible + ",'" + obj[i].codigo_capitulo + "','" + obj[i].capitulo + "')\" identifier_cg='" + obj[i].tbl_capitulo_gasto_id + "' class='list-cg list-group-item' id='list-home-list'>" + obj[i].codigo_capitulo + " | " + obj[i].capitulo + " </a>";
                }
            }
            $('#identifier_CAP_GASTO').html('');
            $('#identifier_CAP_GASTO').html(body_cg);

            ClickStartCapGas___();


        });
    });

}

function callaction_cg(valoren, cg, p_capitulo_des) {

    $(".list-cg").each(function () {

        console.log('callaction_cg');
        //console.log($('#catalogo_gral_').val());

        obj = JSON.parse($('#catalogo_gral_').val());
        $(this).click(function () {
            var identifiere_ = $(this).attr('identifier_cg');
            for (var i = 0; i <= obj.length - 1; i++) {
                $("[identifier_cg='" + obj[i].tbl_capitulo_gasto_id + "']").removeClass('active');
            }
            $(this).addClass('active');
            $('#panel-list-press').show();
            $('#codSeleccionado').val(identifiere_);
            $('#p_monto').html("$" + valoren);
            $('#p_capitulo').html(cg);
            $('#p_capitulo_des').html(p_capitulo_des);
        });
    });

}

function fillBag() {


    if ($('#codSeleccionado').val() == '') { return false; }
    if ($('#areaSeleccionada').val() == '') { return false; }


    var Monto_Porejercer__ = $('#monto_por_ejercer').val();
    var des__ = $('#txt_numero_descripcion').val();
    var numero__ = $('#txt_numero_ejercer').val();
    var disponible__ = $('#p_monto').html().replace('$', '');

    if (
        Monto_Porejercer__ == '' ||
        des__ == '' ||
        numero__ == ''
    ) {

        Swal.fire({
            type: 'error',
            title: 'Datos incompletos',
            text: 'Todos los datos son requeridos'
        });
        return;
    }
    if (parseInt(disponible__) < parseInt(Monto_Porejercer__)) {
        Swal.fire({
            type: 'error',
            title: 'Datos incompletos',
            text: 'El monto solicitado es mayor al disponible.'
        });
        return;
    }


    var body_bag = '';

    var obj = {
        p_capitulo: $('#p_capitulo').html(),
        p_capitulo_des: $('#p_capitulo_des').html(),
        monto_por_ejercer: $('#monto_por_ejercer').val(),
        des_personal: $('#txt_numero_ejercer').val(),
        des_numero: $('#txt_numero_descripcion').val(),
        idcapgast: $('#codSeleccionado').val(),
        areaSeleccionada: $('#areaSeleccionada').val(),
        dependencia: $('#HDidDependencia').val()
    };

    var val_bool = true;
    for (var i = 0; i <= baseline.length - 1; i++) {
        if (baseline[i].idcapgast == obj.idcapgast && baseline[i].areaSeleccionada == obj.areaSeleccionada && baseline[i].des_numero == obj.des_personal) {
            Swal.fire({
                type: 'error',
                title: 'Datos Erroneos',
                text: 'El Area, el numero de partido y capitulo de gasto no pueden repetirse.'
            });
            val_bool = false;
        }
    }
    if (!val_bool) { return false; }

    baseline.push(obj);

    for (var i = 0; i <= baseline.length - 1; i++) {
        var inicio = "<a ident='5' id='" + baseline[i].des_numero + "_" + baseline[i].areaSeleccionada + "_" + baseline[i].idcapgast + "' identifier_cg='1' class='list-monto list-group-item' style='padding: 0;background-color: #f5f5f5;' id=''><div class='row'>";
        var a1 = "<div class='col-lg-2 text-center' style='padding: 0;'><label style='font-size: 10px;'>" + baseline[i].p_capitulo + "</label></div>";
        var a2 = "<div class='col-lg-5 text-center' style='padding: 0;'><label style='font-size: 10px;'>" + baseline[i].p_capitulo_des + "</label></div>";
        var a3 = "<div class='col-lg-3 text-center' style='padding: 0;'><label style='font-size: 10px;'>$ " + baseline[i].monto_por_ejercer + "</label></div>";
        var a4 = "<div class='col-lg-2 text-center' style='padding: 0;' onclick=\"remover_('" + baseline[i].des_numero + "','" + baseline[i].areaSeleccionada + "','" + baseline[i].idcapgast + "','" + baseline[i].monto_por_ejercer + "'); \" > <i class='fa fa-fw fa-remove'></i> </div>";
        var fin = "</div></a>";
        body_bag = body_bag + inicio + a1 + a2 + a3 + a4 + fin;
    }
    $('#input_bags').html(body_bag);

    var conteo_var = 0;
    for (var i = 0; i <= baseline.length - 1; i++) {
        conteo_var = conteo_var + parseInt(baseline[i].monto_por_ejercer);
    }

    console.log('fillBag');
    //console.log($('#catalogo_gral_').val());

    PresUsado_ = JSON.parse($('#catalogo_gral_').val());

    for (var i = 0; i <= PresUsado_.length - 1; i++) {
        if (PresUsado_[i].tbl_area_id == $('#areaSeleccionada').val() && PresUsado_[i].tbl_capitulo_gasto_id == $('#codSeleccionado').val()) {
            PresUsado_[i].monto_disponible = parseInt(PresUsado_[i].monto_disponible) - parseInt($('#monto_por_ejercer').val());
        }

    }
    console.log('fillBag');
    console.log(JSON.stringify(PresUsado_));

    $('#catalogo_gral_').val(JSON.stringify(PresUsado_));

    $('#mtae').html(conteo_var);




    $('#monto_por_ejercer').val('');
    $('#txt_numero_ejercer').val('');
    $('#txt_numero_descripcion').val('');
    $('#codSeleccionado').val('');
    $('#areaSeleccionada').val('');

    $(".list-cg").each(function () {
        $(this).removeClass('active');
    });
    $(".list-area").each(function () {
        $(this).removeClass('active');
    });
    $('#panel-list-press').hide();


}

function validate_Areas__() {
    var banderaErrores = 0;
    if (parseInt($('#mtae').html()) > 0) {
        //$('#a3').val(1);
        //Swal.fire({
        //    type: 'success',
        //    title: 'Datos Completos',
        //    text: 'La suficiencia presupuestal es correcta'
        //});
    }
    else {
        Swal.fire({
            type: 'error',
            title: 'Datos incompletos',
            text: 'debe agregar partidas'
        });
        banderaErrores = banderaErrores + 1;
    }
    //agregar validación de instancia y de area seleccionada
    if ($("#dependencias_usuario").val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Datos incompletos',
            text: 'debe seleccionar una dependencia'
        });
        banderaErrores = banderaErrores + 1;
    }
    if ($("#areaAsignadaContrato").val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Datos incompletos',
            text: 'debe seleccionar una estructura para el contrato'
        });
        banderaErrores = banderaErrores + 1;
    }
    if ($("#nivelAreaAsignadaContrato").val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Datos incompletos',
            text: 'debe seleccionar una estructura para el contrato'
        });
        banderaErrores = banderaErrores + 1;
    }

    if (banderaErrores == 0) {
        $('#a3').val(1);
        Swal.fire({
            type: 'success',
            title: 'Datos Completos',
            text: 'La suficiencia presupuestal es correcta'
        });
        $('.div_areas').hide();
        $('.div_firmantes').show();
        INIT_Firmantes();
    }


}

function calldetails(obj_) {
    $('#lbl_cap_gasto_modal').html(obj_.p_capitulo);
    $('#lbl_gasto_modal').html(obj_.p_capitulo_des);
    $('#lbl_numero_modal').html(obj_.des_numero);
    $('#lbl_descripcion_modal').html(obj_.des_personal);
    $('#lbl_monto_ejercer_modal').html(obj_.monto_por_ejercer);
    $('#modaldetails').modal('show');
}

function ClickStartCapGas___() {
    var body_cg = '';
    console.log('ClickStartCapGas___');
    /*console.log($('#catalogo_gral_').val());*/
    var obj = JSON.parse($('#catalogo_gral_').val());


    $(".list-area").each(function () {
        $(this).click(function () {


            for (var i = 0; i <= obj.length - 1; i++) {
                if (obj[i].tbl_area_id == $(this).attr('identifier')) {
                    body_cg = body_cg + "<a onclick = \"callaction_cg(" + obj[i].monto_disponible + ",'" + obj[i].codigo_capitulo + "','" + obj[i].capitulo + "')\" identifier_cg='" + obj[i].tbl_capitulo_gasto_id + "' class='list-cg list-group-item' id='list-home-list'>" + obj[i].codigo_capitulo + " | " + obj[i].capitulo + " </a>";
                }
            }
            $('#identifier_CAP_GASTO').html('');
            $('#identifier_CAP_GASTO').html(body_cg);
        });
    });

}

function remover_(num, area__, cod__, ejerc) {
    console.log('remover_');
    console.log($('#catalogo_gral_').val());

    var obj = JSON.parse($('#catalogo_gral_').val());
    for (var i = 0; i <= obj.length - 1; i++) {
        if (obj[i].tbl_area_id == area__ && obj[i].tbl_capitulo_gasto_id == cod__) {
            obj[i].monto_disponible = parseInt(obj[i].monto_disponible) + parseInt(ejerc);
        }

    }
    console.log('remover_ ');
    console.log(JSON.stringify(obj));
    $('#catalogo_gral_').val(JSON.stringify(obj));

    for (var i = 0; i <= baseline.length - 1; i++) {
        if (baseline[i].idcapgast == cod__ && baseline[i].areaSeleccionada == area__) {
            baseline.splice(i, 1);

            $('#' + num + '_' + area__ + '_' + cod__).remove();
            console.log('#' + num + '_' + area__ + '_' + cod__);

        }
    }




}

function JustUpdate() {
    baseline = JSON.parse($('#JSONupdate').val());
    var body_bag = '';
    for (var i = 0; i <= baseline.length - 1; i++) {
        var inicio = "<a ident='5' id='" + baseline[i].des_numero + "_" + baseline[i].areaSeleccionada + "_" + baseline[i].idcapgast + "' identifier_cg='1' class='list-monto list-group-item' style='padding: 0;background-color: #f5f5f5;' id=''><div class='row'>";
        var a1 = "<div class='col-lg-2 text-center' style='padding: 0;'><label style='font-size: 10px;'>" + baseline[i].p_capitulo + "</label></div>";
        var a2 = "<div class='col-lg-5 text-center' style='padding: 0;'><label style='font-size: 10px;'>" + baseline[i].p_capitulo_des + "</label></div>";
        var a3 = "<div class='col-lg-3 text-center' style='padding: 0;'><label style='font-size: 10px;'>$ " + baseline[i].monto_por_ejercer + "</label></div>";
        var a4 = "<div class='col-lg-2 text-center' style='padding: 0;' onclick=\"remover_('" + baseline[i].des_numero + "','" + baseline[i].areaSeleccionada + "','" + baseline[i].idcapgast + "','" + baseline[i].monto_por_ejercer + "'); \" > <i class='fa fa-fw fa-remove'></i> </div>";
        var fin = "</div></a>";
        body_bag = body_bag + inicio + a1 + a2 + a3 + a4 + fin;
    }
    $('#input_bags').html(body_bag);
    var conteo_var = 0;
    for (var i = 0; i <= baseline.length - 1; i++) {
        conteo_var = conteo_var + parseInt(baseline[i].monto_por_ejercer);
    }
    console.log('JustUpdate ');
    //console.log($('#catalogo_gral_').val());

    PresUsado_ = JSON.parse($('#catalogo_gral_').val());

    for (var i = 0; i <= PresUsado_.length - 1; i++) {
        if (PresUsado_[i].tbl_area_id == $('#areaSeleccionada').val() && PresUsado_[i].tbl_capitulo_gasto_id == $('#codSeleccionado').val()) {
            PresUsado_[i].monto_disponible = parseInt(PresUsado_[i].monto_disponible) - parseInt($('#monto_por_ejercer').val());
        }
    }
    console.log('JustUpdate ');
    //console.log(JSON.stringify(PresUsado_));

    $('#catalogo_gral_').val(JSON.stringify(PresUsado_));
    $('#mtae').html(conteo_var);
}


