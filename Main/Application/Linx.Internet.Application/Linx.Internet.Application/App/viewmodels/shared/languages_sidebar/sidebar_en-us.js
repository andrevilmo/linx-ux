define([], function () {

    var main = {
        langSidebar: function () {
            return "en-us";
        },

        langPropsSidebar: function () {
            return Labels = {
                sair: "Exit",
                ambientes: "Environments",
                alterarSenha: "Change Password",
                reAutenticar : "Re-Authenticate",
                temas: "Themes",
                grpEcon: "Economic Group",
                empresa: "Company",
                redePadrao: "Default Network",
                cache: "Cache",
                dados: "Data",
                limpar: "Clear",
                suporte: "Support",
                gerarUrlSuporte: "Generate Url for Support",
                urlSuporte: "Url Support",
                configuracao: "Configuration",
                titlePaginaInicial: "Home page",
                resultadoTabular: "Tabular Output",
                manterUltimoFiltro: "Keep Last Filter",
                esconderAssistentes: "Hide Wizards",
                idioma: "Language",

                availableLanguages: [
                    { id: "pt-br", name: "Portuguese" },
                    { id: "en-us", name: "English" },
                    { id: "es-es", name: "Spanish" }
                ],
            }
        }

    }

    return main;
});

