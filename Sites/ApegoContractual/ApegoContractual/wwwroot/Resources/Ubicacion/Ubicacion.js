var ID_UBICACION_NUEVA = "00000000-0000-0000-0000-000000000000";
var LONGITUD_1000 = 1000;
var URL_SERVICIO_BASE = URL_OBTENER_UBICACIONES = URL_AGREGAR_UBICACION = URL_EDITAR_UBICACION = URL_ELIMINAR_UBICACION = URL_OBTENER_UBICACION_POR_ID = URL_AGREGAR_EDITAR_EJECUTOR = URL_ELIMINAR_EJECUTOR = URL_VALIDAR_UBICACION_LIGADA = "";
var URL_OBTENER_RESPONSABLES = URL_OBTENER_EJECUTORES = URL_OBTENER_ESTADOS = URL_OBTENER_CIUDADES = "";

//$(function () {

//    obtenerListado();
//    //obtenerListadoUnidadesMedida();
//});

$(document).ready(function () {
    establecerRutasServicio();

    obtenerListado();
    obtenerResponsables();
    obtenerEjecutores();
    obtenerEstados();

    LaunchLoader(true);

    //$("#tbl_ubicaciones").DataTable({
    //    "language": {
    //        "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
    //    },
    //    "columnDefs": [
    //        { "className": "dt-center", "targets": "_all" },
    //        { "width": "10%", "targets": 0 },
    //        { "width": "20%", "targets": 1 },
    //        { "width": "20%", "targets": 2 },
    //        { "width": "10%", "targets": 3 },
    //        { "width": "20%", "targets": 4 },
    //        //{ "width": "18%", "targets": 5 },
    //        { "width": "11%", "targets": 5 }
    //    ]
    //});


    //$("#tbl_ubicaciones").DataTable();
    
});

function establecerRutasServicio() {
    var idDependencia = $("#hdnIdDependencia").val();

    URL_SERVICIO_BASE = $("#EndPointAC").val();

    URL_OBTENER_UBICACIONES = URL_SERVICIO_BASE + "UbicacionesCatalog/Get/Dependencia/" + idDependencia;
    URL_AGREGAR_UBICACION = URL_SERVICIO_BASE + "UbicacionesCatalog/Add";
    URL_AGREGAR_EDITAR_EJECUTOR = URL_SERVICIO_BASE + "UbicacionesCatalog/Update/Ejecutor";
    URL_EDITAR_UBICACION = URL_SERVICIO_BASE + "UbicacionesCatalog/Update/Ubicacion";
    URL_ELIMINAR_UBICACION = URL_SERVICIO_BASE + "UbicacionesCatalog/Delete/";
    URL_ELIMINAR_EJECUTOR = URL_SERVICIO_BASE + "UbicacionesCatalog/Eliminar/Ejecutor";
    URL_OBTENER_UBICACION_POR_ID = URL_SERVICIO_BASE + "UbicacionesCatalog/Get/unitario/";
    URL_VALIDAR_UBICACION_LIGADA = URL_SERVICIO_BASE + "UbicacionesCatalog/Validar/";

    URL_OBTENER_RESPONSABLES = URL_SERVICIO_BASE + "SerServidorPublico/Get/sigla/RU/dependencia/" + idDependencia;
    URL_OBTENER_EJECUTORES = URL_SERVICIO_BASE + "SerServidorPublico/Get/sigla/EPE/dependencia/" + idDependencia;

    URL_OBTENER_ESTADOS = URL_SERVICIO_BASE + "SerServidorPublico/Get/Estados";
    URL_OBTENER_CIUDADES = URL_SERVICIO_BASE + "SerServidorPublico/Get/Ciudades/Estado/";
}

$("#btn-abrir-modal-guardar").click(function () {

    $("#hdnId").val(ID_UBICACION_NUEVA);
    
    //$("#clave").val('');
    //$("#unidad").val('');
    //$("#direccion").val('');
    //$("#telefonos").val('');
    //$("#dependencia").val('');
    //$("#responsable").val('');

    var bagControl_js = [];
    $('#BagsControlResp').val(JSON.stringify(bagControl_js));

    //Reestablecer contenedor de ejecutores cada que se abra el modal
    $('#bloque_ejecutores').html('');

    abrirModalGuardar();
    $(".campo-formulario").val('');
    var bodyS = "<option value=''>Seleccione...</option>";
    $('#ciudad').html(bodyS);
});

function obtener_datos_ubicacion(idUbicacion) {

    $.get(URL_OBTENER_UBICACION_POR_ID + idUbicacion, function (data, status) {


        for (var i = 0; i <= data.length - 1; i++) {
            if (data[i].responsable == true) { //SE LLENA LA INFORMACION DEL FORMULARIO CUANDO EL SERVIDOR ES RESPONSABLE
                var item = data[0];

                var item = data[0];
                obtenerCiudades(item.tbl_estado_id);

                $("#clave_editar").val(item.clave);
                $("#unidad_editar").val(item.unidad);
                $("#direccion_editar").val(item.direccion);
                $("#telefonos_editar").val(item.telefono);
                $("#dependencia_editar").val(item.tbl_dependencia_id);
                $("#referencia_editar").val(item.referencia);
                $("#responsable_editar").val(item.responsable_ubicacion_id);
                $("#txtDiasAtencion_editar").val(item.dias_atencion);
                $("#txtHorarioAtencion_editar").val(item.horario_atencion);
                $("#estado_editar").val(item.tbl_estado_id);


                setTimeout(function () {
                    $("#ciudad_editar").val(item.tbl_ciudad_id);
                }, 1500)
            }
        }


    });

}


function abrirModalEditarUbicacion(id) {
    var bagControl_js = [];
    $('#BagsControlResp').val(JSON.stringify(bagControl_js));
    $('.AltUbicacion').html('Modificar ubicación');
    $("#hdnId").val(id);
    obtener_datos_ubicacion(id);

    //OPCION 2 PARA ABRIR MODAL DE EDICION CON SOLICITUDES LIGADAS A PLAN
    //$.get(URL_VALIDAR_UBICACION_LIGADA + id, function (data, status) {
    //    if (data.ubicacion_ligada) {

    //        $("#clave_editar").prop("disabled",true);
    //        $("#unidad_editar").prop("disabled", true);
    //        $("#direccion_editar").prop("disabled", true);
    //        $("#telefonos_editar").prop("disabled", true);
    //        $("#dependencia_editar").prop("disabled", true);
    //        $("#referencia_editar").prop("disabled", true);
    //        $("#responsable_editar").prop("disabled", true);
    //        $("#txtDiasAtencion_editar").prop("disabled", true);
    //        $("#txtHorarioAtencion_editar").val(item.horario_atencion);
    //        $("#estado_editar").val(item.tbl_estado_id);

    //    } else {
    //        $("#clave_editar").prop("disabled", false);
    //        $("#unidad_editar").prop("disabled", false);
    //        $("#direccion_editar").prop("disabled", false);
    //        $("#telefonos_editar").prop("disabled", false);
    //        $("#dependencia_editar").prop("disabled", false);
    //        $("#referencia_editar").prop("disabled", false);
    //        $("#responsable_editar").prop("disabled", false);
    //        $("#txtDiasAtencion_editar").prop("disabled", false);
    //        $("#txtHorarioAtencion_editar").prop("disabled", false);
    //        $("#estado_editar").prop("disabled", false);

    //    }

    //    $("#modalEditarUbicacion").modal('show');    
    //});


     //OPCION 1 PARA ABRIR MODAL DE EDICION CON SOLICITUDES LIGADAS A PLAN
    $.get(URL_VALIDAR_UBICACION_LIGADA + id, function (data, status) {
        if (data.ubicacion_ligada) {

            Swal.fire({
                type: 'error',
                title: 'ATENCIÓN',
                text: 'No se puede editar la ubicación porque esta ligada a uno o más planes de entrega'
            });

            return;

        } else {
           
            $("#modalEditarUbicacion").modal('show');    
        }

        
    });

    
    
}

function obtener_ejecutores_ubicacion(idUbicacion) {

    $.get(URL_OBTENER_UBICACION_POR_ID + idUbicacion, function (data, status) {

        var listado = [];
        for (var i = 0; i <= data.length - 1; i++) {


            if (data[i].responsable == false) { //SE LLENA LA INFORMACION DEL FORMULARIO CUANDO EL SERVIDOR ES RESPONSABLE

                var fila = [];

                fila.push(data[i].tbl_rol_usuario); //Nombre
                fila.push(data[i].unidad);
                fila.push("<button class='btn btn-primary' title='Editar ejecutor' onclick=\"abrirModalGuardarEditarEjecutor('" + data[i].tbl_rol_usuario_id + "','" + data[i].id + "','" + data[i].tbl_ubicacion_servidor_id + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar ejecutor' onclick=\"eliminarEjecutor('" + data[i].tbl_ubicacion_servidor_id + "','" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");

                listado.push(fila);

            }
        }

        var table = $('#tbl_Ejecutores').DataTable();

        table.destroy();

        $('#tbl_Ejecutores').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: listado,
            columns: [
                { title: "Ejecutor" },
                { title: "Unidad" },
                { title: "Acciones" },

            ],
            columnDefs: [

                { "className": "dt-center", "targets": "_all" },
                { "width": "50%", "targets": 0 },
                { "width": "30%", "targets": 1 },
                { "width": "20%", "targets": 2 }
            ]
        });

    });
}

function abrirModalEjecutores(idUbicacion) {

    
    $("#hdnId").val(idUbicacion);

    obtener_ejecutores_ubicacion(idUbicacion);
   
    $("#modalEjecutores").modal('show');

}

function abrirModalGuardarEditarEjecutor(idRolUsuario, idUbicacion, idUbicacionServidor) {

    if (idRolUsuario == '') {
        $('#drop_ejecutor').val('');
    } else if (idRolUsuario != '') {
        $('#drop_ejecutor').val(idRolUsuario);
    }

    if (idUbicacion == '') {
        $('#hdnId').val('');
    } else {
        $('#hdnId').val(idUbicacion);
    }

    if (idUbicacionServidor == '') {
        $('#hdnIdUbicacionServidor').val('');
    } else {
        $('#hdnIdUbicacionServidor').val(idUbicacionServidor);
    }

    
    $("#modalGuardarEditarEjecutor").modal('show');

}



$("#btn-abrir-modal-guardar-ejecutor").click(function () {

    var idUbicacion = $('#hdnId').val();
    abrirModalGuardarEditarEjecutor('', idUbicacion ,'');
});


function abrirModalGuardar() {
    $("#modalGuardarUbicacion").modal('show');
    $('.AltUbicacion').html('Alta de ubicación');
}

function obtenerListado() {

    $.get(URL_OBTENER_UBICACIONES, function (data, status) {

        var listado = [];

        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];

            fila.push(data[i].clave);
            fila.push(data[i].unidad);
            fila.push(data[i].direccion);
            fila.push(data[i].telefono);
            fila.push(data[i].responsable_ubicacion);
            fila.push("<button class='btn btn-success' title='Editar ubicación' onclick=\"abrirModalEditarUbicacion('" + data[i].id + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-primary' title='Editar ejecutores' onclick=\"abrirModalEjecutores('" + data[i].id + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar' onclick=\"eliminar('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");

            listado.push(fila);
        }

        var table = $('#tbl_ubicaciones').DataTable();

        table.destroy();

        $('#tbl_ubicaciones').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: listado,
            columns: [
                { title: "Clave" },
                { title: "Unidad" },
                { title: "Dirección" },
                { title: "Teléfonos" },
                { title: "Responsable" },
                { title: "Acciones" },
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });
}

function obtenerResponsables() {

    $.get(URL_OBTENER_RESPONSABLES, function (data, status) {
        console.log(data);
        console.log(URL_OBTENER_RESPONSABLES);
        if (data) {
            if (data.length == 0) {
                ErrorSA('No se encontraron responsables de ubicaciones', 'Su dependencia no cuenta con responsables de ubicaciones registrados, favor de crearlos en los datos maestros');

            } else {
                var body = "<option value=''>Seleccione...</option>";

                for (var i = 0; i <= data.length - 1; i++) {
                    body = body + "<option value='" + data[i].tbl_rol_usuario_id + "'>" + data[i].nombrecompleto + "</option>";
                }
                $('#responsable').html(body);
                $('#responsable_editar').html(body);
            }

        } else {
            ErrorSA('No se encontraron responsables de ubicaciones', 'Ocurrió un error al obtener los datos');
        }
    });
}

function obtenerEjecutores() {

    $.get(URL_OBTENER_EJECUTORES, function (data, status) {

        var body = "<option value=''>Seleccione...</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].tbl_rol_usuario_id + "'>" + data[i].nombrecompleto + "</option>";
        }

        $('#ejecutores').html(body);
        $('#drop_ejecutor').html(body);
        
    });
}

function obtenerEstados() {

    $.get(URL_OBTENER_ESTADOS, function (data, status) {

        var body = "<option value=''>Seleccione...</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }

        $('#estado').html(body);
        $('#estado_editar').html(body);
    });
}

$("#estado").on("change", function () {
    obtenerCiudades($("#estado").val());
});

function obtenerCiudades(id) {

    $.get(URL_OBTENER_CIUDADES + id, function (data, status) {
        console.log(data)
        var body = "<option value=''>Seleccione...</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }

        $('#ciudad').html(body);
        $('#ciudad_editar').html(body);
    });
}
//en caso de ocuparse los ejecutores descomentar las lineas siguientes 
function addRespo_bag() {
    var contenedorjson = "BagsControlResp";
    var value = $("#ejecutores").val();
    var text = $("#ejecutores option:selected").text();

    if (value != '' && value != null) { 
        if (!FindBagResp(value, contenedorjson)) {
            var bag = "<div id='id_" + value + "' class='div_responsable'><br><a class='Bag-Obligaciones'> " + text + " <i onclick=\"RemoveBagResp('" + value + "','" + contenedorjson + "')\" class='fa fa-fw fa-remove over'></i></a></div>";
            var obj = {
                value: value,
                text: text
            }
            var array_obj = JSON.parse($('#' + contenedorjson).val());
            array_obj.push(obj);
            $('#' + contenedorjson).val(JSON.stringify(array_obj));
            $("#bloque_ejecutores").append(bag);
        }
    }
}

function listarEjecutoresPorUbicacion(data) { //SE LLENA EL BLOQUE DE EJECUTORES CUANDO SE ABRE EL MODAL EDITAR

    $('#bloque_ejecutores').html('');

    for (var i = 0; i < data.length; i++) {

        if (data[i].responsable == false) {
            var bag = "<div id='id_" + data[i].tbl_rol_usuario_id + "' class='div_responsable'><br><a class='Bag-Obligaciones'> " + data[i].tbl_rol_usuario + " <i onclick=\"RemoveBagResp('" + data[i].tbl_rol_usuario_id + "','" + 'BagsControlResp' + "')\" class='fa fa-fw fa-remove over'></i></a></div>";

        var obj = {
            value: data[i].tbl_rol_usuario_id,
            text: data[i].tbl_rol_usuario
        };

        var array_obj = JSON.parse($('#BagsControlResp').val());
        array_obj.push(obj);
        $('#BagsControlResp').val(JSON.stringify(array_obj));

            $('#bloque_ejecutores').append(bag);
        }
    }

    //$.get("/Request/Ubicacion/GetEjecutoresPorUbicacion/" + idUbicacion, function (data, status) {
    //    console.log(data)
    //    for (var i = 0; i < data.length; i++) {
    //        var bag = "<div id='id_" + data[i].Id + "' class='div_responsable'><br><a class='Bag-Obligaciones'> " + data[i].NombreCompleto + " <i onclick=\"RemoveBagResp('" + data[i].Id + "','" + 'BagsControlResp' + "')\" class='fa fa-fw fa-remove over'></i></a></div>";

    //        var obj = {
    //            value: data[i].Id,
    //            text: data[i].NombreCompleto
    //        };

    //        var array_obj = JSON.parse($('#BagsControlResp').val());
    //        array_obj.push(obj);
    //        $('#BagsControlResp').val(JSON.stringify(array_obj));

    //        $('#bloque_ejecutores').append(bag);
    //    }
    //});
}

function FindBagResp(value, contenedorjson) {
    console.log(contenedorjson);
    var bit = false;
    var obj = JSON.parse($('#' + contenedorjson).val());

    for (var i = 0; i <= obj.length - 1; i++) {
        if (obj[i].value == value) {
            bit = true;
        }
    }
    return bit;
}

function RemoveBagResp(value, contenedorjson) {
    var obj = JSON.parse($('#' + contenedorjson).val());
    for (var i = 0; i <= obj.length - 1; i++) {
        if (obj[i].value == value) {
            obj[i] = null;
        }
    }
    var newobj = [];
    for (i = 0; i <= obj.length - 1; i++) {
        if (obj[i] != null) {
            newobj.push(obj[i]);
        }
    }
    $('#id_' + value).remove();
    $('#BagsControlResp').val(JSON.stringify(newobj));
}

$("#modalGuardarUbicacion #btnGuardar").click(function () {
    guardar();
});

$("#btnEditar").click(function () {
    EditarUbicacion();
});

$("#btnGuardar_Ejecutor").click(function () {
    guardarEjecutor();
});


function guardar() {

    var evaluacion = validarEntidad();

    if (evaluacion.Bit) {

        ErrorSA('Hay un error en los datos de entrada', evaluacion.Texto);

        return;
    }
    console.log(evaluacion.objeto.p_id);
    //var url = evaluacion.objeto.p_id == "00000000-0000-0000-0000-000000000000" ? URL_AGREGAR_UBICACION : URL_EDITAR_UBICACION;
    //var url = evaluacion.objeto.ubicacion.p_id == "00000000-0000-0000-0000-000000000000" ? URL_AGREGAR_UBICACION : URL_EDITAR_UBICACION;
    //var tipo = evaluacion.objeto.ubicacion.p_id == "00000000-0000-0000-0000-000000000000" ? "POST" : "PUT";

    console.log(JSON.stringify(evaluacion.objeto));
    
    $.ajax({
        url: URL_AGREGAR_UBICACION,
        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(evaluacion.objeto),
        type: 'POST',
        success: function (data) {

            if (data[0].cod == 'success') {
                $('.campo-formulario').val('');

                obtenerListado();

                $('#tbl_ubicaciones, #modalGuardarUbicacion').modal('hide');

                SuccessSA('', data[0].msg);
                //window.location.reload();

                //ContinueSuccessSA();
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Error al guardar el registro',
                    text: data[0].msg
                });
            }
        },
        error: function (data) {

            Swal.fire({
                type: 'error',
                title: 'Error al guardar el registro',
                text: data
            });
        }
    });
}




function EditarUbicacion() {

    var evaluacion = validarEntidadEditarUbicacion();

    if (evaluacion.Bit) {

        ErrorSA('Hay un error en los datos de entrada', evaluacion.Texto);

        return;
    }
    console.log(evaluacion.objeto.p_id);

    console.log(JSON.stringify(evaluacion.objeto));

    $.ajax({
        url: URL_EDITAR_UBICACION,
        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(evaluacion.objeto),
        type: 'PUT',
        success: function (data) {


            //var Obj = JSON.parse(data);
            if (data[0].cod == 'success') {

                //Se limpia el formulario
                $('.campo-formulario').val('');

                obtenerListado();

                
                $('#modalEditarUbicacion').modal('hide');

                SuccessSA('', 'El registro se guardó exitosamente');

            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Error al guardar el registro',
                    text: data[0].msg
                });
            }
        },
        error: function (data) {

            Swal.fire({
                type: 'error',
                title: 'Error al guardar el registro',
                text: data.msg
            });
        }
    });
}



function guardarEjecutor() {

    var objEjecutor = claseEjecutor;

    var idRolUsuario = $('#drop_ejecutor').val();
    var idUbicacion = $('#hdnId').val();
    var idUbicacionServidor = $('#hdnIdUbicacionServidor').val();
    


    if (idRolUsuario == '' || idRolUsuario == null) {

        Swal.fire({
            type: 'error',
            title: 'Error al guardar el registro',
            text: 'No se ha seleccionado un ejecutor'
        });
        return;

    } else if (idUbicacionServidor == '' || idUbicacionServidor == null) {
        objEjecutor.p_opt = 2;
        objEjecutor.p_tbl_ubicacion_servidor_id = '00000000-0000-0000-0000-000000000000';
        objEjecutor.p_tbl_ubicacion_id = idUbicacion;
    } else {
        objEjecutor.p_opt = 3;
        objEjecutor.p_tbl_ubicacion_servidor_id = idUbicacionServidor;
        objEjecutor.p_tbl_ubicacion_id = '00000000-0000-0000-0000-000000000000';
        
    }

    objEjecutor.p_tbl_rol_usuario_id = idRolUsuario;
    

    console.log(JSON.stringify(objEjecutor));
    
    $.ajax({
        url: URL_AGREGAR_EDITAR_EJECUTOR,
        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(objEjecutor),
        type: 'POST',
        success: function (data) {

            if (data.cod == 'success') {

                obtener_ejecutores_ubicacion(idUbicacion);
                $('.campo-formulario').val('');
                $('#modalGuardarEditarEjecutor').modal('hide');

                SuccessSA('', data.msg);
                
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Error al guardar el registro',
                    text: data.msg
                });
            }
        },
        error: function (data) {

            Swal.fire({
                type: 'error',
                title: 'Error al guardar el registro',
                text: data
            });
        }
    });
}



$('.Clear').click(function () {
    $('#responsable').val('');
    $('#ejecutores').val('');
    $('.campo-formulario').val('');

});

function confirmarEliminar(id) {
    $.ajax({
        url: URL_ELIMINAR_UBICACION + id,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: 'DELETE',
        success: function (data) {
            //alert(data);
            var objresponse = JSON.parse(data);
            if (objresponse.response[0].cod == 'success') {
                obtenerListado();
                SuccessSA('', objresponse.response[0].msg);
            }
            else {
                ErrorSA("", objresponse.response[0].msg);
            }
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', 'Hubo un error');
        }
    });
}

function confirmarEliminarEjecutor(idUbicacionServidor, idUbicacion) {

    var objEjecutor = claseEjecutor;

    objEjecutor.p_opt = 4;
    objEjecutor.p_tbl_ubicacion_servidor_id = idUbicacionServidor;
    objEjecutor.p_tbl_ubicacion_id = '00000000-0000-0000-0000-000000000000';
    objEjecutor.p_tbl_rol_usuario_id = '00000000-0000-0000-0000-000000000000';


    $.ajax({
        url: URL_ELIMINAR_EJECUTOR,
        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(objEjecutor),
        type: 'DELETE',
        success: function (data) {
            //alert(data);
            
            if (data.cod=='success') {

                obtener_ejecutores_ubicacion(idUbicacion);

                SuccessSA('', data.msg);

            }
            else {
                ErrorSA("", data.msg);
            }
        },
        error: function () {
            
            ErrorSA('', 'Hubo un error al eliminar');
        }
    });
}

function eliminar(id) {
    function Confirmacion() {
        return confirmarEliminar(id);
    }

    var AccionSi = eval(Confirmacion);

    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}




function eliminarEjecutor(idUbicacionServidor, idUbicacion) {
    function Confirmacion() {
        return confirmarEliminarEjecutor(idUbicacionServidor, idUbicacion);
    }

    var AccionSi = eval(Confirmacion);

    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function validarEntidad() {
    var OBJ = claseUbicacion;

    var Response = { Texto: '', Bit: true, objeto: null };

    var strClave = $('#clave').val();
    var strUnidad = $('#unidad').val();
    var responsable = $('#responsable').val();
    var strDireccion = $('#direccion').val();
    var strReferencia = $('#referencia').val();
    var strTelefonos = $('#telefonos').val();
    var strDiasAtencion = $('#txtDiasAtencion').val();
    var strHorarioAtencion = $('#txtHorarioAtencion').val();
    var estado = $('#estado').val();
    var ciudad = $('#ciudad').val();

    var lblClave = $("#lblClave").html();
    var lblUnidad = $("#lblUnidad").html();
    var lblDireccion = $("#lblDireccion").html();
    var lblReferencia = $("#lblReferencia").html();
    var lblTelefonos = $("#lblTelefonos").html();
    var lblDiasAtencion = $("#lblDiasAtencion").html();
    var lblHorarioAtencion = $("#lblHorarioAtencion").html();
    var lblResponsable = $("#lblResponsable").html();
    var lblEstado = $("#lblEstado").html();
    var lblCiudad = $("#lblCiudad").html();

    if ($.trim(strClave) === '') {
        Response.Texto = 'El campo "' + lblClave + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strUnidad) === '') {
        Response.Texto = 'El campo "' + lblUnidad + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if (responsable === '' || responsable === null) {
        Response.Texto = 'El campo "' + lblResponsable + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strDireccion) === '') {
        Response.Texto = 'El campo "' + lblDireccion + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strReferencia) === '') {
        Response.Texto = 'El campo "' + lblReferencia + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strTelefonos) === '') {
        Response.Texto = 'El campo "' + lblTelefonos + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strDiasAtencion) === '') {
        Response.Texto = 'El campo "' + lblDiasAtencion + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strHorarioAtencion) === '') {
        Response.Texto = 'El campo "' + lblHorarioAtencion + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if (estado === '' || estado === null) {
        Response.Texto = 'El campo "' + lblEstado + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (ciudad === '' || ciudad === null) {
        Response.Texto = 'El campo "' + lblCiudad + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if (!validarLongitudCadena(strClave, LONGITUD_1000)) {
        Response.Texto = generarMensajeValidacionLongitud(lblClave, LONGITUD_1000);
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strUnidad, LONGITUD_1000)) {
        Response.Texto = generarMensajeValidacionLongitud(lblUnidad, LONGITUD_1000);
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strDireccion, LONGITUD_1000)) {
        Response.Texto = generarMensajeValidacionLongitud(lblDireccion, LONGITUD_1000);
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strTelefonos, LONGITUD_1000)) {
        Response.Texto = generarMensajeValidacionLongitud(lblTelefonos, LONGITUD_1000);
        Response.Bit = true;
        return Response;
    }

    OBJ.ubicacion.p_id = $("#hdnId").val();
    OBJ.ubicacion.p_tbl_dependencia_id = $('#hdnIdDependencia').val();
    OBJ.ubicacion.p_tbl_rol_usuario_id = $('#responsable').val();
    OBJ.ubicacion.p_clave = $('#clave').val();
    OBJ.ubicacion.p_unidad = $('#unidad').val();
    OBJ.ubicacion.p_direccion = $('#direccion').val();
    OBJ.ubicacion.p_referencia = $('#referencia').val();
    OBJ.ubicacion.p_telefono = $('#telefonos').val();
    OBJ.ubicacion.p_dias_atencion = $('#txtDiasAtencion').val();
    OBJ.ubicacion.p_horario_atencion = $('#txtHorarioAtencion').val();
    OBJ.ubicacion.p_tbl_ciudad_id = $('#ciudad').val();

    var Ejecutoresobj = JSON.parse($('#BagsControlResp').val());
    var idsEjecutores = [];
    for (var i = 0; i <= Ejecutoresobj.length - 1; i++) {
        idsEjecutores.push(Ejecutoresobj[i].value);
    }
    OBJ.ejecutores.p_str_ids = (idsEjecutores.toString());
    //Validar y obtener ejecutores
    if ($(".div_responsable").length !== 0) {
        //var lstEjecutores = [];
        var lstEjecutores = "";
        var token = "";

        $(".div_responsable").each(function () {
            var idEjecutor = this.id.substring(3);

            //lstEjecutores.push(idEjecutor);
            lstEjecutores += token + idEjecutor;

            token = ",";
        });
        //OBJ.p_str_ejecutores = lstEjecutores;
    }
    else {
        Response.Texto = 'Es requerido agregar al menos un ejecutor.';
        Response.Bit = true;
        return Response;
    }
    console.log(OBJ);
    Response.Texto = '';
    Response.Bit = false;
    Response.objeto = OBJ;

    return Response;
}


function validarEntidadEditarUbicacion() {
    var OBJ = ubicacion_clase;

    var Response = { Texto: '', Bit: true, objeto: null };

    var strClave = $('#clave_editar').val();
    var strUnidad = $('#unidad_editar').val();
    var responsable = $('#responsable_editar').val();
    var strDireccion = $('#direccion_editar').val();
    var strReferencia = $('#referencia_editar').val();
    var strTelefonos = $('#telefonos_editar').val();
    var strDiasAtencion = $('#txtDiasAtencion_editar').val();
    var strHorarioAtencion = $('#txtHorarioAtencion_editar').val();
    var estado = $('#estado_editar').val();
    var ciudad = $('#ciudad_editar').val();

    var lblClave = $("#lblClave").html();
    var lblUnidad = $("#lblUnidad").html();
    var lblDireccion = $("#lblDireccion").html();
    var lblReferencia = $("#lblReferencia").html();
    var lblTelefonos = $("#lblTelefonos").html();
    var lblDiasAtencion = $("#lblDiasAtencion").html();
    var lblHorarioAtencion = $("#lblHorarioAtencion").html();
    var lblResponsable = $("#lblResponsable").html();
    var lblEstado = $("#lblEstado").html();
    var lblCiudad = $("#lblCiudad").html();

    if ($.trim(strClave) === '') {
        Response.Texto = 'El campo "' + lblClave + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strUnidad) === '') {
        Response.Texto = 'El campo "' + lblUnidad + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if (responsable === '' || responsable === null) {
        Response.Texto = 'El campo "' + lblResponsable + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strDireccion) === '') {
        Response.Texto = 'El campo "' + lblDireccion + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strReferencia) === '') {
        Response.Texto = 'El campo "' + lblReferencia + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strTelefonos) === '') {
        Response.Texto = 'El campo "' + lblTelefonos + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strDiasAtencion) === '') {
        Response.Texto = 'El campo "' + lblDiasAtencion + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if ($.trim(strHorarioAtencion) === '') {
        Response.Texto = 'El campo "' + lblHorarioAtencion + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if (estado === '' || estado === null) {
        Response.Texto = 'El campo "' + lblEstado + '" es requerido.';
        Response.Bit = true;
        return Response;
    }
    if (ciudad === '' || ciudad === null) {
        Response.Texto = 'El campo "' + lblCiudad + '" es requerido.';
        Response.Bit = true;
        return Response;
    }

    if (!validarLongitudCadena(strClave, LONGITUD_1000)) {
        Response.Texto = generarMensajeValidacionLongitud(lblClave, LONGITUD_1000);
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strUnidad, LONGITUD_1000)) {
        Response.Texto = generarMensajeValidacionLongitud(lblUnidad, LONGITUD_1000);
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strDireccion, LONGITUD_1000)) {
        Response.Texto = generarMensajeValidacionLongitud(lblDireccion, LONGITUD_1000);
        Response.Bit = true;
        return Response;
    }
    if (!validarLongitudCadena(strTelefonos, LONGITUD_1000)) {
        Response.Texto = generarMensajeValidacionLongitud(lblTelefonos, LONGITUD_1000);
        Response.Bit = true;
        return Response;
    }

    OBJ.p_id = $("#hdnId").val();
    OBJ.p_tbl_dependencia_id = $('#hdnIdDependencia').val();
    OBJ.p_tbl_rol_usuario_id = $('#responsable_editar').val();
    OBJ.p_clave = $('#clave_editar').val();
    OBJ.p_unidad = $('#unidad_editar').val();
    OBJ.p_direccion = $('#direccion_editar').val();
    OBJ.p_referencia = $('#referencia_editar').val();
    OBJ.p_telefono = $('#telefonos_editar').val();
    OBJ.p_dias_atencion = $('#txtDiasAtencion_editar').val();
    OBJ.p_horario_atencion = $('#txtHorarioAtencion_editar').val();
    OBJ.p_tbl_ciudad_id = $('#ciudad_editar').val();

    
    console.log(OBJ);
    Response.Texto = '';
    Response.Bit = false;
    Response.objeto = OBJ;

    return Response;
}

function generarMensajeValidacionLongitud(lblCampoValidar, longitudCadena) {
    return 'La longitud del campo "' + lblCampoValidar + '" debe ser menor o igual a ' + longitudCadena + ' caracteres.';
}

var claseUbicacion = {
    ubicacion: {
        p_opt: 0,
        p_id: null,
        p_tbl_dependencia_id: $("#HDidDependencia").val(),
        p_tbl_ciudad_id: '',
        p_clave: '',
        p_unidad: '',
        p_direccion: '',
        p_referencia: '',
        p_telefono: '',
        p_activo: 'true',
        p_dias_atencion: '',
        p_horario_atencion: '',
        p_tbl_rol_usuario_id: '',
    },
    ejecutores: {
        p_opt: 0,
        p_tbl_ubicacion_id: '00000000-0000-0000-0000-000000000000',
        p_str_ids: null, 
    }
};


var claseEjecutor = {
    p_opt: 0,
    p_tbl_ubicacion_servidor_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_ubicacion_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_rol_usuario_id: '00000000-0000-0000-0000-000000000000'
};


var ubicacion_clase = {
    
    p_opt: 0,
    p_id: null,
    p_tbl_dependencia_id: $("#HDidDependencia").val(),
    p_tbl_ciudad_id: '',
    p_clave: '',
    p_unidad: '',
    p_direccion: '',
    p_referencia: '',
    p_telefono: '',
    p_activo: 'true',
    p_dias_atencion: '',
    p_horario_atencion: '',
    p_tbl_rol_usuario_id: '',

};


