using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using Linx.Business.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Linx.Data;
using System.Web.Http.OData;
using Linx.DataService;
using LinxTraining002.BM;

namespace LinxTraining002.BM.WebAPI.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/ModelLFW002Controller1
    public partial class ModelLFW002Controller1Controller : ODataController
    {
        private ModeloVendaCliente _context;
        public ModeloVendaCliente Context { get {  if (_context == null) { _context = new ModeloVendaCliente(); } return _context; }  }
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Cidade> GetCidade(int key)
        {
            return this.Context.Cidade.Where(e => e.ID_Cidade == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Cidade> GetCidade()
        {
            return this.Context.Cidade.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Estado> GetCidade__Estado(int key, string navigation)
        {
            var entity = this.Context.Cidade.Include(navigation).FirstOrDefault(e => e.ID_Cidade == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Estado)
                   return (new Estado[] { (Estado)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Estado>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Estado>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Loja> GetCidade__Loja(int key, string navigation)
        {
            var entity = this.Context.Cidade.Include(navigation).FirstOrDefault(e => e.ID_Cidade == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Loja)
                   return (new Loja[] { (Loja)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Loja>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Loja>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Clientes> GetClientes(Guid key)
        {
            return this.Context.Clientes.Where(e => e.ID_Clientes == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Clientes> GetClientes()
        {
            return this.Context.Clientes.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Vendas> GetClientes__Vendas(Guid key, string navigation)
        {
            var entity = this.Context.Clientes.Include(navigation).FirstOrDefault(e => e.ID_Clientes == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Vendas)
                   return (new Vendas[] { (Vendas)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Vendas>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Vendas>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Estado> GetEstado(int key)
        {
            return this.Context.Estado.Where(e => e.ID_Estado == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Estado> GetEstado()
        {
            return this.Context.Estado.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Cidade> GetEstado__Cidade(int key, string navigation)
        {
            var entity = this.Context.Estado.Include(navigation).FirstOrDefault(e => e.ID_Estado == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Cidade)
                   return (new Cidade[] { (Cidade)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Cidade>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Cidade>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Loja> GetEstado__Loja(int key, string navigation)
        {
            var entity = this.Context.Estado.Include(navigation).FirstOrDefault(e => e.ID_Estado == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Loja)
                   return (new Loja[] { (Loja)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Loja>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Loja>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<FilhaNotNull> GetFilhaNotNull(int key)
        {
            return this.Context.FilhaNotNull.Where(e => e.ID_FilhaNotNull == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<FilhaNotNull> GetFilhaNotNull()
        {
            return this.Context.FilhaNotNull.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<PaiNotNull> GetFilhaNotNull__PaiNotNull(int key, string navigation)
        {
            var entity = this.Context.FilhaNotNull.Include(navigation).FirstOrDefault(e => e.ID_FilhaNotNull == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is PaiNotNull)
                   return (new PaiNotNull[] { (PaiNotNull)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<PaiNotNull>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<PaiNotNull>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Loja> GetLoja(int key)
        {
            return this.Context.Loja.Where(e => e.ID_Loja == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Loja> GetLoja()
        {
            return this.Context.Loja.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Estado> GetLoja__Estado(int key, string navigation)
        {
            var entity = this.Context.Loja.Include(navigation).FirstOrDefault(e => e.ID_Loja == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Estado)
                   return (new Estado[] { (Estado)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Estado>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Estado>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Cidade> GetLoja__Cidade(int key, string navigation)
        {
            var entity = this.Context.Loja.Include(navigation).FirstOrDefault(e => e.ID_Loja == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Cidade)
                   return (new Cidade[] { (Cidade)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Cidade>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Cidade>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<PaiNotNull> GetPaiNotNull(int key)
        {
            return this.Context.PaiNotNull.Where(e => e.ID_PaiNotNull == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<PaiNotNull> GetPaiNotNull()
        {
            return this.Context.PaiNotNull.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<FilhaNotNull> GetPaiNotNull__FilhaNotNull(int key, string navigation)
        {
            var entity = this.Context.PaiNotNull.Include(navigation).FirstOrDefault(e => e.ID_PaiNotNull == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is FilhaNotNull)
                   return (new FilhaNotNull[] { (FilhaNotNull)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<FilhaNotNull>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<FilhaNotNull>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<TestePIVOT> GetTestePIVOT(int key)
        {
            return this.Context.TestePIVOT.Where(e => e.ID_TestePIVOT == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<TestePIVOT> GetTestePIVOT()
        {
            return this.Context.TestePIVOT.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<TiposCampos> GetTiposCampos(int key)
        {
            return this.Context.TiposCampos.Where(e => e.ID_TiposCampos == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<TiposCampos> GetTiposCampos()
        {
            return this.Context.TiposCampos.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<TiposCamposFilha> GetTiposCampos__TiposCamposFilha(int key, string navigation)
        {
            var entity = this.Context.TiposCampos.Include(navigation).FirstOrDefault(e => e.ID_TiposCampos == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is TiposCamposFilha)
                   return (new TiposCamposFilha[] { (TiposCamposFilha)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<TiposCamposFilha>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<TiposCamposFilha>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<TiposCamposFilha> GetTiposCamposFilha(int key)
        {
            return this.Context.TiposCamposFilha.Where(e => e.ID_TiposCamposFilha == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<TiposCamposFilha> GetTiposCamposFilha()
        {
            return this.Context.TiposCamposFilha.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<TiposCampos> GetTiposCamposFilha__TiposCampos(int key, string navigation)
        {
            var entity = this.Context.TiposCamposFilha.Include(navigation).FirstOrDefault(e => e.ID_TiposCamposFilha == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is TiposCampos)
                   return (new TiposCampos[] { (TiposCampos)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<TiposCampos>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<TiposCampos>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<VendaDetalhe> GetVendaDetalhe(int key)
        {
            return this.Context.VendaDetalhe.Where(e => e.ID_VendaDetalhe == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<VendaDetalhe> GetVendaDetalhe()
        {
            return this.Context.VendaDetalhe.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Vendas> GetVendaDetalhe__Vendas(int key, string navigation)
        {
            var entity = this.Context.VendaDetalhe.Include(navigation).FirstOrDefault(e => e.ID_VendaDetalhe == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Vendas)
                   return (new Vendas[] { (Vendas)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Vendas>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Vendas>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Vendas> GetVendas(int key)
        {
            return this.Context.Vendas.Where(e => e.ID_Vendas == key).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Vendas> GetVendas()
        {
            return this.Context.Vendas.AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<Clientes> GetVendas__Clientes(int key, string navigation)
        {
            var entity = this.Context.Vendas.Include(navigation).FirstOrDefault(e => e.ID_Vendas == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is Clientes)
                   return (new Clientes[] { (Clientes)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<Clientes>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<Clientes>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<VendaDetalhe> GetVendas__VendaDetalhe(int key, string navigation)
        {
            var entity = this.Context.Vendas.Include(navigation).FirstOrDefault(e => e.ID_Vendas == key);
            if (entity != null)
            {
               var navProperty = entity.GetPropertyValue(navigation);
               if (navProperty is VendaDetalhe)
                   return (new VendaDetalhe[] { (VendaDetalhe)navProperty }).AsQueryable();
               else
                   return ((IEnumerable<VendaDetalhe>)navProperty).AsQueryable();
            }
            else
               return default(IQueryable<VendaDetalhe>);
        }
    }
    
}
