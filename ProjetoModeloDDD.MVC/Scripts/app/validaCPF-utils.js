(function () {

    var moduleName = 'validaCPF.Utils';

    angular.module(moduleName, [])
        .directive('cpfValido', validaCPF);

    function validaCPF() {

        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                scope.$watch(attrs.ngModel, function () {

                    if (elem[0].value.length == 0)
                        ctrl.$setValidity('cpfValido', true);
                    else if (elem[0].value.length < 11) {
                        //aplicar o algoritmo de validação completo do CPF
                        ctrl.$setValidity('cpfValido', false);
                    }
                    else {
                        var cpf = elem[0].value;

                        cpf = cpf.replace(/[^\d]+/g, '');
                        if (cpf == '') return ctrl.$setValidity('cpfValido', false);
                        // Elimina CPFs invalidos conhecidos    
                        if (cpf.length != 11 ||
                            cpf == "00000000000" ||
                            cpf == "11111111111" ||
                            cpf == "22222222222" ||
                            cpf == "33333333333" ||
                            cpf == "44444444444" ||
                            cpf == "55555555555" ||
                            cpf == "66666666666" ||
                            cpf == "77777777777" ||
                            cpf == "88888888888" ||
                            cpf == "99999999999")
                            return ctrl.$setValidity('cpfValido', false);
                        // Valida 1o digito 
                        add = 0;
                        for (i = 0; i < 9; i++)
                            add += parseInt(cpf.charAt(i)) * (10 - i);
                        rev = 11 - (add % 11);
                        if (rev == 10 || rev == 11)
                            rev = 0;
                        if (rev != parseInt(cpf.charAt(9)))
                            return ctrl.$setValidity('cpfValido', false);
                        // Valida 2o digito 
                        add = 0;
                        for (i = 0; i < 10; i++)
                            add += parseInt(cpf.charAt(i)) * (11 - i);
                        rev = 11 - (add % 11);
                        if (rev == 10 || rev == 11)
                            rev = 0;
                        if (rev != parseInt(cpf.charAt(10)))
                            return ctrl.$setValidity('cpfValido', false);
                        return ctrl.$setValidity('cpfValido', true);
                    }
                });
            }
        };

    }
    
    })();


