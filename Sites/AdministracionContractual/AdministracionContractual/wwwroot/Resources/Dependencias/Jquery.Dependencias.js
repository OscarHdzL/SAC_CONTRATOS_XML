$(document).ready(function () {
    //LaunchLoader(true);
    urls();
    AjusteTabla();
    //GetDependencias();
    GetInstancias_fil();
    obtenerEstados();    
});
var con = $("#EndPointAdmon").val();
function urls(){    
    URL_OBTENER_ESTADOS = con + "Dependencia/Get/Estados";
    URL_OBTENER_CIUDADES = con + "Dependencia/Get/Ciudades/Estado/";
    URL_OBTENER_EJERCICIO = con + "Dependencia/Get/Ejercicio";
}

function AjusteTabla() {
    $('#tbl_Dependencias').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "10%", "targets": 0 },
            { "width": "40%", "targets": 1 },
            { "width": "10%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "20%", "targets": 5 }
        ],
    });
    //$('#tbl_Areas').DataTable({
    //    "language": {
    //        "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
    //    },
    //    "columnDefs": [
    //        { "className": "dt-center", "targets": "_all" },
    //        { "width": "10%", "targets": 0 },
    //        { "width": "45%", "targets": 1 },
    //        { "width": "15%", "targets": 2 },
    //        { "width": "15%", "targets": 3 },
    //        { "width": "15%", "targets": 4 }
    //    ],
    //});
    $('#tbl_partidas').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "10%", "targets": 0 },
            { "width": "80%", "targets": 1 },
            { "width": "10%", "targets": 2 }
        ],
    });
}

function obtenerEstados(ed) {

    $.get(URL_OBTENER_ESTADOS, function (data, status) {

        var body = "<option disabled value=''>Seleccione...</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#Estado').html(body);
        $('#Estado_ed').html(body); 
        $('#Estado_ed > option[value="' + ed + '"]').attr("selected", "selected"); 
    });
}

function GetInstancias_fil() {
    $.get($("#EndPointAdmon").val() + "Instancia/Get_Drop", function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#instancia_dep_filtro').html(body);
    });
    return;
}
$("#instancia_dep_filtro").on("change", function () {
    GetDependencias($("#instancia_dep_filtro").val());
    $('.box-body').prop('hidden', false);
    $('.temporal').prop('hidden', true);
});

function GetInstancias(ed) {
    $.get($("#EndPointAdmon").val() + "Instancia/Get_Drop", function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#instancia_dep').html(body);
        $('#instancia_dep_ed').html(body);
        $('#instancia_dep_ed > option[value="' + ed + '"]').attr("selected", "selected"); 
    });
    return;
}

$("#Estado").on("change", function () {
    obtenerCiudades($("#Estado").val());
    $('#Ciudad').prop("disabled", false);
});

function obtenerCiudades(id,ed) {

    $.get(URL_OBTENER_CIUDADES + id, function (data, status) {
        console.log(data)
        var body = "<option disabled selected value=''>Seleccione...</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#Ciudad').html(body);
        $('#Ciudad_ed').html(body);
        $('#Ciudad_ed > option[value="' + ed + '"]').attr("selected", "selected");
    });
}

function muestraModalAgregarDependencia() {    
    GetInstancias();
    $('.Clean').val('');
    $('#Ciudad').html('');
    $('#Ciudad').prop("disabled", true);
    $('#ModalAgregarDependencia').modal('show');
}

//function SubAreas(item) {
//    getSub_Areas(item);
//    $('#ModalAreas').modal('show');
//}

function GetDependencias(inst) {

    $.get(con + "Dependencia/Get/" + inst, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var ir = null;
            var ir_b = null;
            var Interno = [];
            Interno.push(i + 1);
            Interno.push(data[i].dependencia);

            ir = "<button class='btn btn-default' title='Áreas' onclick=\"muestraModalAreas('" + data[i].tbl_instancia_id + "');\"><i class='glyphicon glyphicon-list'></i></button>"

            //if (data[i].num_hijas > 0) {
            //    ir = "<button class='btn btn-default' title='Áreas' onclick=\"muestraModalAreas('" + data[i].tbl_instancia_id + "');\"><i class='glyphicon glyphicon-list'></i></button>"
            //    //ir = "<button class='btn btn-default' title='Tiene áreas'><i class='glyphicon glyphicon-list'></i></button>"
            //} 
            Interno.push(ir);
            //if (data[i].puestos > 0) {
            ir_b = "<button class='btn btn-default' title='Capítulos de gasto' onclick=\"muestrapartidas('" + data[i].id + "');\"><i class='glyphicon glyphicon-list-alt'></i></button>"
            //}
            Interno.push(ir_b);
            Interno.push("<button class='btn btn-default' title='Administrador de dependencias' onclick=\"muestraModalUsuariosAdmin('" + data[i].tbl_instancia_id + "');\"><i class='glyphicon glyphicon-list-alt'></i></button>");
            Interno.push("<button class='btn btn-primary' title='Editar' onclick=\"muestraModalEditarDep('" + data[i].id + "/" + data[i].dependencia + "/" + data[i].tbl_ciudad_id + "/" + data[i].id_estado + "/" + data[i].tbl_instancia_id + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar' onclick=\"DeleteItemDep('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");
            Arreglo_arreglos.push(Interno);
        }
        var table = $('#tbl_Dependencias').DataTable();
        table.destroy();
        console.log(Arreglo_arreglos);
        $('#tbl_Dependencias').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Dependencia" },
                { title: "Áreas" },
                { title: "Capítulos de gasto" },
                { title: "Administrador de dependencias" },
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
//function IrDependenciaArea(id) {

//    //return window.location.href = "/Areas/Index/" + item;
//    var route = '/Areas/Index';
//    $.get("/Dependencias/Dependencias_areas/" + id, function (data, status) {
//        return window.location.replace(route);
//    });
//}

$("#GuardarDependencia").click(function () {
    AddDependencia();
});
$("#EditarDependencia").click(function () {
    EditDependencia();
});
$("#Close").click(function () {
    $('.Clean').val('');
});
$("#Close_part").click(function () {
    $('.Clean').val('');
});

function AddDependencia() {
    var Validacion = ValidarDep();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_dep_class;
        OBJ_Form.id = '00000000-0000-0000-0000-000000000000';
        OBJ_Form.dependencia = $('#Dependencia').val();
        OBJ_Form.tbl_ciudad_id = $('#Ciudad').val();
        OBJ_Form.tbl_instancia_id = $('#instancia_dep').val();
        
        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'post',

            success: function (data) {
                var data_b = $.parseJSON(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", data_b[0].msg);
                    GetDependencias();
                    $('#ModalAgregarDependencia').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Dependencia/Add")

        })
    }
}

function muestraModalEditarDep(string) {
    item = string.split('/');
    obtenerEstados(item[3]);    
    obtenerCiudades(item[3], item[2]);
    GetInstancias(item[4]);
    $('#id_dep_edit').val(item[0]);
    $('#Dependencia_ed').val(item[1]);
    $('#ModalEditarDependencia').modal('show');
}

function EditDependencia() {
    var Validacion = ValidarDep_Ed();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_dep_class;
        OBJ_Form.id = $('#id_dep_edit').val();
        OBJ_Form.dependencia = $('#Dependencia_ed').val();
        OBJ_Form.tbl_ciudad_id = $('#Ciudad_ed').val();
        OBJ_Form.tbl_instancia_id = $('#instancia_dep_ed').val();

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'put',

            success: function (data) {
                var data_b = $.parseJSON(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", data_b[0].msg);
                    GetDependencias();
                    $('#ModalEditarDependencia').modal('hide');                    
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Dependencia/Update")

        })
    }
}

function DeleteItemDep(item) {
    function Confirmacion() {
        return DeleteDep(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?", "Si", "No", AccionSi, "");
}

function DeleteDep(item) {
    var OBJ_Form = tbl_dep_class;
    OBJ_Form.id = item;
    OBJ_Form.dependencia = '';
    OBJ_Form.tbl_ciudad_id = '';
    OBJ_Form.tbl_instancia_id = '';

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'delete',

        success: function (data) {
            var data_b = $.parseJSON(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", data_b[0].msg);
                GetDependencias();
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: (con + "Dependencia/Delete")
    })

}

function ValidarDep() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#Dependencia').val() == '') {
        Response.Texto = 'Debe agregar una dependencia';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Dependencia').val(), 'Dependencia') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Dependencia"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Estado').val() == '') {
        Response.Texto = 'Debe seleccionar un estado';
        Response.Bit = true;
        return Response;
    }
    if ($('#Ciudad').val() == '') {
        Response.Texto = 'Debe seleccionar una ciudad';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function ValidarDep_Ed() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#Dependencia_ed').val() == '') {
        Response.Texto = 'Debe agregar una dependencia';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Dependencia_ed').val(), 'Dependencia_ed') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Dependencia"';
        Response.Bit = true;
        return Response;
    }
    if ($('#Estado_ed').val() == '') {
        Response.Texto = 'Debe seleccionar un estado';
        Response.Bit = true;
        return Response;
    }
    if ($('#Ciudad_ed').val() == '') {
        Response.Texto = 'Debe seleccionar una ciudad';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}


//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
//function getSub_Areas(item) {
//    $.get(con + "Area/Get_Sub/" + item, function (data, status) {
//        var Arreglo_arreglos = [];
//        for (var i = 0; i <= data.length - 1; i++) {
//            var ir = null;
//            var ir_b = null;
//            var Interno = [];
//            Interno.push(i + 1);
//            Interno.push(data[i].area);
//            if (data[i].num_hijas > 0) {
//                ir = "<button class='btn btn-default' title='Editar' onclick=\"SubAreas('" + data[i].id + "');\"><i class='glyphicon glyphicon-list'></i></button>"
//            }
//            Interno.push(ir);
//            if (data[i].puestos > 0) {
//                ir_b = "<button class='btn btn-default' title='Editar' onclick=\"Puestos('" + data[i].id + "');\"><i class='glyphicon glyphicon-list'></i></button>"
//            }
//            Interno.push(ir_b);
//            Interno.push("<button class='btn btn-primary' title='Editar' onclick=\"muestraModalEditar('" + data[i].id + "/" + data[i].tbl_dependencia_id + "/" + data[i].area + "/" + data[i].id_area_padre + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar' onclick=\"DeleteItemArea('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");
//            Arreglo_arreglos.push(Interno);
//        }
//        var table = $('#tbl_Sub_Areas').DataTable();
//        table.destroy();
//        console.log(Arreglo_arreglos);
//        $('#tbl_Sub_Areas').DataTable({
//            "language": {
//                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
//            },
//            data: Arreglo_arreglos,
//            columns: [
//                { title: "No." },
//                { title: "Área" },
//                { title: "Sub-Áreas" },
//                { title: "Puestos" },
//                { title: "Acción" }
//            ],
//            columnDefs: [
//                {
//                    targets: '_all',
//                    className: 'dt-body-center'
//                }]
//        });
//    });
//}

function muestrapartidas(item) {  
    $('.Clean').val('');
    $('#tbl_partidas').html('');
    GetEjercicio();
    $("#id_dep_part").val(item);
    $('#ModalPartida').modal('show');
}

function GetEjercicio() {
    $.get(URL_OBTENER_EJERCICIO, function (data, status) {
        console.log(data)
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#Ejercicio').html(body);
    });
}
$("#Ejercicio").on("change", function () {
    GetPartidas($("#Ejercicio").val());
});

function GetPartidas(item) {
    $.get(con + "Dependencia/Get_list_partida/" + item + "/" + $('#HDidInstancia').val() + "/"+ $("#id_dep_part").val(), function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var ir = null;
            var Interno = [];
            Interno.push(i + 1);
            Interno.push(data[i].numero + " - " + data[i].descripcion);            
            if (data[i].chek != null) {
                ir = "<button class='btn btn-default' title='Desactivar' onclick=\"Des_Partida('" + data[i].chek + "');\"><i class='glyphicon glyphicon-check' style='color: #00a65a'></i></button>"
            } else {
                ir = "<button class='btn btn-default' title='Activar' onclick=\"Act_Partida('" + data[i].id + "');\"><i class='glyphicon glyphicon-unchecked'></i></button>"
            }
            Interno.push(ir);
            //Interno.push('<input type="checkbox" class="flat-red">')
            Arreglo_arreglos.push(Interno);
        }
        var table = $('#tbl_partidas').DataTable();
        table.destroy();
        console.log(Arreglo_arreglos);
        $('#tbl_partidas').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Capitulos" },
                { title: "Acción" }
            ],
            columnDefs: [
                {
                    targets: -3, className: 'dt-body-center'
                },
                {
                    targets: -1, className: 'dt-body-center'
                }]
        });
    });
}
function Act_Partida(item) {

    var OBJ_Form = tbl_capitulo_gasto_dependencia;
    OBJ_Form.p_tbl_dependencia_id = $('#id_dep_part').val();
    OBJ_Form.p_tbl_capitulo_gasto_id = item;
    OBJ_Form.p_tbl_ejercicio_id = $('#Ejercicio').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                //SuccessSA("Operación exitosa", data_b[0].msg);
                GetPartidas($("#Ejercicio").val());                
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA("", "Ocurrio un error.");
        },
        url: (con + "Estructura/Add/CapituloG/Dependencia")

    })    
}

function Des_Partida(item) {
    var OBJ_Form = tbl_capitulo_gasto_dependencia;
    OBJ_Form.p_id = item;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'delete',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                //SuccessSA("Operación exitosa", data_b[0].msg);
                GetPartidas($("#Ejercicio").val());
            }
            else {
                ErrorSA("", objresponse[0].msg);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: (con + "Estructura/Delete/CapituloG/Dependencia")
    })
}

var tbl_partida_class = {      
      id: null,
      tbl_dependencia_id: null,
      tbl_partida_id: null,
      tbl_ejercicio_id: null
}

var tbl_capitulo_gasto_dependencia = {
    p_opt: 0,                   
    p_id: '00000000-0000-0000-0000-000000000000',                   
    p_tbl_dependencia_id: '',    
    p_tbl_capitulo_gasto_id: '',   
    p_tbl_ejercicio_id: '',      
    p_monto_asignado: 0        
}

var tbl_dep_class = {
    id: null,
    dependencia: null,
    tbl_ciudad_id: null,
    tbl_instancia_id: null
}