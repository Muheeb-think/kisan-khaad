// JavaScript Document
var engCodes = new Array(126, 96, 33, 49, 64, 50, 35, 51, 36, 52, 37, 53, 94, 54, 38, 55, 42, 56, 40, 57, 41, 48, 95, 45, 43, 61, 81, 113, 87, 119, 69, 101, 82, 114, 84, 116, 89, 121, 85, 117, 73, 105, 79, 111, 80, 112, 123, 91, 125, 93, 124, 92, 65, 97, 83, 115, 68, 100, 70, 102, 71, 103, 72, 104, 74, 106, 75, 107, 76, 108, 58, 59, 34, 39, 90, 122, 88, 120, 67, 99, 86, 118, 66, 98, 78, 110, 77, 109, 60, 44, 62, 46, 63, 47, 0188, 0189, 0197, 46, 38, 0170);
var hinCodes = new Array('ृ', '्', '!', '1', '/', '2', 'रू', '3', '+', '4', 'ः', '5', '\'', '6', '-', '7', '(', '8', ')', '9', 'द्ध', '0', 'ऋ', '़', '्', 'त्र', 'फ', 'ु', 'ॅ', 'ू', 'म्‍', 'म', 'त्‍', 'त', 'ज्‍', 'ज', 'ल्‍', 'ल', 'न्‍', 'न', 'प्‍', 'प', 'व्‍', 'व', 'च्‍', 'च', 'क्ष्‍', 'ख्‍', 'द्व', ',', 'द्य', '\\', '।', 'ं', 'ै', 'े', 'क्‍', 'क', 'थ्‍', 'ि', 'ळ', 'ह', 'भ्‍', 'ी', 'श्र', 'र', 'ज्ञ', 'ा', 'स्‍', 'स', 'रू', 'य', 'ष्‍', 'श्‍', 'र्', '्र', 'ग्‍', 'ग', 'ब्‍', 'ब', 'ट', 'अ', 'ठ', 'इ', 'छ', 'द', 'ड', 'उ', 'ढ', 'ए', 'झ', 'ण्‍', 'घ्‍', 'ध्‍', '(', ')', 'ऊ', 'ण्‍', '-', '');

/*new Array('्','ृ','!','१','/','२','रू','३','?','४','-','५','','६','','७','द्ध','८','त्र','९','ऋ','०','','़','्','ृ','फ','ु','ॅ','ू','म्‍','म','त्‍','त','ज्‍','ज','ल्‍','ल','न्‍','न','प्‍','प','व्‍','व','च्‍','च','क्ष्‍','ख्‍','द्व',',','द्य','.','ा','ं','ै','े','क्‍','क','थ्‍','ि','ळ','ह','भ्‍','ी','श्र','र','ज्ञ','ा','स्‍','स','रू','य','ष्‍','श्‍','र्','्र','ग्‍','ग','ब्‍','ब','ट','अ','ठ','इ','छ','द','ड','उ','ढ','ए','झ','ण्‍','घ्‍','ध्‍');*/
function iz(name) {
    if (navigator.appName.indexOf(name) == 0)
        return true;
    else
        return false;
}
//inserted by Binod
function storeCaret(textEl) { textEl.caretPos = document.selection.createRange().duplicate(); }
function insertAtCaret(textEl, text, keyCode) {
    storeCaret(textEl);
    if (textEl.createTextRange && textEl.caretPos) {
        textEl.caretPos.text = text;
    }
    else {
        //alert(textEl.caretPos + ' ' + text); 
    }
}

function insertCode(txtBox, insertKey, keyCode) {
    if (document.selection) {
        insertAtCaret(txtBox, insertKey, keyCode);
    }
    else if (txtBox.selectionStart || txtBox.selectionStart == '0') {
        var str = txtBox.value;
        var a = txtBox.selectionStart, b = txtBox.selectionEnd;
        txtBox.value = str.substring(0, a) + insertKey + str.substring(b, str.length);
        //alert(str.substring(0, a)+ "=" + insertKey +"="+ str.substring(b, str.length));
        var s1 = str.substring(0, a);
        var pos = s1.length + 1;
        if (keyCode == '85' || keyCode == '69' || keyCode == '82' || keyCode == '84' || keyCode == '89' || keyCode == '73' || keyCode == '79' || keyCode == '80' || keyCode == '125' || keyCode == '91' || keyCode == '39' || keyCode == '34' || keyCode == '58' || keyCode == '76' || keyCode == '75' || keyCode == '71' || keyCode == '72' || keyCode == '74' || keyCode == '70' || keyCode == '68' || keyCode == '63' || keyCode == '67' || keyCode == '88' || keyCode == '122' || keyCode == '46' || keyCode == '47') {
            pos = pos + 2;
        }
        if (keyCode == '123') {
            pos = pos + 4;
        }

        ///////////////////
        setCaretPosition(txtBox, pos);
        return;
    }
};

function doGetCaretPosition(ctrl) {
    var CaretPos = 0;    // IE Support
    if (document.selection) {
        ctrl.focus();
        var Sel = document.selection.createRange();
        Sel.moveStart('character', -ctrl.value.length);
        CaretPos = Sel.text.length;

    }
    // Firefox support
    else if (ctrl.selectionStart || ctrl.selectionStart == '0')
        CaretPos = ctrl.selectionStart;

    //alert('1111  ' + CaretPos);
    return (CaretPos);
}
function setCaretPosition(ctrl, pos) {
    if (ctrl.setSelectionRange) {
        ctrl.focus();
        ctrl.setSelectionRange(pos, pos);

    }
    else if (ctrl.createTextRange) {
        var range = ctrl.createTextRange();
        range.collapse(true);
        range.moveEnd('character', pos);
        range.moveStart('character', pos);
        range.select();

    }
}
//////


function allEve(e) {
    var ev = (window.event) ? window.event : e;
    if (!ev || !ev.type) return false;
    var ME = ev;
    if (ME.type.indexOf('key') != -1) {
        if (ME.type.indexOf('keypress') != -1) {
            //            ME.key= (ev.keyCode)? ev.keyCode: ((ev.charCode)? ev.charCode: ev.which);
            ME.key = (ev.keyCode) ? ev.keyCode : ev.which;
        }
        else ME.key = ev.charCode;


        if (ME.key) ME.letter = String.fromCharCode(ME.key);
    }
    return ME;
}
function handleKey(e) {
    var c = allEve(e).key;
    return c;
}

function convertToUnicode(e, txtBox, maxlength) {

    if (maxlength == undefined) {
        maxlength = 255;
    }
    if (txtBox.value.length > maxlength) {
        return false;
    }

    var keycode = (e.keyCode) ? e.keyCode : e.which;
    if (navigator.appName == 'Netscape') {
        if (e.charCode == 0)
            return true;
        if (keycode == 13 || keycode == 8 || keycode == 9 || keycode == 32 || keycode == 33 || keycode == 37 || keycode == 38 || keycode == 35 || keycode == 36 || keycode == 45 || keycode == undefined)
        { return true }
        else {
            var newText = "";
            //    newText=txtBox.value+engToHin(keycode);

            ////Changes By Binod
            //newText =
            insertCode(txtBox, engToHin(keycode), keycode)

            //    txtBox.value=newText;
            remXtra(txtBox);
            return false;
        }
    }
    else {
        if (keycode == 13 || keycode == 8 || keycode == 9 || keycode == 32 || keycode == 33 || keycode == 37 || keycode == 38 || keycode == 35 || keycode == 36 || keycode == 45 || keycode == undefined)
        { return true }
        else {
            var newText = "";
            //    newText=txtBox.value+engToHin(keycode);

            ////Changes By Binod
            //newText =
            insertCode(txtBox, engToHin(keycode), keycode)
            //
            //    txtBox.value=newText;
            remXtra(txtBox);
            return false;
        }
    }
}

function engToHin(engCode) {
    var i = 0, hinCode = engCode;
    for (i = 0; i < engCodes.length; i++) {
        if (engCodes[i] == engCode) {
            hinCode = hinCodes[i];
            break;
        }
    }
    return (hinCode);
}

var flag = true;
function remXtra(txtBox) {
    var curText = txtBox.value;
    if (curText.charCodeAt(curText.length - 4) == 2359 && curText.charCodeAt(curText.length - 3) == 2366 && curText.charCodeAt(curText.length - 2) == 2352 && curText.charCodeAt(curText.length - 1) == 2381) {
        txtBox.value = curText.substring(0, txtBox.value.length - 4) + String.fromCharCode(2352) + String.fromCharCode(2381) + String.fromCharCode(2359) + String.fromCharCode(2366);
    }
    if (curText.charCodeAt(curText.length - 4) == 2358 && curText.charCodeAt(curText.length - 3) == 2352 && curText.charCodeAt(curText.length - 2) == 2381 && curText.charCodeAt(curText.length - 1) == 2368) {
        txtBox.value = curText.substring(0, txtBox.value.length - 4) + String.fromCharCode(2352) + String.fromCharCode(2381) + String.fromCharCode(2358) + String.fromCharCode(2368);
    }
    if (curText.charCodeAt(curText.length - 3) == 2367 && curText.charCodeAt(curText.length - 2) == 2352 && curText.charCodeAt(curText.length - 1) == 2381) {
        //alert(String.fromCharCode(curText.charCodeAt(curText.length-5)));
        txtBox.value = curText.substring(0, txtBox.value.length - 5) + String.fromCharCode(2352, 2381, curText.charCodeAt(curText.length - 5), 2367);
        flag = false;
        return (true);
    }
    /*if(curText.charCodeAt(curText.length-3)==2352&&curText.charCodeAt(curText.length-2)==2381&&curText.charCodeAt(curText.length-1)==2367)
    {
    alert('hi');
    txtBox.value=curText.substring(0,txtBox.value.length-4)+String.fromCharCode(2352,2381,curText.charCodeAt(curText.length-4),2367);
    flag=false;
    return(true);    
    }*/
    /*if(curText.charCodeAt(curText.length-2)==2367&&flag)
    {
    //alert(curText.charCodeAt(curText.length-2));
    txtBox.value=curText.substring(0,txtBox.value.length-2)+String.fromCharCode(curText.charCodeAt(curText.length-1),2367);
    flag=false;
    return(true);        
    }*/
    if (curText.charCodeAt(curText.length - 3) == 2381 && curText.charCodeAt(curText.length - 2) == 8205 && curText.charCodeAt(curText.length - 1) == 2366) {

        txtBox.value = curText.substring(0, txtBox.value.length - 3);
    }
    if (curText.charCodeAt(curText.length - 2) == 2309 && curText.charCodeAt(curText.length - 1) == 2366) {

        txtBox.value = curText.substring(0, txtBox.value.length - 3) + String.fromCharCode(2310);
    }
    if (curText.charCodeAt(curText.length - 2) == 2366 && curText.charCodeAt(curText.length - 1) == 2375) {

        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2379);
    }
    if (curText.charCodeAt(curText.length - 2) == 2352 && curText.charCodeAt(curText.length - 1) == 2381) {

        txtBox.value = curText.substring(0, txtBox.value.length - 3) + String.fromCharCode(2352, 2381, curText.charCodeAt(curText.length - 3));
    }
    if (curText.charCodeAt(curText.length - 2) == 2366 && curText.charCodeAt(curText.length - 1) == 2373) {

        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2377);
    }
    else if (curText.charCodeAt(curText.length - 2) == 2377 && curText.charCodeAt(curText.length - 1) == 2306) {

        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2366, 2305);
    }
    if (curText.charCodeAt(curText.length - 3) == 2352 && curText.charCodeAt(curText.length - 2) == 2381 && curText.charCodeAt(curText.length - 1) == 2311) {

        txtBox.value = curText.substring(0, txtBox.value.length - 3) + String.fromCharCode(2312);
    }
    if (curText.charCodeAt(curText.length - 2) == 2310 && curText.charCodeAt(curText.length - 1) == 2375) {

        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2323);
    }
    if (curText.charCodeAt(curText.length - 2) == 2310 && curText.charCodeAt(curText.length - 1) == 2376) {

        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2324);
    }
    if (curText.charCodeAt(curText.length - 2) == 2313 && curText.charCodeAt(curText.length - 1) == 2370) {

        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2314);
    }
    if (curText.charCodeAt(curText.length - 2) == 2319 && curText.charCodeAt(curText.length - 1) == 2375) {

        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2320);
    }
    if (curText.charCodeAt(curText.length - 2) == 2310 && curText.charCodeAt(curText.length - 1) == 2373) {

        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2321);
    }
    if (curText.charCodeAt(curText.length - 2) == 2366 && curText.charCodeAt(curText.length - 1) == 2376) {
        txtBox.value = curText.substring(0, txtBox.value.length - 2) + String.fromCharCode(2380);
    }
    if (curText.charCodeAt(curText.length - 3) == 2366 && curText.charCodeAt(curText.length - 2) == 2352 && curText.charCodeAt(curText.length - 1) == 2381) {

        txtBox.value = curText.substring(0, txtBox.value.length - 4) + String.fromCharCode(2352, 2381, curText.charCodeAt(curText.length - 4), 2366);
    }
    if (curText.charCodeAt(curText.length - 2) == 2352 && curText.charCodeAt(curText.length - 1) == 2381 && curText.charCodeAt(curText.length - 3) == 2311) {

        txtBox.value = curText.substring(0, txtBox.value.length - 3) + String.fromCharCode(2312);
    }
    if (curText.charCodeAt(curText.length - 2) == 2352 && curText.charCodeAt(curText.length - 1) == 2381 && curText.charCodeAt(curText.length - 3) == 2368) {

        txtBox.value = curText.substring(0, txtBox.value.length - 4) + String.fromCharCode(2352, 2381, curText.charCodeAt(curText.length - 4), 2368);
    }

    flag = true;
    return (true);
}
