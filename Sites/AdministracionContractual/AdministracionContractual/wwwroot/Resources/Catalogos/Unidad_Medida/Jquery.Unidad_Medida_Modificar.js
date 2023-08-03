function Cerrar_Modal_Unidad_Medida_Upd() {
    $('#Modal_Unidad_Medida_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Unidad_Medida() {

    if ($("#txt_unidad_medida_upd").val() == "" || $("#txt_clave_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar unidad de medida y clave.');
    }

    tbl_unidad_medida.p_id = $("#id_unidad_m").val();
    tbl_unidad_medida.p_unidad_medida = $("#txt_unidad_medida_upd").val();
    tbl_unidad_medida.p_clave = $("#txt_clave_upd").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_unidad_medida),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Unidad_Medida_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_unidad_medida();

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
        url: $("#EndPointAdmon").val() + "Catalogos/Update/UnidadesMedida"

    })
}

$("#btn_upd_unidad_m").click(function () {
    Modificar_Unidad_Medida();
})

var tbl_unidad_medida = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_unidad_medida: "",
    p_clave: ""
}