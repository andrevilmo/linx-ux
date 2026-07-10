define(['durandal/app', 'managers/__auth'], function (app, managerAuth) {

   var dataCombo = {
       combos: [],
       registerCombos: function (route, lookup) {
           if (route != undefined && lookup != undefined) {
               dataCombo.fillDataCombos(lookup, route);
           }
      },
      getItems: function (comboName, valuesFilter) {
           var items = dataCombo.combos[comboName];
           if (!isNullOrEmpty(valuesFilter) && items && items.length > 0) {
               for (var i = items.length - 1; i >= 0; i--) {
                   if ((',' + valuesFilter + ',').indexOf(',' + items[i].id + ',') === -1) {
                       items.removeAt(i);
                   }
               }
           }
           return (items && items.length > 0 ? items : []);
   },
   fillDataCombos: function (lookup, route, complete) {
       if (route != undefined) {
           route = managerAuth.getServiceAddress(route);
           $.ajax({
               type: 'GET',
               url: route + '/Get' + lookup + 'ByEntitySearch?jEntitySearch=' + lookup + '',
               dataType: 'json',
               cache: false,
               error: function (jqXHR, textStatus, errorThrown) {
                   var msg = 'O seguinte ComboBox não pode ser carregado: [' + lookup + ']';
                   app.showMessage(msg, 'Alerta', ['Ok']);
               },
               success: function (data) {
                   dataCombo.combos[lookup] = data;
                   if (complete) complete();
               }
           });
           }
       }
   };

   dataCombo.registerCombos();
   return dataCombo;

});
