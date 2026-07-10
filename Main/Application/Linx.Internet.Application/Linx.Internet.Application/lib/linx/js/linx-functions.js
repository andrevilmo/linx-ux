/**
 * Seta a visibilidade de uma coluna da Grid / campo no Template
 * Permite utilizar o nome do campo ou a posição da coluna (index)
 * @param {object} vm - Vm
 * @param {string} gridName - Nome da Grid
 * @param {string} column - Nome da coluna ou posição (index)
 * @param {boolean} visible - Visível
 */

function setColGridVisibility(vm, gridName, column, visible) {
    var controlGrid = $lx(vm, "#" + (gridName.indexOf("dGrid") < 0 ? "dGrid" : "") + gridName);
    var managerAuth = require('managers/__auth');

    if (controlGrid.length && controlGrid.data('igGrid')) {
        controlGrid.igGridHiding(visible ? 'showColumn' : 'hideColumn', column);

        var columns = controlGrid.igGrid("option", "columns");

        if (typeof column === "number" && columns[column]) {
            column = columns[column].key;
        }

        setLayoutVisibility(vm, controlGrid[0].id + "_" + column, visible);
    }
    else if (managerAuth.shellMode === 'DEV') {
        console.warn("Controle('" + gridName + "') não encontrado.");
    }
}

/**
 * Seta a visibilidade de um campo da UI / Visão de Grid
 * @param {object} vm - Vm
 * @param {string} field - Nome do campo
 * @param {boolean} visible - Visível
 * @param {string} gridColumn - Campo na Visão de Grid
 */
function setVisibility(vm, field, visible, gridColumn) {
    var control = $lx(vm, "#" + field);
    var managerAuth = require('managers/__auth');

    if (control.length) {

        setLayoutVisibility(vm, control[0].id, visible);

        if (gridColumn && !isNullOrEmpty(gridColumn)) {
            var controlGrid = $('#scy' + vm.viewName + '_dGrid');
            if (controlGrid.length && controlGrid.data('igGrid')) {
                controlGrid.igGridHiding(visible ? 'showColumn' : 'hideColumn', gridColumn);
            }
        }
    }
    else if (managerAuth.shellMode === 'DEV')
        console.warn("Controle('" + field + "') não encontrado.");
}

/**
 * Seta a Visibilidade da Grid
 * @param {object} vm - vm
 * @param {string} gridName - Nome da Grid
 * @param {boolean} visible - Visível
 * @param {boolean} forceBind - Efetua novo Bind da Grid
 */
function setGridVisibility(vm, gridName, visible, forceBind) {
    var control = $lx(vm, "#" + gridName);
    var managerAuth = require('managers/__auth');

    if (control.length) {
        setLayoutVisibility(vm, control[0].id, visible);

        if (forceBind) {
            var dataSource = $.grep(vm.dataSource, function (e) { return e.key === control[0].id; });

            if (dataSource.length > 0) {
                dataSource[0].itemsSource.dataBind();
            }
        }
    }
    else if (managerAuth.shellMode === 'DEV')
        console.warn("Grid ('" + gridName + "') não encontrada.");
}

/**
 * Seta a propriedade que controla a visibilidade dos campos da UI
 * @param {object} vm - vm
 * @param {string} control - Nome do campo
 * @param {boolean} visible - Visível
 */
function setLayoutVisibility(vm, control, visible) {
    try {
        if (vm.flattenLayout()[control] !== undefined) {
            vm.flattenLayout()[control].Visible = visible;
        }
        else {
            Object.keys(vm.flattenLayout()).some(function (prop) {
                if (vm.flattenLayout()[prop].Id !== undefined) {
                    if (vm.flattenLayout()[prop].Id === control) {
                        vm.flattenLayout()[prop].Visible = visible;
                    }
                }
                else if (vm.flattenLayout()[prop].Name === control) {
                    vm.flattenLayout()[prop].Visible = visible;
                }
            });
        }
        vm.flattenLayout(vm.flattenLayout());
    }
    catch (e) { return; }
}

/**
 * Seta a edição de um campo na Grid e no Template
 * Permite utilizar o nome do campo ou a posição da coluna (index)
 * @param {object} vm - vm
 * @param {string} gridName - Nome da Grid
 * @param {string} column - Nome da coluna ou posição (index)
 * @param {boolean} isBlocked - Bloqueia
 * @param {boolean} isBlockedTemplate - Bloqueia no template
 */

function setColGridEditable(vm, gridName, column, isBlocked, isBlockedTemplate) {
    var controlGrid = $lx(vm, "#" + (gridName.indexOf("dGrid") < 0 ? "dGrid" : "") + gridName);
    var managerAuth = require('managers/__auth');

    if (controlGrid.length && controlGrid.data('igGrid')) {
        var columns = controlGrid.igGrid("option", "columns");
        var columnSettings = controlGrid.igGridUpdating('option', 'columnSettings');

        //se está utilizando index
        if (typeof column === "number" && columns[column]) {
            column = columns[column].key;
        }

        var gridColumn = $.grep(columnSettings, function (e) {
            return e.columnKey === column;
        });

        if (gridColumn.length > 0) {
            gridColumn[0]["readOnly"] = isBlocked;
            gridColumn[0]["fieldTplDisabled"] = isBlockedTemplate;

            setTimeout(function () { controlGrid.igGridUpdating('option', 'columnSettings', columnSettings); }, 100);

            var control = controlGrid[0].id + "_" + column;
            if (vm.flattenLayout()[control] !== undefined) {
                ctrl.setCustomEnable(vm, '#' + vm.flattenLayout()[control].Id, !isBlockedTemplate);
            }
        }
        else if (managerAuth.shellMode === 'DEV') {
            console.warn("Coluna('" + column + "') não encontrada.");
        }
    }
    else if (managerAuth.shellMode === 'DEV') {
        console.warn("Grid ('" + gridName + "') não encontrada.");
    }
}

/**
 * Seta a visibilidade de um TabItem
 * @param {object} vm - vm
 * @param {string} tabItemName - Nome do TabItem
 * @param {boolean} visible - Visível
 * @param {boolean} setActive - Indica se torna a TabItem ativa
 */
function setTabVisibility(vm, tabItemName, visible, setActive) {
    var managerAuth = require('managers/__auth');
    var control = $lx(vm, '#' + (!tabItemName.contains('ti') ? 'ti' : '') + tabItemName);

    if (control.length) {
        if (visible) {
            control.show();
            if (setActive) {
                setTabItemActive(vm, control[0].id);
            }
        }
        else {
            control.hide();
            control.removeClass('active');
            control.removeClass('active in');
        }
    }
    else if (managerAuth.shellMode === 'DEV')
        console.warn("TabItem '" + tabItemName + "' não encontrado.");
}

/**
 * Seta um TabItem como ativo
 * @param {object} vm - vm
 * @param {string} tabItemName - Nome do TabItem
 */
function setTabItemActive(vm, tabItemName) {
    var managerAuth = require('managers/__auth');
    var control = $lx(vm, '#' + (!tabItemName.contains('ti') ? 'ti' : '') + tabItemName);

    if (control.length) {
        control.find('> a').first().tab('show');
    }
    else if (managerAuth.shellMode === 'DEV')
        console.warn("TabItem '" + tabItemName + "' não encontrado.");
}

var controlLayout = {
    /**
    * Retorna o ColumnSpan 
    * @param {object} vm - vm
    * @param {string} name - Chave do campo no arquivo de Layout
    * @param {boolean} isDialogOpen - Caixa de diálogo da Grid aberta
    * @returns {string} - ColumnSpan
    */
    getColSpan: function (vm, name, isDialogOpen) {
        try {
            var colSpan = 12;
            if (vm.flattenLayout()[name] !== undefined) {
                colSpan = vm.flattenLayout()[name].ColumnSpan;
            }
            else {
                if (isDialogOpen) {
                    Object.keys(vm.flattenLayout()).some(function (prop) {
                        if (vm.flattenLayout()[prop].Id !== undefined) {
                            if (vm.flattenLayout()[prop].Id === name)
                                colSpan = vm.flattenLayout()[prop].ColumnSpan;
                        }
                        else if (vm.flattenLayout()[prop].Name === name)
                            colSpan = vm.flattenLayout()[prop].ColumnSpan;
                    });
                }
            }
            return 'col-lg-' + colSpan + ' col-md-' + colSpan;
        }
        catch (e) {
            return 'col-lg-' + colSpan + ' col-md-' + colSpan;
        }
    },

    /**
    * Retorna o DisplayName 
    * @param {object} vm - vm
    * @param {string} name - Chave do campo no arquivo de Layout
    * @param {boolean} isDialogOpen - Caixa de diálogo da Grid aberta
    * @returns {string} - DisplayName
    */
    getDisplayName: function (vm, name, isDialogOpen) {
        try {
            var displayName = "";
            if (vm.flattenLayout()[name] !== undefined) {
                displayName = vm.flattenLayout()[name].DisplayName;
            }
            else {
                if (isDialogOpen) {
                    Object.keys(vm.flattenLayout()).some(function (prop) {
                        if (vm.flattenLayout()[prop].Id !== undefined) {
                            if (vm.flattenLayout()[prop].Id === name)
                                displayName = vm.flattenLayout()[prop].DisplayName;
                        }
                        else if (vm.flattenLayout()[prop].Name === name)
                            displayName = vm.flattenLayout()[prop].DisplayName;
                    });
                }
            }
            return displayName;
        }
        catch (e) { return ""; }
    },

    /**
    * Retorna a Visibilidade
    * @param {object} vm - vm
    * @param {string} name - Chave do campo no arquivo de Layout
    * @param {boolean} isDialogOpen - Caixa de diálogo da Grid aberta
    * @returns {boolean} - Visibilidade
    */
    getVisibility: function (vm, name, isDialogOpen) {
        try {
            var visibility = true;
            if (vm.flattenLayout()[name] !== undefined) {
                visibility = vm.flattenLayout()[name].Visible;
            }
            else {
                if (isDialogOpen) {
                    Object.keys(vm.flattenLayout()).some(function (prop) {
                        if (vm.flattenLayout()[prop].Id !== undefined) {
                            if (vm.flattenLayout()[prop].Id === name)
                                visibility = vm.flattenLayout()[prop].Visible;
                        }
                        else if (vm.flattenLayout()[prop].Name === name)
                            visibility = vm.flattenLayout()[prop].Visible;
                    });
                }
            }
            return visibility;
        }
        catch (e) { return true; }
    },

    /**
    * Retorna o UniqueName da Dimensão informada
    * @param {object} vm - vm
    * @param {string} name - Chave do campo no arquivo de Layout
    * @returns {string} - UniqueName
    */
    getDimensionUniqueName: function (vm, name) {
        try {
            return vm.flattenLayout()[name].DimensionUniqueName;
        }
        catch (e) { return ''; }
    },

    /**
    * Retorna o Display Name do Header da Grid
    * @param {object} vm - vm
    * @param {string} name - Chave do campo no arquivo de Layout
    * @returns {string} - Display Name para o Header da Grid
    */
    getGridHeaderDisplayName: function (vm, name) {
        return controlLayout.getDisplayName(vm, name, true);
    }

};

var pivotLayout = {
    /**
     * Seta o Display Name (caption) de um campo da Pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} columnList - Lista de campos : displayNames
     */
    setDisplayName: function (vm, pivotName, columnList) {
        var managerAuth = require('managers/__auth');

        Object.keys(columnList).forEach(function (key) {
            var fullName = vm.viewName + '_pivot' + pivotName + '_' + key;
            var layoutField = $.grep(Object.keys(vm.flattenLayout()), function (e) { return vm.flattenLayout()[e].Id === fullName; });

            if (layoutField && vm.flattenLayout()[layoutField[0]]) {
                vm.flattenLayout()[layoutField[0]].DisplayName = columnList[key];
            }
            else {
                if (managerAuth.shellMode === 'DEV')
                    console.warn("Controle('" + key + "') não encontrado.");
            }
        });
        pivotLayout.updateLayout(vm, pivotName);
    },
    /**
     * Atualiza o layout da Pivot (DisplayName e Visible)
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     */
    updateLayout: function (vm, pivotName) {
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var pivotStructure = pivotLayout.getPivotStructure(vm, pivot);
        var report = pivot.instance.getReport();

        if (report) {
            report.dataSource.data[0] = pivotStructure;

            //Measures
            if (report.slice.measures) {
                report.slice.measures.forEach(function (item) {
                    if (pivotStructure[item.uniqueName]) {
                        item.caption = pivotStructure[item.uniqueName].caption;
                    }
                });
            }

            //Rows
            if (report.slice.rows) {
                report.slice.rows.forEach(function (item) {
                    if (pivotStructure[item.uniqueName]) {
                        item.caption = pivotStructure[item.uniqueName].caption;
                    }
                });
            }

            //Columns
            if (report.slice.columns) {
                report.slice.columns.forEach(function (item) {
                    if (pivotStructure[item.uniqueName]) {
                        item.caption = pivotStructure[item.uniqueName].caption;
                    }
                });
            }

            //Filters
            if (report.slice.reportFilters) {
                report.slice.reportFilters.forEach(function (item) {
                    if (pivotStructure[item.uniqueName]) {
                        item.caption = pivotStructure[item.uniqueName].caption;
                    }
                });
            }

            pivot.instance.setReport(report);
            pivotLayout.updatePivotFormats(vm, pivot);
        }
    },
    /**
     * Remove o campo da lista de campos disponíveis
     * @param {oject} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} fieldList - Lista de campos
     */
    removeField: function (vm, pivotName, fieldList) {
        var managerAuth = require('managers/__auth');

        fieldList.forEach(function (key) {
            var fullName = vm.viewName + '_pivot' + pivotName + '_' + key;
            var layoutField = $.grep(Object.keys(vm.flattenLayout()), function (e) { return vm.flattenLayout()[e].Id === fullName; });

            if (layoutField && vm.flattenLayout()[layoutField[0]]) {
                vm.flattenLayout()[layoutField[0]].Visible = false;
            }
            else {
                if (managerAuth.shellMode === 'DEV')
                    console.warn("Campo ('" + key + "') não encontrado.");
            }
        });
        pivotLayout.updateLayout(vm, pivotName);
    },
    /**
     * Adiciona o campo na lista de campos disponíveis
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} fieldList - Lista de campos
     */
    addField: function (vm, pivotName, fieldList) {
        var managerAuth = require('managers/__auth');

        fieldList.forEach(function (key) {
            var fullName = vm.viewName + '_pivot' + pivotName + '_' + key;
            var layoutField = $.grep(Object.keys(vm.flattenLayout()), function (e) { return vm.flattenLayout()[e].Id === fullName; });

            if (layoutField && vm.flattenLayout()[layoutField[0]]) {
                vm.flattenLayout()[layoutField[0]].Visible = true;
            }
            else {
                if (managerAuth.shellMode === 'DEV')
                    console.warn("Campo ('" + key + "') não encontrado.");
            }
        });
        pivotLayout.updateLayout(vm, pivotName);
    },
    /**
     * Remove o Valor (Measure) da pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} measureList - Lista de Valores (Measures)
     */
    removeMeasure: function (vm, pivotName, measureList) {
        var managerAuth = require('managers/__auth');
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var report = pivot.instance.getReport();
        if (report) {
            measureList.forEach(function (key) {
                var item = $.grep(report.slice.measures, function (element, index) { return element.uniqueName === key; });

                if (!item || item.count() === 0) {
                    if (managerAuth.shellMode === 'DEV') {
                        console.warn("Valor ('" + key + "') não encontrado.");
                    }
                }
                else {
                    report.slice.measures.removeItem(item[0]);
                }
            });
            pivot.instance.setReport(report);
        }

    },
    /**
     * Adiciona o Valor (measure) com a função de agregação indicada na posição informada
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} measureList - Lista de Valores (Measures) / Posição / Função de agregação (sum, count, distinctcount, average, product, min, max, percent, percentofcolumn, percentofrow, index, difference, %difference, runningtotals, stdevp, stdevs)
     */
    addMeasure: function (vm, pivotName, measureList) {
        var managerAuth = require('managers/__auth');
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var report = pivot.instance.getReport();
        if (report) {
            var pivotStructure = pivotLayout.getPivotStructure(vm, pivot);
            var formats = pivotLayout.getPivotFormats(vm, pivot);
            measureList.forEach(function (item) {
                if (!pivotStructure[item.measure]) {
                    if (managerAuth.shellMode === 'DEV') {
                        console.warn("Valor ('" + item.measure + "') não encontrado.");
                    }
                }
                else {
                    if (!item.aggregation) {
                        item.aggregation = "sum";
                    }

                    var format = $.grep(report.formats, function (element, index) { return element.name === item.measure; });
                    if (!format || format.count() === 0) {
                        report.formats.push(formats[item.measure], item.measure);
                    }

                    var measure = $.grep(report.slice.measures, function (element, index) { return element.uniqueName === item.measure; });
                    if (measure && measure.count() > 0) {
                        report.slice.measures.removeItem(measure[0]);
                    }
                    report.slice.measures.splice(item.position, 0, { uniqueName: item.measure, aggregation: item.aggregation, caption: pivotStructure[item.measure].caption, active: true, format: item.measure });
                }
            });
            pivot.instance.setReport(report);
        }
    },
    /**
     * Remove o filtro da pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} filterColumnList - Lista de filtros
     */
    removeFilter: function (vm, pivotName, filterColumnList) {
        var managerAuth = require('managers/__auth');
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var report = pivot.instance.getReport();
        if (report) {
            if (report.slice.reportFilters) {
                filterColumnList.forEach(function (key) {
                    var item = $.grep(report.slice.reportFilters, function (element, index) { return element.uniqueName === key; });
                    if (!item || item.count() === 0) {
                        if (managerAuth.shellMode === 'DEV') {
                            console.warn("Filtro ('" + key + "') não encontrado.");
                        }
                    }
                    else {
                        report.slice.reportFilters.removeItem(item[0]);
                    }
                });
                pivot.instance.setReport(report);
            }
            else {
                if (managerAuth.shellMode === 'DEV') {
                    console.warn("Filtro não encontrado.");
                    return;
                }
            }
        }
    },
    /**
     * Adiciona filtro na pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} filterColumnList - Colunas filtro : posição
     */
    addFilter: function (vm, pivotName, filterColumnList) {
        var managerAuth = require('managers/__auth');
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var report = pivot.instance.getReport();
        if (report) {
            var pivotStructure = pivotLayout.getPivotStructure(vm, pivot);
            Object.keys(filterColumnList).forEach(function (key) {
                if (!pivotStructure[key]) {
                    if (managerAuth.shellMode === 'DEV') {
                        console.warn("Coluna ('" + key + "') não encontrada.");
                    }
                }
                else {
                    if (!report.slice.reportFilters) {
                        report.slice.reportFilters = [];
                    }
                    report.slice.reportFilters.splice(filterColumnList[key], 0, { uniqueName: key });
                }
            });
            pivot.instance.setReport(report);
        }
    },
    /**
     * Remove coluna da pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} columnList - Lista de colunas
     */
    removeColumn: function (vm, pivotName, columnList) {
        var managerAuth = require('managers/__auth');
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var report = pivot.instance.getReport();
        if (report) {
            columnList.forEach(function (key) {
                var item = $.grep(report.slice.columns, function (element, index) { return element.uniqueName === key; });
                if (!item || item.count() === 0) {
                    if (managerAuth.shellMode === 'DEV') {
                        console.warn("Coluna ('" + key + "') não encontrada.");
                    }
                }
                else {
                    report.slice.columns.removeItem(item[0]);
                }
            });
            pivot.instance.setReport(report);
        }
    },
    /**
     * Adiciona coluna na Pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} columnList - Lista de colunas / Posição (inicia em 0 (zero))
     */
    addColumn: function (vm, pivotName, columnList) {
        var managerAuth = require('managers/__auth');
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var report = pivot.instance.getReport();
        if (report) {
            var pivotStructure = pivotLayout.getPivotStructure(vm, pivot);
            Object.keys(columnList).forEach(function (key) {
                if (!pivotStructure[key]) {
                    if (managerAuth.shellMode === 'DEV') {
                        console.warn("Coluna ('" + key + "') não encontrada.");
                    }
                }
                else {
                    if (!report.slice.columns) {
                        report.slice.columns = [];
                    }
                    report.slice.columns.splice(columnList[key], 0, { uniqueName: key });
                }
            });
            pivot.instance.setReport(report);
        }
    },
    /**
     * Remove linha da Pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} rowColumnList - Lista de colunas 
     */
    removeRow: function (vm, pivotName, rowColumnList) {
        var managerAuth = require('managers/__auth');
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var report = pivot.instance.getReport();
        if (report) {
            rowColumnList.forEach(function (key) {
                var item = $.grep(report.slice.rows, function (element, index) { return element.uniqueName === key; });
                if (!item || item.count() === 0) {
                    if (managerAuth.shellMode === 'DEV') {
                        console.warn("Coluna ('" + key + "') não encontrada.");
                    }
                }
                else {
                    report.slice.rows.removeItem(item[0]);
                }
            });
            pivot.instance.setReport(report);
        }
    },
    /**
     * Adiciona linha na Pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @param {object} rowColumnList - List de colunas / Posição (inicia em 0 (zero))
     */
    addRow: function (vm, pivotName, rowColumnList) {
        var managerAuth = require('managers/__auth');
        var pivot = pivotLayout.getPivotInstance(vm, pivotName);

        if (!pivot) {
            return;
        }

        var report = pivot.instance.getReport();
        if (report) {
            var pivotStructure = pivotLayout.getPivotStructure(vm, pivot);
            Object.keys(rowColumnList).forEach(function (key) {
                if (!pivotStructure[key]) {
                    if (managerAuth.shellMode === 'DEV') {
                        console.warn("Coluna ('" + key + "') não encontrada.");
                    }
                }
                if (!report.slice.rows) {
                    report.slice.rows = [];
                }
                report.slice.rows.splice(rowColumnList[key], 0, { uniqueName: key, caption: pivotStructure[key].caption });
            });
            pivot.instance.setReport(report);
        }
    },
    /**
     * Retorna a instância da Pivot
     * @param {object} vm - vm
     * @param {string} pivotName - Nome da Pivot
     * @returns {object} Pivot
     */
    getPivotInstance: function (vm, pivotName) {
        var managerAuth = require('managers/__auth');

        if (!vm.pivots || vm.pivots.count() === 0) {
            if (managerAuth.shellMode === 'DEV')
                console.warn("Pivot ('" + pivotName + "') não encontrada.");
            return null;
        }

        var pivot = $.grep(vm.pivots, function (element, index) { return element.pivotName === pivotName; });

        if (pivot.count() === 0) {
            if (managerAuth.shellMode === 'DEV')
                console.warn("Pivot ('" + pivotName + "') não encontrada.");
            return null;
        }

        return pivot[0];
    },
    /**
     * Retorna a estrutura da Pivot
     * @param {object} vm - vm
     * @param {object} pivot - Pivot (instância)
     * @returns {Array} structure 
     */
    getPivotStructure: function (vm, pivot) {
        var dataSource = $.grep(vm.dataSource, function (element, index) { return element.key === pivot.container; });
        return dataSource[0].itemsSource.getStructure();
    },
    /**
     * Retorna a formatação dos campos da Pivot
     * @param {object} vm - vm
     * @param {object} pivot - Pivot (instância)
     * @returns {Array} formats
     */
    getPivotFormats: function (vm, pivot) {
        var dataSource = $.grep(vm.dataSource, function (element, index) { return element.key === pivot.container; });
        return dataSource[0].itemsSource.getPivotFormats();
    },
    /**
     * Atualiza a formatação dos campos da Pivot
     * @param {object} vm - vm
     * @param {object} pivot - Pivot (instância)
     */
    updatePivotFormats: function (vm, pivot) {
        var formats = pivotLayout.getPivotFormats(vm, pivot);

        Object.keys(formats).forEach(function (key) {
            pivot.instance.setFormat(formats[key], key);
        });
    }
};

var gridFunctions = {
    /**
     * Função para ordenação das Grids (complement)
     * @param {array} data - Dados para ordenação
     * @param {array} fields - Informações do campo a ser ordenado
     * @param {string} direction - Direção da ordenação
     * @returns {array} - Dados ordenados
     */
    sort: function (data, fields, direction) {
        if (data.length === 0)
            return data;

        var sorted = [];

        switch (typeof data[0][fields[0].fieldName]) {
            case "number":
                sorted = data.sort(function (a, b) { return direction === "ascending" ? a[fields[0].fieldName] - b[fields[0].fieldName] : b[fields[0].fieldName] - a[fields[0].fieldName]; });
                break;

            case "object":
                sorted = data.sort(function (a, b) { return direction === "ascending" ? a[fields[0].fieldName].getTime() - b[fields[0].fieldName].getTime() : b[fields[0].fieldName].getTime() - a[fields[0].fieldName].getTime(); });
                break;

            default:
                sorted = data.sort(function (a, b) { return direction === "ascending" ? a[fields[0].fieldName].toString().localeCompare(b[fields[0].fieldName].toString()) : b[fields[0].fieldName].toString().localeCompare(a[fields[0].fieldName].toString()); });
        }

        return sorted;
    }

};

function setMaskedRandom(vm, inputNames) {
    inputNames.forEach(function (item) {
        var maskedText = "";
        var control = $lx(vm, item);
        var input;
        if (!item.contains('lUp'))
            input = control[0];
        else
            input = control[0].firstElementChild;

        for (var i = 0; i < input.value.length; i++) {
            var char = input.value[i]
            if (char !== " ") {
                if (Math.floor(Math.random() * 100) < 50)
                    maskedText += char;
                else
                    maskedText += "*";
            } else {
                maskedText += char;
            }
        }

        if (!item.contains('lUp'))
            control[0].value = maskedText;
        else
            control[0].firstElementChild.value = maskedText;
        
    });
}