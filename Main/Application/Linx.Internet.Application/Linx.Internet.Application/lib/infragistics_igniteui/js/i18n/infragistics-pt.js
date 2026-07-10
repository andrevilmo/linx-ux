/*!@license
* Infragistics.Web.ClientUI data source localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {

    $.ig = $.ig || {};

    if (!$.ig.DataSourceLocale) {
        $.ig.DataSourceLocale = {};

        $.extend($.ig.DataSourceLocale, {

            locale: {
                invalidDataSource: "A fonte de dados não é válida. Tipo escalar.",
                unknownDataSource: "Não foi possível determinar o tipo de fonte de dados. Especificar se é do tipo JSON ou XML. .",
                errorParsingArrays: "Não foi possível analisar os dados da matriz e aplicar o esquema de dados definido: ",
                errorParsingJson: "Não foi possível analisar os dados JSON e aplicar o esquema de dados definido:",
                errorParsingXml: "Não foi possível analisar os dados XML e aplicar o esquema de dados definido: ",
                errorParsingHtmlTable: "Não foi possível extrair os dados da tabela HTML e aplicar o esquema:  ",
                errorExpectedTbodyParameter: "Espera-se um tbody ou uma tabela como parâmetro.",
                errorTableWithIdNotFound: "Não foi encontrada a tabela HTML com o seguinte Id.: ",
                errorParsingHtmlTableNoSchema: "Não foi possível analisar o DOM da tabela: ",
                errorParsingJsonNoSchema: "Não foi possível de analisar / avaliar a cadeia de JSON: ",
                errorParsingXmlNoSchema: "Ocorreu um erro ao analisar a sequência de caracteres XML:",
                errorXmlSourceWithoutSchema: "A fonte de dados fornecida é um documento xml, mas nenhum esquema de dados foi definido ($.IgDataSchema) ",
                errorUnrecognizedFilterCondition: "  A condição de filtro especificada não foi reconhecida: ",
                errorRemoteRequest: "Erro na solicitação remota de recuperação de dados: ",
                errorSchemaMismatch: "Os dados de entrada não coincidem com o esquema e não foi possível atribuir o seguinte campo: ",
                errorSchemaFieldCountMismatch: "Os dados de entrada não coincidem com o esquema em termos de número de campos. ",
                errorUnrecognizedResponseType: "O tipo de resposta não está configurado corretamente ou não foi possível detectá-lo automaticamente. Definir settings.responseDataType e/o settings.responseContentType.",
                hierarchicalTablesNotSupported: "Tabelas não suportadas para HierarchicalSchema",
                cannotBuildTemplate: "Não foi possível gerar o modelo jQuery. Não há registros presentes na fonte de dados e nenhuma coluna definida.",
                unrecognizedCondition: "Condição de filtro não reconhecida na seguinte expressão: ",
                fieldMismatch: "A seguinte expressão contém um campo ou condição de filtro inválido: ",
                noSortingFields: "Nenhum campo foi especificado. É necessário especificar pelo menos um campo como sort() para efetuar a ordenação.",
                filteringNoSchema: "Nenhum esquema /campo foi especificado. É necessário especificar um esquema com definições e os tipos de campo para ser possível filtrar a origem de dados."
            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI common DV widget localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Chart) {
        $.ig.Chart = {};

        $.extend($.ig.Chart, {

            locale: {
                seriesName: "é necessário especificar o nome da série para definir as opções.",
                axisName: "é necessário especificar o nome do eixo para definir as opções.",
                invalidLabelBinding: "Não existem valores para os labels dos binds.",
                close: "Fechar",
                overview: "Informação geral",
                zoomOut: "Diminuir zoom",
                zoomIn: "Aumentar zoom",
                resetZoom: "Redefinir zoom"
            }
        });

    }
})(jQuery);

/*!@license
* Infragistics.Web.ClientUI shared localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.SharedLocale) {
        $.ig.SharedLocale = {};

        $.extend($.ig.SharedLocale, {

            locale: {

            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI templating localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Templating) {
        $.ig.Templating = {};

        $.extend($.ig.Templating, {
            locale: {
                undefinedArgument: 'Não foi possível recuperar as propriedades da fonte de dados:: '
            }
        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Barcode localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};
//Validar com ricardo
    if (!$.ig.Barcode) {
        $.ig.Barcode = {
            locale: {
                aILength: "O Identificador de Aplicação (AI) deve ter pelo menos 2 dígitos.",
                badFormedUCCValue: "Os dados do código de barras UCC não estão corretos. Eles devem ter a estrutura (AI)GTIN.",
                code39_NonNumericError: "'{0}' não é um caráter válido para os dados CODE39. Os caracteres válidos são:  {1}",
                countryError: "Erro ao converter o código do país. O seu tipo deve ser numérico.",
                emptyValueMsg: "O valor dos dados está vazio.",
                encodingError: "Erro na conversão. Verifique os valores das propriedades.",
                errorMessageText: "Valor inválido. Verifique a estrutura de dados dos códigos de barras.",
                gS1ExMaxAlphanumNumber: "A família do GS1 DataBar estendida pode codificar até 41 caracteres alfanuméricos.",
                gS1ExMaxNumericNumber: "A família do GS1 DataBar estendida poede codificar até 74 caracteres numéricos.",
                gS1Length: "O código de barras GS1 DataBar é utilizado para os códigos de barras GTIN 8, 12, 13, 14 e seu comprimento deve ser 7, 11, 12 ou 13. O último dígito é reservado como verificador.",
                gS1LimitedFirstChar: "O código do GS1 DataBar limitado deve possuir 0 ou 1 como primeiro dígito. E de dados GTIN-14 com um valor inicial superior a 1, requerem a utilização d códigos de barras Omnidirectional, Stacked, Stacked Omnidirectional ou Truncated.",
                i25Length: "O código de barras Interleaved2of5 deve ter um número par de dígitos. Caso seja um número ímpar, é necessário colocar um 0 no seu início.",
                intelligentMailLength: "O comprimento do valor dos dados no código de barras Intelligent Mail deve ser de 20, 25, 29 o 31 caracteres: código de controle de 20 dígitos (2 para o identificador do código de barras, 3 para o identificador do tipo de serviço, 6 ou 9 para identificação da origem e 9 ou 6 para o número de série) e 0, 5, 9 ou 11 para o CEP.",
                intelligentMailSecondDigit: "O segundo dígito deve estar no intervalo entre 0 e 4.",
                invalidAI: "Existem sequências inválidas no elemento identificador da aplicação. Certifique-se de que os identificadores estão formatados corretamente.",
                invalidCharacter: "O caráter '{0}'não é válido para o tipo de código de barras atual. Os caracteres válidos são: {1}",
                invalidDimension: "AS dimensões do código de barras não pode ser determinada porque por uma combinação incorreta dos valores das propriedades Stretch, BarsFillMode e XDimension.",
                invalidHeight: "As linhas da grade do código de barras (número {0}) não é compatível com a altura de ({1} pixels).",
                invalidLength: "Os dados do código de barras deve ter {0} dígitos.",
                invalidPostalCode: "PostalCode inválido: O tipo 2 identifica códigos postais de até 9 dígitos (código postal norte americano.), enquanto o tipo 3 identifica códigos alfanuméricos de até 6 caracteres.",
                invalidPropertyValue: "O valor da propriedade {0} de estar no intervalo entre {1} e {2}.",
                invalidVersion: "O número SizeVersion não gera células suficientes para codificar os dados no modo de codificação e o nível de correção de erros atuais.",
                invalidWidth: "As colunas da grade do código de barras (número {0}) não é compatível com a largura de ({1} pixels). Verifique o valor da propriedade XDimension e/ou da propriedade WidthToHeightRatio.",
                invalidXDimensionValue: "O valor XDimension deve estar no intervalo entre {0} e {1} para o tipo de código de barras atual.",
                maxLength: "O comprimento {0} do texto excede o máximo codificável para o tipo códigos de barras atual. Ele pode conter no máximo {1} caracteres.",
                notSupportedEncoding: "O código correspondente a {0} {1} não é permitido.",
                pDF417InvalidRowsColumnsCombination: "As codewords (de correção de dados e erros) excedem o limite que pode ser codificado em símbolos através de uma matriz {0}x{1}.",
                primaryMessageError: "Não é possível extrair a mensagem principal do valor de dados. Verifique a documentação da estrutura.",
                serviceClassError: "Não foi possível converter a classe de serviço. O valor deve ser numérico.",
                smallSize: "Não foi possível ajustar o tamanho da grade ({0}, {1}) com a configuração definida no Stretch.",
                unencodableCharacter: "O caráter '{0}' não pode ser codificado.",
                uPCEFirstDigit: "Por padrão, o primeiro dígito do UPCE sempre deve ser zero.",
                warningString: "Aviso Barcode: ",
                wrongCompactionMode: "A mensagem de dados não pode ser compactado com o modo {0}.",
                notLoadedEncoding: "A codificação {0} não foi carregada."
            }
        };
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Combo localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/
//Ricardo
/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Combo) {
        $.ig.Combo = {
            locale: {
                noMatchFoundText: 'Não há resultados',
                dropDownButtonTitle: 'Exibir lista suspensa',
                clearButtonTitle: 'Limpar valor',
                placeHolder: 'selecionar...',
                notSuported: 'Operação não suportada',
                errorNoSupportedTextsType: "Tipo de texto não suportado. São admitidos apenas textos do tipo: string e matriz.",
                errorUnrecognizedHighlightMatchesMode: "O Highlight Matches Mode não é reconhecido. Os valores suportados são multi, contains, startsWith, full e null."
            }
        };
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Dialog localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Dialog) {
        $.ig.Dialog = {
            locale: {
                closeButtonTitle: "Fechar",
                minimizeButtonTitle: "Minimizar",
                maximizeButtonTitle: "Maximizar",
                pinButtonTitle: "Fixar",
                unpinButtonTitle: "Desafixar",
                restoreButtonTitle: "Restaurar"
            }
        };
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Doughnut Chart localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.igDoughnutChart) {
        $.ig.igDoughnutChart = {};

        $.extend($.ig.igDoughnutChart, {
            locale: {
                invalidBaseElement: " não é permitido como elemento base. Use DIV em seu lugar."
            }
        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Editors localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Editor) {
        $.ig.Editor = {
            locale: {
                spinUpperTitle: 'Incrementar',
                spinLowerTitle: 'Reduzir',
                buttonTitle: 'Exibir lista',
                clearTitle: 'Excluir valor',
                datePickerButtonTitle: 'Exibir calendário',
                updateModeUnsupportedValue: 'A opção UpdateMode suporta dois valores possíveis: "onChange" e "immediate"'
            }
        };
    }
})(jQuery);

/*!@license
* Infragistics.Web.ClientUI Grid localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Grid) {
        $.ig.Grid = {};

        $.extend($.ig.Grid, {

            locale: {
                noSuchWidget: "Widget não foi carregado: ",
                autoGenerateColumnsNoRecords: "O AutoGenerateColumns está ativado, mas não há registros na fonte de dados para determinar as colunas",
                optionChangeNotSupported: "Não é permitido alterar a opção a seguir, após a criação do igGrid.",
                optionChangeNotScrollingGrid: "Não é possível alterar a opção a seguir, após a criação da grid.",
                noPrimaryKeyDefined: "Não foi definida una chave primária para a grid.",
                indexOutOfRange: "O índice de linhas especificado está fora do intervalo.",
                noSuchColumnDefined: "A chave de coluna especificada não corresponde a nenhuma das colunas definidas.",
                columnIndexOutOfRange: "O índice de coluna especificada está fora do intervalo.",
                recordNotFound: "Não foi encontrado o registro com Id. especificado na visão de dados.",
                columnNotFound: "Não foi encontrada a coluna correspondente a chave.",
                colPrefix: "Coluna ",
                columnVirtualizationRequiresWidth: "A virtualização / colunaVirtualização está estabelecida como True, mas não possível definir a largura das colunas da grade. É necessário definir os seguintes parâmetros: a) largura da grade, b) defaultColumnWidth c) largura definida para cada coluna",
                virtualizationRequiresHeight: "A virtualização foi definida como True, portanto, é necessário definir a altura da grade.",
                colVirtualizationDenied: "columnVirtualization só é aplicável para a virtualização fixa",
                noColumnsButAutoGenerateTrue: "AutoGenerateColumns definido como False, sem que fossem definidas as colunas da grade. É necessário definir AutoGenerateColumns como True ou especificar as colunas",
                noPrimaryKey: "Para utilizar a ferramenta igHierarchicalGrid é necessário definir uma chave primária.",
                templatingEnabledButNoTemplate: "jQueryTemplating foi definido como True, mas nenhuma rowTemplate foi definida.",
                expandTooltip: "Expandir linha",
                collapseTooltip: "Fechar linha",
                movingNotAllowedOrIncompatible: "Não foi possível substituir a coluna desejada. A coluna não foi encontrada ou o resultado não é compatível com o layout da coluna.",
                allColumnsHiddenOnInitialization: "Não é possível ocultar todas as colunas da grade. É necessário exibir ao menos uma das colunas.",
                columnVirtualizationNotSupportedWithPercentageWidth: "A virtualização das colunas não é permitida quando a largura da grade é definida em porcentagem.",
                mixedWidthsNotSupported: "Não é permitido configurar a largura da coluna mista/parcial. A definição de algumas larguras em porcentagem e outras em pixels não é suportada."
            }
        });

        $.ig.GridFiltering = $.ig.GridFiltering || {};

        $.extend($.ig.GridFiltering, {
            locale: {
                startsWithNullText: "Começa por...",
                endsWithNullText: "Termina com...",
                containsNullText: "Contém...",
                doesNotContainNullText: "Não contém...",
                equalsNullText: "Igual a...",
                doesNotEqualNullText: "Diferente de...",
                greaterThanNullText: "Maior que...",
                lessThanNullText: "Menor que...",
                greaterThanOrEqualToNullText: "Maior ou igual a...",
                lessThanOrEqualToNullText: "Menor ou igual a...",
                onNullText: "Contido em...",
                notOnNullText: "Não contido em...",
                afterNullText: "Depois",
                beforeNullText: "Antes",
                emptyNullText: "Vazio",
                notEmptyNullText: "Não vazio",
                nullNullText: "Nulo",
                notNullNullText: "Não nulo",
                startsWithLabel: "Começa com",
                endsWithLabel: "Termina com",
                containsLabel: "Contém",
                doesNotContainLabel: "Não contém",
                equalsLabel: "Igual a",
                doesNotEqualLabel: "Diferente de",
                greaterThanLabel: "Maior que",
                lessThanLabel: "Menor que",
                greaterThanOrEqualToLabel: "Maior ou igual a",
                lessThanOrEqualToLabel: "Menor ou igual a",
                trueLabel: "Verdadeiro",
                falseLabel: "Falso",
                afterLabel: "Depois",
                beforeLabel: "Antes",
                todayLabel: "Hoje",
                yesterdayLabel: "Ontem",
                thisMonthLabel: "Este mês",
                lastMonthLabel: "Mês anterior",
                nextMonthLabel: "Mês seguinte",
                thisYearLabel: "Ano atual",
                lastYearLabel: "Ano passado",
                nextYearLabel: "Ano seguinte",
                clearLabel: "Limpar filtro",
                noFilterLabel: "Não",
                onLabel: "Contido em",
                notOnLabel: "Não contido em",
                advancedButtonLabel: "Avançado",
                filterDialogCaptionLabel: "PESQUISA AVANÇADA",
                filterDialogConditionLabel1: "Mostrar registros correspondentes ",
                filterDialogConditionLabel2: " dos seguintes critérios",
                filterDialogOkLabel: "OK",
                filterDialogCancelLabel: "Cancelar",
                filterDialogAnyLabel: "QUALQUER",
                filterDialogAllLabel: "TODOS",
                filterDialogAddLabel: "Adicionar",
                filterDialogErrorLabel: "O número máximo de filtros foi excedido.",
                filterSummaryTitleLabel: "Resultados da pesquisa",
                filterSummaryTemplate: "${matches} Registros correspondentes",
                filterDialogClearAllLabel: "Excluir TODOS",
                tooltipTemplate: "${condition} filtro aplicado",
                // M.H. 13 Oct. 2011 Fix for bug #91007
                featureChooserText: "Ocultar filtro",
                featureChooserTextHide: "Exibir filtro",
                // M.H. 17 Oct. 2011 Fix for bug #91007
                featureChooserTextAdvancedFilter: "Filtro avançado",
                virtualizationSimpleFilteringNotAllowed: "Quando ativada a visualização horizontal, o filtro simples (linha do filtro) não é suportado.  Defina o modo como 'avançado' e / ou definir advancedModeEditorsVisible",
                featureChooserNotReferenced: "Não foi feita nenhuma referência ao Seletor de Funções. É necessário incluir o arquivo ig.ui.grid.featurechooser.js ou utilizar o carregador ou um dos arquivos de script combinado."
            }
        });

        $.ig.GridGroupBy = $.ig.GridGroupBy || {};

        $.extend($.ig.GridGroupBy, {
            locale: {
                emptyGroupByAreaContent: "{0} para selecionar ou arraste para esta área a(s) coluna(s) que deseja agrupar",
                emptyGroupByAreaContentSelectColumns: "Clique aqui",
                emptyGroupByAreaContentSelectColumnsCaption: "seleção de colunas",
                expandTooltip: "Expandir linha agrupada",
                collapseTooltip: "Fechar linha agrupada",
                removeButtonTooltip: "Remover coluna agrupada",
                featureChooserText: "Desagrupar por",
                featureChooserTextHide: "Agrupar por",
                modalDialogCaptionButtonDesc: "Clique para classificar de forma crescente",
                modalDialogCaptionButtonAsc: "Clique para ordenar de forma decrescente",
                modalDialogCaptionButtonUngroup: "Clique para desagrupar",
                modalDialogGroupByButtonText: "Agrupar por",
                modalDialogCaptionText: 'Adicionar ao grupo por',
                modalDialogDropDownLabel: 'Exibindo:',
                modalDialogClearAllButtonLabel: 'fechar todos',
                modalDialogRootLevelHierarchicalGrid: 'raiz',
                modalDialogDropDownButtonCaption: "Clique para exibir/ocultar",
                modalDialogButtonApplyText: 'Aplicar',
                modalDialogButtonCancelText: 'Cancelar',
                fixedVirualizationNotSupported: 'A função GroupBy não está disponível com a virtualização ativada.'
            }
        });

        $.ig.GridHiding = $.ig.GridHiding || {};

        $.extend($.ig.GridHiding, {
            locale: {
                columnChooserDisplayText: "Seletor de colunas",
                hiddenColumnIndicatorTooltipText: "Coluna(s) oculta(s)",
                columnHideText: "Ocultar",
                columnChooserCaptionLabel: "Seletor de colunas",
                columnChooserCheckboxesHeader: "ver",
                columnChooserColumnsHeader: "coluna",
                columnChooserCloseButtonTooltip: "Fechar",
                hideColumnIconTooltip: "Ocultar",
                featureChooserNotReferenced: "Não foi feita nenhuma referência ao Seletor de Funções. É necessário incluir o arquivo ig.ui.grid.featurechooser.js ou utilizar o carregador ou um dos arquivos de script combinado.",
                columnChooserShowText: "Exibir",
                columnChooserHideText: "Ocultar",
                columnChooserResetButtonLabel: "Reestabelecer",
                columnChooserButtonApplyText: 'Aplicar',
                columnChooserButtonCancelText: 'Cancelar'
            }
        });

        $.ig.GridResizing = $.ig.GridResizing || {};

        $.extend($.ig.GridResizing, {
            locale: {
                noSuchVisibleColumn: "Não foi encontrado nenhuma coluna visível ou com chave especificada. É necessário alterar o tamanho das columnas visíveis.",
                resizingAndFixedVirtualizationNotSupported: "A função de alteração de tamanho quando a virtualizacão ou a virtualização de colunas estão habilitadas com virtualizationMode. Defina virtualizationMode como 'continuous' ou utilize somente rowVirtualization."
            }
        });

        $.ig.GridPaging = $.ig.GridPaging || {};

        $.extend($.ig.GridPaging, {

            locale: {
                pageSizeDropDownLabel: "Exibir ",
                pageSizeDropDownTrailingLabel: "registros",
                //pageSizeDropDownTemplate: "Mostrar ${dropdown} registros",
                nextPageLabelText: "seguinte",
                prevPageLabelText: "anterior",
                firstPageLabelText: "",
                lastPageLabelText: "",
                currentPageDropDownLeadingLabel: "Pág",
                currentPageDropDownTrailingLabel: "de ${count}",
                //currentPageDropDownTemplate: "Pág ${dropdown} de ${count}",
                currentPageDropDownTooltip: "Escolher índices de páginas",
                pageSizeDropDownTooltip: "Escolher número de registros por página",
                pagerRecordsLabelTooltip: "Intervalo de registros atuais",
                prevPageTooltip: "ir à página anterior",
                nextPageTooltip: "ir à página seguinte",
                firstPageTooltip: "ir à primeira página",
                lastPageTooltip: "ir à última página",
                pageTooltipFormat: "página ${index}",
                pagerRecordsLabelTemplate: "${startRecord} - ${endRecord} de ${recordCount} registros",
                invalidPageIndex: "Índice de página não válido: deve ser igual ou superior a 0 e inferior ao número de páginas"
            }
        });

        $.ig.GridSelection = $.ig.GridSelection || {};

        $.extend($.ig.GridSelection, {
            locale: {
                persistenceImpossible: "A seleção persistente entre os estados requer que se defina a opção de primaryKey de igGrid. Defina uma chave primária ou desative a persistência."
            }
        });

        $.ig.GridRowSelectors = $.ig.GridRowSelectors || {};

        $.extend($.ig.GridRowSelectors, {

            locale: {
                selectionNotLoaded: "igGridSelection não foi inicializado. Habilite a função de Seleção para a grade ou defina a propriedade requireSelection da função Selectores de Linhas como Falso.",
                columnVirtualizationEnabled: "Não é possível utilizar igGridRowSelectors quando a virtualização de colunas está habilitada. Habilite somente a virtualização de filas ativando a propriedade 'rowVirtualization' da grade ou modificando a virtualização para 'continuous'."
            }
        });

        $.ig.GridSorting = $.ig.GridSorting || {};
        //validar com ricar ordenado
        $.extend($.ig.GridSorting, {
            locale: {
                sortedColumnTooltipFormat: 'ordenado ${direction}',
                unsortedColumnTooltip: 'clique para ordenar a coluna',
                ascending: 'crescente',
                descending: 'descendente',
                modalDialogSortByButtonText: 'ordenar por',
                modalDialogResetButton: "restaurar",
                modalDialogCaptionButtonDesc: "Clique para ordenar de forma descendente",
                modalDialogCaptionButtonAsc: "Clique para ordenar de forma ascendente",
                modalDialogCaptionButtonUnsort: "Clique para remover os critérios de ordenação",
                featureChooserText: "Ordenação múltipla",
                modalDialogCaptionText: "Ordem múltiplas",
                modalDialogButtonApplyText: 'Aplicar',
                modalDialogButtonCancelText: 'Cancelar',
                sortingHiddenColumnNotSupport: 'A ordenação de colunas ocultas não é permitida',
                featureChooserSortAsc: 'Ordenar de A a Z',
                featureChooserSortDesc: 'Ordenar de Z a A'
                //modalDialogButtonSlideCaption: "Haga clic para mostrar/ocultar columnas ordenadas"
            }
        });

        $.ig.GridSummaries = $.ig.GridSummaries || {};

        $.extend($.ig.GridSummaries, {
            locale: {
                featureChooserText: "Ocultar sumários",
                featureChooserTextHide: "Mostrar sumários",
                dialogButtonOKText: 'Aceitar',
                dialogButtonCancelText: 'Cancelar',
                emptyCellText: '',
                summariesHeaderButtonTooltip: 'Mostrar/ocultar sumários',
                // M.H. 13 Oct. 2011 Fix for bug 91008
                defaultSummaryRowDisplayLabelCount: 'Contar',
                defaultSummaryRowDisplayLabelMin: 'Mín.',
                defaultSummaryRowDisplayLabelMax: 'Máx.',
                defaultSummaryRowDisplayLabelSum: 'Soma',
                defaultSummaryRowDisplayLabelAvg: 'Média.',
                defaultSummaryRowDisplayLabelCustom: 'Personalizado',
                calculateSummaryColumnKeyNotSpecified: "Especifique a chave da coluna para sumarização",
                featureChooserNotReferenced: "Não foi feita qualquer referência ao script do Seletor de Funções. Inclua o arquivo ig.ui.grid.featurechooser.js ou utilize um dos arquivos de scripts combinado."
            }
        });

        $.ig.GridUpdating = $.ig.GridUpdating || {};

        $.extend($.ig.GridUpdating, {
            locale: {
                doneLabel: 'Concluir',
                doneTooltip: 'Concluir a edição e atualizar',
                cancelLabel: 'Cancelar',
                cancelTooltip: 'Concluir a edição e não atualizar',
                addRowLabel: 'Adicionar uma nova linha',
                addRowTooltip: 'Clique para adicionar uma nova linha',
                deleteRowLabel: '',
                deleteRowTooltip: 'Deletar linha',
                igEditorException: 'Para atualizar o ui.igGrid, o ui.igEditor deve estar carregado',
                igComboException: 'Para utilizar o tipo combinado na ui.igGrid, o ui.igCombo deve estar carregado',
                igRatingException: 'Para utilizar o igRating como editor na ui.igGrid, o ui.igRating deve estar carregado',
                igValidatorException: 'Opções de validação definidas no igGridUpdating necessitam que ui.igValidator esteja carregado',
                noPrimaryKeyException: 'Para permitir as operações de atualizacão depois de fechar uma linha, a aplicação deve definir uma "primaryKey" entre as opções de igGrid.',
                hiddenColumnValidationException: 'Não é possível editar uma linha que possua uma coluna oculta com validacão habilitada.',
                dataDirtyException: 'A grade possui operações pendentes que podem afetar a representação de dados. É necessário habilitar a opção "autoCommit" de igGrid ou processar o evento "dataDirty" de igGridUpdating como Falso. Ao processar esse evento, será possível executar o "commit()" de dados na igGrid.',
                recordOrPropertyNotFoundException: 'Registro ou propriedade não encontrado na fonte de dados.',
                rowEditDialogCaptionLabel: 'Editar dados da linha',
                unboundColumnsNotSupported: 'ColumnFixing não é suportado com colunas desacopladas',
                excelNavigationNotSupportedWithCurrentEditMode: "O modo de navegação Excel permite somente a edição nos modos de célula e linha. Desative excelNavigationMode e configure o editMode na célula ou linha."
            }
        });

        $.ig.ColumnMoving = $.ig.ColumnMoving || {};

        $.extend($.ig.ColumnMoving, {
            locale: {
                movingDialogButtonApplyText: 'Aplicar',
                movingDialogButtonCancelText: 'Cancelar',
                movingDialogCaptionButtonDesc: 'Baixar',
                movingDialogCaptionButtonAsc: 'Carregar',
                movingDialogCaptionText: 'Mover colunas',
                movingDialogDisplayText: 'Mover colunas',
                movingDialogDropTooltipText: "Mover para cá",
                dropDownMoveLeftText: 'Mover à esquerda',
                dropDownMoveRightText: 'Mover à direita',
                dropDownMoveFirstText: 'Mover primeiro',
                dropDownMoveLastText: 'Mover por último',
                featureChooserNotReferenced: "Não foi feita qualquer referência ao script do seletor de funções. Para evitar a exibição dessa mensagem, inclua o arquivo ig.ui.grid.featurechooser.js ou utilize o carregador e/ou um dos arquivos de script.",
                movingToolTipMove: 'Mover',
                featureChooserSubmenuText: 'Mover'
            }
        });

        $.ig.ColumnFixing = $.ig.ColumnFixing || {};
        //Validar com Ricardo
        $.extend($.ig.ColumnFixing, {
            locale: {
                headerFixButtonText: 'Clique para fixar esta coluna',
                headerUnfixButtonText: 'Clique para desafixar esta coluna',
                featureChooserTextFixedColumn: 'Fixar coluna',
                featureChooserTextUnfixedColumn: 'Soltar coluna',
                groupByNotSupported: 'igGridGroupBy não é suportado com ColumnFixing',
                virtualizationNotSupported: 'A virtualização da opção de grade permite a virtualização de linhas e colunas. Não é possível virtualiza colunas através do ColumnFixing. Ative a opção de grade rowVirtualization',
                columnVirtualizationNotSupported: 'A virtualização de colunas não é suportada com ColumnFixing',
                columnMovingNotSupported: 'igGridColumnMoving não é suportada com ColumnFixing',
                hidingNotSupported: 'igGridHiding não é suportada com ColumnFixing',
                hierarchicalGridNotSupported: 'igHierarchicalGrid não é suportada com ColumnFixing',
                responsiveNotSupported: 'igGridResponsive não é suportada com ColumnFixing',
                noGridWidthNotSupported: 'É necessário especificar a largura da grade em pixels ao utilizar ColumnFixing',
                defaultColumnWidthInPercentageNotSupported: "A largura da coluna padrão em percentagem não é suportada quando usado se utiliza ColumnFixing",
                columnsWidthShouldBeSetInPixels: 'ColumnFixing requer que a largura de todas as colunas da grade seja definido como pixels. Verifique a largura das colunas: ',
                unboundColumnsNotSupported: 'ColumnFixing não permite o uso de colunas soltas',
                excelNavigationNotSupportedWithCurrentEditMode: "O modo de navegação Excel permite somente a edição de células e de linhas. Desative o excelNavigationMode e utilize o editMode em célula ou linha.",
                internalErrors: {
                    none: 'Nenhum erro',
                    notValidIdentifier: 'Não existe nenhuma coluna com o identificador especificado',
                    fixingRefused: 'Configuração negada porque há apenas uma coluna solta visível',
                    fixingRefusedMinVisibleAreaWidth: 'Não é permitido fixar uma nova coluna devido a largura mínima das colunas soltas já apresentadas',
                    alreadyHidden: 'Tentando fixar/soltar uma coluna oculta',
                    alreadyUnfixed: 'Essa coluna já foi desafixada',
                    alreadyFixed: 'Essa coluna já foi fixada',
                    unfixingRefused: 'Ação negada porque há apenas uma coluna fixada visivel e pelo menos uma coluna oculta.',
                    targetNotFound: 'Coluna de destino não encontrada ou com identificador não especificado'
                }
            }
        });

        $.ig.GridAppendRowsOnDemand = $.ig.GridAppendRowsOnDemand || {};

        $.extend($.ig.GridAppendRowsOnDemand, {
            locale: {
                loadMoreDataButtonText: 'Carregar mais dados',
                appendRowsOnDemandRequiresHeight: 'A função AppendRowsOnDemand requer preenchimento da altura',
                groupByNotSupported: 'igGridGroupBy não é suportado com AppendRowsOnDemand',
                pagingNotSupported: 'igGridPaging não é suportado com AppendRowsOnDemand',
                cellMergingNotSupported: 'igGridCellMerging não é suportado com AppendRowsOnDemand',
                virtualizationNotSupported: 'Virtualização não é suportado com AppendRowsOnDemand'
            }
        });

        $.ig.igGridResponsive = $.ig.igGridResponsive || {};

        $.extend($.ig.igGridResponsive, {
            locale: {
                fixedVirualizationNotSupported: 'igGridResponsive não permite virtualização fixa'
            }
        });

        $.ig.igGridMultiColumnHeaders = $.ig.igGridMultiColumnHeaders || {};

        $.extend($.ig.igGridMultiColumnHeaders, {
            locale: {
                multiColumnHeadersNotSupportedWithColumnVirtualization: 'Não é possível utilizar cabeçalhos de múltiplas colunas em columnVirtualization'
            }
        });

    }
})(jQuery);

/*!@license
* Infragistics.Web.ClientUI HTML Editor localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.HtmlEditor) {
        $.ig.HtmlEditor = {};

        $.extend($.ig.HtmlEditor, {

            locale: {
                boldButtonTitle: 'Negrito',
                italicButtonTitle: 'Itálico',
                underlineButtonTitle: 'Sublinhado',
                strikethroughButtonTitle: 'Tachado',
                increaseFontSizeButtonTitle: 'Aumentar tamanho da fonte',
                decreaseFontSizeButtonTitle: 'Diminuir tamanho da fonte',
                alignTextLeftButtonTitle: 'Alinhar texto a esquerda',
                alignTextRightButtonTitle: 'Alinhar texto a direita',
                alignTextCenterButtonTitle: 'Centralizar',
                justifyButtonTitle: 'Justificar',
                bulletsButtonTitle: 'Marcadores',
                numberingButtonTitle: 'Numeração',
                decreaseIndentButtonTitle: 'Diminuir recuo',
                increaseIndentButtonTitle: 'Aumentar recuo',
                insertPictureButtonTitle: 'Inserir imagem',
                fontColorButtonTitle: 'Cor da fonte',
                textHighlightButtonTitle: 'Cor do realce do texto',
                insertLinkButtonTitle: 'Inserir hiperlink',
                insertTableButtonTitle: 'Tabela',
                addRowButtonTitle: 'Adicionar linha',
                removeRowButtonTitle: 'Remover linha',
                addColumnButtonTitle: 'Adicionar coluna',
                removeColumnButtonTitle: 'Remover coluna',
                inserHRButtonTitle: 'Inserir régua horizontal',
                viewSourceButtonTitle: 'Exibir origem',
                cutButtonTitle: 'Cortar',
                copyButtonTitle: 'Copiar',
                pasteButtonTitle: 'Colar',
                undoButtonTitle: 'Desfazer',
                redoButtonTitle: 'Refazer',
                imageUrlDialogText: 'Endereço URL da imagem:',
                imageAlternativeTextDialogText: 'Texto alternativo:',
                imageWidthDialogText: 'A largura da imagem:',
                imageHeihgtDialogText: 'Altura da imagem:',
                linkNavigateToUrlDialogText: 'Abrir URL:',
                linkDisplayTextDialogText: 'Exibir texto:',
                linkOpenInDialogText: 'Abrir em:',
                linkTargetNewWindowDialogText: 'Nova janela',
                linkTargetSameWindowDialogText: 'Mesma janela',
                linkTargetParentWindowDialogText: 'Janela primária',
                linkTargetTopmostWindowDialogText: 'Janela de nível superior',
                applyButtonTitle: 'Aplicar',
                cancelButtonTitle: 'Cancelar',
                defaultToolbars: {
                    textToolbar: "Barra de ferramentas de manipulação de texto",
                    formattingToolbar: "Barra de ferramentas de formatação de texto",
                    insertObjectToolbar: "Barra de ferramentas de inserção de objetos",
                    copyPasteToolbar: "Barra de ferramentas copiar / colar"
                },
                fontNames: {
                    win: [
                            { text: "Times New Roman", value: "Times New Roman" },
                            { text: "Arial", value: "Arial" },
                            { text: "Arial Black", value: "Arial Black" },
                            { text: "Helvetica", value: "Helvetica" },
                            { text: "Comic Sans MS", value: "Comic Sans MS" },
                            { text: "Courier New", value: "Courier New" },
                            { text: "Georgia", value: "Georgia" },
                            { text: "Impact", value: "Impact" },
                            { text: "Lucida Console", value: "Lucida Console" },
                            { text: "Lucida Sans Unicode", value: "Lucida Sans Unicode" },
                            { text: "Palatino Linotype", value: "Palatino Linotype" },
                            { text: "Tahoma", value: "Tahoma" },
                            { text: "Trebuchet MS", value: "Trebuchet MS" },
                            { text: "Verdana", value: "Verdana" },
                            { text: "Symbol", value: "Symbol" },
                            { text: "Webdings", value: "Webdings" },
                            { text: "Wingdings", value: "Wingdings" },
                            { text: "MS Sans Serif", value: "MS Sans Serif" },
                            { text: "MS Serif", value: "MS Serif" }
                    ],
                    mac: [
                            { text: "Times New Roman", value: "Times New Roman" },
                            { text: "Arial", value: "Arial" },
                            { text: "Arial Black", value: "Arial Black" },
                            { text: "Helvetica", value: "Helvetica" },
                            { text: "Comic Sans MS", value: "Comic Sans MS" },
                            { text: "Courier New", value: "Courier New" },
                            { text: "Georgia", value: "Georgia" },
                            { text: "Impact", value: "Impact" },
                            { text: "Monaco", value: "Monaco" },
                            { text: "Lucida Grande", value: "Lucida Grande" },
                            { text: "Book Antiqua", value: "Book Antiqua" },
                            { text: "Geneva", value: "Geneva" },
                            { text: "Trebuchet MS", value: "Trebuchet" },
                            { text: "Verdana", value: "Verdana" },
                            { text: "Symbol", value: "Symbol" },
                            { text: "Webdings", value: "Webdings" },
                            { text: "Zapf Dingbats", value: "Zapf Dingbats" },
                            { text: "New York", value: "New York" }
                    ]
                },
                fontSizes: [
                    { text: "1", value: "7.5 pt" },
                    { text: "2", value: "10 pt" },
                    { text: "3", value: "12 pt" },
                    { text: "4", value: "13.5 pt" },
                    { text: "5", value: "18 pt" },
                    { text: "6", value: "24 pt" },
                    { text: "7", value: "36 pt" }
                ],
                formatsList: [
                        { text: "h1", value: "Cabeçalho 1" },
                        { text: "h2", value: "Cabeçalho 2" },
                        { text: "h3", value: "Cabeçalho 3" },
                        { text: "h4", value: "Cabeçalho 4" },
                        { text: "h5", value: "Cabeçalho 5" },
                        { text: "h6", value: "Cabeçalho 6" },
                        { text: "p", value: "Normal" }
                ]
            }

        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Pivot Shared localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.PivotShared) {
        $.ig.PivotShared = {};

        $.extend($.ig.PivotShared, {
            locale: {
                invalidDataSource: "Fonte de dados invalida.",
                measureList: "Medidas",
                ok: "Aceitar",
                cancel: "Cancelar",
                addToMeasures: "Adicionar às medidas",
                addToFilters: "Adicionar aos filtros",
                addToColumns: "Adicionar às colunas",
                addToRows: "Adicionar às linhas"
            }
        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Pivot Data Selector localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.PivotDataSelector) {
        $.ig.PivotDataSelector = {};

        $.extend($.ig.PivotDataSelector, {
            locale: {
                invalidBaseElement: " tipo de elemento base não permitido. Utilize um elemento DIV em seu lugar.",
                catalog: "Catálogo",
                cube: "Cubo",
                measureGroup: "Grupo de medidas",
                measureGroupAll: "(Tudo)",
                rows: "Linhas",
                columns: "Colunas",
                measures: "Medidas",
                filters: "Filtros",
                deferUpdate: "Aplicar atualização",
                updateLayout: "Atualizar layout",
                selectAll: "Selecionar tudo"
            }
        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Pivot Grid localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.PivotGrid) {
        $.ig.PivotGrid = {};

        $.extend($.ig.PivotGrid, {
            locale: {
                filtersHeader: "Soltar aqui os campos de filtro",
                measuresHeader: "Soltar aqui os elementos de medida",
                rowsHeader: "Soltar aqui os campos de linha",
                columnsHeader: "Soltar aqui os campos de coluna",
                disabledFiltersHeader: "Campos de filtro",
                disabledMeasuresHeader: "Elementos de medidas",
                disabledRowsHeader: "Campos de linha",
                disabledColumnsHeader: "Campos de coluna",
                noSuchAxis: "Eixo inexistente"
            }
        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Popover localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Popover) {
        $.ig.Popover = {};

        $.extend($.ig.Popover, {
            locale: {
                popoverOptionChangeNotSupported: "Não é permitido alterar a seguinte opção depois de inicializar o igPopover:",
                popoverShowMethodWithoutTarget: "O alvo do parâmetro da função exibido é obrigatório quando se utiliza essa opção selectors"
            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Rating localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Rating) {
        $.ig.Rating = {};

        $.extend($.ig.Rating, {
            locale: {
                setOptionError: 'Alterações em tempo de execução não são permitidas às seguinte opções: '
            }
        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Splitter localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Splitter) {
        $.ig.Splitter = {};

        $.extend($.ig.Splitter, {
            locale: {
                errorPanels: 'O número de painéis não pode ser maior que dois.',
                errorSettingOption: 'Falha ao selecionar opção.'
            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Tile Manager localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.TileManager) {
        $.ig.TileManager = {};

        $.extend($.ig.TileManager, {
            locale: {
                renderDataError: "Os dados não foram recuperados ou analisados corretamente.",
                setOptionItemsLengthError: "O tamanho dos itens configurados não corresponde ao número de tiles."
            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Toolbar localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Toolbar) {
        $.ig.Toolbar = {};

        $.extend($.ig.Toolbar, {

            locale: {
                collapseButtonTitle: 'Recolher',
                expandButtonTitle: 'Expandir'
            }

        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Tree localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Tree) {
        $.ig.Tree = {};

        $.extend($.ig.Tree, {
            locale: {
                invalidArgumentType: 'O tipo de argumento informado não é válido.',
                errorOnRequest: 'Ocorreu um erro na recuperação de dados: ',
                noDataSourceUrl: 'O controle igTree requer que se informe uma dataSourceUrl para iniciar a solicitações dados para a URL.',
                incorrectPath: 'Caminho incorreto: ',
                incorrectNodeObject: 'O argumento fornecido não é um elemento de nó de jQuery.',
                setOptionError: 'Alterações em tempo de execução não são permitidas para a seguinte opção: ',
                moveTo: '<strong>Mover para</strong> {0}',
                moveBetween: '<strong>Mover entre</strong> {0} y {1}',
                moveAfter: '<strong>Mover depois de</strong> {0}',
                moveBefore: '<strong>Mover antes de</strong> {0}',
                copyTo: '<strong>Copiar em</strong> {0}',
                copyBetween: '<strong>Copiar entre</strong> {0} y {1}',
                copyAfter: '<strong>Copiar depois de</strong> {0}',
                copyBefore: '<strong>Copiar antes de</strong> {0}',
                and: 'e'
            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Tree Grid localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.TreeGrid) {
        $.ig.TreeGrid = {};

        $.extend($.ig.TreeGrid, {
            locale: {
                fixedVirtualizationNotSupported: 'Virtualização fixa não suportada. Por favor selecione o modo de virtualização continua para permitir a virtualização da linha.'
            }
        });
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Upload localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Upload) {
        $.ig.Upload = {};

        $.extend($.ig.Upload, {

            locale: {
                labelUploadButton: "Carregar arquivo",
                labelAddButton: "Adicionar",
                labelClearAllButton: "Excluir carregados ",
                // M.H. 13 May 2011 - fix bug 75042
                labelSummaryTemplate: "{0} de {1} carregados",
                labelSummaryProgressBarTemplate: "{0}/{1}",
                labelShowDetails: "Mostrar detalhes",
                labelHideDetails: "Ocultar detalhes",
                labelSummaryProgressButtonCancel: "Cancelar",
                // M.H. 1 June 2011 Fix bug #77532
                labelSummaryProgressButtonContinue: "Carregar",
                labelSummaryProgressButtonDone: "Concluído",
                labelProgressBarFileNameContinue: "...",

                //error messages
                errorMessageFileSizeExceeded: "O tamanho máximo do arquivo foi excedido.",
                errorMessageGetFileStatus: "Não foi possível obter o estado do arquivo atual! Provavelmente houve um corte na conexão.",
                errorMessageCancelUpload: "Não é possível enviar comandos para o servidor para cancelar o carregamento! Provavelmente, houve um corte na conexão.",
                errorMessageNoSuchFile: "O arquivo solicitado não foi encontrado. Probablemente o arquivo seja grande demais.",
                errorMessageOther: "Erro interno ao carregar o arquivo. Código de error: {0}.",
                errorMessageValidatingFileExtension: "Erro na validação da extensão do arquivo.",
                errorMessageAJAXRequestFileSize: "Erro de AJAX ao tentar obter o tamanho do arquivo.",
                errorMessageMaxUploadedFiles: "O número máximo de arquivos que podem ser carregados foi excedido.",
                errorMessageMaxSimultaneousFiles: "O valor de maxSimultaneousFilesUploads está incorreto. Deve ser maior que a 0 ou nulo.",
                errorMessageTryToRemoveNonExistingFile: "Você está tentando excluir um arquivo inexistente com o Id. {0}.",
                errorMessageTryToStartNonExistingFile: "Você está tentando executar um arquivo inexistente com o Id. {0}.",

                // M.H. 12 May 2011 - fix bug 74763: add title to all buttons
                // title attributes            
                titleUploadFileButtonInit: "Carregar arquivo",
                titleAddFileButton: "Adicionar",
                titleCancelUploadButton: "Cancelar",
                // M.H. 1 June 2011 Fix bug #77532
                titleSummaryProgressButtonContinue: "Carregar",
                titleClearUploaded: "Excluir arquivos carregados",
                titleShowDetailsButton: "Mostrar detalhes",
                titleHideDetailsButton: "Ocultar detalhes",
                titleSummaryProgressButtonCancel: "Cancelar",
                titleSummaryProgressButtonDone: "Concluído",
                // M.H. 1 June 2011 Fix bug #77532
                titleSingleUploadButtonContinue: "Carregar",
                titleClearAllButton: "Excluir arquivos carregados"
            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Validator localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Validator) {
        $.ig.Validator = {
            locale: {
                defaultMessage: 'Edite este campo',
                selectMessage: 'Informe um valor',
                rangeSelectMessage: 'Informe um número de elementos entre {1} e {0}',
                minSelectMessage: 'Informe pelo menos {0} elementos',
                maxSelectMessage: 'Não selecione mais que {0} elementos',
                rangeLengthMessage: 'Informe um valor que contenha entre {0} e {1} caracteres',
                minLengthMessage: 'Informe pelo menos {0} caracteres',
                maxLengthMessage: 'Não digite mais que {0} caracteres',
                requiredMessage: 'Este campo é obrigatório',
                maskMessage: 'Informar todos os dados obrigatórios',
                dateFieldsMessage: 'Informar os campos de data',
                invalidDayMessage: 'Dia do mês inválido. Informe o dia correto',
                dateMessage: 'Informe uma data válida',
                numberMessage: 'Informe um número válido',
                rangeMessage: 'Informe um valor entre {0} e {1}',
                minMessage: 'Informe um valor maior ou igual a {0}',
                maxMessage: 'Informe um valor maior ou igual a {0}'
            }
        };
    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Video Player localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.VideoPlayer) {
        $.ig.VideoPlayer = {};

        $.extend($.ig.VideoPlayer, {

            locale: {
                liveStream: "Vídeo ao vivo",
                live: "Ao vivo",
                paused: "Pausado",
                playing: "Reproduzindo",
                play: 'Reproduzir',
                volume: "Volume",
                unsupportedVideoSource: "Formato de vídeo não compatível com seu navegador.",
                missingVideoSource: "Fonte de vídeo não suportada.",
                progressLabelLongFormat: "$hora atual$ / $duração$",
                progressLabelShortFormat: "$hora atual$",
                enterFullscreen: "Mostrar em tela cheia",
                exitFullscreen: "Sair da tela cheia",
                skipTo: "IR PARA",
                unsupportedBrowser: "O navegador atual não oferece suporte a vídeo HTML5. <br/>Tente atualizar para uma das seguintes versões:",
                currentBrowser: "Navegador atual: {0}",
                ie9: "Microsoft Internet Explorer V 9+",
                chrome8: "Google Chrome V 8+",
                firefox36: "Mozilla Firefox V 3.6+",
                safari5: "Apple Safari V 5+",
                opera11: "Opera V 11+",
                ieDownload: "http://www.microsoft.com/windows/internet-explorer/default.aspx",
                operaDownload: "http://www.opera.com/download/",
                chromeDownload: "http://www.google.com/chrome",
                firefoxDownload: "http://www.mozilla.com/",
                safariDownload: "http://www.apple.com/safari/download/",
                buffering: 'Armazenando em buffer...',
                adMessage: 'Publicidade: O vídeo será retomado em $duração$ segundos.',
                adMessageLong: 'Publicidade: O vídeo será retomado em $duração$.',
                adMessageNoDuration: 'Publicidade: O vídeo será retomado após o anúncio.',
                adNewWindowTip: 'Publicidade: Clique para abrir o conteúdo do anúncio em uma nova janela.',
                nonDivException: 'O Infragistics HTML5 Video Player só pode ser instanciado em uma tag DIV.',
                relatedVideos: 'VÍDEOS RELACIONADOS',
                replayButton: 'Voltar a reproduzir',
                replayTooltip: 'Clique para reproduzir o último vídeo.'
            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI Zoombar localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.Zoombar) {
        $.ig.Zoombar = {};

        $.extend($.ig.Zoombar, {

            locale: {
                zoombarTargetNotSpecified: "Alvo da barra de Zoom não especificado.",
                zoombarTypeNotSupported: "Esse tipo de ferramenta não é suportado pela barra de zoom.",
                optionChangeNotSupported: "Não é possível alterar essa opção com a barra de zoom ativada:"
            }
        });

    }
})(jQuery);
/*!@license
* Infragistics.Web.ClientUI common utilities localization resources 15.1.20151.1005
*
* Copyright (c) 2011-2015 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
(function ($) {
    $.ig = $.ig || {};

    if (!$.ig.util) {
        $.ig.util = {};

        $.extend($.ig.util, {

            locale: {
                unsupportedBrowser: "O navegador atual não oferece suporte ao elemento canvas de HTML5. <br/>Tente atualizar para uma das seguintes versões:",
                currentBrowser: "Navegador atual: {0}",
                ie9: "Microsoft Internet Explorer V 9+",
                chrome8: "Google Chrome V 8+",
                firefox36: "Mozilla Firefox V 3.6+",
                safari5: "Apple Safari V 5+",
                opera11: "Opera V 11+",
                ieDownload: "http://www.microsoft.com/windows/internet-explorer/default.aspx",
                operaDownload: "http://www.opera.com/download/",
                chromeDownload: "http://www.google.com/chrome",
                firefoxDownload: "http://www.mozilla.com/",
                safariDownload: "http://www.apple.com/safari/download/"
            }
        });

    }
})(jQuery);

