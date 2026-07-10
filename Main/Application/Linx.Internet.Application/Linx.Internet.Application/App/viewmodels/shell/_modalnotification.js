define(['durandal/app', 'plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common', 'managers/message'],
    function (app, dialog, ko, logger, managerAuth, common, managerMessage) {

        var modalNotification = function (message) {
            var _this = this;

            var canClose = true;
            var moveToExternalUI = false; //Indica se a tela atual será redirecionada a uma nova URL externa, fechando a atual
            var openExternalUI = false; //Indica se uma URL externa será aberta dentro do modal
            
            if (typeof message.Obrigatoria === 'boolean' && message.tipoConteudo == "text-xuri") {
                canClose = (message.Obrigatoria == false); //Se a mensagem NÃO for obrigatória, exibe botões de fechamento do modal
                moveToExternalUI = (message.Obrigatoria == true); //Se a mensagem for obrigatória, redireciona para a página da URL da mensagem
                openExternalUI = (message.Obrigatoria == false); //Se a mensagem NÃO for obrigatória, abre Iframe no modal

                if (message.corpo?.toLowerCase().includes('/respondida')) {
                    moveToExternalUI = false; //Não permite que seja redirecionado para uma nova URL caso seja de uma pesquisa já respondida
                }
            }            

            this.canClose = ko.observable(canClose); //Observável criado para controlar a exibição dos botões de fechamento do modal no HTML (ko if: canClose())
            this.openExternalUI = ko.observable(openExternalUI); //Observável criado para controlar a exibição do Iframe no HTML (ko if: openExternalUI())

            this.message = message;

            this.compositionComplete = function () {
                if (moveToExternalUI == true) { //Fluxo para redirecionar para outra URL
                    window.ignoreCloseConfirmation == true; //Variável global para ignorar mensagem de confirmação de fechamento de página na function setWindowMessage do common.js
                    window.location.href = message.corpo; //Redireciona a página para a URL do corpo da mensagem
                }
                else if (openExternalUI == true) { //Fluxo para abrir Iframe no modal
                    $('#externalFrame').attr('src', message.corpo); //Abre o Iframe com a URL do corpo da mensagem
                }
                else {  //Fluxo para abrir HTML no modal
                    $('#divContent').html(message.corpo); //Abre o HTML do corpo da mensagem  no modal
                }
            };

            this.activate = function () {
            };

            //buttons
            this.ok = function () {
                dialog.close(this);
            };

            this.cancel = function () {
                dialog.close(this);
            }
        };

        modalNotification.show = function (message) {
            return dialog.show(new modalNotification(message));
        };

        return modalNotification;
    });

