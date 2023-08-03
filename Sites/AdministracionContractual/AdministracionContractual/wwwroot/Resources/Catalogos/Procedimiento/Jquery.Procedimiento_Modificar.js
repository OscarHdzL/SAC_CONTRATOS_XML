function Cerrar_Modal_Procedimiento_Upd() {
    $('#Modal_Procedimiento_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Procedimiento() {

    if ($("#txt_procedimiento_upd").val() == "" || $("#txt_sigla_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar procedimiento y sigla.');
    }

    tbl_procedimiento_.p_id = $("#id_procedimiento").val();
    tbl_procedimiento_.p_procedimiento = $("#txt_procedimiento_upd").val();
    tbl_procedimiento_.p_sigla = $("#txt_sigla_upd").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_procedimiento_),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Procedimiento_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_procedimiento();

            }
            else if (objresponse[0].cod == "warning") {
                ErrorSA("", objresponse[0].msg);
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.responseText);
            if (XMLHttpRequest.responseText != null) {
                var errorResponse = JSON.parse(XMLHttpRequest.responseText);
                console.log(errorResponse);
                if (errorResponse.length > 0) {
                    if (errorResponse[0].msg != null) {
                        ErrorSA('Error', errorResponse[0].msg);
                    } else {
                        ErrorSA('Error', "Ocurrio un error.");
                    }
                } else {
                    ErrorSA('Error', "Ocurrio un error.");
                }

            } else {
                ErrorSA('Error', "Ocurrio un error.");
            }
        },
        processData: false,
        type: 'PUT',
        url: $("#EndPointAdmon").val() + "Catalogos/Update/Procedimiento"

    })
}

$("#btn_upd_unidad_m").click(function () {
    Modificar_Procedimiento();
})

var tbl_procedimiento_ = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_procedimiento: "",
    p_sigla: ""
}