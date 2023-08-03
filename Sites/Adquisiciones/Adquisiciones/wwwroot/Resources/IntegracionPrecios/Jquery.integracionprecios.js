$(document).ready(function () {
    $(".exist").each(function () {
        $(this).hide();
    });
    $('#tbl_validar_documentos, #tbl_Cotizacion, #tbl_dictamen').DataTable({
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "10%", "targets": 0 },
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "30%", "targets": 3 },
            { "width": "15%", "targets": 4 },
            { "width": "15%", "targets": 5 }
        ], "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        }
    });

});

$("#opciones_integracion").on("change", function () {
    var valor = $("#opciones_integracion").val();
    switch (valor) {
        case "1":
            $("#validardocumentos").each(function () {
                $(".exist").hide();
                $(this).show();
            });
            break;
        case "2":
            $("#cotizacion").each(function () {
                $(".exist").hide();
                $(this).show();
            });
            break;
        case "3":
            $("#dictamen").each(function () {
                $(".exist").hide();
                $(this).show();
            });
            break;
        default:
            $(".exist").each(function () {
                $(this).hide();
            });
    }

});