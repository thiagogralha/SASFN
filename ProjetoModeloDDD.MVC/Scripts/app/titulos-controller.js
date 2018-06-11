angular.module('TitulosApp', ['ngAnimate', 'angularUtils.directives.dirPagination', 'ui.mask'])
    .controller('TitulosCtrl',
    function ($scope, $http) {

        $scope.model = {};
        $scope.show = { erro: false };
        $scope.labels = {};
        $scope.titulo = null;
        $scope.loader = false;
        
        $scope.ordenar = function (keyname) {
            $scope.sortKey = keyname;
            $scope.reverse = !$scope.reverse;
        };

        $scope.pesquisarTitulos = function () {

            $scope.loader = true; 

            $http({
                method: 'POST',
                url: '/Titulos/GetTitulos?tituloId=' + $scope.model.Procurar
            })
                .then(function (response) {
                    if (response.data.erro) {
                        $scope.show.erro = response.data.erro;
                        $scope.titulo = null;
                    }
                    else {
                        $scope.show.erro = null;
                        $scope.titulo = response.data;
                    }

                    $scope.loader = false; 
                },
                function (response) {
                    $scope.titulo = null;
                    $scope.loader = false; 
                });
        }

        $scope.confirmarAlteracao = function () {

            $http({
                method: 'POST',
                url: '/Titulos/InsertTitulo',
                data: $scope.titulo
            })
                .then(function (response) {                    
                    return response;
                },
                function (response) {
                    return response;
                });
        }

    });
