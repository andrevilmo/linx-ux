Flexmonster.initLinxToolbar = function (info) {//{toolbarInstance: { },vm: { }, pivotName: '', pivotAdapterLayout: '', tb, tb_layoutToolbar:true}
    if (isNull(info) || isNull(info.toolbarInstance))
        throw "O Método [Flexmonster.initLinxToolbar] não foi iniciado corretamente";

    var inputFileName, toolbarInstance = info.toolbarInstance;
    var container = toolbarInstance.pivotContainer;
    var vm = info.vm;
    var hasChanges = false;
    var layoutInfo = {
        pivotName: info.pivotName,
        pivotDataSource: info.pivotAdapterLayout
    };

    var _o = {
        __managerAuth: null,
        managerAuth: function () {
            if (this.__managerAuth == null) this.__managerAuth = require('managers/__auth');
            return this.__managerAuth;
        },
        __app: null,
        app: function () {
            if (this.__app == null) this.__app = require('durandal/app');
            return this.__app;
        },
        __breeze: null,
        breeze: function () {
            if (this.__breeze == null) this.__breeze = require('breeze');
            return this.__breeze;
        }
    };

    load();



    function load() {
        createToolbarCustomPropertiesAndMethods();
        createPivotCustomPropertiesAndMethods();

        createCustomTabs();
        addToolbar();


        if (info.tb_layoutToolbar) {
            var expandCollapseTab = $("<a><i id='icon-expand-collapse' class='fa fa-chevron-down'></i></a>");
            expandCollapseTab.click(function toogleWrapper() {
                $("#fm-toolbar-wrapper", "#" + container.getAttribute("id")).slideToggle('fast');
                $("#icon-expand-collapse", "#" + container.getAttribute("id")).addClass(function (i, current) {
                    $(this).removeClass();
                    if (current.indexOf("down") >= 0)
                        current = current.replace("down", "up");
                    else
                        current = current.replace("up", "down");
                    return current;
                });
            });
            $(container).insertBefore(expandCollapseTab, $(container).children().first());
        }

        var selected = $.grep(info.vm.layoutFiles, function (element, index) { return element.selected });
        if (selected.count() > 0) {
            toolbarInstance.pivot.currentLayout = selected[0].layoutFullName;
        }

        var labelLayout = document.createElement('p');
        labelLayout.setAttribute("id", container.id + "_p");
        labelLayout.setAttribute("style", "text-align: center;font-size: 18px;");
        var textnode = document.createTextNode("");
        labelLayout.appendChild(textnode);
        container.parentNode.insertBefore(labelLayout, container.parentNode.childNodes[0]);
        infoLayout();
    }

    //methods
    function createToolbarCustomPropertiesAndMethods() {
        toolbarInstance.idObjetoConteudo = 0;
        toolbarInstance.layoutFiles = info.vm && info.vm.layoutFiles;
        toolbarInstance.projectName = info.vm && info.vm.layoutFiles && info.vm.layoutFiles[0].projectName;
        toolbarInstance.pivot.prefixNameLayout = info.pivotName;

        toolbarInstance.toggleToolbar = function () {
            $("#fm-toolbar-wrapper", "#" + container.getAttribute("id")).slideToggle('fast');
            $(this).find('i').addClass(function (i, current) {
                $(this).removeClass();
                if (current.indexOf("down") >= 0)
                    current = current.replace("down", "up");
                else
                    current = current.replace("up", "down");
                return current;
            });
        };
    }

    function addToolbar() {
        var ul = $('<ul>');
        var create = false;
        if (info.tb_FullScreen) {
            create = true;
            $('<li><i class=\"fa fa-arrows-alt btn-full-screen\" title=\"Tela cheia\" /></li>')
                .appendTo(ul)
                .click(function () { toolbarInstance.toggleFullscreen(); });
        }
        if (info.tb_ToggleView) {
            create = true;
            $('<li><i class=\"fa fa-list btn-toggle-view\" title=\"Mudar visão\" /></li>')
                .appendTo(ul)
                .click(function () {
                    var options = toolbarInstance.pivot.getOptions();
                    if (options.viewType == 'grid') {
                        options.chartType = (options.chartType || 'bar');
                        toolbarInstance.pivot.showCharts(options.chartType, true);
                    } else {
                        toolbarInstance.pivot.showGrid();
                    }
                });
        }
        if (info.tb_OpenReport) {
            create = true;
            $('<li><i class=\"fa fa-folder btn-open-report\" title=\"Carregar layout\" /></li>')
                .appendTo(ul)
                .click(openRemoteReportCustom);
        }
        if (!info.tb_layoutToolbar) {
            create = true;
            $('<li><i class="fa fa-angle-down btn-toggle-toolbar" title="Expandir toolbar" /></li>')
                .appendTo(ul)
                .click(function () {
                    toolbarInstance.toggleToolbar();
                });
            //hide toolbar
            toolbarInstance.toggleToolbar();
        }

        if (create) $(container).append($('<div class="plus-actions"/>').append(ul));
    }

    function createCustomTabs() {
        var tabs = [];
        if (!info.tb_layoutToolbar)
            tabs = toolbarInstance.getTabs();

        toolbarInstance.getTabs = function () {
            if (info.tb_layoutToolbar) {
                tabs.push({ title: this.Labels.save, icon: this.icons.save, id: "lx-tab-save", handler: customSaveHandler, mobile: false });
                tabs.push({ divider: true });
            } else {
                delete tabs[0];
                delete tabs[1];
                delete tabs[2];
                tabs.insert(0, {
                    title: this.Labels.save,
                    icon: this.icons.save,
                    id: "lx-tab-save",
                    menu: [
                        { title: this.Labels.save, id: "lx-tab-save-main", handler: customSaveHandler, args: false },
                        { title: (this.Labels.save_as_linx ? this.Labels.save_as_linx : "Salvar como"), id: "lx-tab-save-as", handler: customSaveHandler, args: true }
                    ]
                });
            }
            tabs.push({
                id: "fm-tab-delete-layout-lx",
                icon: '<svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 -8 36 36"><path d="M3 6v18h18v-18h-18zm5 14c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm5 0c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm5 0c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm4-18v2h-20v-2h5.711c.9 0 1.631-1.099 1.631-2h5.315c0 .901.73 2 1.631 2h5.712z"/></svg>',
                title: (this.Labels.delete_layout_linx ? this.Labels.delete_layout_linx : "Excluir"),
                handler: customDeleteLayoutHandler
            });
            tabs.insert(0, {
                title: this.Labels.open,
                icon: this.icons.open,
                id: "lx-tab-open",
                menu: [
                    { title: this.Labels.local_report, id: "fm-tab-open-local-report", handler: openLocalLinxReport, mobile: false },
                    { title: this.Labels.remote_report, id: "fm-tab-open-remote-report", handler: openRemoteReportCustom }
                ]
            });

            return tabs;
        };
    }

    function infoLayout() {
        var layoutName = '(Nenhum)';
        var selected = $.grep(info.vm.layoutFiles, function (element, index) { return element.pivotName == info.pivotName && (toolbarInstance.pivot.currentLayout == element.layoutFullName || toolbarInstance.pivot.currentLayout == element.id) });
        if (selected.count() > 0) {
            layoutName = selected[0].name;
        }
        updateLabelLayout(layoutName);
    }

    function updateLabelLayout(layoutName) {
        var p = $('#' + container.id + '_p');
        p.text("Layout: " + layoutName);
    }

    function customDeleteLayoutHandler() {
        var deleteLayoutHandler = function () {
            var app = require('durandal/app');
            var idUser = _o.managerAuth().loginInfo.UidUsuario;
            var idLayout = $('#layoutsToDel').val();

            info.vm.showProcessing('Deletando Layout...');

            $.ajax({
                async: true,
                cache: false,
                type: 'DELETE',
                dataType: 'json',
                globalError: true,
                headers: _o.managerAuth().getHeaders(),
                contentType: 'application/json; charset=UTF-8',
                url: _o.managerAuth().getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/DeleteLayoutPivot?IdLayout=' + idLayout + '&uidUsuario=' + idUser,
                success: function (data) {
                    info.vm.closeProcessing();
                    app.showMessage("Layout deletado com sucesso!", "Informação");
                    info.vm.getLayoutsFiles();

                    if (toolbarInstance.idObjetoConteudo.toString() === idLayout.toString()) {
                        toolbarInstance.idObjetoConteudo = 0;
                        toolbarInstance.nomeLayout = "";
                    }

                },
                error: function (error) {
                },
                complete: function () {
                    info.vm.closeProcessing();
                }
            });
        };

        var dialog = new FlexmonsterToolbar.PopupManager.PopupWindow(this.popupManager);
        dialog.setTitle('Excluir Layout Remoto');
        dialog.setToolbar([
            { label: 'Deletar', handler: deleteLayoutHandler, isPositive: true },
            { label: 'Cancelar' }
        ]);
        var content = document.createElement('div');
        content.className = 'fm-form';
        var selectTag = document.createElement('select');
        selectTag.setAttribute('id', 'layoutsToDel');
        var optionTag = [];

        info.vm.layoutFiles.forEach(function (item) {
            if (item.name != '(Nenhum)' && item.pivotName == info.pivotName) {
                var _optionTag = document.createElement('option');
                var iconTag = document.createElement('i');
                if (item.id && typeof (item.id) === 'number') {
                    _optionTag.setAttribute('value', item.id);
                    iconTag.setAttribute('class', 'fa fa-server');
                    _optionTag.text = item.name;
                    _optionTag.appendChild(iconTag);
                    optionTag.push(_optionTag);
                }
            }
        });
        optionTag.forEach(function (i) {
            selectTag.appendChild(i);
        });
        content.appendChild(selectTag);
        dialog.setContent(content);
        this.popupManager.addPopup(dialog.content, toolbarInstance);
    }

    function openLocalLinxReport() {

        var inputXmlUx = $('#' + container.id + '_openFile');

        if (inputXmlUx.length === 0) {
            var input = $('<input id="' + container.id +'_openFile" type="file" accept=".json" />');
            input.css({ position: 'absolute', top: '-100px' });
            $('body').append(input);
            inputXmlUx = inputXmlUx = $('#' + container.id + '_openFile')[0];

            inputXmlUx.onclick = function (evt) {
                this.value = null;
            };

            inputXmlUx.onchange = function (evt) {

                var reader = new FileReader();
                reader.addEventListener('load', (loadEvent) => {
                    try {
                        toolbarInstance.pivot.setReport(JSON.parse(loadEvent.target.result));
                        var layoutName = evt.target.files[0].name + " - (local)";
                        updateLabelLayout(layoutName);

                        if (typeof vm.OnPivotLoadLayoutCompleted === 'function') {
                            setTimeout(function () { vm.OnPivotLoadLayoutCompleted(info.pivotName, layoutName); }, 200);
                        }

                        info.vm.layoutFiles.forEach(function (item) {
                            item.selected = item.name == '(Nenhum)';
                        });

                        toolbarInstance.pivot.currentLayout = '-1';
                    } catch (error) {
                        console.error(error);
                    }
                });
                reader.readAsText(evt.target.files[0]);
            };
        }
        else {
            inputXmlUx = $('#' + container.id + '_openFile')[0];
        }
        inputXmlUx.click();
    }

    function openRemoteReportCustom() {
        var applyHandler = function () {
            var selectedOption = selectTag.find("option:selected");
            toolbarInstance.pivot.currentLayout = selectedOption.val();
            if (selectedOption.text() != "(Nenhum)") {
                if (selectedOption.val().indexOf('.xml') > 0 || selectedOption.val().indexOf('.json') > 0) {
                    toolbarInstance.pivot.load(selectedOption.val());
                    toolbarInstance.idObjetoConteudo = 0;
                    toolbarInstance.nomeLayout = selectedOption.text();
                    infoLayout();
                    if (typeof vm.OnPivotLoadLayoutCompleted === 'function') {
                        setTimeout(function () { vm.OnPivotLoadLayoutCompleted(info.pivotName, selectedOption.text()); }, 200);
                    }
                } else {

                    $.ajax({
                        async: true,
                        cache: false,
                        type: 'GET',
                        dataType: 'json',
                        globalError: true,
                        headers: _o.managerAuth().getHeaders(),
                        url: _o.managerAuth().getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/GetPivotLayout?uidObjetoConteudo=' + selectedOption.val(),
                        success: function (data) {
                            toolbarInstance.pivot.setReport(JSON.parse(data.Content));
                            toolbarInstance.idObjetoConteudo = selectedOption.val();
                            toolbarInstance.nomeLayout = selectedOption.text();
                            toolbarInstance.pivot.isUserLayout = data.IsUserLayout;
                            infoLayout();
                            if (info.vm.status() == "Q") {
                                try { info.vm.currentDataItem().fillDetails(true, info.vm.viewName); }
                                catch (e) { }
                            }

                            if (typeof vm.OnPivotLoadLayoutCompleted === 'function') {
                                setTimeout(function () { vm.OnPivotLoadLayoutCompleted(info.pivotName, selectedOption.text()); }, 200);
                            }

                        },
                        error: function (error) {
                        },
                        complete: function () {
                        }
                    });
                }
            } else {
                var newPivot = toolbarInstance.pivot.getReport();
                toolbarInstance.pivot.setReport(newPivot);
                toolbarInstance.pivot.refresh();
                infoLayout();

                if (typeof vm.OnPivotLoadLayoutCompleted === 'function') {
                    setTimeout(function () { vm.OnPivotLoadLayoutCompleted(info.pivotName, selectedOption.text()); }, 200);
                }
            }
        };

        var dialog = new FlexmonsterToolbar.PopupManager.PopupWindow(toolbarInstance.popupManager);

        dialog.setTitle("Abrir Layout Remoto");
        dialog.setToolbar([
            { label: toolbarInstance.Labels.open, handler: applyHandler, isPositive: true },
            { label: toolbarInstance.Labels.cancel }
        ]);

        var content = $('<div class="fm-form" />');
        var selectTag = $('<select name="layouts"/>');

        info.vm.layoutFiles.forEach(function (item) {
            if (item.pivotName == info.pivotName) {
                var _optionTag = $('<option value="' + (item.id ? item.id : item.layoutFullName) + '" ' +
                    ((item.selected && toolbarInstance.pivot.currentLayout == '') || toolbarInstance.pivot.currentLayout == item.layoutFullName || toolbarInstance.pivot.currentLayout == item.id ? 'selected="true"' : '') + '>' + item.name + '</option>');
                _optionTag.append('<i class="fa fa-' + (item.id ? 'server' : 'user') + '"/>');
                selectTag.append(_optionTag);
            }
        });

        content.append(selectTag);
        dialog.setContent(content[0]);
        toolbarInstance.popupManager.addPopup(dialog.content);
    }

    function customSaveHandler(isNew) {
        isNew = (this.idObjetoConteudo === 0 || isNew || !this.pivot.isUserLayout);

        require(['viewmodels/shared/modalPivotSaveLayout'], function (mdl) {
            mdl.show(vm, toolbarInstance, layoutInfo, isNew).then(function () {
                updateLabelLayout(toolbarInstance.nomeLayout);
            });
        });
    }

    function createPivotCustomPropertiesAndMethods() {
        toolbarInstance.pivot.currentLayout = "";
        toolbarInstance.pivot.prefixNameLayout = "";
        toolbarInstance.pivot.isUserLayout = false;
    }

    function _alert(msg, title) {
        if (isNullOrEmpty(title))
            title = 'Alerta';
        _o.app().showMessage(msg, title, ['Ok']);
    }

};

Flexmonster.getMeasureCalculated = function (pivot) {

    var formatMeasures = [];
    var measures = pivot.getMeasures();

    if (measures && measures.some(function (item) { return item.calculated; })) {
        var measuresCalculated = measures.filter(function (item) { return item.calculated; });
        measuresCalculated.forEach(function (item) {
            formatMeasures.push({
                name: item.uniqueName,
                current: pivot.getFormat(item.uniqueName)
            });
        });
    }

    return formatMeasures;
}

Flexmonster.parsejEntitySearch = function (filters) {
    var jEntitySearch = '';
    if (filters && filters.length)
        filters.forEach(function (item) {
            var separator = item.values.length > 1 ? ',' : '';
            var type = item.values.length > 1 ? 'S' : (isNaN(item.values[0].split('.')[1].replace('[', '').replace(']', '')) ? 'S' : 'I');
            var operator = (item.negation) ? '!=' : '==';
            if (item.values.length > 1)
                operator = (item.negation) ? '!In' : 'In';
            if (jEntitySearch != '') jEntitySearch += ';';
            jEntitySearch += item.key + '#' + operator + '#' + type;
            item.values.forEach(function (value) {
                value = value.split('.')[1].replace('[', '').replace(']', '');
                jEntitySearch += value + separator;
            });
        });
    return jEntitySearch;
};

Flexmonster.getVisibleRowsColumns = function (pivot) {
    var context = [];
    if (pivot) {
        var measures = pivot.getMeasures();
        if (measures && measures.length)
            measures.forEach(function (item) {
                if (context.indexOf(item.uniqueName) < 0 && item.uniqueName != '[Measures]' && !item.calculated)
                    context.push(item.uniqueName);
            });

        var rows = pivot.getRows();
        if (rows && rows.length)
            rows.forEach(function (item) {
                if (context.indexOf(item.uniqueName) < 0 && item.uniqueName != '[Measures]')
                    context.push(item.uniqueName);
            });

        var coluns = pivot.getColumns();
        if (coluns && coluns.length)
            coluns.forEach(function (item) {
                if (context.indexOf(item.uniqueName) < 0 && item.uniqueName != '[Measures]')
                    context.push(item.uniqueName);
            });

        var filters = pivot.getPages();
        if (filters && filters.length)
            filters.forEach(function (item) {
                if (context.indexOf(item.uniqueName) < 0 && item.uniqueName != '[Measures]')
                    context.push(item.uniqueName);
            });
    }

    return context.join(',');
}