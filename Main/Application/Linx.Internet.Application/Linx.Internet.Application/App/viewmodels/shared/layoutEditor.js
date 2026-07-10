define(['plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common', 'viewmodels/shared/layoutConfiguration'],
    function (dialog, ko, logger, managerAuth, common, layoutConfig) {

        var vm = {
            activate: function () {
            },
            canActivate: function () {
                return true;
            },
            canDeactivate: function () {
                return true;
            },
            compositionComplete: function () {
            },

            stopListener: false,

            title: ko.observable('Editor de Layout'),

            currentItem: ko.observable({
                Name: ko.observable(''),
                DisplayName: ko.observable(''),
                ColumnSpan: ko.observable(1),
                Visible: ko.observable(false),
                Editable: ko.observable(false),
            }),

            currentNode: ko.observable({}),

            parentVM: ko.observable({}),

            layout: ko.observable({}),

            layoutToSave: ko.observable({}),

            parseLayoutToTree: function (layout) {
                var me = this;
                var ret = [];
                if (layout.length === undefined) {
                    ret.push({
                        text: layout.Name,
                        children: layout.Items ? me.parseLayoutToTree(layout.Items) : [],
                        state: { opened: true, disabled: true },
                        data: layout
                    });
                }
                else {
                    for (var idx in layout) {
                        var item = layout[idx];
                        if (item.Name.indexOf("_dGrid") < 0) {
                            if (item.Name.indexOf("TabControl") >= 0)
                                var name = "TabControl";
                            else
                                var name = item.DisplayName ? item.DisplayName : item.Name;
                            ret.push({
                                text: name,
                                children: item.Items ? me.parseLayoutToTree(item.Items) : [],
                                state: { opened: true },
                                data: item
                            });
                        }
                    }
                }
                return ret;
            },
            showEditor: function (context) {
                var me = this;
                me.parentVM(context);
                me.layout(context.flattenLayout());
                me.layoutToSave(context.layout());

                require(['text!views/shared/layoutEditor.html'], function (html) {
                    $.jsPanel({
                        theme: 'none',
                        contentSize: { width: 1100, height: 500 },
                        headerRemove: true,
                        resizeit: true,
                        draggable: {
                            handle: "#layout-editor div.header",
                            opacity: 0.8
                        },
                        content: html,
                        callback: function (panel) {

                            $('#layout-editor .layout-tree').jstree({
                                core: {
                                    data: me.parseLayoutToTree(context.layout()),
                                    themes: {
                                        animation: false,
                                        stripes: false,
                                        ellipsis: true,
                                        dots: false,
                                        icons: false
                                    }
                                }
                            }).on('changed.jstree', function (e, data) {
                                me.currentNode(data.node);
                                var item = me.layout()[data.node.data.Name];
                                if (item) {
                                    me.stopListener = true;
                                    me.currentItem().Name(item.Name);
                                    me.currentItem().ColumnSpan(item.ColumnSpan);
                                    me.currentItem().DisplayName(item.DisplayName);
                                    me.currentItem().Visible(item.Visible);
                                    if (data.node.data.Name.indexOf('_ti') < 0) {
                                        me.currentItem().Editable(true);
                                    } else {
                                        me.currentItem().Editable(false);
                                    }
                                    me.stopListener = false;
                                }
                            });
                            //$('#layout-editor .layout-tree').css({ 'overflow': 'auto' });
                            ko.applyBindings(vm, document.getElementById('layout-editor'));

                            $('#layout-editor span.fa-close').on('click', function () { panel.close(); });
                            $('#layout-editor span.fa-save').on('click', function () {
                                panel.close();
                                var saveAs = false;
                                var vmParent = me.parentVM();
                                var layoutNameView = vmParent.viewName;
                                layoutConfig.showModal(vmParent, me, layoutNameView, saveAs).then(function (refreshSource, selectedIdLayout) {
                                    if (typeof selectedIdLayout !== "undefined") {
                                        require(['viewmodels/shared/customLayoutForm'],
                                            function (customLayout) {
                                                customLayout.currentLayoutId(selectedIdLayout);
                                                customLayout.applyLayout();
                                            });
                                    }
                                });
                            });

                            $('#layout-editor span.fa-copy').on('click', function () {
                                panel.close();
                                var saveAs = true;
                                var vmParent = me.parentVM();
                                var layoutNameView = vmParent.viewName;
                                layoutConfig.showModal(vmParent, me, layoutNameView, saveAs).then(function (refreshSource, selectedIdLayout) {
                                    if (typeof selectedIdLayout !== "undefined") {
                                        require(['viewmodels/shared/customLayoutForm'],
                                            function (customLayout) {
                                                customLayout.currentLayoutId(selectedIdLayout);
                                                customLayout.applyLayout();
                                            });
                                    }
                                });
                            });

                        }
                    }).content.addClass('linx-layout-editor');
                });
            }
        }

        var updateLayoutToSave = function (itemJson) {

            function changeValuesItems(obj) {
                if (obj.Items) obj.Items.forEach(function (item) {
                    if (itemJson.Name == item.Name) {
                        item.ColumnSpan = itemJson.ColumnSpan;
                        item.DisplayName = itemJson.DisplayName
                        item.Visible = itemJson.Visible;

                        return false;
                    }
                    else if (item.Items) {
                        return changeValuesItems(item);
                    }
                })
            }

            if (vm.layoutToSave().Items) vm.layoutToSave().Items.forEach(function (item) {
                if (itemJson.Name == item.Name) {
                    item.ColumnSpan = itemJson.ColumnSpan;
                    item.DisplayName = itemJson.DisplayName
                    item.Visible = itemJson.Visible;

                    return false;
                }
                else if (item.Items) {
                    return changeValuesItems(item);
                }
            });
        }

        vm.currentItem().Visible.subscribe(function (newValue) {
            if (vm.stopListener == false) {
                vm.layout()[vm.currentItem().Name()].Visible = newValue;
                vm.parentVM().flattenLayout(vm.layout());
                updateLayoutToSave(vm.layout()[vm.currentItem().Name()]);
            }
        });

        vm.currentItem().DisplayName.subscribe(function (newValue) {
            if (vm.stopListener == false) {
                $('#layout-editor .layout-tree').jstree(true).rename_node(vm.currentNode(), newValue);
                vm.layout()[vm.currentItem().Name()].DisplayName = newValue;
                vm.parentVM().flattenLayout(vm.layout());
                updateLayoutToSave(vm.layout()[vm.currentItem().Name()]);
            }
        });

        vm.currentItem().ColumnSpan.subscribe(function (newValue) {
            if (vm.stopListener == false) {
                if (vm.currentItem().Name() != "") {
                    vm.layout()[vm.currentItem().Name()].ColumnSpan = newValue;
                    vm.parentVM().flattenLayout(vm.layout());
                    updateLayoutToSave(vm.layout()[vm.currentItem().Name()]);
                }
            }
        });

        return vm;
    });