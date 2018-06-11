angular.module('PlanosApp', ['ngAnimate', 'angularUtils.directives.dirPagination', 'ui.mask'])
    .controller('PlanosCtrl',
    function ($scope, $http) {

        $scope.model = {};
        $scope.model.Plano = {};
        $scope.show = { erro: false };
        $scope.labels = {};
        
        $scope.init = function () {

            $http.get('/Planos/GetPlanos')
                .success(function (data) {
                    $scope.planos = data;
                });
        };

        $scope.init();

        $scope.ordenar = function (keyname) {
            $scope.sortKey = keyname;
            $scope.reverse = !$scope.reverse;
        };

        $scope.confirmarInclusao = function () {

            $http({
                method: 'POST',
                url: '/Planos/InsertPlano',
                data: $scope.model.Plano
            })
                .then(function (response) {

                    $scope.fecharmodal('#ModalInclusao');
                    $scope.init();
                    return response;
                },
                function (response) {
                    return response;
                });

        }

        $scope.excluir = function (id) {

            if (confirm("Deseja realmente excluir esse plano? ")) {

                $http.post('/Planos/Excluir?id=' + id).success(function (response) {

                    $scope.mensagem = response;
                    $scope.init();

                }).error(function (error) {
                    console.log(error);
                })
            }

        }
        
        $scope.cancelarInclusao = function () {

            $scope.show.inclusao = false;
            $scope.show.erro = false;
            $scope.fecharmodal('#ModalInclusao');

        }

        $scope.abrirModalInclusao = function () {

            $scope.model.Plano = {};
            $scope.labels.operacao = 'Inclusão do plano';
            $scope.labels.botaoIncluir = 'Incluir';
            $scope.show.inclusao = true;
            $scope.abrirmodal('#ModalInclusao');

        };

        $scope.abrirModalEdicao = function (data) {

            $scope.model.Plano = data;
          
            $scope.labels.operacao = 'Edição do plano';
            $scope.labels.botaoIncluir = 'Confirma Alteração';
            $scope.show.inclusao = true;
            $scope.abrirmodal('#ModalInclusao');

        };
        
        //////// MÉTODOS GENÉRICOS ///////////////////////////////

        $scope.abrirmodal = function (modalid) {
            $(modalid).modal('show');
        };

        $scope.fecharmodal = function (modalid) {
            $(modalid).modal('hide');
        };

    });
