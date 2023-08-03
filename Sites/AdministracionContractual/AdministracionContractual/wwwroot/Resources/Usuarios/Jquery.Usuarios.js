$(document).ready(function () {
    LaunchLoader(true);
    AjusteTabla();
    getUsuarios();

    $('#txt_Fecha_Inicial, #txt_Fecha_Final, #txt_Fecha_Inicial_ed, #txt_Fecha_Final_ed').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    //$('#txt_Fecha_Final').datetimepicker({
    //    format: 'YYYY-MM-DD'
    //});
    var correo_editar, rfc_editar = null;
});

$(".btn-danger").click(function () {    
    $('.Clean').val(''); 
});

function AjusteTabla() {
    $('#tbl_Usuarios').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "5%", "targets": 0 },
            { "width": "25%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "15%", "targets": 4 },
            { "width": "5%", "targets": 5 },
            { "width": "10%", "targets": 6 }
        ],
    });    
}

var con = $("#EndPointAdmon").val();
//var con = "https://localhost:44359/";
var ins = $('#HDidInstancia').val();

function getUsuarios() {
    $.get(con + "Usuarios/Get_Lista/" + ins, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var ir_b = null;
            var Interno = [];
            Interno.push(i + 1);
            Interno.push(data[i].email);
            Interno.push(data[i].nombre);
            Interno.push(data[i].ap_paterno);
            Interno.push(data[i].ap_materno);
            //Interno.push(data1[i].activo);
            if (data[i].activo != null) {
                if (data[i].activo == 1) {
                    //Interno.push("<button class='btn btn-success' title='Activo'><i class='glyphicon glyphicon-ok'></i></button>");
                    Interno.push("<button class='btn btn-default' title='Desactivar' onclick=\"DeleteU('" + data[i].id_persona + "');\"><i class='glyphicon glyphicon-check'></i></button>");
                }
                else if (data[i].activo == 0) {
                    //Interno.push("<button class='btn btn-danger' title='Inactivo'><i class='glyphicon glyphicon-ban-circle'></i></button>");
                    Interno.push("<button class='btn btn-default' title='Activar' onclick=\"ActivU('" + data[i].id_persona + "');\"><i class='glyphicon glyphicon-unchecked'></i></button>");
                }
            }
            Interno.push("<button class='btn btn-default' title='Detalle' onclick=\"muestraModaldetalle('" + data[i].id_persona + "');\"><i class='fa fa-send'></i></button> <button class='btn btn-primary' title='Editar' onclick=\"muestraModalEditar('" + data[i].id_persona + "');\"><i class='fa fa-edit'></i></button>");
            Arreglo_arreglos.push(Interno);
        }
        var table = $('#tbl_Usuarios').DataTable();
        table.destroy();
        //console.log(Arreglo_arreglos);
        $('#tbl_Usuarios').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Email" },
                { title: "Nombre" },
                { title: "Apellido Paterno" },
                { title: "Apellido Materno" },
                { title: "Activo" },
                { title: "Acción" }
            ],
            columnDefs: [
                {
                    targets: '_all',
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });
}

function muestraModalAgregarUsuario() {
    $('.Clean').val('');
    $('.Clean').prop("disabled", false);   
    $('#SuperUser').prop("checked", false);
    GetDependenciasM();
    GetRol();
    GetDependenciasSecundarias();
    $('#ddl_Area').html('');    
    $('#ddl_Area').prop("disabled", true);
    $('#txt_Fecha_Inicial').data("DateTimePicker").clear();
    $('#txt_Fecha_Final').data("DateTimePicker").clear();
    $('#ModalAgregarUsuario').modal('show');
}

function GetDependenciasM(id) {
    $.get(con + "Dependencia/Get/" + ins, function (data, status) {        
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].dependencia + "</option>";
        }
        $('#DependenciaA').html(body);
        $('#DependenciaA_ed').html(body);
        $('#DependenciaA_ed > option[value="' + id + '"]').attr("selected", "selected");
    });
    return;
}

function GetDependenciasSecundarias() {
    $.get(con + "Dependencia/Get/" + ins, function (data, status) {
        var body = "";
        console.log('obteniendo dependencias');
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            body += "<tr><td>" + data[i].dependencia + "</td><td><input type='checkbox' id='" + data[i].id + "' name='" + data[i].id + "' value='" + data[i].id + "'  class='dependencias_adicionales'></td></tr>"
        }
        $('#tbl_body_dependencias').html(body);
    });
    return;
}

function GetDependenciasSecundariasM() {
    $.get(con + "Dependencia/Get/" + ins, function (data, status) {
        console.log('obteniendo dependencias');
        console.log(data);
        var body = "";
        for (var i = 0; i <= data.length - 1; i++) {
            body += "<tr><td>" + data[i].dependencia + "</td><td><input type='checkbox' id='" + data[i].id + "' name='" + data[i].id + "' value='" + data[i].id + "'  class='dependencias_adicionales_editar'></td></tr>"
        }
        $('#tbl_body_dependencias_editar').html(body);
    });
    return;
}
function getDependenciasAsignadasUsuario(id_usuario) {
    $.get(con + "Usuarios/Get/DependenciasAsignadas/Usuario/" + id_usuario, function (data, status) {
        console.log('obteniendo dependencias del usuario');
        console.log(data);
        for (var i = 0; i < data.length; i++) {
            if (data[i].principal == 1) {
                console.log('dependencia principal ' + data[i].nombre_dependencia);
            }
            if (data[i].principal == 0) {
                console.log('dependencia adicional ' + data[i].nombre_dependencia);
                $("#"+data[i].tbl_dependencia_id+"").prop("checked", true);
            }
        }

    });
}

function ObtenerDependenciasSeleccionadas() {
    var dependencias_l = document.getElementsByClassName('dependencias_adicionales');
    var dependencias_json = [];
    for (var i = 0; i < dependencias_l.length; i++) {
        if (dependencias_l[i].checked) {
            console.log(dependencias_l[i].value);
            var dseleccionada = {};
            dseleccionada.tbl_dependencia_id = dependencias_l[i].value;
            dependencias_json.push(dseleccionada);
            //rate_value = rates[i].value;
        }
    }
    console.log(dependencias_json);
    if (dependencias_json.length == 0) {
        return null;
    } else {
        return dependencias_json;
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
            //rate_value = rates[i].value;
        }
    }
    console.log(dependencias_json);
    if (dependencias_json.length == 0) {
        return [];
    } else {
        return dependencias_json;
    }
}

function GetRol(id) {
    $.get(con + "Usuarios/Get/Dropdown/Rol", function (data, status) {        
        var body = "<option selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#ddl_Rol').html(body);
        $('#ddl_Rol_ed').html(body);
        //$('#ddl_Rol_ed > option[value="' + id+ '"]').attr("selected", "selected");
    });
    return;
}

$("#DependenciaA").change(function () {
    GetSelectiveAreas($('#DependenciaA').val())
    $('#ddl_Area').prop("disabled", false)
});

$("#DependenciaA_ed").change(function () {
    GetSelectiveAreas($('#DependenciaA_ed').val())
    $('#ddl_Area_ed').prop("disabled", false)
});
function GetSelectiveAreas(dep, id) {
    $.get(con + "Area/Get/DropDown/" + dep, function (data, status) {
        var body = "<option disabled selected value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#ddl_Area').html(body);
        $('#ddl_Area_ed').html(body);
        $('#ddl_Area_ed > option[value="' + id+ '"]').attr("selected", "selected");
    });
    return;
}

function muestraModalEditar(item) { 
    $("#id_Persona_ed").val(item);
    GetDependenciasSecundariasM();
    getedit(item);
    $('#title').html('Editar usuario');
    $('.Clean').prop("disabled", false);
    $('#EditarUsuario').show(true);
    $('#SuperUser_ed').prop("disabled", false);
    $('#ModalEditarUsuario').modal('show');
    $('#add_rol').prop("disabled", false);

}
function muestraModaldetalle(item) {
    geteditDetalle(item);
    $('#title').html('Detalle usuario');
    $('#ModalEditarUsuario').modal('show');
    $('.Clean').prop("disabled", true);
    $('#SuperUser_ed').prop("disabled", true);
    $('#add_rol').prop("disabled", true);

    $('#EditarUsuario').hide(true);

}

function getedit(item) {
    correo_editar = null;
    rfc_editar = null;
    var id_usuario;
    $.get(con + "Usuarios/Get_Persona/" + item, function (data, status) {

        if (data !== null) {
            for (var i = 0; i <= data.length - 1; i++) {
                $('#txt_Nombre_ed').val(data[i].nombre);
                $('#txt_Apellido_Paterno_ed').val(data[i].ap_paterno);
                $('#txt_Apellido_Materno_ed').val(data[i].ap_materno);
                $('#txt_Correo_ed').val(data[i].email);
                $('#txt_RFC_ed').val(data[i].rfc);
                $('#Telefono_ed').val(data[i].telefono);
                $('#Extension_ed').val(data[i].extencion);
                $('#txt_usuario_ed').val(data[i].usuario);
                var fechainicio = (data[i].fecha_inicio).split('T');
                var fechafin = (data[i].fecha_fin).split('T');
                $('#txt_Fecha_Inicial_ed').val(fechainicio[0]);
                $('#txt_Fecha_Final_ed').val(fechafin[0]);
                GetSelectiveAreas(data[i].id_dependencia, data[i].id_area);
                GetDependenciasM(data[i].id_dependencia);
                GetRol(data[i].id_rol);
                if (data[i].super_usuario == true) {
                    $('#SuperUser_ed').prop("checked", true);
                } else if (data[i].super_usuario == false){
                    $('#SuperUser_ed').prop("checked", false);
                }
                correo_editar = data[i].email;
                rfc_editar = data[i].rfc;
                id_usuario = data[i].id_usuario;
                $("#id_Usuario_ed").val(data[i].id_usuario);

            }
            getRolesUsuario(id_usuario);
            getDependenciasAsignadasUsuario(id_usuario);
        }

    });
}

function geteditDetalle(item) {
    correo_editar = null;
    rfc_editar = null;
    var id_usuario;
    $.get(con + "Usuarios/Get_Persona/" + item, function (data, status) {

        if (data !== null) {
            for (var i = 0; i <= data.length - 1; i++) {
                $('#txt_Nombre_ed').val(data[i].nombre);
                $('#txt_Apellido_Paterno_ed').val(data[i].ap_paterno);
                $('#txt_Apellido_Materno_ed').val(data[i].ap_materno);
                $('#txt_Correo_ed').val(data[i].email);
                $('#txt_RFC_ed').val(data[i].rfc);
                $('#Telefono_ed').val(data[i].telefono);
                $('#Extension_ed').val(data[i].extencion);
                $('#txt_usuario_ed').val(data[i].usuario);
                var fechainicio = (data[i].fecha_inicio).split('T');
                var fechafin = (data[i].fecha_fin).split('T');
                $('#txt_Fecha_Inicial_ed').val(fechainicio[0]);
                $('#txt_Fecha_Final_ed').val(fechafin[0]);
                GetSelectiveAreas(data[i].id_dependencia, data[i].id_area);
                GetDependenciasM(data[i].id_dependencia);
                GetRol(data[i].id_rol);
                if (data[i].super_usuario == true) {
                    $('#SuperUser_ed').prop("checked", true);
                } else if (data[i].super_usuario == false) {
                    $('#SuperUser_ed').prop("checked", false);
                }
                correo_editar = data[i].email;
                rfc_editar = data[i].rfc;
                id_usuario = data[i].id_usuario;
                $("#id_Usuario_ed").val(data[i].id_usuario);

            }

            getRolesUsuarioDetalle(id_usuario);

        }

    });
}

function getRolesUsuario(id_usuario) {
    $('.form-check').addClass('hidden');

    $.get(con + "Usuarios/Get_Roles/" + id_usuario, function (data, status) {

        if (data !== null) {
            var html = "";
            var principal;
            var tbl_usuario_rol_id;
            var checked;

            for (var i = 0; i <= data.length - 1; i++) {
                if (data[i].principal == 1) {
                    checked = "checked";
                } else {
                    checked = "";
                }

                html += "<tr><td>" + data[i].nombre + "</td><td><input type='radio' id='" + data[i].id + "' name='optradio' " + checked + " onclick='actualizaPrincipal(this.id)'></td><td><button type='button' class='btn btn-primary' name='btn_delete_rol' title='Borrar rol' id='" + data[i].id + "' onclick='DeleteR(this.id)' ><i class='fa fa-trash' aria-hidden='true'></i></button></td></tr>"

                if (data[i].tbl_rol_id == 'd7032c28-864b-11ea-b826-00155d1b3502') {
                    $('.form-check').removeClass('hidden');
                }
            }
            $('#tbl_body_roles').html(html);


        }

    });

}

function getRolesUsuarioDetalle(id_usuario) {
    $('.form-check').addClass('hidden');
    $.get(con + "Usuarios/Get_Roles/" + id_usuario, function (data, status) {

        if (data !== null) {
            var html = "";
            var principal;
            var tbl_usuario_rol_id;
            var checked;

            for (var i = 0; i <= data.length - 1; i++) {
                if (data[i].principal == 1) {
                    checked = "checked";
                } else {
                    checked = "";
                }

                html += "<tr><td>" + data[i].nombre + "</td><td><input type='radio' id='" + data[i].id + "' name='optradio' " + checked + " onclick='actualizaPrincipal(this.id)' disabled></td><td><button type='button' class='btn btn-primary' name='btn_delete_rol' title='Borrar rol' id='" + data[i].id + "' onclick='DeleteR(this.id)' disabled><i class='fa fa-trash' aria-hidden='true'></i></button></td></tr>"

                if (data[i].tbl_rol_id == 'd7032c28-864b-11ea-b826-00155d1b3502') {
                    $('.form-check').removeClass('hidden');
                }
            }
            $('#tbl_body_roles').html(html);


        }

    });

}

function actualizaPrincipal(idRolUsuario) {
    var OBJ_usuario_rol = obj_usuario_rol;

    OBJ_usuario_rol.idRolUsuario = idRolUsuario;
   
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_usuario_rol),
        type: 'put',

        success: function (data) {
            var data_b = $.parseJSON(data);

            if (data_b[0].cod == 'success') {
                SuccessSA("Se actualizó el rol principal", '');
            }
            else {
                ErrorSA("", data_b[0].msg);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', data_b[0].msg)
        },
        url: (con + "Usuarios/Update_Roles")

    })
}


function DeleteR(idRolUsuario) {
    function Confirmacion() {
        return deleteRol(idRolUsuario);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de borrar un rol ¿Desea continuar?", "Si", "No", AccionSi, "");
}

function deleteRol(idRolUsuario) {
    var OBJ_usuario_rol = obj_usuario_rol;

    OBJ_usuario_rol.idRolUsuario = idRolUsuario;

    var id_usuario = $("#id_Usuario_ed").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_usuario_rol),
        type: 'delete',
        success: function (data) {
            var data_b = $.parseJSON(data);

            if (data_b[0].cod == 'success') {
                SuccessSA("Se eliminó el rol", '');
                getRolesUsuario(id_usuario);

            }
            else {
                ErrorSA("", data_b[0].msg);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', data_b[0].msg)
        },
        url: (con + "Usuarios/Delete_Roles")
    })
}

function add_rol(id_rol) {
    var OBJ_rol = obj_rol;

    OBJ_rol.idRol = id_rol;
    OBJ_rol.idUsuario = $("#id_Usuario_ed").val();


    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_rol),
        type: 'post',

        success: function (data) {
            var data_b = $.parseJSON(data);

            if (data_b[0].cod == 'success') {
                SuccessSA("Se agrego un rol", '');
                getRolesUsuario(OBJ_rol.idUsuario);
            }
            else {
                ErrorSA("", data_b[0].msg);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', data_b[0].msg)
        },
        url: (con + "Usuarios/Add_roles")

    })
}


$("#GuardarUsuario").click(function () {
    AddUsuario();
});
$("#EditarUsuario").click(function () {
    EditUsuario();
});

function AddUsuario() {
    var Validacion = ValidarUsuarios();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_usuario_class;
        OBJ_Form.id_persona = '00000000-0000-0000-0000-000000000000';
        OBJ_Form.email = $('#txt_Correo').val();
        OBJ_Form.ap_paterno = $('#txt_Apellido_Paterno').val();
        OBJ_Form.ap_materno = $('#txt_Apellido_Materno').val();
        OBJ_Form.nombre = $('#txt_Nombre').val();
        OBJ_Form.rfc = $('#txt_RFC').val();
        OBJ_Form.telefono = $('#Telefono').val();
        OBJ_Form.extencion = $('#Extension').val();
        OBJ_Form.fecha_inicio = $('#txt_Fecha_Inicial').val();
        OBJ_Form.fecha_fin = $('#txt_Fecha_Final').val();
        OBJ_Form.id_dependencia = $('#DependenciaA').val();
        OBJ_Form.id_area = $('#ddl_Area').val();
        OBJ_Form.id_rol = $('#ddl_Rol').val();
        var i = $('#SuperUser').is(":checked") ? 1 : 0;
        OBJ_Form.super_usuario = i;
        OBJ_Form.usuario = $('#txt_usuario').val();
        OBJ_Form.dependencias_adicionales = ObtenerDependenciasSeleccionadas();
        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'post',

            success: function (data) {
                var data_b = $.parseJSON(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                        SuccessSA("Operación exitosa", data_b[0].msg);
                        getUsuarios();
                        $('#ModalAgregarUsuario').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Usuarios/Add")

        })
    }
}

$(function () {
    $('#txt_Correo').focusout(function () {
        if ($('#txt_Correo').val() !== '') {
            $.get(con + 'Usuarios/Get/exist/' + 1 + '/' + $('#txt_Correo').val(), function (data, status) {
                if (data[0].cod == 'warning') {
                    Aviso_ErrorSA('', 'El correo ya existe');
                    $('#txt_Correo').val('');
                }
            });
        }
    });
    $('#txt_Correo_ed').focusout(function () {
        if ($('#txt_Correo_ed').val() !== correo_editar) {
            $.get(con + 'Usuarios/Get/exist/' + 1 + '/' + $('#txt_Correo_ed').val(), function (data, status) {
                if (data[0].cod == 'warning') {
                    Aviso_ErrorSA('', 'El correo que intesta actualizar ya existe');
                    $('#txt_Correo_ed').val(correo_editar);
                }
            });
        }
    });
    $('#txt_RFC').focusout(function () {
        if ($('#txt_RFC').val() !== '') {
            $.get(con + 'Usuarios/Get/exist/' + 2 + '/' + $('#txt_RFC').val(), function (data, status) {
                if (data[0].cod == 'warning') {
                    Aviso_ErrorSA('', 'El RFC ya existe');
                    $('#txt_RFC').val('');
                }
            });
        }
    });
    $('#txt_RFC_ed').focusout(function () {
        if ($('#txt_RFC_ed').val() !== rfc_editar) {
            $.get(con + 'Usuarios/Get/exist/' + 2 + '/' + $('#txt_RFC_ed').val(), function (data, status) {
                if (data[0].cod == 'warning') {
                    Aviso_ErrorSA('', 'El RFC que intesta actualizar ya existe');
                    $('#txt_RFC_ed').val(rfc_editar);
                }
            });
        }
    });
    $('#txt_usuario').focusout(function () {
        if ($('#txt_usuario').val() !== '') {
            $.get(con + 'Usuarios/Get/exist/' + 3 + '/' + $('#txt_usuario').val(), function (data, status) {
                if (data[0].cod == 'warning') {
                    Aviso_ErrorSA('', 'El usuario ya existe');
                    $('#txt_usuario').val('');
                }
            });
        }
    });
}); 

function ValidarUsuarios() {
    var Response = { Texto: '', Bit: true, objeto: null };
    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if ($('#txt_Nombre').val() == '') {
        Response.Texto = 'Debe agregar un nombre';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Nombre').val(), 'txt_Nombre') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Nombre"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Apellido_Paterno').val() == '') {
        Response.Texto = 'Debe agregar un apellido paterno';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Apellido_Paterno').val(), 'txt_Apellido_Paterno') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Apellido Paterno"';
        Response.Bit = true;
        return Response;
    }
    //if ($('#txt_Apellido_Materno').val() == '') {
    //    Response.Texto = 'Debe agregar un apellido materno';
    //    Response.Bit = true;
    //    return Response;
    //}
    //else if (ValidaCadena($('#txt_Apellido_Materno').val(), 'txt_Apellido_Materno') != '') {
    //    Response.Texto = 'No se permiten caracteres especiales en el campo "Apellido Materno"';
    //    Response.Bit = true;
    //    return Response;
    //}
    if ($('#txt_RFC').val() == '') {
        Response.Texto = 'Debe agregar un RFC';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_RFC').val(), 'txt_RFC') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "RFC"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_RFC').val().length < 13) {
        Response.Texto = 'Debe ingresar un RFC valido, mínimo 13 caracteres.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Fecha_Inicial').val() == '') {
        Response.Texto = 'Debe ingresar una fecha inicial';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Fecha_Final').val() == '') {
        Response.Texto = 'Debe ingresar una fecha final';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Correo').val() == '') {
        Response.Texto = 'Debe ingresar un correo';
        Response.Bit = true;
        return Response;
    }
    if (regex.test($('#txt_Correo').val().trim())) {
        //alert('Correo validado');

    } else {
        Response.Texto = 'La direccón de correo no es válida';
        Response.Bit = true;
        return Response;
        //alert('La direccón de correo no es válida');
    }
    if ($('#Telefono').val() == '') {
        Response.Texto = 'Debe ingresar un teléfono';
        Response.Bit = true;
        return Response;
    }
    if ($('#DependenciaA').val() == '') {
        Response.Texto = 'Debe seleccionar una dependencia';
        Response.Bit = true;
        return Response;
    }
    if ($('#ddl_Area').val() == null) {
        Response.Texto = 'Debe seleccionar un área';
        Response.Bit = true;
        return Response;
    }
    if ($('#ddl_Rol').val() == null) {
        Response.Texto = 'Debe seleccionar un rol';
        Response.Bit = true;
        return Response;
    }
    if (ValidarDependenciaAgregar()) {
        Response.Texto = 'La dependencia principal no debe estar seleccionada en las dependencias adicionales';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
function ValidarDependenciaAgregar() {
    let arreglo = ObtenerDependenciasSeleccionadas();
    if (arreglo == null) {
        return false;
    }
    if (arreglo.length == 0) {
        return false;
    }
    let duplicado = arreglo.findIndex(x => x.tbl_dependencia_id == $('#DependenciaA').val());
    if (duplicado != -1) {
        return true;
    } else {
        return false;
    }
}


function EditUsuario() {
    var Validacion = ValidarUsuarioedit();
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_usuario_class;
        OBJ_Form.id_persona = $("#id_Persona_ed").val();
        OBJ_Form.email = $('#txt_Correo_ed').val();
        OBJ_Form.ap_paterno = $('#txt_Apellido_Paterno_ed').val();
        OBJ_Form.ap_materno = $('#txt_Apellido_Materno_ed').val();
        OBJ_Form.nombre = $('#txt_Nombre_ed').val();
        OBJ_Form.rfc = $('#txt_RFC_ed').val();
        OBJ_Form.telefono = $('#Telefono_ed').val();
        OBJ_Form.extencion = $('#Extension_ed').val();
        OBJ_Form.fecha_inicio = $('#txt_Fecha_Inicial_ed').val();
        OBJ_Form.fecha_fin = $('#txt_Fecha_Final_ed').val();
        OBJ_Form.id_dependencia = $('#DependenciaA_ed').val();
        OBJ_Form.id_area = $('#ddl_Area_ed').val();
        OBJ_Form.id_rol = 0;        
        OBJ_Form.super_usuario = $('#SuperUser_ed').is(":checked") ? 1 : 0;
        OBJ_Form.dependencias_adicionales = ObtenerDependenciasSeleccionadasEditar();
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
                    getUsuarios();
                    $('#ModalEditarUsuario').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "Usuarios/Update")

        })
    }
}
function ValidarUsuarioedit() {
    var Response = { Texto: '', Bit: true, objeto: null };
    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if ($('#txt_Nombre_ed').val() == '') {
        Response.Texto = 'Debe agregar un nombre';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Nombre_ed').val(), 'txt_Nombre_ed') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Nombre"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Apellido_Paterno_ed').val() == '') {
        Response.Texto = 'Debe agregar un apellido paterno';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Apellido_Paterno_ed').val(), 'txt_Apellido_Paterno_ed') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Apellido Paterno"';
        Response.Bit = true;
        return Response;
    }
    //if ($('#txt_Apellido_Materno_ed').val() == '') {
    //    Response.Texto = 'Debe agregar un apellido materno';
    //    Response.Bit = true;
    //    return Response;
    //}
    //else if (ValidaCadena($('#txt_Apellido_Materno_ed').val(), 'txt_Apellido_Materno_ed') != '') {
    //    Response.Texto = 'No se permiten caracteres especiales en el campo "Apellido Materno"';
    //    Response.Bit = true;
    //    return Response;
    //}
    if ($('#txt_RFC_ed').val() == '') {
        Response.Texto = 'Debe agregar un RFC';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('#txt_RFC_ed').val(), 'txt_RFC_ed') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "RFC"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_RFC_ed').val().length < 13) {
        Response.Texto = 'Debe ingresar un RFC valido, mínimo 13 caracteres.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Fecha_Inicial_ed').val() == '') {
        Response.Texto = 'Debe ingresar una fecha inicial';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Fecha_Final_ed').val() == '') {
        Response.Texto = 'Debe ingresar una fecha final';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_Correo_ed').val() == '') {
        Response.Texto = 'Debe ingresar un correo';
        Response.Bit = true;
        return Response;
    }
    if (regex.test($('#txt_Correo_ed').val().trim())) {
        //alert('Correo validado');

    } else {
        Response.Texto = 'La direccón de correo no es válida';
        Response.Bit = true;
        return Response;
        //alert('La direccón de correo no es válida');
    }
    if ($('#Telefono_ed').val() == '') {
        Response.Texto = 'Debe ingresar un teléfono';
        Response.Bit = true;
        return Response;
    }
    if ($('#DependenciaA_ed').val() == '') {
        Response.Texto = 'Debe seleccionar una dependencia';
        Response.Bit = true;
        return Response;
    }
    if ($('#ddl_Area_ed').val() == '') {
        Response.Texto = 'Debe seleccionar un área';
        Response.Bit = true;
        return Response;
    }
    //if ($('#ddl_Rol_ed').val() == '') {
    //    Response.Texto = 'Debe seleccionar un rol';
    //    Response.Bit = true;
    //    return Response;
    //}
    if (ValidarDependenciaEditar()) {
        Response.Texto = 'La dependencia principal no debe estar seleccionada en las dependencias adicionales';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function ValidarDependenciaEditar() {
    let arreglo = ObtenerDependenciasSeleccionadasEditar();
    if (arreglo == null) {
        return false;
    }
    if (arreglo.length == 0) {
        return false;
    }
    let duplicado = arreglo.findIndex(x => x.tbl_dependencia_id == $('#DependenciaA_ed').val());
    if (duplicado != -1) {
        return true;
    } else {
        return false;
    }
}

function DeleteU(item) {
    function Confirmacion() {
        return DelUsuario(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de desactivar este usuario ¿Desea continuar?", "Si", "No", AccionSi, "");
}
function ActivU(item) {
    function Confirmacion() {
        return ActivUsuario(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA("Atención", "Usted está a punto de activar este usuario ¿Desea continuar?", "Si", "No", AccionSi, "");
}
function ActivUsuario(item) {
    var OBJ_Form = tbl_usuario_class;
    OBJ_Form.id_usuario = "";
    OBJ_Form.id_persona = item;
    OBJ_Form.email = "";
    OBJ_Form.ap_paterno = "";
    OBJ_Form.ap_materno = "";
    OBJ_Form.activo = true;
    OBJ_Form.nombre = "";
    OBJ_Form.rfc = "";
    OBJ_Form.telefono = "";
    OBJ_Form.extencion = "";
    OBJ_Form.fecha_inicio = "0001-01-01T00:00:00";
    OBJ_Form.fecha_fin = "0001-01-01T00:00:00";
    OBJ_Form.id_dependencia = "";
    OBJ_Form.id_area = "";
    OBJ_Form.id_rol = "";

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
                //if (data_b[0].code == 'success') {
                SuccessSA("Operación exitosa", 'Usuario activado');
                getUsuarios();
                //} else {
                //    ErrorSA("", data_b[0].msg);
                //}
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: (con + "Usuarios/Activ")
    })
}
function DelUsuario(item) {  
    var OBJ_Form = tbl_usuario_class;
    OBJ_Form.id_persona = item;
    OBJ_Form.email = "";
    OBJ_Form.ap_paterno = "";
    OBJ_Form.ap_materno = "";
    OBJ_Form.nombre = "";
    OBJ_Form.rfc = "";
    OBJ_Form.telefono = "";
    OBJ_Form.extencion = "";
    OBJ_Form.fecha_inicio = "0001-01-01T00:00:00";
    OBJ_Form.fecha_fin = "0001-01-01T00:00:00";
    OBJ_Form.id_dependencia = "";
    OBJ_Form.id_area = "";
    OBJ_Form.id_rol = "";

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'delete',

        success: function (data) {
            var data_b = $.parseJSON(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                //if (data_b[0].code == 'success') {
                    SuccessSA("Operación exitosa", 'Usuario desactivado');
                    getUsuarios();                    
                //} else {
                //    ErrorSA("", data_b[0].msg);
                //}
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: (con + "Usuarios/Delete")
    })    
}

var tbl_usuario_class = {
    id_usuario: null,
    id_persona: null,
    email: null,
    nombre: null,
    ap_paterno: null,
    ap_materno: null,
    activo: null,
    rfc: null,
    telefono: null,
    extencion: null,
    fecha_inicio: null,
    fecha_fin: null,
    id_dependencia: null,
    id_area: null,
    id_rol: null,
    super_usuario: 0,
    usuario: '',
    dependencias_adicionales: null
}

var obj_usuario_rol = {
    idRolUsuario: null
}


var obj_rol = {
    idUsuario: null,
    idRol: null
}
var dependencias_ad = {
    tbl_dependencia_id: null
}
