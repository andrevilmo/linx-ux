using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Framework.ControleSistema.BM;
using Linx.TCS0101.BO.TcsAutorizacao;

namespace Linx.TCS0101.BO.TcsObjeto
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsObjetoDomainService
    {



        [Query(HasSideEffects = true)]
        public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMnt_Limpo()
        {
            IQueryable<TcsObjetoConteudoMnt> result =
                from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO
                //where entity0.Type == 1//tipo excel
                select new TcsObjetoConteudoMnt()
                 {
                     UidObjetoConteudo = entity0.UID_OBJETO_CONTEUDO,
                     ConteudoXml = null,
                     UidObjeto = entity0.TCS_OBJETO.UID_OBJETO
                 };

            return result;
        }


        [Query(HasSideEffects = true)]
        public IEnumerable<TcsObjeto> GetTcsObjetoLayout(string serializedEntitySearch)
        {
            //Local Layouts - Customer Database
            List<TcsObjeto> tcsObjeto = this.GetTcsObjetoByEntitySearch(serializedEntitySearch).ToList();

            //Linx Layouts - Autorization Database
            TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();
            List<TcsObjeto> tcsObjetoAutorizacao =
                (from result in ds.GetTcsObjetoAutorizacaoByEntitySearch(serializedEntitySearch)
                select new TcsObjeto
                {
                    ClasseNome = result.ClasseNome,
                    DescObjeto = result.DescObjeto,
                    LxTipoObjeto = result.LxTipoObjeto,
                    PathObjeto = result.PathObjeto,
                    UidObjeto = result.UidObjeto,
                    ObjetoLinx = result.ObjetoLinx,
                    TcsObjetoConteudoList =
                    (
                        from result1 in result.TcsObjetoAutorizacaoConteudoList
                        select new TcsObjetoConteudo
                        {
                            ConteudoXml = result1.ConteudoXml,
                            UidObjeto = result1.UidObjeto,
                            UidObjetoConteudo = result1.UidObjetoConteudo
                        }
                    ),
                    TcsLayoutList =
                    (
                    from result2 in result.TcsObjetoAutorizacaoLayoutList
                    select new TcsLayout
                    {
                        ConteudoXml = result2.ConteudoXml,
                        DescLayout = result2.DescLayout,
                        Detalhes = result2.Detalhes,
                        Idioma = result2.Idioma,
                        Inativo = result2.Inativo,
                        LayoutPadrao = result2.LayoutPadrao,
                        LxConteudoObjeto = result2.LxConteudoObjeto,
                        LxTipoLayout = result2.LxTipoLayout,
                        NomeUsuario = null,
                        PossuiFiltro = result2.PossuiFiltro,
                        Publico = result2.Publico,
                        UidLayout = result2.UidObjetoConteudo,
                        UidObjeto = result2.UidObjeto,
                        UidObjetoConteudo = result2.UidObjetoConteudo,
                        UidUsuario = null,
                        UltAtualizacao = result2.UltAtualizacao,
                        LayoutLinx = result2.LayoutLinx
                    }
                    )
                }).ToList();

            return
                (from result in tcsObjeto
                 select result).Union
                (from result1 in tcsObjetoAutorizacao
                 select result1);
        }

        [Query(HasSideEffects = false)]
        public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMnt_LimpoJson()
        {
            return GetTcsObjetoConteudoMnt_Limpo();
        }
    }
}
