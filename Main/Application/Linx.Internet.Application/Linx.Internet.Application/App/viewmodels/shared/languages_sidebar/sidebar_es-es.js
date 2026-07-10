define([], function () {

    var main = {
        langSidebar: function () {
            return "es-es";
        },

        langPropsSidebar: function () {
            return Labels = {
                sair: "Salir",
                ambientes: "Ambientes",
                alterarSenha: "Cambiar contraseña",
                reAutenticar: "Volver a autenticarse",
                temas: "Temas",
                grpEcon: "Grupo económico",
                empresa: "Empresa",
                redePadrao: "Red predeterminada",
                cache: "Cache",
                dados: "Datos",
                limpar: "Limpiar",
                suporte: "Apoyo",
                gerarUrlSuporte: "Generar Url para Soporte",
                urlSuporte: "Soporte Url",
                configuracao: "Configuración",
                titlePaginaInicial: "Página de inicio",
                resultadoTabular: "Salida tabular",
                manterUltimoFiltro: "Mantener último filtro",
                esconderAssistentes: "Ocultar asistentes",
                idioma: "Idioma",

                availableLanguages: [
                    { id: "pt-br", name: "Portugués" },
                    { id: "en-us", name: "Inglés" },
                    { id: "es-es", name: "Español" }
                ],
            }
        }

    }

    return main;
});

