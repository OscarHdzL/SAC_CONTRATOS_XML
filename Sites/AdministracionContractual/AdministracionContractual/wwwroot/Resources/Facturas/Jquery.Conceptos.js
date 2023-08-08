$(document).ready(function () {
    LaunchLoader(true);
    $('#tbl_Concepto').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "10%", "targets": 0 },
            { "width": "10%", "targets": 1 },
            { "width": "30%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            { "width": "10%", "targets": 6 }
        ],
    });
    get_Conceptos();
})

var url_base = $("#EndPointAdmon").val();
function get_Conceptos() {
    var id_factura = $('#id_factura').val();

    $.get(url_base + "GeneracionXMLController/ListarById/" + id_factura, function (data, status) {

        const jsonArr = data[0].conceptos;
        const arr = JSON.parse(jsonArr);

        var Arreglo_arreglos = [];
        for (var i = 0; i <= arr.length - 1; i++) {

            var fila = [];
            fila.push(arr[i].importe);
            fila.push(arr[i].valorUnitario);
            fila.push(arr[i].descripcion);
            fila.push(arr[i].noIdentificacion);
            fila.push(arr[i].unidad);
            fila.push(arr[i].cantidad);


            fila.push("<button class='btn btn-primary' title='Modificar concepto' onclick=\"Editar_Concepto('" + data[i].Concepto + "','" + data[i].id + "');\"><i class='fa fa-edit'></i></button> <button class='btn btn-danger' title='Eliminar concepto' onclick=\"Delete_Concepto('" + data[i].id + "');\"><i class='fa fa-trash'></i></button>");
            Arreglo_arreglos.push(fila);
        }
        var table = $('#tbl_concepto').DataTable();
        table.destroy();
        $('#tbl_concepto').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "importe" },
                { title: "valorUnitario" },
                { title: "descripcion" },
                { title: "noIdentificacion" },
                { title: "unidad" },
                { title: "cantidad" },
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
    $.get("/Areas/NombreConcepto/" + id, function (data, status) {
        return window.location.replace(route);
    });
}
    

function Validar_sub() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_Concepto').val() == '') {
        Response.Texto = 'Debe ingresar una subárea.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Concepto').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Subárea"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}
function Add_Concepto() {
    var Validacion = Validar_sub();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var OBJ_S = tbl_Concepto_class;
    OBJ_S.p_tbl_area_id = $('#id_area').val();
    OBJ_S.p_Concepto = $('#txt_Concepto').val();

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
                get_Conceptos();
                $('.clr').val('');
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        url: url_base + "Area/Add/Concepto"
    })
}
$('#btn_add_Concepto').click(function () {
    Add_Concepto();
})


function Editar_Concepto(sub, id) {
    $('#id_Concepto_upd').val(id);
    $('#txt_Concepto_upd').val(sub);
    $('#Modal_Upd_Concepto').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Upd_Concepto').modal('show');
}
function Validar_sub_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_Concepto_upd').val() == '') {
        Response.Texto = 'Debe ingresar una subárea.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Concepto_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Subárea"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}
function Update_Concepto() {
    var Validacion = Validar_sub_upd();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var OBJ_S = tbl_Concepto_class;
    OBJ_S.p_id = $('#id_Concepto_upd').val();
    OBJ_S.p_tbl_area_id = $('#id_area').val();
    OBJ_S.p_Concepto = $('#txt_Concepto_upd').val();

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
                get_Conceptos();
                $('.clr').val('');
                $('#Modal_Upd_Concepto').modal('hide');
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        url: url_base + "Area/Update/Concepto"
    })
}
$('#btn_upd_Concepto').click(function () {
    Update_Concepto();
})


function Delete_Concepto(item) {
    function Confirmacion() {
        return eliminar_Concepto(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?", "Si", "No", AccionSi, "");
}
function eliminar_Concepto(item) {
    var OBJ_S = tbl_Concepto_class;
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
                get_Conceptos();
                $('.clr').val('');
            }
            else {
                ErrorSA("", "Este elemento tiene otro proceso asociado, imposible eliminarlo.");
            }
        },
        error: function () {
            ErrorSA('', "Este elemento tiene otro proceso asociado, imposible eliminarlo.")
        },
        url: url_base + "Area/Delete/Concepto"
    })
}


var tbl_Concepto_class = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_area_id: "",
    p_Concepto: ""
}