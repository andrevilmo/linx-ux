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
                var regexMessage = "A senha deve ter no mínimo 7 e no máximo 50 caracteres e conter ao menos um caracter especial";
                var regexExpression = /(?=.*[^a-zA-Z0-9])(?=.*[a-zA-Z0-9]).{7,50}/g;

                jQuery.validator.addMethod("regex", function (value, element, regexp) {
                    var re = new RegExp(regexp);
                    return this.optional(element) || re.test(value);
                },
                    regexMessage
                );

                var validator = $("#formPassword").validate({
                    errorElement: 'span',
                    errorClass: 'help-block help-block-error',
                    focusInvalid: false,
                    ignore: "",
                    rules: {
                        senhaAtual: {
                            required: true,
                            regex: regexExpression
                        },
                        novaSenha: {
                            required: true,
                            regex: regexExpression
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
                            regex: regexMessage
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
                    url: managerAuth.getServiceAddress('LinxFrameworkAutorizacao/ChangeUserPassword'),
                    data: {
                        userUid: managerAuth.userId,
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
                        common.closeProcess('.modal-body');
                        app.showMessage("Senha alterada com sucesso.", 'Atenção', ['Ok']);
                        managerAuth.expiracao = false;
                        _this.cancel();
                    }
                });
            };

            this.cancel = function () {
                dialog.close(this);
            }
        };

        modalChangePassword.show = function (canClose) {
            return dialog.show(new modalChangePassword(canClose));
        };

        return modalChangePassword;
    });

