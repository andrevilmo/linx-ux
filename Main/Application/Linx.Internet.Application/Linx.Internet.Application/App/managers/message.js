define(['durandal/system', 'durandal/app', 'services/logger', 'managers/__auth', 'common', 'knockout', 'viewmodels/shell/_modalnotification'],
    function (system, app, logger, managerAuth, common, ko, modalNotification) {

        var _self = this;
        var unread = ko.observable(0);
        var messages = ko.observableArray();
        var environmentInfo = [];
        var messageItem = function () {
            this.idMensagem = 0;
            this.titulo = "";
            this.corpo = "";
            this.entregue = "";
            this.tipoConteudo = "";
            this.Silenciosa = true;
            this.Obrigatoria = false;
            this.DataInicio = "";
            this.DataTermino = "";
            this.Codigo = "";
            this.lida = ko.observable(false);
        }

        var start = function () {
            if (!managerAuth.messageServiceEnabled) {
                return;
            }

            if (managerAuth.isShellProdMode && !managerAuth.loginInfo.IsSupportMode && !managerAuth.isLoginPOSUXMode) {
                startTimer();
            }
            loadMessages();
            loadNotificacoes();
        }

        var getEnvironmentInfo = function () {
            if (environmentInfo.length == 0) {
                for (var i = 0; i < managerAuth.loginInfo.Ambientes.length; i++) {
                    var item = managerAuth.loginInfo.Ambientes[i];
                    environmentInfo.push({ Hash: '', EnvironmentId: item.IdTcsAmbiente, ApplicationUid: item.UidAplicacao, CompanyUid: item.UidEmpresa, AplicativeId: item.IdTcsAplicativo });
                }
            }
            return environmentInfo;
        }

        var timer;

        var startTimer = function () {
            timer = setInterval(function () {
                loadNewMessages();
            }, managerAuth.messageCheckInterval);
        }

        var cancelTimer = function () {
            if (timer) {
                clearInterval(timer);
            }
        }

        var loadNewMessages = function (force) {

            if (managerAuth.messageCheckInterval == 0 && !force) {
                cancelTimer();
            }

            var dfd = $.Deferred();
            var environmentInfo = getEnvironmentInfo();

            return $.ajax({
                type: 'GET',
                message: "Buscando novas mensagens.",
                messageUser: "Buscando novas mensagens.",
                headers: managerAuth.getHeaders(),
                globalError: false,
                url: managerAuth.getServiceAddress('LinxAdmManagerEqualsENotifica', 'Linx.Adm.Manager.BV') + '/GetNewMessages',
                data: JSON.stringify(environmentInfo),
                contentType: "application/json",
                async: true,
                cache: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    cancelTimer();
                    dfd.resolve(null);
                },
                success: function (data, textStatus, response) {

                    if (data.length > 0) {

                        toastr.options = {
                            "closeButton": true,
                            "debug": false,
                            "newestOnTop": true,
                            "progressBar": false,
                            "positionClass": "toast-top-right",
                            "preventDuplicates": false,
                            "showDuration": "300",
                            "hideDuration": "1000",
                            "timeOut": "15000",
                            "extendedTimeOut": "1000",
                            "showEasing": "swing",
                            "hideEasing": "linear",
                            "showMethod": "fadeIn",
                            "hideMethod": "fadeOut"
                        }

                        for (var i = 0; i < data.length; i++) {
                            var messageType = data[i].TipoMensagem;
                            var newMessage = new messageItem()
                            newMessage.idMensagem = data[i].IdTcsMensagemLog;
                            newMessage.titulo = data[i].Titulo;
                            newMessage.corpo = data[i].Corpo;
                            newMessage.lida = ko.observable(data[i].Lida);
                            newMessage.entregue = data[i].entregue;
                            newMessage.Obrigatoria = data[i].Obrigatoria;
                            newMessage.Silenciosa = data[i].Silenciosa;
                            newMessage.tipoConteudo = data[i].TipoConteudo;
                            toastr[messageType](data[i].Corpo, data[i].Titulo, { onclick: function () { showModalNotification(newMessage) } });
                        }
                        loadMessages();

                        dfd.resolve();
                    }
                }
            });
            dfd.promise();
        };

        var loadNotificacoes = function () {
            var dfd = $.Deferred();
            var environmentInfo = getEnvironmentInfo();

            return $.ajax({
                type: 'GET',
                message: "Buscando Notificacoes.",
                messageUser: "Buscando Notificacoes.",
                headers: managerAuth.getHeaders(),
                globalError: false,
                url: managerAuth.getServiceAddress('LinxAdmManagerEqualsENotifica', 'Linx.Adm.Manager.BV') + '/ObterNotificacoesPendentesEProcessar',
                data: JSON.stringify(environmentInfo),
                contentType: "application/json",
                async: true,
                cache: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    dfd.resolve(null);
                },
                success: function (data, textStatus, response) {
                    //messages.removeAll();
                    console.log('dados: ' + data);

                    var newMessage = new messageItem();

                    if (data.Data != null && data.Sucesso) {
                        let code = data.Data.Codigo;

                        newMessage.idMensagem = data.Data.IdTcsMensagemLog;
                        newMessage.titulo = data.Data.Resumo;
                        newMessage.corpo = data.Data.Conteudo;
                        newMessage.lida = ko.observable(true);
                        newMessage.tipoConteudo = data.Data.tipo_conteudo;
                        newMessage.Obrigatoria = data.Data.Obrigatoria;
                        newMessage.Silenciosa = data.Data.Silenciosa;
                        newMessage.entregue = new Date();
                        // messages.push(newMessage);

                        showModalNotification(newMessage);

                        if (newMessage.tipoConteudo == "text-xuri" && newMessage.Obrigatoria && !newMessage.Silenciosa)
                            dismiss(newMessage.idMensagem);
                    }

                    updateUnread();
                    dfd.resolve();
                }
            });
            dfd.promise();
        };

        var loadMessages = function () {
            var dfd = $.Deferred();
            var environmentInfo = getEnvironmentInfo();

            return $.ajax({
                type: 'GET',
                message: "Buscando mensagens.",
                messageUser: "Buscando mensagens.",
                headers: managerAuth.getHeaders(),
                globalError: false,
                url: managerAuth.getServiceAddress('LinxAdmManagerEqualsENotifica', 'Linx.Adm.Manager.BV') + '/GetMessages',
                data: JSON.stringify(environmentInfo),
                contentType: "application/json",
                async: true,
                cache: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    dfd.resolve(null);
                },
                success: function (data, textStatus, response) {
                    messages.removeAll();

                    for (var i = 0; i < data.length; i++) {
                        var newMessage = new messageItem()
                        newMessage.idMensagem = data[i].IdTcsMensagemLog;
                        newMessage.titulo = data[i].Titulo;
                        newMessage.corpo = data[i].Corpo;
                        newMessage.lida = ko.observable(data[i].Lida);
                        newMessage.entregue = data[i].entregue;
                        newMessage.Obrigatoria = data[i].Obrigatoria;
                        newMessage.Silenciosa = data[i].Silenciosa;
                        newMessage.tipoConteudo = data[i].TipoConteudo;
                        //newMessage.idMensagem = data[i].IdTcsMensagemLog;
                        //newMessage.titulo = data[i].Titulo;
                        //newMessage.corpo = data[i].Corpo;
                        //newMessage.lida = ko.observable(data[i].Lida);
                        //newMessage.entregue = data[i].entregue;
                        messages.push(newMessage);
                    }
                    updateUnread();
                    dfd.resolve();
                }
            });
            dfd.promise();
        };

        var markAsRead = function (idMensagem) {
            var dfd = $.Deferred();
            var environmentInfo = getEnvironmentInfo();

            return $.ajax({
                type: 'POST',
                message: "Atualizando mensagem",
                messageUser: "Atualizando mensagem",
                headers: managerAuth.getHeaders(),
                globalError: false,
                url: managerAuth.getServiceAddress('LinxFrameworkMensagem', 'Linx.Framework.BV') + '/MarkMessageAsRead?messageId=' + idMensagem,
                contentType: "application/json",
                async: true,
                cache: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    dfd.resolve(null);
                },
                success: function (data, textStatus, response) {
                    var mensagem = $.grep(messages(), function (element, index) { return element.idMensagem == idMensagem });
                    if (mensagem.length > 0) {
                        mensagem[0].lida(true);
                        updateUnread();
                    }
                    dfd.resolve();
                }
            });
            dfd.promise();
        }

        var markAsUnread = function (idMensagem) {
            var dfd = $.Deferred();
            var environmentInfo = getEnvironmentInfo();

            return $.ajax({
                type: 'POST',
                message: "Atualizando mensagem",
                messageUser: "Atualizando mensagem",
                headers: managerAuth.getHeaders(),
                globalError: false,
                url: managerAuth.getServiceAddress('LinxFrameworkMensagem', 'Linx.Framework.BV') + '/MarkMessageAsUnread?messageId=' + idMensagem,
                contentType: "application/json",
                async: true,
                cache: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    dfd.resolve(null);
                },
                success: function (data, textStatus, response) {
                    var mensagem = $.grep(messages(), function (element, index) { return element.idMensagem == idMensagem });
                    if (mensagem.length > 0) {
                        mensagem[0].lida(false);
                        updateUnread();
                    }
                    dfd.resolve();
                }
            });
            dfd.promise();
        }

        var dismiss = function (idMensagem) {
            var dfd = $.Deferred();
            var environmentInfo = getEnvironmentInfo();

            return $.ajax({
                type: 'POST',
                message: "Atualizando mensagem",
                messageUser: "Atualizando mensagem",
                headers: managerAuth.getHeaders(),
                globalError: false,
                url: managerAuth.getServiceAddress('LinxFrameworkMensagem', 'Linx.Framework.BV') + '/DismissMessage?messageId=' + idMensagem,
                contentType: "application/json",
                async: true,
                cache: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    dfd.resolve(null);
                },
                success: function (data, textStatus, response) {
                    messages.remove(function (item) { return item.idMensagem == idMensagem });
                    updateUnread();
                    dfd.resolve();
                }
            });
            dfd.promise();
        }

        var updateUnread = function () {
            unread($.grep(messages(), function (element, index) { return !element.lida() }).length);
        }

        var showModalNotification = function (mensagem) {
            return markAsRead(mensagem.idMensagem).then(modalNotification.show(mensagem).then(function (success) { }));
        }

        var showModalNotificationWithoutMarkAsRead = function (mensagem) {
            return modalNotification.show(mensagem).then(function (success) { });
        }

        return {
            messages: messages,
            unread: unread,
            loadMessages: loadMessages,
            loadNewMessages: loadNewMessages,
            markAsRead: markAsRead,
            markAsUnread: markAsUnread,
            dismiss: dismiss,
            showModalNotification: showModalNotification,
            start: start
        };
    });