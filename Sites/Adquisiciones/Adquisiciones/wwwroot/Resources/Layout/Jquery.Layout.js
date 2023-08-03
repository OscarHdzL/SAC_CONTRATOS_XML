
$(document).ready(function () {
    setInterval("$('body').css('padding-right','0px');", 100);
    getURL_token_logo_miniatura($('#HD_logo_min').val());
    getlogohome($('#HD_logo_ini').val());
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
function getlogohome(token_) {
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/1";
    var URIENC = '';
    $.get(Uri, function (data, status) {
        URIENC = data;
        RES = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
        $('#logo_inicio').attr('src', RES);
        return RES;
    });
}

function LaunchLoader(value) {
	if (value) {
		$('#Loader').modal('show');
	}
	else {
		$('#Loader').modal('hide');
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
    CharArray.push('\\');
    CharArray.push('°');
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