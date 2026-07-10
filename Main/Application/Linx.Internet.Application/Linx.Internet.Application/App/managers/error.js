define(['durandal/system', 'durandal/app', 'plugins/router', 'knockout', 'viewmodels/shared/modalConsole'],
    function (system, app, router, ko, modalConsole) {

        //////////////////////
        // class: EventItemVM
        //////////////////////
        var EventItemVM = function (p) {
            var self = this;
            self.type = p.type; // ['log', 'info', 'warn', 'error']
            self.displayName = p.displayName;
            self.displayStack = p.displayStack;
            self.displayStackVisible = ko.observable(false);
            self.visible = ko.observable(false);
            self.className = p.className;

        };

        return {
            EVENTS_ERROR_VM: ko.observableArray(),
            EVENTS_WARN_VM: ko.observableArray(),

            ///////////////////////
            // method: registerManager()
            ///////////////////////
            registerManager: function () {
                var that = this;

                app.on('shell:log').then(function (type, displayName, displayStack) {
                    var item = new EventItemVM({
                        type: type,
                        displayName: displayName,
                        displayStack: (displayStack.indexOf('at ') > -1 ? displayStack.split('at ') : new Array()),
                    });

                    if (type === 'error') {
                        item.className = "todo-tasklist-item todo-tasklist-item-border-red";
                        item.visible = ko.observable((that.EVENTS_ERROR_VM().length == 0));
                        that.EVENTS_ERROR_VM.push(item);
                        that.showEvents('error');
                    }
                    else {
                        item.className = "todo-tasklist-item todo-tasklist-item-border-orange";
                        item.visible = ko.observable((that.EVENTS_WARN_VM().length == 0));
                        that.EVENTS_WARN_VM.unshift(item);
                    }
                });

            },

            ///////////////////////
            // method: cleanEvents()
            ///////////////////////
            cleanEvents: function () {
                var that = this;
                that.EVENTS_ERROR_VM.removeAll();
                that.EVENTS_WARN_VM.removeAll();
            },

            ///////////////////////
            // method: showEvents()
            ///////////////////////
            showEvents: function (type) {
                var that = this;
                var first = null;

                if (type == 'error') {
                    first = that.EVENTS_ERROR_VM()[0];

                    if (first == null)
                        return;
                    modalConsole.showModal(that.EVENTS_ERROR_VM, 'Erro(s)').then(function (dialogResult) {
                        that.EVENTS_ERROR_VM.removeAll();
                    });

                    //app.showMessage(first.displayName, 'Script inválido', ['Ok']).then(function (dialogResult) {
                    //    that.EVENTS_ERROR_VM.removeAll();
                    //});
                }
                else {
                    first = that.EVENTS_WARN_VM()[0];

                    if (first == null)
                        return;
                    modalConsole.showModal(that.EVENTS_WARN_VM, 'Alerta(s)').then(function (dialogResult) {
                        that.EVENTS_WARN_VM.removeAll();
                    });

                    //app.showMessage(first.displayName, 'Alerta', ['Ok']).then(function (dialogResult) {
                    //    that.EVENTS_WARN_VM.removeAll();
                    //});
                }
            }

        };
    });
