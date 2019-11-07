app.controller("form", function ($http, $state,$stateParams) {
    vm = this;
    vm.product = {};
    vm.id = $stateParams.id;
    if (vm.id) {
        $http({
            method: "GET",
            url:"/api/product?Id="+vm.id,
        }).then(function (res) {
            vm.product = res.data;
        })
    }

    vm.Update = function (index) {
  
        var post = $http({
            method: "POST",
            url: "/api/product",
            data: '{customer:' + JSON.stringify(customer) + '}',
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        });
        post.success(function (data, status) {
            //Setting EditMode to FALSE hides the TextBoxes for the row.
            customer.EditMode = false;
        });
    };
});