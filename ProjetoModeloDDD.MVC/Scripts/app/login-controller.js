
angular.module('LoginApp', [])
    .controller('LoginCtrl', ['$scope', '$window', '$http', function ($scope, $window, $http) {

        $scope.model = {};
        $scope.loader = false;

        $scope.Logar = function () {

            $scope.loader = true; 

            $http.post('/Login/BuscarDetalhesFuncionario', $scope.model).success(function (data) {                            
                
                if (data == "Success") {
                    $scope.Mensagem = null;
                    $window.location.href = '/Home';
                    $scope.loader = false; 
                    return;
                }

                $scope.loader = false; 
                $scope.Mensagem = data;                
            });
        }; 
    }]);
