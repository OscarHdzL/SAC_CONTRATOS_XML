function muestraModalAreas(_instancia_id) {
    LaunchLoader(true);
    $('#ModalAreas').modal('show');
    $('#_instancia_id').val(_instancia_id);
    //$('#tbl_Areas').DataTable({
    //    "language": {
    //        "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
    //    },
    //    "columnDefs": [
    //        { "className": "dt-center", "targets": "_all" },
    //        { "width": "5%", "targets": 0 },
    //        { "width": "35%", "targets": 1 },
    //        { "width": "35%", "targets": 2 },
    //        { "width": "10%", "targets": 3 },
    //        { "width": "15%", "targets": 4 }
    //    ],
    //});
    get_Areas(_instancia_id);
    //GetDependencias(_instancia_id);
}



var url_base = $("#EndPointAdmon").val();

//function GetDependencias(_instancia_id) {

//    $.get(url_base + "Dependencia/Get/" + _instancia_id, function (data, status) {
//        var body = "<option disabled selected value='-1'>Seleccione...</option>";
//        for (var i = 0; i <= data.length - 1; i++) {
//            body = body + "<option value='" + data[i].id + "'>" + data[i].dependencia + "</option>";
//        }
//        $('#DependenciaA, #DependenciaA_upd').html(body);
//    });
//    //return;
//}

function get_Areas(_instancia_id) {
    //var id = null;
    //var su = null;

    //if ($('#HDSuperUsuario').val() == 'False') {
    //    su = 0
    //    id = $('#HDidDependencia').val();
    //}
    //if ($('#HDSuperUsuario').val() == 'True') {
    //    if ($('#id_dependencia_area_nav').val() != '') {
    //        su = 0
    //        id = $('#id_dependencia_area_nav').val();
    //    }
    //    else {
    //        su = 1
    //        id = $('#HDidInstancia').val();
    //    }
        
    //}
    

    $.get(url_base + "Area/Get/Lista/" + _instancia_id + "/1", function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            var sub = null;
            fila.push(i + 1);
            fila.push(data[i].dependencia);
            fila.push(data[i].area);

            //if (data[i].num_sub > 0) {
            //    sub = "<label style='color:green'>" + data[i].num_sub + "</label>" + " " + "<button class='btn btn-default' title='Listar subáreas de " + data[i].area + "' onclick=\"SubAreas('" + data[i].id + "','" + data[i].area + "');\"><i class='fa fa-list'></i></button>";
            //} else {
            //    sub = "<label style='color:red'>" + data[i].num_sub + "</label>" + " " + "<button class='btn btn-success' title='Agregar subáreas a " + data[i].area + "' onclick=\"SubAreas('" + data[i].id + "','" + data[i].area + "');\"><i class='fa fa-plus-circle'></i></button>";

            //}
            //fila.push(sub);
            fila.push("<button class='btn btn-primary' title='Modificar área' onclick=\"Editar_Area('" + data[i].id + "','" + data[i].id_dependencia + "','" + data[i].area + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar área' onclick=\"Delete_Area('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");
            Arreglo_arreglos.push(fila);

            var body = "<option value='" + data[i].id_dependencia + "'>" + data[i].dependencia + "</option>";
            $('#DependenciaA_upd').html(body);
            $('#DependenciaA').html(body);
        }
        var table = $('#tbl_Areas').DataTable();
        table.destroy();
        $('#tbl_Areas').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Dependencia" },
                { title: "Área" },
                //{ title: "Subáreas" },
                { title: "Acción" }
            ],
            columnDefs: [
                { targets: -1, className: 'dt-body-center' },
                { targets: -2, className: 'dt-body-center' }
            ]
        });
        LaunchLoader(false);
    });
}

//function SubAreas(item, id) {
//    var route = '/Areas/Subareas/' + item;
//    $.get("/Areas/NombreArea/" + id, function (data, status) {
//        return window.location.replace(route);
//    });
//}

$('#Add_area').click(function () {
    if ($('#HDSuperUsuario').val() == 'False') {
        //var id = $('#HDidDependencia').val()
        //$('#DependenciaA').val(id).prop('disabled', true);
    } else {
    }
    $('#Modal_Add_Area').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Add_Area').modal('show');
})

$('#btn_add_area').click(function () {
    AddArea();
})
function ValidarArea() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#Area').val() == '') {
        Response.Texto = 'Debe agregar un área';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Area').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Área"';
        Response.Bit = true;
        return Response;
    }
    if ($('#DependenciaA').val() == null) {
        Response.Texto = 'Debe seleccionar una dependencia';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
function AddArea() {
    var _instancia_id = $('#_instancia_id').val();
  var Validacion = ValidarArea();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_area_class;

        OBJ_Form.area = $('#Area').val();
        OBJ_Form.tbl_dependencia_id = $('#DependenciaA').val();

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'post',

            success: function (data) {
                var data_b = $.parseJSON(data);
                if (data_b[0].cod =="success") {
                    SuccessSA("Operación exitosa", 'El registro se guardado correctamente');
                    get_Areas(_instancia_id);
                    $('#Modal_Add_Area').modal('hide');
                }
                else {
                    ErrorSA("", "Ocurrio un error.");
                }
            },
            error: function () {
                ErrorSA("", "Ocurrio un error.");
            },
            url: (url_base + "Area/Add")

        })
    }
}

function Editar_Area(id_a, id_d, area) {
    $('#id_area_upd').val(id_a);
    $('#Area_upd').val(area);
    $('#DependenciaA_upd').val(id_d).prop('disabled', true)
    $('#Modal_Upd_Area').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Upd_Area').modal('show');
}
function ValidarArea_Ed() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#Area_upd').val() == '') {
        Response.Texto = 'Debe agregar un área';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#Area_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Área"';
        Response.Bit = true;
        return Response;
    }
    if ($('#DependenciaA_upd').val() == null) {
        Response.Texto = 'Debe seleccionar una dependencia';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
function UpdateArea() {
    var _instancia_id = $('#_instancia_id').val();
  var Validacion = ValidarArea_Ed();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_area_class;
        OBJ_Form.id = $('#id_area_upd').val();
        OBJ_Form.area = $('#Area_upd').val();

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'put',

            success: function (data) {
                var data_b = $.parseJSON(data);
                if (data_b[0].cod == "success") {
                    SuccessSA("Operación exitosa", 'El registro se guardado correctamente');
                    get_Areas(_instancia_id);
                    $('#Modal_Upd_Area').modal('hide');
                }
                else {
                    ErrorSA("", "Ocurrio un error.");
                }
            },
            error: function () {
                ErrorSA("", "Ocurrio un error.");
            },
            url: (url_base + "Area/Update")

        })
    }
}
$('#btn_upd_area').click(function () {
    UpdateArea();
})

function Delete_Area(item) {
    function Confirmacion() {
        return DeleteArea(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?", "Si", "No", AccionSi, "");
}
function DeleteArea(item) {
    var _instancia_id = $('#_instancia_id').val();

    var OBJ_Form = tbl_area_class;
    OBJ_Form.id = item;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'delete',

        success: function (data) {
            var data_b = $.parseJSON(data);
            if (data_b[0].cod == "success") {
                SuccessSA("Operación exitosa", 'El registro se eliminó exitosamente');
                get_Areas(_instancia_id);
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA("", "Ocurrio un error.");
        },
        url: (url_base + "Area/Delete")
    })

}

var tbl_area_class = {
    id: '00000000-0000-0000-0000-000000000000',
    tbl_dependencia_id: '',
    area: '',
    total_personal: 0,
    sueldo_promedio: 0,
    total_externos: 0,
    nivel: 0,
    id_area_padre: ''
}