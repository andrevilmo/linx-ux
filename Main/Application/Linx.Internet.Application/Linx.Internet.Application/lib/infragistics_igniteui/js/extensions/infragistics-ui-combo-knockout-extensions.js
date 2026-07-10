/*!@license
* Infragistics.Web.ClientUI igCombo KnockoutJS extension 15.1.20151.2300
*
* Copyright (c) 2012-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
* Depends on:
*	jquery-1.7.2.js
*	ig.util.js
*	ig.dataSource.js
*/

/*global ko, jQuery*/
(function ($) {
    ko.bindingHandlers.igCombo = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var combo = $(element),
                options = valueAccessor(),
        		selectedItems = valueAccessor().selectedItems,
                isArray = false;

            if (ko.isObservable(selectedItems)) {
                isArray = Array.isArray(ko.utils.unwrapObservable(selectedItems));
            }

            combo.igCombo(options);

            // Attach the different custom binding handlers
            ko.applyBindingsToNode(element, {
                igComboSelection: {
                    selectedItems: selectedItems
                }
            }, selectedItems);

            ko.applyBindingsToNode(combo.data("igCombo")._options.$dropDownCont[0], {
                igComboList: {
                    combo: combo,
                    options: options,
                    dataSource: valueAccessor().dataSource,
                    selectedItems: selectedItems
                }
            }, valueAccessor().dataSource);

            ko.utils.registerEventHandler(element, "igcomboselectionchanged", function (evt, ui) {
                var valueKey = ui.owner.options.valueKey,
                    items = ui.items,
                    selectedItems = valueAccessor().selectedItems,
                    selectionType = valueAccessor().selectedItemType,
                    selectedValues = [],
                    item, itemData, firstItem, itemForSelection, index;

                if (items && typeof selectedItems !== 'undefined') {
                    if (ko.isObservable(selectedItems))
                        selectedItems = ko.utils.unwrapObservable(selectedItems);
                    if (!selectionType) {
                        if (selectedItems && (!Array.isArray(selectedItems) || (Array.isArray(selectedItems) && selectedItems.length > 0))) {
                            // Take the format of the initially selected items set in the ViewModel:
                            if (Array.isArray(selectedItems))
                                firstItem = selectedItems[0];
                            else firstItem = selectedItems;
                            if (typeof firstItem === "function") {
                                firstItem = firstItem();
                            }
                            if (typeof firstItem === "object") {
                                // ViewModel code: this.selectedItems = ko.observableArray([data[1]]);
                                selectionType = "object";
                            } else {
                                // ViewModel code: this.selectedItems = ko.observableArray(["value1"]);
                                selectionType = "primitive";
                            }
                        } else {
                            // This means that in the ViewModel there isn't initially selected items:
                            // ViewModel code: this.selectedItems = ko.observableArray();
                            // In such a case we create seletedItems as array of primitives
                            selectionType = "primitive";
                        }
                    }

                    items = (typeof items === "function") ? items() : items;
                    for (index = 0; index < items.length; index++) {
                        item = items[index];
                        itemData = item.data;
                        if (typeof itemData === "function") {
                            itemData = itemData();
                        }
                        if (selectionType === "object") {
                            itemForSelection = itemData;
                        } else if (selectionType === "primitive") {
                            itemForSelection = itemData[valueKey];
                        }
                        if (typeof itemForSelection === "function") {
                            itemForSelection = itemForSelection();
                        }
                        selectedValues.push(itemForSelection);
                    }
                }
                if (ko.isObservable(valueAccessor().selectedItems)) {
                    if (isArray)
                        valueAccessor().selectedItems(selectedValues);
                    else
                        valueAccessor().selectedItems(selectedValues.length > 0 ? selectedValues[0] : null);
                }
                else {
                    updatePropertyValue(element, viewModel, selectedValues.length > 0 ? selectedValues[0] : null);
                }
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var combo = $(element),
               options = valueAccessor(),
               selectedItems = valueAccessor().selectedItems;

        }
    };

    ko.bindingHandlers.igComboSelection = {
        update: function (element, valueAccessor) {
            selectItems($(element), valueAccessor().selectedItems);
        }
    };

    ko.bindingHandlers.igComboList = {
        init: function (element, valueAccessor) {
            var combo = valueAccessor().combo,
				$comboList = combo.igCombo("listItems"),
				options = valueAccessor().options,
                dataSource = ko.utils.unwrapObservable(valueAccessor().dataSource),
				i;

            if (dataSource) {
                for (i = 0; i < $comboList.length; i++) {
                    ko.applyBindingsToNode($comboList[i], {
                        igComboItem: {
                            combo: combo,
                            value: dataSource[i],
                            index: i,
                            options: options
                        }
                    }, dataSource[i]);
                }
            }
        },
        update: function (element, valueAccessor) {
            var combo = $(valueAccessor().combo),
				listLength = combo.igCombo("listItems").length,
				options = valueAccessor().options,
        		dataSource = ko.utils.unwrapObservable(valueAccessor().dataSource),
				$comboList, i;

            if (listLength !== dataSource.length) {
                combo.one("igcomboitemsrendered", function () {
                    $comboList = combo.igCombo("listItems");
                    if (dataSource) {
                        for (i = 0; i < $comboList.length; i++) {
                            ko.applyBindingsToNode($comboList[i], {
                                igComboItem: {
                                    combo: combo,
                                    value: dataSource[i],
                                    index: i,
                                    options: options
                                }
                            }, dataSource[i]);
                        }
                    }
                    selectItems(combo, valueAccessor().selectedItems);
                });
                // N.A. 8/5/2015 Bug #203826 Set datasource, cause in this case it is analyzed and then the dataBind happens.
                // This necessay in cases, when data source was empty array initially.
                combo.igCombo("option", "dataSource", dataSource);
            }
        }
    };

    ko.bindingHandlers.igComboItem = {
        update: function (element, valueAccessor) {
            var combo = valueAccessor().combo,
                textKey = valueAccessor().options.textKey,
                valueKey = valueAccessor().options.valueKey,
                item, index, dsItem;

            if (valueKey === undefined && textKey === undefined || combo.igCombo("itemsFromIndex", index) == null) {
                return;
            }
            index = valueAccessor().index;
            dsItem = valueAccessor().value;
            item = combo.igCombo("itemsFromIndex", index) == null ? null : combo.igCombo("itemsFromIndex", index).element;
            combo.data("igCombo")._updateItem(item, dsItem);
            combo.data("igCombo")._updateInputValues();
        }
    };

    ko.bindingHandlers.igComboVisible = {
        update: function (element, valueAccessor) {
            var visible = valueAccessor(),
                combo = $(element);
            if (!ko.isObservable(visible)) {
                return;
            }
            combo.css("display", visible() ? "inline-block" : "none");
        }
    };

    function selectItems(combo, selectedItems) {
        var valueKey = combo.igCombo("option", "valueKey"),
			selectedValues = [],
			index, item, value;

        if (typeof selectedItems != 'undefined' && selectedItems != null && selectedItems.toString() != '') {
            if (typeof selectedItems === 'function')
                selectedItems = ko.utils.unwrapObservable(selectedItems);
            if (typeof selectedItems != 'undefined' && selectedItems != null && selectedItems.toString() != '') {
                if (Array.isArray(selectedItems)) {
                    for (index = 0; index < selectedItems.length; index++) {
                        item = selectedItems[index];
                        if (!item) return;;

                        if (typeof item === "function") {
                            item = item();
                        }
                        if (typeof item === "object") {
                            value = item[valueKey];
                        } else {
                            value = item;
                        }
                        selectedValues.push(value);
                    }
                } else {
                    selectedValues.push(selectedItems);
                }


                combo.igCombo("value", selectedValues);
            } else combo.igCombo("clearInput");
        } else combo.igCombo("clearInput");
    }

    function updatePropertyValue(element, viewModel, newValue) {
        var reg = new RegExp("igCombo" + "\\s*:\\s*(?:{.*,?\\s*selectedItems\\s*:\\s*)?([^{},\\s]+)"),
			key,
			res = $(element).attr('data-bind').match(reg);
        if (res) {
            key = res[1];
            if (typeof viewModel[key] !== 'undefined') {
                viewModel[key] = newValue;
            }
        }
    }
}(jQuery));