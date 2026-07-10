//------------------------------------------------------------------------------
//  Creation date: 14/06/2017 14:20:47
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

/* @ngInject */
function NavbarController($scope) {
    var vm = this;

    vm.name = "Workarea";

    // Posicionamento do popover
    vm.placement = {
        selected: 'bottom'
    };
}

module.exports = NavbarController;