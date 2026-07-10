[
    {
        "title": "TextBox",
        "description": "Componente textbox do html",
        "template": "LinxTextBox.html",
        "icon": "fa-font",
        "drag": true,
        "id": 1,
        "groupType": "input",
        "events": [
            {
                "key": "onClick"
            }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
            {
                "key": "model",
                "type": "input",
                "templateOptions": {
                    "label": "Model",
                    "placeholder": "model"
                }
            },
            {
                "key": "placeholder",
                "type": "input",
                "templateOptions": {
                    "label": "Placeholder",
                    "placeholder": "Placeholder"
                }
            },
            {
                "key": "mode",
                "type": "select",
                "defaultValue": "edit",
                "templateOptions": {
                    "label": "Modo",
                    "valueProp": "value",
                    "labelProp": "name",
                    "options": [
                        {
                            "name": "Editavél",
                            "value": "edit"
                        },
                        {
                            "name": "Leitura",
                            "value": "read"
                        }
                    ]
                }
            },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
                "key": "ngChange",
                "type": "input",
                "templateOptions": {
                    "label": "ng-change",
                    "placeholder": "ng-change"
                }
            }

        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "TextArea",
        "description": "Componente textarea do html",
        "template": "LinxTextArea.html",
        "icon": "fa-align-justify",
        "drag": true,
        "id": 2,
        "groupType": "input",
        "events": [
            {
                "key": "onClick"
            }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
            {
                "key": "model",
                "type": "input",
                "templateOptions": {
                    "label": "Model",
                    "placeholder": "model"
                }
            },
            {
                "key": "placeholder",
                "type": "input",
                "templateOptions": {
                    "label": "Placeholder",
                    "placeholder": "Placeholder"
                }
            },
            {
                "key": "mode",
                "type": "select",
                "defaultValue": "edit",
                "templateOptions": {
                    "label": "Modo",
                    "valueProp": "value",
                    "labelProp": "name",
                    "options": [
                        {
                            "name": "Editavél",
                            "value": "edit"
                        },
                        {
                            "name": "Leitura",
                            "value": "read"
                        }
                    ]
                }
            },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "ComboBox",
        "description": "Componente combo do html",
        "template": "LinxComboBox.html",
        "icon": "fa-sort-desc",
        "drag": true,
        "id": 4,
        "groupType": "select",
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
            {
                "key": "model",
                "type": "input",
                "templateOptions": {
                    "label": "ng-model",
                    "placeholder": "model"
                }
            },
            {
                "key": "change",
                "type": "input",
                "templateOptions": {
                    "label": "ng-change",
                    "placeholder": "Evento"
                }
            },
            {
                "key": "options",
                "type": "input",
                "templateOptions": {
                    "label": "ng-options",
                    "placeholder": "Opções(expresão)"
                }
            },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "DateTime",
        "description": "Componente datetime do html",
        "template": "LinxDateTimeTextBox.html",
        "icon": "fa-calendar",
        "drag": true,
        "id": 5,
        "groupType": "input",
        "events": [
           {
               "key": "onClick"
           }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
             {
                 "key": "model",
                 "type": "input",
                 "templateOptions": {
                     "label": "Model",
                     "placeholder": "model"
                 }
             },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
                "key": "mode",
                "type": "select",
                "defaultValue": "Date",
                "templateOptions": {
                    "label": "Modo",
                    "valueProp": "value",
                    "labelProp": "name",
                    "options": [
                        {
                            "name": "Data (dd/MM/yyyy)",
                            "value": "Date"
                        },
                        {
                            "name": "Tempo (HH:mm:ss)",
                            "value": "Time"
                        },
                        {
                            "name": "Tempo (dd/MM/yyyy HH:mm:ss)",
                            "value": "DateTime"
                        }
                    ]
                }
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "LookUp",
        "description": "Componente lookup do html",
        "template": "LinxLookUpTextBox.html",
        "icon": "fa-search",
        "drag": true,
        "id": 6,
        "groupType": "input",
        "events": [
           {
               "key": "onClick"
           }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
            {
                "key": "placeholder",
                "type": "input",
                "templateOptions": {
                    "label": "Placeholder",
                    "placeholder": "Placeholder"
                }
            },
            {
                "key": "titleModal",
                "type": "input",
                "templateOptions": {
                    "label": "TitleModal",
                    "placeholder": "TitleModal"
                }
            },
            {
                "key": "searchPlaceholder",
                "type": "input",
                "templateOptions": {
                    "label": "SearchPlaceholder",
                    "placeholder": "SearchPlaceholder"
                }
            },
            {
                "key": "paginationSize",
                "type": "input",
                "templateOptions": {
                    "label": "paginationSize",
                    "placeholder": "paginationSize"
                }
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "Numérico",
        "description": "Componente númerico do html",
        "template": "LinxNumericTextBox.html",
        "icon": "fa-sort-numeric-asc",
        "drag": true,
        "id": 7,
        "groupType": "input",
        "events": [
           {
               "key": "onClick"
           }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
             {
                 "key": "model",
                 "type": "input",
                 "templateOptions": {
                     "label": "Model",
                     "placeholder": "model"
                 }
             },
            {
                "key": "placeholder",
                "type": "input",
                "templateOptions": {
                    "label": "Placeholder",
                    "placeholder": "Placeholder"
                }
            },
            {
                "key": "mode",
                "type": "select",
                "defaultValue": "edit",
                "templateOptions": {
                    "label": "Modo",
                    "valueProp": "value",
                    "labelProp": "name",
                    "options": [
                        {
                            "name": "Editavél",
                            "value": "edit"
                        },
                        {
                            "name": "Leitura",
                            "value": "read"
                        }
                    ]
                }
            },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
                "key": "decimal",
                "type": "input",
                "defaultValue": "0",
                "templateOptions": {
                    "label": "Decimal",
                    "placeholder": "Decimal"
                }
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "RadioButton",
        "description": "Componente radio button do html",
        "template": "LinxRadioButton.html",
        "icon": "fa-dot-circle-o",
        "drag": true,
        "id": 8,
        "groupType": "check",
        "events": [
            {
                "key": "onClick"
            }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
            {
                "key": "model",
                "type": "input",
                "templateOptions": {
                    "label": "Model",
                    "placeholder": "model"
                }
            },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "Label",
        "description": "Componente label do html",
        "template": "LinxLabel.html",
        "icon": "fa-text-width",
        "drag": true,
        "id": 9,
        "groupType": "label",
        "events": [
           {
               "key": "onClick"
           }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
                "key": "ngIf",
                "type": "input",
                "templateOptions": {
                    "label": "ng-if",
                    "placeholder": "ng-if"
                }
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "Password",
        "description": "Componente password do html",
        "template": "LinxPasswordTextBox.html",
        "icon": "fa-lock",
        "drag": true,
        "id": 12,
        "groupType": "input",
        "events": [
           {
               "key": "onClick"
           }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
             {
                 "key": "model",
                 "type": "input",
                 "templateOptions": {
                     "label": "Model",
                     "placeholder": "model"
                 }
             },
            {
                "key": "placeholder",
                "type": "input",
                "templateOptions": {
                    "label": "Placeholder",
                    "placeholder": "Placeholder"
                }
            },
            {
                "key": "mode",
                "type": "select",
                "defaultValue": "edit",
                "templateOptions": {
                    "label": "Modo",
                    "valueProp": "value",
                    "labelProp": "name",
                    "options": [
                        {
                            "name": "Editavél",
                            "value": "edit"
                        },
                        {
                            "name": "Leitura",
                            "value": "read"
                        }
                    ]
                }
            },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "Button",
        "description": "Componente button do html",
        "template": "LinxButton.html",
        "icon": "fa-square-o",
        "drag": true,
        "id": 14,
        "groupType": "button",
        "events": [
           {
               "key": "onClick"
           }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
            {
                "key": "visible",
                "type": "select",
                "defaultValue": "true",
                "templateOptions": {
                    "label": "Visível",
                    "valueProp": "value",
                    "labelProp": "name",
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
                "key": "method",
                "type": "input",
                "defaultValue": "vm.searchButton",
                "templateOptions": {
                    "label": "Method",
                    "placeholder": "Method"
                }
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ],
        "ngclass": [
            {
                "key": "ngclass",
                "type": "input",
                "templateOptions": {
                    "label": "ng-class",
                    "placeholder": "ng-class"
                }
            }
        ]

    },
    {
        "title": "Checkbox",
        "description": "Componente check do html",
        "template": "LinxCheckBox.html",
        "icon": "fa-font",
        "drag": true,
        "id": 17,
        "groupType": "check",
        "events": [
           {
               "key": "onClick"
           }
        ],
        "properties": [
            {
                "key": "label",
                "type": "input",
                "templateOptions": {
                    "label": "Label",
                    "placeholder": "Label"
                }
            },
            {
                "key": "model",
                "type": "input",
                "templateOptions": {
                    "label": "Model",
                    "placeholder": ""
                }
            },
                        {
                            "key": "checked",
                            "type": "input",
                            "templateOptions": {
                                "label": "Checked",
                                "placeholder": "ng-checked"
                            }
                        }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "Editor Html",
        "description": "Componente de html",
        "template": "LinxHtml.html",
        "icon": "fa-html5",
        "drag": true,
        "id": 18,
        "groupType": "html"
    },
    {
        "title": "Html Externo",
        "description": "Componente de html",
        "template": "LinxHtmlInclude.html",
        "icon": "fa-html5",
        "drag": true,
        "id": 19,
        "groupType": "html",
        "properties": [
            {
                "key": "path",
                "type": "input",
                "templateOptions": {
                    "label": "Arquivo:",
                    "placeholder": "Caminho do html"
                }
            }
        ]
    },
    {
        "title": "Div Bind",
        "description": "Componente div com um bind",
        "template": "LinxDivBind.html",
        "icon": "fa-html5",
        "drag": true,
        "id": 20,
        "groupType": "html",
        "properties": [],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "Lista Simples",
        "description": "Ionic list",
        "template": "LinxSimpleList.html",
        "icon": "fa-list",
        "drag": true,
        "id": 21,
        "groupType": "grid",
        "properties": [
            {
                "key": "repeater",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Entidade da lista",
                    "placeholder": "Nome da entidade referente a lista (Ex: )"
                }
            },
            {
                "key": "headerPropertie",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Propriedade título",
                    "placeholder": "Nome da principal propriedade da lista"
                }
            },
            {
                "key": "routeLink",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Link da rota",
                    "placeholder": "Rota para o link (somente lista simples e card list)"
                }
            },
            {
                "key": "ngiflist",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "ng-if",
                    "placeholder": "Função ng-if"
                }
            }
        ],
        "class": [
          {
              "key": "ngclass",
              "type": "input",
              "templateOptions": {
                  "label": "Class item",
                  "placeholder": "CSS Class"
              }
          }
        ]
    },
     {
         "title": "Lista Card",
         "description": "Ionic list",
         "template": "LinxCardList.html",
         "icon": "fa-list",
         "drag": true,
         "id": 22,
         "groupType": "grid",
         "properties": [
             {
                 "key": "repeater",
                 "type": "input",
                 "defaultValue": "",
                 "templateOptions": {
                     "label": "Entidade da lista",
                     "placeholder": "Nome da entidade referente a lista (Ex: )"
                 }
             },
             {
                 "key": "headerPropertie",
                 "type": "input",
                 "defaultValue": "",
                 "templateOptions": {
                     "label": "Propriedade título",
                     "placeholder": "Nome da principal propriedade da lista"
                 }
             },
             {
                 "key": "legendPropertie",
                 "type": "input",
                 "defaultValue": "",
                 "templateOptions": {
                     "label": "Propriedade legenda",
                     "placeholder": "Nome da propriedade de legenda (somente em card list)"
                 }
             },
             {
                 "key": "routeLink",
                 "type": "input",
                 "defaultValue": "",
                 "templateOptions": {
                     "label": "Link da rota",
                     "placeholder": "Rota para o link (somente lista simples e card list)"
                 }
             },
             {
                 "key": "imagePropertie",
                 "type": "input",
                 "defaultValue": "",
                 "templateOptions": {
                     "label": "Imagem",
                     "placeholder": "Nome da expresão de imagem (somente em card list)"
                 }
             },
             {
                 "key": "ngiflist",
                 "type": "input",
                 "defaultValue": "",
                 "templateOptions": {
                     "label": "ng-if",
                     "placeholder": "Função ng-if"
                 }
             }
         ],
         "class": [
           {
               "key": "ngclass",
               "type": "input",
               "templateOptions": {
                   "label": "Class item",
                   "placeholder": "CSS Class"
               }
           }
         ]
     },
    {
        "title": "List Button",
        "description": "Ionic list",
        "template": "LinxButtonList.html",
        "icon": "fa-list",
        "drag": true,
        "id": 23,
        "groupType": "grid",
        "properties": [
            {
                "key": "repeater",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Entidade da lista",
                    "placeholder": "Nome da entidade referente a lista (Ex: )"
                }
            },
            {
                "key": "headerPropertie",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Propriedade título",
                    "placeholder": "Nome da principal propriedade da lista"
                }
            },
            {
                "key": "excludeButton",
                "type": "select",
                "defaultValue": "false",
                "templateOptions": {
                    "label": "Botão de Exculir (somente lista com botões)",
                    "valueProp": "value",
                    "labelProp": "name",
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
                "key": "excludeFunction",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Função de exclusão",
                    "placeholder": "Função (somente lista com botões)"
                }
            },
            {
                "key": "editButton",
                "type": "select",
                "defaultValue": "false",
                "templateOptions": {
                    "label": "Botão de Editar (somente lista com botões)",
                    "valueProp": "value",
                    "labelProp": "name",
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
                "key": "editFunction",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Função de edição",
                    "placeholder": "Função (somente lista com botões)"
                }
            },
            {
                "key": "customButton",
                "type": "select",
                "defaultValue": "false",
                "templateOptions": {
                    "label": "Botão personalizado (somente lista com botões)",
                    "valueProp": "value",
                    "labelProp": "name",
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
                "key": "customLabel",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Label do botão",
                    "placeholder": "Nome a ser exibido (somente lista com botões)"
                }
            },
            {
                "key": "customFunction",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Função do botão personalizado",
                    "placeholder": "Função (somente lista com botões)"
                }
            },
            {
                "key": "ngiflist",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "ng-if",
                    "placeholder": "Função ng-if"
                }
            }
        ],
        "class": [
          {
              "key": "ngclass",
              "type": "input",
              "templateOptions": {
                  "label": "Class item",
                  "placeholder": "CSS Class"
              }
          }
        ]
    },
    {
        "title": "Table",
        "description": "Ionic table",
        "template": "LinxTable.html",
        "icon": "fa-table",
        "drag": true,
        "id": 24,
        "groupType": "grid",
        "properties": [
            {
                "key": "repeater",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Entidade da tabela",
                    "placeholder": "Nome da entidade referente a tabela (Ex: )"
                }
            },
            {
                "key": "headersTable",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Cabeçalho Tabela",
                    "placeholder": "Nome das colunas separados por virgula"
                }
            },
            {
                "key": "tablePropertie",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Propriedade da tabelas",
                    "placeholder": "Nome da propriedade da tabela"
                }
            }
        ]
    },
    {
        "title": "Image",
        "description": "Image",
        "template": "LinxImage.html",
        "icon": "fa-file-image-o",
        "drag": true,
        "id": 25,
        "groupType": "image",
        "properties": [
            {
                "key": "path",
                "type": "input",
                "templateOptions": {
                    "label": "Arquivo:",
                    "placeholder": "Caminho da imagem"
                }
            },
            {
                "key": "width",
                "type": "input",
                "templateOptions": {
                    "label": "Width",
                    "placeholder": "Width"
                }
            },
            {
                "key": "height",
                "type": "input",
                "templateOptions": {
                    "label": "Height",
                    "placeholder": "Height"
                }
            },
            {
                "key": "alt",
                "type": "input",
                "templateOptions": {
                    "label": "Alt",
                    "placeholder": "Alt"
                }
            }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ]
    },
    {
        "title": "Toggle",
        "description": "Toggle",
        "template": "LinxToggle.html",
        "icon": "fa-toggle-on",
        "drag": true,
        "id": 26,
        "groupType": "button",
        "properties": [
            {
                "key": "width",
                "type": "input",
                "templateOptions": {
                    "label": "Width",
                    "placeholder": "Width"
                }
            },
            {
                "key": "height",
                "type": "input",
                "templateOptions": {
                    "label": "Height",
                    "placeholder": "Height"
                }
            }
        ]
    },
    {
        "title": "ToggleList",
        "description": "Toggle Ionic list",
        "template": "LinxToggleList.html",
        "icon": "fa-toggle-on",
        "drag": true,
        "id": 27,
        "groupType": "grid",
        "properties": [
            {
                "key": "repeater",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Entidade da lista",
                    "placeholder": "Nome da entidade referente a lista"
                }
            },
            {
                "key": "headerPropertie",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Propriedade título",
                    "placeholder": "Nome da principal propriedade da lista"
                }
            },
            {
                "key": "legendPropertie",
                "type": "input",
                "defaultValue": "",
                "templateOptions": {
                    "label": "Propriedade legenda",
                    "placeholder": "Nome da propriedade de legenda"
                }
            }
        ]
    },
    {
        "title": "Flexmonster",
        "description": "Flexmonster - Pivot Table",
        "template": "LinxFlexmonster.html",
        "icon": "fa-bar-chart",
        "drag": true,
        "id": 28,
        "groupType": "chart",
        "properties": [
          {
              "key": "width",
              "type": "input",
              "defaultValue": "100%",
              "templateOptions": {
                  "label": "Largura",
                  "placeholder": "largura"
              }
          },
          {
              "key": "height",
              "type": "input",
              "defaultValue": "350",
              "templateOptions": {
                  "label": "Tamanho",
                  "placeholder": "tamanho"
              }
          },
          {
              "key": "containerId",
              "type": "input",
              "defaultValue": "ui-pivot-table",
              "templateOptions": {
                  "label": "Componente id",
                  "placeholder": "componente id"
              }
          },
          {
              "key": "pivot.name",
              "type": "input",
              "defaultValue": "",
              "templateOptions": {
                  "label": "Nome",
                  "placeholder": "nome componente"
              }
          },
          {
              "key": "pivot.caption",
              "type": "input",
              "defaultValue": "",
              "templateOptions": {
                  "label": "Nome aparente",
                  "placeholder": "nome aparente"
              }
          },
          {
              "key": "model",
              "type": "input",
              "templateOptions": {
                  "label": "Model",
                  "placeholder": "Model"
              }
          }
        ]
    },
    {
        "title": "DataToolBar",
        "description": "Componente toolBar",
        "template": "LinxDataToolBar.html",
        "icon": "fa fa-cogs",
        "drag": true,
        "id": 29,
        "groupType": "ToolBar",
        "events": [
           {
               "key": "onClick"
           }
        ],
        "properties": [
                        {
                            "key": "method",
                            "type": "input",
                            "defaultValue": "",
                            "templateOptions": {
                                "label": "Method",
                                "placeholder": "Method"
                            }
                        }
        ],
        "class": [
            {
                "key": "class",
                "type": "input",
                "templateOptions": {
                    "label": "CSS Class",
                    "placeholder": "CSS Class"
                }
            }
        ],
        "ngclass": [
            {
                "key": "ngclass",
                "type": "input",
                "templateOptions": {
                    "label": "ng-class",
                    "placeholder": "ng-class"
                }
            }
        ]
    }
]
