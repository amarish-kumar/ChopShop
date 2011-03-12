/// <reference path="jquery-1.4.4-vsdoc.js"/>
/// <reference path="chopShop.admin.js" />

/// Configure all buttons and jQueryUI behaviours from here.  Call functions in chopShop.admin for real functionality
$(function () {
    $('button, input:submit').button().click(function () { return false; });

    $('#dialog-addCategory').dialog({
        autoOpen: false,
        beforeClose: function () {
            admin.refreshCategoriesForProduct();
        }
    });

    $('#dialog-addPrice').dialog({
        autoOpen: false
    });

    $('#button-addCategory').click(function () {
        admin.displayAllCategories(function () {
            $('#dialog-addCategory').dialog('open');
        });
    });

    $('#button-addPrice').click(function () {
        $('#dialog-addPrice').dialog('open');
    });

    $('#button-closeCategoryDialog').click(function () {
        $('#dialog-addCategory').dialog('close');
    });
});