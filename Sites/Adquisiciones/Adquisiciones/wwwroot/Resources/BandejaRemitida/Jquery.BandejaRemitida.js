$(function () {
    $(".eachTbl").each(function (indice, elemento) {
        $(elemento).DataTable();
    });
});