function Cerrar_Modal_Tipo_Int_Upd() {
    console.log('cerrando modal de update');
    $('#Modal_Tipo_interlocutor_Upd').modal('hide');
    $('.clear_txt_upd').val('');
}

function Modificar_Tipo_Interlocutor() {

    if ($("#txt_tipo_interlocutor_upd").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de interlocutor.');
    }
    tbl_tipo_interlocutor.id = $("#id_tipo_int").val();
    tbl_tipo_interlocutor.nombre= $("#txt_tipo_interlocutor_upd").val();
    if ($('#check_es_comercial_upd').is(":checked") == false) {
        tbl_tipo_interlocutor.comercial = 0;
    } else {
        tbl_tipo_interlocutor.comercial = 1;
    }

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_interlocutor),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Int_Upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_interlocutor();

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
        url: $("#EndPointAdmon").val() + "Catalogos/Update/TInterlocutor"

    })
}

$("#btn_upd_tipo_int").click(function () {
    Modificar_Tipo_Interlocutor();
})

var tbl_tipo_interlocutor = {
    id: "00000000-0000-0000-0000-000000000000",
    nombre: "",
    comercial: ""
}