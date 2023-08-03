$(document).ready(function () {    
    LaunchLoader(true);
    GetEjercicio();
    AjusteTabla();    

    GetTipoProyecto();
    GetCriticidad();
    GetTipo_Ejecucion();
    getProyectos();

    $('#fecha_inicio').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    $('#fecha_fin').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    $('#fecha_inicio_ed').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    $('#fecha_fin_ed').datetimepicker({
        format: 'YYYY-MM-DD'
    });

});

var con = $("#EndPointAdmon").val();
var ins = $('#HDidInstancia').val();
var dep = $('#HDidDependencia').val();

$("#Close").click(function () {
    $('.Clean').val('');
});

function AjusteTabla() {
    $('#tbl_Proyectos').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "5%", "targets": 0 },
            { "width": "55%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "5%", "targets": 4 },
            //{ "width": "5%", "targets": 5 },
            { "width": "15%", "targets": 5 }
        ],
    });
}

function GetEjercicio() {
    $.get(con + "Dependencia/Get/Ejercicio", function (data, status) {
        //console.log(data)
        //var body = "<option disabled selected value=''>Seleccione...</option>";
        var body = null;
        for (var i = 0; i <= data.length - 1; i++) {
           body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#Ejercicio').html(body);
    });
}
function GetTipoProyecto(id) {
    $.get(con + "Proyectos/Get/Tipo_P/" + ins, function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#tipo_proyecto').html(body);
        $('#tipo_proyecto_ed').html(body);
        $('#tipo_proyecto_ed > option[value="' + id + '"]').attr("selected", "selected");
    });
    return;
}
function GetCriticidad(id) {
    $.get(con + "Proyectos/Get/Criticidad", function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#Criticidad').html(body);
        $('#Criticidad_ed').html(body);
        $('#Criticidad_ed > option[value="' + id + '"]').attr("selected", "selected");
    });
    return;
}
function GetTipo_Ejecucion(id) {
    $.get(con + "Proyectos/Get/Tipo_Ejecucion", function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#tipo_ejecucion').html(body);
        $('#tipo_ejecucion_ed').html(body);
        $('#tipo_ejecucion_ed > option[value="' + id + '"]').attr("selected", "selected");
    });
    return;
}
function GetEstatus(id) {
    $.get(con + "Proyectos/Get/Estatus_Proyecto", function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#Estatus_ed').html(body);
        $('#Estatus_ed > option[value="' + id + '"]').attr("selected", "selected");
    });
    return;
}

$("#Ejercicio").on("change", function () {
    getProyectos();
});

function getProyectos() {
    $.get(con + "Proyectos/Get_Lista/" + dep, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var fechaejercicio = (data[i].fecha_incio).split('-');
            var compareje = $('#Ejercicio option:selected').text();
            if (fechaejercicio[0] == compareje) {
                var ir = null;
                var ir_b = null;
                var Interno = [];
                Interno.push(i + 1);
                Interno.push(data[i].proyecto);
                var fechaini = (data[i].fecha_incio).split('T');
                Interno.push(fechaini[0]);
                var fechafin = (data[i].fecha_fin).split('T');
                Interno.push(fechafin[0]);
                if (data[i].tiene_contrato > 0) {
                    ir = "<button class='btn btn-default' title='Contratos' onclick=\"IrContratos('" + data[i].id + "');\"><i class='glyphicon glyphicon-list'></i></button>"
                }
                Interno.push(ir);
                //if (data[i].tiene_documentos > 0) {
                //    ir_b = "<button class='btn btn-default' title='Documentos' onclick=\"IrDocumentos('" + data[i].id + "');\"><i class='glyphicon glyphicon-list'></i></button>"
                //}
                //Interno.push(ir_b);
                Interno.push("<button class='btn btn-default' title='Detalle' onclick=\"muestraModaldetalle('" + data[i].id + "');\"><i class='fa fa-send'></i></button> <button class='btn btn-primary' title='Editar' onclick=\"muestraModalEditar('" + data[i].id + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar' onclick=\"DeleteItem('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");
                Arreglo_arreglos.push(Interno);
            }
        }
        var table = $('#tbl_Proyectos').DataTable();
        table.destroy();
        $('#tbl_Proyectos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Proyecto" },
                { title: "Fecha inicio" },
                { title: "Fecha fin" },
                { title: "Contratos" },
                //{ title: "Documentos" },
                { title: "Acción" }
            ],
            columnDefs: [
                {
                    targets: '_all',
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });
}

function IrContratos(item) {
    var route = '/contratos/Lista/' + item;
    $.get("/Proyectos/Index", function (data, status) {
        return window.location.replace(route);
    });
}

function muestraModalAgregarProyectos() {
    $('.Clean').val('');
    $('.Clean').prop("disabled", false); 
    $('#ModalAgregarProyecto').modal('show');
}
function muestraModalEditar(item) {
    $('.Clean').val('');
    $("#id_proyecto_edit").val(item);
    getedit(item);    
    $('.Clean').prop("disabled", false);
    $('#EditarProyecto').show(true);
    $('#title').html('Editar proyecto');
    $('#ModalEditarProyecto').modal('show');
}
function muestraModaldetalle(item) {
    $('.Clean').val('');
    getedit(item);
    $('#title').html('Detalle proyecto');
    $('#ModalEditarProyecto').modal('show');
    $('.Clean').prop("disabled", true);
    $('#EditarProyecto').hide(true);    
}

function getedit(item) {
    $.get(con + "Proyectos/Get_Proyecto/" + item, function (data, status) {

        if (data !== null) {
            for (var i = 0; i <= data.length - 1; i++) {
                $('#Nom_Proy_ed').val(data[i].proyecto);
                $('#Objetivo_ed').val(data[i].objetivo);
                $('#Alcance_ed').val(data[i].alcance);                
                GetEstatus(data[i].tbl_estatus_proyecto_id);
                GetTipoProyecto(data[i].tbl_tipo_proyecto_id);
                GetCriticidad(data[i].tbl_criticidad_proyecto_id);
                GetTipo_Ejecucion(data[i].tbl_tipo_ejecucion_id);
                var fechainicio = (data[i].fecha_incio).split('T');
                var fechafin = (data[i].fecha_fin).split('T');
                $('#fecha_inicio_ed').val(fechainicio[0]);
                $('#fecha_fin_ed').val(fechafin[0]);
            }
        }
    });
}

$("#GuardarProyecto").click(function () {
    AddProyecto();
});
$("#EditarProyecto").click(function () {
    EditProyecto();
});

function AddProyecto() {
    var Validacion = ValidarProyecto();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_proyecto;
        OBJ_Form.id = '00000000-0000-0000-0000-000000000000';
        OBJ_Form.tbl_tipo_proyecto_id = $('#tipo_proyecto').val();
        OBJ_Form.proyecto = $('#Nom_Proy').val();
        OBJ_Form.objetivo = $('#Objetivo').val();
        OBJ_Form.alcance = $('#Alcance').val();
        OBJ_Form.tbl_criticidad_proyecto_id = $('#Criticidad').val();
        OBJ_Form.fecha_incio = $('#fecha_inicio').val();
        OBJ_Form.fecha_fin = $('#fecha_fin').val();
        OBJ_Form.tbl_estatus_proyecto_id = "11656baa-387c-11ea-82d7-00155d1b3502";
        OBJ_Form.tbl_tipo_ejecucion_id = $('#tipo_ejecucion').val();
        OBJ_Form.tbl_etapa_proyecto_id = "";
        OBJ_Form.tbl_tipo_analisis_id = "";
        OBJ_Form.tbl_nivel_analisis_id = "";
        OBJ_Form.criterio_economico = 0;
        OBJ_Form.criterio_social = 0;
        OBJ_Form.criterio_ambienta = 0;
        OBJ_Form.criterio_politico = 0;
        OBJ_Form.criterio_tecnico_institucional = 0;
        OBJ_Form.tbl_dependencia_id = dep;

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'post',

            success: function (data) {
                //var data_b = JSON.parse(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", objresponse[0].msg);
                    getProyectos();
                    $('#ModalAgregarProyecto').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Proyectos/Add")

        })
    }
}
function ValidarProyecto() {
    var Response = { Texto: '', Bit: true, objeto: null };
    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if ($('#Nom_Proy').val() == '') {
        Response.Texto = 'Debe agregar un nombre de proyecto';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Nom_Proy').val(), 'Nom_Proy') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Nombre del proyecto"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Objetivo').val() == '') {
        Response.Texto = 'Debe agregar un objetivo';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Objetivo').val(), 'Objetivo') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Objetivo"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Alcance').val() == '') {
        Response.Texto = 'Debe agregar un alcance';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Alcance').val(), 'Alcance') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Alcance"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Criticidad').val() == ('' || null)) {
        Response.Texto = 'Debe seleccionar una criticidad';
        Response.Bit = true;
        return Response;
    }
    if ($('#tipo_ejecucion').val() == ('' || null)) {
        Response.Texto = 'Debe seleccionar un tipo de ejecición';
        Response.Bit = true;
        return Response;
    }
    if ($('#tipo_proyecto').val() == ('' || null)) {
        Response.Texto = 'Debe seleccionar un tipo de proyecto';
        Response.Bit = true;
        return Response;
    }
    if ($('#fecha_inicio').val() == '') {
        Response.Texto = 'Debe ingresar una fecha inicial';
        Response.Bit = true;
        return Response;
    }
    if ($('#fecha_fin').val() == '') {
        Response.Texto = 'Debe ingresar una fecha final';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function EditProyecto() {
    var Validacion = ValidarProyectoEd();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_proyecto;
        OBJ_Form.id = $('#id_proyecto_edit').val();
        OBJ_Form.tbl_tipo_proyecto_id = $('#tipo_proyecto_ed').val();
        OBJ_Form.proyecto = $('#Nom_Proy_ed').val();
        OBJ_Form.objetivo = $('#Objetivo_ed').val();
        OBJ_Form.alcance = $('#Alcance_ed').val();
        OBJ_Form.tbl_criticidad_proyecto_id = $('#Criticidad_ed').val();
        OBJ_Form.fecha_incio = $('#fecha_inicio_ed').val();
        OBJ_Form.fecha_fin = $('#fecha_fin_ed').val();
        OBJ_Form.tbl_estatus_proyecto_id = $('#Estatus_ed').val();
        OBJ_Form.tbl_tipo_ejecucion_id = $('#tipo_ejecucion_ed').val();
        OBJ_Form.tbl_etapa_proyecto_id = "";
        OBJ_Form.tbl_tipo_analisis_id = "";
        OBJ_Form.tbl_nivel_analisis_id = "";
        OBJ_Form.criterio_economico = 0;
        OBJ_Form.criterio_social = 0;
        OBJ_Form.criterio_ambienta = 0;
        OBJ_Form.criterio_politico = 0;
        OBJ_Form.criterio_tecnico_institucional = 0;
        OBJ_Form.tbl_dependencia_id = dep;

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'put',

            success: function (data) {
                //var data_b = JSON.parse(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", objresponse[0].msg);
                    getProyectos();
                    $('#ModalEditarProyecto').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Proyectos/Update")

        })
    }
}
function ValidarProyectoEd() {
    var Response = { Texto: '', Bit: true, objeto: null };    
    if ($('#Nom_Proy_ed').val() == '') {
        Response.Texto = 'Debe agregar un nombre de proyecto';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Nom_Proy_ed').val(), 'Nom_Proy_ed') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Nombre del proyecto"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Objetivo_ed').val() == '') {
        Response.Texto = 'Debe agregar un objetivo';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Objetivo_ed').val(), 'Objetivo_ed') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Objetivo"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Alcance_ed').val() == '') {
        Response.Texto = 'Debe agregar un alcance';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Alcance_ed').val(), 'Alcance_ed') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Alcance"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Criticidad_ed').val() == ('' || null)) {
        Response.Texto = 'Debe seleccionar una criticidad';
        Response.Bit = true;
        return Response;
    }
    if ($('#tipo_ejecucion_ed').val() == ('' || null)) {
        Response.Texto = 'Debe seleccionar un tipo de ejecición';
        Response.Bit = true;
        return Response;
    }
    if ($('#tipo_proyecto_ed').val() == ('' || null)) {
        Response.Texto = 'Debe seleccionar un tipo de proyecto';
        Response.Bit = true;
        return Response;
    }
    if ($('#Estatus_ed').val() == ('' || null)) {
        Response.Texto = 'Debe seleccionar un estatus de proyecto';
        Response.Bit = true;
        return Response;
    }
    if ($('#fecha_inicio_ed').val() == '') {
        Response.Texto = 'Debe ingresar una fecha inicial';
        Response.Bit = true;
        return Response;
    }
    if ($('#fecha_fin_ed').val() == '') {
        Response.Texto = 'Debe ingresar una fecha final';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function DeleteItem(item) {
    function Confirmacion() {
        return DeleteProy(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?", "Si", "No", AccionSi, "");
}
function DeleteProy(item) {
    var OBJ_Form = {};
    OBJ_Form.id = item;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'delete',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", objresponse[0].msg);
                getProyectos();
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: (con + "Proyectos/Delete")
    })

}

var tbl_proyecto = {
    id: null,
    tbl_tipo_proyecto_id: null,
    proyecto: null,
    objetivo: null,
    alcance: null,
    tbl_criticidad_proyecto_id: null,
    fecha_incio: null,
    fecha_fin: null,
    tbl_estatus_proyecto_id: null,
    tbl_tipo_ejecucion_id: null,
    tbl_etapa_proyecto_id: null,
    tbl_tipo_analisis_id: null,
    tbl_nivel_analisis_id: null,
    criterio_economico: null,
    criterio_social: null,
    criterio_ambienta: null,
    criterio_politico: null,
    criterio_tecnico_institucional: null,
    tbl_dependencia_id: null

}