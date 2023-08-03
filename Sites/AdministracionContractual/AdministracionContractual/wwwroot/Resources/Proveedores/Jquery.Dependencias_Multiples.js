function GetDependenciasSecundarias() {
    $.get($("#EndPointAdmon").val() + "Dependencia/Get/XPermisoUsuario/" + $("#HDidInstancia").val() + "/" + $("#HDidUsuario").val(), function (data, status) {
        var body = "";
        console.log('obteniendo dependencias');
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            if (data[i].permiso == 1) {
                body += "<tr><td>" + data[i].dependencia + "</td><td><input type='checkbox' id='" + data[i].id + "' name='" + data[i].id + "' value='" + data[i].id + "'  class='dependencias_adicionales_nuevo'></td></tr>";
            } else {
                body += "<tr><td>" + data[i].dependencia + "</td><td><input type='checkbox' id='" + data[i].id + "' name='" + data[i].id + "' value='" + data[i].id + "'  class='dependencias_adicionales_nuevo' disabled></td></tr>";
            }
        }
        $('#tbl_body_dependencias').html(body);
    });
    return;
}

function muestraFormDependencias() {
    $('#formDependenciasAdd').removeClass('hidden');
    GetDependenciasSecundarias();

}

function ObtenerDependenciasSeleccionadasNuevo() {
    var dependencias_l = document.getElementsByClassName('dependencias_adicionales_nuevo');
    var dependencias_json = [];
    for (var i = 0; i < dependencias_l.length; i++) {
        if (dependencias_l[i].checked) {
            var d = {};
            console.log(dependencias_l[i].value);
            d.tbl_dependencia_id = dependencias_l[i].value;
            dependencias_json.push(d);
        }
    }
    console.log(dependencias_json);
    if (dependencias_json.length == 0) {
        return null;
    } else {
        return dependencias_json;
    }
}

function ValidarDependenciaNuevo() {
    let arreglo = ObtenerDependenciasSeleccionadasNuevo();
    if (arreglo == null) {
        return false;
    } else if (arreglo.length == 0) {
        return false;
    } else {
        return true;
    }
}

function ObtenerDependenciasSeleccionadasEditar() {
    var dependencias_l = document.getElementsByClassName('dependencias_adicionales_editar');
    var dependencias_json = [];
    for (var i = 0; i < dependencias_l.length; i++) {
        if (dependencias_l[i].checked) {
            var d = {};
            console.log(dependencias_l[i].value);
            d.tbl_dependencia_id = dependencias_l[i].value;
            dependencias_json.push(d);
        }
    }
    console.log(dependencias_json);
    if (dependencias_json.length == 0) {
        return null;
    } else {
        return dependencias_json;
    }
}

function ValidarDependenciaEditar() {
    let arreglo = ObtenerDependenciasSeleccionadasEditar();
    if (arreglo == null) {
        return false;
    } else if (arreglo.length == 0) {
        return false;
    } else {
        return true;
    }
}

function GetDependenciasSecundariasEditar() {
    $.get($("#EndPointAdmon").val() + "Dependencia/Get/XPermisoUsuario/" + $("#HDidInstancia").val() + "/" + $("#HDidUsuario").val(), function (data, status) {
        var body = "";
        console.log('obteniendo dependencias');
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            if (data[i].permiso == 1) {
                body += "<tr><td>" + data[i].dependencia + "</td><td><input type='checkbox' id='" + data[i].id + "' name='" + data[i].id + "' value='" + data[i].id + "'  class='dependencias_adicionales_editar'></td></tr>";
            } else {
                body += "<tr><td>" + data[i].dependencia + "</td><td><input type='checkbox' id='" + data[i].id + "' name='" + data[i].id + "' value='" + data[i].id + "'  class='dependencias_adicionales_editar' disabled></td></tr>";
            }

            
        }
        $('#tbl_body_dependencias_editar').html(body);
        GetDependenciasSecundariasGuardadas();
    });
    return;
}


function GetDependenciasSecundariasGuardadas() {
    $.get($("#EndPointAdmon").val() + "Proveedor/Get/proveedor_dependencias/" + $("#id_proveedor").val(), function (data, status) {
        var body = "";
        console.log('obteniendo dependencias del proveedor');
        console.log(data);
        for (var i = 0; i < data.length; i++) {
            console.log('dependencia adicional ' + data[i].nombre_dependencia);
            $("#" + data[i].tbl_dependencia_id + "").prop("checked", true);
        }
    });
    return;
}