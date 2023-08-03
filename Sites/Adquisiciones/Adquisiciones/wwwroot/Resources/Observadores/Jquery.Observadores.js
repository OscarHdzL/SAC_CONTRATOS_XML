$(document).ready(function () {
    $('#tbl_observadores').DataTable({});

});

$(function () {
    GetObservadores();
});

var con = $('#EndPointAQ').val();

$(".btn-save-observador").click(function () {
    if ($('.textobservadores').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Debé Ingresar un observador'
        });
        return;
    }
    if (ValidaCadena($('.textobservadores').val(), 'Observador') != '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Observador" no puede contener caracteres especiales'
        })
        return;
    }
    sendObservador();
});

function GetObservadores() {

    $.get(con + "JuntaAclaracion/Get_Obs/" + $('#_SOLICITUD').val() + "/" + $('#_TIPOACTA').val() + "/" + $('#HD_programacion').val(), function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var InternoArr = [];

            InternoArr.push(data[i].folioConvocatoria == '' ? data[i].num_solicitud : data[i].folioConvocatoria);
            InternoArr.push(data[i].observador);
            //if (data[i].tipo_acta == 'eco') {
            //    InternoArr.push(data[i].tipo_acta == 'eco' ? 'Económico' : 'Técnico');
            //}
            //else if (data[i].tipo_acta == 'tec') {
            //    InternoArr.push(data[i].tipo_acta == 'tec' ? 'Técnico' : 'Económico');
            //}
            //else {
            //    InternoArr.push('-');
            //}
            //InternoArr.push(data[i].tipo == 'eco' ? 'Económico' : 'Técnico');

            InternoArr.push("<button class='btn btn-danger' onclick=\"DeleteItemObservador('" + data[i].id + "');\">Eliminar</button>");

            Arreglo_arreglosdot.push(InternoArr);
        }

        var table = $('#tbl_observadores').DataTable();

        table.destroy();

        $('#tbl_observadores').DataTable({
            data: Arreglo_arreglosdot,
            columns: [
                { title: "Folio Conv." },
                { title: "Observador" },
                //{ title: "Tipo" },

                { title: "Acciones" }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });

    });
}

function DeleteItemObservador(item) {
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        type: 'delete',

        success: function (data) {
            var data_b = $.parseJSON(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", data_b[0].msg);
                GetObservadores();
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: (con + "JuntaAclaracion/Delete_Obs/" + item)
    })

}

function sendObservador() {

    var evaluacion = ObservadoresClass;

    evaluacion.tbl_solicitud_id = $('#_SOLICITUD').val();
    evaluacion.observador = $('.textobservadores').val();
    evaluacion.tipo_acta = $('#_TIPOACTA').val();
    evaluacion.tbl_programacion_id = $('#HD_programacion').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(evaluacion),
        type: 'post',

        success: function (data) {
            var obj = JSON.parse(data);
            if (obj[0].cod == 'success') {

                GetObservadores();

            } else if (obj[0].cod != 'success') {

                Swal.fire({
                    type: 'error',
                    title: 'Error al guardar funcionario',
                    text: obj[0].msg
                })

            }

        },
        error: function (data) {
            console.log(data);
            Swal.fire({
                type: 'error',
                title: 'Error al guardar funcionario',
                text: data
            })
        },
        url: con + "JuntaAclaracion/Add_Obs"
    });


}

var ObservadoresClass = {
    id: '',
    tbl_solicitud_id: '',
    tipo_acta: '',
    observador: '',
    tbl_programacion_id: '',
    inclusion: '0001-01-01'
}