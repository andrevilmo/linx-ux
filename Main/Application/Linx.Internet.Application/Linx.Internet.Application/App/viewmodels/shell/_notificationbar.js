define(['durandal/system', 'durandal/app', 'services/logger', 'managers/__auth', 'common', 'managers/message'],
    function (system, app, logger, managerAuth, common, managerMessage) {

        var activate = function () {
            return true;
        }

        var messageDismiss = function (idMensagem) {
            common.showProcess();
            managerMessage.dismiss(idMensagem).then(function () { common.closeProcess() });
        }

        var changeRead = function (mensagem) {
            if (mensagem.lida()) {
                managerMessage.markAsUnread(mensagem.idMensagem).then(function () {  });
            }
            else {
                managerMessage.markAsRead(mensagem.idMensagem).then(function () { });
            }
        }

        var openModal = function (mensagem) {
            managerMessage.showModalNotification(mensagem).then(function (success) { });
        }

        var refreshMessages = function () {
            common.showProcess();
            managerMessage.loadNewMessages(true).then(function () { common.closeProcess() });
        }

        var closeWindow = function () {
            var eleNotification = $('#notificationBarContent');
            eleNotification.toggle();
        }

        return {
            activate: activate,
            managerMessage: managerMessage,
            messageDismiss: messageDismiss,
            changeRead: changeRead,
            openModal: openModal,
            refreshMessages: refreshMessages,
            closeWindow: closeWindow
        };
    });
