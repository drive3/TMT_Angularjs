app.controller("OrderController", ['$scope', 'CrudService', '$http', '$stateParams', '$state',
    function ($scope, CrudService, $http, $stateParams, $state) {
        var vm = this;
        var baseUrl = '/api/Order/';
        var proUrl = '/api/Product/';
        var cusUrl = '/api/Customers/';
        vm.btnText = "Save Order";
        vm.orderID = 0;
        vm.value = 1;
        vm.order = {};
        vm.customers = {};
        vm.id = $stateParams.id;
        if (vm.id) {
            debugger
            $http({
                method: "GET",
                url: "/api/order?id=" + vm.id,
            }).then(function (res) {
                vm.order = res.data;
                vm.orderID = res.data.Id,
                vm.btnText = "Update Order";
            })
        }
        //Get Kenndo Customer
        //get customer
        vm.GetCustomers = function () {
            debugger
            var apiRoute = cusUrl;
            var customer = CrudService.getAll(apiRoute);
            customer.then(function (response) {
                vm.customers = response.data;
                vm.productsDataSource = {
                    serverFiltering: true,
                    data: response.data,
                };
            },
                function (error) {
                    console.log("Error: " + error);
                });
        }
        vm.GetCustomers();
        // get product
        vm.GetProducts = function () {
            debugger
            var apiRoute = proUrl;
            var product = CrudService.getAll(apiRoute);
            product.then(function (response) {
                debugger
                vm.products = response.data;
                ////page
                //$scope.tableParams = new NgTableParams({}, { dataset: response.data });
            }, function (error) {
                console.log("Error: " + error);
            });
        }
        vm.GetProducts();
        //SaveOrder
        vm.SaveOrderProduct = function () {
            debugger
            var order = {
                Id: vm.orderID,
                OrderNumber: vm.qty(),
                TotalAmount: vm.getTotal(),
                orderDate: vm.order.orderDate,
                Items: vm.order.Items,
                CustomerId: vm.order.CustomerId,
            }
            if (vm.btnText == "Save Order") {
                var apiRoute = baseUrl + 'SaveOrderProduct/';
                var saveorder = CrudService.post(apiRoute, order);
                saveorder.then(function (response) {
                    if (response.data != "") {
                        alert("Lưu Hoá Đơn  Thành Công");
                        vm.clearCart();  
                    } else {
                        alert("Vui Lòng Nhập Đầy Đủ Thông Tin");
                    }

                }, function (error) {
                    console.log("Error: " + error);
                });
            } else {
                var apiRoute = baseUrl + 'UpdateOrderProduct?key=' + vm.id;
                var UpdateOrder = CrudService.put(apiRoute, order);
                UpdateOrder.then(function (response) {
                    if (response.data != "") {
                        alert("Thay Đổi Thông Tin Hoá Đơn Thành Công");
                        vm.clearCart();
                        $state.go('listorder');
                    } else {
                        alert("Thay Đổi Không Thành Công");
                    }
                }, function (error) {
                    console.log("Error: " + error);
                });
            }
        }
     
        //get 

        // Cost
        vm.getCost = function (item) {
            return item.Quantiy * item.Price;
        }
        vm.order.Items = [];
        var findItemById = function (items, id) {
            debugger
            return find(items, function (item) {
                return item.id === Id;
            });
        };
        // add item

        vm.addItem = function (itemToAdd) {
            debugger
            var found = findItemById(vm.order.Items, itemToAdd.id);
            itemToAdd.qty = 1;
            data = {
                ProductName: itemToAdd.Name,
                //Quantiy: itemToAdd.Quantiy,
                Price: itemToAdd.Price,
                ProductId: itemToAdd.Id,
                Quantiy: itemToAdd.qty
            }
            if (found) {
                found.qty += itemToAdd.qty;
            }
            else {
                vm.order.Items.push(angular.copy(data));
            }
        };
        // TOtal
        vm.getTotal = function () {
            var total = 0;
            vm.order.Items.forEach(function (product) {
                total += product.Price * product.Quantiy;
            });
            return total;
        };
        vm.getTotal();
        // Sum Qty
        vm.qty = function () {
            debugger
            var total = 0;
            vm.order.Items.forEach(function (product) {
                total += product.Quantiy;
            });
            return total;
        };
        vm.qty();

        //clear data 
        vm.clearCart = function () {
            vm.orderID = 0;
            vm.order.Items.length = 0;
            vm.order.orderDate = 0;
            vm.order.CustomerId = ""; 
            vm.Items = {};
            vm.getCost = 0;
        };
        vm.removeItem = function (item) {
            var index = vm.order.Items.indexOf(item);
            vm.order.Items.splice(index, 1);
        };
    }]);

app.filter('xf', function () {
    function keyfind(f, obj) {
        if (obj === undefined)
            return -1;
        else {
            var sf = f.split(".");
            if (sf.length <= 1) {
                return obj[sf[0]];

            } else {
                var newobj = obj[sf[0]];
                sf.splice(0, 1);
                return keyfind(sf.join("."), newobj)
            }
        }

    }
    return function (input, clause, fields) {
        var out = [];
        if (clause && clause.query && clause.query.length > 0) {
            clause.query = String(clause.query).toLowerCase();
            angular.forEach(input, function (cp) {
                for (var i = 0; i < fields.length; i++) {
                    var haystack = String(keyfind(fields[i], cp)).toLowerCase();
                    if (haystack.indexOf(clause.query) > -1) {
                        out.push(cp);
                        break;
                    }
                }
            })
        } else {
            angular.forEach(input, function (cp) {
                out.push(cp);
            })
        }
        return out;
    }

});