
var app;
(function () {
    'use strict'; //Defines that JavaScript code should be executed in "strict mode"  
    app = angular.module('myapp',
        ['ui.router',
            "ngTable", 
            "kendo.directives",
        ]);
})
();  

app.config(function ($stateProvider, $urlRouterProvider) {
    //$urlRouterProvider.otherwise('/home'); // Url không hợp lệ sẽ chuyển về state home
    $stateProvider
        .state('listproduct', { // định nghĩa State
            url: 'listproduct',  // khai báo Url hiển thị
            templateUrl: 'Product/ListProduct' // đường dẫn đến View của mình
        })
        .state('CreateProduct', {
            url: 'CreateProduct?id=',
            templateUrl: 'Product/CreateProduct'
        })

        .state('editproduct', {
            url: 'editproduct',
            templateUrl: 'Product/EditProduct'
        })
        //CUstomer
        .state('listcustomer', {
            url: 'listcustomer',
            templateUrl: 'Customer/ListCustomer'
        })

        .state('addcustomer', {
            url: 'addcustomer',
            templateUrl: 'Customer/AddCustomer'
        })
        .state('editcustomer', {
            url: 'editcustomer',
            templateUrl: 'Customer/EditCustomer'
        })
        .state('deletecustomer', {
            url: 'deletecustomer',
            templateUrl: 'Customer/DeleteCustomer'
        })

        //Order Product
        .state('createorder', {
            url: 'createorder?id=',
            templateUrl: 'Order/CreateOrder'
        })
        .state('listorder', {
            url: 'listorder',
            templateUrl: 'Order/ListOrder'
        })
        .state('list', {
            url: 'list',
            templateUrl: 'Product/List'
        })
        .state('form', {
            url: '/form?id',
            templateUrl: 'Product/Form'
        })  
        .state('detail', {
            url: '/detail?id',
            templateUrl: 'Order/Detail'
        }) 
        //Catelogy
        .state('listcatelogy', {
            url: '/listcatelogy',
            templateUrl: 'Catelogy/ListCatelogy'
        }) 
});

