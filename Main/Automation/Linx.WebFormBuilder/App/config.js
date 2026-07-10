(function () {
    'use strict';

    /*
    	Colocar as constantes e configurações do ambiente
    */

    var app = angular.module('FormBuilder');

    app.config(function($modalProvider) {
      $modalProvider.options.animation = false;
    });

    var mobileGridster = {
      margins: [10, 10],
    	outerMargin: true,
      columns: 6,
      isMobile: true,
      pushing: true,
      minSizeX: 2,
      //maxSizeY: 1,
      floating: true,
      draggable: {
          enabled: true
      },
      resizable: {
          enabled: true,
          //handles: ['e']
          handles: ['n', 'e', 's', 'w', 'ne', 'se', 'sw', 'nw']
      }
    };

    var webGridster = {
        margins: [10, 10],
        outerMargin: true,
        columns: 6,
        minSizeX: 1,
        pushing: true,
        floating: true,
        draggable: {
            enabled: true
        },
        resizable: {
            enabled: true,
            handles: ['e', 'se']
        }
    };

    app.constant('gridsterOptions', {
    	mobileGridster: mobileGridster,
    	webGridster: webGridster
    });

    var formProperties = [
       {
            "key": "title",
            "type": "input",
            "templateOptions": {
                "label": "Título"
            }
        },
        {
            "key": "toolbar",
            "type": "select",
            "templateOptions": {
                "label": "Toolbar",
                "valueProp": "value",
                "labelProp": "name",
                "defaultValue": "true",
                "options": [
                    {
                        "name": "Sim",
                        "value": "true"
                    },
                    {
                        "name": "Não",
                        "value": "false"
                    }
                ]
            }
        },
        {
            "key": "blank",
            "type": "select",
            "templateOptions": {
                "label": "Tela em branco",
                "valueProp": "value",
                "labelProp": "name",
                "defaultValue": "false",
                "options": [
                    {
                        "name": "Sim",
                        "value": "true"
                    },
                    {
                        "name": "Não",
                        "value": "false"
                    }
                ]
            }
        },
        {
            "key": "class",
            "type": "input",
            "templateOptions": {
                "label": "Classe CSS"
            }
        }
    ];

    app.constant('formProperties', formProperties);

    var formPropertiesDefault = {
            "title": "",
            "toolbar": "false",
            "blank": "true",
            "class":""
    };

    app.constant('formPropertiesDefault', formPropertiesDefault);

    app.constant('projectVariables', {
        git: 'https://fcamaralinx:fcamara123@bitbucket.org/linxframework/linxblankmobile.git',
        gitBreezeCore: 'https://fcamaralinx:fcamara123@bitbucket.org/Fcamaralinx/linxblankbreezemobile.git'
    });

})();
