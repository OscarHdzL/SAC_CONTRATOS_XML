function Get_arbol(dependencia) {
    $.get("/Contratos/EstructuraOrganica?idDependencia=" + dependencia, function (data, status) {
        $("#_arbol").html(data);
        LaunchLoader(false);
        if ($("#dependenciaContrato").val() != null) {
            var areaA = $("#areaAsignadaContrato").val();
            //console.log(areaA);
            //console.log('#est_' + areaA);
            var nConcat = '#est_' + areaA;
            var nombreAA = $(nConcat).text();
            /*console.log(nombreAA);*/
            $("#nombre_arbol_seleccionado").text(nombreAA);
        }
    });
}

function getDependenciasAsignadasUsuario() {
    $.get($('#EndPointAdmon').val() + "Usuarios/Get/DependenciasAsignadas/Usuario/" + $("#HDidUsuario").val(), function (data, status) {
        //console.log('obteniendo dependencias del usuario');
        //console.log(data);
        for (var i = 0; i < data.length; i++) {
            var body = "<option selected value=''>Seleccione...</option>";
            for (var i = 0; i <= data.length - 1; i++) {
                body = body + "<option value='" + data[i].tbl_dependencia_id + "' >" + data[i].nombre_dependencia + "</option>";
            }
            $("#dependencias_usuario").html(body);
            $("#dependencias_usuario").val($("#dependenciaContrato").val());
            if ($("#dependenciaContrato").val() != null) {
                $("#dependencias_usuario").trigger("change");
                catalogPresupuesto();
            }
        }

    });
}

function getDependenciasAsignadasUsuarioEditar() {
    $.get($('#EndPointAdmon').val() + "Usuarios/Get/DependenciasAsignadas/Usuario/" + $("#HDidUsuario").val(), function (data, status) {
        console.log('obteniendo dependencias del usuario');
        console.log(data);
        for (var i = 0; i < data.length; i++) {
            var body = "<option selected value=''>Seleccione...</option>";
            for (var i = 0; i <= data.length - 1; i++) {
                body = body + "<option value='" + data[i].tbl_dependencia_id + "' >" + data[i].nombre_dependencia + "</option>";
            }
            $("#dependencias_usuario").html(body);
            $("#dependencias_usuario").val($("#dependenciaContrato").val());


        }

    });
}

$("#dependencias_usuario").change(function () {
    if ($(this).val() != '') {
        Get_arbol($(this).val());
        $("#dependenciaContrato").val($(this).val());

        //$("#nombre_arbol_seleccionado").text('');
        //$("#areaAsignadaContrato").val('');
        //$("#nivelAreaAsignadaContrato").val('');
        catalogPresupuesto();
        if ($("#areaAsignadaContratoBandera").val() == '1') {
            console.log('refrescado de bd');
            $("#areaAsignadaContratoBandera").val(0);
        }
        else if ($("#areaAsignadaContrato").val()!=''){
            console.log('refrescando manual');
            $("#nombre_arbol_seleccionado").text('');
            $("#areaAsignadaContrato").val('');
            $("#nivelAreaAsignadaContrato").val('');
            $("#areaAsignadaContratoBandera").val(0);
            Swal.fire({
                type: 'error',
                title: 'Cambio de la dependencia',
                text: 'Los responsables e interlocutores se obtendrán a partir de la nueva dependencia, si ya tenía asignados deberá asignarlos nuevamente'
            });
            $('#a4').val(0);
            $('#a5').val(0);
            $('.tcre_5').removeClass('primary');
            $('.tcre_5').addClass('secondary');
            $('.tcre_6').removeClass('primary');
            $('.tcre_6').addClass('secondary');
            refrescarProveedoresResponsables();
        }
        //cambiar los proveedores y los responsables
        //if ($("#contrato_identifier").val() != '' && $("#contrato_identifier").val() != undefined && $("#areaAsignadaContratoBandera").val() != '1') {
        //    console.log('refrescando proveedores y responsables de contrato existente');
        //    Swal.fire({
        //        type: 'error',
        //        title: 'Cambio de la dependencia',
        //        text: 'Los responsables e interlocutores se obtendrán a partir de la nueva dependencia, si ya tenía asignados deberá asignarlos nuevamente'
        //    });
        //    refrescarProveedoresResponsables();
        //}
    }

});

function refrescarProveedoresResponsables() {
    console.log('refrescarProveedoresResponsables');
    $('#responsables_hd').val('');
    INIT_Firmantes();

    INIT_Proveedores();
}

function asignar_estructura(IdItem, IdItem_d, ItemTexto, TipoItem) {
    //console.log(IdItem);
    //console.log(IdItem_d);
    //console.log(ItemTexto);
    //console.log(TipoItem);
    var etiqueta = '';
    if (TipoItem == 1) {
        console.log('dependencia');
    }
    if (TipoItem == 2) {
        console.log('area');
    }
    if (TipoItem == 3) {
        console.log('sub area');
    }
    if (TipoItem == 4) {
        console.log('area subordinada');
    }
    etiqueta = etiqueta + ' ' + IdItem_d;
    $("#nombre_arbol_seleccionado").text(ItemTexto);
    $("#areaAsignadaContrato").val(IdItem);
    $("#nivelAreaAsignadaContrato").val(TipoItem);
}