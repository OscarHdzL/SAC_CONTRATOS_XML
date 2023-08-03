$(document).ready(function () {

});

$("#btnCargaMasiva").click(function () {
    $("#modalCargaMasiva").modal("show");
});

$("#btnCargarUsuariosServPub").click(function () {
    var fileUpload = $("#archivo").get(0);

    var formData = new FormData();
    formData.append('archivo', fileUpload.files[0]);

    $.ajax({
        url: "/CargaUsuariosServPub/RealizarCargaMasiva",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {

            console.log(data);

            SuccessSA();
            $("#modalCargaMasiva").modal("hide");

            var exitosos = data.contenido.lstRegistrosExitosos;
            var fallidos = data.contenido.lstRegistrosFallidos;

            //console.log(data.Contenido.lstRegistrosExitosos[0].ItemArray[0]);

            var listado = [];

            for (var i = 0; i <= exitosos.length - 1; i++) {
                var fila = [];

                fila.push(exitosos[i][1] + " " + exitosos[i][2] + " " + exitosos[i][3]);
                fila.push(exitosos[i][0]);
                fila.push("Exitoso");

                listado.push(fila);
            }

            console.log(listado);

            var tablaExitosos = $('#tbl_registrosexitosos').DataTable();

            tablaExitosos.destroy();

            $('#tbl_registrosexitosos').DataTable({
                data: listado,
                columns: [
                    { title: "Nombre" },
                    { title: "Correo" },
                    { title: "Resultado" }
                ],
                columnDefs: [
                    {
                        targets: -1,
                        className: 'dt-body-center'
                    }]
            });

            listado = [];

            for (i = 0; i <= fallidos.length - 1; i++) {
                fila = [];

                fila.push(fallidos[i].nombre);
                fila.push(fallidos[i].correo);
                fila.push("Fallido. " + fallidos[i].msgError);

                listado.push(fila);
            }
            console.log(fallidos);
            var tablaFallidos = $('#tbl_registrosfallidos').DataTable();

            tablaFallidos.destroy();

            $('#tbl_registrosfallidos').DataTable({
                data: listado,
                columns: [
                    { title: "Nombre" },
                    { title: "Correo" },
                    { title: "Resultado" }
                ],
                columnDefs: [
                    {
                        targets: -1,
                        className: 'dt-body-center'
                    }]
            });
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Error al realizar la operación',
                text: data.Excepcion
            });
        }
    });
});

//function obtenerListado() {

//    $.get("/Request/CargaUsuariosServPub/Get", function (data, status) {
//        console.log(data)
//        var listado = [];

//        for (var i = 0; i <= data.length - 1; i++) {
//            var fila = [];

//            fila.push(data[i].NOMBRE + " " + data[i].AP_PATERNO + " " + data[i].AP_MATERNO);
//            fila.push(data[i].RFC);
//            fila.push(data[i].EMAIL);

//            listado.push(fila);
//        }

//        var table = $('#tbl_usuarios').DataTable();

//        table.destroy();

//        $('#tbl_usuarios').DataTable({
//            data: listado,
//            columns: [
//                { title: "Nombre" },
//                { title: "RFC" },
//                { title: "Email" }
//            ],
//            columnDefs: [
//                {
//                    targets: -1,
//                    className: 'dt-body-center'
//                }]
//        });
//    });
//}