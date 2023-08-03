$(document).ready(function () {
    $('#tbl_funcionarios').DataTable({});

});


$(function () {
    GetServidores();
    Getfuncionarios();
});


$(".btn-save-servpublic").click(function () {
    if ($('.cmb_funcionarios').val() == '') {
        Swal.fire({
            type: 'error',
            title: 'Debé seleccionar un servidor público'
        });
        return;
    }
    sendfuncionario();
});



function Getfuncionarios() {

    //$.get("/Request/ServidoresPublicos/Get/Convocatoria/" + $('#idconvocatoria').val() + "/" + $('#txt_tipo').val() + "/" + $('#HD_programacion').val(), function (data, status) {
    $.get($('#EndPointAQ').val() + "SerSolicitudFuncionario/Get/Funcionario/" + $('#_SOLICITUD').val() + "/" + $('#_TIPOACTA').val() + "/" + $('#HD_programacion').val(), function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var InternoArr = [];

            InternoArr.push(data[i].folioConvocatoria == '' ? data[i].num_solicitud : data[i].folioConvocatoria);
            InternoArr.push(data[i].servidor_publico);
            //InternoArr.push(data[i].tipo_acta);
            //if (data[i].tipo_acta == 'eco') {
            //    InternoArr.push(data[i].tipo_acta == 'eco' ? 'Económico' : 'Técnico');
            //}
            //else if (data[i].tipo_acta == 'tec') {
            //    InternoArr.push(data[i].tipo_acta == 'tec' ? 'Técnico' : 'Económico');
            //}
            //else {
            //	InternoArr.push('-');
            //}


            InternoArr.push("<button class='btn btn-danger' onclick=\"DeleteItemfunpub('" + data[i].id + "');\">Eliminar</button>");

            Arreglo_arreglosdot.push(InternoArr);
        }

        var table = $('#tbl_funcionarios').DataTable();

        table.destroy();

        $('#tbl_funcionarios').DataTable({
            data: Arreglo_arreglosdot,
            columns: [
                { title: "Folio conv." },
                { title: "Funcionario público." },
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



function DeleteItemfunpub(item) {
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        type: 'delete',

        success: function (data) {
            var obj = JSON.parse(data);
            if (obj.cod == 'success') {
                SuccessSA("Operación exitosa", "El registro se eliminado correctamente");
                Getfuncionarios();
            }
            else {
                ErrorSA("Error", obj.msg);
            }
        },
        error: function () {

            ErrorSA('Error', "Ocurrio un error")
        },
        url: $('#EndPointAQ').val() + "SerSolicitudFuncionario/delete/" + item
    });
}





function sendfuncionario() {

    var evaluacion = FuncionariosConvClass;

    evaluacion.p_opt = 2;
    evaluacion.p_id = '00000000-0000-0000-0000-000000000000';
    evaluacion.p_tbl_solicitud_id = $('#_SOLICITUD').val()
    evaluacion.p_tbl_servidor_publico_id = $('.cmb_funcionarios').val();
    evaluacion.p_tipo_acta = $('#_TIPOACTA').val();
    evaluacion.p_tbl_programacion_id = $('#HD_programacion').val();

    $.ajax({

        dataType: 'json',  // what to expect back from the PHP script, if anything
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(evaluacion),
        type: 'post',

        success: function (data) {
            if (data.cod == 'success') {
                Getfuncionarios();
            }
            else if (data.cod != 'success') {

                Swal.fire({
                    type: 'error',
                    title: 'Error al guardar funcionario',
                    text: data.msg
                })

            }

        },
        error: function (data) {
            console.log(data);
            Swal.fire({
                type: 'error',
                title: 'Error al guardar funcionario',
                text: data.msg
            })
        },
        processData: false,
        type: 'POST',
        //url: '/Request/ServidoresPublicos/add'
        url: $('#EndPointAQ').val() + "SerSolicitudFuncionario/Add"
    });


}



var FuncionariosConvClass = {
    p_opt: '',
    p_id: '',
    p_tbl_solicitud_id: '',
    p_tbl_servidor_publico_id: '',
    p_tipo_acta: '',
    p_tbl_programacion_id: ''
}


////Microservicio nuevo


function GetServidores() {
    var instancia = $('#HDidInstancia').val();

    var Uri = $('#EndPointAQ').val() + 'SerSolicitudFuncionario/Get/Servidores/' + instancia;
    $.get(Uri, function (data, status) {
        var body = '';
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";

        }
        body = '<option> Seleccione...</option>' + body;
        $('.cmb_funcionarios').html(body);
    }, 'json');

}