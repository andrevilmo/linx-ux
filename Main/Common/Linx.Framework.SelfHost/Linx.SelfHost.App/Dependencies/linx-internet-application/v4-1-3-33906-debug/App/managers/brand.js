define(['durandal/system', 'durandal/app', 'services/logger', 'managers/__auth', 'managers/brand', 'common'],
    function (system, app, logger, managerAuth, managerBrand, common) {
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
            BRANDS_VM: [],
            IdBandeiraRedeDefault: "",
            BrandDefault: null,

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

                var cacheKey = common.getCachePrefix('API', 'LinxFrameworkRede/GetTbcBandeiraRedeList');;
                var cacheObj = $.ezstorage.get(cacheKey);

                var cacheKeyHash = common.getCachePrefix('HASH', 'LinxFrameworkRede/GetTbcBandeiraRedeList');
                var cacheValueHash = $.ezstorage.get(cacheKeyHash);

                if (cacheValueHash == null || cacheObj == null) {
                    system.log('Main: Loading Brands...');

                    // dados vazio
                    if (cacheObj == null)
                        cacheValueHash = null;
                    else
                        // contem dados mas o "hash" expirou, forca chamar a api novamente
                        cacheValueHash = cacheObj.hash;

                    return $.ajax({
                        type: 'GET',
                        message: "buscando redes...",
                        messageUser: "Acesso as redes/bandeiras configuradas",
                        globalError: true,
                        url: managerAuth.getServiceAddress('LinxFrameworkRede/GetTbcBandeiraRedeList?cacheHash=' + cacheValueHash),
                        dataType: 'json',
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
                            that.setDefaultBrand();
                            that.loadBrandsVM();

                            dfd.promise();
                        }
                    });
                }
                else {
                    system.log('Main: Loading Brands... [Storage]');
                    that.BRANDS = cacheObj.value;
                    that.setDefaultBrand();
                    that.loadBrandsVM();

                    return dfd.resolve();
                }

            },

            setDefaultBrand: function () {
                var COD_BANDEIRA_REDE = globalDataParameters.parameters["BANDEIRA_REDE_PADRAO"];

                if (isNullOrEmpty(COD_BANDEIRA_REDE))
                    COD_BANDEIRA_REDE = '';

                // busca a bandeira pelo codigo
                for (var i = 0; i < this.BRANDS.length; i++) {
                    var item = this.BRANDS[i];

                    if (item.CodBandeiraRede.trim() === COD_BANDEIRA_REDE.trim()) {
                        this.BrandDefault = item;
                        this.IdBandeiraRedeDefault = item.IdBandeiraRede.toString();
                        return;
                    }
                }

                this.IdBandeiraRedeDefault = "";
            },

            getDefaultBrand: function () {

                var COD_BANDEIRA_REDE = globalDataParameters.parameters["BANDEIRA_REDE_PADRAO"];

                if (isNullOrEmpty(COD_BANDEIRA_REDE)) {
                    return "(Vazio)"
                } 
                else if (this.BrandDefault == null) {
                    return "(Inválido)"
                }
                else {
                    return this.BrandDefault.DescBandeiraRede;
                }
            },

            ///////////////////////
            // method: loadBrandsVM()
            ///////////////////////
            loadBrandsVM: function () {
                var that = this;
                var cacheKey = common.getCachePrefix('VM', 'BrandsVM');
                var cacheValue = $.ezstorage.get(cacheKey);

                if (cacheValue == null) {
                    system.log('Main: Loading BrandsVM...');
                    that.BRANDS_VM = [];
                    var allBrands = '';
                    $.each(this.BRANDS,
                        function (index, value) {
                            allBrands += (index === 0 ? '' : ',') + value.IdBandeiraRede.toString();
                        }
                    );


                    if (this.BRANDS.length > 1) {
                        that.BRANDS_VM.push(new BrandItemVM({
                            id: allBrands,
                            cod: '',
                            text: "Todas as redes",
                            html: '<span class="hide">Rede</span><img src="' + this.buildUrlRedes(200, 34) + '" />',
                            html_select2: '<span class="hide">Rede</span><img src="' + this.buildUrlRedes(200, 34) + '" />'
                        }));
                    }

                    for (var i = 0; i < this.BRANDS.length; i++) {
                        var src;
                        var src_select2;

                        if (this.BRANDS[i].Midia != null) {
                            // imagem gravada como multimidia no registro da REDE
                            src = this.BRANDS[i].Midia.Url;
                            src_select2 = src;
                        }
                        else {

                            // imagem padrao do shell
                            src = this.buildUrlRede(200, 34, this.BRANDS[i].CodBandeiraRede);
                            src_select2 = this.buildUrlRede(200, 34, this.BRANDS[i].CodBandeiraRede);
                        }


                        that.BRANDS_VM.push(new BrandItemVM({
                            id: this.BRANDS[i].IdBandeiraRede.toString(),
                            cod: this.BRANDS[i].CodBandeiraRede.toString().trim(),
                            text: this.BRANDS[i].DescBandeiraRede,
                            html: '<span class="hide">Rede</span><img src="' + src + '" height="34"/>',
                            html_select2: '<span class="hide">Rede</span><img src="' + src_select2 + '" height="34" />'
                        }));
                    }

                    $.ezstorage.set(cacheKey, that.BRANDS_VM, { expires: 90 })
                }
                else {
                    system.log('Main: Loading BrandsVM... [Storage]');
                    that.BRANDS_VM = cacheValue;
                }

            },

            ///////////////////////
            // method: searchBrandsVM()
            ///////////////////////
            searchBrandsVM: function (pValue) {
                var that = this;

                for (var i = 0; i < that.BRANDS_VM.length; i++) {
                    var item = that.BRANDS_VM[i];

                    if (item.id == pValue) {
                        return item;
                    }
                }

                return null;
            },

            ///////////////////////
            // method: saveBrandUser() 
            ///////////////////////
            saveDefaultBrand: function (id) {
                var dfd = $.Deferred();

                var obj = this.searchBrandsVM(id);

                return common.saveParameter("BANDEIRA_REDE_PADRAO", "TCS_USUARIO", managerAuth.userId, obj.cod).then(function () {
                    globalDataParameters.parameters["BANDEIRA_REDE_PADRAO"] = obj.cod;
                    dfd.resolve();
                });
            }


        };
    });
