define(['durandal/app', 'plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common'],
    function (app, dialog, ko, logger, managerAuth, common) {

        var modalChangePassword = function (canClose) {
            var _this = this;

            this.canClose = ko.observable(canClose === true);
            this.mustReturnToLogin = ko.observable(requiresLogoffBeforePasswordChange(canClose));
            this._shouldReturnToLogin = this.mustReturnToLogin();
            this._redirectStarted = false;
            this._leaveBound = false;

            //Durandal Methods
            this.compositionComplete = function () {
                if (_this._shouldReturnToLogin) {
                    bindLeaveGuards(_this);
                }
            };

            this.detached = function () {
                unbindLeaveGuards(_this);
                if (_this._shouldReturnToLogin && !_this._redirectStarted) {
                    goToLogin(_this);
                }
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
                        var errorMessage = jqXHR.responseJSON && jqXHR.responseJSON.ExceptionMessage
                            ? jqXHR.responseJSON.ExceptionMessage
                            : 'Não foi possível alterar a senha.';
                        app.showMessage(errorMessage, 'Atenção', ['Ok']);
                    },

                    success: function (data) {
                        managerAuth.expiracao = false;
                        managerAuth.passwordChangeOnlyMode = false;
                        common.closeProcess('.modal-body');
                        // Do not wait for the success dialog Ok — closing the host window
                        // used to leave a blank shell with no click targets.
                        goToLogin(_this);
                    }
                });
            };

            this.cancel = function () {
                if (_this._shouldReturnToLogin) {
                    goToLogin(_this);
                    return;
                }
                dialog.close(_this);
            };

            this.voltarAoLogin = function () {
                goToLogin(_this);
            };
        };

        function goToLogin(vm) {
            if (goToLogin._started) {
                return;
            }
            goToLogin._started = true;
            if (vm) {
                vm._redirectStarted = true;
            }
            redirectToLoginAfterPasswordChange();
        }

        function bindLeaveGuards(vm) {
            if (vm._leaveBound) {
                return;
            }
            vm._onPageHide = function () {
                if (managerAuth.passwordChangeOnlyMode === true || managerAuth.expiracao === true) {
                    goToLogin(vm);
                }
            };
            window.addEventListener('pagehide', vm._onPageHide);
            vm._leaveBound = true;
        }

        function unbindLeaveGuards(vm) {
            if (!vm._leaveBound) {
                return;
            }
            if (vm._onPageHide) {
                window.removeEventListener('pagehide', vm._onPageHide);
            }
            vm._leaveBound = false;
        }

        function redirectToLoginAfterPasswordChange() {
            if (managerAuth.isLoginPOSUXMode) {
                $.ezstorage.remove('Hash_Login');
                window.location.href = window.location.origin + window.location.pathname;
                return;
            }

            try {
                $.sessionStorage.removeAll();
            } catch (e) {
            }
            window.location.href = managerAuth.buildRoot('logoff');
        }

        function requiresLogoffBeforePasswordChange(canClose) {
            return canClose === false || managerAuth.expiracao === true;
        }

        modalChangePassword.performLogoffForPasswordChange = function () {
            if (managerAuth.isLoginPOSUXMode) {
                $.ezstorage.remove('Hash_Login');
                return $.when();
            }

            try {
                $.sessionStorage.removeAll();
            } catch (e) {
            }

            return $.ajax({
                type: 'GET',
                globalError: false,
                url: managerAuth.buildRoot('LogoffForPasswordChange'),
                dataType: 'json',
                async: true,
                cache: false
            });
        };

        modalChangePassword.redirectToLogin = function () {
            goToLogin._started = false;
            redirectToLoginAfterPasswordChange();
        };

        modalChangePassword.show = function (canClose) {
            var showDialog = function () {
                goToLogin._started = false;
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
            }).fail(function () {
                // Avoid blank Durandal shell when the soft logoff call fails.
                modalChangePassword.redirectToLogin();
            });
        };

        return modalChangePassword;
    });
