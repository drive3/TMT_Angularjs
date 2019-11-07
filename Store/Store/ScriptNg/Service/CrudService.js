app.service('CrudService', function ($http) {
    var urlGet = '';
    this.post = function (apiRoute, Model) {
        var request = $http({
            method: "post",
            url: apiRoute,
            data: Model
        });
        return request;
    }
    this.put = function (apiRoute, Model) {
        var request = $http({
            method: "put",
            url: apiRoute,
            data: Model
        });
        return request;
    }
    this.delete = function (apiRoute) {
        var request = $http({
            method: "delete",
            url: apiRoute
        });
        return request;
    }
    this.getAll = function (apiRoute) {
        urlGet = apiRoute;
        return $http.get(urlGet);
    }
    this.getbyIDProduct = function (apiRoute, productID) {
        urlGet = apiRoute + '/' + productID;
        return $http.get(urlGet);
    }
    this.getbyIDCustomer = function (apiRoute, customerID) {
        urlGet = apiRoute + '/' + customerID;
        return $http.get(urlGet);
    }
    this.getbyIdOrder = function (apiRoute, orderID) {
        urlGet = apiRoute + '/' + orderID;
        return $http.get(urlGet);
    }
    this.getbyIdCatelogy = function (apiRoute, catelogyID) {
        urlGet = apiRoute + '/' + catelogyID;
        return $http.get(urlGet);
    }
});  