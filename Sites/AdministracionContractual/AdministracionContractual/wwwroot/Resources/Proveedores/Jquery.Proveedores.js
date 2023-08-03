
$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_lista_proveedores").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "20%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            { "width": "20%", "targets": 4 },
            { "width": "15%", "targets": 5 },
        ],
    });
    Get_lista_proveedores_contrato();

    var rfc_editar = null;

    Get_lista_tipo_interlocutor();
});

function Abrir_Modal_Proveedor() {
    $('#txt_clear').val("");
    $('#Modal_Proveedor').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Proveedor').modal('show');

    $('#btn_siguiente_i').prop('disabled', false);
    $('#txt_tipo_interlocutor_new').prop('disabled', false);
    $('#txt_tipo_interlocutor_new').val('0');
    $('#formProveedor').addClass('hidden');
    $('#btn_guardar_p').addClass('hidden');
    console.log('ocultando dependencias');
    $('#formDependenciasAdd').addClass('hidden');
}

function Cerrar_Modal_Proveedor() {
    $('#Modal_Proveedor').modal('hide');
    $('#txt_clear').val("");
}

$("#Add_Proveedor").click(function () {
    Abrir_Modal_Proveedor();
});

function Get_lista_proveedores_contrato() {
    $.get($("#EndPointAdmon").val() + "Proveedor/Get/lista/" + $("#HDidInstancia").val(), function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            var contratos = "";
            fila.push(i + 1);
            fila.push(data[i]._tbl_proveedor.razon_social);
            fila.push(data[i]._tbl_proveedor.rep_legal_nombre + " " +
                      data[i]._tbl_proveedor.rep_legal_ap_paterno + " " +
                      data[i]._tbl_proveedor.rep_legal_ap_materno);
            fila.push('<a href="mailto:' + data[i]._tbl_proveedor.correo_electronico+'">'+ data[i]._tbl_proveedor.correo_electronico +'<\a>');

            if (data[i]._tbl_contrato != null) {
                for (var j = 0; j <= data[i]._tbl_contrato.length - 1; j++) {
                    contratos = contratos + '<p>' + '<a onclick="Mostrar_Contrato(\'' + data[i]._tbl_contrato[j].id + '\')" class="btn btn-link" title="Ir al contrato: ' + data[i]._tbl_contrato[j].numero + '"><i class="fa fa-folder-open"></i>' + data[i]._tbl_contrato[j].numero + '</a>' + '</p>';
                }
            }
            else {
                contratos = '';
            }
            fila.push(contratos);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Proveedor('" + data[i]._tbl_proveedor.id + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-info' title='Detalles' onclick=\"DetalleProveedor('" + data[i]._tbl_proveedor.id + "');\"><i class='fa fa-send'></i></button> "+
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteProveedor('" + data[i]._tbl_proveedor.id + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_lista_proveedores').DataTable();

        table.destroy();

        $('#tbl_lista_proveedores').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Razón social" },
                { title: "Representante legal" },
                { title: "Correo" },
                { title: "Contratos" },
                { title: "Acciones" },
            ],
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });

}

function Get_lista_proveedores_contrato_filtro(tbl_tipo_interlocutor_id) {
    if (tbl_tipo_interlocutor_id != 0) {
        $.get($("#EndPointAdmon").val() + "Proveedor/Get/lista/" + $("#HDidInstancia").val(), function (data, status) {
            var listado = [];
            //console.log(data);
            var n = 1; 
            for (var i = 0; i <= data.length - 1; i++) {
                if (data[i]._tbl_proveedor.tbl_tipo_interlocutor_id === tbl_tipo_interlocutor_id) {
                    var fila = [];
                    var contratos = "";
                    fila.push(n);
                    n = n + 1;
                    fila.push(data[i]._tbl_proveedor.razon_social);
                    fila.push(data[i]._tbl_proveedor.rep_legal_nombre + " " +
                        data[i]._tbl_proveedor.rep_legal_ap_paterno + " " +
                        data[i]._tbl_proveedor.rep_legal_ap_materno);
                    fila.push('<a href="mailto:' + data[i]._tbl_proveedor.correo_electronico + '">' + data[i]._tbl_proveedor.correo_electronico + '<\a>');

                    if (data[i]._tbl_contrato != null) {
                        for (var j = 0; j <= data[i]._tbl_contrato.length - 1; j++) {
                            contratos = contratos + '<p>' + '<a onclick="Mostrar_Contrato(\'' + data[i]._tbl_contrato[j].id + '\')" class="btn btn-link" title="Ir al contrato: ' + data[i]._tbl_contrato[j].numero + '"><i class="fa fa-folder-open"></i>' + data[i]._tbl_contrato[j].numero + '</a>' + '</p>';
                        }
                    }
                    else {
                        contratos = '';
                    }
                    fila.push(contratos);
                    fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Proveedor('" + data[i]._tbl_proveedor.id + "');\"><i class='fa fa-edit'></i></button> " +
                        "<button class='btn btn-info' title='Detalles' onclick=\"DetalleProveedor('" + data[i]._tbl_proveedor.id + "');\"><i class='fa fa-send'></i></button> " +
                        "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteProveedor('" + data[i]._tbl_proveedor.id + "');\"><i class='fa fa-trash'></i></button> ");
                    listado.push(fila);
                }

            }

            var table = $('#tbl_lista_proveedores').DataTable();

            table.destroy();

            $('#tbl_lista_proveedores').DataTable({
                data: listado,
                columns: [
                    { title: "No." },
                    { title: "Razón social" },
                    { title: "Representante legal" },
                    { title: "Correo" },
                    { title: "Contratos" },
                    { title: "Acciones" },
                ],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
                },
                columnDefs: [
                    {
                        targets: -1,
                        className: 'dt-body-center'
                    }]
            });
            LaunchLoader(false);
        });
    } else {
                Get_lista_proveedores_contrato();
    }

   

}

function Get_lista_tipo_interlocutor() {
    $.get($("#EndPointAdmon").val() + "Proveedor/Get/lista_tipo_interlocutor/1", function (data, status) {
        var html = "<option value='0' selected>Seleccione un tipo de interlocutor</option>";

        var htmllistfiltro = "<option value='0' selected>Filtrar por tipo de interlocutor</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            html += "<option value = '" + data[i].id + "'>" + data[i].nombre + "</option>"

            htmllistfiltro += "<option value = '" + data[i].id + "'>" + data[i].nombre + "</option>"
        }
        $('#txt_tipo_interlocutor_new').html(html);
        $('#txt_tipo_interlocutor_det').html(html);
        $('#txt_tipo_interlocutor_upd').html(html);
        $('#txt_tipo_interlocutor_list').html(htmllistfiltro);
    });
}

function Validar() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_razon_social').val() == '') {
        Response.Texto = 'Debe ingresar una razón social.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_RFC').val() == '') {
        Response.Texto = 'Debe ingresar un RFC.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_RFC').val().length < 12) {
        Response.Texto = 'Debe ingresar un RFC valido, mínimo 12 caracteres.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_representante').val() == '') {
        Response.Texto = 'Debe ingresar el nombre del representante legal.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_apellido_paterno_r').val() == '') {
        Response.Texto = 'Debe ingresar el apellido paterno del representante legal.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_ejecutivo_cuenta').val() == '') {
        Response.Texto = 'Debe ingresar el nombre del ejecutivo de cuentas.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_apellido_paterno_eje').val() == '') {
        Response.Texto = 'Debe ingresar el apellido paterno del ejecutivo de cuentas.';
        Response.Bit = true;
        return Response;
    }
    //if ($('#txt_apellido_materno_eje').val() == '') {
    //    Response.Texto = 'Debe ingresar el apellido materno del ejecutivo de cuentas.';
    //    Response.Bit = true;
    //    return Response;
    //}
    if ($('#txt_Domicilio_fiscal').val() == '') {
        Response.Texto = 'Debe ingresar el domicilio fiscal.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_telefono').val() == '') {
        Response.Texto = 'Debe ingresar un número de teléfono.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_correo_electronico').val() == '') {
        Response.Texto = 'Debe ingresar un correo electrónico.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_tipo_interlocutor_new').val() == '0') {
        Response.Texto = 'Debe seleccionar un tipo de interlocutor.';
        Response.Bit = true;
        return Response;
    }
    if (ValidarDependenciaNuevo()==false) {
        Response.Texto = 'Debe seleccionar al menos 1 dependencia.';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

$("#btn_guardar_p").click(function () {
    Add_Proveedor();
})

function muestraForm() {
    var tbl_tipo_interlocutor_id = $("#txt_tipo_interlocutor_new").val();
    //no mamen con esto
    if (tbl_tipo_interlocutor_id == 'fd975710-caae-49e3-8c9e-7a2ddc0ac948') { /*Proveedor*/
        $('#formProveedor').removeClass('hidden');
        $('#btn_guardar_p').removeClass('hidden');
        $('#btn_siguiente_i').prop('disabled', true)
        $('#txt_tipo_interlocutor_new').prop('disabled', true)
        
    }
    else if (tbl_tipo_interlocutor_id == 'fd975710-caae-49e3-8c9e-7a2ddc0ac958') { /*Cliente*/
        $('#formProveedor').removeClass('hidden');
        $('#btn_guardar_p').removeClass('hidden');
        $('#btn_siguiente_i').prop('disabled', true);
        $('#txt_tipo_interlocutor_new').prop('disabled', true);
    } else { /* Cualquier otro? */
        $('#formProveedor').removeClass('hidden');
        $('#btn_guardar_p').removeClass('hidden');
        $('#btn_siguiente_i').prop('disabled', true);
        $('#txt_tipo_interlocutor_new').prop('disabled', true);
    }
}


function Add_Proveedor() {
    var Validacion = Validar();
    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    tbl_proveedor.p_razon_social = $("#txt_razon_social").val();
    tbl_proveedor.p_rfc = $("#txt_RFC").val();
    tbl_proveedor.p_domicilio_fiscal = $("#txt_Domicilio_fiscal").val();
    tbl_proveedor.p_rep_legal_nombre = $("#txt_representante").val();
    tbl_proveedor.p_rep_legal_ap_paterno = $("#txt_apellido_paterno_r").val();
    tbl_proveedor.p_rep_legal_ap_materno = $("#txt_apellido_materno_r").val();
    tbl_proveedor.p_eje_cuenta_nombre = $("#txt_ejecutivo_cuenta").val();
    tbl_proveedor.p_eje_cuenta_ap_paterno = $("#txt_apellido_paterno_eje").val();
    tbl_proveedor.p_eje_cuenta_ap_materno = $("#txt_apellido_materno_eje").val();
    tbl_proveedor.p_telefono = $("#txt_telefono").val();
    tbl_proveedor.p_correo_electronico = $("#txt_correo_electronico").val();
    tbl_proveedor.p_tipo_interlocutor = $("#txt_tipo_interlocutor_new").val();


    var arregloD = ObtenerDependenciasSeleccionadasNuevo();
    tbl_proveedor.p_tbl_dependencia_id = arregloD[0].tbl_dependencia_id;
    if (arregloD.length > 1) {
        arregloD.splice(0, 1);
        tbl_proveedor.dependencias_adicionales = arregloD
    }
    console.log(JSON.stringify(tbl_proveedor));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_proveedor),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Proveedor();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_proveedores_contrato();

            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAdmon").val() + "Proveedor/add"

    })
}

function DeleteProveedor(item) {
    function Confirmacion() {
        return eliminar_proveedor(item);
    }

    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_proveedor(item) {
    $.ajax({
        url: $("#EndPointAdmon").val() + "Proveedor/Delete/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_proveedores_contrato();
            }
            else {
                ErrorSA("", "Ocurrió un error al eliminar, intente nuevamente.");
            }

        },
        error: function (data) {
            ErrorSA("", "Ocurrió un error al eliminar, intente nuevamente.");
        }
    });
}

function Editar_Proveedor(item) {
    $('#Modal_Editar_Proveedor').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Editar_Proveedor').modal('show');
    $("#id_proveedor").val(item);

    GetDependenciasSecundariasEditar();

    rfc_editar = null;
    $.get($("#EndPointAdmon").val()+ "Proveedor/Get/" + item, function (data, status) {
        for (var x = 0; x <= data.length - 1; x++) {
            $("#txt_razon_social_upd").val(data[x].razon_social);
            $("#txt_RFC_upd").val(data[x].rfc);
            $("#txt_Domicilio_fiscal_upd").val(data[x].domicilio_fiscal);
            $("#txt_representante_upd").val(data[x].rep_legal_nombre);
            $("#txt_apellido_paterno_r_upd").val(data[x].rep_legal_ap_paterno);
            $("#txt_apellido_materno_r_upd").val(data[x].rep_legal_ap_materno);
            $("#txt_ejecutivo_cuenta_upd").val(data[x].eje_cuenta_nombre);
            $("#txt_apellido_paterno_eje_upd").val(data[x].eje_cuenta_ap_paterno);
            $("#txt_apellido_materno_eje_upd").val(data[x].eje_cuenta_ap_materno);
            $("#txt_telefono_upd").val(data[x].telefono);
            $("#txt_correo_electronico_upd").val(data[x].correo_electronico);
            $("#txt_tipo_interlocutor_upd").val(data[x].tbl_tipo_interlocutor_id);
            rfc_editar = data[x].rfc;

            if (data[x].tbl_tipo_interlocutor_id == null) {
                $("#txt_tipo_interlocutor_upd").val('0');
                $('#txt_tipo_interlocutor_upd').prop('disabled', false);
            } else {
                $('#txt_tipo_interlocutor_upd').prop('disabled', true);
            }
        }
    });




}

function DetalleProveedor(item) {
    $('#Modal_Detalle_Proveedor').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Detalle_Proveedor').modal('show');
    $.get($("#EndPointAdmon").val() + "Proveedor/Get/" + item, function (data, status) {
        for (var x = 0; x <= data.length - 1; x++) {
            $("#txt_razon_social_det").val(data[x].razon_social);
            $("#txt_RFC_det").val(data[x].rfc);
            $("#txt_Domicilio_fiscal_det").val(data[x].domicilio_fiscal);
            $("#txt_representante_det").val(data[x].rep_legal_nombre);
            $("#txt_apellido_paterno_r_det").val(data[x].rep_legal_ap_paterno);
            $("#txt_apellido_materno_r_det").val(data[x].rep_legal_ap_materno);
            $("#txt_ejecutivo_cuenta_det").val(data[x].eje_cuenta_nombre);
            $("#txt_apellido_paterno_eje_det").val(data[x].eje_cuenta_ap_paterno);
            $("#txt_apellido_materno_eje_det").val(data[x].eje_cuenta_ap_materno);
            $("#txt_telefono_det").val(data[x].telefono);
            $("#txt_correo_electronico_det").val(data[x].correo_electronico);
            $("#txt_tipo_interlocutor_det").val(data[x].tbl_tipo_interlocutor_id);

            if (data[x].tbl_tipo_interlocutor_id == null) {
                $("#txt_tipo_interlocutor_det").val('0');
            } 
        }
    });
}

$(function () {
    $('#txt_RFC').focusout(function () {
        if ($('#txt_RFC').val() !== '') {
            $.get($("#EndPointAdmon").val() + 'Usuarios/Get/exist/' + 4 + '/' + $('#txt_RFC').val(), function (data, status) {
                if (data[0].cod == 'warning') {
                    Aviso_ErrorSA('', 'El RFC ya existe');
                    $('#txt_RFC').val('');
                }
            });
        }
    });
});

var tbl_proveedor = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_dependencia_id: $('#HDidDependencia').val(),
    p_numero: "",
    p_razon_social: "",
    p_rfc: "",
    p_domicilio_fiscal: "",
    p_rep_legal_nombre: "",
    p_rep_legal_ap_paterno: "",
    p_rep_legal_ap_materno: "",
    p_eje_cuenta_nombre: "",
    p_eje_cuenta_ap_paterno: "",
    p_eje_cuenta_ap_materno: "",
    p_telefono: "",
    p_extension: "",
    p_correo_electronico: "",
    p_es_global: 0,
    p_activo: true,
    p_tipo_interlocutor: 0,
    dependencias_adicionales:null
}