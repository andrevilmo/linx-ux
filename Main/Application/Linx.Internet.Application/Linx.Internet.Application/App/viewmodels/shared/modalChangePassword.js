define(['durandal/app', 'plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common'],
    function (app, dialog, ko, logger, managerAuth, common) {

        var modalChangePassword = function (canClose) {
            var _this = this;

            this.canClose = ko.observable(canClose);

            //Durandal Methods
            this.compositionComplete = function () {

            };

            this.activate = function () {
            };

            //buttons
            this.ok = function () {

                var requiredMessage = "Campo obrigatório";
                var regexMessage = "A senha deve ter no mínimo 12 caracteres, conter ao menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial permitido";
                var sequentialNumbersMessage = "A senha não pode conter números em sequência (ex.: 123 ou 987)";
                var regexExpression = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[~!@#$%^&*+\-\/\.,\\{}\[\]();:?<>"'_])[A-Za-z\d~!@#$%^&*+\-\/\.,\\{}\[\]();:?<>"'_]{12,}$/;

                var hasSequentialNumbers = function (value, sequenceLength) {
                    sequenceLength = sequenceLength || 3;
                    if (!value || value.length < sequenceLength) {
                        return false;
                    }
                    for (var i = 0; i <= value.length - sequenceLength; i++) {
                        var slice = value.substring(i, i + sequenceLength);
                        if (!/^\d+$/.test(slice)) {
                            continue;
                        }
                        var ascending = true;
                        var descending = true;
                        for (var j = 1; j < slice.length; j++) {
                            var diff = slice.charCodeAt(j) - slice.charCodeAt(j - 1);
                            if (diff !== 1) {
                                ascending = false;
                            }
                            if (diff !== -1) {
                                descending = false;
                            }
                        }
                        if (ascending || descending) {
                            return true;
                        }
                    }
                    return false;
                };

                jQuery.validator.addMethod("regex", function (value, element, regexp) {
                    var re = new RegExp(regexp);
                    return this.optional(element) || re.test(value);
                },
                    regexMessage
                );

                jQuery.validator.addMethod("noSequentialNumbers", function (value, element) {
                    return this.optional(element) || !hasSequentialNumbers(value);
                },
                    sequentialNumbersMessage
                );

                var validator = $("#formPassword").validate({
                    errorElement: 'span',
                    errorClass: 'help-block help-block-error',
                    focusInvalid: false,
                    ignore: "",
                    rules: {
                        senhaAtual: {
                            required: true,
                        },
                        novaSenha: {
                            required: true,
                            regex: regexExpression,
                            noSequentialNumbers: true
                        },
                        confirmacao: {
                            required: true,
                            equalTo: "#novaSenha"
                        },
                    },
                    messages: {
                        senhaAtual: {
                            required: requiredMessage,
                            regex: regexMessage
                        },
                        novaSenha: {
                            required: requiredMessage,
                            regex: regexMessage,
                            noSequentialNumbers: sequentialNumbersMessage
                        },
                        confirmacao: {
                            required: requiredMessage,
                            equalTo: "Senha não compatível"
                        }
                    },

                    invalidHandler: function (event, validator) {
                    },

                    errorPlacement: function (error, element) {
                        var icon = $(element).parent('.input-icon').children('i');
                        icon.removeClass('fa-check').addClass("fa-warning");
                        icon.attr("data-original-title", error.text()).tooltip({ 'container': 'body' });
                    },

                    highlight: function (element) {
                        $(element).closest('.form-group').removeClass("has-success").addClass('has-error');
                        var span = $('#span_' + element.id);
                        $(span).css("visibility", "visible").css("display", "");
                    },

                    unhighlight: function (element) {
                    },

                    success: function (label, element) {
                        var icon = $(element).parent('.input-icon').children('i');
                        $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
                        icon.removeClass("fa-warning").addClass("fa-check");
                        var span = $('#span_' + element.id);
                        $(span).css("visibility", "hidden").css("display", "none");

                    },

                    submitHandler: function (form) {
                    }

                });

                validator.form();

                if (!validator.valid())
                    return;

                var oldPassword = $('#senhaAtual').val();
                var newPassword = $('#novaSenha').val();

                common.showProcess('.modal-body');

                $.ajax({
                    type: 'GET',
                    messageUser: "Alteração de senha de usuário",
                    headers: managerAuth.getHeaders(managerAuth.loginInfo.IdTcsAmbienteDefault),
                    url: managerAuth.getServiceAddress('LinxFrameworkAutorizacao', 'Linx.Framework.BV') + '/ChangeUserPassword',
                    data: {
                        userUid: managerAuth.loginInfo.UidUsuario,
                        oldPassword: oldPassword,
                        newPassword: newPassword 
                    },
                    dataType: 'json',
                    async: true,
                    cache: false,

                    error: function (jqXHR, textStatus, errorThrown) {
                        common.closeProcess('.modal-body');
                        var errorMessage = jqXHR.responseJSON.ExceptionMessage;
                        app.showMessage(errorMessage, 'Atenção', ['Ok']);
                    },

                    success: function (data) {

                        managerAuth.expiracao = false;
                        managerAuth.passwordChangeOnlyMode = false;

                        $.ajax({
                            type: 'POST',
                            messageUser: "Alteração de senha de usuário",
                            url: managerAuth.buildRoot('UpdateExpiration'),
                            dataType: 'json',
                            async: true,
                            cache: false,

                            error: function (jqXHR, textStatus, errorThrown) {
                                common.closeProcess('.modal-body');
                                _this.cancel();
                            },

                            success: function (data) {
                                common.closeProcess('.modal-body');
                                app.showMessage("Senha alterada com sucesso.", 'Atenção', ['Ok']);
                                _this.cancel();
                            }
                        });

                    }
                });
            };

            this.cancel = function () {
                dialog.close(this);
            }
        };

        function requiresLogoffBeforePasswordChange(canClose) {
            return canClose === false || managerAuth.expiracao === true;
        }

        modalChangePassword.performLogoffForPasswordChange = function () {
            if (managerAuth.isLoginPOSUXMode) {
                $.ezstorage.remove('Hash_Login');
                return $.when();
            }

            $.sessionStorage.removeAll();

            return $.ajax({
                type: 'GET',
                globalError: false,
                url: managerAuth.buildRoot('LogoffForPasswordChange'),
                dataType: 'json',
                async: true,
                cache: false
            });
        };

        modalChangePassword.show = function (canClose) {
            var showDialog = function () {
                return dialog.show(new modalChangePassword(canClose));
            };

            if (!requiresLogoffBeforePasswordChange(canClose)) {
                return showDialog();
            }

            if (managerAuth.passwordChangeOnlyMode === true) {
                return showDialog();
            }

            return modalChangePassword.performLogoffForPasswordChange().then(function () {
                managerAuth.passwordChangeOnlyMode = true;
                return showDialog();
            });
        };

        return modalChangePassword;
    });

