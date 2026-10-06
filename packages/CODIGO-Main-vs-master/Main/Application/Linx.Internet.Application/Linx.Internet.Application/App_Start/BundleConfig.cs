using Linx.Internet.Application.Framework.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

using System.Web.Optimization;
using Linx.Internet.Application.Helpers;
using BundleTransformer.Core.Bundles;
using System.Configuration;
using Linx.Internet.Application;

namespace Linx.Internet.Application
{
    public class BundleConfig
    {
        public static Guid ApplicationExecutionId = Guid.NewGuid();

        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = BaseHelpers.GetShellCombineAndMinifyCssJsMode();
            BundleTable.VirtualPathProvider = new AssemblyResourceVirtualPathProvider();
            bundles.IgnoreList.Clear();

            AddDefaultIgnorePatterns(bundles.IgnoreList);

            #region start.js
            var startJs = new ScriptBundle(HtmlHelper.UrlWithModuleId("start.js"));
            startJs.Orderer = new AsIsBundleOrderer();
            startJs.Transforms.Clear();
            startJs
                .Include(HtmlHelper.UrlWithModuleId("/lib/telerik_kendoui/js/jquery-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/linx/js/start.js"));
            bundles.Add(startJs);
            #endregion

            #region core.js
            var coreJs = new ScriptBundle(HtmlHelper.UrlWithModuleId("/scripts/core.js"));
            coreJs.Orderer = new AsIsBundleOrderer();
            coreJs.Transforms.Clear();

            coreJs
                //.Include(HtmlHelper.UrlWithModuleIdMin("/lib/metronic/plugins/jquery-1-10-2{0}.js", "-min"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery-1-10-2-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery-migrate-1-2-1-min.js"))
                //alteração devido a erro que ocorre no DatePicker com IE
                //.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_ui/jquery-ui-1-10-3-custom-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_ui/jquery-ui-1-10-3-custom.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap/js/bootstrap-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/knockout/knockout-3-1-0-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/knockout/knockout-mapping-latest-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/q/q-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/moment/moment-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/math/Math-uuid.js"))
                //.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/tabdrop/js/bootstrap-tabdrop.js"))



                .Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jquery-ui-datepicker-pt-BR.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/i18n/infragistics-pt.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/infragistics-core-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/infragistics-dvv-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/infragistics-lob.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/modules/infragistics-util-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/CustomLinx/gridSaveStates.js"))




                .Include(HtmlHelper.UrlWithModuleId("/lib/telerik_kendoui/js/kendo-all-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/telerik_kendoui/js/print.min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/telerik_kendoui/js/cultures/kendo.culture.pt-BR.min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/telerik_kendoui/js/telerikReportViewer-9-0-15-422-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/modules/i18n/regional/infragistics-ui-regional-i18n-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/modules/i18n/regional/infragistics-ui-regional-pt-BR-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/extensions/infragistics-datasource-knockoutjs-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/extensions/infragistics-ui-combo-knockout-extensions.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/extensions/infragistics-ui-datachart-knockout-extensions-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/extensions/infragistics-ui-editors-knockout-extensions-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/extensions/infragistics-ui-grid-knockout-extensions-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/js/extensions/infragistics-ui-tree-knockout-extensions-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/globalize/globalize.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/globalize/cultures/globalize-culture-pt-BR2.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/fuse/fuse.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/gridster/jquery.gridster.min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/jspanel/jquery.jspanel.min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/jstree/jstree.min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/swiper/js/swiper.jquery.min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/mark/jquery.mark.min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/sifter/sifter.min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/html2canvas/html2canvas.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/html2canvas/html2canvas.svg.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/pdfmake/pdfmake.min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/linx/js/linx-common.js"))
                //
                .Include(HtmlHelper.UrlWithModuleId("/lib/linx/js/linx-functions.js"))
                //
                .Include(HtmlHelper.UrlWithModuleId("/lib/lodash/lodash.min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jquery-rstorage.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jquery_ezstorage/jquery-ezstorage.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jstree/jstree.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jsrender/jsrender-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jquery_lazy/jquery-lazy.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_validation/js/jquery-validate-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/select2/select2-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_wysihtml5/wysihtml5-0-3-0.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_wysihtml5/bootstrap-wysihtml5.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_wizard/jquery-bootstrap-wizard-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery-cookie-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_editable/js/bootstrap-editable-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_slimscroll/jquery-slimscroll-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery-blockui-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_switch/js/bootstrap-switch-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/ion_rangeslider/js/ion-rangeSlider-min.js"))

                //ERRO NA GRID (Fernando) .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/vendor/jquery.ui.widget.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/vendor/tmpl-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/vendor/load-image-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/vendor/canvas-to-blob-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/vendor/jquery-blueimp-gallery-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/jquery-iframe-transport.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/jquery-fileupload.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/jquery-fileupload-process.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/jquery-fileupload-image.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/jquery-fileupload-audio.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/jquery-fileupload-video.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/jquery-fileupload-validate.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/js/jquery-fileupload-ui.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/fullcalendar/fullcalendar-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jreject/js/jquery-reject.js"))
                //.Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/gridster/js/jquery-gridster-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/signalr/jquery-signalr-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/fancybox/source/jquery-fancybox-pack.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_touchspin/js/jquery-bootstrap-touchspin-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_toastr/toastr.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_tagsinput/bootstrap-tagsinput.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/scripts/app.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/scripts/quick-sidebar.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/scripts/form-components.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/flexmonster/flexmonster.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/flexmonster/_custom/flexmonster-custom.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/clipboard/clipboard.js"));
            //.Include(HtmlHelper.UrlWithModuleId("/lib/jsSHA/sha.js"));

            //.Include(HtmlHelper.UrlWithModuleId("/lib/requirejs/require.js"))
            //.Include(HtmlHelper.UrlWithModuleId("/lib/linx/js/config-require.js"));

            bundles.Add(coreJs);
            #endregion

            #region core-clean-js
            var coreCleanJs = new ScriptBundle(HtmlHelper.UrlWithModuleId("core-clean-js"));
            coreCleanJs.Orderer = new AsIsBundleOrderer();
            coreCleanJs.Transforms.Clear();
            coreCleanJs
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery-1-10-2-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery-migrate-1-2-1-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap/js/bootstrap-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/tabdrop/js/bootstrap-tabdrop.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery-cookie-min.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/uniform/jquery-uniform-min.js"))

                .Include(HtmlHelper.UrlWithModuleId("/lib/metronic/scripts/app.js"))
                .Include(HtmlHelper.UrlWithModuleId("/lib/linx/js/start-clean.js"));
            bundles.Add(coreCleanJs);
            #endregion

            #region core.css
            var coreCss = new CustomStyleBundle(HtmlHelper.UrlWithModuleId("/lib/core.css"));
            coreCss.Orderer = new AsIsBundleOrderer();
            //coreCss.Transforms.Clear();

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/font_awesome/font-awesome.less"));
            //coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/font_awesome/css/font-awesome.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap/css/bootstrap.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/uniform/css/uniform-default.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/select2/select2_metro.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_wysihtml5/bootstrap-wysihtml5.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_editable/css/bootstrap-editable.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_switch/css/bootstrap-switch-min.css"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/css/components.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/fonts/font.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/css/style.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/css/style-responsive.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/css/plugins.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/css/custom.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_toastr/toastr.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_tagsinput/bootstrap-tagsinput.css"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jreject/css/jquery-reject.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jtree/themes/default/style.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/jquery_file_upload/css/jquery-fileupload-ui.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/fancybox/source/jquery-fancybox.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/swiper/css/swiper.min.css"));


            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/gridster/jquery.gridster.min.css"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/jspanel/jquery.jspanel.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/jstree/themes/default/style.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/linx/css/linx-common.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/linx/css/linx-common-responsive.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/simple_line_icons/simple-line-icons.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/telerik_kendoui/styles/kendo-blueopal-min.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/telerik_kendoui/styles/kendo-common-min.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/telerik_kendoui/styles/telerikReportViewer-9-0-15-422.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/ion_rangeslider/css/ion-rangeSlider-Metronic.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/bootstrap_touchspin/css/jquery-bootstrap-touchspin.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/fullcalendar/fullcalendar.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/metronic/plugins/fullcalendar/fullcalendar-print.less"));

            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/jquery/plugins/jstree/themes/default/style.less"));
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/flexmonster/flexmonster.min.css"));
            //coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/flexmonster/theme/flexmonster-base.less"));

            //Mantenha nessa ordem
            coreCss.Include(HtmlHelper.UrlWithModuleId("/lib/linx/css/linx-theme-default.less"));

            bundles.Add(coreCss);
            #endregion   

            bundles.Add(BuildThemeBundle("default"));
            //bundles.Add(BuildThemeBundle("orange"));
            //bundles.Add(BuildThemeBundle("black"));
        }

        public static CustomStyleBundle BuildThemeBundle(string themeName)
        {
            var themeCss = new CustomStyleBundle(HtmlHelper.UrlWithModuleId(string.Format("/lib/theme-css-{0}.css", themeName)));
            themeCss.Orderer = new AsIsBundleOrderer();
            //coreCss.Transforms.Clear();

            themeCss.Include(HtmlHelper.UrlWithModuleId(string.Format("/lib/metronic/css/themes/{0}.less", themeName)));
            themeCss.Include(HtmlHelper.UrlWithModuleId(string.Format("/lib/infragistics_igniteui/css/themes/infragistics/infragistics-theme-{0}.less", themeName)));
            themeCss.Include(HtmlHelper.UrlWithModuleId("/lib/infragistics_igniteui/css/structure/infragistics.less"));

            return themeCss;

        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
        }

        public class AsIsBundleOrderer : IBundleOrderer
        {
            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }
    }
}