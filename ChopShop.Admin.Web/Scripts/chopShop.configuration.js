/// <reference path="jquery-1.4.4-vsdoc.js"/>

/// Configure all buttons and jQueryUI behaviours from here.  Call functions in chopShop.admin for real functionality
$(function () {
    $('button, input:submit').button().click(function () { return false; });

    $('#dialog-addCategory').dialog({
        autoOpen: false
    });

    $('#dialog-addPrice').dialog({
        autoOpen: false
    });

    $('#button-addCategory').click(function () {
        $('#dialog-addCategory').dialog('open');
    });

    $('#button-addPrice').click(function () {
        $('#dialog-addPrice').dialog('open');
    });
});