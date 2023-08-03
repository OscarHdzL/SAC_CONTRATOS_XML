///VARIABLES GLOBALES 
var idPlanEntrega = null;
var URL_SERVICIO_BASE = "";
//-------------------------INSTANCIAS NUEVAS

var DatosBD = null;
var UbicacionDetalle = null;
//-----------------------------------------------------

///INICIO (READY)
$(document).ready(function () {
    console.log('establecer rutas de servicio');
    //-- obtener ruta iva de la instancia --//
    establecerRutasServicio();
    idPlanEntrega = $("#idPlan").val();
    getPlanesEntrega();
    setTimeout(function () {
        $.extend($.fn.dataTable.defaults, {
            responsive: true
        });
        $('#Producto_tbl').DataTable();
        $('#Producto_tbl_length').remove();
        $('#Producto_tbl_info').remove();
    }, 500);

    //INICIO - Se inicializan datatables
    $('#tbl_ubicaciones_detalle').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "0%", "targets": 0 },
            { "width": "20%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "30%", "targets": 4 },
            { "width": "20%", "targets": 5 },
        ],
    });

    $('#tbl_productos_eliminar').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "30%", "targets": 0 },
            { "width": "20%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            { "width": "15%", "targets": 4 },
        ],
    });
    //FIN - Se inicializan datatables

    fechasIni();

});
////END Document Ready

function fechasIni() {
    $('.fechas').datetimepicker({
        format: 'YYYY-MM-DD'
    });
}

///Consultas

function getPlanesEntrega() {
    $.get($("#EndPointAC").val() + 'Operaciones/PE/Get/PE/detalle/idPlan/' + $("#idPlan").val(), function (data, status) {
        console.log(data);
        if (data) {
            if (data.length > 0) {
                console.log(data);
                DatosBD = data[0];
                if (DatosBD.header == null) {
                    window.history.back();
                }
                var Pheader = data[0].header;
                $("#txtIdentificador").val(Pheader.identificador);
                $("#txtPeriodo").val(Pheader.periodo);
                var fechaE = Pheader.fecha_ejecucion != null ? Pheader.fecha_ejecucion.toString().slice(0, Pheader.fecha_ejecucion.toString().indexOf("T")) : "";
                $("#txtEjecucion").val(fechaE);
                $("#txtDescripcion").val(Pheader.descripcion);
                $("#DropTipoPlan").val($('select#DropTipoPlan option:contains(' + Pheader.tipo_entrega + ')').val());
                $("#txtResponsablePE").val(Pheader.responsable_PE);
                llenarArreglosUbicaciones(data[0].ubicaciones);

            } else {
                ErrorSA('Error en los datos de entrada', 'No se pudo cargar el plan de entrega');
            }
        } else {
            ErrorSA('Error en los datos de entrada', 'No se pudo cargar el plan de entrega');
        }
    })
}

//-- inicio llenar arreglos desde datos
function llenarArreglosUbicaciones(listadoUbicaciones) {
    for (var i = 0; i < listadoUbicaciones.length; i++) {
        var t = $('#tbl_ubicaciones_detalle').DataTable();
        var listadoProductosL = '';
        for (var j = 0; j < listadoUbicaciones[i].listado_productos.length; j++) {
            listadoProductosL += listadoUbicaciones[i].listado_productos[j].elemento + ' : ' + listadoUbicaciones[i].listado_productos[j].cantidad + ' <br/>';
        }
        t.row.add([
            listadoUbicaciones[i].unidad_ubicacion,
            listadoUbicaciones[i].direccion_ubicacion,
            listadoUbicaciones[i].ejecutor_nombre,
            '<p>' + listadoProductosL + '</p>',
            '<button class="btn btn-info" onclick="ModalEliminarProductos(\'' + listadoUbicaciones[i].tbl_ubicacion_id + '\');" type="button">Ver detalle ubicación</button>',
            '<button class="btn btn-danger" onclick="EliminarUbicacion(\'' + listadoUbicaciones[i].tbl_ubicacion_id + '\');" type="button">Eliminar Posición</button>'
        ]).draw();
    }
}

//-- fin llenar arreglos desde datos
function ModalEliminarProductos(IdUbicacion) {
    Redimension();
    UbicacionDetalle = DatosBD.ubicaciones.find(x => x.tbl_ubicacion_id == IdUbicacion);
    console.log(UbicacionDetalle);
    $('#TitleModalProducto').html('Eliminar productos de la ubicación  ' + UbicacionDetalle.unidad_ubicacion);
    $('#M_idUbicacion').val(IdUbicacion);

    $('#txtUnidadUbicacion').val(UbicacionDetalle.unidad_ubicacion);
    $('#txtDireccion').val(UbicacionDetalle.direccion_ubicacion);
    $('#txtEjecutor').val(UbicacionDetalle.ejecutor_nombre);
    //tbl_productos_eliminar
    $('#ModalEliminarProducto').modal('show');

    for (var i = 0; i < UbicacionDetalle.listado_productos.length; i++)
    {
        var t_productos = $('#tbl_productos_eliminar').DataTable();
        t_productos.row.add([
            UbicacionDetalle.listado_productos[i].elemento,
            UbicacionDetalle.listado_productos[i].clave_producto,
            UbicacionDetalle.listado_productos[i].cantidad,
            UbicacionDetalle.listado_productos[i].total,
            '<button class="btn btn-danger" onclick="EliminarProductoIndividual(\'' + UbicacionDetalle.listado_productos[i].tbl_plan_entrega_producto_id + '\',\'' + UbicacionDetalle.listado_productos[i].tbl_ubicacion_plan_entrega_id + '\',\'' + UbicacionDetalle.listado_productos[i].tbl_contrato_producto_id + '\');" type="button">Eliminar producto</button>'
        ]).draw(false);
    }
}

function EliminarProductoIndividual(tbl_plan_entrega_producto_id, tbl_ubicacion_plan_entrega_id, tbl_contrato_producto_id)
{
    console.log('Eliminando producto');
    console.log('primer parámetro ' + DatosBD.header.tbl_plan_entrega_id);
    console.log('segundo parámetro ' + tbl_plan_entrega_producto_id);
    console.log('tercer parámetro ' + tbl_ubicacion_plan_entrega_id);
    console.log('cuarto parámetro ' + tbl_contrato_producto_id);
    var modelo =
    {
        tbl_plan_entrega_id: DatosBD.header.tbl_plan_entrega_id,
        plan_entrega_producto_id: tbl_plan_entrega_producto_id,
        tbl_ubicacion_plan_entrega_id: tbl_ubicacion_plan_entrega_id,
        tbl_contrato_producto_id: tbl_contrato_producto_id
    };
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(modelo),
        type: 'post',
        success: function (data) {
            console.log(data);
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                ExitoEliminarProducto(objresponse.msg);
            } else {
                ErrorEliminarProducto(objresponse.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.responseText);
            if (XMLHttpRequest.responseText != null) {
                var errorResponse = JSON.parse(XMLHttpRequest.responseText);
                if (errorResponse.msg != null) {
                    ErrorSA('Error', errorResponse.msg);
                } else {
                    ErrorSA('Error', "Ocurrio un error al eliminar la ubicación.");
                }

            } else {
                ErrorSA('Error', "Ocurrio un error al eliminar la ubicación.");
            }
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAC").val() + "Confirmar/PE/EliminarProducto"

    });



}

function ErrorEliminarProducto(mensaje) {
    Swal.fire({
        allowOutsideClick: false,
        type: 'warning',
        title: 'Atención.!',
        text: mensaje,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Continuar',
        cancelButtonText: 'Cancelar',
    }).then((result) => {
        if (result.value) {
            // SuccessSA('', 'Picaste si');
        }
        else {
            // SuccessSA('Cancelado', 'Picaste no')
        }
    });
}

function ExitoEliminarProducto(mensaje) {
    Swal.fire({
        allowOutsideClick: false,
        type: 'success',
        title: 'Éxito.!',
        text: mensaje,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Continuar',
        cancelButtonText: 'Cancelar',
    }).then((result) => {
        if (result.value) {
            // SuccessSA('', 'Picaste si');
            Recargar();
        }
        else {
            // SuccessSA('Cancelado', 'Picaste no')
            Recargar();
        }
    });
}

function EliminarUbicacion(tbl_ubicacion_id)
{
    var modelo =
    {
        tbl_plan_entrega_id: DatosBD.header.tbl_plan_entrega_id,
        tbl_ubicacion_id: tbl_ubicacion_id
    };
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(modelo),
        type: 'post',
        success: function (data) {
            console.log(data);
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                ExitoEliminarProducto(objresponse.msg);
            } else {
                ErrorEliminarProducto(objresponse.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.responseText);
            if (XMLHttpRequest.responseText != null) {
                var errorResponse = JSON.parse(XMLHttpRequest.responseText);
                if (errorResponse.msg != null) {
                    ErrorSA('Error', errorResponse.msg);
                } else {
                    ErrorSA('Error', "Ocurrio un error al eliminar la ubicación.");
                }
                
            } else {
                ErrorSA('Error', "Ocurrio un error al eliminar la ubicación.");
            }
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAC").val() + "Confirmar/PE/EliminarUbicacion"

    });
}

function Recargar() {
    location.reload();
}

function LimpiarModalEliminarProducto() {
    UbicacionDetalle = null;

    var table = $('#tbl_productos_eliminar').DataTable();
    table.clear().draw();
    $('#TitleModalProducto').html('Eliminar productos de la ubicación  ');
    $('#M_idUbicacion').val(null);

    $('#txtUnidadUbicacion').val(null);
    $('#txtDireccion').val(null);
    $('#txtEjecutor').val(null);
}

function LimpiarModalAgregarProducto() {
    $('#AgregarProducto').show();
    $('#CancelarVerProducto').hide();
    $('#DropProductos').val('');
    $('#txtClaveProducto').val('');
    $('#txtCantidad').val('');
    $('#txtCantidadTotal').val('');
    $('#Monto').val('');
    $('#MontoIVA').val('');
    $('#Total').val('');
    //$('#tbl_productos').DataTable().clear().draw();
    $('.listado-obligaciones').html('');

}

//////////MODALES

function CerrarModalUbicacion() {
    $('#ModalAgregarUbicacion').modal('hide');
    LimpiarFormUbicacion();
}

function Redimension() {

    var tables = document.getElementsByTagName('table');
    for (var i = 0; i < tables.length; i++) {
        if (tables[i].id != "") {
            $('#' + tables[i].id + '').resize();
        }
    }

}

function establecerRutasServicio() {
    console.log('obteniendo ruta del iva');
    var idInstancia = $("#IdInstancia").val();
    URL_SERVICIO_BASE = $("#EndPointAC").val();
}
