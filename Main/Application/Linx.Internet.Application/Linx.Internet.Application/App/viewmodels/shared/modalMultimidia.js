define(['plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common'],
    function (dialog, ko, logger, managerAuth, common) {
        //////////////////////
        // class: ReturnModalVM
        //////////////////////
        var ReturnModalVM = function (p) {
            var self = this;
            self.UrlThumbnail = p.UrlThumbnail;
            self.UidDocumentos = p.UidDocumentos;
            self.UI_MULTIMIDIA_VM = p.UI_MULTIMIDIA_VM;
        };

        ///////////////////////
        // event: UI_btnOK_Click()
        ///////////////////////
        var __UI_btnOK_Click = function () {
            var r = new ReturnModalVM({
                UrlThumbnail: (this.MULTIMIDIA_VM().length > 0 ? this.MULTIMIDIA_VM()[0].UrlThumbnail() + "&" + managerAuth.META_HASH + "&nocache=" + Math.uuid(15) : this.urlNoImage),
                UidDocumentos: this.getMultimidiaTable(),
                UI_MULTIMIDIA_VM: ko.toJS(this.MULTIMIDIA_VM)
            });

            dialog.close(this, r);
        };

        ///////////////////////
        // event: UI_btnCancel_Click()
        ///////////////////////
        var __UI_btnCancel_Click = function () {
            var r = new ReturnModalVM({
                UrlThumbnail: (this.MULTIMIDIA_VM().length > 0 ? this.MULTIMIDIA_VM()[0].UrlThumbnail() + "&" + managerAuth.META_HASH + "&nocache=" + Math.uuid(15) : this.urlNoImage),
                UidDocumentos: this.getMultimidiaTable(),
                UI_MULTIMIDIA_VM: ko.toJS(this.MULTIMIDIA_VM)
            });

            dialog.close(this, r);
        };

        var __KO_afterRenderImage = function (data, element, url) {
            //alert(url);
            var e = $(data[1]);

            $(e).attr('data-src', url)

            $(e).lazy({
                bind: "event",
                delay: 0,
                //visibleOnly: true,
                afterLoad: function (element) {
                    if (e[0].height > e[0].width) {
                        $(e[0]).addClass('fitHeightInContent');
                    } else {
                        $(e[0]).addClass('fitWidthInContent');
                    }
                },
                onError: function (element) {
                    console.log("image loading error: " + element.attr("data-src"));
                }
            });
        }

        var __KO_getBrowser = function () {
            return $.browser.name;
        }

        var vm = {
            common: common,
            nomeTabela: "",
            idChave: 0,
            uidChave: "00000000-0000-0000-0000-000000000000",
            urlNoImage: managerAuth.getServiceAddress("LinxFrameworkMultimidia", "Linx.Framework.BV") + "/GetMediaThumbnail" + "?uidDocumento=00000000-0000-0000-0000-000000000000&uidGrupoAcesso=00000000-0000-0000-0000-000000000000&uidEmpresa=" + managerAuth.getCompanyId() + "&uidGrupoEconomico=" + managerAuth.loginInfo.UidGrupoEconomico + "&idAmbiente=" + managerAuth.getEnvironmentId() + "&uidUsuario=" + managerAuth.loginInfo.UidUsuario,
            parentMultimidias: false,

            // bindings
            MULTIMIDIA_VM: ko.observableArray(),
            MULTIMIDIA_VM_COMPUTED: function (tipoMidia) {
                var self = this;
                return ko.utils.arrayFilter(self.MULTIMIDIA_VM(), function (item) {
                    return (item.TipoMidia() == tipoMidia) && (item.lxDeleted() == false);
                });
            },
            enabledForEditing: ko.observable(false),

            // events
            UI_btnDeleteMidia_Click: function (o) {
                var self = this;
                if (o.lxNew() == true) {
                    common.showProcess('.modal-body');

                    // solicia a api as midias cadastradas
                    $.ajax({
                        type: 'DELETE',
                        messageUser: "Manutenção de midias",
                        globalError: true,
                        url: managerAuth.getServiceAddress('linxframeworkmultimidia', 'Linx.Framework.BV') + '/deletemedia?uidDocumento=' + o.UidDocumento(),
                        headers: managerAuth.getHeaders(),
                        dataType: 'json',
                        async: true,
                        cache: false,

                        error: function (jqXHR, textStatus, errorThrown) {
                            common.closeProcess('.modal-body');
                        },

                        success: function (data) {
                            self.MULTIMIDIA_VM.remove(o);
                            common.closeProcess('.modal-body');
                        }
                    });
                }
                else {
                    self.MULTIMIDIA_VM.remove(o);
                }
            },
            UI_btnCancel_Click: __UI_btnCancel_Click,
            UI_btnOK_Click: __UI_btnOK_Click,
            KO_afterRenderImage: __KO_afterRenderImage,
            KO_getBrowser: function () {
                return $.browser.name;
            },

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
            canDeactivate: function () {
                return true;
            },

            ///////////////////////
            // method: DURANDAL: compositionComplete()
            ///////////////////////
            compositionComplete: function () {
                this.getMultimedia();
                this.configPluginUpload();

                $.ajaxSetup({
                    headers: managerAuth.getHeaders(),
                    beforeSend: function (xhr, settings) {
                        if (settings.url.length >= 2048) {
                            xhr.abort('Foi excedido o limite de caracteres para a pesquisa!');
                            return false;
                        }

                        return xhr;
                    }
                });
            },

            ///////////////////////
            // method: showModal()
            ///////////////////////
            showModal: function (tabela, value, vm, data) {
                // limpa lista de midias
                this.MULTIMIDIA_VM.removeAll();
                this.parentMultimidias = false;

                if (getAbsoluteValue(data.TableMedia) == null) {
                    data.UI_MULTIMIDIA_VM = null;
                }

                if (data.UI_MULTIMIDIA_VM != null) {
                    //this.MULTIMIDIA_VM(ko.mapping.fromJS(data.UI_MULTIMIDIA_VM));
                    this.MULTIMIDIA_VM = ko.mapping.fromJS(data.UI_MULTIMIDIA_VM);
                    this.parentMultimidias = true;
                }

                this.nomeTabela = tabela;
                //this.parentRoot = vm
                this.enabledForEditing(vm.enabledForEditing());

                // tratamento id (int ou guid)
                if (typeof (value) == 'function')
                    value = value();

                if (typeof (value) === "string" && value.length != 36) {
                    value = parseInt(value);
                }

                if (typeof (value) === "string") {
                    this.uidChave = value;
                }
                else {
                    this.idChave = parseInt(value);
                }

                return dialog.show(this);
            },

            ///////////////////////
            // method: getMultimedia()
            ///////////////////////
            getMultimedia: function () {
                var that = this
                if (this.parentMultimidias == true)
                    return;

                common.showProcess('.modal-body');

                // solicia a api as midias cadastradas
                $.ajax({
                    type: 'GET',
                    messageUser: "Accesso as midias",
                    globalError: true,
                    headers: managerAuth.getHeaders(),
                    url: managerAuth.getServiceAddress('linxframeworkmultimidia', 'Linx.Framework.BV') + '/getmultimedia',
                    headers: managerAuth.getHeaders(),
                    data: {
                        nomeTabela: this.nomeTabela,
                        idChave: this.idChave,
                        uidChave: this.uidChave,
                        tipoDocumento: 0,
                        uidUsuario: managerAuth.loginInfo.UidUsuario
                    },
                    dataType: 'json',
                    async: true,
                    cache: false,

                    error: function (jqXHR, textStatus, errorThrown) {
                        common.closeProcess('.modal-body');
                    },

                    success: function (data) {
                        common.closeProcess('.modal-body');
                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            item.lxDeleted = false;
                            item.lxNew = false;

                            if (item.UrlServiceBus.length > 0 && item.TipoMidia == 1) // imagem
                            {
                                item.UrlThumbnailUI = item.UrlServiceBus + "?w=70";
                                item.Url = item.UrlServiceBus
                            }
                            else {
                                item.UrlThumbnailUI = item.UrlThumbnail
                            }

                            that.MULTIMIDIA_VM.push(ko.mapping.fromJS(item));
                        }
                    }
                });
            },

            ///////////////////////
            // method: configPluginUpload()
            ///////////////////////
            configPluginUpload: function () {
                var that = this;
                // configura o plugin de upload
                var fu = $('#fileupload').fileupload();

                $(fu).fileupload('option', {
                    maxFileSize: 5242880, // 5 megas
                    limitMultiFileUploads: 10,
                    limitConcurrentUploads: 2,
                    resizeMaxWidth: 1920,
                    resizeMaxHeight: 1200,
                    autoUpload: true
                });

                //$(fu).bind('fileuploadadd', function (e, data) {
                //    alert(data)
                //})

                $(fu).bind('fileuploadsubmit', function (e, data) {
                    var selTipoMidia = $('#selTipoMidia');
                    data.formData = { TipoMidia: selTipoMidia.val(), JExpression: null, NomeTabela: that.nomeTabela };
                });

                $(fu).bind('fileuploadprocessdone', function (e, data) {
                    var jqXHR = data.submit()
                        .success(function (result, textStatus, jqXHR) {
                            var dto = result.files[0].midia;
                            dto.lxNew = true;
                            dto.lxDeleted = false;

                            dto.UrlThumbnailUI = dto.UrlThumbnail
                            result.files[0].loadingUrl = common.getUrlLoadingImage();

                            // adiciona o item no comeco do array
                            that.MULTIMIDIA_VM.push(ko.mapping.fromJS(dto));
                        });
                });

                $(fu).bind('fileuploadfinished', function (e, data) {
                    $("img.lx_linkimg[src$='loading-spinner-grey.gif'][data-src]").lazy({
                        bind: "event",
                        delay: 0,
                        removeAttribute: true,
                        //visibleOnly: true,
                        afterLoad: function (e) {
                            if (e[0].height > e[0].width) {
                                $(e[0]).addClass('fitHeightInContent');
                            } else {
                                $(e[0]).addClass('fitWidthInContent');
                            }
                        },
                        onError: function (element) {
                            console.log("image loading error: " + element.attr("data-src"));
                        },
                    });
                })

                $(fu).bind('fileuploaddestroy', function (e, data) {
                    common.showProcess('.modal-body');
                })

                $(fu).bind('fileuploaddestroyed', function (e, data) {
                    that.MULTIMIDIA_VM.remove(function (item) {
                        return item.UrlDelete() == data.url
                    })
                    common.closeProcess('.modal-body');
                })
            },

            getMultimidiaTable: function () {
                var that = this;
                var retorno = '';
                if (that.enabledForEditing()) {
                    for (var i = 0; i < that.MULTIMIDIA_VM().length; i++) {
                        var item = that.MULTIMIDIA_VM()[i];

                        if (item.lxDeleted() == false)
                            retorno += item.UidDocumento() + ',';
                    }

                    // apaga todas as midias
                    if (retorno.length == 0) {
                        retorno = "00000000-0000-0000-0000-000000000000"
                    }
                }

                return retorno;
            },

        }

        return vm;
    });