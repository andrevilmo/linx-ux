define(['durandal/app', 'durandal/system', 'knockout', 'plugins/router', 'common', 'managers/__auth', 'managers/user'],
    function (app, system, ko, router, common, managerAuth, managerUser) {
        //////////////////////
        // class: QueryStringItem
        //////////////////////
        var QueryStringItem = function (p) {
            var self = this;
            self.filter = p.filter;
            self.translation = p.translation;
        };

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            this.back = null;

            var queryString = null;

            // Method: activate()
            this.activate = function (context) {
                if (isNullOrEmpty(context)) {
                    queryString = new QueryStringItem({ filter: '', translation: '' });
                    this.back = '#';
                }
                else {
                    queryString = new QueryStringItem({ filter: context.filter, translation: context.translation });
                    this.back = isNullOrEmpty(context.back) ? '#' : context.back;
                }

            };

            this.binding = function () {
                return { cacheViews: false };
            };

            this.bindingComplete = function () {
            };

            this.attached = function () {
            };

            this.compositionComplete = function () {
                //$('.preloading').css('overflow', 'hidden');

                $("#reportViewer1")
                    .telerik_ReportViewer({
                        // The url of the service which will provide the report viewer with reports.
                        // The service must be properly configured so that the report viewer can
                        // successfully communicate with the server.
                        // For more information on how to configure the service please check http://www.telerik.com/help/reporting/telerik-reporting-rest-conception.html.
                        serviceUrl: managerAuth.getServiceAddress('api/LinxReportAccessBVTelerikReport'),
                        // The url for the report viewer template. The template can be edited -
                        // new functionalities can be added and unneeded ones can be removed.
                        // For more information please check http://www.telerik.com/help/reporting/html5-report-viewer-templates.html.
                        templateUrl: managerAuth.buildUrl('lib/telerik_kendoui/templates/telerikReportViewerTemplate-9-0-15-422.html'),
                        // The ReportSource as string - TypeReportSource or UriReportSource.
                        //reportSource: { },
                        // Specifies whether the viewer is in interactive or print preview mode.
                        // PRINT_PREVIEW - Displays the paginated report as if it is printed on paper. Interactivity is not enabled.
                        // INTERACTIVE - Displays the report in its original width and height witn no paging. Additionally interactivity is enabled.
                        viewMode: telerikReportViewer.ViewModes.INTERACTIVE,
                        // Sets the scale mode of the viewer.
                        // Three modes exist currently:
                        // FIT_PAGE - The whole report will fit on the page (will zoom in or out), regardless of its width and height.
                        // FIT_PAGE_WIDTH - The report will be zoomed in or out so that the width of the screen and the width of the report match.
                        // SPECIFIC - Uses the scale to zoom in and out the report.
                        scaleMode: telerikReportViewer.ScaleModes.SPECIFIC,
                        // Zoom in and out the report using the scale
                        // 1.0 is equal to 100%, i.e. the original size of the report
                        scale: 1.0,

                        ready: function () {
                            //this.refreshReport();
                        },

                        exportBegin: function (e) {
                            common.setWindowMessage(false);
                        },

                        exportEnd: function (e) {
                        }

                    });

                var idRelatorio = managerUser.searchReports(router.activeInstruction().config.route);
                var viewer = $("#reportViewer1").data("telerik_ReportViewer");
                viewer.reportSource({
                    report: idRelatorio,
                    parameters: {
                        CurrentUser: managerAuth.userId,
                        CurrentUserName: managerAuth.nomeUsuario,
                        CurrentCompany: managerAuth.companyId,
                        AuthorizationToken: managerAuth.tokenId,
                        AccessGroup: managerAuth.accessGroupId,
                        EconomicGroup: managerAuth.economicGroupId,
                        Environment: managerAuth.environmentId,
                        Application: managerAuth.applicationId,
                        JqueryExpression: queryString.filter,
                        TranslatedJqueryExpression: queryString.translation,
                        CompanyLogo: globalDataParameters.parameters["TCS_LOGO_PADRAO"],
                        CompanyName: globalDataParameters.parameters["TCS_NOME_EMPRESA"],
                        Username: 'abc',
                        Password: 'abc'
                    }
                });

                viewer.refreshReport();
            };

            this.canDeactivate = function () {
                return true;
            };
            
            this.deactivate = function () {
                common.setWindowMessage(true);
            };

            this.detached = function () {
            };

        };

        return VM;
    });
