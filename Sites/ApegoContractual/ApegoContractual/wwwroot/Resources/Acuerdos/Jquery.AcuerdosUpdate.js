function UpdateAcuerdo() {

    var contrato = localStorage.getItem('Contrato');
    var Validacion = ValidarAcuerdoUpdate();
    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);

    }
    else {
        var OBJ_AcuerdoForm = AcuerdoClass;
        OBJ_AcuerdoForm.p_opt = 3;
        OBJ_AcuerdoForm.p_id = $('#idAcuerdo').val(); 
        OBJ_AcuerdoForm.p_tbl_contrato_id = contrato;
        OBJ_AcuerdoForm.p_tbl_contrato_servidor_resp_id = $('#ResponsableA_update').val();
        OBJ_AcuerdoForm.p_tbl_tipo_acuerdo_id = $('#TipoA_update').val();
        OBJ_AcuerdoForm.p_acuerdo = $('#txt_Acuerdo_update').val();
        OBJ_AcuerdoForm.p_fecha_registro = $('#txt_FechaRegistro_update').val();
        OBJ_AcuerdoForm.p_fecha_compromiso = $('#txt_FechaCompromiso_update').val();
        OBJ_AcuerdoForm.p_fecha_cierre = $('#txt_FechaCierre_update').val();
        OBJ_AcuerdoForm.p_estatus_acuerdo = $('#cmbEstatusAcuerdo_update').val();
        OBJ_AcuerdoForm.p_comentario = $('#txt_ComentarioAcuerdo_update').val();
        //OBJ_AcuerdoForm.Inclusion = date;
        OBJ_AcuerdoForm.p_estatus = 1;

        //var form_data = new FormData();
        //form_data.append('AcuerdoForm', JSON.stringify(OBJ_AcuerdoForm));

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_AcuerdoForm),
            type: 'put',

            success: function (data) {
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", "El registro se actualizó correctamente");
                    getAcuerdos();
                    $('#Modal_EditarAcuerdo').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            //processData: false,
            //type: 'POST',
            url: ($('#EndPointAC').val() + "SerAcuerdos/update")

        })
    }
}


function ValidarAcuerdoUpdate() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_Acuerdo_update').val() == '') {
        Response.Texto = 'Debe agregar un Acuerdo';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Acuerdo_update').val(), 'Acuerdo') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Acuerdo"';
        Response.Bit = true;
        return Response;
    }

    if ($('#txt_ComentarioAcuerdo_update').val() == '') {
        Response.Texto = 'Debe agregar un Comentario';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_ComentarioAcuerdo_update').val(), 'Comentario') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Comentario"';
        Response.Bit = true;
        return Response;
    }

    if ($('#drop_Responsables_update').val() == '') {
        Response.Texto = 'Debe seleccionar un responsable';
        Response.Bit = true;
        return Response;
    }
    if ($('#drop_TiposAcuerdo_update').val() == '') {
        Response.Texto = 'Debe seleccionar un tipo de acuerdo';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmbEstatusAcuerdo_update').val() == '') {
        Response.Texto = 'Debe seleccionar un estatus';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaRegistro_update').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de registro';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaCompromiso_update').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de compromiso';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_FechaCierre_update').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de cierre';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

