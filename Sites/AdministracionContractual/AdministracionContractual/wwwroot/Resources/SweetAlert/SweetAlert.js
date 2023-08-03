
function SuccessSA(Titulo, Mensaje) {
    if (Titulo == undefined && Mensaje == undefined) {
        Titulo = ""; Mensaje = "";
    }
    Swal.fire({
        allowOutsideClick: true,
        type: 'success',
        title: Titulo != '' ? Titulo : 'Éxito.!',
        text: Mensaje != '' ? Mensaje : 'Operación exitosa.'
    })
}


function ErrorSA(Titulo, Mensaje) {
    if (Titulo == undefined && Mensaje == undefined) {
        Titulo = ""; Mensaje = "";
    }
    Swal.fire({
        allowOutsideClick: false,
        type: 'error',
        title: Titulo != '' ? Titulo : 'Error.!',
        text: Mensaje != '' ? Mensaje : 'Ocurrió un error inesperado.'
    })
}

function Aviso_ErrorSA(Titulo, Mensaje) {
    if (Titulo == undefined && Mensaje == undefined) {
        Titulo = ""; Mensaje = "";
    }
    Swal.fire({
        allowOutsideClick: false,
        type: 'warning',
        title: Titulo != '' ? Titulo : 'Aviso.!',
        text: Mensaje != '' ? Mensaje : 'Ocurrió un error inesperado.'
    })
}

function WarningSA(TituloSA, MensajeSA, TextBtnConfirmacion, TextBtnCancelacion, AccionSI, AccionNo) {
    if (TituloSA == undefined && MensajeSA == undefined && TextBtnConfirmacion == undefined
        && TextBtnCancelacion == undefined && AccionSI == undefined && AccionNo == undefined) {
        TituloSA = ""; MensajeSA = ""; TextBtnConfirmacion = ""; TextBtnCancelacion = ""; AccionSI = ""; AccionNo = "";
    }
    Swal.fire({
        allowOutsideClick: false,
        type: 'warning',
        title: TituloSA != '' ? TituloSA : 'Atención.!',
        text: MensajeSA != '' ? MensajeSA : 'Usted está a punto de eliminar este registro permanentemente ¿Desea continuar?',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',

        confirmButtonText: TextBtnConfirmacion != '' ? TextBtnConfirmacion : 'Continuar',
        cancelButtonText: TextBtnCancelacion != '' ? TextBtnCancelacion : 'Cancelar',
    }).then((result) => {
        if (result.value) {
            if (AccionSI == "") {
                ErrorSA('Error', 'Ningún método definido')
            }
            else {
                AccionSI();
            }
        }
        else {
            if (AccionNo == "") {
                SuccessSA('Cancelado', 'Proceso cancelado')
            }
            else {
                AccionNo();
            }
        }
    })
}

function QuestionSA(TituloSA, MensajeSA, TextBtnConfirmacion, TextBtnCancelacion, AccionSI, AccionNo) {
    if (TituloSA == undefined && MensajeSA == undefined && TextBtnConfirmacion == undefined
        && TextBtnCancelacion == undefined && AccionSI == undefined && AccionNo == undefined) {
        TituloSA = ""; MensajeSA = ""; TextBtnConfirmacion = ""; TextBtnCancelacion = ""; AccionSI = ""; AccionNo = "";
    }
    Swal.fire({
        allowOutsideClick: false,
        type: 'question',
        title: TituloSA != '' ? TituloSA : '¿Desea Continuar?',
        text: MensajeSA != '' ? MensajeSA : '',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: TextBtnConfirmacion != '' ? TextBtnConfirmacion : 'Continuar',
        cancelButtonText: TextBtnCancelacion != '' ? TextBtnCancelacion : 'Cancelar',
    }).then((result) => {
        if (result.value) {
            if (AccionSI == "") {
                ErrorSA('Error', 'Ningún método definido')
            }
            else {
                AccionSI();
            }
        }
        else {
            if (AccionNo == "") {
                SuccessSA('Cancelado', 'Proceso cancelado')
            }
            else {
                AccionNo();
            }

        }
    })
}




//********************************************************************************************************
function InfoSA(TituloSA, MensajeSA, TextBtnConfirmacion, TextBtnCancelacion, AccionSI, AccionNo) {
    if (TituloSA == undefined && MensajeSA == undefined && TextBtnConfirmacion == undefined
        && TextBtnCancelacion == undefined && AccionSI == undefined && AccionNo == undefined) {
        TituloSA = ""; MensajeSA = ""; TextBtnConfirmacion = ""; TextBtnCancelacion = ""; AccionSI = ""; AccionNo = "";
    }
    Swal.fire({
        allowOutsideClick: false,
        type: 'info',
        title: TituloSA != '' ? TituloSA : '¿Desea Continuar?',
        text: MensajeSA != '' ? MensajeSA : '',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: TextBtnConfirmacion != '' ? TextBtnConfirmacion : 'Continuar',
        cancelButtonText: TextBtnCancelacion != '' ? TextBtnCancelacion : 'Cancelar',
    }).then((result) => {
        if (result.value) {
            if (AccionSI == "") {
                ErrorSA('Error', 'Ningún método definido')
            }
            else {
                AccionSI();
            }
        }
        else {
            if (AccionNo == "") {
                SuccessSA('Cancelado', 'Proceso cancelado')
            }
            else {
                AccionNo();
            }

        }
    })
}



//////////////////////Se replicaron funciones para success y error y requieren una accion 

function SuccessSAAction(Titulo, Mensaje, AccionSI) {
    if (Titulo == undefined && Mensaje == undefined) {
        Titulo = ""; Mensaje = "";
    }
    Swal.fire({
        allowOutsideClick: false,
        type: 'success',
        title: Titulo != '' ? Titulo : 'Éxito.!',
        text: Mensaje != '' ? Mensaje : 'Operación exitosa'
    }).then((result) => {
        if (result.value) {
            AccionSI();
        }
    })
}

function ErrorSAAction(Titulo, Mensaje, AccionSI) {
    if (Titulo == undefined && Mensaje == undefined && AccionSI == undefined) {
        Titulo = ""; Mensaje = ""; AccionSI = "";
    }
    Swal.fire({
        allowOutsideClick: false,
        type: 'error',
        title: Titulo != '' ? Titulo : 'Error.!',
        text: Mensaje != '' ? Mensaje : 'Ocurrió un error inesperado'
    }).then((result) => {
        if (result.value) {
            AccionSI();
        }
    })
}

function Aviso_ErrorSAAction(Titulo, Mensaje, AccionSI) {
    if (Titulo == undefined && Mensaje == undefined && AccionSI == undefined) {
        Titulo = ""; Mensaje = ""; AccionSI = "";
    }
    Swal.fire({
        allowOutsideClick: false,
        type: 'warning',
        title: Titulo != '' ? Titulo : 'Aviso.!',
        text: Mensaje != '' ? Mensaje : 'Ocurrió un error inesperado.'
    }).then((result) => {
        if (result.value) {
            AccionSI();
        }
    })
}
