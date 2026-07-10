//------------------------------------------------------------------------------
//  Creation date: 14/06/2017 14:30:55
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

/* @ngInject */
function SidebarController($scope) {
    var vm = this;

    // Menus
    vm.sidebarMenuItems = [{
        'title': 'Favoritos',
        'iconClass': 'fa-star'
    }, {
        'title': 'Analytics',
        'iconClass': 'fa-bar-chart-o'
    }, {
        'title': 'Atendimento Vendas',
        'iconClass': 'fa-phone'
    }, {
        'title': 'Cadastro Base',
        'iconClass': 'fa-list-alt'
    }, {
        'title': 'Compras',
        'iconClass': 'fa-shopping-basket'
    }]
}

module.exports = SidebarController;