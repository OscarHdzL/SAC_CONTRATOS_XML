var URL_SERVICIO_BASE = URL_OBTENER_PRODUCTOS_POR_DEPENDENCIA = URL_OBTENER_PRODUCTOS_POR_CONTRATO = URL_AGREGAR_PRODUCTO_POR_CONTRATO = URL_OBTENER_PRODUCTO_POR_CONTRATO_POR_ID = URL_ELIMINAR_PRODUCTO_POR_CONTRATO = "";
var existentes = [];
$(document).ready(function () {

    LaunchLoader(true);

    $('#ContratoProductoAC').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        }
    });

    establecerRutasServicio();

    //GetProductoServ();
    GetContratoProducto();
});

function establecerRutasServicio() {
    var idDependencia = $("#hdnIdDependencia").val();
    var idContrato = $("#idContratoCP").val();

    URL_SERVICIO_BASE = $("#EndPointAC").val();

    URL_OBTENER_PRODUCTOS_POR_DEPENDENCIA = URL_SERVICIO_BASE + "AsocContProd/Get/Dependencia/" + idDependencia;
    URL_OBTENER_PRODUCTOS_POR_CONTRATO = URL_SERVICIO_BASE + "AsocContProd/Get/Contrato/" + idContrato;
    URL_AGREGAR_PRODUCTO_POR_CONTRATO = URL_SERVICIO_BASE + "AsocContProd/Add";
    URL_OBTENER_PRODUCTO_POR_CONTRATO_POR_ID = URL_SERVICIO_BASE + "AsocContProd/Get/";
    URL_EDITAR_PRODUCTO_POR_CONTRATO = URL_SERVICIO_BASE + "AsocContProd/Put/";
    URL_ELIMINAR_PRODUCTO_POR_CONTRATO = URL_SERVICIO_BASE + "AsocContProd/Delete/";
}

$('#RegistrarCP').click(function () {
    LaunchLoader(true);
    GetProductoServ();
    $('#ContratoProducto').modal({ backdrop: 'static', keyboard: false });
    $('#ContratoProducto').modal('show');
    $('.ProductoSer').prop('disabled', true);

    setTimeout(function () {
        QuitarItme();
        $('.ProductoSer').prop('disabled', false);
        LaunchLoader(false);
    }, 500);
});

function QuitarItme() {
    for (var i = 0; i <= existentes.length - 1; i++) {
        $(".ProductoSer option[value='" + existentes[i] + "']").remove();
    }
}

function GetProductoServ() {
    //var idDep = $('#idDependenciaCP').val();

    $.get(URL_OBTENER_PRODUCTOS_POR_DEPENDENCIA, function (data, status) {
        var BodyC = "<option value='' disabled selected>Selecciona una opción</option>";
        
        for (var i = 0; i <= data.length - 1; i++) {
            BodyC = BodyC + "<option value='" + data[i].value   + "'>" + data[i].text + "</option>";
        }

        $('.ProductoSer').html(BodyC);
        
		LaunchLoader(false);
    }, 'json');
}

//$('#MontoMin, #MontoMax').keyup(function (event) {
//    if (event.which >= 37 && event.which <= 40) {
//        event.preventDefault();
//    }

//    $(this).val(function (index, value) {
//        return value
//            .replace(/\D/g, "")
//            .replace(/([0-9])([0-9]{2})$/, '$1.$2')
//            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",")
//            ;
//    });
//});

$('#Unitario').keyup(function () {
    if ($('#CantMin').val() === '') {
        return ErrorSA('Error', 'Debe ingresar el valor mínimo');
    }
    if ($('#CantMax').val() === '') {
        return ErrorSA('Error', 'Debe ingresar el valor máximo');
    }

    var unitario = parseFloat($('#Unitario').val());

    var montomin = parseInt($('#CantMin').val().replace(/,/g, ""));
    var resultadoMin = parseFloat(unitario * montomin);
    $('#MontoMin').val(resultadoMin);

    var montomax = parseInt($('#CantMax').val().replace(/,/g, ""));
    var resultadoMax = parseFloat(unitario * montomax);
    $('#MontoMax').val(resultadoMax);
});

$('#CantMin').keyup(function () {
    var unitario = parseFloat($('#Unitario').val() == '' ? 0 : $('#Unitario').val());

    var montomin = parseInt($('#CantMin').val().replace(/,/g, ""));
    var resultadoMin = parseFloat(unitario * montomin);
    $('#MontoMin').val(resultadoMin);

});

$('#CantMax').keyup(function () {
    var unitario = parseFloat($('#Unitario').val() == '' ? 0 : $('#Unitario').val());

    var montomax = parseInt($('#CantMax').val().replace(/,/g, ""));
    var resultadoMax = parseFloat(unitario * montomax);
    $('#MontoMax').val(resultadoMax);
});


$('#CantMin, #CantMax').keyup(function (event) {
    if (event.which >= 37 && event.which <= 40) {
        event.preventDefault();
    }
    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, "")
            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",")
            ;
    });
});

function Validar() {
    var minimon = $('#CantMin').val() == '0' ? 0 : $('#CantMin').val().replace(/,/g, "");
    var maximo = $('#CantMax').val() == '0' ? 0 : $('#CantMax').val().replace(/,/g, "");
    var pres_unitario = $('#Unitario').val() == '0' ? 0 : $('#Unitario').val().replace(/,/g, "");

    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('.ProductoSer').val() === null) {
        Response.Texto = 'Debe seleccionar un producto/servicio';
        Response.Bit = true;
        return Response;
    }
    if ($('#CantMin').val() === '') {
        Response.Texto = 'Debe ingresar un valor numérico en el campo "Cantidad minima"';
        Response.Bit = true;
        return Response;
    }
    if (minimon == 0) {
        Response.Texto = 'Debe ingresar un valor mayor a 0 en el campo "Cantidad minima"';
        Response.Bit = true;
        return Response;
    }

    if ($('#CantMax').val() === '') {
        Response.Texto = 'Debe ingresar un valor numérico en el campo "Cantidad maxima"';
        Response.Bit = true;
        return Response;
    }
    if (maximo == 0) {
        Response.Texto = 'Debe ingresar un valor mayor a 0 en el campo "Cantidad maxima"';
        Response.Bit = true;
        return Response;
    }
    if (parseInt(maximo) <= parseInt(minimon)) {
        Response.Texto = 'La cantidad maxima no puede ser menor o igual a la cantidad minima';
        Response.Bit = true;
        return Response;
    }

    if (($('#Unitario').val() == '') || (pres_unitario == 0)) {
        Response.Texto = 'Debe ingresar un valor numérico o numero mayor a 0 en el campo "Precio unitario"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtDescripcion').val() === '') {
        Response.Texto = 'Debe ingresar una descripción';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('#txtDescripcion').val(), '') !== '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Descripción"';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

$('#GuardarCP').click(function () {
    AddContratoProducto();
});

function CerraModal() {
    $('#ContratoProducto').modal('hide');
}

function AddContratoProducto() {
    var Validacion = Validar();

    var url, tipoVerbo;

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var OBJ_CP = ContratoPClass;
    var d = new Date();
    var date = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();

    if ($('#idCP').val() === '') {
        OBJ_CP.p_id = '00000000-0000-0000-0000-000000000000';
        url = URL_AGREGAR_PRODUCTO_POR_CONTRATO;
        tipoVerbo = "POST";
    }
    else {
        OBJ_CP.p_id = $('#idCP').val();
        url = URL_EDITAR_PRODUCTO_POR_CONTRATO;
        tipoVerbo = "PUT";
    }

    OBJ_CP.p_tbl_contrato_id = $('#idContratoCP').val();
    OBJ_CP.p_tbl_producto_servicio_id = $('.ProductoSer').val();
    OBJ_CP.p_cantidad_minima = parseInt($('#CantMin').val().replace(/,/g, ""));
    OBJ_CP.p_cantidad_maxima = parseInt($('#CantMax').val().replace(/,/g, ""));
    OBJ_CP.p_unitario = parseFloat($('#Unitario').val());
    OBJ_CP.p_monto_minimo = parseFloat($('#MontoMin').val());
    OBJ_CP.p_monto_maximo = parseFloat($('#MontoMax').val());
    OBJ_CP.p_descripcion = $('#txtDescripcion').val();
    //OBJ_CP.p_inclusion = date;
    OBJ_CP.p_estatus = 1;

    var form_data = new FormData();
    form_data.append('CPForm', JSON.stringify(OBJ_CP));

    //console.log(JSON.stringify(OBJ_CP));

    $.ajax({
        url: url,
        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_CP),
        type: tipoVerbo,
        success: function (data) {
            //console.log(data)
            //var objresponse = JSON.parse(data);
           
            //if (!objresponse.Bit) {
            if (data[0].cod === "success") {

                function confirmacion() {
                    return CerraModal();
                }

                var AccionSi = eval(confirmacion);

                $('.CleanCP').val('');
                $('.ProductoSer').val(null);
                $('#idCP').val('');
                $('.titulo').text('Agregar un nuevo registro');
                $('#GuardarCP').text('Guardar');

                GetContratoProducto();

                SuccessSAAction("Operación exitosa", "El registro se guardó correctamente", AccionSi);
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);

            ErrorSA('', objresponse.Descripcion);
        }
    });
}

$('.cerrarMCP').click(function () {
    $('.CleanCP').val('');
    $('.ProductoSer').val(null);
    $('#idCP').val('');
    $('.titulo').text('Agregar un nuevo registro');
    $('#GuardarCP').text('Guardar');
});

function GetContratoProducto() {
    //var idCon = $('#idContratoCP').val();

    $.get(URL_OBTENER_PRODUCTOS_POR_CONTRATO, function (data, status) {
        var Arreglo_arreglos = [];
        console.log(data)

        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            Interno.push(data[i].contrato);
            Interno.push(data[i].productoServicio);

            existentes.push(data[i].tbl_producto_servicio_id);
            console.log(existentes);
            Interno.push(data[i].cantidad_minima);
            Interno.push(data[i].cantidad_maxima);
            Interno.push(data[i].unitario === null ? "$" + " " + 0 : "$" + " " + data[i].unitario);
            Interno.push(data[i].monto_minimo === null ? "$" + " " + 0 : "$" + " " + data[i].monto_minimo);
            Interno.push(data[i].monto_maximo === null ? "$" + " " + 0 : "$" + " " + data[i].monto_maximo);
            Interno.push(data[i].descripcion);
            //Interno.push('<a onclick="btnEditCP(\'' + data[i].id + '\')" class="fa fa-pencil-square-o btn btn-primary" title="Modificar registro"> </a> <a onclick="btnDeleteCP(\'' + data[i].id + '\')" class="fa fa-trash-o btn btn-danger" title="Eliminar registro"> </a>');
            if (data[i].estatus == true) {
                Interno.push('<a onclick="btnEditCP(\'' + data[i].id + '\')" class="fa fa-pencil-square-o btn btn-primary" title="Modificar registro"> </a> <a onclick="btnDeleteCP(\'' + data[i].id + '\')" class="fa fa-trash-o btn btn-danger" title="Eliminar registro"> </a>');
            } else {
                Interno.push('');
                $('#RegistrarCP').addClass('hidden');
            }

            Arreglo_arreglos.push(Interno);
        }

        var table = $('#ContratoProductoAC').DataTable().destroy();

        //table.destroy();
        //console.log(Arreglo_arreglos);
        $('#ContratoProductoAC').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No. Contrato" },
                { title: "Producto" },
                { title: "Cantidad mínima" },
                { title: "Cantidad máxima" },
                { title: "Precio unitario" },
                { title: "Monto mínimo" },
                { title: "Monto máximo" },
                { title: "Descripción" },
                { title: "Acciones" }
            ]
        });

        LaunchLoader(false);
    });
}

function EliminarCP(id) {
    //console.log("eliminar")
    $.ajax({
        url: URL_ELIMINAR_PRODUCTO_POR_CONTRATO + id,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                existentes = [];
                SuccessSA('', 'El registro se eliminó exitosamente');
                GetContratoProducto();
            }
            else if (objresponse[0].cod == "warning") {
                ErrorSA('', objresponse[0].msg);
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

function btnDeleteCP(id) {
    function Confirmacion() {
        return EliminarCP(id);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function btnEditCP(id) {
    GetProductoServ();
    $('#ContratoProducto').modal({ backdrop: 'static', keyboard: false });
    $('#ContratoProducto').modal('show');
    $('#idCP').val(id);
    $('.titulo').text('Modificar registro');
    $('#GuardarCP').text('Modificar');
    var id_prod = null;
    $.get(URL_OBTENER_PRODUCTO_POR_CONTRATO_POR_ID + id, function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {

            $('.ProductoSer').prop('disabled', true);
            $('#CantMin').val(data[i].cantidad_minima);
            $('#CantMax').val(data[i].cantidad_maxima);
            $('#Unitario').val(data[i].unitario);
            $('#MontoMin').val(data[i].monto_minimo);
            $('#MontoMax').val(data[i].monto_maximo);
            $('#txtDescripcion').val(data[i].descripcion);
            id_prod = data[i].tbl_producto_servicio_id;

        }
        setTimeout(function () {
            $('.ProductoSer').val(id_prod);
        }, 200);
    });
}

var ContratoPClass = {
    p_opt: 0,
    p_id: null,
    p_tbl_contrato_id: null,
    p_tbl_producto_servicio_id: null,
    p_cantidad_minima: null,
    p_cantidad_maxima: null,
    p_monto_minimo: null,
    p_monto_maximo: null,
    p_descripcion: null,
    p_estatus: null,
    p_unitario: null
};
