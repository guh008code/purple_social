
/* Máscaras utilizadas no projeto */
function isAlphanumeric(str) {
    var validate = document.getElementById(str).value;
    if (/[^a-zA-Z0-9]/.test(validate)) {
        alert(validate + ' Este carácter é inválido.');
        return false;
    }
    return true;
}

function ismaxlength(obj) {
    var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : ""
    if (obj.getAttribute && obj.value.length > mlength) {
        obj.value = obj.value.substring(0, mlength)
    }
}

/*Função Pai de Mascaras*/
function Mascara(o, f) {
    v_obj = o
    v_fun = f
    setTimeout("execmascara()", 1)
}

/*Função que Executa os objetos*/
function execmascara() {
    v_obj.value = v_fun(v_obj.value)
}

/*Função que Determina as expressões regulares dos objetos*/
function leech(v) {
    v = v.replace(/o/gi, "0")
    v = v.replace(/i/gi, "1")
    v = v.replace(/z/gi, "2")
    v = v.replace(/e/gi, "3")
    v = v.replace(/a/gi, "4")
    v = v.replace(/s/gi, "5")
    v = v.replace(/t/gi, "7")
    return v
}

/*Função que padroniza telefone (11) 4184-1241*/
function Telefone(v) {
    v = v.replace(/\D/g, "")
    v = v.replace(/^(\d\d)(\d)/g, "($1)$2")
    v = v.replace(/(\d{4})(\d)/, "$1-$2")
    return v
}

/*Função que padroniza telefone (11) 4184-1241*/
function SoNumero(v) {
    v = v.replace(/\D/g, "")
    v = v.replace(/(\d{4})(\d)/, "$1$2")
    return v
}

function TelefoneSemDDD(v) {
    v = v.replace(/\D/g, "")
    v = v.replace(/(\d{4})(\d)/, "$1-$2")
    return v
}


function Cep(v) {
    v = v.replace(/\D/g, "")
    v = v.replace(/(\d{5})(\d)/, "$1-$2")
    return v
}

function Horas(v) {
    v = v.replace(/\D/g, "")
    v = v.replace(/(\d{4})(\d)/, "$1:$2")
    return v
}

function Cpf(v) {
    v = v.replace(/\D/g, "")
    v = v.replace(/(\d{3})(\d)/, "$1.$2")
    v = v.replace(/(\d{3})(\d)/, "$1.$2")
    v = v.replace(/(\d{3})(\d)/, "$1-$2")
    return v
}


function Cnpj(v) {
    v = v.replace(/\D/g, "")
    v = v.replace(/(\d{2})(\d)/, "$1.$2")
    v = v.replace(/(\d{3})(\d)/, "$1.$2")
    v = v.replace(/(\d{3})(\d)/, "$1/$2")
    v = v.replace(/(\d{4})(\d)/, "$1-$2")
    return v
}


function Cnpj_cpf(v) {
    if (v.toString().length < 15) {
        v = v.replace(/\D/g, "")
        v = v.replace(/(\d{3})(\d)/, "$1.$2")
        v = v.replace(/(\d{3})(\d)/, "$1.$2")
        v = v.replace(/(\d{3})(\d)/, "$1-$2")
    }
    else if (v.toString().length > 15) {
        v = v.replace(/\D/g, "")
        v = v.replace(/(\d{2})(\d)/, "$1.$2")
        v = v.replace(/(\d{3})(\d)/, "$1.$2")
        v = v.replace(/(\d{3})(\d)/, "$1/$2")
        v = v.replace(/(\d{4})(\d)/, "$1-$2")
    }
    return v
}

function f_filtra_teclas_numero() {
    er = /\d/;
    if (!er.test(String.fromCharCode(window.event.keyCode)) || (window.event.keyCode == 13)) {
        window.event.keyCode = 0;
    }
}

function f_filtra_teclas_moeda() {
    er = /[0-9,]/;
    if (!er.test(String.fromCharCode(window.event.keyCode)) || (window.event.keyCode == 13)) {
        window.event.keyCode = 0;
    }
}

function f_filtra_teclas_hora() {
    er = /[0-9:]/;
    if (!er.test(String.fromCharCode(window.event.keyCode)) || (window.event.keyCode == 13)) {
        window.event.keyCode = 0;
    }
}

function f_filtra_teclas_data() {
    er = /[0-9\/]/;
    if (!er.test(String.fromCharCode(window.event.keyCode)) || (window.event.keyCode == 13)) {
        window.event.keyCode = 0;
    }
}

function f_filtra_teclas_moedanegativo() {
    er = /[0-9,-]/;
    if (!er.test(String.fromCharCode(window.event.keyCode)) || (window.event.keyCode == 13)) {
        window.event.keyCode = 0;
    }
}

function f_formata_valor(vValor) {
    if (vValor == '') {
        return '';
    }

    reg = /,/g;
    if (!reg.test(vValor)) {
        vValor = vValor + ',00';
    }
    else {
        aValor = vValor.split(',');
        if (aValor[0].length > 0) {
            if (aValor[1].length == 0) {
                vValor = vValor + '00';
            }
            else {
                if (aValor[1].length == 1) {
                    vValor = vValor + '0';
                }
            }
        }
        else {
            vValor = '';
        }
    }
    return vValor;
}

function MascaraMoeda(objTextBox, SeparadorMilesimo, SeparadorDecimal, e) {
    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;
    if (whichCode == 13) return true;
    key = String.fromCharCode(whichCode); // Valor para o código da Chave
    if (strCheck.indexOf(key) == -1) return false; // Chave inválida
    len = objTextBox.value.length;
    for (i = 0; i < len; i++)
        if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
    aux = '';
    for (; i < len; i++)
        if (strCheck.indexOf(objTextBox.value.charAt(i)) != -1) aux += objTextBox.value.charAt(i);
    aux += key;
    len = aux.length;
    if (len == 0) objTextBox.value = '';
    if (len == 1) objTextBox.value = '0' + SeparadorDecimal + '0' + aux;
    if (len == 2) objTextBox.value = '0' + SeparadorDecimal + aux;
    if (len > 2) {
        aux2 = '';
        for (j = 0, i = len - 3; i >= 0; i--) {
            if (j == 3) {
                aux2 += SeparadorMilesimo;
                j = 0;
            }
            aux2 += aux.charAt(i);
            j++;
        }
        objTextBox.value = '';
        len2 = aux2.length;
        for (i = len2 - 1; i >= 0; i--)
            objTextBox.value += aux2.charAt(i);
        objTextBox.value += SeparadorDecimal + aux.substr(len - 2, len);
    }
    return false;
}

function Formatadata(Campo, teclapres) {
    var tecla = teclapres.keyCode;
    var vr = new String(Campo.value);
    vr = vr.replace("/", "");
    vr = vr.replace("/", "");
    vr = vr.replace("/", "");
    tam = vr.length + 1;
    if (tecla != 8 && tecla != 8) {
        if (tam > 0 && tam < 2)
            Campo.value = vr.substr(0, 2);
        if (tam > 2 && tam < 4)
            Campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 2);
        if (tam > 4 && tam < 7)
            Campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 2) + '/' + vr.substr(4, 7);
    }
}

function FormatadataMesAno(Campo, teclapres) {
    var tecla = teclapres.keyCode;
    var vr = new String(Campo.value);
    vr = vr.replace("/", "");
    vr = vr.replace("/", "");
    vr = vr.replace("/", "");
    tam = vr.length + 1;
    if (tecla != 8 && tecla != 8) {
        if (tam > 0 && tam < 2)
            Campo.value = vr.substr(0, 2);
        if (tam > 2 && tam < 4)
            Campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 5);
        if (tam > 4 && tam < 8)
            Campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 5);
    }
}

function FormataHora(Campo, teclapres) {
    var tecla = teclapres.keyCode;
    var vr = new String(Campo.value);
    vr = vr.replace("/", "");
    vr = vr.replace(":", "");
    tam = vr.length + 1;
    if (tecla != 8 && tecla != 8) {
        if (tam > 0 && tam < 2)
            Campo.value = vr.substr(0, 2);
        if (tam > 2 && tam < 4)
            Campo.value = vr.substr(0, 2) + ':' + vr.substr(2, 2);
    }
}

function f_formata_data(vData) {
    sData = vData

    reg = /\//;
    if (!reg.test(vData)) {
        sData = sData.substr(0, 2) + '/' + sData.substr(2, 2) + '/' + sData.substr(4, 4);
    }

    aData = sData.split('/');

    if (aData.length != 3) {
        return vData;
    }

    Dia = aData[0];
    Mes = aData[1];
    Ano = aData[2];

    if (Ano == '' || Ano.length == 1) {
        return vData;
    }

    if (Dia.length != 2) Dia = '0' + Dia;
    if (Mes.length != 2) Mes = '0' + Mes;
    if (Ano.length != 4) {
        if (Ano < 50) {
            Ano = '20' + Ano;
        }
        else {
            Ano = '19' + Ano;
        }
    }
    return Dia + '/' + Mes + '/' + Ano;
}

function f_valida_data(vObj) {
    if (vObj.value.length == '') {
        return true;
    }

    vObj.value = f_formata_data(vObj.value);

    conteudo = vObj.value;

    if (conteudo.length != 10) {
        alert('Data Invalida. Utilize o formato DD/MM/YYYY');
        vObj.focus();
        return false;
    }

    array_data = conteudo.split('/');

    if ((array_data[0] == undefined) || (array_data[0] == '')) {
        alert('Data Invalida');
        vObj.focus();
        return false;

    }
    if ((array_data[1] == undefined) || (array_data[1] == '')) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    if ((array_data[2] == undefined) || (array_data[2] == '')) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }

    if (array_data == null) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    dia = array_data[0];
    mes = array_data[1];
    ano = array_data[2];
    if ((mes < 1) || (mes > 12)) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    if ((dia < 1) || (dia > 31)) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    if (((mes == 4) || (mes == 6) || (mes == 9) || (mes == 11)) && (dia == 31)) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    if (mes == 2) {
        var bissexto = (((ano % 4) == 0) && (((ano % 100) != 0) || ((ano % 400) == 0)));
        if ((dia > 29) || ((dia == 29) && !bissexto)) {
            alert('Data Invalida');
            vObj.focus();
            return false;
        }
    }
    return true;
}

function f_formata_data_pequena(vData) {
    sData = vData

    reg = /\//;
    if (!reg.test(vData)) {
        sData = sData.substr(0, 2) + '/' + sData.substr(2, 4);
    }

    aData = sData.split('/');

    Mes = aData[0];
    Ano = aData[1];

    if (Ano == '' || Ano.length == 1) {
        return vData;
    }

    if (Mes.length != 2) Mes = '0' + Mes;
    if (Ano.length != 4) {
        if (Ano < 50) {
            Ano = '20' + Ano;
        }
        else {
            Ano = '19' + Ano;
        }
    }
    return Mes + '/' + Ano;
}

function f_valida_data_pequena(vObj) {
    if (vObj.value.length == '') {
        return true;
    }

    vObj.value = f_formata_data_pequena(vObj.value);

    conteudo = vObj.value;

    if (conteudo.length != 7) {
        alert('Data Invalida. Utilize o formato MM/YYYY ou MMYYYY');
        vObj.focus();
        return false;
    }

    array_data = conteudo.split('/');

    if ((array_data[0] == undefined) || (array_data[0] == '')) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    if ((array_data[1] == undefined) || (array_data[1] == '')) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }

    if (array_data == null) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    mes = array_data[0];
    ano = array_data[1];

    if ((mes < 1) || (mes > 12)) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    if (ano < 1800) {
        alert('Data Invalida');
        vObj.focus();
        return false;
    }
    return true;
}

function f_formata_hora(vHora) {

    reg = /:/;
    if (!reg.test(vHora)) {
        vHora = vHora.substr(0, 2) + ':' + vHora.substr(2, 2);
    }

    aHora = vHora.split(':');

    if (aHora.length != 2) {
        return vHora;
    }

    Hora = aHora[0]
    Minuto = aHora[1]

    if (Hora.length != 2) Hora = '0' + Hora
    if (Minuto.length != 2) Minuto = '0' + Minuto
    return Hora + ':' + Minuto
}

function f_valida_hora(vObj) {
    if (vObj.value.length == '') {
        return true;
    }

    vObj.value = f_formata_hora(vObj.value);

    conteudo = vObj.value;

    if (conteudo.length != 5) {
        alert('Hora Invalida. Utilize o formato HH:MM');
        vObj.focus();
        return false;
    }

    array_hora = conteudo.split(':');

    if ((array_hora[0] == undefined) || (array_hora[0] == '')) {
        alert('Hora Invalida');
        vObj.focus();
        return false;

    }
    if ((array_hora[1] == undefined) || (array_hora[1] == '')) {
        alert('Hora Invalida');
        vObj.focus();
        return false;
    }

    if (array_hora == null) {
        alert('Hora Invalida');
        vObj.focus();
        return false;
    }

    hora = array_hora[0];
    minuto = array_hora[1];

    if ((hora < 0) || (hora > 23)) {
        alert('Hora Invalida');
        vObj.focus();
        return false;
    }

    if ((minuto < 0) || (minuto > 59)) {
        alert('Hora Invalida');
        vObj.focus();
        return false;
    }

    return true;
}

function f_formata_telefone(vObj) {
    if (vObj.value.length == 0) {
        return true;
    }

    sTel = vObj.value;
    sTel = f_retira_formatacao(sTel);

    vObj.value = '(' + sTel.substr(0, 2) + ') ' + sTel.substr(0, 4) + '-' + sTel.substr(4, 4);
    if (vObj.value.length > 10) {
        vObj.value += '' + sTel.substr(10, sTel.length);
    }
}

function f_formata_telefone_semDDD(vObj) {
    if (vObj.value.length == 0) {
        return true;
    }

    sTel = vObj.value;
    sTel = f_retira_formatacao(sTel);

    vObj.value = sTel.substr(0, 4) + '-' + sTel.substr(4, 4);
    if (vObj.value.length > 10) {
        vObj.value += '' + sTel.substr(10, sTel.length);
    }
}

function f_formata_celular(vObj) {
    if (vObj.value.length == 0) {
        return true;
    }

    sTel = vObj.value;
    sTel = f_retira_formatacao(sTel);

    if (vObj.value.length > 8) {
        vObj.value = sTel.substr(0, 5) + '-' + sTel.substr(5, 4);
    }
    else {
        vObj.value = sTel.substr(0, 4) + '-' + sTel.substr(4, 4);
    }

    if (vObj.value.length > 10) {
        vObj.value += '' + sTel.substr(10, sTel.length);
    }
}

function f_formata_cep1(vObj) {
    if (vObj.value.length == 0) {
        return true;
    }

    sCep = vObj.value;
    sCep = f_retira_formatacao(sCep);

    vObj.value = sCep.substr(0, 5) + '-' + sCep.substr(5, 3);
}

function f_retira_formatacao(s_txt) {

    er = /\d/;
    sDado = '';

    for (i = 0; i <= s_txt.length - 1; i++) {
        if (er.test(s_txt.substr(i, 1))) {
            sDado += s_txt.substr(i, 1)
        }
    }

    return sDado;
}

function visivel(vObj) {
    if (document.all(vObj).style.visibility == 'hidden') {
        document.all(vObj).style.visibility = 'visible'
        document.all(vObj).style.position = 'static'
    }
    else {
        document.all(vObj).style.visibility = 'hidden'
        document.all(vObj).style.position = 'absolute'
    }
}

function MudaCor(vObj) {
    document.all(vObj).className = 'selecionado'
}

function VoltaCor(vObj, Classe) {
    document.all(vObj).className = Classe
}

function f_formata_cpf(s_cpf) {
    if (s_cpf.length > 0) {
        s_cpf = f_retira_formatacao(s_cpf);
        s_cpf = s_cpf.substr(0, 3) + '.' + s_cpf.substr(3, 3) + '.' + s_cpf.substr(6, 3) + '-' + s_cpf.substr(9, 2);
    }

    return s_cpf;
}

function f_valida_cpf(s_cpf) {

    if (s_cpf.length == 0) {
        return true;
    }

    s_cpf = f_retira_formatacao(s_cpf);

    if (s_cpf.length == 11) {
        if ((s_cpf == 11111111111) ||
			    (s_cpf == 22222222222) ||
			    (s_cpf == 33333333333) ||
			    (s_cpf == 44444444444) ||
			    (s_cpf == 55555555555) ||
			    (s_cpf == 66666666666) ||
			    (s_cpf == 77777777777) ||
			    (s_cpf == 88888888888) ||
			    (s_cpf == 99999999999)) {
            return false;
        }

        valor = s_cpf;

        dig1 = 0;
        dig2 = 0;
        mult1 = 10;
        mult2 = 11;

        //verifica o primeiro número que faz parte do dígito
        for (i = 0; i <= 8; i++) {
            dig1 = dig1 + valor.substr(i, 1) * mult1;
            mult1 = mult1 - 1;
        }

        //verifica o segundo número que faz parte do dígito
        for (j = 0; j <= 9; j++) {
            dig2 = dig2 + valor.substr(j, 1) * mult2;
            mult2 = mult2 - 1;
        }

        //calcula o digito
        dig1 = (dig1 * 10) % 11;
        dig2 = (dig2 * 10) % 11;


        if (dig1 == 10) dig1 = 0;
        if (dig2 == 10) dig2 = 0;

        if (valor.substr(9, 1) != dig1.toString()) return false;
        if (valor.substr(10, 1) != dig2.toString()) return false;
    }
    else {
        return false;
    }

    return true;
}

function f_formata_cnpj(s_cnpj) {
    if (s_cnpj.length > 0) {
        s_cnpj = f_retira_formatacao(s_cnpj);
        s_cnpj = s_cnpj.substr(0, 2) + '.' + s_cnpj.substr(2, 3) + '.' + s_cnpj.substr(5, 3) + '/' + s_cnpj.substr(8, 4) + '-' + s_cnpj.substr(12, 2);
    }

    return s_cnpj;
}

function f_formata_cep(s_cep) {
    if (s_cep.length > 0) {
        s_cep = f_retira_formatacao(s_cep);
        s_cep = s_cep.substr(0, 5) + '-' + s_cnpj.substr(5, 3);
    }

    return s_cep;
}

function f_valida_cnpj(s_cnpj) {
    if (s_cnpj.length == 0) {
        return true;
    }

    s_cnpj = f_retira_formatacao(s_cnpj);

    if (s_cnpj.length == 14) {
        valor = s_cnpj;

        dig1 = 0;
        dig2 = 0;
        mult1 = '543298765432'
        mult2 = '6543298765432'

        //verifica o primeiro número que faz parte do dígito
        for (i = 0; i <= 11; i++) {
            dig1 = dig1 + valor.substr(i, 1) * mult1.substr(i, 1);
        }

        //verifica o segundo número que faz parte do dígito
        for (j = 0; j <= 12; j++) {
            dig2 = dig2 + valor.substr(j, 1) * mult2.substr(j, 1);
        }

        //calcula o digito
        dig1 = (dig1 * 10) % 11;
        dig2 = (dig2 * 10) % 11;


        if (dig1 == 10) dig1 = 0;
        if (dig2 == 10) dig2 = 0;

        if (valor.substr(12, 1) != dig1.toString()) return false;
        if (valor.substr(13, 1) != dig2.toString()) return false;
    }
    else {
        return false;
    }

    return true;

}

function f_consiste_cpf_cnpj(v_cpf_cnpj) {
    s_cpf_cnpj = v_cpf_cnpj.value;
    exp = /[\.-]/g
    cpfCNPJ = s_cpf_cnpj.toString().replace(exp, "");
    v_cpf_cnpj.value = cpfCNPJ;


    if (cpfCNPJ.length <= 11) //Fisica
    {
        if (f_valida_cpf(v_cpf_cnpj.value) == false) {
            alert('CPF Invalido.');
            v_cpf_cnpj.focus();
            v_cpf_cnpj.value = '';
        }
        else {
            v_cpf_cnpj.value = f_formata_cpf(v_cpf_cnpj.value);
        }
    }
    else //Juridica
    {
        if (f_valida_cnpj(v_cpf_cnpj.value) == false) {
            alert('CNPJ Invalido.');
            v_cpf_cnpj.focus();
            v_cpf_cnpj.value = '';
        }
        else {
            v_cpf_cnpj.value = f_formata_cnpj(v_cpf_cnpj.value);
        }
    }
}



function f_consiste_cpf_cnpj(v_cpf_cnpj) {
    s_cpf_cnpj = v_cpf_cnpj.value;
    exp = /[\.-]/g
    cpfCNPJ = s_cpf_cnpj.toString().replace(exp, "");
    v_cpf_cnpj.value = cpfCNPJ;


    if (cpfCNPJ.length <= 11) //Fisica
    {
        if (f_valida_cpf(v_cpf_cnpj.value) == false) {
            alert('CPF Invalido.');
            v_cpf_cnpj.focus();
            v_cpf_cnpj.value = '';
        }
        else {
            v_cpf_cnpj.value = f_formata_cpf(v_cpf_cnpj.value);
        }
    }
    else //Juridica
    {
        if (f_valida_cnpj(v_cpf_cnpj.value) == false) {
            alert('CNPJ Invalido.');
            v_cpf_cnpj.focus();
            v_cpf_cnpj.value = '';
        }
        else {
            v_cpf_cnpj.value = f_formata_cnpj(v_cpf_cnpj.value);
        }
    }
}

function f_formata_rg(s_rg) {
    if (s_rg.length > 0) {
        s_rg = f_retira_formatacao(s_rg);

        s_rg_dig = '';
        // Acima de nove digitos tem digito verificador
        if (s_rg.length >= 9) {
            s_rg_dig = '-' + s_rg.substr(s_rg.length - 1, 1);
            s_rg = s_rg.substr(0, s_rg.length - 1);  //Retira o digito para gerar os '.'
        }

        fim = s_rg.length;
        for (i = fim; i > 3; i -= 3) {
            s_rg = s_rg.substr(0, i - 3) + '.' + s_rg.substr(i - 3, fim);
        }
        s_rg = s_rg + s_rg_dig;
    }

    return s_rg
}

function f_valida_rg(s_rg) {

    if (s_rg.length == 0) {
        return true;
    }

    s_rg = f_retira_formatacao(s_rg);

    //Menos que nove digitos não temos digito verificador
    if (s_rg.length < 9) {
        return true;
    }

    var s_rg = s_rg.split("");
    tamanho = s_rg.length;
    vetor = new Array(tamanho);

    if (tamanho >= 1) {
        vetor[0] = parseInt(s_rg[0]) * 2;
    }
    if (tamanho >= 2) {
        vetor[1] = parseInt(s_rg[1]) * 3;
    }
    if (tamanho >= 3) {
        vetor[2] = parseInt(s_rg[2]) * 4;
    }
    if (tamanho >= 4) {
        vetor[3] = parseInt(s_rg[3]) * 5;
    }
    if (tamanho >= 5) {
        vetor[4] = parseInt(s_rg[4]) * 6;
    }
    if (tamanho >= 6) {
        vetor[5] = parseInt(s_rg[5]) * 7;
    }
    if (tamanho >= 7) {
        vetor[6] = parseInt(s_rg[6]) * 8;
    }
    if (tamanho >= 8) {
        vetor[7] = parseInt(s_rg[7]) * 9;
    }
    if (tamanho >= 9) {
        vetor[8] = parseInt(s_rg[8]) * 100;
    }

    total = 0;

    if (tamanho >= 1) {
        total += vetor[0];
    }
    if (tamanho >= 2) {
        total += vetor[1];
    }
    if (tamanho >= 3) {
        total += vetor[2];
    }
    if (tamanho >= 4) {
        total += vetor[3];
    }
    if (tamanho >= 5) {
        total += vetor[4];
    }
    if (tamanho >= 6) {
        total += vetor[5];
    }
    if (tamanho >= 7) {
        total += vetor[6];
    }
    if (tamanho >= 8) {
        total += vetor[7];
    }
    if (tamanho >= 9) {
        total += vetor[8];
    }

    resto = total % 11;
    if (resto != 0) {
        return false;
    }
    else {
        return true;
    }
}

function f_consiste_rg(v_rg) {
    if (f_valida_rg(v_rg.value) == false) {
        alert('RG Invalido.');
        v_rg.focus()
    }
    else {
        v_rg.value = f_formata_rg(v_rg.value);
    }
}

function f_modulo_11(n_numero) {

    v_numero = n_numero.toString();

    v_tamanho = v_numero.length;
    v_soma = 0;
    v_fator = 9;

    //verifica o primeiro número que faz parte do dígito
    for (i = v_tamanho - 1; i >= 0; i--) {
        v_soma = v_soma + (v_numero.substr(i, 1) * v_fator);
        v_fator = v_fator - 1;
        if (v_fator == 1) {
            v_fator = 9;
        }
    }

    //calcula o digito
    v_digito = v_soma % 11;
    if (v_digito == 10) v_digito = 0;

    return v_digito;
}


function f_filtra_teclas_numero_serie(Num) {
    sNum = Num.value
    if (sNum.length >= 2) {
        er = /\d/;
        if (!er.test(String.fromCharCode(window.event.keyCode)) && (window.event.keyCode != 13)) {
            window.event.keyCode = 0;
        }
    }
}

function Encripta(Texto) {
    var A, C, Senha, sAsc
    var B
    new String(Senha)
    Senha = Texto
    A = ""
    for (i = 0; i < Senha.length; i++) {
        B = Math.round((255 * Math.random()) - 1)
        sAsc = (Senha.substring(i, i + 1)).charCodeAt(0)
        while (sAsc + B > 255) {
            B = B - 28
        }
        C = String.fromCharCode(B + 30)
        A = A + String.fromCharCode(sAsc + B) + C
    }
    return A
}

function Desencripta(Texto) {
    var A, C, Senha
    var B
    Senha = Texto
    A = ""
    for (i = 1; i < (Senha.length / 2) + 1; i++) {
        n = (i * 2) - 1
        B = (Senha.substring(n, n + 1)).charCodeAt(0) - 30
        C = (Senha.substring(n - 1, n))
        A = A + String.fromCharCode(C.charCodeAt(0) - B)
    }
    return A
}

function f_formata_valor(vObjeto) {
    vValor = vObjeto.value

    ValidChars = /[0-9,]/;

    if (vValor != '') {
        if (ValidChars.test(vValor)) {
            if (vValor.indexOf(',') == -1) {
                vObjeto.value = vValor + ',00';
                return true;
            }
            else {

                aVal = vValor.split(',');

                if (aVal.length == 2) {
                    if (aVal[0].length > 0) {
                        if (aVal[1].length == 0) {
                            vObjeto.value = vObjeto.value + '00';
                        }
                        else {
                            if (aVal[1].length == 1) {
                                vObjeto.value = vObjeto.value + '0';
                            }
                        }
                    }
                    else {
                        vObjeto.value = '';
                    }
                }
                else {
                    alert('Valor Invalido!');
                    vObjeto.focus();
                }
            }
        }
        else {
            alert('Valor Invalido!');
            vObjeto.focus();
        }
    }
}

function AbreTela(path, altura, largura) {
    feautures = "dialogHeight: " + altura + "px; dialogWidth: " + largura + "px; edge: Raised; center: Yes; help: No; resizable: Yes; status: Yes;"
    window.showModalDialog(path, window, feautures);

}

function AbreTelaRetorno(path, altura, largura) {
    var rc = new Array(0, 0)
    feautures = "dialogHeight: " + altura + "px; dialogWidth: " + largura + "px; edge: Raised; center: Yes; help: No; resizable: Yes; status: Yes;"
    rc = window.showModalDialog(path, window, feautures);
    return rc
}

function retornaSelecao(id, nome) {
    var rc = new Array(id, nome);
    window.returnValue = rc;
    window.close();
}

function Max(txarea, qtd) {

    total = qtd;
    tam = txarea.value.length;
    str = "";

    str = str + tam;
    //Restante.innerHTML = total - str;

    if (tam > total) {
        aux = txarea.value;
        txarea.value = aux.substring(0, total);
        //Restante.innerHTML = 0;
    }



}
