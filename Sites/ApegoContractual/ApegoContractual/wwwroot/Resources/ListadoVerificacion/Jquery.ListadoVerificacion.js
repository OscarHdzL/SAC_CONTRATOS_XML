$.extend($.fn.dataTable.defaults, {
    responsive: true
});
function Redimension() {
    try {
        var tables = document.getElementsByTagName('table');
        for (var i = 0; i < tables.length; i++) {
            $('#' + tables[i].id + '').resize();
        }
    }
    catch (err) { }
}
$(document).ready(function () {
    $('#ListadoPreguntas').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "90%", "targets": 0 },
        ],
    });
    setInterval('Redimension()', 500);
    //GetProposionesEva();
    getDependenciasAsignadasUsuario();
});
// dependencias del usuario

function getDependenciasAsignadasUsuario() {
    $.get($('#EndPointAC_Admon').val() + "Usuarios/Get/DependenciasAsignadas/Usuario/" + $("#HDidUsuario").val(), function (data, status) {
        for (var i = 0; i < data.length; i++) {
            var body = "<option selected value=''>Seleccione...</option>";
            for (var i = 0; i <= data.length - 1; i++) {
                body = body + "<option value='" + data[i].tbl_dependencia_id + "' >" + data[i].nombre_dependencia + "</option>";
            }
            $("#dependencias_usuario").html(body);
        }

    });
}

$("#dependencias_usuario").change(function () {
    if ($(this).val() != '') {
        $("#dependenciaVerificacion").val($(this).val());
        GetProposionesEva();
    }

});


//*************************************************AGREGAR DE LOS PUNTOS A EVALUAR***************************************************//

$('#AddPregunta').click(function () {
    if ($('#dependenciaVerificacion').val() == '') {
        return ErrorSA('Error', 'Debe seleccionar una dependencia');
    }
    else {
        $('#ModalAddPuntoAverificar').modal({ backdrop: 'static', keyboard: false });
        $('#ModalAddPuntoAverificar').modal('show');
    }

})

//$('#DropDowDependencia').click(function () {
//    GetProposionesEva($('#DropDowDependencia').val())
//})

function Validar() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txtPuntoAver').val() == '') {
        Response.Texto = 'Debe agregar un punto a verificar';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#txtPuntoAver').val(), 'Punto a evaluar') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Punto a evaluar"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

$('#AddPuntoAVerificar').click(function () {
    AddPuntoAEvaluar();
})
function CerrarModalLV() {
    $('#ModalAddPuntoAverificar').modal('hide');
}

function AddPuntoAEvaluar() {
    var Validacion = Validar();
    if (Validacion.Bit) {
        $('#txtPuntoAver').val('')
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);

    }
    var OBJ_PregForm = Preg_FormClass;
    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

    OBJ_PregForm.p_opt = 2;
    OBJ_PregForm.p_id = '00000000-0000-0000-0000-000000000000';
    OBJ_PregForm.p_tbl_dependencia_id = $('#dependenciaVerificacion').val();
    OBJ_PregForm.p_pregunta = $('#txtPuntoAver').val();
    OBJ_PregForm.p_inclusion = date;
    OBJ_PregForm.p_estatus = 1;

    //var form_data = new FormData();
    //form_data.append('PregForm', JSON.stringify(OBJ_PregForm));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_PregForm),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                function conf() {
                    return CerrarModalLV()
                }
                var AccionSi = eval(conf)
                SuccessSAAction("Operación exitosa", objresponse[0].msg, AccionSi);
                $('#txtPuntoAver').val('')
                GetProposionesEva();
            }
            else {
                ErrorSA("", objresponse[0].msg);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        //processData: false,
        //type: 'POST',
        url: ($('#EndPointAC').val() +"SerPreguntasForm/add")

    })
}

$('.Clear').click(function () {
    $('#txtPuntoAver').val('');
})

//*********************************************FIN AGREGAR DE LOS PUNTOS A EVALUAR***************************************************


//*************************************************LISTA DE LOS PUNTOS A EVALUAR*****************************************************

function GetProposionesEva() {
    var id = $('#dependenciaVerificacion').val();
    $.get($('#EndPointAC').val() + "SerVerificacion/Get/Lista/" + id, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            Interno.push("<span style='word-wrap:break-word'>" + data[i].pregunta + "</span>");
            Interno.push('<a id="btnAddLV_' + data[i].idpregunta + '" onclick="AddEditado(\'' + data[i].idpregunta + '\', \'' + data[i].tbl_dependencia_id + '\', \'' + data[i].pregunta + '\')" class="fa fa-edit btn btn-primary" title="Guardar cambios"> </a> <a onclick="DeletePEV(\'' + data[i].idpregunta + '\')"   class="fa fa-trash-o btn btn-danger" title="Eliminar Registro"></a>');
            Arreglo_arreglos.push(Interno);
        }

        $('#ListadoPreguntas').DataTable().destroy();

        //table.destroy();
        console.log(Arreglo_arreglos);
        $('#ListadoPreguntas').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "Puntos de verificación", "width": "90%" },
                { title: "Acciones", "width": "15%" }
            ]
        });

    });

}

//***********************************************FIN LISTA DE LOS PUNTOS A EVALUAR***************************************************//


function EliminarPEV(item) {
    var OBJ_PregForm = Preg_FormClass;

    OBJ_PregForm.p_opt = 4;
    OBJ_PregForm.p_id = item;
    OBJ_PregForm.p_estatus = 0;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_PregForm),
        type: 'delete',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                $('#Idpunto').val('');
                $('#iddep').val('');
                $('#txtPuntoAverMod').val('');
                function conf() {                    
                }
                var si = eval(conf);
                SuccessSAAction("Operación exitosa", objresponse[0].msg, si);
                GetProposionesEva($('#DropDowDependencia').val())//verificar
            }
            else {
                ErrorSA("", objresponse[0].msg);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        //processData: false,
        //type: 'POST',
        url: ($('#EndPointAC').val() + "SerPreguntasForm/delete")

    })
}

function DeletePEV(item) {
    function Confirmacion() {
        return EliminarPEV(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

//********************************************MODIFICACIÓN DE LOS PUNTOS A EVALUAR***************************************************//

function AddPEVEditado() {
    if ($('#txtPuntoAverMod').val() == '') {
        return ErrorSA('Error en los datos de entrada', 'El campo no puede quedar vacio')
    }
    if (ValidaCadenab($('#txtPuntoAverMod').val(), '') != '') {
        return ErrorSA('Error en los datos de entrada', 'El campo no puede contener caracteres especiales')
    }
    var OBJ_PregForm = Preg_FormClass;
    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

    OBJ_PregForm.p_opt = 3;
    OBJ_PregForm.p_id = $('#Idpunto').val();;
    OBJ_PregForm.p_tbl_dependencia_id = $('#iddep').val();;
    OBJ_PregForm.p_pregunta = $('#txtPuntoAverMod').val();
    OBJ_PregForm.p_inclusion = date;
    OBJ_PregForm.p_estatus = 1;

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_PregForm),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
                if (objresponse[0].cod == "success") {
                $('#Idpunto').val('');
                $('#iddep').val('');
                $('#txtPuntoAverMod').val('');
                function conf() {
                    return cerrarModalMod();
                }
                var si = eval(conf);
                SuccessSAAction("Operación exitosa", objresponse[0].msg, si);
                GetProposionesEva();
            }
            else {
                ErrorSA("", objresponse[0].msg);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        //processData: false,
        //type: 'POST',
        url: ($('#EndPointAC').val() +"SerPreguntasForm/update")

    })
}
function cerrarModalMod() {
    $('#ModalModidicarPuntoAverificar').modal('hide');
}
function AddEditado(id, idDep, Punto) {
    $('#ModalModidicarPuntoAverificar').modal({ backdrop: 'static', keyboard: false });
    $('#ModalModidicarPuntoAverificar').modal('show');

    $('#Idpunto').val(id);
    $('#iddep').val(idDep);
    $('#txtPuntoAverMod').val(Punto);
}

$('#ModPuntoAVerificar').click(function () {
    AddPEVEditado();
})

//*****************************************FIN MODIFICACIÓN DE LOS PUNTOS A EVALUAR***************************************************//


var Preg_FormClass = {
    p_opt: null,
    p_id: null,
    p_tbl_dependencia_id: null,
    p_pregunta: null,
    p_inclusion: null,
    p_estatus: null
}