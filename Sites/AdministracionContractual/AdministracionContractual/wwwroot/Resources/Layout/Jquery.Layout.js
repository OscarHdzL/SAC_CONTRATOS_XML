//var URL_MOSTRAR_LOGIN = "/Login/ObtenerLogin/";

$(document).ready(function () {
    //mostrarLogin();
    setInterval("$('body').css('padding-right','0px');", 100);
    getURL_token_logo_miniatura($('#HD_logo_min').val());
});


function getURL_token_logo_miniatura(token_) {
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/1";
    var URIENC = '';
    $.get(Uri, function (data, status) {
        URIENC = data;
        RES = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
        $('#logo_header').attr('src', RES);
        return RES;
    });
}


//function mostrarLogin() {
//    var idUsuario = 1;

//    $.get(URL_MOSTRAR_LOGIN + idUsuario, function (data, status) {
//        console.log(data)
//        $('body').append(data);
//    });
//}

function LaunchLoader(value) {
	if (value) {
		$('#Loader').modal('show');
	}
	else {
		$('#Loader').modal('hide');
	}
}
function LaunchLoaderFun(value) {
	if (value) {
		$('#LoaderFUN').modal('show');
	}
	else {
		$('#LoaderFUN').modal('hide');
	}
}
function validarLongitudCadena(cadena, longitud) {

    if (cadena.length > longitud) {
        return false;
    }
    else {
        return true;
    }
}

function ValidaCadenab(valor, Campo) {
    var input = valor.split('');
    var CharArray = [];
    CharArray.push('<');
    CharArray.push('~');
    CharArray.push('>');
    //CharArray.push('|');
    CharArray.push('\\');
    CharArray.push('°');
    //CharArray.push('/');
    CharArray.push('¬');
    CharArray.push('=');
    CharArray.push('*');
    CharArray.push('^');
    CharArray.push('&');
    CharArray.push('[');
    CharArray.push(']');
    CharArray.push('{');
    CharArray.push('}');
    CharArray.push('}');
    CharArray.push('$');
    /*CharArray.push('#');*/
    /*CharArray.push('\'');*/
    /*CharArray.push('"');*/
    /*CharArray.push('!');*/
    /*CharArray.push('¡');*/
    /*CharArray.push('¿');*/
    /*CharArray.push('?');*/


    for (var i = 0; i <= input.length - 1; i++) {
        if (CharArray.includes(input[i])) {
            return 'El caracter ' + CharArray[CharArray.indexOf(input[i])] + ' no esta permitido para el campo ' + Campo;
        }
    }
    return '';
}


function ValidaCadena(valor, Campo) {
    var input = valor.split('');
    var CharArray = [];
    CharArray.push('<');
    CharArray.push('~');           
    CharArray.push('>');
    CharArray.push('|');                
    CharArray.push('\\');
    CharArray.push('°');                 
    CharArray.push('/');
    CharArray.push('¬');              
    CharArray.push('=');                
    CharArray.push('*');                  
    CharArray.push('^');
    CharArray.push('&');
    CharArray.push('[');
    CharArray.push(']');
    CharArray.push('{');
    CharArray.push('}');
    CharArray.push('}');
    CharArray.push('$');
  /*CharArray.push('#');*/
  /*CharArray.push('\'');*/
  /*CharArray.push('"');*/
  /*CharArray.push('!');*/
  /*CharArray.push('¡');*/
  /*CharArray.push('¿');*/
  /*CharArray.push('?');*/


    for (var i = 0; i <= input.length - 1; i++) {
        if (CharArray.includes(input[i])) {
            return 'El caracter ' + CharArray[CharArray.indexOf(input[i])] + ' no esta permitido para el campo ' + Campo;
        }
    }
    return '';
}

$(document).ready(function () {
    $.extend($.fn.dataTable.defaults, {
        responsive: true
    });

    $(".eachTbl").each(function (indice, elemento) {
        $(elemento).DataTable();

    });

    Redimension();
});


function Redimension() {

    var tables = document.getElementsByTagName('table');
    for (var i = 0; i < tables.length; i++) {
        if (tables[i].id != "") {
            $('#' + tables[i].id + '').resize();
        }
    }

}

function Des_nav_dep() {
    var id = "";
    $.get("/Dependencias/Dependencias_areas/" + id, function (data, status) {
        return window.location.replace(route);
    });
}

function lista_estructura_org() {
    var id_instancia = $("#HDidInstancia").val();
    $.get($("#EndPointAdmon").val() + "Asignacion/Get/" + id_instancia, function (data, status) {
        console.log(data);
        if (data.length == 0) {
            ErrorSA("", "Error al cargar la lista.")
        }
        else {
            var formData = new FormData();
            formData.append("lista", JSON.stringify(data));
            console.log(JSON.stringify(data));

            $.ajax({
                url: '/AsignacionOrganizacional/lista',
                dataType: 'json',
                data: formData,
                contentType: false,
                type: 'POST',
                processData: false,
                success: function (data) {
                    console.log(data);
                },
                error: function (data) {

                }
            });
        }
    });
}