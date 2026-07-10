define(['plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common'],
    function (dialog, ko, logger, managerAuth, common) {
        
        var vm = {
            EVENTS: ko.observableArray(),
            titleModal: ko.observable(''),

            // events
            UI_btnOK_Click: function () {
                dialog.close(this);
            },

            // events
            UI_btnMostrar_Click: function () {
                for (var i = 0; i < this.EVENTS().length ; i++)
                {
                    var item = this.EVENTS()[i];
                    item.visible(true);
                }
                
                $('#btnMostrar').css('display', 'none');
            },

            // events
            UI_btnMostrarDetalhesErros_Click: function (data, e) {
                var el = $(e.srcElement);
                if ($(el).hasClass("ico-adicao")) {
                    $(el).removeClass('ico-adicao');
                    $(el).addClass('ico-subtracao');

                    $(e.srcElement).parent().find("dd").removeClass('lista-oculta');
                }

                else {
                    $(el).addClass('ico-adicao');
                    $(el).removeClass('ico-subtracao');

                    $(e.srcElement).parent().find("dd").addClass('lista-oculta');
                }
            },

            ///////////////////////
            // method: DURANDAL: activate()
            ///////////////////////
            activate: function () {
            },

            /////////////////////// 
            // method: DURANDAL: activate()
            ///////////////////////
            canActivate: function () {
                return true;
            },

            ///////////////////////
            // method: DURANDAL: activate()
            ///////////////////////
            canDeactivate: function () {
                return true;
            },

            ///////////////////////
            // method: DURANDAL: compositionComplete()
            ///////////////////////
            compositionComplete: function () {
            },

            ///////////////////////
            // method: showModal()
            ///////////////////////
            showModal: function (events, title) {
                this.EVENTS = events;
                this.titleModal = ko.observable(title);

                return dialog.show(this);
            }
        }

        return vm;
    });