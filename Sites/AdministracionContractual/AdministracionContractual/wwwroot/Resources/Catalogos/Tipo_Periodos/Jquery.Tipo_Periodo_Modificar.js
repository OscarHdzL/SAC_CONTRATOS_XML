function Cerrar_Modal_Tipo_Per_Upd() {
    $('#Modal_Tipo_Periodo_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Tipo_Periodo() {

    if ($("#txt_tipo_periodo_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de riesgo.');
    }
    tbl_tipo_periodo.p_id = $("#id_tipo_periodo").val();
    tbl_tipo_periodo.p_periodo = $("#txt_tipo_periodo_upd").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_periodo),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(objresponse[0].cod)
            if (objresponse.response[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Per_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_periodo();

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
        url: $("#EndPointApego").val() + "SerObligacion/Update/Periodos"

    })
}

$("#btn_upd_tipo_periodo").click(function () {
    Modificar_Tipo_Periodo();
})

var tbl_tipo_periodo = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_periodo: ""
}