function gridSaveStates($grid, vm) {
    var _vm = vm;
    save();

    function _saveAppendRowsOnDemandSettings(oLayout) {
        if ($grid.data("igGridAppendRowsOnDemand") !== undefined) {
            var _appendRowsOnDemandSettings = {};

            $.extend(true, _appendRowsOnDemandSettings, $grid.data("igGridAppendRowsOnDemand").options);
            oLayout._features.push(_appendRowsOnDemandSettings);
        }
    }

    function _saveCellMergingSettings(oLayout) {
        if ($grid.data("igGridCellMerging") !== undefined) {
            var _cellMergingSettings = {};

            $.extend(true, _cellMergingSettings, $grid.data("igGridCellMerging").options);
            oLayout._features.push(_cellMergingSettings);
        }
    }

    function _saveColumnFixingSettings(oLayout) {
        if ($grid.data("igGridColumnFixing") !== undefined) {
            var _columnFixingSettings = {};
            $.extend(true, _columnFixingSettings, $grid.data("igGridColumnFixing").options);

            oLayout._features.push(_columnFixingSettings);

        }
    }

    function _saveColumnHidingSettings(oLayout) {
        if ($grid.data("igGridHiding") !== undefined) {
            var _columnHidingSettings = {};
            var columns = $grid.igGrid("option", "columns");
            $.extend(true, _columnHidingSettings, $grid.data("igGridHiding").options);

            for (var i = 0 ; i < columns.length ; i++) {
                if (columns[i].hidden) {
                    _columnHidingSettings.columnSettings.push({ columnKey: columns[i].key, hidden: true })
                }
            }

            oLayout._features.push(_columnHidingSettings);
        }
    }

    function _saveColumnMovingSettings(oLayout) {
        if ($grid.data("igGridColumnMoving") !== undefined) {
            var _columnMovingSettings = {};
            $.extend(true, _columnMovingSettings, $grid.data("igGridColumnMoving").options);

            oLayout._features.push(_columnMovingSettings);
        }
    }

    function _saveColumns(oLayout) {
        oLayout._columns = [];
        if ($grid.data("igGridMultiColumnHeaders") !== undefined) {
            //$.extend(true, oLayout._columns, $grid.igGridMultiColumnHeaders("getMultiColumnHeaders") );

            $.extend(true, oLayout._columns, $grid.igGrid("option", "columns"));

            for (var i = 0 ; i < oLayout._columns.length ; i++) {
                if (oLayout._columns[i].difference !== undefined) {
                    delete oLayout._columns[i].difference;
                    delete oLayout._columns[i].oWidth;
                }
            }
        } else {
            $.extend(true, oLayout._columns, $grid.igGrid("option", "columns"));

            for (var i = 0 ; i < oLayout._columns.length ; i++) {
                if (oLayout._columns[i].difference !== undefined) {
                    delete oLayout._columns[i].difference;
                    delete oLayout._columns[i].oWidth;
                }
            }
        }
    }

    function _saveFilteringSettings(oLayout) {
        if ($grid.data("igGridFiltering") !== undefined) {
            var _filteringSettings = {};
            var current = $grid.data("igGrid").dataSource.settings.filtering.expressions;
            var k = 0;

            $.extend(true, _filteringSettings, $grid.data("igGridFiltering").options);

            for (var i = 0 ; i < current.length ; i++) {
                for (var j = 0 ; j < _filteringSettings.columnSettings.length ; j++) {
                    if (_filteringSettings.columnSettings[j].columnKey == current[i].fieldName) {
                        _filteringSettings.columnSettings[j].defaultExpressions = [{
                            expr: current[i].expr,
                            cond: current[i].cond
                        }];
                    }
                }
            }

            oLayout._features.push(_filteringSettings);
        }
    }

    function _saveGroupBySettings(oLayout) {
        if ($grid.data("igGridGroupBy") !== undefined) {
            var _groupBySettings = {};
            var current = $grid.data("igGrid").dataSource.settings.sorting.expressions;

            $.extend(true, _groupBySettings, $grid.data("igGridGroupBy").options);
            ///rjmj
            for (var j = 0 ; j < _groupBySettings.columnSettings.length ; j++) {
                _groupBySettings.columnSettings[j].isGroupBy = false;
            }
            ///rjmj
            for (var i = 0 ; i < current.length ; i++) {
                if (current[i].isGroupBy) {
                    for (var j = 0 ; j < _groupBySettings.columnSettings.length ; j++) {
                        if (_groupBySettings.columnSettings[j].columnKey == current[i].fieldName) {

                            _groupBySettings.columnSettings[j].isGroupBy = true;
                            _groupBySettings.columnSettings[j].dir = current[i].dir;
                            break;
                        }
                    }
                }
            }

            oLayout._features.push(_groupBySettings);
        }
    }

    function _saveMultiColumnHeadersSettings(oLayout) {
        if ($grid.data("igGridMultiColumnHeaders") !== undefined) {
            var _multiColumnHeadersSettings = {};
            $.extend(true, _multiColumnHeadersSettings, $grid.data("igGridMultiColumnHeaders").options);

            oLayout._features.push(_multiColumnHeadersSettings);
        }
    }

    function _savePagingSettings(oLayout) {
        if ($grid.data("igGridPaging") !== undefined) {
            var _pagingSettings = {};
            $.extend(true, _pagingSettings, $grid.data("igGridPaging").options);

            oLayout._features.push(_pagingSettings);
        }
    }

    function _saveResizingSettings(oLayout) {
        if ($grid.data("igGridResizing") !== undefined) {
            var _resizingSettings = {};
            $.extend(true, _resizingSettings, $grid.data("igGridResizing").options);

            oLayout._features.push(_resizingSettings);
        }
    }

    function _saveResponsiveSettings(oLayout) {
        if ($grid.data("igGridResponsive") !== undefined) {
            var _responsiveSettings = {};
            $.extend(true, _responsiveSettings, $grid.data("igGridResponsive").options);

            oLayout._features.push(_responsiveSettings);
        }
    }

    function _saveRowSelectorsSettings(oLayout) {
        if ($grid.data("igGridRowSelectors") !== undefined) {
            var _rowSelectorsSettings = {};
            $.extend(true, _rowSelectorsSettings, $grid.data("igGridRowSelectors").options);

            oLayout._features.push(_rowSelectorsSettings);
        }
    }

    function _initSelection(oLayout) {
        if ($grid.data("igGridSelection") !== undefined && oLayout && Array.isArray(oLayout._selection)) {
            var _selectionSettings = $grid.data("igGridSelection").options;
            if (_selectionSettings.mode === "row") {
                for (var i = 0 ; i < oLayout._selection.length ; i++) {
                    var ind = $grid.find("tr[data-id='" + oLayout._selection[i].id + "']").index();
                    if (ind >= 0) {
                        $grid.igGridSelection("selectRow", ind);
                    }
                }
            } else {
                for (var i = 0 ; i < oLayout._selection.length ; i++) {
                    var ind = $grid.find("tr[data-id='" + oLayout._selection[i].id + "']").index();
                    if (ind >= 0) {
                        var colIndex;
                        var cols = $grid.igGrid("option", "columns");
                        for (var j = 0 ; j < cols.length ; j++) {
                            if (cols[j].key === oLayout._selection[i].columnKey) {
                                colIndex = j;
                                break;
                            }
                        }
                        $grid.igGridSelection("selectCell", ind, colIndex);
                    }
                }
            }

            _saveSelectionSettings(oLayout);
        }
    }

    function _saveSelectionSettings(oLayout) {
        if ($grid.data("igGridSelection") !== undefined) {
            oLayout._selection = [];
            var _selectionSettings = {};
            $.extend(true, _selectionSettings, $grid.data("igGridSelection").options);

            oLayout._features.push(_selectionSettings);

            if (_selectionSettings.multipleSelection) {
                if (_selectionSettings.editMode === "row") {
                    $.extend(true, oLayout._selection, $grid.data("igGridSelection").selectedRows());
                } else {
                    $.extend(true, oLayout._selection, $grid.data("igGridSelection").selectedCells());
                }
            } else {
                if (_selectionSettings.editMode === "row") {
                    if ($grid.data("igGridSelection").selectedRow() !== undefined &&
						$grid.data("igGridSelection").selectedRow() !== {}) {
                        var row = {};
                        $.extend(row, $grid.data("igGridSelection").selectedRow());

                        oLayout._selection.push(row);
                    }
                } else {
                    if ($grid.data("igGridSelection").selectedCell() !== undefined &&
						$grid.data("igGridSelection").selectedCell() !== {}) {
                        var cell = {};
                        $.extend(cell, $grid.data("igGridSelection").selectedCell());

                        oLayout._selection.push(cell);
                    }
                }
            }
        }
    }

    function _saveSortingSettings(oLayout) {
        if ($grid.data("igGridSorting") !== undefined) {
            var _sortingSettings = {};
            $.extend(true, _sortingSettings, $grid.data("igGridSorting").options);

            oLayout._features.push(_sortingSettings);
        }
    }

    function _saveSummariesSettings(oLayout) {
        if ($grid.data("igGridSummaries") !== undefined) {
            var _summariesSettings = {};
            $.extend(true, _summariesSettings, $grid.data("igGridSummaries").options);

            oLayout._features.push(_summariesSettings);
        }
    }

    function _saveTooltipsSettings(oLayout) {
        if ($grid.data("igGridTooltips") !== undefined) {
            var _tooltipsSettings = {};
            $.extend(true, _tooltipsSettings, $grid.data("igGridTooltips").options);

            oLayout._features.push(_tooltipsSettings);
        }
    }

    function _saveUpdatingSettings(oLayout) {
        if ($grid.data("igGridUpdating") !== undefined) {
            var _updatingSettings = {};
            $.extend(true, _updatingSettings, $grid.data("igGridUpdating").options);

            oLayout._features.push(_updatingSettings);
        }
    }

    function _initGrid(oLayout, options) {
        $grid.igGrid({
            accessibilityRendering: options.accessibilityRendering,
            adjustVirtualHeights: options.adjustVirtualHeights,
            aggregateTransactions: options.aggregateTransactions,
            alternateRowStyles: options.alternateRowStyles,
            autoAdjustHeight: options.autoAdjustHeight,
            autoCommit: options.autoCommit,
            autofitLastColumn: options.autofitLastColumn,
            autoFormat: options.autoFormat,
            autoGenerateColumns: options.autoGenerateColumns,
            avgColumnWidth: options.avgColumnWidth,
            avgRowHeight: options.avgRowHeight,
            caption: options.caption,
            columns: oLayout._columns,
            columnVirtualization: options.columnVirtualization,
            dataSource: options.dataSource,
            dataSourceType: options.dataSourceType,
            dataSourceUrl: options.dataSourceUrl,
            defaultColumnWidth: options.defaultColumnWidth,
            enableHoverStyles: options.enableHoverStyles,
            enableResizeContainerCheck: options.enableResizeContainerCheck,
            enableUTCDates: options.enableUTCDates,
            featureChooserIconDisplay: options.featureChooserIconDisplay,
            features: oLayout._features,
            fixedFooters: options.fixedFooters,
            fixedHeaders: options.fixedHeaders,
            height: options.height,
            jQueryTemplating: options.jQueryTemplating,
            jsonpRequest: options.jsonpRequest,
            localSchemaTransform: options.localSchemaTransform,
            mergeUnboundColumns: options.mergeUnboundColumns,
            primaryKey: options.primaryKey,
            renderCheckboxes: options.renderCheckboxes,
            requestType: options.requestType,
            responseContentType: options.responseContentType,
            responseDataKey: options.responseDataKey,
            responseTotalRecCountKey: options.responseTotalRecCountKey,
            restSettings: options.restSettings,
            rowTemplate: options.rowTemplate,
            rowVirtualization: options.rowVirtualization,
            serializeTransactionLog: options.serializeTransactionLog,
            showFooter: options.showFooter,
            showHeader: options.showHeader,
            tabIndex: options.tabIndex,
            templatingEngine: options.templatingEngine,
            updateURL: options.updateURL,
            virtualization: options.virtualization,
            virtualizationMode: options.virtualizationMode,
            virtualizationMouseWheelStep: options.virtualizationMouseWheelStep,
            width: options.width,
            rendered: function (evt, ui) {
                _vm.gridSaveStates[ui.owner.id()].gridSaveStates = gridSaveStates(ui.owner.element, _vm);
                _vm.gridSaveStates[ui.owner.id()].gridSaveStates._initSelection(oLayout);
            }
        });
    }

    function createLayoutObject() {
        return {
            _columns: [],
            _features: [],
            _selection: []
        };
    }

    function convertToJSON(_object) {
        var seen = [];
        return JSON.stringify(_object, function (key, val) {
            if (val != null && typeof val == "object") {
                if (seen.indexOf(val) >= 0) {
                    return;
                }
                seen.push(val);
            }
            return val;
        });
    }

    function convertToObject(_json) {
        return JSON.parse(_json);
    }
    //public methods

    function save() {
        var oLayout = createLayoutObject();

        _saveAppendRowsOnDemandSettings(oLayout);
        _saveCellMergingSettings(oLayout);
        _saveColumnFixingSettings(oLayout);
        _saveColumnHidingSettings(oLayout);
        _saveColumnMovingSettings(oLayout);
        _saveFilteringSettings(oLayout);
        _saveGroupBySettings(oLayout);
        _saveMultiColumnHeadersSettings(oLayout);
        _savePagingSettings(oLayout);
        _saveResizingSettings(oLayout);
        _saveResponsiveSettings(oLayout);
        _saveRowSelectorsSettings(oLayout);
        _saveSelectionSettings(oLayout);
        _saveSortingSettings(oLayout);
        _saveSummariesSettings(oLayout);
        _saveTooltipsSettings(oLayout);
        _saveUpdatingSettings(oLayout);

        _saveColumns(oLayout);
        return convertToJSON(oLayout)
    }

    function returnToSavedState(oLayout) {
        oLayout = convertToObject(oLayout);
        var gridOptions = $grid.data("igGrid").options;

        $grid.igGrid("destroy");
        _initGrid(oLayout, gridOptions);
    }

    function getFeatures() {
        return _features;
    }

    return {
        _initSelection: _initSelection,
        save: save,
        returnToSavedState: returnToSavedState,
        getFeatures: getFeatures
    }
}