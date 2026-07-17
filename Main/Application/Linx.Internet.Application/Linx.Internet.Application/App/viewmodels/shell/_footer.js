define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'managers/error', 'managers/hub'],
    function (logger, router, app, ko, managerUser, managerAuth, managerError, managerHub) {
        var copyrightHtml = ko.observable('');
        var releaseVersion = ko.observable('');

        var FOOTER_STYLE = 'background-color: white;color:#fff;z-index:50 !important;position:fixed;bottom:0;left:0;right:0;width:100%;text-align:right;min-height:36px;height:36px;padding:8px 20px;margin:0;display:block;visibility:visible;opacity: 0.8;box-sizing:border-box;line-height:20px;font-size:12px;box-shadow:0px -1px 5px 1px rgba(0, 0, 0, 0.2);';
        var COPYRIGHT_STYLE = 'color: black;font-weight:lighter;display:inline-block;vertical-align:middle;text-align:right;width:100%;';

        var vm = {
            activate: activate,
            attached: attached,
            beforeBind: beforeBind,
            afterBind: afterBind,
            canDeactivate: canDeactivate,
            canActivate: canActivate,
            deactivate: deactivate,
            compositionComplete: compositionComplete,

            router: router,
            managerUser: managerUser,
            managerAuth: managerAuth,
            managerError: managerError,
            managerHub: managerHub,
            copyrightHtml: copyrightHtml
        };

        return vm;

        // © {YEAR} Linx - Todos direitos reservados. [{version} :: {dd/MM/yyyy HH:mm}]
        function buildCopyrightHtml() {
            var year = managerAuth.YEAR || (new Date()).getFullYear();
            var version = releaseVersion()
                || managerAuth.SHELL_VERSION
                || $('meta[name=linx-internet-application-version]').attr('content')
                || $('meta[name=linx-internet-application-version-label]').attr('content')
                || '';
            var dateVersion = managerAuth.SHELL_BUILD_DATE
                || $('meta[name=linx-internet-application-date-version]').attr('content')
                || '';

            return '\u00A9 ' + year + ' Linx - Todos direitos reservados. [' + version + (version && dateVersion ? ' :: ' : '') + dateVersion + ']';
        }

        function refreshFooterContent() {
            copyrightHtml(buildCopyrightHtml());
        }

        function loadHighestReleaseVersion() {
            if (!managerAuth || typeof managerAuth.getServiceAddress !== 'function') {
                return;
            }

            return $.ajax({
                type: 'GET',
                headers: managerAuth.getHeaders(),
                url: managerAuth.getServiceAddress('LinxFrameworkModulo', 'Linx.Framework.BV') + '/GetHighestReleaseVersion',
                cache: true,
                globalError: false
            }).done(function (versao) {
                var value = (versao || '').toString().replace(/^"|"$/g, '').trim();
                if (value) {
                    releaseVersion(value);
                    refreshFooterContent();
                }
            });
        }

        function canActivate() {
            return true;
        }

        function canDeactivate() {
            return true;
        }

        function beforeBind() {
            return true;
        }

        function afterBind() {
            return true;
        }

        function attached(view) {
            refreshFooterContent();
            ensureFooterVisible(view);
            loadHighestReleaseVersion();
            return true;
        }

        function deactivate() {
            return true;
        }

        function activate() {
            refreshFooterContent();
            loadHighestReleaseVersion();
            return true;
        }

        function resolveFooter($root) {
            var $footer = $root.find('#shellCopyrightFooter').addBack('#shellCopyrightFooter').filter('#shellCopyrightFooter').first();
            if (!$footer.length) {
                $footer = $root.find('.footer').addBack('.footer').filter('.footer').first();
            }
            return $footer;
        }

        function ensureFooterVisible(view) {
            var $root = view ? $(view) : $('#mainFooter');
            var $footer = resolveFooter($root);
            if (!$footer.length) {
                $footer = $('#shellCopyrightFooter');
            }
            if (!$footer.length) {
                return;
            }

            $('#mainFooter').css({
                'width': '100%',
                'box-shadow': 'none'
            });

            // Force exact attribute values (do not merge with prior dark styles)
            $footer.attr({
                'id': 'shellCopyrightFooter',
                'class': 'footer',
                'style': FOOTER_STYLE
            });
            if ($footer[0] && $footer[0].style && $footer[0].style.setProperty) {
                $footer[0].style.setProperty('z-index', '50', 'important');
                $footer[0].style.setProperty('width', '100%', 'important');
                $footer[0].style.setProperty('text-align', 'right', 'important');
                $footer[0].style.setProperty('box-shadow', '0px -1px 5px 1px rgba(0, 0, 0, 0.2)', 'important');
            }

            var $text = $footer.find('#shellCopyrightText');
            if (!$text.length) {
                $text = $footer.children('.footer-inner').first();
                $text.attr('id', 'shellCopyrightText');
            }
            $text.attr({
                'class': 'footer-inner',
                'style': COPYRIGHT_STYLE
            });
            if ($text[0] && $text[0].style && $text[0].style.setProperty) {
                $text[0].style.setProperty('width', '100%', 'important');
                $text[0].style.setProperty('text-align', 'right', 'important');
            }

            $(window).trigger('resize');
        }

        function compositionComplete(view) {
            refreshFooterContent();
            ensureFooterVisible(view);
            loadHighestReleaseVersion();
            // Beat any late theme/metronic footer styling
            setTimeout(function () { ensureFooterVisible(view); }, 0);
            setTimeout(function () { ensureFooterVisible(view); }, 100);
        }
    });
