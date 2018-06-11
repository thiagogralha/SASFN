angular.module('FuncionariosApp', ['ngAnimate', 'angularUtils.directives.dirPagination', 'ui.mask'])
    .controller('FuncionariosCtrl',
    function ($scope, $http) {

        $scope.model = {};
        $scope.model.Funcionario = {};
        $scope.show = { erro: false };
        $scope.labels = {};
        
        $scope.init = function () {

            $http.get('/Funcionarios/GetFuncionarios')
                .success(function (data) {
                    $scope.funcionarios = data;
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
                url: '/Funcionarios/InsertFuncionario',
                data: $scope.model.Funcionario
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

            if (confirm("Deseja realmente excluir esse funcionário? ")) {

                $http.post('/Funcionarios/Excluir?id=' + id).success(function (response) {

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

            $scope.model.Funcionario = {};
            $scope.labels.operacao = 'Inclusão do Funcionário';
            $scope.labels.botaoIncluir = 'Incluir';
            $scope.show.inclusao = true;
            $scope.abrirmodal('#ModalInclusao');

        };

        $scope.abrirModalEdicao = function (data) {

            $scope.model.Funcionario = data;
          
            $scope.labels.operacao = 'Edição do Funcionário';
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
