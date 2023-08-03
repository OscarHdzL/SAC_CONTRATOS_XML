$(function () {
    settings_();
    hidebox(1);
    $("#MM1").trigger('click');
    $('#example').DataTable();



});
function settings_() {
    $("#MM1").click(function () { hidebox(1); });
    $("#MM2").click(function () { hidebox(2); });
    $("#MM3").click(function () { hidebox(3); });
    $("#MM4").click(function () { hidebox(4); });
}
function hidebox(val) {
    $("#menu1").hide();
    $("#menu2").hide();
    $("#menu3").hide();
    $("#menu4").hide();
    $("#menu" + val).show();
}

function SendCMArea() {
    $('#Loader').modal('show');
    var fd = new FormData();
    fd.append('files', $('#CM_input_area')[0].files[0]);
    fd.append('idDependencia', $('#HDidDependencia').val());


    $.ajax({
        url: '/CargasMasivas/FileUpload',
        data: fd,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            console.log(data);

            var Arreglo_arreglosdot = [];
            for (var i = 0; i <= data.length - 1; i++) {
                var HistoricoArr = [];

                HistoricoArr.push(data[i].id);
                HistoricoArr.push(data[i].tipo);
                HistoricoArr.push(data[i].descripcion);
                //<i class="fa fa-fw fa-check-circle"></i>
                if (data[i].estatus == 'success') {
                    HistoricoArr.push('<i title="success" style="font-size: 200%;color: #28A745;" class="fa fa-fw fa-check-circle"></i> <label>');
                }
                else {
                    HistoricoArr.push('<i title="fail" style="font-size: 200%;color: #DC3545;" class="fa fa-fw fa-exclamation-circle"></i>');
                }
                
                Arreglo_arreglosdot.push(HistoricoArr);
            }

            var table = $('#tbl_log_area').DataTable();

            table.destroy();

            $('#tbl_log_area').DataTable({
                data: Arreglo_arreglosdot,
                columns: [
                    { title: "Id" },
                    { title: "Tipo" },
                    { title: "Descripción" },
                    { title: "Estatus" }
                ],
                columnDefs: [
                    {
                        targets: -1,
                        className: 'dt-body-center'
                    }]
            });

            $('#Loader').modal('hide');

        }
    });
}


function SendCMProveedores() {
    $('#Loader').modal('show');
    var fd = new FormData();
    fd.append('files', $('#fileuploadproveedor')[0].files[0]);
    fd.append('idDependencia', $('#HDidDependencia').val());
    fd.append('idInstancia', $('#HDidInstancia').val());


    $.ajax({
        url: '/CargasMasivas/FileUploadProveedores',
        data: fd,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            console.log(data);

            var Arreglo_arreglosdot = [];
            for (var i = 0; i <= data.length - 1; i++) {
                var HistoricoArr = [];

                HistoricoArr.push(data[i].id);
                HistoricoArr.push(data[i].tipo);
                HistoricoArr.push(data[i].descripcion);
                //<i class="fa fa-fw fa-check-circle"></i>
                if (data[i].estatus == 'success') {
                    HistoricoArr.push('<i title="success" style="font-size: 200%;color: #28A745;" class="fa fa-fw fa-check-circle"></i> <label>');
                }
                else {
                    HistoricoArr.push('<i title="fail" style="font-size: 200%;color: #DC3545;" class="fa fa-fw fa-exclamation-circle"></i>');
                }

                Arreglo_arreglosdot.push(HistoricoArr);
            }

            var table = $('#tbl_log_provedores').DataTable();

            table.destroy();

            $('#tbl_log_provedores').DataTable({
                data: Arreglo_arreglosdot,
                columns: [
                    { title: "Id" },
                    { title: "Tipo" },
                    { title: "Descripción" },
                    { title: "Estatus" }
                ],
                columnDefs: [
                    {
                        targets: -1,
                        className: 'dt-body-center'
                    }]
            });

            $('#Loader').modal('hide');
            $("#fileuploadproveedor").val(null);
        }
    });
}




function downLayoutProveedores() {
    window.open('/CargasMasivas/DownLoadLayoutInterlocutores', '_blank');
}