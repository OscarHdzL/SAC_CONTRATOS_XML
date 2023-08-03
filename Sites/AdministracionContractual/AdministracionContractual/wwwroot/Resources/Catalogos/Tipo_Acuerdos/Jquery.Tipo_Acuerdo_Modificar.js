function Cerrar_Modal_Tipo_Acu_Upd() {
    $('#Modal_Tipo_Acuerdo_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Tipo_Acuerdo() {

    if ($("#txt_tipo_acuerdo_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de riesgo.');
    }
    tbl_tipo_acuerdo.p_id = $("#id_tipo_acuerdo").val();
    tbl_tipo_acuerdo.p_tipo_acuerdo = $("#txt_tipo_acuerdo_upd").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_acuerdo),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(objresponse[0].cod)
            if (objresponse.response[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Acu_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_acuerdo();

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
        url: $("#EndPointApego").val() + "SerAcuerdos/Update/Acuerdos"

    })
}

$("#btn_upd_tipo_acuerdo").click(function () {
    Modificar_Tipo_Acuerdo();
})


var tbl_tipo_acuerdo = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_acuerdo: ""
}
