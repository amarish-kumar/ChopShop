/// <reference path="jquery-1.4.4-vsdoc.js"/>
/// <reference path="http://ajax.microsoft.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js" />

var admin = {
    product: {},
    categoriesHaveChanged: false,

    getAllCategories: function (callback) {
        $.get('/Category/CategoriesForSelectDialog', { 'id': admin.product.Id }, function (result) {
            if (callback) {
                callback(result);
            }
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
                    jQuery.each(result, function () {
                        $('#page-listCategory').append('<li>' + $(this).Name + '</li>');
                    });
                }
            });
            this.categoriesHaveChanged = false;
        }
    },

    addCategoryToProduct: function (categoryId, callback) {
        $.post('/Category/AddToProduct', { 'categoryId': categoryId, 'productId': admin.product.Id }, function (result) {
            if (callback) {
                admin.categoriesHaveChanged = true;
                callback(result);
            }
        }, 'json');
    },

    removeCategoryFromProduct: function(categoryId, callback){
        $.post('/Category/RemoveFromProduct', {'categoryId':categoryId, 'productId':admin.product.Id}, function(result){
            if(callback){
                admin.categoriesHaveChanged = true;
                callback(result);
            }
        }, 'json');
    },

    getAllCategoriesForProduct: function (callback) {
        $.get('/Category/CategoriesForProduct', { 'id': admin.product.Id }, function (result) {
            if (callback) {
                callback(result);
            }
        }, 'json');
    }

};

