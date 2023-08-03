function Validar_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    var rfc = $('#txt_RFC_upd').val();
    if ($('#txt_razon_social_upd').val() == '') {
        Response.Texto = 'Debe ingresar una razón social.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_RFC_upd').val() == '') {
        Response.Texto = 'Debe ingresar un RFC.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_RFC_upd').val().length < 12) {
        Response.Texto = 'Debe ingresar un RFC valido mínimo 12 caracteres.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_representante_upd').val() == '') {
        Response.Texto = 'Debe ingresar el nombre del representante legal.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_apellido_paterno_r_upd').val() == '') {
        Response.Texto = 'Debe ingresar el apellido paterno del representante legal.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_ejecutivo_cuenta_upd').val() == '') {
        Response.Texto = 'Debe ingresar el nombre del ejecutivo de cuentas.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_apellido_paterno_eje_upd').val() == '') {
        Response.Texto = 'Debe ingresar el apellido paterno del ejecutivo de cuentas.';
        Response.Bit = true;
        return Response;
    }
    //if ($('#txt_apellido_materno_eje_upd').val() == '') {
    //    Response.Texto = 'Debe ingresar el apellido materno del ejecutivo de cuentas.';
    //    Response.Bit = true;
    //    return Response;
    //}
    if ($('#txt_Domicilio_fiscal_upd').val() == '') {
        Response.Texto = 'Debe ingresar el domicilio fiscal.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_telefono_upd').val() == '') {
        Response.Texto = 'Debe ingresar un número de teléfono.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_correo_electronico_upd').val() == '') {
        Response.Texto = 'Debe ingresar un correo electrónico.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_tipo_interlocutor_upd').val() == '0') {
        Response.Texto = 'Debe seleccionar un tipo de interlocutor.';
        Response.Bit = true;
        return Response;
    }
    if (ValidarDependenciaEditar() == false) {
        Response.Texto = 'Debe seleccionar al menos 1 dependencia.';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Update_Proveedor() {
    var Validacion = Validar_upd();
    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    tbl_proveedor.p_id = $("#id_proveedor").val();
    tbl_proveedor.p_razon_social = $("#txt_razon_social_upd").val();
    tbl_proveedor.p_rfc = $("#txt_RFC_upd").val();
    tbl_proveedor.p_domicilio_fiscal = $("#txt_Domicilio_fiscal_upd").val();
    tbl_proveedor.p_rep_legal_nombre = $("#txt_representante_upd").val();
    tbl_proveedor.p_rep_legal_ap_paterno = $("#txt_apellido_paterno_r_upd").val();
    tbl_proveedor.p_rep_legal_ap_materno = $("#txt_apellido_materno_r_upd").val();
    tbl_proveedor.p_eje_cuenta_nombre = $("#txt_ejecutivo_cuenta_upd").val();
    tbl_proveedor.p_eje_cuenta_ap_paterno = $("#txt_apellido_paterno_eje_upd").val();
    tbl_proveedor.p_eje_cuenta_ap_materno = $("#txt_apellido_materno_eje_upd").val();
    tbl_proveedor.p_telefono = $("#txt_telefono_upd").val();
    tbl_proveedor.p_correo_electronico = $("#txt_correo_electronico_upd").val();
    tbl_proveedor.p_tipo_interlocutor = $("#txt_tipo_interlocutor_upd").val();

    var arregloDE = ObtenerDependenciasSeleccionadasEditar();
    tbl_proveedor.p_tbl_dependencia_id = arregloDE[0].tbl_dependencia_id;
    if (arregloDE.length > 1) {
        arregloDE.splice(0, 1);
        tbl_proveedor.dependencias_adicionales = arregloDE;
    }

    console.log(JSON.stringify(tbl_proveedor));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_proveedor),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Proveedor_Editar();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_proveedores_contrato();
                $("#id_proveedor").val('');

            }
            else if (objresponse[0].cod == "warning") {
                Aviso_ErrorSA("", "El RFC ya existe, favor de verificar.");
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', 'Ocurrio un error.')
        },
        processData: false,
        type: 'PUT',
        //url: ($('#EndPointAC').val() + "SerAcuerdos/add")
        url: $("#EndPointAdmon").val() + "Proveedor/Update"

    })
}

$("#btn_actualizar_p").click(function () {
    Update_Proveedor();
})

function Cerrar_Modal_Proveedor_Editar() {
    $('#Modal_Editar_Proveedor').modal('hide');
    $('#txt_clear').val("");
}


$(function () {
    $('#txt_RFC_upd').focusout(function () {
        if ($('#txt_RFC_upd').val() !== rfc_editar) {
            $.get($("#EndPointAdmon").val() + 'Usuarios/Get/exist/' + 4 + '/' + $('#txt_RFC_upd').val(), function (data, status) {
                if (data[0].cod == 'warning') {
                    Aviso_ErrorSA('', 'El RFC que intesta actualizar ya existe');
                    $('#txt_RFC_upd').val(rfc_editar);
                }
            });
        }
    });
});