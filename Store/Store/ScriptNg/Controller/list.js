app.controller("list", function ($http,$state) {
    vm = this;
    vm.product = [{}];
    $http({
        method: "GET",
        url:"/api/product"
    }).then(function(res){
        vm.product = res.data;
        $scope.mainGridOptions = {
            dataSource: {
                data: res.data,
                pageSize: 5,
                serverPaging: true,
                serverSorting: true
            },
            sortable: true,
            pageable: true,
            columns: [{
                field: "Id",
                title: "Id",
                width: "120px"
            }, {
                field: "Name",
                title: "Name",
                width: "120px"
            },
            {
                field: "LastName",
                title: "Last Name",
                width: "120px"
            }]
        };

    })

    vm.edit = edit;
    function edit(item) {
        debugger
        $state.go("form", {id:item.Id})
    }
});