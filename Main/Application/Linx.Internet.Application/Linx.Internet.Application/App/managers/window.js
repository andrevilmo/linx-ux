define(['durandal/system', 'durandal/app', 'plugins/router', 'services/logger', 'knockout'],
    function (system, app, router, logger, ko) {

        //////////////////////
        // class: WindowItemVM
        //////////////////////
        var WindowItemVM = function (p) {
            var self = this;
            self.windowUID = p.windowUID;
            self.displayName = p.displayName;
            self.moduleName = p.moduleName;
            self.urlLink = p.urlLink;
            self.transactionCode = p.transactionCode;

        };

        return {
            WINDOWS_VM: ko.observableArray(),

            ///////////////////////
            // method: registerManager()
            ///////////////////////
            registerManager: function () {
                var that = this;

                router.on('router:navigation:complete').then(function (instance, instruction, router) {
                    //if (instruction.config.type != "transaction-report")
                    //    $('.preloading').css('overflow', 'auto');

                    $('body, html').scrollTop(0);

                    $('#main').scrollTop(0);

                    if (instruction.config.type != "transaction-assembly" && instruction.config.type != "transaction")
                        return;
                        
                    var item = new WindowItemVM({
                        windowUID: 0,
                        displayName: instruction.config.title,
                        moduleName: (instruction.config.currentData == null ? instruction.config.lxAssemblyName : instruction.config.currentData.ModuleDescription),
                        urlLink: "#" + instruction.config.route,
                        transactionCode: (instruction.config.currentData == null ? 'Cód: DEV' : 'Cód: ' + instruction.config.currentData.TransactionCode.trim())
                    });

                    var match = ko.utils.arrayFirst(that.WINDOWS_VM(), function (i) {
                        return i.urlLink === item.urlLink;
                    });

                    if (!match)
                        that.WINDOWS_VM.unshift(item);
                });
            },

            ///////////////////////
            // method: closeAll()
            ///////////////////////
            closeAll: function () {
                var that = this;
                app.trigger("shell:close:all");
                that.WINDOWS_VM.removeAll();
                router.navigate('#')
            },

            ///////////////////////
            // method: close()
            ///////////////////////
            close: function (id) {
                alert(id)
                var that = this;
                app.trigger("shell:close:all");
                that.WINDOWS_VM.removeAll();
                router.navigate('#')
            }
        };
    });
