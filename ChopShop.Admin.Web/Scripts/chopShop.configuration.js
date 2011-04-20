/// <reference path="jquery-1.4.4-vsdoc.js"/>
/// <reference path="chopShop.admin.js" />

/// Configure all buttons and jQueryUI behaviours from here.  Call functions in chopShop.admin for real functionality
$(function () {
    $('button, .button').button().click(function () { return false; });
    $('input:submit').button();
    $('#dialog-addCategory').dialog({
        autoOpen: false,
        beforeClose: function () {
            admin.refreshCategoriesForProduct();
        }
    });

    $('#dialog-addPrice').dialog({
        autoOpen: false,
        beforeClose: function () {
            admin.refreshPricesForProduct();
        }
    });

    $('#button-selectCategory').click(function () {
        admin.displayAllCategories(function () {
            $('#dialog-addCategory').dialog('open');
        });
    });

    $('#button-addPrice').click(function () {
        admin.resetAddPriceDialog(function () {
            $('#dialog-addPrice').dialog('open');
        });
    });

    $('#button-closeCategoryDialog').click(function () {
        $('#dialog-addCategory').dialog('close');
    });

    $('#button-addCategory').click(function () {
        admin.addCategory(function (result) { // create a new category
            if (result != '00000000-0000-0000-0000-000000000000') {
                admin.addCategoryToProduct(result, function (result) { // add the new category to the product
                    if (result) {
                        admin.displayAllCategories(); // refresh the list of categories in the dialog
                    }
                });
            }
        });
    });

    $('#page-listCategory span').live('click', function () {
        var categoryId = $(this).data('categoryId');
        var $this = $(this);
        admin.removeCategoryFromProduct(categoryId, function () {
            $this.parent().remove();
        });

    });

    $('#dialog-listCategory span').live('click', function () {
        var categoryId = $(this).data('categoryId');
        var $this = $(this);
        admin.addCategoryToProduct(categoryId, function () {
            $this.parent().addClass('item-selected');
        });
    });

    $('#button-createPrice').click(function () {
        admin.addPriceToProduct(function () {
            $('#dialog-addPrice').dialog('close');
        });
    });
});