Instruções para alterar o idioma da Pivot(flexmonster)

Para alterar o idioma da Toolbar
--No diretório da 'flexmonster/toolbar/language' do LIA, encontra-se o modelo do arquivo para traduçao da toolbar,
	o modelo para cópia é o da lingua inglesa.
	-- Basta duplicar o arquivo js com e renomea-lo baseado no idioma que ira editar, ex(fr-fr(frances da frança) fr-fr.js ;es-es(espanhol da espanha) es-es.js), etc.
	-- Após duplica-lo e renomea-lo alterar o retorno do metodo langToolbar() para o prefixo do idioma em questão como no exemplo do nome do arquivo citado acima
		e traduzir os valores dos atributos do metodo langPropsToolbar()

	***OBS: Após duplica-lo e renomea-lo não se esqueça de configurar o novo arquivo com Build Action igual a Embedded Resource

Para alterar o idioma das configurações das propriedades da pivot
--No diretório da 'flexmonster/report_lang' do LIA, encontra-se o modelo do arquivo para traduçao das propriedades da pivot,
	--Neste diretório existem dois arquivos 'js' que terminam com o sufixo do idioma padrão que é o portugues do brasil ("pt-br")
		--Basta duplica-los e renomea-los baseados no idioma que ira editar, ex(fr-fr(frances da frança) fr-fr.js ;es-es(espanhol da espanha) es-es.js), etc.
		***Arquivo report_pt-br.xml
			--Neste arquivo esta a referencia para o arquivo que realmente faz a tradução da propriedades e mas algumas propriedades como mostrado abaixo:
				--Alem do nome do arquivo que devera ser alterado, tambem é necessario alterar o parametro "localSettingsURL" para o nome do xml do novo
					arquivo.
					Exemplo:<param name="localSettingsURL">report_lang/loc_en-us.xml</param> //alterado para o arquivo que corresponde ao idioma em inglês.
					<config>
					  <params>
						<param name="localSettingsURL">report_lang/loc_pt-br.xml</param>
						<param name="showChartsWarning">false</param>
						<param name="showHeaders">false</param>
						<param name="fitGridlines">false</param>
						<param name="datePattern">dd/MM/yyyy</param>
						<param name="dateTimePattern">dd/MM/yyyy</param>
						<param name="configuratorActive">false</param>    
					  </params>
					</config>

		***Arquivo loc_pt-br.xml
			--Neste arquivo estão os atributos no formato xml que devem ser traduzidos para o novo idioma escolhido.

	***OBS: Após duplica-los e renomea-los não se esqueça de configurar os novos arquivos com Build Action igual a Embedded Resource

***Feito isso basta compilar e publicar o projeto do LIA.

///// Propriedades add no arquivo de tradução
file = lib/flexmonster/report_lang/loc_pt-br
nó "toolbar"
	"save_as_linx": "Salvar como",
    "delete_layout_linx": "Excluir Layout",