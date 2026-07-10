//------------------------------------------------------------------------------
//  Creation date: 27/06/2017 20:13:48
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function ImageService($q, $http) {
    this.get = get;

    function get(relativePath) {
        return require('../../resources/' + relativePath);
    }

}

module.exports = function(appModule) {
    appModule.service('imageService', ImageService);
};