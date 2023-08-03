$.extend($.fn.dataTable.defaults, {
    responsive: true
});

$(document).ready(function () {
    LaunchLoader(true);
    $('#ExpedientesContrato').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
    });
    setInterval('Redimension()', 500);
    GetSolicitudDoc($('#IdContrato').val())
});
function Redimension() {
    try {
        var tables = document.getElementsByTagName('table');
        for (var i = 0; i < tables.length; i++) {
            $('#' + tables[i].id + '').resize();
        }
    }
    catch (err) { }
}

function GetSolicitudDoc(idCon) {
    $.get($('#EndPointAC').val() + "SerRegSolDoc/Get/Solicitud/Expediente/" + idCon, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            Interno.push(i + 1);
            Interno.push(data[i].nombre_documento);
            Interno.push(data[i].nombre_responsable);
            Interno.push(data[i].observacion);

          
                if (data[i].token_doc != null) {
                    Interno.push('<p><font color="green">Entregado</font></p>');
                }
                  else {
                    Interno.push('<p><font color="red">No entregado</font></p>');
                }
            
            if (data[i].token_doc != null && data[i].token_doc != "") {

                    Interno.push('<a onclick="btnDowloadGE(\'' + data[i].token_doc + '\')" class="fa fa-arrow-circle-down btn btn-success" title="Descargar archivo"></a> <a onclick="btnChange(\'' + data[i].tbl_gestion_expediente_contrato_id + '\',\'' + data[i].id_solicitud + '\')" class="fa fa-exchange btn btn-primary" title="Remplazar archivo"></a>');
                }
                else {
                    Interno.push('<a onclick="btnUpload(\'' + data[i].id_solicitud + '\')" class="fa fa-arrow-circle-up btn btn-primary" title="Cargar archivo"> </a>');
                }         
            

            Arreglo_arreglos.push(Interno);
        }
        var table = $('#ExpedientesContrato').DataTable();
        table.destroy();
        console.log(Arreglo_arreglos);
        $('#ExpedientesContrato').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Nombre del documento" },
                { title: "Responsable de elaboración" },
                { title: "Comentarios" },
                { title: "Estado de entrega" },
                { title: "Acciones" },
            ]
        });
        LaunchLoader(false);
    });
}

function btnUpload(item) {
    $('#IdSolicitudDoc').val(item);
    $('#RegistroExpediente').modal({ backdrop: 'static', keyboard: false });
    $('#RegistroExpediente').modal('show');
}
function btnChange(idExp, idSol) {
    $('#IdExpedienteCon').val(idExp);
    $('#IdSolicitudDoc').val(idSol);
    $('#RegistroExpediente').modal({ backdrop: 'static', keyboard: false });
    $('#RegistroExpediente').modal('show');
}
$('.cerrarMEXP').click(function () {
    $('#IdExpedienteCon').val('');
    $('#IdSolicitudDoc').val('');
    $('#DocumentoEXP').val('');
})
function CerrarModalGEC() {
    $('#RegistroExpediente').modal('hide');
}
function cerrarLoader() {
    LaunchLoader(false);
}
function AgregarExpediente() {
    LaunchLoader(true);

    if ($('#DocumentoEXP').val() == '') {
        function confirmar() {
            return cerrarLoader();
        }
        var okey = eval(confirmar);
        return ErrorSAAction('Error en los datos de entrada', 'Debe seleccionar un documento', okey);
        
    }
    var OBJ_Exp = ExpedienteClass;
    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());
    OBJ_Exp.p_opt = 2;
    if ($('#IdExpedienteCon').val() == '') {
        OBJ_Exp.p_id = '00000000-0000-0000-0000-000000000000';
    }
    else {
        OBJ_Exp.p_id = $('#IdExpedienteCon').val();
    }
    
    OBJ_Exp.p_tbl_contrato_solicitud_docto_id = $('#IdSolicitudDoc').val();
    OBJ_Exp.p_inclusion = date;
    OBJ_Exp.p_estatus = 1;




    /////////////////////////Carga de documento



    var form_data_file = new FormData();
    var file_ = $('#DocumentoEXP').prop('files')[0];

    form_data_file.append('file', file_);

    $.ajax({
        url: $("#EndPointFileAC").val() + 'Upload/',
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data_file,
        type: 'POST',
        async: false,
        success: function (data) {
            var token = data.replace(/[ '"]+/g, '');
            OBJ_Exp.p_token_doc = token;
            console.log(token);
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
        }
    });

    ///////////////////////////////////



    var form_data = new FormData();
    var file_data = $('#DocumentoEXP').prop('files')[0];
    form_data.append('File_Documento', file_data);
    form_data.append('ExpedienteForm', JSON.stringify(OBJ_Exp));



    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data,
        type: 'post',
        async: false,

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                function ConfirmacionMGE() {
                    return CerrarModalGEC()
                }
                var AccionSi = eval(ConfirmacionMGE);
                
                SuccessSAAction("Operación exitosa", "El registro se guardo correctamente", AccionSi);
                $('#IdExpedienteCon').val('');
                $('#IdSolicitudDoc').val('');
                $('#DocumentoEXP').val('');
                
                GetSolicitudDoc($('#IdContrato').val());
            }
            else {
                ErrorSA("", objresponse.Descripcion);
                
            }
            
        },

        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
            
        },

        processData: false,
        type: 'POST',
        url: ($('#EndPointAC').val() + "SerGestionExpediente/Add")

    })
    //LaunchLoader(false);
}
$('#RegistrarEXP').click(function () {
    
    AgregarExpediente();
    
})

function btnDowloadGE(tokenDoc) {
    getURL(tokenDoc)
    modalVisualizacion()
}
var ExpedienteClass = {
    p_opt: null,
    p_id: null,
    p_token_doc: null,
    p_tbl_contrato_solicitud_docto_id: null,
    p_inclusion: null,
    p_estatus: null,
}