define(['durandal/app', 'plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common'],
    function (app, dialog, ko, logger, managerAuth, common) {

        var customSearchTreeView = function (vm, controllerName, nodePath) {
            var _this = this;

            this.nodePath = nodePath.split(";");

            //Durandal Methods
            this.compositionComplete = function () {

                //jsTree
                sel = '#jsTree';

                var url = managerAuth.getServiceAddress(controllerName + '/GetBmEntityProperties');
                var rootTypeName = isNullOrEmpty(vm.rootBmTypeName) ? vm.rootDataTypeName : vm.rootBmTypeName;
                $(sel).jstree({
                    'core': {
                        'multiple': false,
                        'data': {
                            'url': function (node) {
                                if (node.id === "#") {
                                    return url + "?entityName=" + rootTypeName + "&parentDataPath=";
                                }
                                else {
                                    return url + "?entityName=" + node.original.entityName + "&parentDataPath=" + node.id;
                                }
                            },
                            'data': function (node) {
                                return { 'id': node.id };
                            }
                        }
                    },
                    "types": {
                        "default": {
                            "icon": "fa fa-database"
                        }
                    },
                    "plugins": ["types", "wholerow"]
                });

                $('#jsTree').on('loaded.jstree', function () {
                    if (_this.nodePath != "") {
                        _this.loadNode(_this.nodePath[0]);
                    }
                }).jstree();

                $('#jsTree').on('after_open.jstree', function (e, data) {
                    if (_this.nodePath != "" && _this.nodePath.count() > 0) {
                        var i = data.node.id;
                        if (i == _this.nodePath[0]) {
                            _this.nodePath.removeAt(0);
                            if (_this.nodePath.count() > 1) {
                                _this.loadNode(_this.nodePath[0]);
                            }
                            else if (_this.nodePath.count() == 1) {
                                $('#jsTree').jstree('select_node', _this.nodePath[0]);
                                _this.nodePath.removeAt(0);
                            }
                        }
                    }
                }).jstree();
            };

            this.loadNode = function (nodeId) {
                $('#jsTree').jstree('open_node', nodeId, null, false);
            };

            this.activate = function () {

            };

            //buttons
            this.ok = function () {
                var selected = $("#jsTree").jstree("get_node", $("#jsTree").jstree("get_selected"));

                if (!selected || !selected.original.enabled)
                    return;

                dialog.close(this, selected, $("#jsTree").jstree("get_path", selected, ";", true));
            };

            this.cancel = function () {
                dialog.close(this, null, null);
            }
        };

        customSearchTreeView.show = function (vm, controllerName, nodePath) {
            return dialog.show(new customSearchTreeView(vm, controllerName, nodePath));
        };

        return customSearchTreeView;
    });

