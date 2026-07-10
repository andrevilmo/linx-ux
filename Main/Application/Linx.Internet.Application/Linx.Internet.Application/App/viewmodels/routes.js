define(['durandal/system', 'durandal/app', 'services/logger', 'plugins/router', 'viewmodels/shared/modalMultimidia', 'viewmodels/shared/modalMultimidiaBatch', 'plugins/dialog', 'viewmodels/shared/modal', 'common'],
    function (system, app, logger, router, modalMultimidia, modalMultimidiaBatch, dialog, modal, common) {
        var selectedNodeText = ko.observable('');

        var vm = {
            activate: activate,
            router: router,
            compositionComplete: compositionComplete,
            showModalMultimidia1: showModalMultimidia1,
            showModalMultimidia2: showModalMultimidia2,
            showModalLarge: showModalLarge,
            showModalSmall: showModalSmall,
            showModalDefault: showModalDefault,
            showModalMultimidiaBatch: showModalMultimidiaBatch,
            count: 0,
            selectedNodeText: selectedNodeText
        };
        return vm;

        function activate() {
            //app.trigger('viewmodel:loadingComplete', vm);
            //alert(dialog.getNextZIndex())
            return true;
        }
        function compositionComplete() {
            
            //$('#selTree').editable({
            //    //inputclass: 'form-control input-large select2',
            //    //select2: {
            //    //    minimumResultsForSearch: -1,
            //    //    allowClear: true,
            //    //    formatResult: format,
            //    //    formatSelection: format,
            //    //    escapeMarkup: function (m) {
            //    //        return m;
            //    //    }
            //    //},

            //    xtree: {
            //        "core": {
            //            "themes": {
            //                "responsive": false
            //            },
            //            "data": [
            //         { "id": "a", "parent": "#", "text": "Simple root node" },
            //         { "id": "b", "parent": "#", "text": "Root node 2" },
            //         { "id": "c", "parent": "b", "text": "Child 1" },
            //         { "id": "d", "parent": "b", "text": "Child 2" },
            //            ],
            //        },
            //        "types": {
            //            "default": {
            //                "icon": "fa fa-folder icon-state-warning icon-lg"
            //            },
            //            "file": {
            //                "icon": "fa fa-file icon-state-warning icon-lg"
            //            }
            //        },
            //        "plugins": ["types"],
            //        'onSelectedNode': function (e, data) {
            //            if (data.node.children.length == 0) {
            //                selectedNodeText(data.instance.get_node(data.selected[0]).text);
            //            }
            //    },
            //    },
            //    //value: self.currentSettings.vm.currentBrands,
            //    url: '',
            //    //source: ,
            //    value: {
            //        //city: "Clique para a",
            //        //street: "Lenina",
            //        //building: "12"
            //    },
            //    type: 'tree',
            //    title: function (title) {
            //        return 'Título'; 
            //    },
            //    placement: 'left',
            //    onblur: 'submit',
            //    highlight: false,
            //    showbuttons: false,

            //    error: function (data) {
            //        alert(data);
            //    },
            //    //success: function (response, newValue) {
            //    //    alert(response);
            //    //    alert(newValue); 
            //    //},

            //    validate: function (value) {
            //        //if ($.trim(value) == '')
            //        //    return 'Seleção obrigatória!';
            //    },

            //    display: function (value) {

            //        value = selectedNodeText() == '' ? value : selectedNodeText();
            //        var html;
                    
            //        if (selectedNodeText() != '') {
            //            html = '<b> Nome do nó escolhido: ' + selectedNodeText() + '</b>';
            //        }

            //        else {
            //            html = '<b>' + 'Clique para abrir o TreeView' + '</b>'
            //        }

            //        $(this).html(html);
            //    }
            //});
         
            // general settings
            //$.fn.modal.defaults.spinner = $.fn.modalmanager.defaults.spinner =
            //  '<div class="loading-spinner" style="width: 200px; margin-left: -100px;">' +
            //    '<div class="progress progress-striped active">' +
            //      '<div class="progress-bar" style="width: 100%;"></div>' +
            //    '</div>' +
            //  '</div>';

            //$.fn.modalmanager.defaults.resize = true;

            var $modal = $('#ajax-modal');
            //var $modal = $('#full-width');

            $('#btnModal').on('click', function () {
                // create the backdrop and wait for next modal to be triggered
                $('body').modalmanager('loading');

                setTimeout(function () {
                    $modal.load('app/views/shared/_modal.html', '', function () {
                        $modal.modal();
                    });
                }, 1000);
            });

            common.showModalReport("#link");

            common.showModalReport("#link2");

            //$("#link").fancybox({
            //    //parent: '#applicationHost',
            //    //maxWidth: 800,
            //    //maxHeight: 600,
            //    fitToView: false,
            //    width: '95%',
            //    height: '95%',
            //    autoSize: false,
            //    openEffect: 'none',
            //    closeEffect: 'none',
            //    scrollOutside: 'false',
            //    iframe : {
            //        preload: true
            //        ,scrolling: 'false'
            //    },
            //    tpl: {
            //        closeBtn: '<a title="Close" class="fancybox-item fancybox-close" href="javascript:;" onclick="jQuery.fancybox.close()"></a>',
            //    },
            //    beforeLoad: function () {
            //        $("body").css("overflow", "")
            //        return;
            //    },
            //    afterShow: function () {
            //        $(".fancybox-inner").css("overflow", "")
            //    },
            //    afterClose: function () {
            //        $("body").css("overflow", "auto");
            //        return;
            //    }
            //});

            //$("#link2").fancybox({
            //    //parent: '#applicationHost',
            //    //maxWidth: 800,
            //    //maxHeight: 600,
            //    fitToView: false,
            //    width: '95%',
            //    height: '95%',
            //    autoSize: false,
            //    openEffect: 'none',
            //    closeEffect: 'none',
            //    scrollOutside: 'false',
            //    iframe: {
            //        preload: true
            //        , scrolling: 'false'
            //    }
            //    ,tpl: {
            //        closeBtn: '<a title="Close" class="fancybox-item fancybox-close" href="javascript:;" onclick="jQuery.fancybox.close()"></a>',
            //    }
            //    //modal: true
            //});
        };

        function showModalMultimidia1() {
            modalMultimidia.showModal("PRD_SKU_PRODUTO", 141083, null).then(function (response) {
                //alert(response)
            });
        }

        function showModalMultimidia2() {
            //modalMultimidia.showModal("PRD_SKU_PRODUTO", 116391, null);
            //vm.count++;

            modal.showModal("pkg_test-pessoasfw-bv-spa/viewmodels/FWPessoa001", vm, 'TESTE', ['Ok', 'Cancelar']).then(function (r, data) {
                //alert(r.dialogResult);
                //alert(r.parentDataContext);
            });
        }

        function showModalLarge() {

            //"ERRAUT002 - Acesso não permitido. 
            //O usuário não possui acesso a nenhum dos seguintes objetos : Linx.Repo01.BV / LinxRepo01ParametroValor / LinxRepo01ParametroValor/GetTcsParametroValorByEntitySearchNoAssociations. Bancos de dados : a-srv111.linx-inves.com.br-Portal_Development / a-srv111 Omni_Development. "

            //app.showMessage('Nenhum registro<BR>foi encontrado!', 'Informação', ['Ok']);
            app.showMessage('Nenhum registro\nfoi encontrado!', 'Informação', ['Ok']);
            app.showMessage('Nenhum registro\r\nfoi encontrado!', 'Informação', ['Ok']);
            //modal.showModal("pkg_test-pessoasfw-bv-spa/viewmodels/FWPessoa001", vm, 'TESTE', ['Ok', 'Cancelar'], 'large');
        }

        function showModalDefault() {
            modal.showModal("pkg_linx-vendas-bv-spa/viewmodels/CadClient", vm, 'cadclient', ['Ok', 'Cancelar']);
        }

        function showModalSmall() {
            modal.showModal("pkg_test-pessoasfw-bv-spa/viewmodels/FWPessoa001", vm, 'FWPessoa001', ['Ok', 'Cancelar'], 'small');
        }

        function showModalMultimidiaBatch() {
            //var modalMultimidiaBatch = require('viewmodels/shared/modalMultimidiaBatch');

            modalMultimidiaBatch.showModal("PRD_SKU_PRODUTO", 116391).then(function (r, data) {
            });

            //vm.count++;

            //modal.showModal("pkg_test-pessoasfw-bv-spa/viewmodels/FWPessoa001", vm, 'TESTE', ['Ok', 'Cancelar']).then(function (r, data) {
            //    //alert(r.dialogResult);
            //    //alert(r.parentDataContext);
            //});
        }
    });