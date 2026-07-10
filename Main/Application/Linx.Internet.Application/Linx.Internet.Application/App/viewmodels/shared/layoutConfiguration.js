define(['plugins/dialog', 'durandal/app', 'knockout', 'services/logger', 'managers/__auth', 'managers/user', 'common', 'plugins/router'],
   function (dialog, app, ko, logger, managerAuth, managerUser, common, router) {
       var isBusy = function (newValue) {
           if ($(".page-container").html() == undefined || $(".page-container").html().length == 0)
               return;
           if (newValue) {
               common.showProcess('#main');
           }
           else {
               common.closeProcess('#main');
           }
       };

       var vm = {
           comboUser: function () { return $('#cboUsersPermission'); },
           comboProfiles: function () { return $('#cboProfilesPermission'); },
           dfd: null,
           sourceVM: null,
           layoutName: '',
           layoutObject: null,
           NomeLayout: ko.observable(null),
           LayoutPadrao: ko.observable(false),
           CanEditNomeLayout: ko.observable(true),
           title: ko.observable('Configuração de layout do formulário'),
           currentLayout: ko.observable(null),
           canSave: function () {
               if (isNullOrEmpty(this.currentLayout().NomeLayout)) {
                   app.showMessage("Preencha o nome do layout!", "Alerta");
                   return false;
               }

               return true;
           },
           save_click: function () {
               var _this = this;

               this.currentLayout().NomeLayout = this.NomeLayout();
               this.currentLayout().LayoutPadrao = this.LayoutPadrao();
               if (!_this.canSave()) return;


               this.currentLayout().ConteudoJson = this.layoutObject;
               this.currentLayout().PermissaoUsuario = this.comboUser().select2('val').join();
               this.currentLayout().PermissaoPerfil = this.comboProfiles().select2('val').join();

               managerUser.saveGridLayout(this.currentLayout()).then(
                   function saveSuccess(itemSaved) {
                       app.showMessage("Salvo com sucesso!").then(function () {
                           _this.close(true, itemSaved.Id);
                       });
                   },
                   function saveFail(jqXHR, textStatus, errorThrown) {
                       app.showMessage(jqXHR.responseJSON.ExceptionMessage, 'Erro', ['Ok']);
                   });

           },
           close: function (needRefresh, selectedId) {
               if (needRefresh != null && needRefresh)
                   this.dfd.resolve(true, selectedId);
               else
                   this.dfd.resolve(false);
               dialog.close(this, { cancel: !needRefresh });
           },
           cancel_Click: function () {
               this.close();
           },
           compositionComplete: function () {
               var _this = this;

               _this.getData();
               //populates combos
               var idLayout = _this.currentLayout() ? _this.currentLayout().Id : 0;
               managerUser.getAllUserPermission(idLayout).then(function (data) {
                   var temp = [];
                   data.forEach(function (item) {
                       temp.push({ id: item.IdUsuario, text: item.NomeUsuario });
                   })
                   _this.comboUser().select2({
                       data: temp,
                       tags: true
                   });
                   if (!isNullOrEmpty(_this.currentLayout().PermissaoUsuario)) {
                       _this.comboUser().select2('val', _this.currentLayout().PermissaoUsuario.split(','));
                   }
               });
               managerUser.getAllProfiles(idLayout).then(function (data) {
                   var temp = [];
                   data.forEach(function (item) {
                       temp.push({ id: item.IdPerfil, text: item.NomePerfil });
                   });
                   _this.comboProfiles().select2({
                       data: temp,
                       tags: true
                   });
                   if (!isNullOrEmpty(_this.currentLayout().PermissaoPerfil)) {
                       _this.comboProfiles().select2('val', _this.currentLayout().PermissaoPerfil.split(','));
                   }
               });

           },
           getData: function () {
               isBusy(true);
               var _this = this;
               if (isNull(this.layoutObject))
                   this.layoutObject = JSON.stringify(this.layoutInfo.layoutToSave());

               isBusy(false);
           },

           showModal: function (sourceVM, layoutInfo, layoutName, saveAs) {
               this.dfd = $.Deferred();
               this.sourceVM = sourceVM;
               this.layoutName = layoutName;
               this.layoutInfo = layoutInfo;
               this.currentLayout({ Id: 0, NomeLayout: '', PermissaoPerfil: '', PermissaoUsuario: '', LayoutPadrao: false });
               this.CanEditNomeLayout(true);
               this.NomeLayout('');
               this.LayoutPadrao(false);
               this.layoutObject = null;

               if (!saveAs && this.sourceVM.currentLayout() && this.sourceVM.currentLayout().Id > 0) {
                   this.currentLayout(this.sourceVM.currentLayout());
                   this.NomeLayout(this.sourceVM.currentLayout().NomeLayout);
                   this.LayoutPadrao(this.sourceVM.currentLayout().LayoutPadrao);
                   this.CanEditNomeLayout(false);
               }
               if (this.sourceVM.currentLayout() && this.sourceVM.currentLayout().ConteudoJson) {
                   //this.layoutObject = JSON.parse(this.sourceVM.currentLayout().ConteudoJson);
                   this.currentLayout().PermissaoUsuario = this.sourceVM.currentLayout().PermissaoUsuario;
                   this.currentLayout().PermissaoPerfil = this.sourceVM.currentLayout().PermissaoPerfil;
                   this.LayoutPadrao(this.sourceVM.currentLayout().LayoutPadrao);
                   if (saveAs)
                       this.NomeLayout(this.sourceVM.currentLayout().NomeLayout + '_copia');
               }

               this.currentLayout().Modulo = this.sourceVM.__moduleId__;
               this.currentLayout().NomeObjeto = this.layoutName;
               
               dialog.show(this);
               return this.dfd;
           }
       }

       return vm;
   });