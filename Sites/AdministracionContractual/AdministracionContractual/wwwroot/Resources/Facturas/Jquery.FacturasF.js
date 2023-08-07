$(document).ready(function () {
    LaunchLoader(true);
    AjusteTablaF();
    getFacturasF();
});

$(".btn-danger").click(function () {    
    $('.Clean').val(''); 
});

function AjusteTablaF() {
    $('#DatosXmlRequest').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "5%", "targets": 0 },
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "20%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            { "width": "10%", "targets": 6 },
            { "width": "10%", "targets": 7 }
        ],
    });    
}

var con = $("#EndPointAdmon").val();
var ins = $('#HDidInstancia').val();

function getFacturasF() {
    $.get(con + "GeneracionXMLController/Listar", function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var ir_b = null;
            var Interno = [];

            Interno.push(i+1);
            Interno.push(data[i].tblContratoId);
            Interno.push(data[i].id);
            Interno.push(data[i].fecha);
            Interno.push(data[i].nombreReceptor);
            Interno.push(data[i].rfcReceptor);
            Interno.push(data[i].conceptos);


            Interno.push("<button class='btn btn-default' title='Detalle' onclick=\"muestraModaldetalle('" + data[i].id + "');\"><i class='fa fa-send'></i></button> <button class='btn btn-primary' title='Editar' onclick=\"muestraModalEditar('" + data[i].id + "');\"><i class='fa fa-edit'></i></button>");

            Arreglo_arreglos.push(Interno);
        }
        var table = $('#DatosXmlRequest').DataTable();
        table.destroy();
        $('#DatosXmlRequest').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Contrato Id" },
                { title: "Factura Id" },
                { title: "Fecha Emisión" },
                { title: "Nombre Receptor" },
                { title: "RFC Receptor" },
                { title: "Conceptos" },
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

function GetContrato(id) {
    $.get(con + "GeneracionXMLController/Listar/", function (data, status) {
        var body = "<option selected value=''>Seleccione...</option>";

        const unicos = [];

        for (var i = 0; i < data.length; i++) {

            const elemento = data[i].tblContratoId;

            if (!unicos.includes(data[i].tblContratoId)) {
                unicos.push(elemento);
            }
        }

        for (var i = 0; i <= unicos.length - 1; i++) {

            body = body + "<option value='" + unicos[i] + "'>" + unicos[i] + "</option>";
        }
        $('#ddl_Contrato').html(body);
        $('#ddl_Contrato_ed').html(body);
    });
    return;
}

function muestraModalAgregarFactura() {
    $('.Clean').val('');
    $('.Clean').prop("disabled", false);
    GetContrato();
    $('#ModalAgregarFactura').modal('show');
}

function muestraModalEditar(item) {
    $("#id_Factura_ed").val(item);
    getedit(item);
    $('#title').html('Editar factura');
    $('.Clean').prop("disabled", false);
    $('#EditarFactura').show(true);
    $('#ModalEditarFactura').modal('show');

}
function muestraModaldetalle(item) {
    geteditDetalle(item);
    $('#title').html('Detalle Factura');
    $('#ModalEditarFactura').modal('show');
    $('.Clean').prop("disabled", true);
    $('#SuperUser_ed').prop("disabled", true);
    $('#add_rol').prop("disabled", true);

    $('#EditarFactura').hide(true);

}

function getedit(item) {
    correo_editar = null;
    rfc_editar = null;
    var id_Factura;
    $.get(con + "Facturas/Get_Persona/" + item, function (data, status) {

        if (data !== null) {
            for (var i = 0; i <= data.length - 1; i++) {
                $('#txt_Nombre_ed').val(data[i].nombre);
                $('#txt_Apellido_Paterno_ed').val(data[i].ap_paterno);
                $('#txt_Apellido_Materno_ed').val(data[i].ap_materno);
                $('#txt_Correo_ed').val(data[i].email);
                $('#txt_RFC_ed').val(data[i].rfc);
                $('#Telefono_ed').val(data[i].telefono);
                $('#Extension_ed').val(data[i].extencion);
                $('#txt_Factura_ed').val(data[i].Factura);
                var fechainicio = (data[i].fecha_inicio).split('T');
                var fechafin = (data[i].fecha_fin).split('T');
                $('#txt_Fecha_Inicial_ed').val(fechainicio[0]);
                $('#txt_Fecha_Final_ed').val(fechafin[0]);
                GetSelectiveAreas(data[i].id_dependencia, data[i].id_area);
                GetDependenciasM(data[i].id_dependencia);
                GetRol(data[i].id_rol);
                if (data[i].super_Factura == true) {
                    $('#SuperUser_ed').prop("checked", true);
                } else if (data[i].super_Factura == false) {
                    $('#SuperUser_ed').prop("checked", false);
                }
                correo_editar = data[i].email;
                rfc_editar = data[i].rfc;
                id_Factura = data[i].id_Factura;
                $("#id_Factura_ed").val(data[i].id_Factura);

            }
            getRolesFactura(id_Factura);
            getDependenciasAsignadasFactura(id_Factura);
        }

    });
}

function geteditDetalle(item) {
    correo_editar = null;
    rfc_editar = null;
    var id_Factura;
    $.get(con + "Facturas/Get_Persona/" + item, function (data, status) {

        if (data !== null) {
            for (var i = 0; i <= data.length - 1; i++) {
                $('#txt_Nombre_ed').val(data[i].nombre);
                $('#txt_Apellido_Paterno_ed').val(data[i].ap_paterno);
                $('#txt_Apellido_Materno_ed').val(data[i].ap_materno);
                $('#txt_Correo_ed').val(data[i].email);
                $('#txt_RFC_ed').val(data[i].rfc);
                $('#Telefono_ed').val(data[i].telefono);
                $('#Extension_ed').val(data[i].extencion);
                $('#txt_Factura_ed').val(data[i].Factura);
                var fechainicio = (data[i].fecha_inicio).split('T');
                var fechafin = (data[i].fecha_fin).split('T');
                $('#txt_Fecha_Inicial_ed').val(fechainicio[0]);
                $('#txt_Fecha_Final_ed').val(fechafin[0]);
                GetSelectiveAreas(data[i].id_dependencia, data[i].id_area);
                GetDependenciasM(data[i].id_dependencia);
                GetRol(data[i].id_rol);
                if (data[i].super_Factura == true) {
                    $('#SuperUser_ed').prop("checked", true);
                } else if (data[i].super_Factura == false) {
                    $('#SuperUser_ed').prop("checked", false);
                }
                correo_editar = data[i].email;
                rfc_editar = data[i].rfc;
                id_Factura = data[i].id_Factura;
                $("#id_Factura_ed").val(data[i].id_Factura);

            }

            getRolesFacturaDetalle(id_Factura);

        }

    });
}


$("#GuardarFactura").click(function () {
    AddFactura();
});
$("#EditarFactura").click(function () {
    EditFactura();
});

function AddFactura() {
    var Validacion = false;
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_Factura_class;
        OBJ_Form.tbl_contrato_id = $('#ddl_Contrato').val();;
        OBJ_Form.id = "string";
        OBJ_Form.folio = "string";
        OBJ_Form.serie = "string";
        OBJ_Form.fecha = "string";
        OBJ_Form.formaDePago = "string";
        OBJ_Form.condicionesDePago = "string";
        OBJ_Form.total = 0;
        OBJ_Form.moneda = "string";
        OBJ_Form.subTotal = 0;
        OBJ_Form.metodoDePago = "string";
        OBJ_Form.tipoDeComprobante = "string";
        OBJ_Form.lugarExpedicion = "string";
        OBJ_Form.rfcEmisor = "string";
        OBJ_Form.nombreEmisor = "string";
        OBJ_Form.calleEmisor = "string";
        OBJ_Form.noInteriorEmisor = "string";
        OBJ_Form.noExteriorEmisor = "string";
        OBJ_Form.coloniaEmisor = "string";
        OBJ_Form.municipioEmisor = "string";
        OBJ_Form.estadoEmisor = "string";
        OBJ_Form.paisEmisor = "string";
        OBJ_Form.codigoPostalEmisor = "string";
        OBJ_Form.regimenEmisor = "string";
        OBJ_Form.rfcReceptor = "string";
        OBJ_Form.nombreReceptor = "string";
        OBJ_Form.calleReceptor = "string";
        OBJ_Form.noInteriorReceptor = "string";
        OBJ_Form.noExteriorReceptor = "string";
        OBJ_Form.coloniaReceptor = "string";
        OBJ_Form.municipioReceptor = "string";
        OBJ_Form.estadoReceptor = "string";
        OBJ_Form.paisReceptor = "string";
        OBJ_Form.codigoPostalReceptor = "string";
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
                    getFacturas();
                    $('#ModalAgregarFactura').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "GeneracionXMLController/Generar")

        })
    }
}


var tbl_Factura_class = {
    tbl_contrato_id: null,
    id: null,
    folio: null,
    serie: null,
    fecha: null,
    formaDePago: null,
    condicionesDePago: null,
    total: 0,
    moneda: null,
    subTotal: 0,
    metodoDePago: null,
    tipoDeComprobante: null,
    lugarExpedicion: null,
    rfcEmisor: null,
    nombreEmisor: null,
    calleEmisor: null,
    noInteriorEmisor: null,
    noExteriorEmisor: null,
    coloniaEmisor: null,
    municipioEmisor: null,
    estadoEmisor: null,
    paisEmisor: null,
    codigoPostalEmisor: null,
    regimenEmisor: null,
    rfcReceptor: null,
    nombreReceptor: null,
    calleReceptor: null,
    noInteriorReceptor: null,
    noExteriorReceptor: null,
    coloniaReceptor: null,
    municipioReceptor: null,
    estadoReceptor: null,
    paisReceptor: null,
    codigoPostalReceptor: null
}
