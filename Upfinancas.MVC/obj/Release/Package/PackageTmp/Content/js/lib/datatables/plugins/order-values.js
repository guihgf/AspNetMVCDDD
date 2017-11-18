//Ordenação de números com casa decimais em ","
jQuery.fn.dataTableExt.oSort['numeric-comma-asc'] = function (a, b) {
    //remove the dots (.) from the string and then replaces the comma with a dot
    var x = (a == "-") ? 0 : a.replace(/\./g, "").replace(/,/, ".");
    var y = (b == "-") ? 0 : b.replace(/\./g, "").replace(/,/, ".");
    x = parseFloat(x);
    y = parseFloat(y);
    return ((x < y) ? -1 : ((x > y) ? 1 : 0));
};

jQuery.fn.dataTableExt.oSort['numeric-comma-desc'] = function (a, b) {
    var x = (a == "-") ? 0 : a.replace(/\./g, "").replace(/,/, ".");
    var y = (b == "-") ? 0 : b.replace(/\./g, "").replace(/,/, ".");
    x = parseFloat(x);
    y = parseFloat(y);
    return ((x < y) ? 1 : ((x > y) ? -1 : 0));
};

//numeric comma autodetect
jQuery.fn.dataTableExt.aTypes.unshift(
    function (sData) {
        //include the dot in the sValidChars string (don't place it in the last position)
        var sValidChars = "0123456789-.,";
        var Char;
        var bDecimal = false;

        /* Check the numeric part */
        for (i = 0 ; i < sData.length ; i++) {
            Char = sData.charAt(i);
            if (sValidChars.indexOf(Char) == -1) {
                return null;
            }

            /* Only allowed one decimal place... */
            if (Char == ",") {
                if (bDecimal) {
                    return null;
                }
                bDecimal = true;
            }
        }

        return 'numeric-comma';
    }
);/**
 * Created by ghenrique on 08/01/2016.
 */
