function Cerrar_Modal_Tipo_Sanc_Upd() {
    $('#Modal_Tipo_Sancion_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Tipo_Sancion() {

    if ($("#txt_tipo_sancion_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de riesgo.');
    }
    tbl_tipo_sancion.p_id = $("#id_tipo_sancion").val();
    tbl_tipo_sancion.p_sancion = $("#txt_tipo_sancion_upd").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_sancion),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(objresponse[0].cod)
            if (objresponse.response[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Sanc_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_sancion();

            }
            else if (objresponse.response[0].cod == "warning") {
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
        type: 'POST',
        url: $("#EndPointApego").val() + "SerSancion/Update/Sanciones"

    })
}

$("#btn_upd_tipo_sancion").click(function () {
    Modificar_Tipo_Sancion();
})

var tbl_tipo_sancion = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_sancion: ""
}