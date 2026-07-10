require.nodeRequire = window.nodeRequire

// shell
requirejs.config({
    enforceDefine: true,
    baseUrl: $('meta[name=linx-internet-application-root]').attr("content") + $('meta[name=linx-internet-application-module-id]').attr("content") + "/App",
    urlArgs: (window.nodeRequire ? "" : "appmode=" + document.getElementsByTagName('meta')[0].content),
    paths: {
        'text': '../lib/requirejs/text',
        'json': '../lib/requirejs/json',

        'durandal': '../lib/durandal',
        'plugins': '../lib/durandal/plugins',
        'transitions': '../lib/durandal/transitions',

        'jquery': '..//lib/telerik_kendoui/js/jquery-min',
        'knockout': '../lib/knockout/knockout-3-1-0-min',

        'breeze': '../lib/breeze/breeze',

        'jsSHA': '../lib/jsSHA/sha',

        'base32': '../lib/hi_base32/base32'
    },
    shim: {
        'base32': { exports: 'base32' }
    },
    waitSeconds: $('meta[name=linx-internet-application-requirejs-timeout]').attr("content")
});

//
// configuração para tratamento de erro sobre o require
//
//requirejs.onError = function (err) {
//    /* 
//        err has the same info as the errback callback:
//        err.requireType & err.requireModules
//    */
//    //app.trigger('shell:log', 'error', err.requireType, '');
//    //alert(err)

//    console.log(err);
//    var message = '';

//    if (err.requireType === 'timeout') {
//        message = 'Tempo limite excedido ao carregar o(s) arquivo(s): ' + err.requireModules + '.\r\nSerá necessário recarregar a pagina atual!';
//    }
//    else {
//        message = 'Erro ao carregar o(s) arquivo(s): ' + err.requireModules + '.\r\nSerá necessário recarregar a pagina atual!';
//    }

//    if (require.defined('durandal/app') == false) {
//        alert(message);

//        window.onbeforeunload = null;
//        window.location.reload();
//    }
//    else {
//        var app = require('durandal/app');

//        app.showMessage(message.replace("\r\n", "<BR><BR>"), 'Linx UX', ['Reiniciar', 'Cancelar']).then(function (dialogResult) {
//            if (dialogResult != "Cancelar") {
//                window.onbeforeunload = null;
//                window.location.reload();
//            }
//        });
//    }


//    //app.showMessage(err.requireType, 'Requisição inválida', ['Ok']);

//    //console.error(err.requireType);
//    // Be sure to rethrow if you don't want to
//    // blindly swallow exceptions here!!!
//};
