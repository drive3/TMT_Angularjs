app.controller('ProductCtrl', ['$scope', 'CrudService', '$state', 'NgTableParams', '$http', '$stateParams', '$window','flash',
    function ($scope, CrudService, $state, NgTableParams, $http, $stateParams, $window, flash) {
        // Base Url   

        $scope.msgTitle = 'Alert';
        $scope.msgBody = 'The Tomatoes Exploded!';
        $scope.msgType = 'warning';
        $scope.flash = flash;
        //
        var vm = this;
        var baseUrl = '/api/Product/';
        var base = '/api/Catelogy/';
        vm.btnText = "Save";
        vm.productID = 0;
        //update
        vm.product = {};
        vm.header = "Create Product"
        vm.id = $stateParams.id;
        if (vm.id) {
            debugger
            $http({
                method: "GET",
                url: "/api/product?id=" + vm.id,
            }).then(function (res) {
                vm.product = res.data;
                vm.productID = res.data.Id,
                    vm.btnText = "Update";
                vm.header = "Edit Product"
            })
        }
        //
        vm.SaveUpdate = function () {
            debugger
            var product = {
                Name: vm.product.Name,
                Price: vm.product.Price,
                Description: vm.product.Description,
                Note: vm.product.Note,
                DateCreated: new Date(),
                CatelogyID: vm.product.CatelogyID,
                CodeProduct: new Date(),
                Id: vm.productID,
            }
            if (vm.btnText == "Save") {
                var apiRoute = baseUrl;
                var saveProduct = CrudService.post(apiRoute, product);
                saveProduct.then(function (response) {
                    if (response.data != "") {
                        alert("Tạo Sản Phẩm Mới Thành Công")
;                        vm.Clear();
                        vm.GetProducts();
                    } else {
                        alert("Some error");
                    }
                }, function (error) {
                        console.log("Error: " + error);
                        alert("Vui Lòng Nhập Đầy Đủ Thông Tin");
                });
            } else {
                var apiRoute = baseUrl + 'UpdateProduct/';
                var UpdateProduct = CrudService.put(apiRoute, product);
                UpdateProduct.then(function (response) {
                    if (response.data != "") {
                        alert("Data Update Successfully");
                        vm.GetProducts();
                        $state.go('listproduct');
                        vm.Clear();
                     
                    } else {
                        alert("Some error");
                    }
                }, function (error) {
                        console.log("Error: " + error);
                        alert("Vui Lòng Nhập Đầy Đủ Thông Tin");
                });
            }
        }
       // get catelogy
        vm.GetCatelogy = function () {
            debugger
            var apiRoute = base;
            var catelogy = CrudService.getAll(apiRoute);
            catelogy.then(function (response) {
                debugger
                vm.catelogys = response.data;
                vm.productsDataSource = {
                    serverFiltering: true,
                    data: response.data,
                };
            }, function (error) {
                console.log("Error: " + error);
            });
        }
        vm.GetCatelogy();
        //get data
        vm.GetProducts = function () {
            var apiRoute = baseUrl;
            var product = CrudService.getAll(apiRoute);
            product.then(function (response) {
                debugger
                vm.products = response.data;
                //page
                vm.tableParams = new NgTableParams({}, { dataset: response.data });
            }, function (error) {
                console.log("Error: " + error);
            });
        }
        vm.GetProducts();

        vm.DeleteProduct = function (dataModel) {
            debugger
            var apiRoute = baseUrl + 'DeleteProduct?id=' + dataModel.Id;
            var deleteProduct = CrudService.delete(apiRoute);
            if ($window.confirm("Do you want to delete this row?")) {
                deleteProduct.then(function (response) {

                    if (response.data != "") {
                        alert("Data Delete Successfully");
                        vm.GetProducts();
                        vm.Clear();
                    } else {
                        alert("Some error");
                    }
                    
                }, function (error) {
                    console.log("Error: " + error);
                    alert("Không Thể Xoá Sản Phẩm Đã Tồn Tại Trong Hoá Đơn");
                });
            }
        }
        vm.going = function (dataModel) {
            debugger
            $state.go('CreateProduct', { id: dataModel.Id });
        }
        $scope.add = function () {
            debugger
            $state.go('CreateProduct', {});
        }
        vm.Clear = function () {
            vm.productID = 0;
            vm.product.Name = "";
            vm.product.Price = "";
            vm.product.CatelogyID = "";
            vm.product.Description = "";
            vm.product.Note = "";
            vm.btnText = "Save"
            vm.CatelogyID = "";
        }
    }
]);

app.factory("flash", function ($rootScope) {

    return {

        pop: function (message) {
            switch (message.type) {
                case 'success':
                    toastr.success(message.body, message.title);
                    break;
                case 'info':
                    toastr.info(message.body, message.title);
                    break;
                case 'warning':
                    toastr.warning(message.body, message.title);
                    break;
                case 'error':
                    toastr.error(message.body, message.title);
                    break;
            }
        }
    };
});