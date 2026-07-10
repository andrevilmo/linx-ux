define(['services/logger', 'durandal/app', 'knockout', 'managers/brand', 'managers/__auth', 'plugins/router'],
    function (logger, app, ko, managerBrand, managerAuth, router) {
        //////////////////////
        // class: MenuItemVM
        //////////////////////
        var ReportItemVM = function (p) {
            var self = this;
            self.urlLink = p.urlLink;
            self.htmlLink = p.htmlLink;
            self.displayName = p.displayName;
        };


        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            // KO bindings
            var self = this;
            this.currentSettings;
            this.dataToolbar;
            this.hasReports = ko.observable(false);
            this.hasOneReport = ko.observable(false);
            this.firstReportLink = ko.observable('');
            this.REPORTS_VM = ko.observableArray();

            // UI events
            self.UI_btnReport_Click = function (currentReportVM) {
                self.dataToolbar.print();
                var queryString = '', queryTranslation = '';
                if (self.currentSettings.vm.status() == 'Q') {
                    queryString = encodeURIComponent(self.currentSettings.vm.lastJEntitySearch());
                    queryTranslation = encodeURIComponent(self.currentSettings.vm.getTranslatedFilter());
                }
                else {
                    if (typeof self.currentSettings.vm.getQueryFilter === 'function') {
                        queryString = encodeURIComponent(self.currentSettings.vm.getQueryFilter());
                        queryTranslation = encodeURIComponent(self.currentSettings.vm.getTranslatedFilter());
                    }
                }

                //OnReporting event
                if (currentReportVM.urlLink.length > 0 && (typeof self.currentSettings.vm.OnReporting === 'function')) {
                    var reportName = strRight(currentReportVM.urlLink, currentReportVM.urlLink.length - 1);
                    var reportFilter = self.currentSettings.vm.OnReporting(reportName);
                    if (!isNullOrEmpty(reportFilter)) {
                        if (reportFilter === 'Error')
                            return;
                        else
                            queryString += encodeURIComponent(reportFilter);
                    }
                }
                router.navigate(currentReportVM.urlLink + '?filter=' + queryString + '&translation=' + queryTranslation + '&back=' + router.activeInstruction().config.hash);
            };

            // Method: activate()
            this.activate = function (settings) {
                //vm.getJExpression(vm.currentDataItem())
                self.currentSettings = settings;
                self.dataToolbar = settings.vm.dataToolbar
                var reportsVM = [];
                for (var i = 0; i < router.routes.length; i++) {
                    var record = router.routes[i];

                    if (record.type != "transaction-report" || record.currentData == null)
                        continue;

                    if (record.currentData.NomeRelatorio.indexOf(settings.vm.controllerName) > -1 || record.currentData.NomeRelatorio.indexOf(settings.vm.rootNamespace + "." + settings.vm.rootDataTypeName) > -1 || ((typeof settings.vm.isReportComposition === 'function') && settings.vm.isReportComposition(record.currentData.NomeRelatorio))) {
                        reportsVM.push(new ReportItemVM({
                            displayName: record.title,
                            htmlLink: "<i class=\"fa fa-print\"></i>" + record.title,
                            urlLink: record.hash
                        }));

                        //break;
                    }
                }

                if (reportsVM.length > 0) {
                    self.hasReports(true);
                    //self.hasOneReport((reportsVM.length == 1));
                    //self.firstReportLink(reportsVM[0].urlLink);
                    self.REPORTS_VM(reportsVM);
                }
            };

            // Method: compositionComplete()
            this.compositionComplete = function () {
                if ($("#menu").length == 0) {
                    // Passando uma CLASS e criando um novo ELEMENTO via JS.
                    $('#screen-meta-links').addClass('js').before('<div id="menu" class="pull-right"><i class="fa fa-ellipsis-h fa-2x"></i></div>');

                    // Comportamento após clicar no ELEMENTO criando anteriormente.
                    $("#menu").on('click', function () {

                        // Abre DATATOOLBAR
                        $("#screen-meta-links").toggle('slow');
                    });

                    // Remove o ATRIBUTO no evento RESIZE da tela
                    $(window).resize(function () {
                        $('#screen-meta-links').removeAttr('style');
                    });
                }
            };
        };

        return VM;
    });
