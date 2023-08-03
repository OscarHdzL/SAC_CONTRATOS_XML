
//(function () {
//    var entidad = {
//        Email: '',
//        Password: '',
//        Salto: ''
//    };
//})();

var con = $("#EndPointAdmon").val();
//var con = "https://localhost:44359/";
var URL_MOSTRAR_LOGIN = "/Login/ObtenerLogin/";

$(document).ready(function () {
    mostrarLogin();
});

function getTime() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById('showtime').innerHTML = h + ":" + m + ":" + s;
    t = setTimeout(function () { getTime(); }, 500);
}

function myFunction() {
    var x = document.getElementById("Contrasena");
    if (x.type == "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function myFunctionb() {
    var x = document.getElementById("Contrasena_n");
    var y = document.getElementById("Contrasena_r");
    if (x.type == "password") {
        x.type = "text";
        y.type = "text";
    } else {
        x.type = "password";
        y.type = "password";
    }
}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

function mostrarLogin() {
    $.get(URL_MOSTRAR_LOGIN, function (data, status) {
        $('#contentLogin').html(data);
    });
}

function ActPassw() {
    var Validacion = ValidarPass();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = {};
        OBJ_Form.id = $('#id_user').val();
        OBJ_Form.usuario = "";
        OBJ_Form.nombre = "";
        OBJ_Form.ap_paterno = "";
        OBJ_Form.ap_materno = "";
        OBJ_Form.password = $('#Contrasena_r').val();
        OBJ_Form.salto = "";
        OBJ_Form.activo = "";
        OBJ_Form.super_usuario = "";
        OBJ_Form.tbl_estatus_autenticacion_id = "";
        OBJ_Form.estatus_autenticacion = "";
        OBJ_Form.tbl_dependencia_id = "";
        OBJ_Form.tbl_rol_id = "";
        OBJ_Form.tbl_instancia_id = "";
        OBJ_Form.tbl_rol_usuario_id = "";

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'put',

            success: function (data) {
                var data_b = $.parseJSON(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", data_b[0].msg);
                    var conr = $('#Contrasena_r').val();
                    $('#Contrasena').val(conr);                    
                    $('#ModalNewPass').modal('hide');
                    //$('#contentLogin').prop('hidden', false);
                    $('.Clean').val('');
                    ValidarLogin();
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Usuarios/NewPassword")

        })
    }
}

function recupass() {
    if ($('#Usuario').val() != '') {
        $('#ModalNewPass').modal('show');
        $('#recupcon').show();
        $('.ActCon').hide();
    } else {
        ErrorSA('Error en los datos de entrada', 'Ingrese el usuario');
    }
}
function recovePassw() {
    var Validacion = ValidarPass();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = {}; 
        OBJ_Form.id = "";
        OBJ_Form.usuario = $('#Usuario').val();
        OBJ_Form.password = $('#Contrasena_r').val();
        OBJ_Form.salto = "";
        OBJ_Form.activo = "";
        OBJ_Form.super_usuario = "";
        OBJ_Form.tbl_estatus_autenticacion_id = "";
        OBJ_Form.tbl_persona_id = "";
        OBJ_Form.tbl_instancia_id = "";
        OBJ_Form.email = "";

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'put',

            success: function (data) {
                var data_b = $.parseJSON(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    //SuccessSA("Operación exitosa", data_b[0].msg);
                    SuccessSA("Operación exitosa", "Correo enviado, favor de revisar");
                    //var conr = $('#Contrasena_r').val();
                    //$('#Contrasena').val(conr);
                    $('#ModalNewPass').modal('hide');
                    //$('.Clean').val('');
                    //ValidarLogin();
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Usuarios/Recuperarpassword")

        })
    }
}
function ValidarPass() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#Contrasena_n').val() == '') {
        Response.Texto = 'Debe ingresar una contraseña';
        Response.Bit = true;
        return Response;
    }
    if ($('#Contrasena_r').val() == '') {
        Response.Texto = 'Debe repetir la contraseña';
        Response.Bit = true;
        return Response;
    }
    if ($('#Contrasena_n').val() != $('#Contrasena_r').val()) {
        Response.Texto = 'La contraseña no coincide';
        Response.Bit = true;
        return Response;
    }    

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function ValidarLogin() {

    var usuario = $("#Usuario").val();
    var contrasena = $("#Contrasena").val();

    if (usuario == "") {
        ErrorSA('Hay un error en los datos de entrada', "El campo 'Usuario' es requerido.");
        return;
    }

    if (contrasena == "") {
        ErrorSA('Hay un error en los datos de entrada', "El campo 'Contraseña' es requerido.");
        return;
    }

    //var obj = {
    //    Usuario: usuario,
    //    Contrasena: contrasena,
    //    ModoAutenticacion: 1
    //}

    var url = con + "Usuarios/" + usuario;
    console.log(url)
    $.get(url, function (data, estatus) {

        if (data.length === 0) {
            ErrorSA("Error", "El usuario no existe.");
            return false;
        }

        url = con + "Usuarios/VerificarPassword";

        console.log(url);

        entidad.Email = usuario;
        entidad.Password = contrasena;

        $.ajax({
            url: url,
            data: JSON.stringify(entidad),
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            type: "POST",
            processData: false,
            success: function (data) {
                //console.log(data)
                var arrData = JSON.parse(data);
                var item = arrData[0];
                var formData = new FormData();
                if (arrData.length === 0) {
                    ErrorSA("Error", "La contraseña es incorrecta.");
                    return false;
                }
                if (item.estatus_autenticacion === 'CONTRASEÑA NO ACTUALIZADA') {
                    $('#id_user').val(item.id);                    
                    $('#ModalNewPass').modal('show');
                    $('#recupcon').hide();
                    $('.ActCon').show();
                    return false;
                    
                }
                else {                   
                    //console.log(arrData);  
                    var dataSesion = {
                        "ID_USUARIO": item.id,
                        "NOMBRE_USUARIO": item.nombre + " " + item.ap_paterno + " " + item.ap_materno,
                        "CORREO": item.usuario,
                        "PASSWORD": item.password,
                        "ID_INSTANCIA": item.tbl_instancia_id,
                        "ES_SUPER_USUARIO": item.super_usuario,
                        "ID_DEPENDENCIA": item.tbl_dependencia_id,
                        "ID_ROL": item.tbl_rol_id
                    };
                }


                formData.append("strUsuario", JSON.stringify(dataSesion));

                $.ajax({
                    url: 'Login/InicializarSesiones',
                    //dataType: 'json',
                    //data: { strUsuario: JSON.stringify(dataSesion) },
                    data: formData,
                    //contentType: 'application/json; charset=utf-8',
                    contentType: false,
                    type: 'POST',
                    processData: false,
                    success: function (data) {
                        console.log(data);

                        window.location.href = "Home";
                    },
                    error: function (data) {

                    }
                });
            },
            error: function () {

            }
        });
    });
}

var entidad = {
    Email: '',
    Password: '',
    Salto: ''
};