///VARIABLES GLOBALES 
var JSON_Productos = null; ///contiene la lista de los productos de la tabla [tbl_contrato_productos_ac]
var JSON_Ubicaciones = null;
var JSON_PlanesEntrega = null; //

var JSON_Eliminar_P = null;
var JSON_Eliminar_P_Temp = [];

var ArregloProductosNoGuardados = [];

var ArregloUbicaciones = [];
var ArregloProductos = [];
var listaproductos = '';
var idPlanEntrega = null;

var URL_OBTENER_IVA_INSTANCIA = "";
var URL_SERVICIO_BASE = "";
var listaEjecutores = [];
var objUbicacionesXBD = null;
var arregloJsonDiv = [];
var ProductosPlanL = [];

//-------------------------INSTANCIAS NUEVAS

//UbicacionEjecutor

var UbicacionEjecutor = {
    idUbicacion: "00000000-0000-0000-0000-000000000000",
    idEjecutor: "00000000-0000-0000-0000-000000000000"
}



//Principal
var EstructuraPalnEntrega = {
    Header: null,
    UbicacionesProductos: null
}


//header
var sp_plan_entrega_input = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_contrato_servidor_resp_id: "00000000-0000-0000-0000-000000000000",
    p_identificador: null,
    p_periodo: "00000000-0000-0000-0000-000000000000",
    p_descripcion: null,
    p_fecha_ejecucion: "0001-01-01T00:00:00",
    p_activo: null,
    p_tipo_entrega: null
}

//Ubicacion
var UbicacionProductos = {
    tbl_ubicacion_id: "00000000-0000-0000-0000-000000000000",
    EjecutorPorUbicacion: "00000000-0000-0000-0000-000000000000",
    productos: null
}


//producto por ubicacion
var sp_plan_entrega_producto = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_contrato_producto_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_ubicacion_plan_entrega_id: "00000000-0000-0000-0000-000000000000",
    p_estatus: null,
    p_cantidad: 0,
    p_detalle_actividad: null,
    p_tipo: null,
    p_monto: 0,
    p_monto_iva: 0,
    p_total: 0
}

//-----------------------------------------------------


///INICIO (READY)
$(document).ready(function () {
    console.log('establecer rutas de servicio');
    //-- obtener ruta iva de la instancia --//
    establecerRutasServicio();

    //-- fin obtener iva --//

    getDropProductos_PE();
    GetDropResponsables_PE();
    getDropUbicaciones_PE();
    //GetDropEjecutores_PE();
    idPlanEntrega = $("#idPlan").val();
    getPlanesEntrega();


    //Codigo para el Wizard
    setTimeout(function () {
        $.extend($.fn.dataTable.defaults, {
            responsive: true
        });
        $('#Producto_tbl').DataTable();
        //setInterval('Redimension()', 500);
        $('#Producto_tbl_length').remove();
        $('#Producto_tbl_info').remove();
    }, 500);


    var navListItems = $('div.setup-panel div a'),
        allWells = $('.setup-content'),
        allNextBtn = $('.nextBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
            $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('btn-primary').addClass('btn-default');
            $item.addClass('btn-primary');
            allWells.hide();
            $target.show();
            $target.find('input:eq(0)').focus();
        }
    });

    allNextBtn.click(function () {
        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }

        if (isValid) nextStepWizard.removeAttr('disabled').trigger('click');
    });

    $('div.setup-panel div a.btn-primary').trigger('click');

    //End Wizard

    //INICIO - Se inicializan datatables
    $('#tbl_ubicaciones').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "0%", "targets": 0 },
            { "width": "0%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "30%", "targets": 4 },
            { "width": "25%", "targets": 5 },
            { "width": "10%", "targets": 6 },
            { "width": "05%", "targets": 7 },
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            }
        ],
    });

    $('#tbl_productos').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "0%", "targets": 0 },
            { "width": "40%", "targets": 1 },
            { "width": "40%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            { "width": "0%", "targets": 4 },

            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [4],
                "visible": false,
                "searchable": false
            }

        ],
    });


    //INICIO - Se inicializan datatables
    $('#tbl_ubicaciones_vw').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "20%", "targets": 0 },
            { "width": "30%", "targets": 1 },
            { "width": "25%", "targets": 2 },
            { "width": "25%", "targets": 3 },

        ],
    });


    //FIN - Se inicializan datatables


    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });



    ///Eliminar resgistros

    $('#tbl_ubicaciones tbody').on('click', '.eliminar', function () {


        Swal.fire({
            allowOutsideClick: false,
            type: 'warning',
            title: 'Atención.!',
            text: 'Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',

            confirmButtonText: 'Continuar',
            cancelButtonText: 'Cancelar',
        }).then((result) => {
            if (result.value) {

                var table = $('#tbl_ubicaciones').DataTable();

                var _data = table.row($(this).parents('tr')).data();

                if (_data.length == undefined) {



                    var closestRow = $(this).closest('tr');
                    var data = table.row($(this).parents(closestRow)).data();

                    var IDRegistro = data[0].toString();

                    var JSON_Eliminar = JSON.parse($('#' + 'ArregloProductos_' + IDRegistro).html());


                    ///Se suma la cantidad de productos eliminados a JSON_PRODUCTOS
                    //Se comento estas lieneas para la prueba de PE
                    for (var i = 0; i < JSON_Productos.length; i++) {
                        for (var j = 0; j < JSON_Eliminar.length; j++) {
                            if (JSON_Productos[i].tbl_contrato_producto_id == JSON_Eliminar[j].productos[0].p_tbl_contrato_producto_id) {
                                JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) + parseInt(JSON_Eliminar[j].productos[0].p_cantidad);
                            }
                        }
                    }
                    //Se comento estas lieneas para la prueba de PE

                    table
                        .row($(this).parents(closestRow))
                        .remove()
                        .draw();



                } else {
                    var data = table.row($(this).parents('tr')).data();


                    var IDRegistro = data[0].toString();

                    var JSON_Eliminar = JSON.parse($('#' + 'ArregloProductos_' + IDRegistro).html());


                    if (JSON_Eliminar.length != undefined) { //no existen productos

                        ///Se suma la cantidad de productos eliminados a JSON_PRODUCTOS
                        for (var i = 0; i < JSON_Productos.length; i++) {
                            for (var j = 0; j < JSON_Eliminar.length; j++) {
                                if (JSON_Productos[i].tbl_contrato_producto_id == JSON_Eliminar[j].productos[0].p_tbl_contrato_producto_id) {
                                    JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) + parseInt(JSON_Eliminar[j].productos[0].p_cantidad);
                                }
                            }
                        }

                    }
                    //Se comento estas lieneas para la prueba de PE

                    table
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();

                }

                SuccessSA('', 'El registro se eliminó exitosamente');


            }
            else {

                SuccessSA('Cancelado', 'Proceso cancelado')

            }
        });





    })


    $('#tbl_productos tbody').on('click', '.eliminar', function () {

        Swal.fire({
            allowOutsideClick: false,
            type: 'warning',
            title: 'Atención.!',
            text: 'Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',

            confirmButtonText: 'Continuar',
            cancelButtonText: 'Cancelar',
        }).then((result) => {
            if (result.value) {

                var table = $('#tbl_productos').DataTable();
                var data = table.row($(this).parents('tr')).data();
                //var JSON_Eliminar = JSON.parse(data[0]);
                JSON_Eliminar_P = JSON.parse(data[0]);


                JSON_Eliminar_P_Temp.push(JSON_Eliminar_P);
                ///Se suma la cantidad de productos eliminados a JSON_PRODUCTOS
                for (var i = 0; i < JSON_Productos.length; i++) {

                    if (JSON_Productos[i].tbl_contrato_producto_id == JSON_Eliminar_P.productos[0].p_tbl_contrato_producto_id) {
                        JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) + parseInt(JSON_Eliminar_P.productos[0].p_cantidad);


                        ///Sort
                        //$("#DropProductos option[value='']").each(function () {
                        //    $(this).remove();
                        //});
                        //var options = $('select.DropProductos option');
                        //var arr = options.map(function (_, o) { return { t: $(o).text(), v: o.value }; }).get();
                        //arr.sort(function (o1, o2) { return o1.t > o2.t ? 1 : o1.t < o2.t ? -1 : 0; });
                        //options.each(function (i, o) {
                        //    o.value = arr[i].v;
                        //    $(o).text(arr[i].t);
                        //});

                        ///Sort

                        LimpiarModalAgregarProducto();

                    }

                }

                table
                    .row($(this).parents('tr'))
                    .remove()
                    .draw();

                SuccessSA('', 'El registro se eliminó exitosamente');

            }
            else {

                SuccessSA('Cancelado', 'Proceso cancelado')

            }
        });
    })


    fechasIni();


    $('#tbl_productos tbody').on('click', '.ver', function () {
        console.log('ver el elemento');

        var table = $('#tbl_productos').DataTable();
        var data = table.row($(this).parents('tr')).data();
        JSON_Eliminar_P = JSON.parse(data[0]);

        ///Se suma la cantidad de productos eliminados a JSON_PRODUCTOS
        for (var i = 0; i < JSON_Productos.length; i++) {
            if (JSON_Productos[i].tbl_contrato_producto_id == JSON_Eliminar_P.productos[0].p_tbl_contrato_producto_id) {
               console.log('elemento encontrado');
                console.log(JSON_Eliminar_P.productos);

                LimpiarModalAgregarProducto();
                $('#AgregarProducto').hide();
                $('#CancelarVerProducto').show();
                $('#DropProductos').val(JSON_Eliminar_P.productos[0].p_tbl_contrato_producto_id);
                $("#DropProductos").change();

                $('#txtCantidad').val(formatNumber(parseFloat(JSON_Eliminar_P.productos[0].p_cantidad.toString().replaceAll(',', '')).toFixed(2).toString()));
                $('#Monto').val(formatNumber(parseFloat(JSON_Eliminar_P.productos[0].p_monto.toString().replaceAll(',', '')).toFixed(2).toString()));
                $('#MontoIVA').val(formatNumber(parseFloat(JSON_Eliminar_P.productos[0].p_monto_iva.toString().replaceAll(',', '')).toFixed(2).toString()));
                $('#Total').val(formatNumber(parseFloat(JSON_Eliminar_P.productos[0].p_total.toString().replaceAll(',', '')).toFixed(2).toString()));

            }
        }

    })

    $('#CancelarVerProducto').on('click', function () {
        LimpiarModalAgregarProducto();
    });

});
////END Document Ready

function fechasIni() {
    $('.fechas').datetimepicker({
        format: 'YYYY-MM-DD'
    });
}

function testEliminar() {
    var arreglo = [{
        "cantidad": "2",
        "valor": 468,
        "producto": "Banana",
        "idprod": 1
    },
    {
        "cantidad": "3",
        "valor": 678,
        "producto": "cebolla",
        "idprod": 2
    }
    ];

    alert(JSON.stringify(arreglo));

    var eliminar = 1;

    for (var indice in arreglo) {

        var id = arreglo[indice].idprod;

        if (id == eliminar) {

            var index = indice;
        }
    }

    arreglo.splice(index, 1);
    alert(JSON.stringify(arreglo));

}

///Consultas

function getPlanesEntrega() {

    var idCon = $('#idContrato').val();
    var encontrado = false;
    var datosParaEditar = null;
    $.get($("#EndPointAC").val() + 'Operaciones/PE/Get/tipo/contrato/id/' + idCon, function (data, status) {
        if (data) {
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].header.p_id == $("#idPlan").val()) {
                        datosParaEditar = data[i];
                        console.log(datosParaEditar);
                        encontrado = true;
                    }
                }
            }
        }
        if (encontrado == false) {
            ErrorSA('Error en los datos de entrada', 'No se pudo cargar el plan de entrega');
        } else {
            //ErrorSA('Datos encontrados', 'Detalle cargado');
            $("#txtIdentificador").val(datosParaEditar.header.p_identificador);
            $("#txtPeriodo").val(datosParaEditar.header.p_periodo);
            datosParaEditar.header.p_fecha_ejecucion = datosParaEditar.header.p_fecha_ejecucion != null ? datosParaEditar.header.p_fecha_ejecucion.toString().slice(0, datosParaEditar.header.p_fecha_ejecucion.toString().indexOf("T")) : "";
            $("#txtEjecucion").val(datosParaEditar.header.p_fecha_ejecucion);
            $("#txtDescripcion").val(datosParaEditar.header.p_descripcion);
            //p_activo: "1"
            $("#DropResponsable").val(datosParaEditar.header.p_tbl_contrato_servidor_resp_id);
            $("#DropTipoPlan").val($('select#DropTipoPlan option:contains(' + datosParaEditar.header.p_tipo_entrega + ')').val());
            //p_id: "403cb28a-015c-11ed-89ad-00155d1b3502"
            //p_tipo_entrega: "Fijo"
            llenarArreglosBD(datosParaEditar);
        }
        //JSON_PlanesEntrega = data;
        //setTimeout(function () {
        //    calcularCantidadesProductos();
        //}, 500);


    });

}

//-- inicio llenar arreglos desde datos

function llenarArreglosBD(datosBD) {

    for (var i = 0; i < datosBD.ubicacionesProductos.length; i++) {
        objUbicacionesXBD = UbicacionProductos;

        objUbicacionesXBD.tbl_ubicacion_id = datosBD.ubicacionesProductos[i].tbl_ubicacion_id;
        objUbicacionesXBD.EjecutorPorUbicacion = datosBD.ubicacionesProductos[i].ejecutorPorUbicacion;
        arregloJsonDiv = [];
        listaproductos = '';

        console.log(datosBD.ubicacionesProductos[i].productos);
        if (datosBD.ubicacionesProductos[i].productos.length > 0) {

            var IdElemento = CreateGuid();
            var IdEjecutor = datosBD.ubicacionesProductos[i].ejecutorPorUbicacion;
            var idUbicacion = datosBD.ubicacionesProductos[i].tbl_ubicacion_id;

            var registro_Ubicacion_Ejecutor = 'Registro_' + idUbicacion + '_' + IdEjecutor;

            var NombreUbicacion = $("#DropUbicaciones option[value='" + datosBD.ubicacionesProductos[i].tbl_ubicacion_id + "']").text();
            var NombreResponsable = datosBD.ubicacionesProductos[i].ejecutor_nombre;
            var Actividades = datosBD.ubicacionesProductos[0].productos[0] != null ? datosBD.ubicacionesProductos[i].productos[0].p_detalle_actividad : "Sin información";
            objUbicacionesXBD.productos = [];
            var t = $('#tbl_ubicaciones').DataTable();
            objUbicacionesXBD.p_id = IdElemento;
            console.log(objUbicacionesXBD);

            console.log(datosBD.ubicacionesProductos);
            for (var j = 0; j < datosBD.ubicacionesProductos[i].productos.length; j++) {
                //debugger;
                var ubicacionP = {
                    tbl_ubicacion_id: "00000000-0000-0000-0000-000000000000",
                    EjecutorPorUbicacion: "00000000-0000-0000-0000-000000000000",
                    productos: null
                };
                ubicacionP.tbl_ubicacion_id = objUbicacionesXBD.tbl_ubicacion_id;
                ubicacionP.EjecutorPorUbicacion = objUbicacionesXBD.EjecutorPorUbicacion;
                ubicacionP.p_id = objUbicacionesXBD.p_id;
                ubicacionP.productos = [datosBD.ubicacionesProductos[i].productos[j]];
                //objUbicacionesXBD.productos[0] = datosBD.ubicacionesProductos[i].productos[j];                        
                arregloJsonDiv.push(ubicacionP);
                //debugger;
                listaproductos = listaproductos + $("#DropProductos option[value='" + datosBD.ubicacionesProductos[i].productos[j].p_tbl_contrato_producto_id + "']").text() + ': ' + datosBD.ubicacionesProductos[i].productos[j].p_cantidad + '<br/>';

            }
            var ObjhtmlProductos = '<label style="display: none;" class="ArrayUbicacionProductos" id="ArregloProductos_' + IdElemento + '">' + JSON.stringify(arregloJsonDiv) + '</label>' + '<label id="ProductosUbicacion_' + IdElemento + '">' + listaproductos + '</label>';
            var Objhtmlboton = '<button class="btn btn-success" onclick="ModalAgregarProducto(\'' + idUbicacion + '\',\'' + IdEjecutor + '\',\'' + IdElemento + '\',\'' + NombreUbicacion + '\');" id="btnProductos_' + IdElemento + '" type="button">Editar productos</button>';

            t.row.add([

                IdElemento, ///se tomara como un id (GUID), para el momento de eliminar registro, se sumen todas las cantidades al conteo de productos
                registro_Ubicacion_Ejecutor, //Forma una llave entre Ubicacion y Ejecutor, para validar que no se repitan registros 
                '<p id="NombreUbicacion_' + IdElemento + '">' + NombreUbicacion + '</p>',
                '<p id="Ejecutor_' + IdElemento + '">' + NombreResponsable + '</p>',
                '<p id="Actividades_' + IdElemento + '">' + Actividades + '</p>',
                ObjhtmlProductos,
                Objhtmlboton,
                '<a class="btn btn-sm btn-info" title = "Editar ubicación" onclick="ModalEditarUbicacion(\'' + IdElemento + '\')" ><span class="glyphicon glyphicon-edit"></span></a> ' + ' <a class="btn btn-sm btn-danger eliminar"><span class="glyphicon glyphicon-trash"></span></a>'
            ]).draw();


            //var table = $('#tbl_ubicaciones').DataTable();
            //table.destroy();
            //$('#tbl_ubicaciones').DataTable({
            //    "language": {
            //        "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            //    },
            //    data: arreglo,
            //    columns: [
            //        { title: "ID" },
            //        { title: "ID2" },
            //        { title: "Ubicación" },
            //        { title: "Responsable " },
            //        { title: "Actividades " },
            //        { title: "Productos " },
            //        { title: "Asignar producto " },
            //        { title: "Acción " }
            //    ],

            //    columnDefs: [
            //        { "className": "dt-center", "targets": "_all" },
            //        { "width": "0%", "targets": 0 },
            //        { "width": "0%", "targets": 1 },
            //        { "width": "15%", "targets": 2 },
            //        { "width": "15%", "targets": 3 },
            //        { "width": "30%", "targets": 4 },
            //        { "width": "25%", "targets": 5 },
            //        { "width": "10%", "targets": 6 },
            //        { "width": "05%", "targets": 7 },
            //        {
            //            "targets": [0],
            //            "visible": false,
            //            "searchable": false
            //        },
            //        {
            //            "targets": [1],
            //            "visible": false,
            //            "searchable": false
            //        }
            //    ],


            //});

            $('#tbl_productos').DataTable().clear().draw();
            listaproductos = '';
            ArregloProductosNoGuardados = [];


        }

    }

}

function llenarArreglosBD2(datosBD) {

    for (var i = 0; i < datosBD.ubicacionesProductos.length; i++) {
        objUbicacionesXBD = UbicacionProductos;

        objUbicacionesXBD.tbl_ubicacion_id = datosBD.ubicacionesProductos[i].tbl_ubicacion_id;
        objUbicacionesXBD.EjecutorPorUbicacion = datosBD.ubicacionesProductos[i].ejecutorPorUbicacion;
        arregloJsonDiv = [];
        listaproductos = '';

        console.log(datosBD.ubicacionesProductos[i].productos);
        if (datosBD.ubicacionesProductos[i].productos.length > 0) {

            var IdElemento = CreateGuid();
            var IdEjecutor = datosBD.ubicacionesProductos[i].ejecutorPorUbicacion;
            var idUbicacion = datosBD.ubicacionesProductos[i].tbl_ubicacion_id;

            var registro_Ubicacion_Ejecutor = 'Registro_' + idUbicacion + '_' + IdEjecutor;

            var NombreUbicacion = $("#DropUbicaciones option[value='" + datosBD.ubicacionesProductos[i].tbl_ubicacion_id + "']").text();
            var NombreResponsable = datosBD.ubicacionesProductos[i].ejecutor_nombre;
            var Actividades = datosBD.ubicacionesProductos[0].productos[0] != null ? datosBD.ubicacionesProductos[i].productos[0].p_detalle_actividad : "Sin información";
            objUbicacionesXBD.productos = [];
            var t = $('#tbl_ubicaciones').DataTable();
            objUbicacionesXBD.p_id = IdElemento;
            console.log(objUbicacionesXBD);
            t.row.add([

                IdElemento, ///se tomara como un id (GUID), para el momento de eliminar registro, se sumen todas las cantidades al conteo de productos
                registro_Ubicacion_Ejecutor, //Forma una llave entre Ubicacion y Ejecutor, para validar que no se repitan registros 
                '<p id="NombreUbicacion_' + IdElemento + '">' + NombreUbicacion + '</p>',
                '<p id="Ejecutor_' + IdElemento + '">' + NombreResponsable + '</p>',
                '<p id="Actividades_' + IdElemento + '">' + Actividades + '</p>',
                '<label style="display: none;" class="ArrayUbicacionProductos" id="ArregloProductos_' + IdElemento + '">' + JSON.stringify(objUbicacionesXBD) + '</label>' + '<label id="ProductosUbicacion_' + IdElemento + '"></label>',
                '<button class="btn btn-primary" onclick="ModalAgregarProducto(\'' + idUbicacion + '\',\'' + IdEjecutor + '\',\'' + IdElemento + '\',\'' + NombreUbicacion + '\');" id="btnProductos_' + IdElemento + '" type="button">Asignar productos</button>',
                '<a class="btn btn-sm btn-info" title = "Editar ubicación" onclick="ModalEditarUbicacion(\'' + IdElemento + '\')" ><span class="glyphicon glyphicon-edit"></span></a> ' + ' <a class="btn btn-sm btn-danger eliminar"><span class="glyphicon glyphicon-trash"></span></a>'
            ]).draw(false);
            console.log(datosBD.ubicacionesProductos);
            for (var j = 0; j < datosBD.ubicacionesProductos[i].productos.length; j++) {
                //debugger;
                var ubicacionP = {
                    tbl_ubicacion_id: "00000000-0000-0000-0000-000000000000",
                    EjecutorPorUbicacion: "00000000-0000-0000-0000-000000000000",
                    productos: null
                };
                ubicacionP.tbl_ubicacion_id = objUbicacionesXBD.tbl_ubicacion_id;
                ubicacionP.EjecutorPorUbicacion = objUbicacionesXBD.EjecutorPorUbicacion;
                ubicacionP.p_id = objUbicacionesXBD.p_id;
                ubicacionP.productos = [datosBD.ubicacionesProductos[i].productos[j]];
                //objUbicacionesXBD.productos[0] = datosBD.ubicacionesProductos[i].productos[j];                        
                arregloJsonDiv.push(ubicacionP);
                //debugger;
                listaproductos = listaproductos + $("#DropProductos option[value='" + datosBD.ubicacionesProductos[i].productos[j].p_tbl_contrato_producto_id + "']").text() + ': ' + datosBD.ubicacionesProductos[i].productos[j].p_cantidad + '<br/>';

            }
            var ObjhtmlProductos = '<label style="display: none;" class="ArrayUbicacionProductos" id="ArregloProductos_' + IdElemento + '">' + JSON.stringify(arregloJsonDiv) + '</label>' + '<label id="ProductosUbicacion_' + IdElemento + '">' + listaproductos + '</label>';
            var Objhtmlboton = '<button class="btn btn-success" onclick="ModalAgregarProducto(\'' + idUbicacion + '\',\'' + IdEjecutor + '\',\'' + IdElemento + '\',\'' + NombreUbicacion + '\');" id="btnProductos_' + IdElemento + '" type="button">Editar productos</button>';

            var arreglo = $('#tbl_ubicaciones').DataTable().data().toArray();

            for (var h = 0; h < arreglo.length; h++) {
                if (arreglo[h][0] == IdElemento) {
                    arreglo[h][5] = ObjhtmlProductos; ///5 contiene el label de los productos agregados
                    arreglo[h][6] = Objhtmlboton; ///contiene el boton de Agregar/editar
                }

            }

            var table = $('#tbl_ubicaciones').DataTable();
            table.destroy();
            $('#tbl_ubicaciones').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
                },
                data: arreglo,
                columns: [
                    { title: "ID" },
                    { title: "ID2" },
                    { title: "Ubicación" },
                    { title: "Responsable " },
                    { title: "Actividades " },
                    { title: "Productos " },
                    { title: "Asignar producto " },
                    { title: "Acción " }
                ],

                columnDefs: [
                    { "className": "dt-center", "targets": "_all" },
                    { "width": "0%", "targets": 0 },
                    { "width": "0%", "targets": 1 },
                    { "width": "15%", "targets": 2 },
                    { "width": "15%", "targets": 3 },
                    { "width": "30%", "targets": 4 },
                    { "width": "25%", "targets": 5 },
                    { "width": "10%", "targets": 6 },
                    { "width": "05%", "targets": 7 },
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [1],
                        "visible": false,
                        "searchable": false
                    }
                ],


            });

            $('#tbl_productos').DataTable().clear().draw();
            listaproductos = '';
            ArregloProductosNoGuardados = [];


        }

    }

}

//-- fin llenar arreglos desde datos



function calcularCantidadesProductos() {

    //utilizar cuando ya este el microservio que obtiene todos los planes del contrato
    for (var i = 0; i < JSON_Productos.length; i++) {


        for (var j = 0; j < JSON_PlanesEntrega.length; j++) {

            for (var k = 0; k < JSON_PlanesEntrega[j].ubicacionesProductos.length; k++) {

                for (var l = 0; l < JSON_PlanesEntrega[j].ubicacionesProductos[k].productos.length; l++) {

                    if (JSON_PlanesEntrega[j].ubicacionesProductos[k].productos[l].p_tbl_contrato_producto_id == JSON_Productos[i].tbl_contrato_producto_id) {
                        JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) - parseInt(JSON_PlanesEntrega[j].ubicacionesProductos[k].productos[l].p_cantidad)
                    }
                }
            }
        }
    }

    //for (var i = 0; i < JSON_Productos.length; i++) {




    //        for (var k = 0; k < JSON_PlanesEntrega.ubicacionesProductos.length; k++) {

    //            for (var l = 0; l < JSON_PlanesEntrega.ubicacionesProductos[k].productos.length; l++) {

    //                if (JSON_PlanesEntrega.ubicacionesProductos[k].productos[l].tbl_contrato_producto_id == JSON_Productos[i].id) {
    //                    JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) - parseInt(JSON_PlanesEntrega.ubicacionesProductos[k].productos[l].cantidad)
    //                }

    //            }

    //        }


    //}


    console.log('Se actualizaron las cantidades de productos: ' + JSON_Productos);

}


//////////LLENADO DE DROPDOWNS

function getDropProductos_PE() {
    var idContrato = $("#idContrato").val();

    $.get($("#EndPointAC").val() + "ProdServ/List/Contrato/" + idContrato, function (data, status) {
        JSON_Productos = data;

        var Body = "<option value='' selected>Selecciona una opción</option>";
        var temp = JSON.stringify(data);
        temp = temp.toLowerCase();
        var SortObject = JSON.parse(temp);
        SortObject = SortObject.sort(function (a, b) {
            if (a.elemento > b.elemento) {
                return 1;
            }
            if (a.elemento < b.elemento) {
                return -1;
            }
            return 0;
        });


        for (var i = 0; i <= SortObject.length - 1; i++) {
            Body = Body + "<option value='" + SortObject[i].tbl_contrato_producto_id + "'>" + SortObject[i].elemento.charAt(0).toUpperCase() + SortObject[i].elemento.slice(1) + "</option>";
        }
        ProductosPlanL = SortObject;

        $('#DropProductos').html(Body);
        $('#DropProductosGlobal').html(Body);

    }, 'json');

    //$.get("/Request/ProductoServicio/Get/Contrato/Drop/" + idContrato, function (data, status) {

    //    var Body = "<option value='0' selected>Selecciona una opción</option>";
    //    var temp = JSON.stringify(data);
    //    temp = temp.toLowerCase();
    //    var SortObject = JSON.parse(temp);
    //    SortObject = SortObject.sort(function (a, b) {
    //        if (a.value > b.value) {
    //            return 1;
    //        }
    //        if (a.value < b.value) {
    //            return -1;
    //        }
    //        return 0;
    //    });


    //    for (var i = 0; i <= SortObject.length - 1; i++) {
    //        Body = Body + "<option value='" + SortObject[i].key + "'>" + SortObject[i].value.charAt(0).toUpperCase() + SortObject[i].value.slice(1) + "</option>";
    //    }
    //    $('#DropProductos').html(Body);
    //    $('#DropProductosGlobal').html(Body);
    //    //Se hace el calculo de cantidades ya que se cargó JSON_Productos

    //}, 'json');
}

function getDropUbicaciones_PE() {
    //se necesita el id d dependencia que viene de sesion
    var idDependencia = $("#HDidDependencia").val();

    $.get($("#EndPointAC").val() + "UbicacionesCatalog/Get/dependencia/" + idDependencia, function (data, status) {
        JSON_Ubicaciones = data;
        var Body = "<option value='0' selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].unidad + "</option>";
        }
        $('#DropUbicaciones').html(Body);

    }, 'json');
}


function GetDropResponsables_PE() {
    var idcon = $('#idContrato').val();
    $.get($("#EndPointAC").val() + "SerServidorPublico/Get/sigla/EPE/Contrato/" + idcon, function (data, status) {
        var Body = "<option value='0' selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].tbl_contrato_servidor_resp_id + "'>" + data[i].nombrecompleto + "</option>";
        }
        $('#DropResponsable').html(Body);
    }, 'json');
}

formatNumber = (e) => e.replace(/\D/g, "")
    .replace(/([0-9])([0-9]{2})$/, '$1.$2')
    .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");

$('#txtCantidad').keyup(() => {
    const producto = JSON_Productos.find(p => p.tbl_contrato_producto_id == $('#DropProductos').val());
    const cantidadMaxima = producto.cantidad_maxima;
    const cantidad = parseFloat($('#txtCantidad').val());

    if (cantidad <= cantidadMaxima) {
        const valorUnitario = producto.unitario;
        const monto = valorUnitario * cantidad;
        $("#Monto").val(formatNumber(monto.toFixed(2).toString()));

    }
    else {
        const valorUnitario = producto.unitario;
        const monto = valorUnitario * cantidad;
        $('#txtCantidad').val(formatNumber(cantidadMaxima.toFixed(2)));
        $("#Monto").val(formatNumber(monto.toFixed(2).toString()));
    }

    let monto = parseInt($('#Monto').val().replace(/,/g, ""));
    let montoIVA = parseFloat($('#IVA').val());
    let resultado = parseFloat(monto * montoIVA / 100);
    $('#MontoIVA').val(resultado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
    $('#Total').val(0);

    monto = parseFloat($('#Monto').val().replace(/,/g, ""));
    montoIVA = parseFloat($('#MontoIVA').val().replace(/,/g, ""));
    resultado = monto + montoIVA;

    $('#Total').val(resultado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));


});

//////////EVENTOS DE DROPDOWNS
//Obtiene los ejecutores al cambiar de ubicacion
$('#DropProductos').change(function () {

    $('#Monto').val('');
    $('#MontoIVA').val('');
    $('#Total').val('');
    $('#txtCantidad').val('');
    $('#txtCantidad').prop("disabled", !$('#DropProductos').val());


    $('.listado-obligaciones').html('');

    for (var i = 0; i <= JSON_Productos.length - 1; i++) {

        if (JSON_Productos[i].tbl_contrato_producto_id === $('#DropProductos').val()) {

            $('#txtClaveProducto').val(JSON_Productos[i].clave_producto);
            $('#txtCantidadTotal').val(JSON_Productos[i].cantidad_maxima);
        }
    }

    var producto = $('#DropProductos').val();
    console.log('listado de productos para buscar');
    console.log(ProductosPlanL);
    var idP = ProductosPlanL.findIndex(x => x.tbl_contrato_producto_id == producto);
    if (idP != -1) {
        $.get($("#EndPointAC").val() + "SerObligacion/ObligacionesxProducto/Contrato/" + $('#idContrato').val() + "/Producto/" + ProductosPlanL[idP].id, function (data, status) {

            var htmlbody = "<table class='table'><thead>" +
                "<tr>" +
                "<th scope='col'> Cláusula </th>" +
                "<th scope='col'>Nombre de la obligación</th>" +
                "<th scope='col'>Tipo de prioridad</th>" +
                "<th scope='col'>Tipo de obligación</th></tr></thead><tbody>";
            var Lista = data;
            console.log(Lista);
            for (var i = 0; i <= Lista.length - 1; i++) {
                var prioridad = Lista[i].tbl_tipo_prioridad_nombre == null ? 'Sin prioridad' : Lista[i].tbl_tipo_prioridad_nombre;
                var body_int = "<tr>" +
                    "<td>" + Lista[i].clausula + "</td>" +
                    "<td>" + Lista[i].obligacion + "</td>" +
                    "<td>" + Lista[i].tbl_tipo_prioridad_nombre + "</td>" +
                    "<td>" + Lista[i].tipo_obligacion + "</td></tr>"
                    ;
                htmlbody = htmlbody + body_int;
            }
            htmlbody = htmlbody + "</tbody></table>";
            $('.listado-obligaciones').html(htmlbody);
        });
    }
});


function GetDropEjecutores_PE() {
    var IdContrato = $('#idContrato').val();
    $.get($("#EndPointAC").val() + "/Operaciones/PE/Get/Ejecutores/Ubicacion/" + IdContrato, function (data, status) {
        listaEjecutores.push(data);
        console.log(listaEjecutores);
    }, 'json');
}


$('#DropUbicaciones').change(function () {

    var idUbicacion = $('#DropUbicaciones').val();

    $.get($("#EndPointAC").val() + "Operaciones/PE/Get/Ejecutores/Ubicacion/" + idUbicacion, function (data, status) {
        var Body = "<option value='0' selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.response.length - 1; i++) {
            Body = Body + "<option value='" + data.response[i].value + "'>" + data.response[i].text + "</option>";
        }
        $('#DropEjecutores').html(Body);

    }, 'json');

});


//////EVENTOS CLICK

function Recargar() {
    location.reload();
}

function deshabilitarBotonGuardado() {
    $("#btnGuardarPlanEntrega").prop("disabled", true);
}
function habilitarBotonGuardado() {
    $("#btnGuardarPlanEntrega").prop("disabled", false);
}


$('#btnGuardarPlanEntrega').click(function () {

    deshabilitarBotonGuardado();
    var Validacion = ValidarPlanEntrega();
    var t = $('#tbl_ubicaciones').DataTable().data().toArray();
    console.log(t.length);

    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);
        habilitarBotonGuardado();
    } else if (t.length == 0) {

        ErrorSA('Error en los datos de entrada', 'No se han cargado ubicaciones');
        habilitarBotonGuardado();
    } else {


        ///Aqui debe ir una validacion

        var d = new Date();
        var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());


        ///NUEVO
        var objHeaderPlan = sp_plan_entrega_input;
        objHeaderPlan.p_id = $("#idPlan").val(); //Se obtiene de la variable global
        objHeaderPlan.p_identificador = $('#txtIdentificador').val();
        objHeaderPlan.p_periodo = $('#txtPeriodo').val();
        objHeaderPlan.p_descripcion = $('#txtDescripcion').val();
        objHeaderPlan.p_tbl_contrato_servidor_resp_id = $('#DropResponsable').val();
        objHeaderPlan.p_fecha_ejecucion = $('#txtEjecucion').val();
        objHeaderPlan.Inclusion = date;
        objHeaderPlan.Estatus = 1;
        objHeaderPlan.p_activo = 1;
        objHeaderPlan.p_tipo_entrega = $('#DropTipoPlan option:selected').text();




        ////



        var listaobjJSON = [];
        $(".ArrayUbicacionProductos").each(function (index) {
            //Se quitan los corchetes para poder concatenar varios objetos.

            var objJSON = JSON.parse($(this).html());
            listaobjJSON.push(objJSON);

        });

        //se agregan corchetes al inicio y al final para poder convertir a JSON
        console.log(listaobjJSON);


        var listaUbicacionProductos = [];


        //lista_idUbicaciones contiene los ids de las ubicaciones encontradas
        var lista_idUbicaciones = [];

        for (var i = 0; i < listaobjJSON.length; i++) {

            var obj_Ubi_Ej = UbicacionEjecutor;
            obj_Ubi_Ej.idUbicacion = listaobjJSON[i][0].tbl_ubicacion_id;
            obj_Ubi_Ej.idEjecutor = listaobjJSON[i][0].EjecutorPorUbicacion;
            var obj = JSON.stringify(obj_Ubi_Ej);
            lista_idUbicaciones.push(JSON.parse(obj));


            //if (i == 0) {
            //    var obj_Ubi_Ej = UbicacionEjecutor;
            //    obj_Ubi_Ej.idUbicacion = listaobjJSON[i][0].tbl_ubicacion_id;
            //    obj_Ubi_Ej.idEjecutor = listaobjJSON[i][0].EjecutorPorUbicacion;
            //    var obj = JSON.stringify(obj_Ubi_Ej);
            //    lista_idUbicaciones.push(JSON.parse(obj));
            //}

            //for (var j = 0; j < lista_idUbicaciones.length; j++) {

            //    if (((lista_idUbicaciones[j].idUbicacion == listaobjJSON[i][0].tbl_ubicacion_id) || (lista_idUbicaciones[j].idUbicacion != listaobjJSON[i][0].tbl_ubicacion_id)) && (lista_idUbicaciones[j].idEjecutor != listaobjJSON[i][0].EjecutorPorUbicacion)) {



            //        var obj_Ubi_Ej = UbicacionEjecutor;
            //        var ubicacion = listaobjJSON[i][0].tbl_ubicacion_id;
            //        var ejecutor = listaobjJSON[i][0].EjecutorPorUbicacion;
            //        obj_Ubi_Ej.idUbicacion = ubicacion;
            //        obj_Ubi_Ej.idEjecutor = ejecutor;
            //        var obj = JSON.stringify(obj_Ubi_Ej);
            //        lista_idUbicaciones.push(JSON.parse(obj));

            //    }

            //}

        }

        console.log(lista_idUbicaciones);


        var listaProductos = [];

        for (var i = 0; i < lista_idUbicaciones.length; i++) {

            var ubicacion = UbicacionProductos;
            ubicacion.tbl_ubicacion_id = lista_idUbicaciones[i].idUbicacion;
            ubicacion.EjecutorPorUbicacion = lista_idUbicaciones[i].idEjecutor;
            ubicacion.productos = [];
            var obj_ubi = JSON.stringify(ubicacion);
            listaUbicacionProductos.push(JSON.parse(obj_ubi));

        }

        console.log(listaUbicacionProductos);

        //Se recorre el json de todos los productos y se inserta por ubicacion en otro json
        //Se recorre el json de todos los productos y se inserta por ubicacion en otro json


        for (var j = 0; j < listaUbicacionProductos.length; j++) {

            if ((listaobjJSON[j][0].tbl_ubicacion_id == listaUbicacionProductos[j].tbl_ubicacion_id) && (listaobjJSON[j][0].EjecutorPorUbicacion == listaUbicacionProductos[j].EjecutorPorUbicacion)) {

                for (var k = 0; k < listaobjJSON[j].length; k++) {
                    listaUbicacionProductos[j].productos.push(listaobjJSON[j][k].productos[0]);
                }

            }
        }

        console.log("Lista de ubicaciones y productos");
        console.log(listaUbicacionProductos);



        //Se sobreescribe el JSON por si se modifico la informacion de detalle durante el proceso
        for (var i = 0; i < listaUbicacionProductos.length; i++) {

            for (var j = 0; j < listaUbicacionProductos[i].productos.length; j++) {

                //listaUbicacionProductos[i].tbl_RespApego_Contrato_ac_id = $('#DropResponsable').val(); Pendiente de ver como se llamara el responsable
                listaUbicacionProductos[i].productos[j].p_tipo = $('#DropTipoPlan option:selected').text();
            }
        }

        var objPlanGeneral = EstructuraPalnEntrega;

        objPlanGeneral.Header = objHeaderPlan;
        objPlanGeneral.UbicacionesProductos = listaUbicacionProductos;


        console.log('Cadena que se envia al microservicio' + JSON.stringify(objPlanGeneral));

        debugger;
        //cambiar por el endpoint de guardar
        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(objPlanGeneral),
            type: 'post',
            success: function (data) {
                //var objresponse = JSON.parse(data);

                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                //EnviarCorreoProveedor();
                deshabilitarBotonGuardado();

            },
            error: function (data) {

                ErrorSA('', 'No se pudo realizar el registro');
                habilitarBotonGuardado();
            },
            processData: false,
            type: 'POST',
            url: $("#EndPointAC").val() + "Operaciones/PE/modify"
            //url: 'https://localhost:44359/PlanEntrega/add'            

        });
    }
})

$('#AgregarUbicacion').click(function () {

    //if (($('#DropUbicaciones').val() == null) || ($('#DropEjecutores').val() == null)) {
    //    return;
    //}

    var Validacion = ValidarUbicacion();

    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);

    } else {



        var ModoEditar = $('#ModoEditar').val();
        var ElementoEditar = $('#HD1_idElemento').val();
        var IdElemento = CreateGuid();
        var NombreUbicacion = $('#DropUbicaciones option:selected').text();
        var NombreResponsable = $('#DropEjecutores option:selected').text();
        var Actividades = $('#txtActividades').val();
        var idContrato = $('#idContrato').val();
        var idUbicacion = $('#DropUbicaciones').val();
        var responsableCumplimiento = $('#DropResponsable').val();
        var TipoPlan = $('#DropTipoPlan option:selected').text();
        var IdEjecutor = $('#DropEjecutores').val();
        var registro_Ubicacion_Ejecutor = 'Registro_' + idUbicacion + '_' + IdEjecutor;



        if (ModoEditar == '1') { // validacion cuando se esta editando


            //INICIO - Para validar elementos existente
            var objBusqueda = $('#tbl_ubicaciones').DataTable().data().toArray();

            var index = null;

            for (var indice in objBusqueda) {
                if (objBusqueda[indice][0] == ElementoEditar) //la columna [0] tiene el Guid y la columna [1] Contiene la llave formada por la ubicacion y el ejecutor
                {
                    index = indice;
                }
            }

            objBusqueda.splice(index, 1); // se elimina de virtualmente de un arreglo, el registro que se esta editando para validar si no se esta duplicando otro registro

            //Se recorre el arreglo nuevamente para descartar que se este repitiendo otro registro formado por la llave de la ubicacion y el ejecutor
            if (objBusqueda.length > 0) {
                for (var indice in objBusqueda) {

                    if (objBusqueda[indice][1] == registro_Ubicacion_Ejecutor) { //[1] es la posicion del elemento combinado idubicacion con idejecutor, y sirve para validar si ya existe o no
                        ErrorSA('', 'Ya se registró la ubicación y el responsable anteriormente');
                        return;
                    }
                }
            }
            //FIN - Para validar elementos existente


            //Antes de eliminar la tabla, se necesita obtener el JSON para poder sobreescribirlo con la informacion nueva
            var JSON_ProductosUbicacion = JSON.parse($('#' + 'ArregloProductos_' + ElementoEditar).html());

            var banderaProductovacio = null;


            if (JSON_ProductosUbicacion.length == undefined) { //solo hay un registro en el JSON
                JSON_ProductosUbicacion.tbl_ubicacion_id = idUbicacion;   //id ubicacion
                JSON_ProductosUbicacion.EjecutorPorUbicacion = IdEjecutor;  //id ejecutor
                JSON_ProductosUbicacion.productos[0].p_detalle_actividad = Actividades;    //Actividades

                if (JSON_ProductosUbicacion.productos[0].tbl_contrato_producto_id == null) {
                    banderaProductovacio = true;
                }

            } else {  //trae varios registros el JSON

                //Se necesita sobreescribir el JSON de  los productos por que hay que actualizar los cambios
                for (var i = 0; i < JSON_ProductosUbicacion.length; i++) {

                    JSON_ProductosUbicacion[i].tbl_ubicacion_id = idUbicacion;   //id ubicacion
                    JSON_ProductosUbicacion[i].EjecutorPorUbicacion = IdEjecutor;  //id ejecutor
                    JSON_ProductosUbicacion[i].productos[0].p_detalle_actividad = Actividades;    //Actividades
                }
            }

            //$('#' + 'ArregloProductos_' + ElementoEditar).val(JSON.stringify(JSON_ProductosUbicacion));





            ////INICIO - sustitucion de campos en el arreglo de la tabla y se vuelve a generar tabla

            var arreglo = $('#tbl_ubicaciones').DataTable().data().toArray();

            //Son los campos que se pueden editar, se necesita meterlos nuevamente en el arreglo por que genera problemas en el responsivo

            var ObjhtmlNombreUbicacion = '<p id="NombreUbicacion_' + ElementoEditar + '">' + NombreUbicacion + '</p>';
            var ObjhtmlEjecutor = '<p id="Ejecutor_' + ElementoEditar + '">' + NombreResponsable + '</p>';
            var ObjhtmlActividades = '<p id="Actividades_' + ElementoEditar + '">' + Actividades + '</p>';
            var ObjhtmlLabelProductos = '<label style="display: none;" class="ArrayUbicacionProductos" id="ArregloProductos_' + ElementoEditar + '">' + JSON.stringify(JSON_ProductosUbicacion) + '</label>' + '<label id="ProductosUbicacion_' + ElementoEditar + '">' + $('#' + 'ProductosUbicacion_' + ElementoEditar).html() + '</label>';

            if (banderaProductovacio) {
                var Objhtmlbtn = '<button class="btn btn-primary" onclick="ModalAgregarProducto(\'' + idUbicacion + '\',\'' + IdEjecutor + '\',\'' + ElementoEditar + '\',\'' + NombreUbicacion + '\');" id="btnProductos_' + ElementoEditar + '" type="button">Asignar productos</button>';
            } else {
                var Objhtmlbtn = '<button class="btn btn-success" onclick="ModalAgregarProducto(\'' + idUbicacion + '\',\'' + IdEjecutor + '\',\'' + ElementoEditar + '\',\'' + NombreUbicacion + '\');" id="btnProductos_' + ElementoEditar + '" type="button">Editar productos</button>';
            }



            for (var i = 0; i < arreglo.length; i++) {

                if (arreglo[i][0] == ElementoEditar) {

                    arreglo[i][1] = registro_Ubicacion_Ejecutor; ///1 contiene el elemento el id de arrayproductos, que sirve para validar si existe campo
                    arreglo[i][2] = ObjhtmlNombreUbicacion; ///2 contiene el p del Nombre Ubicacion
                    arreglo[i][3] = ObjhtmlEjecutor; ///3 contiene el p del Ejecutor
                    arreglo[i][4] = ObjhtmlActividades; ///4 contiene el p de Actividades
                    arreglo[i][5] = ObjhtmlLabelProductos; ///5 contiene los labels de productos
                    arreglo[i][6] = Objhtmlbtn; ///5 contiene los labels de productos

                }


            }



            ///Se rehace la tabla porque al obtener el arreglo de la misma, no se muestran todos los elementos y genera problemas en el responsive
            var table = $('#tbl_ubicaciones').DataTable();


            table.destroy();

            $('#tbl_ubicaciones').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
                },
                data: arreglo,
                columns: [
                    { title: "ID" },
                    { title: "ID2" },
                    { title: "Ubicación" },
                    { title: "Responsable " },
                    { title: "Actividades " },
                    { title: "Productos " },
                    { title: "Asignar producto " },
                    { title: "Acción " }
                ],

                columnDefs: [
                    { "className": "dt-center", "targets": "_all" },
                    { "width": "0%", "targets": 0 },
                    { "width": "0%", "targets": 1 },
                    { "width": "15%", "targets": 2 },
                    { "width": "15%", "targets": 3 },
                    { "width": "30%", "targets": 4 },
                    { "width": "25%", "targets": 5 },
                    { "width": "10%", "targets": 6 },
                    { "width": "05%", "targets": 7 },
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [1],
                        "visible": false,
                        "searchable": false
                    }
                ],


            });

            /////FIN - sustitucion de campos



            function Confirmacion() {
                return CerrarModalUbicacion();
            }
            var AccionSi = eval(Confirmacion);

            SuccessSAAction("Operación exitosa", "El registro se actualizó correctamente", AccionSi);



        } else {  //validacion cuando es registro nuevo

            var objBusqueda = $('#tbl_ubicaciones').DataTable().data().toArray();

            for (var indice in objBusqueda) {

                if (objBusqueda[indice][1] == registro_Ubicacion_Ejecutor) { //[1] es la posicion del elemento combinado idubicacion con idejecutor, y sirve para validar si ya existe o no
                    ErrorSA('', 'Ya se registró la ubicación y el responsable anteriormente');
                    return;
                }

            }


            var d = new Date();
            var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());


            //////////////////////////////////////CODIGO NUEVO OBJETO

            var objProductos = sp_plan_entrega_producto;

            objProductos.p_opt = 2,
                objProductos.p_id = IdElemento,
                objProductos.p_tbl_contrato_producto_id = null,
                objProductos.p_tbl_ubicacion_plan_entrega_id = "00000000-0000-0000-0000-000000000000",
                objProductos.p_estatus = 1,
                objProductos.p_cantidad = null,
                objProductos.p_detalle_actividad = Actividades,
                objProductos.p_tipo = TipoPlan


            var ListaobjProductos = [];

            ListaobjProductos.push(objProductos);

            //Se llena el objeto que lleva las ubicaciones
            var objUbicaciones = UbicacionProductos;
            objUbicaciones.tbl_ubicacion_id = idUbicacion;
            objUbicaciones.EjecutorPorUbicacion = IdEjecutor;
            objUbicaciones.productos = ListaobjProductos;



            ////////////////////////////////////////////////







            var t = $('#tbl_ubicaciones').DataTable();

            //'ArregloProductos_' + IdElemento;

            t.row.add([

                IdElemento, ///se tomara como un id (GUID), para el momento de eliminar registro, se sumen todas las cantidades al conteo de productos
                registro_Ubicacion_Ejecutor, //Forma una llave entre Ubicacion y Ejecutor, para validar que no se repitan registros 
                '<p id="NombreUbicacion_' + IdElemento + '">' + NombreUbicacion + '</p>',
                '<p id="Ejecutor_' + IdElemento + '">' + NombreResponsable + '</p>',
                '<p id="Actividades_' + IdElemento + '">' + Actividades + '</p>',
                '<label style="display: none;" class="ArrayUbicacionProductos" id="ArregloProductos_' + IdElemento + '">' + JSON.stringify(objUbicaciones) + '</label>' + '<label id="ProductosUbicacion_' + IdElemento + '"></label>',
                '<button class="btn btn-primary" onclick="ModalAgregarProducto(\'' + idUbicacion + '\',\'' + IdEjecutor + '\',\'' + IdElemento + '\',\'' + NombreUbicacion + '\');" id="btnProductos_' + IdElemento + '" type="button">Asignar productos</button>',
                '<a class="btn btn-sm btn-info" title = "Editar ubicación" onclick="ModalEditarUbicacion(\'' + IdElemento + '\')" ><span class="glyphicon glyphicon-edit"></span></a> ' + ' <a class="btn btn-sm btn-danger eliminar"><span class="glyphicon glyphicon-trash"></span></a>'
            ]).draw(false);

            console.log(objUbicaciones);
            //Se agrega el json inicial de la ubicacion en el hidden dentro de la fila correspondiente
            //$('#' + 'ArregloProductos_' + IdElemento).val(JSON.stringify(objPlanEntrega));

            LimpiarFormUbicacion();
            CerrarModalUbicacion();

        }



    }
})



$('#AgregarProducto').click(function () {

    var Validacion = ValidarProducto();

    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);

    } else {

        if (parseInt($('#txtCantidadTotal').val()) < parseInt($('#txtCantidad').val())) {
            ErrorSA('Error', 'No se pudo realizar el registro ya que la cantidad es mayor a la cantidad actual');
            return;
        }





        var idUbicacion = $('#HD_idUbicacion').val();
        var idEjecutor = $('#HD_idEjecutor').val();
        var IdElemento = $('#HD_idElemento').val();



        var idContrato = $('#idContrato').val();
        var nombreProducto = $('#DropProductos option:selected').text();
        var idProducto = $('#DropProductos').val();
        var cantidadProducto = $('#txtCantidad').val();
        var responsableCumplimiento = $('#DropResponsable').val();
        var TipoPlan = $('#DropTipoPlan option:selected').text();

        ///Se valida si no se ha ingresado el producto
        var objBusqueda = $('#tbl_productos').DataTable().data().toArray();

        for (var indice in objBusqueda) {

            if (objBusqueda[indice][4] == idProducto) { //[4] es el id del producto
                ErrorSA('Error', 'El producto ya se registró anteriormente');
                return;
            }

        }



        //se obtiene la cadena que trae el objeto planEntrega, inicializado en la tabla de ubicaciones
        //var cadenaobjPlanEntrega = $('#' + 'ArregloProductos_' + IdElemento).val();
        var cadenaobjPlanEntrega = $('#' + 'ArregloProductos_' + IdElemento).html();


        ///Se agregan los productos que se fueron añadiendo pero al final no se guardan en la ubicacion
        var ArrayProdNoGuardado = [];

        ArrayProdNoGuardado.push(idProducto);
        ArrayProdNoGuardado.push(cantidadProducto);
        //--montos --//
        var nMonto = $('#Monto').val();
        var nIva = $('#MontoIVA').val();
        var nTotal = $('#Total').val();

        ArrayProdNoGuardado.push(nMonto);
        ArrayProdNoGuardado.push(nIva);
        ArrayProdNoGuardado.push(nTotal);

        //-- fin montos --//
        ArregloProductosNoGuardados.push(ArrayProdNoGuardado);

        console.log(ArregloProductosNoGuardados);
        //

        //Se actualiza la cantidad de productos
        for (var i = 0; i < JSON_Productos.length; i++) {
            if (JSON_Productos[i].tbl_contrato_producto_id == idProducto) {
                JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) - parseInt(cantidadProducto);
            }
        }

        //Se convierte la cadena a objeto 
        var objPlanEntrega = JSON.parse(cadenaobjPlanEntrega);
        console.log(objPlanEntrega);
        debugger;

        ///////////////////

        if (objPlanEntrega.length == undefined) {

            ///Se llena el objeto de planEntrega por ubicacion al que le hacen falta los productos

            objPlanEntrega.productos[0].p_tbl_contrato_producto_id = idProducto;
            objPlanEntrega.productos[0].p_cantidad = cantidadProducto;

            //-- montos --//
            objPlanEntrega.productos[0].p_monto = nMonto;
            objPlanEntrega.productos[0].p_monto_iva = nIva;
            objPlanEntrega.productos[0].p_total = nTotal;

            //-- fin montos --//

            //Se vuelve a convertir en cadena
            var CadenaJSON_Producto = JSON.stringify(objPlanEntrega);

        } else {

            ///Se llena el objeto de planEntrega por ubicacion al que le hacen falta los productos

            objPlanEntrega[0].productos[0].p_tbl_contrato_producto_id = idProducto;
            objPlanEntrega[0].productos[0].p_cantidad = cantidadProducto;

            //-- montos --//
            objPlanEntrega[0].productos[0].p_monto = nMonto;
            objPlanEntrega[0].productos[0].p_monto_iva = nIva;
            objPlanEntrega[0].productos[0].p_total = nTotal;

            //-- fin montos --//

            //Se vuelve a convertir en cadena
            var CadenaJSON_Producto = JSON.stringify(objPlanEntrega[0]);
        }



        var t = $('#tbl_productos').DataTable();
        console.log("Eddy", CadenaJSON_Producto);
        t.row.add([
            CadenaJSON_Producto,
            nombreProducto,
            cantidadProducto,
            '<div align="center"><a class="btn btn-sm btn-danger eliminar">Eliminar</a><a class="btn btn-sm btn-primary ver">Ver</a></div>',
            idProducto
        ]).draw(false);


        LimpiarModalAgregarProducto();


    }
});


function LimpiarModalAgregarProducto() {
    $('#txtCantidad').prop("disabled", true);
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

function LimpiarFormUbicacion() {

    $('#DropUbicaciones').val('0');
    $('#DropEjecutores').val('0');
    $('#txtActividades').val('');
    $('#HD1_idElemento').val('');
    $('#ModoEditar').val('0');


}


$('#GuardarProductos').click(function () {


    var idUbicacion = $('#HD_idUbicacion').val();
    var idEjecutor = $('#HD_idEjecutor').val();
    var IdElemento = $('#HD_idElemento').val();
    var NombreUbicacion = $('#HD_nombreUbicacion').val();

    var Arreglo_ProductosUbicacion = $('#tbl_productos').DataTable().data().toArray();
    if (Arreglo_ProductosUbicacion.length == 0) {

        if (JSON_Eliminar_P_Temp.length > 0) {
            for (var i = 0; i < JSON_Productos.length; i++) {
                for (var z = 0; z <= JSON_Eliminar_P_Temp.length - 1; z++) {

                    if (JSON_Productos[i].tbl_contrato_producto_id == JSON_Eliminar_P_Temp[z].productos[0].p_tbl_contrato_producto_id) {
                        JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) - parseInt(JSON_Eliminar_P_Temp[z].productos[0].p_cantidad);
                    }
                }
                LimpiarModalAgregarProducto();
            }
            JSON_Eliminar_P = null;
            JSON_Eliminar_P_Temp = [];
        }
        ErrorSA('Error', "No se han agregado productos");
        //alert("No se han agregado productos");
    }
    else {

        JSON_Eliminar_P_Temp = [];
        //Obtener el arreglo de la tabla de productos

        var arrayProductos = $('#tbl_productos').DataTable().data().toArray();

        for (var indice in arrayProductos) {

            ///Se llena el arreglo con los objetos de la tabla productos

            ArregloProductos.push(JSON.parse(arrayProductos[indice][0]));
            listaproductos = listaproductos + arrayProductos[indice][1] + ': ' + arrayProductos[indice][2] + '<br/>';
        }


        /// $('#' + 'ProductosUbicacion_' + IdElemento).html(listaproductos); se sustituye por el test siguiente


        /////INICIO TEST LLENAR EL ELEMENTO ProductosUbicacion_ QUE CONTIENEN LOS NOMBRES DE LOS PRODUCTOS Y SUS CANTIDADES

        var arreglo = $('#tbl_ubicaciones').DataTable().data().toArray();

        //se generan los elementos html que se agregaran al arreglo de la tabla de ubicaciones, 



        var ObjhtmlProductos = '<label style="display: none;" class="ArrayUbicacionProductos" id="ArregloProductos_' + IdElemento + '">' + JSON.stringify(ArregloProductos) + '</label>' + '<label id="ProductosUbicacion_' + IdElemento + '">' + listaproductos + '</label>';
        var Objhtmlboton = '<button class="btn btn-success" onclick="ModalAgregarProducto(\'' + idUbicacion + '\',\'' + idEjecutor + '\',\'' + IdElemento + '\',\'' + NombreUbicacion + '\');" id="btnProductos_' + IdElemento + '" type="button">Editar productos</button>';
        for (var i = 0; i < arreglo.length; i++) {

            if (arreglo[i][0] == IdElemento) {
                arreglo[i][5] = ObjhtmlProductos; ///5 contiene el label de los productos agregados
                arreglo[i][6] = Objhtmlboton; ///contiene el boton de Agregar/editar
            }


        }


        ///Se rehace la tabla porque al obtener el arreglo de la misma, no se muestran todos los elementos y genera problemas en el responsive
        var table = $('#tbl_ubicaciones').DataTable();

        table.destroy();

        $('#tbl_ubicaciones').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: arreglo,
            columns: [
                { title: "ID" },
                { title: "ID2" },
                { title: "Ubicación" },
                { title: "Responsable " },
                { title: "Actividades " },
                { title: "Productos " },
                { title: "Asignar producto " },
                { title: "Acción " }
            ],

            columnDefs: [
                { "className": "dt-center", "targets": "_all" },
                { "width": "0%", "targets": 0 },
                { "width": "0%", "targets": 1 },
                { "width": "15%", "targets": 2 },
                { "width": "15%", "targets": 3 },
                { "width": "30%", "targets": 4 },
                { "width": "25%", "targets": 5 },
                { "width": "10%", "targets": 6 },
                { "width": "05%", "targets": 7 },
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [1],
                    "visible": false,
                    "searchable": false
                }
            ],


        });

        //Se agrega el arreglo 
        //$('#' + 'ArregloProductos_' + IdElemento).val(JSON.stringify(ArregloProductos));

        /////FIN TEST



        ArregloProductos = [];
        $('#tbl_productos').DataTable().clear().draw();


        listaproductos = '';

        //Se modifica el boton de agregar productos a editar ya que se hayan actualizado los datos
        //$('#' + 'btnProductos_' + IdElemento).text('Editar productos');
        //$('#' + 'btnProductos_' + IdElemento).removeClass('btn-primary');
        //$('#' + 'btnProductos_' + IdElemento).addClass('btn-success');

        ArregloProductosNoGuardados = [];
    }

    $('#DropProductos').val('0');
    $('#txtCantidad').val('');

    $('#ModalAgregarProducto').modal('hide');


})



function CerrarModalProducto() {
    $('.listado-obligaciones').html('');

    LimpiarModalAgregarProducto();
    //$('#tbl_productos').DataTable().clear().draw();
    $('#ModalAgregarProducto').modal('hide');

    ///Se recorre el arreglo que contiene la cantidad de los productos no guardados y se vuelven a sumar al JSON de Productos
    if (JSON_Eliminar_P_Temp.length <= 0) {
        for (var i = 0; i < JSON_Productos.length; i++) {

            for (var j = 0; j < ArregloProductosNoGuardados.length; j++) {

                if (ArregloProductosNoGuardados[j][0] == JSON_Productos[i].tbl_contrato_producto_id) {
                    JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) + parseInt(ArregloProductosNoGuardados[j][1]);

                }

            }

        }
        $('#tbl_productos').DataTable().clear().draw();
    }
    else {
        for (var i = 0; i < JSON_Productos.length; i++) {
            for (var z = 0; z <= JSON_Eliminar_P_Temp.length - 1; z++) {

                if (JSON_Productos[i].tbl_contrato_producto_id == JSON_Eliminar_P_Temp[z].productos[0].p_tbl_contrato_producto_id) {
                    JSON_Productos[i].cantidad_maxima = parseInt(JSON_Productos[i].cantidad_maxima) - parseInt(JSON_Eliminar_P_Temp[z].productos[0].p_cantidad);
                }
            }
            LimpiarModalAgregarProducto();
        }
        JSON_Eliminar_P = null;
        JSON_Eliminar_P_Temp = [];
    }
    ArregloProductosNoGuardados = [];

}




///FUNCION PARA CREAR GUID
function CreateGuid() {
    function _p8(s) {
        var p = (Math.random().toString(16) + "000000000").substr(2, 8);
        return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
    }
    return _p8() + _p8(true) + _p8(true) + _p8();
}
///////////////


//////////MODALES

function ModalAgregarProducto(idUbicacion, idEjecutor, IdElemento, Ubicacion) {
    GetInstanciaIVA();
    Redimension();
    $('#ModalAgregarProducto').modal('show');
    $('#HD_idUbicacion').val(idUbicacion);
    $('#HD_idEjecutor').val(idEjecutor);
    $('#HD_idElemento').val(IdElemento);
    $('#HD_nombreUbicacion').val(Ubicacion);
    $('#TitleModalProducto').html('Productos de la ubicación : ' + Ubicacion);

    LimpiarModalAgregarProducto();

    /////Se destruye la tabla y se vuelve generar con la informacion del JSON que viene de la tabla de ubicaciones

    var Arreglo_arreglos = [];

    //OBTENER EL JSON DE UBICACIONES

    //var JSON_Productos = JSON.parse($('#' + 'ArregloProductos_' + IdElemento).val());
    var JSON_Productos = JSON.parse($('#' + 'ArregloProductos_' + IdElemento).html());
    console.log(JSON_Productos);
    debugger;
    //Si no es undefined, entonces es por que ya trae productos cargados.
    if (JSON_Productos.length != undefined) {

        var Arreglo_arreglos = [];
        for (var i = 0; i <= JSON_Productos.length - 1; i++) {
            var Interno = [];

            Interno.push(JSON.stringify(JSON_Productos[i]));
            var con_prod = JSON_Productos[i].productos[0].p_tbl_contrato_producto_id;
            Interno.push($('#DropProductosGlobal').dropdown().find('option[value="' + con_prod + '"]').text()); //Nombre del Producto
            var cant = JSON_Productos[i].productos[0].p_cantidad;
            Interno.push(cant);

            Interno.push('<div align="center"><a class="btn btn-sm btn-danger eliminar">Eliminar</a><a class="btn btn-sm btn-primary ver">Ver</a></div>');
            var id_prod = JSON_Productos[i].productos[0].p_tbl_contrato_producto_id;
            Interno.push(id_prod);///Se llena el id del producto

            Arreglo_arreglos.push(Interno);
        }

        var table = $('#tbl_productos').DataTable();

        table.destroy();

        $('#tbl_productos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "JSON" },
                { title: "Producto" },
                { title: "Cantidad" },
                { title: "Acción " },
                { title: "ID " }
            ],

            columnDefs: [

                { "className": "dt-center", "targets": "_all" },
                { "width": "0%", "targets": 0 },
                { "width": "40%", "targets": 1 },
                { "width": "40%", "targets": 2 },
                { "width": "20%", "targets": 3 },
                { "width": "0%", "targets": 4 },

                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [4],
                    "visible": false,
                    "searchable": false
                }

            ]


        });

    }



}

function CleanCmb() {
    $("#tbl_productos tbody tr").each(function (index) {
        var campo1, campo2, campo3;
        $(this).children("td").each(function (index2) {
            switch (index2) {
                case 0:
                    campo1 = $(this).text();
                    break;
                case 1:
                    campo2 = $(this).text();
                    break;
                case 2:
                    campo3 = $(this).text();
                    break;
            }

        })
        console.log(campo1);
        jQuery("#DropProductos option:contains('" + campo1 + "')").remove();
    });
    //$('#DropProductos').html($('#DropProductos').html());
}

function ModalEditarUbicacion(IdElemento) {
    Redimension();
    $('#TitleModalUbicacion').html('Editar ubicación');
    $('#HD1_idElemento').val(IdElemento);
    $('#ModoEditar').val(1); //true 

    var IdUbicacion = null;
    var IdEjecutor = null;

    //se obtiene la cadena que trae el objeto planEntrega, inicializado en la tabla de ubicaciones
    //var cadenaobjPlanEntrega = $('#' + 'ArregloProductos_' + IdElemento).val();
    var cadenaobjPlanEntrega = $('#' + 'ArregloProductos_' + IdElemento).html();

    //Se convierte la cadena a objeto 
    var objPlanEntrega = JSON.parse(cadenaobjPlanEntrega);
    console.log(objPlanEntrega);
    if (objPlanEntrega.length == undefined) {

        IdUbicacion = objPlanEntrega.tbl_ubicacion_id;
        IdEjecutor = objPlanEntrega.EjecutorPorUbicacion;

    } else {

        ///Se llena el objeto de planEntrega por ubicacion al que le hacen falta los productos

        IdUbicacion = objPlanEntrega[0].tbl_ubicacion_id;
        IdEjecutor = objPlanEntrega[0].EjecutorPorUbicacion;
    }



    ///Se llenan los datos en el formulario
    $('#DropUbicaciones').val(IdUbicacion);
    //

    var idUbicacion = $('#DropUbicaciones').val();

    $.get($("#EndPointAC").val() + "Operaciones/PE/Get/Ejecutores/Ubicacion/" + idUbicacion, function (data, status) {
        var Body = "<option value='0' selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.response.length - 1; i++) {
            Body = Body + "<option value='" + data.response[i].value + "'>" + data.response[i].text + "</option>";
        }
        $('#DropEjecutores').html(Body);

        $('#DropEjecutores').val(IdEjecutor);
    }, 'json');

    console.log("EDDY", IdEjecutor);
    $('#txtActividades').val($('#' + 'Actividades_' + IdElemento).html());
    $('#ModalAgregarUbicacion').modal('show');
    //$.get("/Request/Ubicacion/GetDropEjecutores/" + IdUbicacion, function (data, status) {
    //    var Body = "<option value='0' selected>Selecciona una opción</option>";
    //    for (var i = 0; i <= data.length - 1; i++) {
    //        Body = Body + "<option value='" + data[i].Value + "'>" + data[i].Text + "</option>";
    //    }
    //    $('#DropEjecutores').html(Body);
    //    $('#DropEjecutores').val(IdEjecutor);

    //    $('#txtActividades').val($('#' + 'Actividades_' + IdElemento).html());
    //    //Hasta que acabe lo anterior se abre el modal
    //    $('#ModalAgregarUbicacion').modal('show');
    //}, 'json');



}


function ModalAgregarUbicacion() {
    $('#DropEjecutores').html('');
    $('#ModalAgregarUbicacion').modal('show');
    $('#TitleModalUbicacion').html('Agregar ubicación');
    GetInstanciaIVA();

}


function CerrarModalUbicacion() {

    $('#ModalAgregarUbicacion').modal('hide');
    LimpiarFormUbicacion();

}


//function LimpiarModalAgregarUbicacion() {

//    $('#DropProductos').val('0');
//    $('#txtClaveProducto').val('');
//    $('#txtCantidad').val('');
//    $('#txtCantidadTotal').val('');



//    //$('#tbl_productos').DataTable().clear().draw();

//}

//////




function ValidarPlanEntrega() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txtIdentificador').val() == '') {
        Response.Texto = 'Debe agregar un identificador';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtIdentificador').val(), 'Identificador') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Identificador"';
        Response.Bit = true;
        return Response;
    }

    if ($('#txtPeriodo').val() == '') {
        Response.Texto = 'Debe agregar un Periodo';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtPeriodo').val(), 'Periodo') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Periodo"';
        Response.Bit = true;
        return Response;
    }

    if ($('#txtEjecucion').val() == '') {
        Response.Texto = 'Debe ingresar una fecha de ejecución';
        Response.Bit = true;
        return Response;
    }

    if ($('#txtDescripcion').val() == '') {
        Response.Texto = 'Debe ingresar una descripción';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropResponsable').val() == '0' || $('#DropResponsable').val() == null) {
        Response.Texto = 'Debe seleccionar un responsable';
        Response.Bit = true;
        return Response;
    }
    if ($('#DropTipoPlan').val() == '0' || $('#DropTipoPlan').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de plan';
        Response.Bit = true;
        return Response;
    }

    //Validacion para saber si todas las ubicaciones tienen productos asignados
    var listaobjJSON = [];
    $(".ArrayUbicacionProductos").each(function (index) {
        //Se quitan los corchetes para poder concatenar varios objetos.

        var objJSON = JSON.parse($(this).html());
        listaobjJSON.push(objJSON);

    });

    if (listaobjJSON == undefined) {
        Response.Texto = 'No se ha cargado información';
        Response.Bit = true;
        return Response;

    } else {

        for (var i = 0; i < listaobjJSON.length; i++) {
            console.log(listaobjJSON[i].length);
            if (listaobjJSON[i].length == undefined) {
                Response.Texto = 'Hay ubicaciones sin productos asignados';
                Response.Bit = true;
                return Response;
            }
        }

    }
    /////


    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function ValidarUbicacion() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#DropUbicaciones').val() == '0' || $('#DropUbicaciones').val() == null) {
        Response.Texto = 'Debe seleccionar una ubicación';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropEjecutores').val() == '0' || $('#DropEjecutores').val() == null) {
        Response.Texto = 'Debe seleccionar un ejecutor';
        Response.Bit = true;
        return Response;
    }

    if ($('#txtActividades').val() == '') {
        Response.Texto = 'Debe agregar actividades';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtActividades').val(), 'Detalle actividades') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Detalle actividades"';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function ValidarProducto() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#DropProductos').val() == '0' || $('#DropProductos').val() == null) {
        Response.Texto = 'Debe seleccionar un producto';
        Response.Bit = true;
        return Response;
    }

    if ($('#txtCantidad').val() == '' || $('#txtCantidad').val() == '0') {
        Response.Texto = 'Debe ingresar una cantidad';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}


function llenarVistaPrevia() {

    Redimension();
    var identificador = $('#txtIdentificador').val() != '' ? $('#txtIdentificador').val() : '';
    var periodo = $('#txtPeriodo').val() != '' ? $('#txtPeriodo').val() : '';
    var descripcion = $('#txtDescripcion').val() != '' ? $('#txtDescripcion').val() : '';
    var ejecucion = $('#txtEjecucion').val() != '' ? $('#txtEjecucion').val() : '';
    var responsable = $('#DropResponsable').val() != '0' ? $('#DropResponsable option:selected').text() : '';
    var tipoplan = $('#DropTipoPlan').val() != '0' ? $('#DropTipoPlan option:selected').text() : '';

    $('#txtIdentificador_vw').val(identificador);
    $('#txtPeriodo_vw').val(periodo);
    $('#txtDescripcion_vw').val(descripcion);
    $('#txtEjecucion_vw').val(ejecucion);
    $('#DropResponsable_vw').val(responsable);
    $('#DropTipoPlan_vw').val(tipoplan);


    var Arreglo_arreglos = [];

    var tablaubicaciones = $('#tbl_ubicaciones').DataTable().data().toArray();


    //Si no es undefined, entonces es por que ya trae productos cargados.

    var Arreglo_arreglos = [];
    for (var i = 0; i <= tablaubicaciones.length - 1; i++) { //el for se utiliza para obtener los id de cada registro en ubicaciones

        var Interno = [];

        var id = tablaubicaciones[i][0];

        Interno.push($('#' + 'NombreUbicacion_' + id).html());
        Interno.push($('#' + 'Ejecutor_' + id).html());
        Interno.push($('#' + 'Actividades_' + id).html());
        Interno.push($('#' + 'ProductosUbicacion_' + id).html());

        Arreglo_arreglos.push(Interno);
    }


    var table = $('#tbl_ubicaciones_vw').DataTable();

    table.destroy();

    $('#tbl_ubicaciones_vw').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        data: Arreglo_arreglos,
        columns: [
            { title: "Ubicación" },
            { title: "Ejecutor" },
            { title: "Actividades" },
            { title: "Productos" }
        ],

        columnDefs: [

            { "className": "dt-center", "targets": "_all" },
            { "width": "20%", "targets": 0 },
            { "width": "30%", "targets": 1 },
            { "width": "25%", "targets": 2 },
            { "width": "25%", "targets": 3 },

        ]


    });




}

$('#btnEnviarPlanEntrega').click(function () {




})


var SolicEmail = {
    Body: null,
    Email: null,
    Asunto: null,
}

function EnviarCorreoProveedor() {

    var identificador = $('#txtIdentificador').val() != '' ? $('#txtIdentificador').val() : '';
    var periodo = $('#txtPeriodo').val() != '' ? $('#txtPeriodo').val() : '';
    var descripcion = $('#txtDescripcion').val() != '' ? $('#txtDescripcion').val() : '';
    var ejecucion = $('#txtEjecucion').val() != '' ? $('#txtEjecucion').val() : '';
    var responsable = $('#DropResponsable').val() != '0' ? $('#DropResponsable option:selected').text() : '';
    var tipoplan = $('#DropTipoPlan').val() != '0' ? $('#DropTipoPlan option:selected').text() : '';
    var idCon = $('#idContrato').val();


    //$.get("/Request/ProveedorResponsable/Get/" + idCon, function (data, status) {
    $.get($("#EndPointAC").val() + 'Operaciones/PE/Get/Proveedor/contrato/' + idCon, function (data, status) {
        var correo = [];
        correo.push(data.response[0].correo_electronico);
        var OBJ_Email = SolicEmail;
        OBJ_Email.Body =

            '<table width="40%">' +
            '<tbody style="text-align:left">' +
            '<tr>' +
            '<td><strong>Identificador</strong></td>' +
            '<td>' + identificador + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>Periodo</strong></td>' +
            '<td>' + periodo + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>Fecha de ejecución</strong></td>' +
            '<td>' + ejecucion + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>Descripción</strong></td>' +
            '<td>' + descripcion + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>Responsable</strong></td>' +
            '<td>' + responsable + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>Tipo de plan de entrega</strong></td>' +
            '<td>' + tipoplan + '</td>' +
            '</tr>' +
            '</tbody>' +
            '</table>' +
            '<br>' + '<h4 style="text-align:center">UBICACIONES Y PRODUCTOS</h4>' +
            '<table width="100%">' +
            '<thead>' +
            '<tr >' +
            '<th width="20%" style="text-align:center">Ubicación</th>' +
            '<th width="20%" style="text-align:center">Ejecutor</th>' +
            '<th width="30%" style="text-align:center">Actividades</th>' +
            '<th width="30%" style="text-align:center">Productos</th>' +
            '</tr>' +
            '</thead>' +
            '<tbody style="text-align:center">' +
            $('#tbl_ubicaciones_vw tbody').html() +
            '</tbody>' +
            '</table>';

        console.log(OBJ_Email.Body);




        OBJ_Email.Email = correo;
        OBJ_Email.Asunto = 'NOTIFICACIÓN PLAN DE ENTREGA'


        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Email),
            type: 'post',

            success: function (data) {
                function conf() {
                    //return Recargar();
                }
                var si = eval(conf);
                SuccessSAAction("Operación exitosa", "Se he enviado el plan de entrega correctamente", si);

            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', 'No se pudo enviar el correo ');
            },
            processData: false,
            type: 'POST',
            url: $("#EndPointAC").val() + 'SerEnvioCorreo/Send/correo'

        })


    });
}


function Redimension() {

    var tables = document.getElementsByTagName('table');
    for (var i = 0; i < tables.length; i++) {
        if (tables[i].id != "") {
            $('#' + tables[i].id + '').resize();
        }
    }

}

//-- Obtener iva de la instancia --//
function establecerRutasServicio() {
    console.log('obteniendo ruta del iva');
    var idInstancia = $("#IdInstancia").val();
    URL_SERVICIO_BASE = $("#EndPointAC").val();
    URL_OBTENER_IVA_INSTANCIA = URL_SERVICIO_BASE + "SerEsquemaPago/Get/Instancia/" + idInstancia;
    console.log(URL_OBTENER_IVA_INSTANCIA);
}
function GetInstanciaIVA() {
    console.log('obteniendo iva');
    $.get(URL_OBTENER_IVA_INSTANCIA, function (data, status) {
        console.log(data);
        if (data) {
            $('#IVA').val(data.iva);
        } else {
            $('#IVA').val(16.0);
        }

    }, 'json');
}

$('#Monto').keyup(function () {
    if (event.which >= 37 && event.which <= 40) {
        event.preventDefault();
    }
    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, "")
            .replace(/([0-9])([0-9]{2})$/, '$1.$2')
            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",")
            ;
    });
    var monto = parseInt($('#Monto').val().replace(/,/g, ""));
    var montoIVA = parseFloat($('#IVA').val());
    var resultado = parseFloat(monto * montoIVA / 100);
    $('#MontoIVA').val(resultado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
    $('#Total').val(0);

    monto = parseFloat($('#Monto').val().replace(/,/g, ""));
    montoIVA = parseFloat($('#MontoIVA').val().replace(/,/g, ""));
    resultado = monto + montoIVA;

    $('#Total').val(resultado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
});
//--fin iva instancia --//

//function GetPlanesEntrega() {

//    $.get($("#EndPointAC").val() + "Operaciones/PE/Get/lista/tipo/contrato/id/" + $('#idContrato').val() + "/" + $('#HDidUsuario').val(), function (data, status) {
//        var nombre = '';
//        console.log(data);
//        if (data) {
//            if (data.length > 0) {

//            }
//        }

//        LaunchLoader(false);
//    });

//}


