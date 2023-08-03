function Cerrar_Modal_Tipo_Con_Upd() {
    $('#Modal_Tipo_Contrato_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Tipo_Contrato() {

    if ($("#txt_tipo_contrato_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de riesgo.');
    }
    tbl_tipo_contrato.p_id = $("#id_tipo_con").val();
    tbl_tipo_contrato.p_tipo_contrato = $("#txt_tipo_contrato_upd").val();
    tbl_tipo_contrato.p_tbl_instancia_id = $("#HDidInstancia").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_contrato),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Con_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_contrato();

            }
            else if (objresponse[0].cod == "warning") {
                ErrorSA("", objresponse[0].msg);
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        processData: false,
        type: 'PUT',
        url: $("#EndPointAdmon").val() + "Catalogos/Update/TContrato"

    })
}

$("#btn_upd_tipo_con").click(function () {
    Modificar_Tipo_Contrato();
})

var tbl_tipo_contrato = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_contrato: "",
    p_tbl_instancia_id: ""
}