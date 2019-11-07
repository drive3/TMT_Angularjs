app.controller('OrderDetailCtrl', ['$scope', 'CrudService', 'NgTableParams', '$state','$stateParams',
    function ($scope, CrudService, NgTableParams, $state, $stateParams) {
        var vm = this;
        var baseUrl = '/api/order/';
        vm.btnText = "Save";
       
        //get list order
        vm.GetOrder = function () {
            var apiRoute = baseUrl;
            var product = CrudService.getAll(apiRoute);
            product.then(function (response) {
                debugger
                vm.orders = response.data;
                //page
                vm.tableParams = new NgTableParams({}, { dataset: response.data });
               
            }, function (error) {
                console.log("Error: " + error);
            });
        }
        vm.GetOrder();
        //Delete Order
        vm.DeleteOrder = function (dataModel) {
            debugger
            var apiRoute = baseUrl + 'DeleteOrder?id=' + dataModel.Id;
            var deleteOrder = CrudService.delete(apiRoute);

            deleteOrder.then(function (response) {
                if (response.data != "") {
                    alert("Data Delete Successfully");
                    vm.GetOrder();
                } else {
                    alert("Some error");
                }
            }, function (error) {
                console.log("Error: " + error);
            });
        }
        ////
        $scope.going = function (dataModel) {
            debugger
            $state.go('createorder', { id: dataModel.Id });      
        }
        $scope.add = function () {
            debugger
            $state.go('createorder', {});
        }
        $scope.detaiOrder = function (dataModel) {
            debugger
            $state.go('detail', { id: dataModel.Id});
        }
    }]);