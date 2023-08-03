function Cerrar_Modal_Tipo_Ejecucion_Upd() {
    $('#Modal_Tipo_Ejecucion_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Tipo_Ejecucion() {

    if ($("#txt_tipo_ejecucion_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de ejecución.');
    }
    tbl_tipo_ejecucion.p_id = $("#id_tipo_ejecucion").val();
    tbl_tipo_ejecucion.p_tipo_ejecucion = $("#txt_tipo_ejecucion_upd").val();
    tbl_tipo_ejecucion.p_tbl_instancia_id = $("#HDidInstancia").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_ejecucion),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Ejecucion_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_ejecucion();

            }
            else if (objresponse[0].cod == "warning") {
                ErrorSA("", objresponse[0].msg);
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA("", "Ocurrio un error.");
        },
        processData: false,
        type: 'PUT',
        url: $("#EndPointAdmon").val() + "Catalogos/Update/TEjecucion"

    })
}

$("#btn_upd_tipo_ejecucion").click(function () {
    Modificar_Tipo_Ejecucion();
})

var tbl_tipo_ejecucion = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_ejecucion: "",
    p_tbl_instancia_id: ""
}