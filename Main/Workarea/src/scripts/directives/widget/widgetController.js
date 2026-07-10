//------------------------------------------------------------------------------
//  Creation date: 13/07/2017 14:18:49
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

/* @ngInject */
function WidgetController($scope) {
    var vm = this;

    vm.widgets = [{
        'name': 'News',
        'icon': 'newspaper-o'
    }, {
        'name': 'To Do List',
        'icon': 'pencil-square-o'
    }]
}

module.exports = WidgetController;