define(['durandal/system', 'plugins/router', 'plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common', 'durandal/app'],
    function (system, router, dialog, ko, logger, managerAuth, common, app) {

        var visibleModalTypeClass = "";

        return {
            title: "",
            modalTypeClass: ko.observable(""),
            viewName: "",
            viewModelName: "",
            parentDataContext: null,
            options: null,
            inUse: false,
            defaultOptions: ['Ok'],

            ///////////////////////
            // method: DURANDAL: activate()
            ///////////////////////
            activate: function () {
            },

            ///////////////////////
            // method: DURANDAL: activate()
            ///////////////////////
            canActivate: function () {
                return true;
            },

            ///////////////////////
            // method: DURANDAL: activate()
            ///////////////////////
            deactivate: function () {
                $("body").removeClass("page-full-width");
            },

            ///////////////////////
            // method: DURANDAL: activate()
            ///////////////////////
            canDeactivate: function () {
                return true;
            },

            ///////////////////////
            // method: DURANDAL: compositionComplete()
            ///////////////////////
            compositionComplete: function () {
                //$(".modal-body > .durandal-wrapper > .page-content").removeClass("page-content");
                $("body").addClass("page-full-width");
            },

            ///////////////////////
            // method: showModal()
            ///////////////////////
            showModalByRoute: function (href, parentDataContext) {
                this.showModal(href, parentDataContext)
            },

            hide: function (value) {
                this.modalTypeClass(visibleModalTypeClass + (value === true ? " hide" : ""));
                if (value === false) this.adjustDialogPosition();
            },
            adjustDialogPosition: function () {
                var $child = $(dialog.getDialog(this).settings.child);
                //Setting a short timeout is need in IE8, otherwise we could do this straight away
                setTimeout(function () {
                    //We will clear and then set width for dialogs without width set 
                    if (!$child.data("predefinedWidth")) {
                        $child.css({ width: '' }); //Reset width
                    }
                    var width = $child.outerWidth(false);
                    var height = $child.outerHeight(false);
                    var windowHeight = $(window).height();
                    var constrainedHeight = Math.min(height, windowHeight);

                    $child.css({
                        'margin-top': (-constrainedHeight / 2).toString() + 'px',
                        'margin-left': (-width / 2).toString() + 'px'
                    });

                    if (!$child.data("predefinedWidth")) {
                        //Ensure the correct width after margin-left has been set
                        $child.outerWidth(width);
                    }

                    if (height > windowHeight) {
                        $child.css("overflow-y", "auto");
                    } else {
                        $child.css("overflow-y", "");
                    }

                    $($child.host).css('opacity', 1);
                    $child.css("visibility", "visible");

                    $child.find('.autofocus').first().focus();
                }, 1);
            },

            ///////////////////////
            // method: showModal()
            ///////////////////////
            showModal: function (moduleId, parentDataContext, title, options, modalType) {
                this.inUse = true;

                var that = this;                
                this.viewModelName = moduleId;
                this.viewName = moduleId.replace("viewmodels", "views");
                this.parentDataContext = parentDataContext;
                this.options = options || this.defaultOptions


                if (isNullOrEmpty(modalType) == true) {
                    visibleModalTypeClass = 'modal-dialog modal-dialog-default';
                    this.modalTypeClass(visibleModalTypeClass);
                }
                else {
                    visibleModalTypeClass = 'modal-dialog modal-dialog-' + modalType.toLowerCase();
                    this.modalTypeClass(visibleModalTypeClass);
                }


                if (isNullOrEmpty(title) == true) {
                    $.each(router.routes, function (index, value) {
                        if (value.moduleId == moduleId.toLowerCase()) {
                            that.title = value.title;
                        }
                    });
                }
                else {
                    this.title = title;
                }
                                
                return dialog.show(this, this.parentDataContext);
            },

            ///////////////////////
            // method: UI_Fechar_Click()
            ///////////////////////
            UI_Close_Click: function () {
                this.inUse = false;
                var contexts = { dialogResult: 'Close', parentDataContext: this.parentDataContext }                
                dialog.close(this, contexts);
            },

            UI_selectOption: function (dialogResult) {
                //Lookup checking
                if (dialogResult == "Ok" && this.parentDataContext && this.parentDataContext.lookupInfo && this.parentDataContext.lookupInfo.vm) {
                    if (typeof this[this.internalUIs[0]] == 'function') {
                        var innerVM = this[this.internalUIs[0]]();                        
                        if (innerVM && (typeof innerVM.getSpecializedLookupItems === 'function')) {
                            var result = innerVM.getSpecializedLookupItems();
                            if (result === false)
                                return;
                            else if (result && result.length == 0) {
                                app.showMessage('Nenhuma informação foi selecionada!', 'Alerta', ['Ok']);
                                return;
                            }
                        }
                    }
                }
                this.inUse = false;
                var contexts = { dialogResult: dialogResult, parentDataContext: this.parentDataContext }
                dialog.close(this, dialogResult);
            }

        }
    });