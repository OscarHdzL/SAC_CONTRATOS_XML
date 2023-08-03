document.onreadystatechange = function () {
    if (document.readyState == "complete") {

        if ($('#IDD').val() != '' || $('#IDD').val() == undefined) {
            GetTableVisor();
            GetTableVisorTBLDOCUMENTSEval_()
            //alert(0);
            disable_fields();
        }
        else {
            setTimeout(function () { Enlace(); GetTable() }, 1000);
        }
        if ($('#Evaluador').val() == '1') {
         
            setTimeout(function () { NumSolicitud_(); }, 2000);
            GetTableEval();
        }
    }
}

function NumSolicitud_() {
    var con = $('#EndPointAQ').val();
    var numsol = '';
    $.get(con + "MesaValidacion/GetNumSol/" + $('#MV').val(), function (data, status) { $('#nsol').html(data.response.cod); }, 'json');
    $('#nsol').html(numsol);
}

function GetTableEval() {
    var table = null;
    //var con = $('#EndPointAQ').val();
    var confile = $('#EndPointAQ').val() + "MesaValidacion/get_mesa_solicitante/";
    //$.get(con + "MesaValidacion/get_mesa/" + $('#MV').val(),
    //    function (data, status) {
    $.get(confile + $('#MV').val(),
        function (data, status) {
            var Arreglo_arreglosdot = [];
            for (var i = 0; i <= data.length - 1; i++) {
                var InternoArr = [];
        
                InternoArr.push(data[i].versions_title);
                InternoArr.push(data[i].versions_consecutive);
                InternoArr.push(data[i].versions_description);

                var actions = "<button class='btn btn-success' onclick = 'getURL(\"" + data[i].token + "\");'><i class='fa fa-fw fa-tv  success'></i></button>";
                actions += "&nbsp;&nbsp;<button class='btn btn-info' onclick = 'SendValuesEval(\"" + data[i].token + "\",\"" + $('#MV_NUM_SOL').val() + "\",\"" + data[i].versions_title + "\",\"" + data[i].alert + "\");'><i class='fa fa-check-circle-o success'></i></button>";

       
                InternoArr.push(actions);
                InternoArr.push(data[i].AlertDetail);

                $('#TOKENSS').val(data[i].token);
                Arreglo_arreglosdot.push(InternoArr);
            }
            var table = $('#TBLDOCUMENTSEVAL').DataTable();
            table.destroy();
            $('#TBLDOCUMENTSEVAL').DataTable({
                data: Arreglo_arreglosdot,
                columns: [
          
                    { title: "Nombre del documento" },
                    { title: "Versión" },
                    { title: "Descripción" },

                    { title: "Acciones" }
                ]
            });
            $('#MV_NUM_FILE').val('');
            $('#MVoberservaciones').val('');
            $('#NomDoc').val('');


        }, 'json');
    //}, 'json');
}

function Enlace() {
    //$('#TBLDOCUMENTS').DataTable()
    var con = $('#EndPointAQ').val();
    //alert(0);
    $.get(con + "MesaValidacion/GetNumSol/" + $('#MV').val(), function (data, status)
    { $('#MV_NUM_SOL').val(data.response.cod); }, 'json');
    $('#TBLDOCUMENTS').DataTable();
}

function sendfile() {
    var con = $('#EndPointAQ').val();
    var fd = new FormData();
    var files = $('#MV_NUM_FILE').prop('files')[0];
    fd.append('file', files);
    fd.append('Descripcion', $('#MVoberservaciones').val());
    fd.append('Autor', $('#H6_val').html());
    fd.append('Nombre', $('#NomDoc').val());
    var tokenss = '';
    if ($('#TOKENSS').val().trim() == '') {
        tokenss = '';
    }
    else {
        tokenss = $('#TOKENSS').val();
    }

    $.ajax({
        url: $("#EndPointFileAQ").val() + 'Upload/Details',
        dataType: 'text',
        type: 'post',
        data: fd,
        contentType: false,
        cache: false,
        async: false,
        processData: false,
        success: function (response) {
            $.post(con + "MesaValidacion/Init/" + $('#MV').val() + "/" + response.split('"').join(""),
                function (data, status)
                {
                    GetTable();
                    
                    SuccessSA("Operación exitosa", 'Documento enviado');
                }, 'json');


        },
    });

}

function GetTable() {
    var table = null;
    //var con = $('#EndPointAQ').val();
    var confile = $('#EndPointAQ').val() + "MesaValidacion/get_mesa_solicitante/";
    //$.get(con + "MesaValidacion/get_mesa/" + $('#MV').val(),
    //    function (data, status) {
    $.get(confile + $('#MV').val(),
                function (data, status) {
                    var Arreglo_arreglosdot = [];
                    for (var i = 0; i <= data.length - 1; i++) {
                        var InternoArr = [];
                        InternoArr.push($('#MV_NUM_SOL').val());
                        InternoArr.push(data[i].versions_title);
                        InternoArr.push(data[i].versions_consecutive);
                        InternoArr.push(data[i].versions_description);

                        var actions = "<button class='btn btn-success' onclick = 'getURL(\"" + data[i].token + "\");'><i class='fa fa-fw fa-tv  success'></i></button>";
                        actions += "&nbsp;&nbsp;<button class='btn btn-info' onclick = 'SendValues(\"" + data[i].token + "\",\"" + $('#MV_NUM_SOL').val() + "\",\"" + data[i].versions_title + "\",\"" + data[i].alert + "\");'><i class='fa fa-check-circle-o success'></i></button>";
                  
                        if (data[i].alert == '1') {
                            actions += "&nbsp;&nbsp;<i class='fa  fa-check-circle-o  ' style='color: #00A65A;font-size: 120%; !important'></i>";
                        }
                        else {
                            actions += "&nbsp;&nbsp;<i class='fa  fa-exclamation-circle' style='color: #DD4B39;font-size: 120%; !important'></i>";
                        }

                        InternoArr.push(actions);
                        $('#TOKENSS').val(data[i].token);
                        Arreglo_arreglosdot.push(InternoArr);
                    }
                    var table = $('#TBLDOCUMENTS').DataTable();
                    table.destroy();
                    $('#TBLDOCUMENTS').DataTable({
                        data: Arreglo_arreglosdot,
                        columns: [
                            { title: "Folio Solicitud" },
                            { title: "Nombre del documento" },
                            { title: "Versión" },
                            { title: "Descripción" },

                            { title: "Acciones" }
                        ]
                    });
                    $('#MV_NUM_FILE').val('');
                    $('#MVoberservaciones').val('');
                    $('#NomDoc').val('');
            

                }, 'json');
    //}, 'json');
}

function getURLVersion(value_,Version) {
    $('#viewer_window_iframe').attr('src', '');
    var RES_ = '';
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + value_ + "/10";
    var URIENC = '';
    $.get(Uri, function (data, status) {
        URIENC = data;
        RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC + "/" + Version;
        var SCRH = ((screen.height / 4) * 3) - 40;
        $('#viewer_window_iframe').css('height', SCRH);
        $('#viewer_window_iframe').attr('src', RES_);
        console.log(RES_);
        console.log(SCRH);
        modalVisualizacion();
        return RES_;
    });

}
function getURL(value_) {
    //Limpiar viewer
    $('#viewer_window_iframe').attr('src', '');
    //


    var RES_ = '';
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + value_ + "/10";
    //alert(Uri);
    var URIENC = '';
    $.get(Uri, function (data, status) {
        //alert(data);
        URIENC = data;
        RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
        
        var SCRH = ((screen.height / 4) * 3) - 40;
        $('#viewer_window_iframe').css('height', SCRH);
        $('#viewer_window_iframe').attr('src', RES_);
        console.log(RES_);
        console.log(SCRH);
        modalVisualizacion();
        return RES_;
    });

}

function SendValues(Token, NumSol,nombre, alert) {
    window.location.replace("../VisorVesiones/" + Token + "|" + NumSol + '|' + nombre + '|' + $('#MV').val() + '|' + alert);
}
function SendValuesEval(Token, NumSol, nombre, alert) {
    window.location.replace("../VisorVesionesEvaluador/" + Token + "|" + NumSol + '|' + nombre + '|' + $('#MV').val() + '|' + alert);
}
 
function getdocInfo(Token, NumSol) {
    var confile = $('#EndPointFileAQ').val() + "Viewer/Version/" + Token;
    $.get(confile, function (data, status) {
        
    },'json');

}

//TBLDOCUMENTSEval_


function GetTableVisorTBLDOCUMENTSEval_() {
    var confile = $('#EndPointFileAQ').val() + "Viewer/Version/" + $('#IDD').val();
    var table = null;
    //var con = $('#EndPointAQ').val();
    //var confile = $('#EndPointAQ').val() + "MesaValidacion/get_mesa_solicitante/";
    //$.get(con + "MesaValidacion/get_mesa/" + $('#MV').val(),
    //    function (data, status) {
    $.get(confile,
        function (data, status) {
            var Arreglo_arreglosdot = [];
            for (var i = 0; i <= data.length - 1; i++) {
                var InternoArr = [];

                InternoArr.push(data[i].versions_title);
                InternoArr.push(data[i].versions_consecutive);
                InternoArr.push(data[i].versions_description);
                InternoArr.push(data[i].alert_description == null ? '' : data[i].alert_description);


                var actions = "<button class='btn btn-success' onclick = 'getURLVersion(\"" + data[i].token + "\",\"" + data[i].versions_consecutive + "\");'><i class='fa fa-fw fa-tv  success'></i></button>";
                if ((data.length - 1) == i) {
                    if (data[i].alert == '1') {
                        actions += "&nbsp;&nbsp;<button onclick=\"Rechazar('" + data[i].token + "');\" class='btn btn-danger cancel-btn' type='button' />Rechazar</button>";
                    }
                    else if (data[i].alert == '2') {
                        actions += "&nbsp;&nbsp;<button  disabled  class='btn btn-warning cancel-btn' type='button' />Rechazada</button>";

                    }
                }
                InternoArr.push(actions);
                $('#TOKENSS').val(data[i].token);
                Arreglo_arreglosdot.push(InternoArr);
            }
            var table = $('#TBLDOCUMENTSEval_').DataTable();
            table.destroy();
            $('#TBLDOCUMENTSEval_').DataTable({
                data: Arreglo_arreglosdot,
                columns: [

                    { title: "Nombre del documento" },
                    { title: "Versión" },
                    { title: "Descripción" },
                    { title: "Alerta" },


                    { title: "Acciones" }
                ]
            });
            $('#MV_NUM_FILE').val('');
            $('#MVoberservaciones').val('');
            $('#NomDoc').val('');


        }, 'json');
    //}, 'json');
}

function GetTableVisor() {
    var confile = $('#EndPointFileAQ').val() + "Viewer/Version/" + $('#IDD').val();
    var table = null;
    //var con = $('#EndPointAQ').val();
    //var confile = $('#EndPointAQ').val() + "MesaValidacion/get_mesa_solicitante/";
    //$.get(con + "MesaValidacion/get_mesa/" + $('#MV').val(),
    //    function (data, status) {
    $.get(confile,
        function (data, status) {
            var Arreglo_arreglosdot = [];
            for (var i = 0; i <= data.length - 1; i++) {
                var InternoArr = [];
                
                InternoArr.push(data[i].versions_title);
                InternoArr.push(data[i].versions_consecutive);
                InternoArr.push(data[i].versions_description);
                InternoArr.push(data[i].alert_description == null ? '' : data[i].alert_description);


                var actions = "<button class='btn btn-success' onclick = 'getURLVersion(\"" + data[i].token + "\",\"" + data[i].versions_consecutive + "\");'><i class='fa fa-fw fa-tv  success'></i></button>";
                if ((data.length - 1) == i) {
                    if (data[i].alert == '1') {
                        //actions += "&nbsp;&nbsp;<button onclick=\"Rechazar('" + data[i].token + "');\" class='btn btn-danger cancel-btn' type='button' />Rechazar</button>";
                    }
                    else if (data[i].alert == '2'){
                        actions += "&nbsp;&nbsp;<button  disabled  class='btn btn-warning cancel-btn' type='button' />Rechazada</button>";

                    }
                }
                InternoArr.push(actions);
                $('#TOKENSS').val(data[i].token);
                Arreglo_arreglosdot.push(InternoArr);
            }
            var table = $('#TBLDOCUMENTS').DataTable();
            table.destroy();
            $('#TBLDOCUMENTS').DataTable({
                data: Arreglo_arreglosdot,
                columns: [
                    
                    { title: "Nombre del documento" },
                    { title: "Versión" },
                    { title: "Descripción" },
                    { title: "Alerta" },


                    { title: "Acciones" }
                ]
            });
            $('#MV_NUM_FILE').val('');
            $('#MVoberservaciones').val('');
            $('#NomDoc').val('');


        }, 'json');
    //}, 'json');
}

function Rechazar(token) {
    $('#Alerta_send').modal('show');
    $('#hidden_').val(token);
}

function sendfile_version() {
    var con = $('#EndPointAQ').val();
    var fd = new FormData();
    var files = $('#MV_NUM_FILE').prop('files')[0];
    fd.append('file', files);
    fd.append('Descripcion', $('#MVoberservaciones').val());
    fd.append('Autor', $('#H6_val').html());
    fd.append('Nombre', $('#NomDoc2').val());
  

    $.ajax({
        url: $("#EndPointFileAQ").val() + 'Upload/Details/' + $('#IDD').val(),
        dataType: 'text',
        type: 'post',
        data: fd,
        contentType: false,
        cache: false,
        async: false,
        processData: false,
        success: function (response) {
            $.post(con + "MesaValidacion/Init/" + $('#MV').val() + "/" + response.split('"').join(""),
                function (data, status) {
                    GetTableVisor();

                    SuccessSA("Operación exitosa", 'Documento enviado');
                }, 'json');


        },
    });

}

function back() {
    window.location.href = "../index/" + $('#idgral').val();
}
function backeval() {
    window.location.href = "../Evaluador/" + $('#idgral').val();
}

function disable_fields() {
    if ($('#alert_txt').val() == '1') {
        $('.controls_').hide();
        $('#MV_NUM_FILE').hide();
        $('#MVoberservaciones').hide();
    }
}


function callAjax() {
    var formData = {
        'description_': $('#descripcion_txtarea').val(),
        'Token': $('#hidden_').val(),
        'tipo': 2
    };

    $.post($("#EndPointFileAQ").val() +'Viewer/Update/State', formData, function (response) {
        console.log("Response: " + response);
        SuccessSA("Operación exitosa", 'Documento enviado');
        GetTableVisorTBLDOCUMENTSEval_();
    });

}














