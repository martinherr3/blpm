
function alerta() {
    alert("llegó");
}

function ValidarCaracteres(textareaControl, maxlength) {

    if (textareaControl.value.length > maxlength) {
        textareaControl.value = textareaControl.value.substring(0, maxlength);
        //alert("Debe ingresar hasta un maximo de " + maxlength + " caracteres");
    }
}

function DoChanges(sel1, sel2) {
    msg = document.getElementById(sel1).getAttribute('value');
    document.getElementById(sel2).setAttribute('value', msg);
}

function onChangeCicloLectivo(ddlCicloLectivo, ddlCurso) {
    var control = document.getElementById(ddlCicloLectivo)
    if (control.value <= 0) {
        SetSelectedIndex(ddlCurso, -1);
        document.getElementById(ddlCurso).disabled = true;
    }
    else
        document.getElementById(ddlCurso).disabled = false;
}

function SetSelectedIndex(dropdownlist, sVal) {
    var a = document.getElementById(dropdownlist);

    for (i = 0; i < a.length; i++) {
        if (a.options[i].value == sVal) {
            a.selectedIndex = i;
        }
    }
}