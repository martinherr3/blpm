
function alerta() {
    alert("llegó");
}

function ValidarCaracteres(textareaControl, maxlength) {

    if (textareaControl.value.length > maxlength) {
        textareaControl.value = textareaControl.value.substring(0, maxlength);
        //alert("Debe ingresar hasta un maximo de " + maxlength + " caracteres");
    }
}

function DoChanges(sel1,sel2)
    {
    msg = document.getElementById(sel1).getAttribute('value');
    document.getElementById(sel2).setAttribute('value', msg);
    }