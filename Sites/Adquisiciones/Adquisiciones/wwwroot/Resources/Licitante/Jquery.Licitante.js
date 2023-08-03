$(function () {
    AutocompleteRfc();
    GetLicitantes();
});



function validacion_tipo_solicitud() {
    
    $.get($('#EndPointAQ').val() + "Modalidad/Get/Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
        if (data.response != null) {
            if (data.response.sigla_licitacion == 'AD') { ///Solicitud de Adjudicacion directa, Se usa sigla del catalogo
                var table = $('#tbl_Licitacion_v2').DataTable();

                if (table.data().count() == 0) {
                    Guardar_licitante();
                } else if (table.data().count() > 0) {

                    Swal.fire({
                        type: 'error',
                        title: 'Solo se puede agregar un licitante en la adjudicacion directa'
                    });
                    return;
                }
            } else {
                Guardar_licitante();
            } 

        }
    });
}



function AutocompleteRfc() {
    //$.get("/Request/Licitantes/GetProveedores/" + $('#HiddenInstancia').val(), function (data, status) {
    $.get($('#EndPointAQ').val() + "SerLicitante/Get/Proveedores/" + $('#HDidInstancia').val(), function (data, status) {
        var Contenido = [];
        for (var i = 0; i <= data.length - 1; i++) {
            Contenido.push(data[i].rfc);
        }
        $(".txt_RFCs").autocomplete({
            source: Contenido
        });
    });
}

function Guardar_licitante() {

    if ($('.txt_RFCs').val().length < 12 || $('.txt_RFCs').val().length > 13) {
        Swal.fire({
            type: 'error',
            title: 'El RFC debe tener entre 12 y 13 caracteres'
        });
        return;
    }
    if (ValidaCadena($('.txt_RFCs').val(), 'RFC') == '') {
        $('.RFCRL').val($('.txt_RFCs').val());
        verificacion();
    }
    else {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "RFC" no puede contener caracteres especiales'
        })
        return;
    }

}

//$(".btn-save-licitante").click(function () {
//    if ($('.txt_RFCs').val().length < 12 || $('.txt_RFCs').val().length > 13) {
//        Swal.fire({
//            type: 'error',
//            title: 'El RFC debe tener entre 12 y 13 caracteres'
//        });
//        return;
//    }
//    if (ValidaCadena($('.txt_RFCs').val(), 'RFC') == '') {
//        $('.RFCRL').val($('.txt_RFCs').val());
//        verificacion();
//    }
//    else {
//        Swal.fire({
//            type: 'error',
//            title: 'Hay un error en los datos de entrada',
//            text: 'El campo "RFC" no puede contener caracteres especiales'
//        })
//        return;
//    }

//});


$(".newLic").click(function () {
    sendlicitante();
});


function GetLicitantes() {

    //$.get("/Request/Licitantes/Get/" + $('#idconvocatoria').val(), function (data, status) {
    $.get($('#EndPointAQ').val() + "SerLicitante/Get/Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var InternoArr = [];

            InternoArr.push(data[i].licitante);
            InternoArr.push(data[i].rep_nombre + ' ' + data[i].rep_paterno + ' ' + data[i].rep_materno);
            InternoArr.push(data[i].rfc);
            if (data[i].es_proveedor) {
                InternoArr.push('Proveedor Licitante');
            }
            else {
                InternoArr.push('Licitante');
            }

            InternoArr.push("<button class='btn btn-danger' onclick=\"DeleteItemlicitante('" + data[i].id + "');\">Eliminar</button>");

            Arreglo_arreglosdot.push(InternoArr);
        }

        var table = $('#tbl_Licitacion_v2').DataTable();

        table.destroy();

        $('#tbl_Licitacion_v2').DataTable({
            data: Arreglo_arreglosdot,
            columns: [
                { title: "Razon Soc." },
                { title: "Representante" },
                { title: "RFC" },
                { title: "Estatus" },
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



function DeleteItemlicitante(item) {
    //$("/Request/Licitantes/delete/" + item, function (data, status) {
    //	if (status == 'success') {
    //		GetLicitantes();
    //	}
    //});


    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        type: 'delete',

        success: function (data) {
            var obj = JSON.parse(data);
            if (obj[0].cod == 'success') {
                SuccessSA("Operación exitosa", "El registro se eliminado correctamente");
                GetLicitantes();
            }
            else {
                ErrorSA("Error", obj[0].msg);
            }
        },
        error: function () {

            ErrorSA('Error', "Ocurrio un error")
        },
        url: $('#EndPointAQ').val() + "SerLicitante/delete/" + item
    });

}



function verificacion() {
    //$.get("/Request/Licitantes/Valida/" + $('.txt_RFCs').val() + "/" + $('#idconvocatoria').val(), function (data, status) {
    $.get($('#EndPointAQ').val() + "SerLicitante/Valida/" + $('.txt_RFCs').val() + "/" + $('#_SOLICITUD').val(), function (data, status) {
        if (data == undefined) {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: "Hay un error en la comunicación de los servicios"
            });
        }
        else if (data.cod == "NoExiste") {
            $('#altalicitante').modal('show');
        }
        else if (data.cod == "success") {
            GetLicitantes();
        }
        else if (data.cod == "warning") {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: data.msg
            });
        }
    });
}





function sendlicitante() {

    var evaluacion = validateLicitante();
    if (evaluacion.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: evaluacion.Texto
        });
        return;
    }



    $.ajax({

        dataType: 'json',  // what to expect back from the PHP script, if anything
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(evaluacion.objeto),
        type: 'post',

        success: function (data) {
            if (data.cod == "success") {
                $('.txt_RFCs').val('');
                $('.NombreRL').val('');
                $('.PaternoRL').val('');
                $('.MaternoRL').val('');
                $('.RazonSocialRL').val('');
                $('.RFCRL').val('');
                GetLicitantes();
                $('#tbl_Licitacion_v2, #altalicitante').modal('hide');


                //window.location.reload();

            }
        },
        error: function (data) {
            alert(data);
            Swal.fire({
                type: 'error',
                title: 'Error al guardar Licitación',
                text: data.msg
            })
        },
        processData: false,
        type: 'POST',
        //url: '/Request/Licitantes/Add'
        url: $('#EndPointAQ').val() + "SerLicitante/Add"
    });


}


function validateLicitante() {
    var OBJ = LicitanteConvClass;
    var Response = { Texto: '', Bit: true, objeto: null };
    //Nombre
    if ($('.NombreRL').val() == '') {
        Response.Texto = 'Debé ingresar el nombre del representante legal';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('.NombreRL').val(), 'Nombre') != '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Nombre" no puede contener caracteres especiales'
        })
        return;
    }

    //paterno
    if ($('.PaternoRL').val() == '') {
        Response.Texto = 'Debé ingresar el apellido Paterno del representante legal';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('.PaternoRL').val(), 'Apellido Paterno') != '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Apellido Paterno" no puede contener caracteres especiales'
        })
        return;
    }

    //materno
    //if ($('.MaternoRL').val() == '') {
    //    Response.Texto = 'Debé ingresar el apellido Paterno del representante legal';
    //    Response.Bit = true;
    //    return Response;
    //}
    //if (ValidaCadena($('.MaternoRL').val(), 'Apellido Materno') != '') {
    //    Swal.fire({
    //        type: 'error',
    //        title: 'Hay un error en los datos de entrada',
    //        text: 'El campo "Apellido Materno" no puede contener caracteres especiales'
    //    })
    //    return;
    //}

    //Razón Social
    if ($('.RazonSocialRL').val() == '') {
        Response.Texto = 'Debé ingresar una razón social';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('.RazonSocialRL').val(), 'Razón Social') != '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Razón Social" no puede contener caracteres especiales'
        })
        return;
    }

    //RFC
    if ($('.RFCRL').val() == '' || $('.RFCRL').val().length < 12) {
        Response.Texto = 'Debé ingresar un RFC a 12 o 13 posiciones';
        Response.Bit = true;
        return Response;
    }

    OBJ.p_opt = 2;
    OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();
    OBJ.p_razon_social = $('.RazonSocialRL').val();
    OBJ.p_rep_legal_nombre = $('.NombreRL').val();
    OBJ.p_rep_legal_ap_paterno = $('.PaternoRL').val();
    OBJ.p_rep_legal_ap_materno = $('.MaternoRL').val();
    OBJ.p_rfc = $('.RFCRL').val();
    OBJ.p_es_proveedor = 0;
    //	OBJ.inclusion = '2019-09-18';


    Response.Texto = '';
    Response.Bit = false;
    Response.objeto = OBJ;
    return Response;
}



var LicitanteConvClass = {
    p_opt: null,
    p_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_solicitud_id: ' 00000000-0000-0000-0000-000000000000',
    p_razon_social: '',
    p_rep_legal_nombre: '',
    p_rep_legal_ap_paterno: '',
    p_rep_legal_ap_materno: '',
    p_rfc: '',
    p_es_proveedor: ''
}