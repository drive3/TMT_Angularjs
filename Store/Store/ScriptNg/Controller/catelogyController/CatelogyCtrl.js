app.controller('CatelogyCtrl', ['$scope', 'CrudService', '$state', 'NgTableParams', '$http', '$stateParams', '$window', 'flash',
    function ($scope, CrudService, $state, NgTableParams, $http, $stateParams, $window, flash) {
 
        var vm = this;
        var baseUrl = '/api/Catelogy/';
        vm.btnText = "Save";
        vm.catelogyID = 0;
        vm.SaveUpdate = function () {
            debugger
            var catelogy = {
                Name: vm.Name,
                Description: vm.Description,
                Date: new Date(),
                Id: vm.catelogyID,
            }
            if (vm.btnText == "Save") {
                var apiRoute = baseUrl;
                var saveCatelogy = CrudService.post(apiRoute, catelogy);
                saveCatelogy.then(function (response) {
                    if (response.data != "") {
                        alert("Tạo Danh Mục Mới Thành Công");
                        vm.Clear();
                        vm.GetCatelogy();
                    } else {
                        alert("Some error");
                    }
                }, function (error) {
                    console.log("Error: " + error);
                    alert("Vui Lòng Nhập Đầy Đủ Thông Tin");
                });
            } else {
                var apiRoute = baseUrl + 'UpdateCatelogy/';
                var UpdateCatelogy = CrudService.put(apiRoute, catelogy);
                UpdateCatelogy.then(function (response) {
                    if (response.data != "") {
                        alert("Data Update Successfully");
                        vm.GetCatelogy();
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
        //get data
        vm.GetCatelogy = function () {
            var apiRoute = baseUrl;
            var catelogy = CrudService.getAll(apiRoute);
            catelogy.then(function (response) {
                debugger
                vm.catelogys = response.data;
                //page
                vm.tableParams = new NgTableParams({}, { dataset: response.data });
            }, function (error) {
                console.log("Error: " + error);
            });
        }
        vm.GetCatelogy();
        vm.GetCatelogyByID = function (dataModel) {
            debugger
            var apiRoute = baseUrl;
            var catelogy = CrudService.getbyIdCatelogy(apiRoute, dataModel.Id);
            catelogy.then(function (response) {
                vm.catelogyID = response.data.Id;
                vm.Name = response.data.Name;
                vm.Description = response.data.Description;
                vm.Date = response.data.Date
                vm.btnText = "Update";
            }, function (error) {
                console.log("Error: " + error);
            });
        }

        vm.DeleteCatelogy = function (dataModel) {
            debugger
            var apiRoute = baseUrl + 'DeleteProduct?id=' + dataModel.Id;
            var deletecatelogy = CrudService.delete(apiRoute);
            if ($window.confirm("Do you want to delete this row?")) {
                deletecatelogy.then(function (response) {
                    if (response.data != "") {
                        alert("Data Delete Successfully");
                        vm.GetCatelogy();
                        vm.Clear();
                    } else {
                        alert("Some error");
                    }

                }, function (error) {
                    console.log("Error: " + error);
                    alert("Không Thể Xoá Danh Mục Đã Tồn Tại Trong Sản Phẩm");
                });
            }
        }
     
        vm.Clear = function () {
            vm.catelogyID = 0;
            vm.Name = "";
            vm.Description = "";
            vm.Date = "";
            vm.btnText = "Save"
        }
        vm.submitForm = function () {
            // check to make sure the form is completely valid
            if ($scope.userForm.$valid) {
                alert('our form is amazing');
            }

        };
    }
]);
