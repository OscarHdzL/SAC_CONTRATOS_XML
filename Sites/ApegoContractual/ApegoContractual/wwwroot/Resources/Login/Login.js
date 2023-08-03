
//(function () {
//    var entidad = {
//        Email: '',
//        Password: '',
//        Salto: ''
//    };
//})();

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

    var url = $('#EndPointAC').val() + "Usuarios/" + usuario;
    console.log(url)
    $.get(url, function (data, estatus) {

        if (data.length === 0) {
            ErrorSA("Error", "El usuario no existe.");
            return false;
        }

        url = $('#EndPointAC').val() + "Usuarios/VerificarPassword";

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
                console.log(data)
                var arrData = JSON.parse(data);

                if (arrData.length === 0) {
                    ErrorSA("Error", "La contraseña es incorrecta.");
                    return false;
                }

                console.log(arrData);

                var item = arrData[0];

                var formData = new FormData();
                
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



                formData.append("strUsuario", JSON.stringify(dataSesion));

                $.ajax({
                    url: '/Login/InicializarSesiones',
                    //dataType: 'json',
                    //data: { strUsuario: JSON.stringify(dataSesion) },
                    data: formData,
                    //contentType: 'application/json; charset=utf-8',
                    contentType: false,
                    type: 'POST',
                    processData: false,
                    success: function (data) {
                        console.log(data);

                        window.location.href = "Home/Index";
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