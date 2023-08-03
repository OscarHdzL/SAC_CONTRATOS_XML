$(document).ready(function () {
    LaunchLoader(true);
    $('#tbl_area_sub').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "5%", "targets": 0 },
            { "width": "40%", "targets": 1 },
            { "width": "40%", "targets": 2 },
            { "width": "15%", "targets": 3 },
        ],
    });
    get_areas_sub();
})

var url_base = $("#EndPointAdmon").val();
function get_areas_sub() {
    var id_area_sub = $('#id_subarea').val();
    $.get(url_base + "Area/Get/Areasubordinada/" + id_area_sub, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {

            var fila = [];
            var sub = null;
            fila.push(i + 1);
            fila.push(data[i].subarea);
            fila.push(data[i].area_subordinada);
            fila.push("<button class='btn btn-primary' title='Modificar subárea' onclick=\"Editar_area_sub('" + data[i].id + "', '" + data[i].area_subordinada + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar' onclick=\"Delete_area_sub('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");
            Arreglo_arreglos.push(fila);
        }
        var table = $('#tbl_area_sub').DataTable();
        table.destroy();
        $('#tbl_area_sub').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Subáreas." },
                { title: "Dpto." },
                { title: "Acción" }
            ],
            columnDefs: [
                { targets: -1, className: 'dt-body-center' }
            ]
        });
        LaunchLoader(false);
    });
}

function Validar_area_sub() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_area_sub').val() == '') {
        Response.Texto = 'Debe ingresar una subárea.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_area_sub').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Subárea"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}
function Add_area_sub() {
    var Validacion = Validar_area_sub();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var OBJ_AS = tbl_area_sub_class;
    OBJ_AS.p_tbl_subarea_id = $('#id_subarea').val();
    OBJ_AS.p_area_subordinada = $('#txt_area_sub').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_AS),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                get_areas_sub();
                $('.clr_as').val('');
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        url: url_base + "Area/Add/Subordinado"
    })
}
$('#btn_add_area_sub').click(function () {
    Add_area_sub();
})


function Editar_area_sub(id, area_sub) {
    $('#id_area_subordinada_upd').val(id);
    $('#txt_area_sub_upd').val(area_sub);
    $('#Modal_Upd_area_subordinada').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Upd_area_subordinada').modal('show');
}
function Validar_area_sub_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_area_sub_upd').val() == '') {
        Response.Texto = 'Debe ingresar una subárea.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_area_sub_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Subárea"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}
function Update_area_sub() {
    var Validacion = Validar_area_sub_upd();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var OBJ_AS = tbl_area_sub_class;
    OBJ_AS.p_id = $('#id_area_subordinada_upd').val();
    OBJ_AS.p_tbl_subarea_id = $('#id_subarea').val();
    OBJ_AS.p_area_subordinada = $('#txt_area_sub_upd').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_AS),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                get_areas_sub();
                $('.clr_as').val('');
                $('#Modal_Upd_area_subordinada').modal('hide');
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        url: url_base + "Area/Update/Subordinado"
    })
}
$('#btn_upd_area_sub').click(function () {
    Update_area_sub();
})

function Delete_area_sub(item) {
    function Confirmacion() {
        return eliminar_area_sub(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?", "Si", "No", AccionSi, "");
}
function eliminar_area_sub(item) {
    var OBJ_AS = tbl_area_sub_class;
    OBJ_AS.p_id = item;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_AS),
        type: 'delete',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                get_areas_sub();
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        url: url_base + "Area/Delete/Subordinado"
    })
}

var tbl_area_sub_class = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_subarea_id: "",
    p_area_subordinada: ""
}