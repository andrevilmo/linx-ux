//------------------------------------------------------------------------------
//  Creation date: 21/06/2017 11:47:32
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function AdministratorController($scope, $state, $stateParams, imageService) {
    var vm = this;
    vm.title = $state.current.displayName;
    vm.init = init;
    vm.image = imageService;

    // Módulos
    vm.modules = [{
        'nameModule': 'Análise de inventário',
        'nameClass': 'fa-line-chart',
        'pathModule': 'Vendas'
    }, {
        'nameModule': 'Distribuição de meta por loja',
        'nameClass': 'fa-bar-chart',
        'pathModule': 'Manutenção'
    }, {
        'nameModule': 'Assistente de Promoção',
        'nameClass': 'fa-percent',
        'pathModule': 'Manutenção'
    }, {
        'nameModule': 'Veiculo de Ação de MKT',
        'nameClass': 'fa-users',
        'pathModule': 'Tabelas de apoio'
    }, {
        'nameModule': 'Consulta de cartão Presente',
        'nameClass': 'fa-gift',
        'pathModule': 'Consulta'
    }, {
        'nameModule': 'Objetivo da Ação de MKT',
        'nameClass': 'fa-users',
        'pathModule': 'Tabelas de apio'
    }];

    // $scope.tabs = [
    //     {
    //         title: 'Módulos',
    //         template: './templates/modulosTemplate.html',
    //         content: 'Dynamic content 1'

    //     },
    //     {
    //         title:'Relatórios',
    //         template: './templates/relatoriosTemplate.html',
    //         content:'Dynamic content 2'
    //     }
    // ];

    $scope.items = [
        'The first choice!',
        'And another choice for you.',
        'but wait! A third!'
    ];

    $scope.tab = 1;

    $scope.setTab = function(newTab) {
        $scope.tab = newTab;
    };

    $scope.isSet = function(tabNum) {
        return $scope.tab === tabNum;
    };

    function init() {}
}

module.exports = function(appModule) {
    appModule.controller('administratorController', AdministratorController);
};