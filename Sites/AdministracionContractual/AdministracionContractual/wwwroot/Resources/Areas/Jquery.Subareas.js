$(document).ready(function () {
    LaunchLoader(true);
    $('#tbl_subarea').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "5%", "targets": 0 },
            { "width": "35%", "targets": 1 },
            { "width": "35%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "15%", "targets": 4 }
        ],
    });
    get_Subareas();
})

var url_base = $("#EndPointAdmon").val();
function get_Subareas() {
    var id_area = $('#id_area').val();
    $.get(url_base + "Area/Get/Subreas/" + id_area, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {

            var fila = [];
            var sub = null;
            fila.push(i + 1);
            fila.push(data[i].area);
            fila.push(data[i].subarea);

            if (data[i].num_sub > 0) {
                sub = "<label style='color:green'>" + data[i].num_sub + "</label>" + " " + "<button class='btn btn-default' title='Listar Deptos. de " + data[i].area + "' onclick=\"Areas_sub('" + data[i].id + "','" + data[i].subarea + "');\"><i class='fa fa-list'></i></button>";
            } else {
                sub = "<label style='color:red'>" + data[i].num_sub + "</label>" + " " + "<button class='btn btn-success' title='Agregar Deptos. a " + data[i].area + "' onclick=\"Areas_sub('" + data[i].id + "','" + data[i].subarea + "');\"><i class='fa fa-plus-circle'></i></button>";

            }
            fila.push(sub);
            fila.push("<button class='btn btn-primary' title='Modificar subárea' onclick=\"Editar_subarea('" + data[i].subarea + "','" + data[i].id + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar subárea' onclick=\"Delete_subarea('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");
            Arreglo_arreglos.push(fila);
        }
        var table = $('#tbl_subarea').DataTable();
        table.destroy();
        $('#tbl_subarea').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Area" },
                { title: "Subáreas" },
                { title: "Deptos" },
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
function Areas_sub(item, id) {
    var route = '/Areas/Subordinados/' + item;
    $.get("/Areas/NombreSubarea/" + id, function (data, status) {
        return window.location.replace(route);
    });
}
    

function Validar_sub() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_subarea').val() == '') {
        Response.Texto = 'Debe ingresar una subárea.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_subarea').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Subárea"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}
function Add_subarea() {
    var Validacion = Validar_sub();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var OBJ_S = tbl_subarea_class;
    OBJ_S.p_tbl_area_id = $('#id_area').val();
    OBJ_S.p_subarea = $('#txt_subarea').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_S),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                get_Subareas();
                $('.clr').val('');
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        url: url_base + "Area/Add/Subarea"
    })
}
$('#btn_add_subarea').click(function () {
    Add_subarea();
})


function Editar_subarea(sub, id) {
    $('#id_subarea_upd').val(id);
    $('#txt_subarea_upd').val(sub);
    $('#Modal_Upd_Subarea').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Upd_Subarea').modal('show');
}
function Validar_sub_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_subarea_upd').val() == '') {
        Response.Texto = 'Debe ingresar una subárea.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_subarea_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Subárea"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}
function Update_subarea() {
    var Validacion = Validar_sub_upd();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var OBJ_S = tbl_subarea_class;
    OBJ_S.p_id = $('#id_subarea_upd').val();
    OBJ_S.p_tbl_area_id = $('#id_area').val();
    OBJ_S.p_subarea = $('#txt_subarea_upd').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_S),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                get_Subareas();
                $('.clr').val('');
                $('#Modal_Upd_Subarea').modal('hide');
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        url: url_base + "Area/Update/Subarea"
    })
}
$('#btn_upd_subarea').click(function () {
    Update_subarea();
})


function Delete_subarea(item) {
    function Confirmacion() {
        return eliminar_subarea(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?", "Si", "No", AccionSi, "");
}
function eliminar_subarea(item) {
    var OBJ_S = tbl_subarea_class;
    OBJ_S.p_id = item;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_S),
        type: 'delete',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                get_Subareas();
                $('.clr').val('');
            }
            else {
                ErrorSA("", "Este elemento tiene otro proceso asociado, imposible eliminarlo.");
            }
        },
        error: function () {
            ErrorSA('', "Este elemento tiene otro proceso asociado, imposible eliminarlo.")
        },
        url: url_base + "Area/Delete/Subarea"
    })
}


var tbl_subarea_class = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_area_id: "",
    p_subarea: ""
}