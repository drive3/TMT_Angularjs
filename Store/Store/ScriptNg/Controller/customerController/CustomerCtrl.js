app.controller('CustomerCtrl', ['$scope', 'CrudService', 'NgTableParams', '$state',
    function ($scope, CrudService, NgTableParams, $state) {

        // Base Url 
        var baseUrl = '/api/Customers/';
        var vm = this;
        vm.btnText = "Save";
        vm.titleModal = "Add Customer"
        vm.customerID = 0;
        vm.customer = {};
        // Add Product and Update
        vm.SaveUpdate = function () {
            debugger
            var customer = {
                Id: vm.customerID,
                Name: vm.Name,
                Address: vm.Address,
                PhoneNumber: vm.PhoneNumber,
                DOB: new Date(),
            }
            if (vm.btnText == "Save") {
                var apiRoute = baseUrl + 'SaveCustomer/';
                var SaveCustomer = CrudService.post(apiRoute, customer);
                SaveCustomer.then(function (response) {
                    if (response.data != "") {
                        alert("Thêm Khách Hàng Thành Công !");
                        vm.GetCustomers();
                        vm.Clear();

                    } else {
                        alert("Thêm Khách Hàng Thất Bại !");
                    }
                }, function (error) {
                    console.log("Error: " + error);
                });
            }
            else {
                var apiRoute = baseUrl + 'UpdateCustomer/';
                var UpdateCustomer = CrudService.put(apiRoute, customer);
                UpdateCustomer.then(function (response) {
                    if (response.data != "") {
                        alert("Thay Đổi Sản Phẩm Thành Công");
                        vm.GetCustomers();              
                        vm.Clear();
                        $state.go('listcustomer');
                    } else {
                        alert("Thay Đổi Sản Phẩm Thất Bại");
                    }

                }, function (error) {
                    console.log("Error: " + error);
                });
            }
        }
        //update Product
        vm.GetCustomerByID = function (dataModel) {
            debugger
            var apiRoute = baseUrl;
            var customer = CrudService.getbyIDCustomer(apiRoute, dataModel.Id);
            customer.then(function (response) {
                vm.customerID = response.data.Id;
                vm.Name = response.data.Name;
                vm.Address = response.data.Address;
                vm.DOB = response.data.DOB;
                vm.PhoneNumber = response.data.PhoneNumber;
                vm.btnText = "Update";
                vm.titleModal = "Edit Customer";
            },
                function (error) {
                    console.log("Error: " + error);
                });
        }
        // Get Data Product
        vm.GetCustomers = function () {
            debugger
            var apiRoute = baseUrl;
            var customer = CrudService.getAll(apiRoute);
            customer.then(function (response) {

                vm.customers = response.data;
                //// page
                vm.tableParams = new NgTableParams({}, { dataset: response.data });  

            },
                function (error) {
                    console.log("Error: " + error);
                });
        }
        vm.GetCustomers();
        // Delete Product
        vm.DeleteCustomer = function (dataModel) {
            debugger
            var apiRoute = baseUrl + 'DeleteCustomer?id=' + dataModel.Id;
            var deleteCustomer = CrudService.delete(apiRoute);
            deleteCustomer.then(function (response) {
                if (response.data != "") {
                    alert("Xoá Khách Hàng Thành Công");
                    vm.GetCustomers();
                    vm.Clear();

                } else {
                    alert("Xoá Khách Hàng Thất Bại");
                }

            }, function (error) {
                    console.log("Error: " + error);
                    alert("Không Thể Xoá Khách Hàng Đã Tồn Tại Trong Hoá Đơn !");
            });
        }
        vm.Clear = function () {
            vm.customerID = 0;
            vm.Name = "";
            vm.Address = "";
            vm.Address = "";
            vm.PhoneNumber = "";
            vm.btnText = "Save";
            vm.titleModal = "Add Customer"
        }
    }]);