var ID_PRODUCTOSERVICIO_NUEVO = 0;
var LONGITUD_500 = 500;
var LONGITUD_30 = 1000;
var URL_SERVICIO_BASE = URL_OBTENER_PRODUCTOS = URL_OBTENER_UNIDADES_MEDIDA = URL_AGREGAR_PRODUCTO = URL_EDITAR_PRODUCTO = URL_ELIMINAR_PRODUCTO = URL_OBTENER_PRODUCTO_POR_ID = URL_OBTENER_TIPO_SERV ="";

$(function () {
    $('#tbl_productoservicio').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
    });
    //obtenerListado();
    //obtenerListadoUnidadesMedida();
});

$(document).ready(function () {

    establecerRutasServicio();

    obtenerListado();
    obtenerListadoUnidadesMedida();
    obtenerListadoTipoPS();
});

function establecerRutasServicio() {
    var idDependencia = $("#HDidDependencia").val();

    URL_SERVICIO_BASE = $("#EndPointAC").val();
    
    URL_OBTENER_PRODUCTOS = URL_SERVICIO_BASE + "ProdServ/List/Dependencia/" + idDependencia;
    URL_OBTENER_UNIDADES_MEDIDA = URL_SERVICIO_BASE + "ProdServ/List/UnidadesMedida";
    URL_OBTENER_TIPO_SERV = URL_SERVICIO_BASE + "ProdServ/List/TipoPS";
    URL_AGREGAR_PRODUCTO = URL_SERVICIO_BASE + "ProdServ/Add";
    URL_EDITAR_PRODUCTO = URL_SERVICIO_BASE + "ProdServ/Put";
    URL_ELIMINAR_PRODUCTO = URL_SERVICIO_BASE + "ProdServ/Delete/";
    URL_OBTENER_PRODUCTO_POR_ID = URL_SERVICIO_BASE + "ProdServ/Unitario/";
}

$("#btnCargaMasiva").click(function () {
    $("#modalCargaMasiva").modal("show");
}); 

$("#btnRealizarCargaMasiva").click(function () {

    var fileUpload = $("#archivo").get(0);

    var formData = new FormData();
    formData.append('archivo', fileUpload.files[0]);

    $.ajax({
        url: "/ProductoServicio/RealizarCargaMasiva",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            console.log(data);
            $("#modalCargaMasiva").modal("hide");
            SuccessSA();
        },
        error: function () {

        }
    });
    //Validar formato de archivo cargado
    //var formData = new FormData();

    //var totalFiles = document.getElementById("archivo").files.length;

    //for (var i = 0; i < totalFiles; i++) {
    //    var archivo = document.getElementById("archivo").files[i];
    //    //var archivo = $("#modalCargaMasiva file").files[0];
    //    console.log(archivo)
    //    formData.append("archivo", archivo);
    //}
    //console.log(formData);

    //$.ajax({
    //    dataType: 'json',
    //    contentType: false,
    //    processData: false,
    //    data: formData,
    //    type: 'post',
    //    url: '/Request/ProductoServicio/RealizarCargaMasiva/' + $("#hdnIdInstancia").val() + '/' + $('#hdnIdDependencia').val() + '/' + $("#hdnIdUsuarioAlta").val(),
    //    success: function (data) {

    //        if (data.respuesta.Bit) {

    //            //SuccessSA();
    //            //$("#modalCargaMasiva").modal("hide");
    //            window.location.reload();

    //            reestablecerPagina();
    //        }
    //        else {
    //            ErrorSA('', data.Excepcion);
    //        }
    //    },
    //    error: function (data) {

    //        if (!data.Bit) {
    //            Swal.fire({
    //                type: 'error',
    //                title: 'Error al realizar la operación',
    //                text: data.Excepcion
    //            });
    //        };
    //    }
    //});
});

$("#btn-abrir-modal-guardar").click(function () {
    $('#tituloPS').text("Alta de producto o servicio");
    $("#hdnId").val(ID_PRODUCTOSERVICIO_NUEVO);

    abrirModalGuardarProductoServicio();
});

function abrirModalEditar(id) {
    
    $.get(URL_OBTENER_PRODUCTO_POR_ID + id, function (data, status) {
        console.log(data);

        var item = data[0];

        $("#hdnId").val(id);
        console.log($("#hdnId").val());
        $("#idProductoServicio").val(item.producto_servicio);
        $("#claveProducto").val(item.clave_producto);
        $("#elemento").val(item.elemento);
        $("#descripcionElemento").val(item.elemento_desc);
        //$("#dependencia").val(data.TBLENT_DEPENDENCIA_id);
        $(".cmb_unidadmedida").val(item.tbl_unidad_medida_id);
        $('.tbl_prodserv').val(item.tbl_tipo_id);
        $('#txtComentarios').val(item.comentario);
    });
    $('#tituloPS').text("Modificar producto o servicio");
    $("#modalGuardarProductoServicio").modal('show');
}

function abrirModalGuardarProductoServicio() {

    $("#modalGuardarProductoServicio").modal('show');
}

function obtenerListado() {

    $.get(URL_OBTENER_PRODUCTOS, function (data, status) {
        var listado = [];

        for (var i = 0; i <= data.length - 1; i++)
        {
            var fila = [];
            var item = data[i];

            fila.push(item.clave_producto);
            fila.push(item.elemento_desc);
            fila.push(item.unidad_medida);
            fila.push(item.tipo);
            fila.push(item.comentario);

            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"abrirModalEditar('" + item.id + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar' onclick=\"eliminar('" + item.id + "');\"><i class='fa fa-trash'></i></button>");

            listado.push(fila);
        }
        
        var tabla = $('#tbl_productoservicio').DataTable();

        tabla.destroy();

        $('#tbl_productoservicio').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: listado,
            columns: [
                { title: "Clave de producto" },
                { title: "Especificación del bien o servicio" },
                { title: "Unidad de medida" },
                { title: "Tipo" },
                { title: "Comentarios" },
                { title: "Acciones" }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
    });
}

function obtenerListadoUnidadesMedida() {

    $.get(URL_OBTENER_UNIDADES_MEDIDA, function (data, status) {
        var body = "<option value='' selected disabled>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].unidad_medida + "</option>";
        }
        $('.cmb_unidadmedida').html(body);
    });
}

function obtenerListadoTipoPS() {

    $.get(URL_OBTENER_TIPO_SERV, function (data, status) {
        var body = "<option value='' selected disabled>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('.tbl_prodserv').html(body);
    });
}

$("#modalGuardarProductoServicio #btnGuardar").click(function () {
    guardarProductoServicio();

});

function guardarProductoServicio() {    
    var evaluacion = validarEntidad();
    if (evaluacion.Bit) {
        ErrorSA('Hay un error en los datos de entrada', evaluacion.Texto);
        return;
    }

    var url = evaluacion.objeto.p_id === (null||'0') ? URL_AGREGAR_PRODUCTO : URL_EDITAR_PRODUCTO;
    var tipoVerbo = evaluacion.objeto.p_id === (null || '0') ? "POST" : "PUT";

    console.log(JSON.stringify(evaluacion.objeto));
    $.ajax({
        url: url,
        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(evaluacion.objeto),
        type: tipoVerbo,
        success: function (data) {
            if (!data.Bit) {
                reestablecerPagina();
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Error al guardar el producto o servicio',
                    text: data.Excepcion
                })
            }
        },
        error: function (data) {
            //alert(data);
            //mostrar loader
            LaunchLoader(true);
            Swal.fire({
                type: 'error',
                title: 'Error al guardar el producto o servicio',
                text: data
            })
        }
    });
}
$('.Clear').click(function () {
    $("#unidadMedida").val('');
    $('.tbl_prodserv').val('');
    $(".campo-formulario").val('');
});

function reestablecerPagina() {
    obtenerListado();
    obtenerListadoUnidadesMedida();
    LaunchLoader(false);
    //ocultar louder
    $('#modalGuardarProductoServicio, #modalCargaMasiva').modal('hide');

    SuccessSA('', '');

    $("#hdnId").val('');
    $(".campo-formulario").val('');
}

/********************************/
$('.cerrarPS').click(function () {
    $("#hdnId").val('');
    $("#idProductoServicio").val('');
    $("#claveProducto").val('');
    $("#elemento").val('');
    $("#descripcionElemento").val('');
    //$("#dependencia").val('');
    $("#unidadMedida").val('');
    $("#txtComentarios").val('');
    $('.tbl_prodserv').val(null);
})

function EliminarPS(id) {
    console.log(URL_ELIMINAR_PRODUCTO + id)
    $.ajax({
        url: URL_ELIMINAR_PRODUCTO + id,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: 'DELETE',
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                obtenerListado();
            }
            else if (objresponse[0].cod == "warning") {
                ErrorSA('', objresponse[0].msg);
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        }
    })
}

function eliminar(id) {
    function Confirmacion() {
        return EliminarPS(id);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}
/********************************/

function validarEntidad()
{
    var OBJ = claseProductoServicio;
    var Response = { Texto: '', Bit: true, objeto: null };
    var strClaveProducto = $('#claveProducto').val();
    var strElemento = $('#elemento').val();
    var strDescripcionElemento = $('#descripcionElemento').val();
    var lblClaveProducto = $("#lblClaveProducto").html();
    var lblElemento = $("#lblElemento").html();
    var lblDescripcionElemento = $("#lblDescripcionElemento").html();
    
 
    
    if ($.trim(strClaveProducto) === '') {
        Response.Texto = 'El campo "' + lblClaveProducto + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('#claveProducto').val(), '') !== '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "' + lblClaveProducto+'"';
        Response.Bit = true;
        return Response;
    }
    if ($.trim(strElemento) === '') {
        Response.Texto = 'El campo "' + lblElemento + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('#elemento').val(), '') !== '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "' + lblElemento + '"';
        Response.Bit = true;
        return Response;
    }
    if ($('.tbl_prodserv').val() === ('' || null)) {
        Response.Texto = 'El campo "Tipo" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if ($('.cmb_unidadmedida').val() === ('' || null)) {
        Response.Texto = 'El campo "Unidad de medida" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if ($.trim(strDescripcionElemento) === '') {
        Response.Texto = 'El campo "' + lblDescripcionElemento + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('#descripcionElemento').val(), '') !== '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "' + lblDescripcionElemento + '"';
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strClaveProducto, LONGITUD_500)) {
        Response.Texto = generarMensajeValidacionLongitud(lblClaveProducto, LONGITUD_500);
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strElemento, LONGITUD_30)) {
        Response.Texto = generarMensajeValidacionLongitud(lblElemento, LONGITUD_30);
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strDescripcionElemento, LONGITUD_30)) {
        Response.Texto = generarMensajeValidacionLongitud(lblDescripcionElemento, LONGITUD_30);
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('#txtComentarios').val(), '') !== '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Comentario"';
        Response.Bit = true;
        return Response;
    }

    OBJ.p_id = $("#hdnId").val();
    OBJ.p_tbl_dependencia_id = $('#HDidDependencia').val();
    //OBJ.p_producto_servicio = $('#idProductoServicio').val();
    OBJ.p_clave_producto = $('#claveProducto').val();
    OBJ.p_elemento = $('#elemento').val();
    OBJ.p_elemento_desc = $('#descripcionElemento').val();
    OBJ.p_tbl_unidad_medida_id = $('.cmb_unidadmedida').val();
    OBJ.p_tbl_tipo_id = $('.tbl_prodserv').val();
    OBJ.p_comentario = $("#txtComentarios").val();

    Response.Texto = '';
    Response.Bit = false;
    Response.objeto = OBJ;

    return Response;
}

function generarMensajeValidacionLongitud(lblCampoValidar, longitudCadena) {
    return 'La longitud del campo "' + lblCampoValidar + '" debe ser menor o igual a ' + longitudCadena + ' caracteres.';
}

var claseProductoServicio = {
    p_opt: 0,
    p_id: null,
    p_tbl_dependencia_id : '',
    p_producto_servicio: '-',
    p_clave_producto: '',
    p_elemento: '',
    p_elemento_desc: '',
    p_tbl_unidad_medida_id: '',
    p_activo: 1,
    //p_inclusion: '20200122',
    p_comentario: '',
    p_tbl_tipo_id: ''
};