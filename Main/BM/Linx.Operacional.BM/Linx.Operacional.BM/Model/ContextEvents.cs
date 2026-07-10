using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using Linx.Tools;
using System.Linq;
using System.Data;
using Linx.Operacional.BM.Rules.PedidoEntrada;
using Linx.Operacional.BM.Model;
using Linx.Operacional.BM.Rules.Estoque;
using Linx.Operacional.BM.Rules.OperacaoFinalidade;
using Linx.Operacional.BM.Rules.Filial;

namespace Linx.Operacional.BM
{
    public enum Aplicativo
    {
        ConsoleAdministracao = 1,
        Administrativo = 2,
        Operacional = 3
    }
    /// <summary>
    /// Events for executing rules before and after saving the context.
    /// e.g.: var addedEntities = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added);
    /// </summary>
    public partial class ContextEvents
    {
        //public static List<KeyValuePair<int, int>> listaNumeroPedidoFilial = new List<KeyValuePair<int, int>>();
        public static List<LGE_PEDIDO_ITEM> listaItensAdicionados = new List<LGE_PEDIDO_ITEM>();
        public static PedidoItensExistentes pedidoItensExistentes = new PedidoItensExistentes();
        public static List<LJV_LOJA> listaLojasAdicionadas = new List<LJV_LOJA>();
        public static bool estaSalvandoInativos = false;

        public static int? GetIdLinxOperacional()
        {
            int? idLinx = null;
            try
            {
                idLinx = Linx.Business.Tools.UserServiceHelper.GetCurrentIdLinx("connLinxOperacional", (int)Aplicativo.Operacional);
            }
            catch
            {
#if DEBUG
                idLinx = 1;
#else
                throw new Exception("Não foi possível determinar o ID_LINX.");
#endif

            }

#if DEBUG
            if (idLinx.IsNullOrEmpty()) idLinx = 1;
#endif

            return idLinx;
        }

        public static bool BeforeSaveChanges(DbContext context)
        {
            int sequencialId = 0;

            listaLojasAdicionadas = context.ChangeTracker.Entries().Where(f => f.State == EntityState.Added && f.Entity.IsTypeOf("LJV_LOJA")).Select(f => f.Entity as LJV_LOJA).ToList();

            //var condicoesPagamento = context.ChangeTracker.Entries().Where(f => (f.State == EntityState.Added || f.State == EntityState.Modified) && f.Entity.IsTypeOf("LNF_CONDICAO_PAGAMENTO")).Select(f => f.Entity).ToList();
            //if (condicoesPagamento.Count > 0)
            //{
            //    foreach (LNF_CONDICAO_PAGAMENTO item in condicoesPagamento)
            //        item.DATA_ULTIMA_ATUALIZACAO = DateTime.Now;
            //}
            //else
            //{
            //    var condicoesPagamentoFKs = context.ChangeTracker.Entries().Where(f => (f.State == EntityState.Added || f.State == EntityState.Modified) && (f.Entity.IsTypeOf("LNF_CONDICAO_PAGAMENTO_FILTRO") || f.Entity.IsTypeOf("LNF_CONDICAO_PAGAMENTO_PARCELA"))).Select(f => f.Entity).ToList();
            //    if (condicoesPagamentoFKs.Count > 0)
            //    {
            //        List<int> idsCondicaoPagamento = new List<int>();
            //        foreach (var item in condicoesPagamentoFKs)
            //        {
            //            if (item.IsTypeOf("LNF_CONDICAO_PAGAMENTO_FILTRO"))
            //            {
            //                var condPagtoFiltro = (LNF_CONDICAO_PAGAMENTO_FILTRO)item;
            //                idsCondicaoPagamento.add
            //            }
            //        }
            //    }
            //}
            
            #region Alterações referente a pedido
            pedidoItensExistentes.IdPedido = 0;
            pedidoItensExistentes.IdsItens = new List<int>();
            pedidoItensExistentes.IdsItens.Clear();
            listaItensAdicionados.Clear();
            

            var poc = ((IObjectContextAdapter)context).ObjectContext;

            ContextEvents contextEvents = new ContextEvents();

            LinxOperacional operacionalContext = context as LinxOperacional;

            var listaOperacoes = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added || c.State == EntityState.Deleted || c.State == EntityState.Modified);

            #region [LOGPEDIDO]

            var listaOperacoesPedidoItem = listaOperacoes.Where(p => p.Entity.IsTypeOf("LGE_PEDIDO_ITEM"));
            var listaExclusoesPedido = listaOperacoes.Where(p => p.Entity.IsTypeOf("LGE_PEDIDO") && p.State == EntityState.Deleted);

            var listaOperacoesPedido = listaOperacoes.Where(p => p.Entity.IsTypeOf("LGE_PEDIDO"));

            #region [OperacoesPedidoItem]

            #region [PedidoExcluido]

            foreach (var pedidoExcluido in listaExclusoesPedido)
            {
                LGE_PEDIDO pedido = pedidoExcluido.Entity as LGE_PEDIDO;
                List<LGE_PEDIDO_ITEM> listaItens = new RepositorioPedido(operacionalContext).GetItensPedido(pedido.ID_LGE_PEDIDO);

                string camposEditadosItens = "";

                foreach (LGE_PEDIDO_ITEM item in listaItens)
                {
                    var ocorrencia = new ADT_LOG_OCORRENCIA();

                    ocorrencia.DATA_HORA_LOG = DateTime.Now;
                    ocorrencia.CLASSE = "LGE_PEDIDO_ITEM";
                    ocorrencia.ID_DOCUMENTO = item.ID_LGE_PEDIDO_ITEM;
                    ocorrencia.ID_DOCUMENTO_TIPO = pedido.ID_DOCUMENTO_TIPO;
                    ocorrencia.ID_LOG_OCORRENCIA = sequencialId;
                    ocorrencia.NUMERO_DOCUMENTO = pedido.NUMERO_PEDIDO;
                    ocorrencia.ID_FILIAL_PFJ = pedido.ID_FILIAL_PEDIDO;
                    ocorrencia.ID_GPECON = pedido.ID_GPECON;
                    ocorrencia.ID_LINX = pedido.ID_LINX;
                    ocorrencia.LX_TIPO_OCORRENCIA = Convert.ToInt32(Domains.LX_TIPO_OCORRENCIA.ExclusaoInativacao.Value);

                    ocorrencia.ID_USUARIO = LinxOperacional.SecurityHelper.GetCurrentUserId().GetValueOrDefault();

                    if (ocorrencia.ID_USUARIO == null)
                        throw new Exception("Não foi possível gravar o log da operação pois o usuário não pode ser encontrado.");

                    camposEditadosItens = " Item Removido \n";

                    CreateLogOcurrenceItem(ocorrencia, camposEditadosItens, operacionalContext);

                    sequencialId -= 1;
                }
            }

            #endregion

            #region [Log dos Status da Capa do Pedido]
            if (listaOperacoesPedido.Count() > 0)
            {
                List<LGE_PEDIDO_STATUS> listaStatus = operacionalContext.LGE_PEDIDO_STATUS.ToList();

                foreach (var pedidoEntity in listaOperacoesPedido)
                {
                    var pedido = pedidoEntity.Entity as LGE_PEDIDO;


                    if (pedidoEntity.State == EntityState.Modified)
                    {
                        string campoStatusEditado = "";

                        var originalValues = poc.ObjectStateManager.GetObjectStateEntry(pedidoEntity.Entity).OriginalValues;
                        var originalStatus = (int?)originalValues["ID_PEDIDO_STATUS"];

                        //pedido.ID_PEDIDO_STATUS_ANTERIOR = originalStatus;

                        context.Entry(pedido).CurrentValues["ID_PEDIDO_STATUS_ANTERIOR"] = originalStatus;

                        var camposEditados = poc.ObjectStateManager.GetObjectStateEntry(pedidoEntity.Entity).GetModifiedProperties();

                        var camposStatusLog = camposEditados.Intersect(new string[1] { "ID_PEDIDO_STATUS" }).ToList();
                        if (camposStatusLog.Count() > 0)
                            campoStatusEditado = CreateEditFields(camposStatusLog, pedidoEntity, listaStatus);

                        if (campoStatusEditado != "")
                        {
                            var ocorrencia = new ADT_LOG_OCORRENCIA();

                            ocorrencia.DATA_HORA_LOG = DateTime.Now;
                            ocorrencia.CLASSE = "LGE_PEDIDO";
                            ocorrencia.ID_DOCUMENTO = pedido.ID_LGE_PEDIDO;
                            ocorrencia.ID_DOCUMENTO_TIPO = pedido.ID_DOCUMENTO_TIPO;
                            ocorrencia.ID_LOG_OCORRENCIA = sequencialId;
                            ocorrencia.NUMERO_DOCUMENTO = pedido.NUMERO_PEDIDO;
                            ocorrencia.ID_FILIAL_PFJ = pedido.ID_FILIAL_PEDIDO;
                            ocorrencia.ID_GPECON = pedido.ID_GPECON;
                            ocorrencia.ID_LINX = pedido.ID_LINX;
                            ocorrencia.LX_TIPO_OCORRENCIA = Convert.ToInt32(Domains.LX_TIPO_OCORRENCIA.Manutencao.Value);
                            ocorrencia.ID_USUARIO = LinxOperacional.SecurityHelper.GetCurrentUserId().GetValueOrDefault();

                            if (ocorrencia.ID_USUARIO == null)
                                throw new Exception("Não foi possível gravar o log da operação pois o usuário não pode ser encontrado.");


                            CreateLogOcurrenceItem(ocorrencia, campoStatusEditado, operacionalContext, "STATUS");

                            sequencialId -= 1;
                        }


                    }

                }


            }
            #endregion

            if (listaOperacoesPedidoItem.Count() > 0)
            {
                List<LGE_PEDIDO_STATUS> listaStatus = operacionalContext.LGE_PEDIDO_STATUS.ToList();

                foreach (var itemPedidoEntity in listaOperacoesPedidoItem)
                {
                    LGE_PEDIDO_ITEM itemPedido = itemPedidoEntity.Entity as LGE_PEDIDO_ITEM;

                    if (itemPedidoEntity.State == EntityState.Modified)
                    {
                        var originalValues = poc.ObjectStateManager.GetObjectStateEntry(itemPedidoEntity.Entity).OriginalValues;
                        var originalStatus = (int?)originalValues["ID_PEDIDO_STATUS"];

                        //itemPedido.ID_PEDIDO_STATUS_ANTERIOR = originalStatus;
                        context.Entry(itemPedido).CurrentValues["ID_PEDIDO_STATUS_ANTERIOR"] = originalStatus;
                    }


                    #region [Pedido Adicionado]

                    if (itemPedidoEntity.State == EntityState.Added && itemPedido.ID_LGE_PEDIDO <= 0)
                    {
                        if (!listaItensAdicionados.Contains(itemPedido))
                            listaItensAdicionados.Add(itemPedido);

                        //KeyValuePair<int, int> pedidoPair;
                        //if (itemPedido.LGE_PEDIDO == null)
                        //{
                        //    itemPedidoEntity..
                        //}
                        //else
                        //{
                        //    pedidoPair  = new KeyValuePair<int, int>(itemPedido.LGE_PEDIDO.NUMERO_PEDIDO, itemPedido.LGE_PEDIDO.ID_FILIAL_PEDIDO);
                        //}

                        //if (!listaNumeroPedidoFilial.Contains(pedidoPair))
                        //    listaNumeroPedidoFilial.Add(pedidoPair);
                    }

                    #endregion

                    else
                    {
                        RepositorioPedido repositorioPedido = new RepositorioPedido(operacionalContext);
                        LGE_PEDIDO pedido = repositorioPedido.GetPedido(Convert.ToInt32(itemPedidoEntity.Entity.GetPropertyValue("ID_LGE_PEDIDO")));

                        if (itemPedidoEntity.State == EntityState.Added)
                        {
                            pedidoItensExistentes.IdPedido = pedido.ID_LGE_PEDIDO;
                            pedidoItensExistentes.IdsItens = new List<int>();

                            foreach (LGE_PEDIDO_ITEM itemExistentePedido in repositorioPedido.GetItensPedido(pedido.ID_LGE_PEDIDO))
                            {
                                pedidoItensExistentes.IdsItens.Add(itemExistentePedido.ID_LGE_PEDIDO_ITEM);
                            }
                        }

                        else
                        {
                            string camposEditadosItens = "";

                            var ocorrencia = new ADT_LOG_OCORRENCIA();

                            ocorrencia.DATA_HORA_LOG = DateTime.Now;
                            ocorrencia.CLASSE = "LGE_PEDIDO_ITEM";
                            ocorrencia.ID_DOCUMENTO = itemPedido.ID_LGE_PEDIDO_ITEM;
                            ocorrencia.ID_DOCUMENTO_TIPO = pedido.ID_DOCUMENTO_TIPO;
                            ocorrencia.ID_LOG_OCORRENCIA = sequencialId;
                            ocorrencia.NUMERO_DOCUMENTO = pedido.NUMERO_PEDIDO;
                            ocorrencia.ID_FILIAL_PFJ = pedido.ID_FILIAL_PEDIDO;
                            ocorrencia.ID_GPECON = pedido.ID_GPECON;
                            ocorrencia.ID_LINX = pedido.ID_LINX;

                            string camposEditadosSimples = "";
                            if (itemPedidoEntity.State == EntityState.Modified)
                            {
                                ocorrencia.LX_TIPO_OCORRENCIA = Convert.ToInt32(Domains.LX_TIPO_OCORRENCIA.Manutencao.Value);

                                var CamposEditadosItem = poc.ObjectStateManager.GetObjectStateEntry(itemPedidoEntity.Entity).GetModifiedProperties();

                                camposEditadosItens = "";
                                camposEditadosItens = CreateEditFields(CamposEditadosItem, itemPedidoEntity, listaStatus);

                                List<string> camposLog = new List<string>();
                                camposLog.Add("QTDE_ENTREGAR");
                                camposLog.Add("QTDE_ALTERAR");
                                camposLog.Add("QTDE_CANCELADA");
                                camposLog.Add("ID_PEDIDO_STATUS");
                                camposLog = CamposEditadosItem.Intersect(camposLog).ToList();
                                if (camposLog.Count() > 0)
                                    camposEditadosSimples = CreateEditFields(camposLog, itemPedidoEntity, listaStatus);
                            }

                            else if (itemPedidoEntity.State == EntityState.Deleted)
                            {
                                ocorrencia.LX_TIPO_OCORRENCIA = Convert.ToInt32(Domains.LX_TIPO_OCORRENCIA.ExclusaoInativacao.Value);
                                camposEditadosItens = " Item Removido \n";
                            }


                            //#warning Remover o Guid Fixo para testes!!
                            //ocorrencia.UID_USUARIO = new Guid("BD59DAE1-B916-4A83-8453-DFFE426A699B");
                            ocorrencia.ID_USUARIO = LinxOperacional.SecurityHelper.GetCurrentUserId().GetValueOrDefault();

                            if (ocorrencia.ID_USUARIO == null)
                                throw new Exception("Não foi possível gravar o log da operação pois o usuário não pode ser encontrado.");


                            if (camposEditadosSimples != "")
                            {
                                ADT_LOG_OCORRENCIA ocorrenciaLogStatus = new ADT_LOG_OCORRENCIA();
                                ocorrenciaLogStatus.CopyInstanceFrom(ocorrencia);
                                sequencialId -= 1;
                                ocorrenciaLogStatus.ID_LOG_OCORRENCIA = sequencialId;
                                CreateLogOcurrenceItem(ocorrenciaLogStatus, camposEditadosSimples, operacionalContext, "STATUS");
                            }

                            //Só loga os campos acima
                            //CreateLogOcurrenceItem(ocorrencia, camposEditadosItens, operacionalContext);

                            sequencialId -= 1;
                        }
                    }
                }
            }
            #endregion



            #endregion
            #endregion Alterações referente a pedido

            //new AtualizacaoSaldoEstoque(context);

            #region [PRD_ARTIGO_ATRIBUTO]


            LinxOperacional operacionalContext2 = new LinxOperacional();

            var listaArtigoAtributoTrackerAux = context.ChangeTracker.Entries().Where(c => (c.State == EntityState.Added || c.State == EntityState.Modified) && c.Entity.IsTypeOf("PRD_ARTIGO_ATRIBUTO"));
            var listaArtigoAtributoTracker = context.ChangeTracker.Entries().Where(c => (c.State == EntityState.Added || c.State == EntityState.Modified || c.State == EntityState.Deleted || c.State == EntityState.Unchanged) && c.Entity.IsTypeOf("PRD_ARTIGO_ATRIBUTO"));
            var listaArtigoAtributoTrackerDeleted = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Deleted && c.Entity.IsTypeOf("PRD_ARTIGO_ATRIBUTO"));

            if ((listaArtigoAtributoTrackerAux != null && listaArtigoAtributoTrackerAux.Count() > 0) ||
                (listaArtigoAtributoTrackerDeleted != null && listaArtigoAtributoTrackerDeleted.Count() > 0))
            {


                int paramClasseMercadologico;
                bool geraMercadologico;

                try
                {
                    paramClasseMercadologico = Linx.Business.Tools.LinxParameters.GetParameter<int>("CLASSE_PRODUTO_PADRAO", null, (int)Aplicativo.Operacional);
                    geraMercadologico = Linx.Business.Tools.LinxParameters.GetParameter<bool>("GERA_MERCADOLOGICO_ARTIGO", null, (int)Aplicativo.Operacional);
                }
                catch
                {
                    paramClasseMercadologico = 0;
                    geraMercadologico = false;
                }

                if (!paramClasseMercadologico.IsNullOrEmpty() && paramClasseMercadologico > 0 && !geraMercadologico.IsNullOrEmpty() && geraMercadologico == true)
                {


                    List<PRD_ARTIGO_ATRIBUTO> listaArtigoAtributo = new List<PRD_ARTIGO_ATRIBUTO>();
                    List<PRD_ARTIGO_ATRIBUTO> listaArtigoAtributoDeleted = new List<PRD_ARTIGO_ATRIBUTO>();

                    listaArtigoAtributoTracker.Foreach(r =>
                    {
                        var artigoAtributo = r.Entity as PRD_ARTIGO_ATRIBUTO;
                        listaArtigoAtributo.Add(artigoAtributo);
                    });

                    listaArtigoAtributoTrackerDeleted.Foreach(r =>
                    {
                        var artigoAtributo = r.Entity as PRD_ARTIGO_ATRIBUTO;
                        listaArtigoAtributoDeleted.Add(artigoAtributo);
                    });


                    listaArtigoAtributo = listaArtigoAtributo.OrderBy(r => r.ID_ARTIGO).ToList();

                    var listaArtigo = (from artigo in listaArtigoAtributo
                                       group new { artigo2 = artigo } by new { artigo.ID_ARTIGO } into artigoAux
                                       select new
                                       {
                                           idArtigo = artigoAux.Key.ID_ARTIGO
                                       });


                    var listaMercadologicoUpdate = new List<PRD_MERCADOLOGICO>();
                    var listaArtigoUpdate = new List<PRD_ARTIGO>();

                    listaArtigo.Foreach(r =>
                    {

                        List<PRD_ARTIGO_ATRIBUTO> listaArtigoAtributoAux = new List<PRD_ARTIGO_ATRIBUTO>();
                        List<PRD_ARTIGO_ATRIBUTO> listaArtigoAtributoAux2 = operacionalContext.PRD_ARTIGO_ATRIBUTO.Where(atributoAux => atributoAux.ID_ARTIGO == r.idArtigo).ToList();

                        if (listaArtigoAtributoAux2 != null && listaArtigoAtributoAux2.Count() > 0)
                        {

                            listaArtigoAtributoAux2.ForEach(y =>
                            {
                                var listaAtributoNew = listaArtigoAtributo.Where(lista => lista.ID_ARTIGO_ATRIBUTO == y.ID_ARTIGO_ATRIBUTO).ToList().FirstOrDefault() as PRD_ARTIGO_ATRIBUTO;

                                var listaAtributoDeleted = listaArtigoAtributoDeleted.Where(lista => lista.ID_ARTIGO_ATRIBUTO == y.ID_ARTIGO_ATRIBUTO).ToList().FirstOrDefault() as PRD_ARTIGO_ATRIBUTO;


                                if (listaAtributoDeleted.IsNullOrEmpty())
                                {

                                    if (!listaAtributoNew.IsNullOrEmpty())
                                    {
                                        PRD_ARTIGO_ATRIBUTO atributoAdd = new PRD_ARTIGO_ATRIBUTO();
                                        atributoAdd.ID_ARTIGO = y.ID_ARTIGO;
                                        atributoAdd.ID_ARTIGO_ATRIBUTO = y.ID_ARTIGO_ATRIBUTO;
                                        atributoAdd.ID_ATRIBUTO = listaAtributoNew.ID_ATRIBUTO;
                                        atributoAdd.ID_ATRIBUTO_DEFINICAO = y.ID_ATRIBUTO_DEFINICAO;
                                        atributoAdd.ID_LINX = y.ID_LINX;
                                        atributoAdd.INDICA_ATRIBUTO_SISTEMA = y.INDICA_ATRIBUTO_SISTEMA;

                                        listaArtigoAtributoAux.Add(atributoAdd);
                                    }
                                    else
                                    {
                                        PRD_ARTIGO_ATRIBUTO atributoUpdate = y as PRD_ARTIGO_ATRIBUTO;
                                        listaArtigoAtributoAux.Add(atributoUpdate);
                                    }
                                }
                            });
                        }

                        listaArtigoAtributo.ForEach(f =>
                        {
                            var listaAtributoNew = listaArtigoAtributoAux.Where(x => x.ID_ARTIGO_ATRIBUTO == f.ID_ARTIGO_ATRIBUTO).ToList();

                            var listaAtributoDeleted = listaArtigoAtributoDeleted.Where(lista => lista.ID_ARTIGO_ATRIBUTO == f.ID_ARTIGO_ATRIBUTO).ToList().FirstOrDefault() as PRD_ARTIGO_ATRIBUTO;

                            if (listaAtributoDeleted.IsNullOrEmpty())
                            {

                                if (listaAtributoNew.IsNullOrEmpty() || listaAtributoNew.Count() == 0)
                                {

                                    PRD_ARTIGO_ATRIBUTO atributoAdd = new PRD_ARTIGO_ATRIBUTO();
                                    atributoAdd.ID_ARTIGO = f.ID_ARTIGO;
                                    atributoAdd.ID_ARTIGO_ATRIBUTO = f.ID_ARTIGO_ATRIBUTO;
                                    atributoAdd.ID_ATRIBUTO = f.ID_ATRIBUTO;
                                    atributoAdd.ID_ATRIBUTO_DEFINICAO = f.ID_ATRIBUTO_DEFINICAO;
                                    atributoAdd.ID_LINX = f.ID_LINX;
                                    atributoAdd.INDICA_ATRIBUTO_SISTEMA = f.INDICA_ATRIBUTO_SISTEMA;

                                    listaArtigoAtributoAux.Add(atributoAdd);

                                }
                            }
                        });

                        listaArtigoAtributo = listaArtigoAtributoAux;

                        var listaArtigoNivel = new List<PRD_MERCADOLOGICO_NIVEL>();


                        String codMercadologico = "";
                        String descMercadologico = "";
                        Int32 idAtributo01 = 0;
                        Int32? idAtributo02 = 0;
                        Int32? idAtributo03 = 0;
                        Int32? idAtributo04 = 0;
                        Int32? idAtributo05 = 0;
                        Int32? idAtributo06 = 0;
                        Int32? idAtributo07 = 0;
                        Int32? idAtributo08 = 0;
                        Int32? idAtributo09 = 0;
                        Int32? idAtributo10 = 0;


                        listaArtigoAtributo.Where(x => x.ID_ARTIGO == r.idArtigo).Foreach(f =>
                        {
                            PRD_MERCADOLOGICO_NIVEL artigoNivel = operacionalContext.PRD_MERCADOLOGICO_NIVEL.Where(nivel => nivel.ID_PRD_GRUPO_MERCADOLOGICO == paramClasseMercadologico && nivel.ID_ATRIBUTO_DEFINICAO == f.ID_ATRIBUTO_DEFINICAO).FirstOrDefault();

                            if (!artigoNivel.IsNullOrEmpty())
                                listaArtigoNivel.Add(artigoNivel);

                        });

                        int cnt = 0;

                        List<PRD_ARTIGO_ATRIBUTO> listaAtributoAux = new List<PRD_ARTIGO_ATRIBUTO>();

                        listaArtigoNivel.OrderBy(ordemNivel => ordemNivel.NIVEL_MERCADOLOGICO).Foreach(nivelMercadologico =>
                        {

                            List<PRD_ARTIGO_ATRIBUTO> listaAtributo = listaArtigoAtributo.Where(artigo => artigo.ID_ARTIGO == r.idArtigo && artigo.ID_ATRIBUTO_DEFINICAO == nivelMercadologico.ID_ATRIBUTO_DEFINICAO).OrderBy(ordemAtributo => ordemAtributo.ID_ARTIGO_ATRIBUTO).ToList();

                            listaAtributo.ForEach(i =>
                            {

                                if (i.INDICA_ATRIBUTO_SISTEMA)
                                {

                                    TCS_ATRIBUTO atributo = operacionalContext.TCS_ATRIBUTO.Where(iAtributo => iAtributo.ID_ATRIBUTO == i.ID_ATRIBUTO).FirstOrDefault();

                                    if (!atributo.IsNullOrEmpty())
                                    {

                                        var atributoVerificado = listaAtributoAux.Where(z => z.ID_ATRIBUTO_DEFINICAO == atributo.ID_ATRIBUTO_DEFINICAO && z.ID_ATRIBUTO == atributo.ID_ATRIBUTO).ToList();

                                        // Verificando se atributo já não está presente na lista de atributos que servem como base para geração do mercadológico.
                                        if (atributoVerificado.IsNullOrEmpty() || atributoVerificado.Count() == 0)
                                        {

                                            listaAtributoAux.Add(i);

                                            codMercadologico += atributo.COD_ATRIBUTO.Trim() + "_";
                                            descMercadologico += atributo.DESC_ATRIBUTO.Trim() + "_";

                                            cnt++;

                                            idAtributo01 = cnt == 1 ? atributo.ID_ATRIBUTO : idAtributo01;
                                            idAtributo02 = cnt == 2 ? atributo.ID_ATRIBUTO : idAtributo02;
                                            idAtributo03 = cnt == 3 ? atributo.ID_ATRIBUTO : idAtributo03;
                                            idAtributo04 = cnt == 4 ? atributo.ID_ATRIBUTO : idAtributo04;
                                            idAtributo05 = cnt == 5 ? atributo.ID_ATRIBUTO : idAtributo05;
                                            idAtributo06 = cnt == 6 ? atributo.ID_ATRIBUTO : idAtributo06;
                                            idAtributo07 = cnt == 7 ? atributo.ID_ATRIBUTO : idAtributo07;
                                            idAtributo08 = cnt == 8 ? atributo.ID_ATRIBUTO : idAtributo08;
                                            idAtributo09 = cnt == 9 ? atributo.ID_ATRIBUTO : idAtributo09;
                                            idAtributo10 = cnt == 10 ? atributo.ID_ATRIBUTO : idAtributo10;
                                        }
                                    }
                                }
                            });

                        });

                        if (!codMercadologico.IsNullOrEmpty())
                            codMercadologico = codMercadologico.Substring(0, codMercadologico.Length - 1);

                        if (!descMercadologico.IsNullOrEmpty())
                            descMercadologico = descMercadologico.Substring(0, descMercadologico.Length - 1);


                        if (!codMercadologico.IsNullOrEmpty())
                        {
                            PRD_MERCADOLOGICO mercadologico = operacionalContext.PRD_MERCADOLOGICO.Where(x => x.COD_PRD_MERCADOLOGICO == codMercadologico).FirstOrDefault();

                            PRD_ARTIGO artigo = null;
                            if (r.idArtigo > 0)
                                artigo = operacionalContext.PRD_ARTIGO.Where(x => x.ID_ARTIGO == r.idArtigo).FirstOrDefault();
                            else
                            {
                                var artigos = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added && c.Entity.IsTypeOf("PRD_ARTIGO")).ToList();
                                if (artigos.Count > 0)
                                    artigo = artigos.FirstOrDefault(f => (f.Entity as PRD_ARTIGO).ID_ARTIGO == r.idArtigo).Entity as PRD_ARTIGO;
                            }


                            if (mercadologico.IsNullOrEmpty())
                            {
                                var mercadologicoUpdate = new PRD_MERCADOLOGICO();

                                mercadologicoUpdate.COD_PRD_MERCADOLOGICO = codMercadologico;
                                mercadologicoUpdate.DESC_PRD_MERCADOLOGICO = descMercadologico;
                                mercadologicoUpdate.ID_LINX = operacionalContext.IdLinx;
                                mercadologicoUpdate.ID_PRD_GRUPO_MERCADOLOGICO = paramClasseMercadologico;
                                mercadologicoUpdate.ID_ATRIBUTO_01 = idAtributo01;
                                mercadologicoUpdate.ID_ATRIBUTO_02 = idAtributo02 > 0 ? idAtributo02 : null;
                                mercadologicoUpdate.ID_ATRIBUTO_03 = idAtributo03 > 0 ? idAtributo03 : null;
                                mercadologicoUpdate.ID_ATRIBUTO_04 = idAtributo04 > 0 ? idAtributo04 : null;
                                mercadologicoUpdate.ID_ATRIBUTO_05 = idAtributo05 > 0 ? idAtributo05 : null;
                                mercadologicoUpdate.ID_ATRIBUTO_06 = idAtributo06 > 0 ? idAtributo06 : null;
                                mercadologicoUpdate.ID_ATRIBUTO_07 = idAtributo07 > 0 ? idAtributo07 : null;
                                mercadologicoUpdate.ID_ATRIBUTO_08 = idAtributo08 > 0 ? idAtributo08 : null;
                                mercadologicoUpdate.ID_ATRIBUTO_09 = idAtributo09 > 0 ? idAtributo09 : null;
                                mercadologicoUpdate.ID_ATRIBUTO_10 = idAtributo10 > 0 ? idAtributo10 : null;
                                mercadologicoUpdate.INATIVO = false;
                                mercadologicoUpdate.DATA_ATUALIZACAO = System.DateTime.Now;
                                mercadologicoUpdate.DATA_CADASTRO = System.DateTime.Now;

                                operacionalContext2.PRD_MERCADOLOGICO.Add(mercadologicoUpdate);
                                operacionalContext2.SaveChanges();

                                if (!artigo.IsNullOrEmpty() && mercadologicoUpdate.ID_PRD_MERCADOLOGICO > 0)
                                {
                                    artigo.ID_PRD_MERCADOLOGICO = mercadologicoUpdate.ID_PRD_MERCADOLOGICO;

                                    if (artigo.ID_ARTIGO > 0)
                                        listaArtigoUpdate.Add(artigo);
                                }

                            }
                            else
                            {

                                if (!artigo.IsNullOrEmpty() && mercadologico.ID_PRD_MERCADOLOGICO > 0)
                                {
                                    artigo.ID_PRD_MERCADOLOGICO = mercadologico.ID_PRD_MERCADOLOGICO;

                                    if (artigo.ID_ARTIGO > 0)
                                        listaArtigoUpdate.Add(artigo);
                                }

                            }

                        }


                    });

                    if (!listaArtigoUpdate.IsNullOrEmpty() && listaArtigoUpdate.Count() > 0)
                    {
                        listaArtigoUpdate.ForEach(r =>
                        {
                            operacionalContext.Entry(r).State = EntityState.Modified;
                        });

                        operacionalContext.ChangeTracker.DetectChanges();

                    }

                }
            }


            #endregion


            #region [Other]

            var excluidas = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Deleted && c.IsTypeOf("PRD_REMARCACAO"));
            foreach (var e in excluidas)
            {
                var remarcacao = e.Entity as PRD_REMARCACAO;
                if (remarcacao.LX_STATUS_PROCESSO != 2)
                {
                    throw new Exception("Não é possível excluir uma remarcação processada!");
                }
            }

            var listaEntidadesAdicionadas = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added && c.IsTypeOf("PRD_CLASSE_VARIANTE_NIVEL"));
            var listaEntidadesAlteradas = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Modified && c.IsTypeOf("PRD_CLASSE_VARIANTE_NIVEL"));
            //var listaEntidadesDeletadas = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Deleted && c.IsTypeOf("PRD_CLASSE_VARIANTE_NIVEL"));

            var db = context as LinxOperacional;

            foreach (var adicionada in listaEntidadesAdicionadas)
            {
                var nivel = adicionada.Entity as PRD_CLASSE_VARIANTE_NIVEL;

                var produto = db.PRD_CLASSE_VARIANTE_NIVEL.Where(x => x.ID_CLASSE_VARIANTE == nivel.ID_CLASSE_VARIANTE).Where(x => x.NIVEL_CLASSE_VARIANTE == nivel.NIVEL_CLASSE_VARIANTE).FirstOrDefault();

                if (produto != null)
                {
                    throw new Exception("Não é possível inserir um novo nível de classe variante, se este nível já existir na base");
                }
            }

            foreach (var editadas in listaEntidadesAlteradas)
            {
                var nivel = editadas.Entity as PRD_CLASSE_VARIANTE_NIVEL;

                var produto = db.PRD_CLASSE_VARIANTE_NIVEL.Where(x => x.ID_CLASSE_VARIANTE == nivel.ID_CLASSE_VARIANTE).Where(x => x.NIVEL_CLASSE_VARIANTE == nivel.NIVEL_CLASSE_VARIANTE).FirstOrDefault();

                if (produto != null)
                {
                    //if ((nivel.ID_ATRIBUTO_DEFINICAO == produto.ID_ATRIBUTO_DEFINICAO) && (nivel.INDICA_VARIANTE_HORIZONTAL == produto.INDICA_VARIANTE_HORIZONTAL) && (nivel.INDICA_ITEM_FISCAL_VARIANTE == produto.INDICA_ITEM_FISCAL_VARIANTE) && (nivel.INDICA_VARIANTE_SOBMEDIDA == produto.INDICA_VARIANTE_SOBMEDIDA))
                    //{
                    //    throw new Exception("Não é possível alterar um novo nível de classe variante, se este nível já existir na base");
                    //}

                    if (produto != null)
                    {
                        throw new Exception("Não é possível alterar um novo nível de classe variante, se este nível já existir na base");
                    }
                }
            }

            #endregion
            
            return true;
        }
        
        public static void AfterSaveChanges(DbContext context)
        {
          

            if (listaLojasAdicionadas.Count > 0)
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 600;
                List<string> idslojas = new List<string>();
                listaLojasAdicionadas.Select(f => f.ID_LOJA).Foreach(lj => idslojas.Add(lj.ToString()));
                var lojas = "|" + String.Join("|", idslojas.ToArray()) + "|";
                try
                {
                    context.Database.SqlQuery<int>("EXEC LX_PRD.PRD_CARREGA_TAB_PRECO {0},{1};Select 0;", null, lojas).ToList();
                }
                catch
                {
                    
                }
            }

            #region Alterações no Pedido
            int sequencialId = 0;

            var listaNumeroPedidoFilial = new List<KeyValuePair<int, int>>();

            listaItensAdicionados.ForEach(r =>
            {

                KeyValuePair<int, int> pedidoPair;
                pedidoPair = new KeyValuePair<int, int>(r.LGE_PEDIDO.NUMERO_PEDIDO, r.LGE_PEDIDO.ID_FILIAL_PEDIDO);

                if (!listaNumeroPedidoFilial.Contains(pedidoPair))
                    listaNumeroPedidoFilial.Add(pedidoPair);

            });


            if (listaNumeroPedidoFilial.Count() > 0)
            {
                using (LinxOperacional operacionalContext = context as LinxOperacional)
                {
                    foreach (var numfilal in listaNumeroPedidoFilial)
                    {
                        RepositorioPedido repositorioPedido = new RepositorioPedido(operacionalContext);
                        LGE_PEDIDO pedido = repositorioPedido.GetPedidoByNumberBranch(numfilal.Key, numfilal.Value);

                        string camposEditadosItens = "";

                        foreach (var itemPedido in pedido.LGE_PEDIDO_ITEM_LISTA)
                        {
                            var ocorrencia = new ADT_LOG_OCORRENCIA();
                            ocorrencia.ID_LOG_OCORRENCIA = sequencialId;
                            ocorrencia.DATA_HORA_LOG = DateTime.Now;
                            ocorrencia.CLASSE = "LGE_PEDIDO_ITEM";
                            ocorrencia.ID_DOCUMENTO = itemPedido.ID_LGE_PEDIDO_ITEM;
                            ocorrencia.ID_DOCUMENTO_TIPO = pedido.ID_DOCUMENTO_TIPO;
                            ocorrencia.NUMERO_DOCUMENTO = pedido.NUMERO_PEDIDO;
                            ocorrencia.ID_FILIAL_PFJ = pedido.ID_FILIAL_PEDIDO;
                            ocorrencia.ID_GPECON = pedido.ID_GPECON;
                            ocorrencia.ID_LINX = pedido.ID_LINX;
                            ocorrencia.ID_USUARIO = LinxOperacional.SecurityHelper.GetCurrentUserId().GetValueOrDefault();
                            if (ocorrencia.ID_USUARIO == null)
                                throw new Exception("Não foi possível gravar o log da operação pois o usuário não pode ser encontrado.");

                            ocorrencia.LX_TIPO_OCORRENCIA = Convert.ToInt32(Domains.LX_TIPO_OCORRENCIA.Inclusao.Value);
                            camposEditadosItens = " Item Adicionado \n";

                            CreateLogOcurrenceItem(ocorrencia, camposEditadosItens, operacionalContext);

                            sequencialId = sequencialId - 1;
                        }
                    }

                    operacionalContext.SaveChanges();
                }
            }

            if (pedidoItensExistentes.IdPedido != 0)
            {
                using (LinxOperacional operacionalContext = context as LinxOperacional)
                {
                    RepositorioPedido repositorioPedido = new RepositorioPedido(operacionalContext);
                    LGE_PEDIDO pedido = repositorioPedido.GetPedido(pedidoItensExistentes.IdPedido);

                    foreach (var item in pedido.LGE_PEDIDO_ITEM_LISTA.Where(p => !pedidoItensExistentes.IdsItens.Contains(p.ID_LGE_PEDIDO_ITEM)))
                    {
                        var ocorrencia = new ADT_LOG_OCORRENCIA();

                        string camposEditadosItens = "";

                        ocorrencia.DATA_HORA_LOG = DateTime.Now;
                        ocorrencia.CLASSE = "LGE_PEDIDO_ITEM";
                        ocorrencia.ID_DOCUMENTO = item.ID_LGE_PEDIDO_ITEM;
                        ocorrencia.ID_DOCUMENTO_TIPO = pedido.ID_DOCUMENTO_TIPO;
                        ocorrencia.ID_LOG_OCORRENCIA = sequencialId;
                        ocorrencia.NUMERO_DOCUMENTO = pedido.NUMERO_PEDIDO;
                        ocorrencia.ID_FILIAL_PFJ = pedido.ID_FILIAL_PEDIDO;
                        ocorrencia.ID_GPECON = pedido.ID_GPECON;
                        ocorrencia.ID_LINX = pedido.ID_LINX;
                        ocorrencia.ID_USUARIO = LinxOperacional.SecurityHelper.GetCurrentUserId().GetValueOrDefault();
                        if (ocorrencia.ID_USUARIO == null)
                            throw new Exception("Não foi possível gravar o log da operação pois o usuário não pode ser encontrado.");

                        ocorrencia.LX_TIPO_OCORRENCIA = Convert.ToInt32(Domains.LX_TIPO_OCORRENCIA.Inclusao.Value);
                        camposEditadosItens = " Item Adicionado \n";

                        CreateLogOcurrenceItem(ocorrencia, camposEditadosItens, operacionalContext);

                        sequencialId = sequencialId - 1;
                    }

                    operacionalContext.SaveChanges();
                }
            }
            #endregion Alterações no Pedido
        }

        public static void CreateLogOcurrenceItem(ADT_LOG_OCORRENCIA ocurrence, string camposEditados, LinxOperacional context, string detalhe = null)
        {
            context.ADT_LOG_OCORRENCIA.Add(ocurrence);

            var ocorrenciaDetalhe = new ADT_LOG_OCORRENCIA_DETALHE();
            ocorrenciaDetalhe.ID_LOG_OCORRENCIA = ocurrence.ID_LOG_OCORRENCIA;
            ocorrenciaDetalhe.LOG_DETALHE = detalhe;
            ocorrenciaDetalhe.LOG_DESCRICAO = camposEditados;
            ocorrenciaDetalhe.ID_LINX = ocurrence.ID_LINX;

            context.ADT_LOG_OCORRENCIA_DETALHE.Add(ocorrenciaDetalhe);
        }

        public static string CreateEditFields(IEnumerable<string> valoresEditados, DbEntityEntry entity, List<LGE_PEDIDO_STATUS> listaStatus)
        {
            string camposEditados = "";
            foreach (var campoEditado in valoresEditados)
            {
                var vAnterior = entity.OriginalValues.GetValue<object>(campoEditado);
                var vNovo = entity.CurrentValues.GetValue<object>(campoEditado);

                string valorAnterior = (vAnterior == null ? "null" : vAnterior.ToString());
                string valorNovo = (vNovo == null ? "null" : vNovo.ToString());
                string campoTratado = campoEditado;

                switch (campoEditado)
                {
                    case "QTDE_ENTREGAR":
                        {
                            campoTratado = "Qtd. Entregar";                            
                            break;
                        }
                    case "QTDE_ALTERAR":
                        {
                            campoTratado = "Qtd. Alterar";
                            break;
                        }
                    case "ID_PEDIDO_STATUS":
                        {
                            if (entity.Entity.GetType().Name.ToUpper() == "LGE_PEDIDO")
                                campoTratado = "Status do Pedido ";
                            else
                                campoTratado = "Status do Item";

                            if (!vAnterior.IsNullOrEmpty())
                            {
                                var status = listaStatus.FirstOrDefault(f => f.ID_PEDIDO_STATUS == (int)vAnterior);
                                if (status != null)
                                    valorAnterior = status.DESC_STATUS;
                            }
                            if (!vNovo.IsNullOrEmpty())
                            {
                                var status = listaStatus.FirstOrDefault(f => f.ID_PEDIDO_STATUS == (int)vNovo);
                                if (status != null)
                                    valorNovo = status.DESC_STATUS;
                            }
                            break;
                        }
                }

                var camposQtd = new string[] { "QTDE_ALTERAR", "QTDE_ENTREGAR", "QTDE_PEDIDO", "QTDE_CANCELADA" };

                if (!valorAnterior.IsNullOrEmpty() || !vNovo.IsNullOrEmpty())
                {
                    camposEditados += "-" + campoTratado + ": de ";

                    if (camposQtd.Contains(campoEditado))
                    {                        
                        camposEditados += (!String.IsNullOrEmpty(valorAnterior) ? String.Format("{0:0.00}", Convert.ToDecimal(valorAnterior)) : "0.00") + " para ";
                        camposEditados += (!String.IsNullOrEmpty(valorNovo) ? String.Format("{0:0.00}", Convert.ToDecimal(valorNovo)) : "0.00") + ".\n ";

                    }
                    else
                    {
                        camposEditados += valorAnterior + " para ";
                        camposEditados += valorNovo + ".\n ";

                    }
                }

            }
            if (!camposEditados.IsNullOrEmpty())
                camposEditados = " Campo(s) Editado(s): \n " + camposEditados;

            return camposEditados;
        }


    }
}