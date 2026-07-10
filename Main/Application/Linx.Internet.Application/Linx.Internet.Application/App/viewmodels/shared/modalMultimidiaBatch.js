define(['plugins/dialog', 'durandal/app', 'knockout', 'services/logger', 'managers/__auth', 'common'],
    function (dialog, app, ko, logger, managerAuth, common) {
        //////////////////////
        // class: ReturnModalVM
        //////////////////////
        var ReturnModalVM = function (p) {
            var self = this;
        };

        //////////////////////
        // class: ArquivoItemVM
        //////////////////////
        var ArquivoItemVM = function (p) {
            var self = this;
            this.nomeArquivo = p.nomeArquivo,
            this.tamanhoArquivo = p.tamanhoArquivo,
            this.previewArquivo = p.previewArquivo,
            this.tipoArquivo = p.tipoArquivo,
            this.objUpload = p.objUpload,
            this.ativo = p.ativo
        };

        //////////////////////
        // class: ConfigVM
        //////////////////////
        var ConfigVM = function (p) {
            var self = this;
            this.modo = p.modo, //dominio: 1-Separador 2-Posicional
            this.modosUso = p.modosUso,
            this.caracterDelimitador = p.caracterDelimitador //ex: _ , -
        };

        //////////////////////
        // class: MapVM
        //////////////////////
        var MapVM = function (p) {
            var self = this;
            this.index = p.index, //ex: sequencial unico
            this.nomeEntidade = p.nomeEntidade, //ex: Produto
            this.nomePropriedade = p.nomePropriedade, //ex: Codigo
            this.indiceDelimitador = p.indiceDelimitador, // ex: caso modo seja 1-Separador o indice do array
            this.inicioCaracter = p.inicioCaracter, // ex: caso seja 2-Posicional referecia da quantidade de caracteres
            this.fimCaracter = p.fimCaracter // ex:
            this.valor = p.valor // ex:

        };

        //////////////////////
        // class: EntidadeVM
        //////////////////////
        var EntidadeVM = function (p) {
            var self = this;
            this.ref = p.ref,
            this.nome = p.nome,
            this.propriedades = p.propriedades
        };

        //////////////////////
        // class: PropriedadeVM
        //////////////////////
        var PropriedadeVM = function (p) {
            var self = this;
            this.ref = p.ref,
            this.nome = p.nome,
            this.tipoDado = p.tipoDado
            this.chave = p.chave
        };

        var vm = {
            common: common,
            parentDataContext: null,
            CONFIG_VM: null,
            idChave: 0,
            uidChave: "00000000-0000-0000-0000-000000000000",
            urlNoImage: managerAuth.getServiceAddress("LinxFrameworkMultimidia", "Linx.Framework.BV") + "/GetMediaThumbnail" + "?uidDocumento=00000000-0000-0000-0000-000000000000&uidGrupoAcesso=00000000-0000-0000-0000-000000000000&uidEmpresa=" + managerAuth.getCompanyId() + " &uidGrupoEconomico=" + managerAuth.loginInfo.UidGrupoEconomico + "&idAmbiente=" + managerAuth.getEnvironmentId() + "&uidUsuario=" + managerAuth.loginInfo.UidUsuario,

            ///////////////////////
            // bindings KO
            ///////////////////////
            ARQUIVO_VM: ko.observableArray(),
            MAP_VM: ko.observableArray(),
            ENTIDADES_VM: ko.observableArray(),
            //ARQUIVO_VM_COMPUTED: function (tipoMidia) {
            //    var self = this;
            //    return ko.utils.arrayFilter(self.ARQUIVO_VM(), function (item) {
            //        return (item.TipoMidia() == tipoMidia) && (item.lxDeleted() == false);
            //    });
            //},
            KO_getBrowser: function () {
                return $.browser.name;
            },

            ///////////////////////
            // events UI
            ///////////////////////
            UI_btnCancel_Click: function () {
                var r = new ReturnModalVM({
                });

                dialog.close(this, r);
            },

            UI_btnOK_Click: function () {
                var r = new ReturnModalVM({
                });

                dialog.close(this, r);
            },

            UI_btnCancel_Click: function () {
                var r = new ReturnModalVM({
                });

                dialog.close(this, r);
            },

            KO_getModoUso: function (item) {
                if (item == 1)
                    return "Por separador"
                else
                    return "Por posição"
            },

            KO_afterRenderPropriedadeTemplate: function (data, element) {
                var self = this;
                var index = element.index();
                
                // componente delimitador
                $('#del_' + index).TouchSpin({
                    min: 0,
                    max: self.MAP_VM().length,
                    initval: (index + 1),
                    verticalbuttons: true,
                    buttondown_class: "btn btn-link",
                    buttonup_class: "btn btn-link"
                });

                $('#del_' + index).on("change", function (e) {
                    var item = self.MAP_VM()[index];
                    var valInt = parseInt($('#del_' + index).val())
                    item.indiceDelimitador(valInt);
                });



                $('#pos_' + index).ionRangeSlider({
                    min: 1,
                    max: 255
                });


                var sel = $('#ent_' + index);

                $(sel).select2({
                    placeholder: "Campo",
                    openOnEnter: true,
                    width: "off",
                    escapeMarkup: function (m) {
                        return m;
                    },
                    formatSelection: function (e)
                    {
                        $(sel).attr('val_entidade', $(e.element).attr('entidade'))
                        return $(e.element).attr('entidade') + '.' + e.text;
                    }
                });

                $(sel).select2("val", "");

                $(sel).on("select2-selected", function (e) {
                    var item = self.MAP_VM()[index];

                    item.nomeEntidade($(sel).attr('val_entidade'));
                    item.nomePropriedade(e.val);

                    $(sel).select2("close");
                });
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
                this.configPluginUpload();
                
                this.configWizard();
            },

            ///////////////////////
            // method: showModal()
            ///////////////////////
            showModal: function (dataContext) {
                this.parentDataContext = dataContext;

                this.ARQUIVO_VM.removeAll();
                this.MAP_VM.removeAll();
                this.ENTIDADES_VM.removeAll();

                this.configEntidadePropriedade();
                this.configGeral();

                return dialog.show(this);
            },

            ///////////////////////
            // method: configEntidadePropriedade()
            ///////////////////////
            configEntidadePropriedade: function () {

                for (var i = 0; i < this.parentDataContext.entityNames.length; i++)
                {
                    var entityName = this.parentDataContext.entityNames[i]; // nome da entidade

                    var ent = new EntidadeVM({
                        ref: entityName,
                        nome: entityName,
                        propriedades: ko.observableArray()
                    });

                    for (var y = 0; y < this.parentDataContext.metadataInfo[entityName].length; y++)
                    {
                        var metadata = this.parentDataContext.metadataInfo[entityName][y] // objeto com  as propriedades

                        if (metadata.dataType == 'string' || metadata.dataType == 'number' || metadata.dataType == 'bool') {
                            ent.propriedades.push(new PropriedadeVM({
                                ref: metadata.key,
                                nome: (metadata.isPartOfKey == true ? metadata.headerText + ' [chave]' : metadata.headerText),
                                tipoDado: metadata.dataType,
                                chave: metadata.isPartOfKey
                            }));
                        }
                    }

                    this.ENTIDADES_VM.push(ent);
                }
            },

            ///////////////////////
            // method: configGeral()
            ///////////////////////
            configGeral: function () {

                // config fixo
                this.CONFIG_VM = ko.mapping.fromJS(new ConfigVM({
                    modo: 1,
                    modosUso: [1, 2],
                    caracterDelimitador: "_"
                }));

                // propriedades
                this.MAP_VM.push(ko.mapping.fromJS(new MapVM({
                    index: 0,
                    nomeEntidade: '',
                    nomePropriedade: '',
                    indiceDelimitador: 1,
                    quantCaracter: 0,
                    inicioCaracter: 1,
                    fimCaracter: 3
                })));

                this.MAP_VM.push(ko.mapping.fromJS(new MapVM({
                    index: 1,
                    nomeEntidade: '',
                    nomePropriedade: '',
                    indiceDelimitador: 2,
                    quantCaracter: 0,
                    inicioCaracter: 1,
                    fimCaracter: 3
                })));

                this.MAP_VM.push(ko.mapping.fromJS(new MapVM({
                    index: 2,
                    nomeEntidade: '',
                    nomePropriedade: '',
                    indiceDelimitador: 3,
                    quantCaracter: 0,
                    inicioCaracter: 1,
                    fimCaracter: 3
                })));
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
                    limitMultiFileUploads: 1000,
                    limitConcurrentUploads: 2,
                    resizeMaxWidth: 1920,
                    resizeMaxHeight: 1200,
                    //sequentialUploads: true,
                    autoUpload: false
                });

                $(fu).bind('fileuploadsubmit', function (e, data) {
                    data.formData = {
                        TipoMidia: $('#selTipoMidia').val(),
                        JExpression: that.ValidarArquivo(data, that)
                    };

                });

                $(fu).bind('fileuploadsend', function (e, data) {
                    // verifica se existe inconsistencia
                    if (data.formData.JExpression.length == 0) {
                        return false;
                    }
                });

                $(fu).bind('fileuploaddone', function (e, data) {
                    console.log('fileuploaddone');

                    var dto = data.result.files[0].midia;
                    dto.lxNew = true;
                    dto.lxDeleted = false;

                    dto.UrlThumbnailUI = dto.UrlThumbnail
                    data.result.files[0].loadingUrl = common.getUrlLoadingImage();

                    // adiciona o item no comeco do array
                    that.ARQUIVO_VM.push(ko.mapping.fromJS(dto));
                });

                $(fu).bind('fileuploadfinished', function (e, data) {
                    console.log('fileuploadfinished');
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
            },

            ///////////////////////
            // method: configWizard()
            ///////////////////////
            configWizard: function () {
                var that = this;
                $('.button-previous-wizard').hide()
                $('.button-submit').hide();

                var wiz = $('#wizWizard');

                $(wiz).bootstrapWizard({
                    tabClass: 'nav nav-pills',
                    nextSelector: '.button-next-wizard',
                    previousSelector: '.button-previous-wizard',
                    finishSelector: '.button-submit',

                    onTabClick: function (tab, navigation, index) {
                        if (index == 0)
                        {
                            return that.ValidarTab0(that);
                        }
                    },

                    onTabShow: function (tab, navigation, index) {
                        var $total = navigation.find('li').length;
                        var $current = index+1;

                        if($current >= $total) {
                            $('.button-previous-wizard').show();
                            $('.button-next-wizard').hide();
                            $('.button-submit').show();
                        } else {
                            $('.button-previous-wizard').hide();
                            $('.button-next-wizard').show();
                            $('.button-submit').hide();
                        }
                    }
                });

                $('#btnPrevious').click(function () {
                    $("table tbody.files").empty();
                    $(wiz).bootstrapWizard('previous');
                });

                $('#btnNext').click(function () {
                    if (that.ValidarTab0(that) == false)
                    {
                        return false;
                    }

                    $(wiz).bootstrapWizard('next');
                });

                $('#btnSubmit').click(function () {
                    var btnStartUpload = $("button[name='btnStartUpload']");

                    if (btnStartUpload.length == 0)
                        app.showMessage('Nenhum arquivo informado!', 'Informação', ['Ok']);

                    $(btnStartUpload).click()
                });
            },

            ///////////////////////
            // method: ValidarTab1
            ///////////////////////
            ValidarTab0: function (that) {
                // executa as consistencias
                if (that.CONFIG_VM.modo() == 1) // separador
                {
                    if (that.CONFIG_VM.caracterDelimitador().length == 0)
                    {
                        app.showMessage('Informe um caracter delimitador!', 'Informação', ['Ok']);
                        return false;
                    }

                    // verifica se existe indice duplicado
                    for (var i = 0; i < that.MAP_VM().length; i++) {
                        var item = that.MAP_VM()[i];

                        var arr1 = ko.utils.arrayFilter(that.MAP_VM(), function (item1) {
                            return item1.indiceDelimitador() == item.indiceDelimitador();
                        });

                        if (arr1.length > 1) {
                            app.showMessage('Verifique as posições informadas, existem valores duplicados!', 'Informação', ['Ok']);
                            return false;
                        }
                    }
                }
                else {

                }

                // verifica se pelo menos um campo (nomePropriedade) tem conteudo
                var arr = ko.utils.arrayFilter(that.MAP_VM(), function (item) {
                    return item.nomePropriedade() == '';
                });
                if (arr.length == that.MAP_VM().length) {
                    app.showMessage('Informe algum campo para importação!', 'Informação', ['Ok']);
                    return false;
                }

                return true;
            },

            ///////////////////////
            // method: ValidarArquivo
            ///////////////////////
            ValidarArquivo: function (data, that) {
                for (var i = 0; i < data.files.length; i++) {
                    var file = data.files[i];

                    console.log('ValidarArquivo: ' + file.name);
                    if (that.CONFIG_VM.modo() == 1) // separador
                    {
                        if (file.name.indexOf(that.CONFIG_VM.caracterDelimitador()) == -1) {
                            file.error = "mascara inválida, caracter delimitador '" + that.CONFIG_VM.caracterDelimitador() + "' não encontrado!"
                            break;
                        }

                        var arr = file.name.split(that.CONFIG_VM.caracterDelimitador());
                        if (arr.length <= 1) {
                            file.error = "mascara inválida!"
                            break;
                        }

                        for (var y = 0; y < that.MAP_VM().length; y++) {
                            var item = that.MAP_VM()[y];

                            var p = arr[item.indiceDelimitador()];
                            if (p != null) {
                                item.valor(p);
                            }
                            else{
                                item.valor(null);
                            }
                        }

                        var arr1 = ko.utils.arrayFilter(that.MAP_VM(), function (item1) {
                            return (item1.valor() == null && item1.nomePropriedade().length > 0);
                        });
                        
                        if (arr1.length > 0)
                        {
                            file.error = "mascara inválida, valor não encontrado!"
                        }

                    }
                    else if (that.CONFIG_VM.modo() == 2) // posicional
                    {

                    }
                };

                if (file.error == null) {
                    return this.FormatarJExpression(that);
                }
                return '';
            },

            FormatarJExpression: function (that)
            {
                var entidades = '';
                var expressaoEntidade = '';
                var expressaoPropriedade = '';

                // seleciona somente os nomes das entidades
                for (var y = 0; y < that.MAP_VM().length; y++) {
                    var item = that.MAP_VM()[y];
                    
                    if (entidades.indexOf(item.nomeEntidade()) == -1)
                    {
                        entidades += item.nomeEntidade() + ";"
                    }
                }


                var arrEntidade = entidades.split(';');
                for (var e = 0; e < arrEntidade.length; e++) {
                    if (arrEntidade[e].length == 0)
                        continue;

                    expressaoEntidade += arrEntidade[e] + "{";

                    var arrPropriedade = ko.utils.arrayFilter(that.MAP_VM(), function (item1) {
                        return item1.nomeEntidade() == arrEntidade[e];
                    });

                    //ProductView{ProductId#==#I234;||#ProductName#Like#Sabc%}
                    // seleciona propriedades
                    expressaoPropriedade = '';
                    for (var p = 0; p < arrPropriedade.length; p++) {
                        var item = arrPropriedade[p];
                        expressaoPropriedade += item.nomePropriedade() + "#==#S" + item.valor() + ";"

                        if (p > 0)
                            expressaoPropriedade += "&&"
                    }

                    expressaoEntidade += expressaoPropriedade + "}";
                }

                //alert(expressaoEntidade)
                return expressaoEntidade;
            }
        }

        return vm;
    });