$(document).ready(function () {
    $('#MatrizRiesgos').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "10%", "targets": 0 },
            { "width": "60%", "targets": 1 },
            { "width": "5%", "targets": 2 },
            { "width": "5%", "targets": 3 },
            { "width": "5%", "targets": 4 },
            { "width": "5%", "targets": 5 },
            { "width": "5%", "targets": 6 },
            { "width": "5%", "targets": 7 },

        ],
    })

});
$('#AddRiesgo').click(function () {
    $('.Clean').val('');
    GetProbabilidad();
    GetImpacto();
    //GetMatriz($('#idObligacionVPMtzR').val());
    $('#tituloM').text('Agregar un nuevo riesgo');
    $('#AddNuegoR').text('Registrar Riesgo');
    $('#ModalNuevoRiesgo').modal({ backdrop: 'static', keyboard: false });
    $('#ModalNuevoRiesgo').modal('show');
    
})
$('#VistaPMtz').click(function () {
    $('#VistaParMtzR').modal({ backdrop: 'static', keyboard: false });
    $('#VistaParMtzR').modal('show');
})

function GetCatRespuesta() {
    $.get($('#EndPointAC').val() + "Riesgos/Get/NivelRiesgo", function (data, status) {
        var BodyC = "<option disabled selected value=''>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            BodyC = BodyC + "<option value='" + data[i].id + "'>" + data[i].nivel_riesgo + "</option>";
        }
        $('.CatRiesgo').html(BodyC);
    }, 'json');
}
function GetProbabilidad() {
    var BodyP = "<option disabled selected>Selecciona una opción</option>";
    BodyP = BodyP + "<option value='1'>1</option>" + " <option value='2'>2</option> " + " <option value='3'>3</option>";
    $('.Probabilidad').html(BodyP);
}
function GetImpacto() {
    var BodyI = "<option disabled selected>Selecciona una opción</option>";
    var BodyI = BodyI + "<option value='1'>1</option> " + " <option value='2'>2</option> " + " <option value='3'>3</option>";
    $('.Impacto').html(BodyI);
}

function GetTipoRespuesta() {
    $.get($('#EndPointAC').val() + "Riesgos/Get/TipoRespuesta", function (data, status) {
        var BodyT = "<option disabled selected value=''>Selecciona una opción</option>";;
        for (var i = 0; i <= data.length - 1; i++) {
            BodyT = BodyT + "<option value='" + data[i].id + "'>" + data[i].tipo_respuesta + "</option>";
        }
        $('.TipoRespuesta').html(BodyT);
    }, 'json');
}

$('#Probabilidad, #Impacto').change(function () {
    var Prov = parseInt($('.Probabilidad').val());
    var Imp = parseInt($('.Impacto').val());
    var resultado = (Prov + Imp) / 2;
    $('#txtPrioridad').val(resultado == isNaN ? 0 : resultado);
})

function Validar() {

    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txtRiesgo').val() == '') {
        Response.Texto = 'Debe agregar un riesgo identificado';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtRiesgo').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Riesgo"';
        Response.Bit = true;
        return Response;
    }

    if (($('.CatRiesgo').val() == '') || ($('.CatRiesgo').val() == null)) {
        Response.Texto = 'Debe seleccionar una categorización del riesgo';
        Response.Bit = true;
        return Response;
    }
    if (($('.Probabilidad').val() == '') || ($('.Probabilidad').val() == null)) {
        Response.Texto = 'Debe seleccionar una probabilidad';
        Response.Bit = true;
        return Response;
    }

    if (($('.Impacto').val() == '') || ($('.Impacto').val() == null)) {
        Response.Texto = 'Debe seleccionar un impacto';
        Response.Bit = true;
        return Response;
    }

    //if ($('.Obligaciones').val() == '') {
    //    Response.Texto = 'Debe seleccionar una obligación';
    //    Response.Bit = true;
    //    return Response;
    //}
    //if ($('.Responsable').val() == '') {
    //    Response.Texto = 'Debe seleccionar un responsable';
    //    Response.Bit = true;
    //    return Response;
    //}
    if (($('.TipoRespuesta').val() == '') || ($('.TipoRespuesta').val() == null)) {
        Response.Texto = 'Debe seleccionar un tipo de respuesta';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtAccioM').val() == '') {
        Response.Texto = 'Debe agregar una acción';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtAccioM').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Acción"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txtAccionCertificacion').val() == '') {
        Response.Texto = 'Debe agregar un resultado de acción';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txtAccionCertificacion').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Resultado de acción"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

$('#AddNuegoR').click(function () {
    AddMatriz();
})

function AddMatriz() {
    var Validacion = Validar();
    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

 /*   var OBJ_Matriz = MatrizClass;
    var d = new Date();

    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());
    if ($('#IdMR').val() == '') {
        OBJ_Matriz.id = '00000000-0000-0000-0000-000000000000';
    }
    else {
        OBJ_Matriz.id = $('#IdMR').val();
    }
    OBJ_Matriz.Riesgo = $('#txtRiesgo').val();
    OBJ_Matriz.tbl_CategorizacionRiesgo_ac_id = $('.CatRiesgo').val();
    OBJ_Matriz.Probabilidad = $('.Probabilidad').val();
    OBJ_Matriz.Impacto = $('.Impacto').val();

    var Prov = parseInt($('.Probabilidad').val());
    var Imp = parseInt($('.Impacto').val());
    var resultado = (Prov + Imp) / 2;

    OBJ_Matriz.Prioridad = (resultado);

    OBJ_Matriz.tbl_LinkObligaciones_ac_id = $('#idObligacionVPMtzR').val();
    OBJ_Matriz.tbl_TipoRespuesta_ac_id = $('.TipoRespuesta').val();
    OBJ_Matriz.Accion = $('#txtAccioM').val();
    OBJ_Matriz.Respuesta = $('#txtAccionCertificacion').val();
    OBJ_Matriz.tbl_RespApego_Contrato_ac_id = $('.Responsable').val();

    OBJ_Matriz.TBLENT_DEPENDENCIA_id = $('#HDidDependencia').val();
    OBJ_Matriz.TBLENT_CONTRATO_id = $('#IdContrato').val();
    OBJ_Matriz.Inclusion = date;
    OBJ_Matriz.Estatus = 1;
    */
    var OBJ_Matriz = Matriz_Riesgo_Class;
    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

    if ($('#IdMR').val() == '') {
        OBJ_Matriz.p_opt = 2;
        OBJ_Matriz.p_id = '00000000-0000-0000-0000-000000000000';
    }
    else {
        OBJ_Matriz.p_opt = 3;
        OBJ_Matriz.p_id = $('#IdMR').val();
    }


    
    
    OBJ_Matriz.p_riesgo = $('#txtRiesgo').val();
    OBJ_Matriz.p_tbl_nivel_riesgo_id = $('.CatRiesgo').val();
    OBJ_Matriz.p_probabilidad = $('.Probabilidad').val();
    OBJ_Matriz.p_impacto = $('.Impacto').val();
    OBJ_Matriz.p_tbl_tipo_respuesta_id = $('.TipoRespuesta').val();

    var Prov = parseInt($('.Probabilidad').val());
    var Imp = parseInt($('.Impacto').val());
    var resultado = (Prov + Imp) / 2;

    OBJ_Matriz.p_prioridad = (resultado);
    OBJ_Matriz.p_tbl_link_obligacion_id = $('#idObligacionVPMtzR').val();
    OBJ_Matriz.p_respuesta = $('#txtAccionCertificacion').val();
    OBJ_Matriz.p_accion = $('#txtAccioM').val();
    OBJ_Matriz.p_estatus = 1;

    console.log(JSON.stringify(OBJ_Matriz));
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Matriz),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", "El registro se guardo correctamente");
                
                GetMatriz($('#idObli').val());
                $('.Clean').val('');
                $('#IdMR').val('');
                $('#ModalNuevoRiesgo').modal('hide');
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        processData: false,
        type: 'POST',
        url: $('#EndPointAC').val() + "Riesgos/Add/"

    })
}

$('#cerrarMMR').click(function () {
    $('.Clean').val('');
    $('#IdMR').val('');
})

function GetMatriz(idObl) {

    $.get($('#EndPointAC').val() + "Riesgos/Get/" + idObl, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            Interno.push(i + 1);
            //Interno.push(data[i].tbl_LinkObligaciones_ac.tbl_Obligaciones_ac.Obligacion);
            Interno.push(data[i].riesgo);
            Interno.push(data[i].nivel_riesgo);
            Interno.push(data[i].probabilidad);
            Interno.push(data[i].impacto);
            Interno.push(data[i].prioridad);
            Interno.push(data[i].tipo_respuesta);                        
            Interno.push(' <a onclick="btnDeleteM(\'' + data[i].id + '\')" class="fa fa-trash-o btn btn-danger" title="Eliminar registro"> </a>');
            Arreglo_arreglos.push(Interno);
        }

        var table = $('#MatrizRiesgos').DataTable();

        table.destroy();
        console.log(Arreglo_arreglos);
        $('#MatrizRiesgos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                //{ title: "Obligación" },
                { title: "Riesgo" },
                { title: "Categorización" },
                { title: "Probabilidad" },
                { title: "Impacto" },
                { title: "Prioridad" },
                { title: "Respuesta"},
                { title: "Acciones" },
            ]
        });

    });

}

function btnEditM(item) {
    $('#ModalNuevoRiesgo').modal({ backdrop: 'static', keyboard: false });
    $('#ModalNuevoRiesgo').modal('show');
    $('#IdMR').val(item);
    $('#tituloM').text('Modificar un Riesgo');
    $('#AddNuegoR').text('Modificar Riesgo');
    
    var idCon = $('#IdContrato').val();
    
    $.get("/Request/Matriz/GetListaEditMR/" + idCon + "/" + item, function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {

            $('#txtRiesgo').val(data[i].Riesgo);
            $('.CatRiesgo').val(data[i].tbl_CategorizacionRiesgo_ac.id);
            $('.Probabilidad').val(data[i].Probabilidad);
            $('.Impacto').val(data[i].Impacto);
            $('.Responsable').val(data[i].tbl_RespApego_Contrato_ac.id);
            $('#txtPrioridad').val(data[i].Prioridad);
            $('.Obligaciones').val(data[i].tbl_LinkObligaciones_ac.tbl_Obligaciones_ac.Obligacion);
            $('.TipoRespuesta').val(data[i].tbl_TipoRespuestaMR_ac.id);
            $('#txtAccioM').val(data[i].Accion);
            $('#txtAccionCertificacion').val(data[i].Respuesta);
        }
    });
}

function EliminarMR(item) {


    //var OBJ_Matriz = Matriz_Riesgo_Class;

    //OBJ_Matriz.p_opt = 4;
    //OBJ_Matriz.p_id = item;
    //OBJ_Matriz.p_riesgo = '-';
    //OBJ_Matriz.p_tbl_nivel_riesgo_id = '00000000-0000-0000-0000-000000000000';
    //OBJ_Matriz.p_probabilidad = 0;
    //OBJ_Matriz.p_impacto = 0;
    //OBJ_Matriz.p_tbl_tipo_respuesta_id = '00000000-0000-0000-0000-000000000000';

    //var Prov = parseInt($('.Probabilidad').val());
    //var Imp = parseInt($('.Impacto').val());
    //var resultado = (Prov + Imp) / 2;

    //OBJ_Matriz.p_prioridad = '';
    //OBJ_Matriz.p_tbl_link_obligacion_id = '00000000-0000-0000-0000-000000000000';
    //OBJ_Matriz.p_respuesta = '';
    //OBJ_Matriz.p_accion = '';
    //OBJ_Matriz.p_estatus = 0;


    $.ajax({
        dataType: 'text',
        //cache: false,
        //contentType: 'application/json',
        //processData: false,
        //data: JSON.stringify(OBJ_Matriz),
        type: 'delete',

        success: function (data) {
            var obj = JSON.parse(data);
            if (obj[0].cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardo correctamente");
                GetMatriz($('#idObli').val());
                $('.Clean').val('');
                $('#IdMR').val('');
                $('#ModalNuevoRiesgo').modal('hide');
            }
            else {
                ErrorSA("", obj[0].msg);
            }
        },
        error: function (data) {
            var obj = JSON.parse(data);
            ErrorSA('', obj[0].msg)
        },
        //processData: false,
        //type: 'POST',
        //url: 'https://localhost:44359/matrizriesgo/delete/' +  item        
        url: $('#EndPointAC').val() + "Riesgos/Delete/" + item

    })



    //$.post("/Request/Matriz/Delete/" + item, function (data, status) {
    //    var objresponse = JSON.parse(data);
    //    if (status == 'success') {
    //        SuccessSA('', 'El registro se eliminó exitosamente');
    //        GetMatriz($('#IdContrato').val());
    //    }
    //    else {
    //        ErrorSA('', objresponse.Descripcion);
    //    }
    //});
}

function btnDeleteM(item) {
    function Confirmacion() {
        return EliminarMR(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

var MatrizClass = {
    id: null,
    Riesgo: null,
    tbl_CategorizacionRiesgo_ac_id: null,
    Probabilidad: null,
    Impacto: null,
    Prioridad: null,
    tbl_LinkObligaciones_ac_id: null,
    tbl_TipoRespuesta_ac_id: null,
    Accion: null,
    Respuesta: null,
    tbl_RespApego_Contrato_ac_id: null,
    TBLENT_DEPENDENCIA_id: null,
    TBLENT_CONTRATO_id: null,
    Inclusion: null,
    Estatus: null,
}

var Matriz_Riesgo_Class = {
  p_opt: null,
  p_id: null,
  p_riesgo: null,
  p_tbl_nivel_riesgo_id: null,
  p_probabilidad: null,
  p_impacto: null,
  p_prioridad: null,
  p_tbl_link_obligacion_id: null,
  p_respuesta: null,
  p_accion: null,
  p_estatus: null
}



$('.modal-child').on('show.bs.modal', function () {
    var modalParent = $(this).attr('data-modal-parent');
    $('#Riesgosmdl').css('opacity', 0);
});

$('.modal-child').on('hidden.bs.modal', function () {
    var modalParent = $(this).attr('data-modal-parent');
    $('#Riesgosmdl').css('opacity', 1);
});
