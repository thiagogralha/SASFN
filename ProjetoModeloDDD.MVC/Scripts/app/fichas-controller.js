angular.module('FichasApp', ['ngAnimate', 'angularUtils.directives.dirPagination', 'validaCPF.Utils', 'ui.mask'])
    .controller('FichasCtrl',
    function ($scope, $http, $timeout) {

        $scope.model = {};
        $scope.model.Campanha = {};
        $scope.model.Dependentes = { pai: null };
        $scope.model.Titulos = {};
        $scope.show = { erro: false };
        $scope.labels = {};
        $scope.loader = false;
        $scope.loaderCEP = false;

        var ctrl = {

            ListarPlano: function () {

                $http.get('/Fichas/GetPlano')
                    .success(function (data) {
                        $scope.listaPlano = data;
                    });
            }
        };

        $scope.init = function () {

            $scope.loader = true;

            $http.get('/Fichas/GetFichas')
                .success(function (data) {
                    $scope.loader = false;
                    $scope.fichas = data;

                });
        };

        $scope.init();

        $scope.ordenar = function (keyname) {
            $scope.sortKey = keyname;
            $scope.reverse = !$scope.reverse;
        };

        $scope.confirmarInclusao = function () {

            $scope.loader = true;

            $http({
                method: 'POST',
                url: '/Fichas/InsertFicha',
                data: $scope.model.Campanha
            })
                .then(function (response) {

                    $scope.fecharmodal('#ModalInclusao');
                    $scope.init();
                    $scope.loader = false;
                    return response;
                },
                function (response) {
                    return response;
                });

        };

        //$scope.excluir = function (id) {

        //    if (confirm("Deseja realmente excluir essa ficha? ")) {

        //        $http.post('/Fichas/Excluir?id=' + id).success(function (response) {

        //            $scope.mensagem = response;
        //            $scope.init();

        //        }).error(function (error) {
        //            console.log(error);
        //        });
        //    }

        //};

        $scope.cancelarInclusao = function () {

            $scope.show.inclusao = false;
            $scope.show.erro = false;
            $scope.fecharmodal('#ModalInclusao');

        };

        $scope.abrirModalInclusao = function () {

            $scope.model.Campanha = {};
            ctrl.ListarPlano();
            $scope.labels.operacao = 'Inclusão da Ficha';
            $scope.labels.botaoIncluir = 'Incluir';
            $scope.show.inclusao = true;
            $scope.abrirmodal('#ModalInclusao');

        };

        $scope.abrirModalEdicao = function (data) {


            $scope.model.Campanha = data;
            $scope.model.Campanha.Plano = data.PlanoId ? $scope.listaPlano.find(function (x) { return x.PlanoId == data.PlanoId }) : null; //CARREGAR O VALUE DE UMA LISTA

            $scope.labels.operacao = 'Edição da Ficha';
            $scope.labels.botaoIncluir = 'Confirma Alteração';
            $scope.show.inclusao = true;
            $scope.abrirmodal('#ModalInclusao');

        };

        $scope.ObterEnderecoPorCEP = function (cep) {

            if (!cep) {
                return;
            }

            $scope.loaderCEP = true;

            $http.post("/Fichas/ConsultarEnderecoPorCEP?cep=" + cep).success(function (response) {

                if (!response) {
                    $scope.loaderCEP = false;
                    return;
                }

                $scope.model.Campanha.Cep = cep;
                $scope.model.Campanha.Endereco = response.Logradouro;
                $scope.model.Campanha.Bairro = response.Bairro;
                $scope.model.Campanha.Cidade = response.Cidade;
                $scope.model.Campanha.Estado = response.UF;

                $scope.loaderCEP = false;

            }).error(function (error) {
                console.log(error);
            })
        }

        ctrl.ListarPlano()

        ///////////// MÉTODOS PARA O DEPENDENTE ////////////////////


        $scope.excluirDependente = function (item) {

            //if (confirm("Deseja realmente excluir essa ficha? ")) {

            $http.post('/Dependentes/Excluir?id=' + item.DependenteId).success(function (response) {

                $scope.model.Dependentes.splice($scope.model.Dependentes.indexOf(item), 1);
                $scope.mensagem = response;
                //$scope.fecharmodal('#ModalVisualizarDependentes');
                //$scope.init();

            }).error(function (error) {
                console.log(error);
            })
            //}

        }

        $scope.abrirModalVisualizarDependentes = function (data, pai) {

            $scope.model.Dependentes = data;
            $scope.model.Dependentes.Pai = pai;
            $scope.abrirmodal('#ModalVisualizarDependentes');
        };

        $scope.abrirModalInclusaoDependentes = function (id) {

            $scope.model.Dependentes = { FichaId: id };
            $scope.labels.operacao = 'Inclusão de Dependentes';
            $scope.labels.botaoIncluir = 'Incluir';
            $scope.show.inclusao = true;
            $scope.abrirmodal('#ModalInclusaoDependentes');

        };

        $scope.confirmarInclusaoDependente = function () {

            $http({
                method: 'POST',
                url: '/Dependentes/InsertDependente',
                data: $scope.model.Dependentes
            })
                .then(function (response) {

                    $scope.fecharmodal('#ModalInclusaoDependentes');
                    $scope.init();
                    return response;
                },
                function (response) {
                    return response;
                });

        }

        $scope.cancelarInclusaoDependente = function () {

            $scope.show.inclusao = false;
            $scope.show.erro = false;
            $scope.model.Dependentes = {};
            $scope.fecharmodal('#ModalInclusaoDependentes');

        }

        $scope.abrirModalEdicaoDependente = function (data) {

            $scope.model.Dependentes = data;

            $scope.labels.operacao = 'Edição do Dependente';
            $scope.labels.botaoIncluir = 'Confirma Alteração';
            $scope.show.inclusao = true;
            $scope.fecharmodal('#ModalVisualizarDependentes');
            $scope.abrirmodal('#ModalInclusaoDependentes');

        };

        $scope.ObterEnderecoPorCEPDependentes = function (cep) {

            if (!cep) {
                return;
            }

            $scope.loaderCEP = true;

            $http.post("/Fichas/ConsultarEnderecoPorCEP?cep=" + cep).success(function (response) {

                if (!response) {
                    $scope.loaderCEP = false;
                    return;
                }

                $scope.model.Campanha.CepDep = cep;
                $scope.model.Dependentes.EnderecoDep = response.Logradouro;
                $scope.model.Dependentes.BairroDep = response.Bairro;
                $scope.model.Dependentes.CidadeDep = response.Cidade;
                $scope.model.Dependentes.EstadoDep = response.UF;

                $scope.loaderCEP = false;

            }).error(function (error) {
                console.log(error);
            })
        }


        //////////////// MÉTODOS PARA OS TÍTULOS ///////////////////

        $scope.abrirModalVisualizarTitulos = function (id) {

            $http({
                method: 'POST',
                url: '/Titulos/GetTitulosPorFicha?fichaId=' + id
            })
                .then(function (response) {
                    if (response.data.erro) {
                        $scope.show.erro = response.data.erro;
                        $scope.titulo = null;
                    }
                    else {
                        $scope.show.erro = null;
                        $scope.model.Titulos = response.data;
                    }
                },
                function (response) {
                    $scope.titulo = null;
                });


            //$scope.model.Titulos = data;

            $scope.abrirmodal('#ModalVisualizarTitulos');
        };

        $scope.confirmarAlteracao = function (titulo) {

            $http({
                method: 'POST',
                url: '/Titulos/InsertTitulo',
                data: titulo
            })
                .then(function (response) {

                    var data = new Date();
                    var dia = data.getDate();
                    if (dia.toString().length == 1)
                        dia = "0" + dia;
                    var mes = data.getMonth() + 1;
                    if (mes.toString().length == 1)
                        mes = "0" + mes;
                    var ano = data.getFullYear();

                    if (titulo.Status == "B") {
                        $scope.model.Titulos[$scope.model.Titulos.indexOf(titulo)].DataBaixa = dia + "/" + mes + "/" + ano;
                    }
                    else {
                        $scope.model.Titulos[$scope.model.Titulos.indexOf(titulo)].DataBaixa = "--";
                    }




                    if (titulo.Mes == 12) {
                        $scope.init();
                        $scope.fecharmodal('#ModalVisualizarTitulos');
                    }

                    return response;
                },
                function (response) {
                    return response;
                });

        }        
        //////// MÉTODOS GENÉRICOS ///////////////////////////////

        $scope.abrirmodal = function (modalid) {
            $(modalid).modal('show');
        };

        $scope.fecharmodal = function (modalid) {
            $(modalid).modal('hide');
        };

    });
