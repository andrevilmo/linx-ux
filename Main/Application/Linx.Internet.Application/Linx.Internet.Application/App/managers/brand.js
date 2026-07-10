define(['durandal/system', 'durandal/app', 'services/logger', 'managers/__auth', 'managers/brand', 'common', 'plugins/router'],
    function (system, app, logger, managerAuth, managerBrand, common, router) {
        //////////////////////
        // class: ModuleItemVM
        //////////////////////
        var BrandItemVM = function (p) {
            this.id = p.id,
            this.cod = p.cod,
            this.text = p.text,
            this.html = p.html,
            this.html_select2 = p.html_select2
        }

        return {
            BRANDS: [],
            VMs: [],
            //BRANDS_VM: [],

            ///////////////////////
            // method: buildUrlRede()
            ///////////////////////
            buildUrlRede: function (width, height, numRede) {
                width = convertToString(width);
                height = convertToString(height);

                var paramWidth = (width.length > 0 ? "&width=" + width : '');
                var paramheight = (height.length > 0 ? "&height=" + height : '');
                var paramCrop = "&crop=(0,0," + (38 + (numRede.length * 19)) + "," + height + ")"

                return managerAuth.buildRootUrl('lib/linx/img/redes/logo-rede-vazio.png?' + managerAuth.META_HASH + paramWidth + paramheight + '&watermark=rede&name=' + numRede + paramCrop);
            },

            ///////////////////////
            // method: buildUrlRedes()
            ///////////////////////
            buildUrlRedes: function (width, height) {

                width = convertToString(width);
                height = convertToString(height);

                var paramWidth = (width.length > 0 ? "&width=" + width : '');
                var paramheight = (height.length > 0 ? "&height=" + height : '');
                var paramCrop = "&crop=(25,0,170," + height + ")"

                return managerAuth.buildRootUrl('lib/linx/img/redes/logo-redes.png?' + managerAuth.META_HASH + paramWidth + paramheight + paramCrop);
            },

            ///////////////////////
            // method: loadBrands()
            ///////////////////////
            loadBrands: function () {
                var that = this;
                var dfd = $.Deferred();

                var cacheKey = common.getCachePrefixEnvironment('API', 'LinxFrameworkRede/GetTbcBandeiraRedeList', managerAuth.loginInfo.CacheKey);
                var cacheObj = $.ezstorage.get(cacheKey);

                var cacheKeyHash = common.getCachePrefixEnvironment('HASH', 'LinxFrameworkRede/GetTbcBandeiraRedeList', managerAuth.loginInfo.CacheKey);
                var cacheValueHash = $.ezstorage.get(cacheKeyHash);

                if (cacheValueHash == null || cacheObj == null) {
                    system.log('Main: Loading Brands...');

                    // dados vazio
                    if (cacheObj == null)
                        cacheValueHash = null;
                    else
                        // contem dados mas o "hash" expirou, forca chamar a api novamente
                        cacheValueHash = cacheObj.hash;

                    var environmentInfo = [];
                    for (var i = 0; i < managerAuth.loginInfo.Ambientes.length; i++) {
                        var item = managerAuth.loginInfo.Ambientes[i];
                        environmentInfo.push({ Hash: cacheValueHash, EnvironmentId: item.IdTcsAmbiente, ApplicationUid: item.UidAplicacao, CompanyUid: item.UidEmpresa, AplicativeId: item.IdTcsAplicativo, IdLoja: managerAuth.idLoja });
                    }

                    return $.ajax({
                        type: 'POST',
                        message: "Buscando Redes",
                        messageUser: "Acesso as redes/bandeiras configuradas",
                        headers: managerAuth.getHeaders(),
                        globalError: true,
                        url: managerAuth.getServiceAddress('LinxFrameworkRede', 'Linx.Framework.BV') + '/TbcBandeiraRedeMultiEnvironment',
                        data: JSON.stringify(environmentInfo),
                        contentType: "application/json",
                        async: true,
                        cache: false,
                        success: function (data, textStatus, response) {
                            var cacheHeaderHash = (response.getResponseHeader('cacheHash') == null ? '' : response.getResponseHeader('cacheHash'));
                            var obj = { hash: cacheHeaderHash, value: data };

                            if (cacheHeaderHash == cacheValueHash) {
                                // conteudo vazio vindo da api
                                obj.value = cacheObj.value;
                            }

                            // armazena em cache os dados e o hash
                            $.ezstorage.set(cacheKeyHash, cacheHeaderHash, { expires: 1 })
                            $.ezstorage.set(cacheKey, obj, { expires: 90 })

                            that.BRANDS = obj.value;
                            that.loadBrandsVM();

                            dfd.promise();
                        }
                    });
                }
                else {
                    system.log('Main: Loading Brands... [Storage]');
                    that.BRANDS = cacheObj.value;
                    that.loadBrandsVM();

                    return dfd.resolve();
                }

            },

            setDefaultBrand: function (idTcsAmbiente) {
                var COD_BANDEIRA_REDE = managerAuth.getParameter("BANDEIRA_REDE_PADRAO", idTcsAmbiente);

                if (isNullOrEmpty(COD_BANDEIRA_REDE))
                    COD_BANDEIRA_REDE = '';

                var vm = $.grep(this.VMs, function (element, index) { return element.IdTcsAmbiente == idTcsAmbiente });

                if (vm.length > 0) {
                    var brandItem = $.grep(this.BRANDS, function (element, index) { return element.IdTcsAmbiente == idTcsAmbiente && element.CodBandeiraRede.trim() == COD_BANDEIRA_REDE.trim() });
                    if (brandItem.length > 0) {
                        vm[0].IdBandeiraRedeDefault = brandItem[0].IdBandeiraRede;
                        vm[0].BrandDefault = brandItem[0];
                    }
                    else {
                        vm[0].IdBandeiraRedeDefault = '';
                        vm[0].BrandDefault = [];
                    }
                }
            },

            getDefaultBrand: function (idTcsAmbiente) {
                if (!idTcsAmbiente) {
                    idTcsAmbiente = 0;

                    if (router.activeInstruction() != null && router.activeInstruction().config.currentData != null) {
                        idTcsAmbiente = router.activeInstruction().config.currentData.IdTcsAmbiente;
                    }
                    else if (managerAuth.shellMode == "DEV" || managerAuth.shellMode == "SETUP") {
                        idTcsAmbiente = managerAuth.loginInfo.Ambientes[0].IdTcsAmbiente;
                    }
                }

                var COD_BANDEIRA_REDE = managerAuth.getParameter("BANDEIRA_REDE_PADRAO", idTcsAmbiente);

                if (isNullOrEmpty(COD_BANDEIRA_REDE)) {
                    return "(Vazio)"
                }
                var vm = $.grep(this.VMs, function (element, index) { return element.IdTcsAmbiente == idTcsAmbiente });

                if (vm.length > 0 && vm[0].BrandDefault != null) {
                    return vm[0].BrandDefault.DescBandeiraRede;
                }
                else {
                    return "(Inválido)"
                }
            },

            getDefaultBrandId: function (idTcsAmbiente) {
                if (!idTcsAmbiente) {
                    idTcsAmbiente = 0;

                    if (router.activeInstruction() != null && router.activeInstruction().config.currentData != null) {
                        idTcsAmbiente = router.activeInstruction().config.currentData.IdTcsAmbiente;
                    }
                    else if (managerAuth.shellMode == "DEV" || managerAuth.shellMode == "SETUP") {
                        idTcsAmbiente = managerAuth.loginInfo.Ambientes[0].IdTcsAmbiente;
                    }
                }

                var COD_BANDEIRA_REDE = managerAuth.getParameter("BANDEIRA_REDE_PADRAO", idTcsAmbiente);

                if (!isNullOrEmpty(COD_BANDEIRA_REDE)) {
                    var vm = $.grep(this.VMs, function (element, index) { return element.IdTcsAmbiente == idTcsAmbiente });

                    if (vm.length > 0 && vm[0].BrandDefault != null && vm[0].BrandDefault.IdBandeiraRede) {
                        return vm[0].BrandDefault.IdBandeiraRede;
                    }
                }

                return -1;
            },

            ///////////////////////
            // method: loadBrandsVM()
            ///////////////////////
            loadBrandsVM: function () {
                var that = this;
                var cacheKey = common.getCachePrefixEnvironment('VM', 'BrandsVM', managerAuth.loginInfo.CacheKey);
                var cacheValue = $.ezstorage.get(cacheKey);

                if (cacheValue == null) {
                    system.log('Main: Loading BrandsVM...');

                    that.VMs = [];

                    for (var ii = 0; ii < managerAuth.loginInfo.Ambientes.length; ii++) {
                        var item = managerAuth.loginInfo.Ambientes[ii];

                        var vmItem = { IdTcsAmbiente: item.IdTcsAmbiente, BrandVM: [], BrandDefault: '', IdBandeiraRedeDefault: '' };

                        var brands = $.grep(that.BRANDS, function (element, index) { return element.IdTcsAmbiente == item.IdTcsAmbiente });

                        var allBrands = '';
                        $.each(brands,
                            function (index, value) {
                                allBrands += (index === 0 ? '' : ',') + value.IdBandeiraRede.toString();
                            }
                        );

                        if (brands.length > 1) {
                            vmItem.BrandVM.push(new BrandItemVM({
                                id: allBrands,
                                cod: '',
                                text: "Todas as redes",
                                html: '<span class="hide">Rede</span><img src="' + this.buildUrlRedes(200, 34) + '" />',
                                html_select2: '<span class="hide">Rede</span><img src="' + this.buildUrlRedes(200, 34) + '" />'
                            }));
                        }

                        for (var i = 0; i < brands.length; i++) {
                            var src;
                            var src_select2;

                            if (brands[i].Midia != null) {
                                // imagem gravada como multimidia no registro da REDE
                                src = brands[i].Midia.Url;
                                src_select2 = src;
                            }
                            else {

                                // imagem padrao do shell
                                src = this.buildUrlRede(200, 34, brands[i].CodBandeiraRede);
                                src_select2 = this.buildUrlRede(200, 34, brands[i].CodBandeiraRede);
                            }

                            vmItem.BrandVM.push(new BrandItemVM({
                                id: brands[i].IdBandeiraRede.toString(),
                                cod: brands[i].CodBandeiraRede.toString().trim(),
                                text: brands[i].DescBandeiraRede,
                                html: '<span class="hide">Rede</span><img src="' + src + '" height="34"/>',
                                html_select2: '<span class="hide">Rede</span><img src="' + src_select2 + '" height="34" />'
                            }));
                        }

                        that.VMs.push(vmItem);
                        that.setDefaultBrand(item.IdTcsAmbiente);
                    }

                    $.ezstorage.set(cacheKey, that.VMs, { expires: 90 })
                }
                else {
                    system.log('Main: Loading BrandsVM... [Storage]');
                    that.VMs = cacheValue;
                    for (var ii = 0; ii < managerAuth.loginInfo.Ambientes.length; ii++) {
                        var item = managerAuth.loginInfo.Ambientes[ii];
                        that.setDefaultBrand(item.IdTcsAmbiente);
                    }
                }
                that.loadBrandsDetails();
            },

            //////////////////////
            // method: loadBrandsDetails()
            //////////////////////
            loadBrandsDetails: function () {
                // get parameter DECIMAIS_DA_BANDEIRA
                var that = this;

                that.VMs.forEach(function (ambiente) {
                    ambiente.BrandVM.forEach(function (brand) {
                        if (!isNullOrEmpty(brand.id) && !isNullOrEmpty(brand.cod)) {
                            globalDataParameters.getParameter('DECIMAIS_DA_BANDEIRA{TBC_BANDEIRA_REDE|' + brand.id + '}', managerAuth).then(function (value) {
                                brand.decimals = value;
                            });
                        }
                    });

                });

            },

            ///////////////////////
            // method: searchBrandsVM()
            ///////////////////////
            searchBrandsVM: function (pValue, idTcsAmbiente) {
                if (!idTcsAmbiente) {
                    if (router.activeInstruction() != null && router.activeInstruction().config.currentData != null) {
                        idTcsAmbiente = router.activeInstruction().config.currentData.IdTcsAmbiente;
                    }
                }

                var that = this;
                var vm = $.grep(that.VMs, function (element, index) { return element.IdTcsAmbiente == idTcsAmbiente });

                if (vm.length > 0) {
                    var vmItem = $.grep(vm[0].BrandVM, function (element, index) { return element.id == pValue });
                    if (vmItem.length > 0) {
                        return vmItem[0];
                    }
                }
                return '';
            },

            ///////////////////////
            // method: saveBrandUser() 
            ///////////////////////
            saveDefaultBrand: function (idTcsAmbiente, id) {
                var dfd = $.Deferred();

                var obj = this.searchBrandsVM(id, idTcsAmbiente);

                return common.saveParameter("BANDEIRA_REDE_PADRAO", "TCS_USUARIO", managerAuth.loginInfo.UidUsuario, obj.cod, idTcsAmbiente).then(function () {
                    managerAuth.setParameter("BANDEIRA_REDE_PADRAO", obj.cod, idTcsAmbiente);
                    require('managers/brand').setDefaultBrand(idTcsAmbiente);
                    dfd.resolve();
                });
            },

            ///////////////////////
            // method: getBrandVM() 
            ///////////////////////
            getBrandVM: function (idTcsAmbiente) {
                if (!idTcsAmbiente) {
                    idTcsAmbiente = 0;

                    if (router.activeInstruction() != null && router.activeInstruction().config.currentData != null) {
                        idTcsAmbiente = router.activeInstruction().config.currentData.IdTcsAmbiente;
                    }
                    else if (managerAuth.shellMode == "DEV" || managerAuth.shellMode == "SETUP") {
                        idTcsAmbiente = managerAuth.loginInfo.Ambientes[0].IdTcsAmbiente;
                    }
                }
                var brands = $.grep(this.VMs, function (element, index) { return element.IdTcsAmbiente == idTcsAmbiente });

                if (brands.length > 0) {
                    return brands[0].BrandVM;
                }
                return [];
            }

        };
    });
