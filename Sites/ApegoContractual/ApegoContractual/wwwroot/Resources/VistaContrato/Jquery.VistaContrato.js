$(document).ready(function () {
    GetVistaContrato($('#idcontrato_VistaContrato').val());
});



function GetVistaContrato(idContrato) {

    $.get($('#EndPointAC').val() +"SerContrato/Get/VistaContrato/" + idContrato, function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {
            $('#txt_numcon').val(data[i].numero);
            $('#txt_nomcont').val(data[i].nombre);
            $('#txt_objcon').val(data[i].objeto);
            $('#txt_monmax').val(data[i].monto_max_sin_iva);
            $('#txt_monmin').val(data[i].monto_min_sin_iva);
            var fechaIni = (data[i].fecha_Iinicio.split('T'));
            var fechaFin = (data[i].fecha_fin.split('T'));
            $('#txt_finicio').val(fechaIni[0]);
            $('#txt_ffin').val(fechaFin[0]);

            $('#txt_dependencia_contrato_v').val(data[i].tbl_dependencia_id);
        }

    });

}