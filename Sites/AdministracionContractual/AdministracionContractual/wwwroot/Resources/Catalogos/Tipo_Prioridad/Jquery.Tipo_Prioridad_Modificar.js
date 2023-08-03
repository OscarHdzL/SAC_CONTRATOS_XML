function Cerrar_Modal_Tipo_Pri_Upd() {
    console.log('cerrando modal de update');
    $('#Modal_Tipo_prioridad_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Tipo_Prioridad() {

    if ($("#txt_tipo_prioridad_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de prioridad.');
    }
    tbl_tipo_prioridad.p_id = $("#id_tipo_pri").val();
    tbl_tipo_prioridad.p_tipo_prioridad = $("#txt_tipo_prioridad_upd").val();
    tbl_tipo_prioridad.p_tbl_instancia_id = $("#HDidInstancia").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_prioridad),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Pri_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_prioridad();

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
        url: $("#EndPointAdmon").val() + "Catalogos/Update/TPrioridad"

    })
}

$("#btn_upd_tipo_pri").click(function () {
    Modificar_Tipo_Prioridad();
})

var tbl_tipo_prioridad = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_prioridad: "",
    p_tbl_instancia_id: ""
}