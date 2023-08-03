function Cerrar_Modal_Tipo_Riesgo_Upd() {
    $('#Modal_Tipo_Riesgo_Modificar').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Tipo_Riesgo() {

    if ($("#txt_tipo_riesgo_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de riesgo.');
    }
    tbl_tipo_riesgo.p_id = $("#id_tipo_riesgo").val();
    tbl_tipo_riesgo.p_tipo_riesgo = $("#txt_tipo_riesgo_upd").val();
    tbl_tipo_riesgo.p_tbl_instancia_id = $("#HDidInstancia").val();

    console.log(JSON.stringify(tbl_tipo_riesgo));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_riesgo),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Riesgo_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_Lista_Tipo_riesgos();

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
        url: $("#EndPointAdmon").val() + "Catalogos/Update/TRiesgo"

    })
}

$("#btn_actualizar_tipo_riesgo").click(function () {
    Modificar_Tipo_Riesgo();
})

var tbl_tipo_riesgo = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_riesgo: "",
    p_tbl_instancia_id: ""
}