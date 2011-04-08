/// <reference path="jquery-1.4.4-vsdoc.js"/>
/// <reference path="http://ajax.microsoft.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js" />
/// <reference path=json2.js" />

var admin = {
    product: {},
    categoriesHaveChanged: false,

    getAllCategories: function (callback) {
        $.get('/Category/_CategoriesForSelectDialog', { 'id': admin.product.Id }, function (result) {
            if (callback) {
                callback(result);
            }
            return result;
        }, 'json');
    },

    displayAllCategories: function (callback) {
        this.getAllCategories(function (result) {
            if (result) {
                $('#dialog-listCategory').empty();
                $('#tmpl-listCategories').tmpl(result).appendTo('#dialog-listCategory');
            }
        });

        if (callback) {
            callback();
        }
    },

    refreshCategoriesForProduct: function (callback) {
        if (this.categoriesHaveChanged) {
            // refresh the categories on the page
            this.getAllCategoriesForProduct(function (result) {
                if (result) {
                    $('#page-listCategory').empty();
                    jQuery.each(result, function (key, value) {
                        var categoryName = value.Name;
                        var categoryId = value.Id;
                        $('#page-listCategory').append('<li>' + categoryName + ' <span data-categoryId="' + categoryId + '">X</span></li>');
                    });
                }
            });
            this.categoriesHaveChanged = false;
        }
    },

    refreshPricesForProduct: function(callback){

    },

    addCategoryToProduct: function (categoryId, callback) {
        $.post('/Category/_AddToProduct', { 'categoryId': categoryId, 'productId': admin.product.Id }, function (result) {
            if (callback) {
                admin.categoriesHaveChanged = true;
                callback(result);
            }
            return result;
        }, 'json');
    },

    removeCategoryFromProduct: function (categoryId, callback) {
        $.post('/Category/_RemoveFromProduct', { 'categoryId': categoryId, 'productId': admin.product.Id }, function (result) {
            if (callback) {
                admin.categoriesHaveChanged = true;
                callback(result);
            }
            return result;
        }, 'json');
    },

    getAllCategoriesForProduct: function (callback) {
        $.get('/Category/_CategoriesForProduct', { 'id': admin.product.Id }, function (result) {
            if (callback) {
                callback(result);
            }
            return result;
        }, 'json');
    },

    addCategory: function (callback) {
        var category = { Name: $('#input-categoryName').val(), Id: '00000000-0000-0000-0000-000000000000', Description: '' };
        $.ajax({
            url: '/Category/_Add',
            type: 'POST',
            data: JSON.stringify(category),
            dataType: 'json',
            contentType: 'application/json;charset=utf-8',
            success: function (result) {
                if (callback) {
                    callback(result);
                }
                return result;
            }
        });
    },

    resetAddPriceDialog: function (callback) {
        $('#Value').val('0.00');
        $('#IsTaxIncluded').attr('checked', false);
        $('#TaxRate').val('0.00');
        $('#Currency').val('GBP');
        if (callback) {
            callback();
        }
    },

    addPriceToProduct: function (callback) {
        var price = {
            Value: $('#Value').val(),
            IsTaxIncluded: $('#IsTaxIncluded').is(':checked'),
            TaxRate: $('#TaxRate').val(),
            Currency: $('#Currency').val(),
            ProductId: admin.product.Id
        };

        $.ajax({
            url: '/Product/_AddPrice',
            type: 'POST',
            data: JSON.stringify(price),
            dataType: 'json',
            contentType: 'application/json;charset=utf-8',
            success: function (result) {
                if (callback) {
                    callback(result);
                }
            }
        });
    }

};

