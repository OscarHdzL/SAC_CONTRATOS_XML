//var URL_MOSTRAR_LOGIN = "/Login/ObtenerLogin/";

$(document).ready(function () {
    //mostrarLogin();
    setInterval("$('body').css('padding-right','0px');", 100);
    getURL_token_logo_miniatura($('#HD_logo_min').val());
    getURL_token_logo_inicio($('#HD_logo_ini').val());
});

function getURL_token_logo_miniatura(token_) {
    var Uri = $('#EndPointFileAC').val() + 'GeneraUrl/' + token_ + "/1";
    var URIENC = '';
    $.get(Uri, function (data, status) {
        URIENC = data;
        RES = $('#EndPointFileAC').val() + "Viewer/" + URIENC;
        $('#logo_header').attr('src', RES);
        return RES;
    });
}


function getURL_token_logo_inicio(token_) {
    var Uri = $('#EndPointFileAC').val() + 'GeneraUrl/' + token_ + "/1";
    var URIENC = '';
    $.get(Uri, function (data, status) {
        URIENC = data;
        RES = $('#EndPointFileAC').val() + "Viewer/" + URIENC;
        $('#logo_inicio').attr('src', RES);
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

function getURL(token_) {
    var RES_ = '';
    var Uri = $('#EndPointFileAC').val() + 'GeneraUrl/' + token_ + "/10";
    //alert(Uri);
    var URIENC = '';
    $.get(Uri, function (data, status) {
        //alert(data);
        URIENC = data;
        RES_ = $('#EndPointFileAC').val() + "Viewer/" + URIENC;
        var SCRH = ((screen.height / 4) * 3) - 40;
        $('#viewer_window_iframe').css('height', SCRH);
        $('#viewer_window_iframe').attr('src', RES_);
        return RES_;
    });

}

function modalVisualizacion() {
    $('#viewer_window').modal('show');
}
