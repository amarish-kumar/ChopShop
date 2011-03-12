/// <reference path="jquery-1.4.4-vsdoc.js"/>
/// <reference path="http://ajax.microsoft.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js" />

var admin = {
    allCategories: {},

    getAllCategories: function (callback) {
        $.get('/Category/List', '', function (result) {
            admin.allCategories = result;
            if (callback) {
                callback();
            }
        }, 'json');
    },

    displayAllCategories: function (callback) {
        this.getAllCategories(function () {
            if (admin.allCategories) {
                $('#tmpl-listCategories').tmpl(admin.allCategories).appendTo('#dialog-listCategory');
            }
        });

        if (callback) {
            callback();
        }
    },

    refreshCategoriesForProduct: function (callback) {
        // refresh the categories on the page
        
    }

};

