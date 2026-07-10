					
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq.Expressions;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Transactions;
using System.Xml.Serialization;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using System.ComponentModel.Composition;
using Linx;
using Linx.Data;
using Linx.Tools;
using Linx.LinqExtensions.Dynamic;
using Linx.LinqExtensions.Query;
using Linx.Framework.ControleSistema.BM;

namespace Linx.Framework.BV.Usuario
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuario,TcsUsuario.TcsUsuarioPerfil,TcsUsuario.TcsUsuarioRegraModulo,TcsUsuario.TcsUsuarioRegraTransacao,TcsUsuario.TcsUsuarioRegraColuna,TcsUsuario.TcsUsuarioBandeiraRede,TcsUsuario.TcsUsuarioLayout,TcsUsuario.TcsUsuarioFilial];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[false];Entities[TCS_USUARIO:IdUsuario];SubQueryInfo[];EdmEntityName[TCS_USUARIO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuario")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Usuario.TcsUsuario")]
	public partial class TcsUsuario : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsUsuarioPerfilList != null && this.TcsUsuarioPerfilList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioPerfilList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsUsuarioRegraModuloList != null && this.TcsUsuarioRegraModuloList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioRegraModuloList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsUsuarioRegraTransacaoList != null && this.TcsUsuarioRegraTransacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioRegraTransacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsUsuarioRegraColunaList != null && this.TcsUsuarioRegraColunaList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioRegraColunaList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsUsuarioBandeiraRedeList != null && this.TcsUsuarioBandeiraRedeList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioBandeiraRedeList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsUsuarioLayoutList != null && this.TcsUsuarioLayoutList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioLayoutList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsUsuarioFilialList != null && this.TcsUsuarioFilialList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioFilialList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsUsuarioPerfilList != null)
	      {
	         foreach (var detail in this.TcsUsuarioPerfilList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioPerfilList = null;
	      }
	      if (this.TcsUsuarioRegraModuloList != null)
	      {
	         foreach (var detail in this.TcsUsuarioRegraModuloList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioRegraModuloList = null;
	      }
	      if (this.TcsUsuarioRegraTransacaoList != null)
	      {
	         foreach (var detail in this.TcsUsuarioRegraTransacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioRegraTransacaoList = null;
	      }
	      if (this.TcsUsuarioRegraColunaList != null)
	      {
	         foreach (var detail in this.TcsUsuarioRegraColunaList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioRegraColunaList = null;
	      }
	      if (this.TcsUsuarioBandeiraRedeList != null)
	      {
	         foreach (var detail in this.TcsUsuarioBandeiraRedeList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioBandeiraRedeList = null;
	      }
	      if (this.TcsUsuarioLayoutList != null)
	      {
	         foreach (var detail in this.TcsUsuarioLayoutList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioLayoutList = null;
	      }
	      if (this.TcsUsuarioFilialList != null)
	      {
	         foreach (var detail in this.TcsUsuarioFilialList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioFilialList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(UsuarioDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsUsuarioPerfil"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioPerfil");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioPerfil and all sub-details
	         if (this.TcsUsuarioPerfilList == null || this.TcsUsuarioPerfilList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioPerfilList = context.GetPagedTcsUsuarioPerfil(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioPerfilList = (from r in context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsUsuarioRegraModulo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioRegraModulo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioRegraModulo and all sub-details
	         if (this.TcsUsuarioRegraModuloList == null || this.TcsUsuarioRegraModuloList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioRegraModuloList = context.GetPagedTcsUsuarioRegraModulo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioRegraModuloList = (from r in context.GetTcsUsuarioRegraModuloByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsUsuarioRegraTransacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioRegraTransacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioRegraTransacao and all sub-details
	         if (this.TcsUsuarioRegraTransacaoList == null || this.TcsUsuarioRegraTransacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioRegraTransacaoList = context.GetPagedTcsUsuarioRegraTransacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioRegraTransacaoList = (from r in context.GetTcsUsuarioRegraTransacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsUsuarioRegraColuna"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioRegraColuna");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioRegraColuna and all sub-details
	         if (this.TcsUsuarioRegraColunaList == null || this.TcsUsuarioRegraColunaList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioRegraColunaList = context.GetPagedTcsUsuarioRegraColuna(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioRegraColunaList = (from r in context.GetTcsUsuarioRegraColunaByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsUsuarioBandeiraRede"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioBandeiraRede");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioBandeiraRede and all sub-details
	         if (this.TcsUsuarioBandeiraRedeList == null || this.TcsUsuarioBandeiraRedeList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioBandeiraRedeList = context.GetPagedTcsUsuarioBandeiraRede(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioBandeiraRedeList = (from r in context.GetTcsUsuarioBandeiraRedeByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsUsuarioLayout"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioLayout");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioLayout and all sub-details
	         if (this.TcsUsuarioLayoutList == null || this.TcsUsuarioLayoutList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioLayoutList = context.GetPagedTcsUsuarioLayout(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioLayoutList = (from r in context.GetTcsUsuarioLayoutByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsUsuarioFilial"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioFilial");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioFilial and all sub-details
	         if (this.TcsUsuarioFilialList == null || this.TcsUsuarioFilialList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioFilialList = context.GetPagedTcsUsuarioFilial(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioFilialList = (from r in context.GetTcsUsuarioFilialByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsUsuarioPerfilElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioPerfil && ((TcsUsuarioPerfil)e.Entity).TcsUsuario == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioPerfil)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioPerfilElements.Count > 0 && this.TcsUsuarioPerfilList.Count() == 0)
 	      {
 	          this.TcsUsuarioPerfilList = _TcsUsuarioPerfilElements.Select(e => (TcsUsuarioPerfil)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioPerfilElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioPerfil)detail.Entity).TcsUsuario = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuario", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioPerfilList", indexDetails.ToArray());
 	      }
 
 	      var _TcsUsuarioRegraModuloElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioRegraModulo && ((TcsUsuarioRegraModulo)e.Entity).TcsUsuario == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioRegraModulo)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioRegraModuloElements.Count > 0 && this.TcsUsuarioRegraModuloList.Count() == 0)
 	      {
 	          this.TcsUsuarioRegraModuloList = _TcsUsuarioRegraModuloElements.Select(e => (TcsUsuarioRegraModulo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioRegraModuloElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioRegraModulo)detail.Entity).TcsUsuario = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuario", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioRegraModuloList", indexDetails.ToArray());
 	      }
 
 	      var _TcsUsuarioRegraTransacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioRegraTransacao && ((TcsUsuarioRegraTransacao)e.Entity).TcsUsuario == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioRegraTransacao)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioRegraTransacaoElements.Count > 0 && this.TcsUsuarioRegraTransacaoList.Count() == 0)
 	      {
 	          this.TcsUsuarioRegraTransacaoList = _TcsUsuarioRegraTransacaoElements.Select(e => (TcsUsuarioRegraTransacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioRegraTransacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioRegraTransacao)detail.Entity).TcsUsuario = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuario", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioRegraTransacaoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsUsuarioRegraColunaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioRegraColuna && ((TcsUsuarioRegraColuna)e.Entity).TcsUsuario == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioRegraColuna)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioRegraColunaElements.Count > 0 && this.TcsUsuarioRegraColunaList.Count() == 0)
 	      {
 	          this.TcsUsuarioRegraColunaList = _TcsUsuarioRegraColunaElements.Select(e => (TcsUsuarioRegraColuna)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioRegraColunaElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioRegraColuna)detail.Entity).TcsUsuario = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuario", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioRegraColunaList", indexDetails.ToArray());
 	      }
 
 	      var _TcsUsuarioBandeiraRedeElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioBandeiraRede && ((TcsUsuarioBandeiraRede)e.Entity).TcsUsuario == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioBandeiraRede)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioBandeiraRedeElements.Count > 0 && this.TcsUsuarioBandeiraRedeList.Count() == 0)
 	      {
 	          this.TcsUsuarioBandeiraRedeList = _TcsUsuarioBandeiraRedeElements.Select(e => (TcsUsuarioBandeiraRede)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioBandeiraRedeElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioBandeiraRede)detail.Entity).TcsUsuario = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuario", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioBandeiraRedeList", indexDetails.ToArray());
 	      }
 
 	      var _TcsUsuarioLayoutElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioLayout && ((TcsUsuarioLayout)e.Entity).TcsUsuario == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioLayout)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioLayoutElements.Count > 0 && this.TcsUsuarioLayoutList.Count() == 0)
 	      {
 	          this.TcsUsuarioLayoutList = _TcsUsuarioLayoutElements.Select(e => (TcsUsuarioLayout)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioLayoutElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioLayout)detail.Entity).TcsUsuario = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuario", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioLayoutList", indexDetails.ToArray());
 	      }
 
 	      var _TcsUsuarioFilialElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioFilial && ((TcsUsuarioFilial)e.Entity).TcsUsuario == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioFilial)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioFilialElements.Count > 0 && this.TcsUsuarioFilialList.Count() == 0)
 	      {
 	          this.TcsUsuarioFilialList = _TcsUsuarioFilialElements.Select(e => (TcsUsuarioFilial)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioFilialElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioFilial)detail.Entity).TcsUsuario = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuario", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioFilialList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioCopia
	    partial void OnIdUsuarioCopiaChanging(Int64 value);
	    partial void OnIdUsuarioCopiaChanged();

	    private Int64 _IdUsuarioCopia;

	    [DataMember(Name = "IdUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de ()];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuarioCopia\" : \"Nome Usuario\", \"IdUsuarioCopia\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuarioCopia\" : true, \"IdUsuarioCopia\" : true}];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuarioCopia#true##12:0##Id Usuario#1#true##::LookUpTcsUsuario##false#true##TCS_USUARIO#Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="0")]
	    public Int64 IdUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioCopia", value);
	    	              this.OnIdUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioCopia");
	    	              this._IdUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioCopia");
	    	              this.OnIdUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro / Número", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município / UF", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuarioCopia
	    partial void OnNomeUsuarioCopiaChanging(System.String value);
	    partial void OnNomeUsuarioCopiaChanged();

	    private System.String _NomeUsuarioCopia;

	    [DataMember(Name = "NomeUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Cópia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Usuário Cópia)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuarioCopia\" : \"Nome Usuario\", \"IdUsuarioCopia\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuarioCopia\" : true, \"IdUsuarioCopia\" : true}];FilterDataKey[String.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeUsuarioCopia#false##2500##Nome Usuario#0#true##::LookUpTcsUsuario##false#true##TCS_USUARIO#Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="String.Empty")]
	    public System.String NomeUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _NomeUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuarioCopia", value);
	    	              this.OnNomeUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuarioCopia");
	    	              this._NomeUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("NomeUsuarioCopia");
	    	              this.OnNomeUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdUsuario;
	    [DataMember(Name = "TemporaryIdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario (Tmp)", Description="Temporary Key", Order = 11, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdUsuario
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuario.IsNullOrEmpty())
	    	                this._TemporaryIdUsuario = this._IdUsuario;
	    	          return this._TemporaryIdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuario != value)
	    	              this._TemporaryIdUsuario = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsUsuarioBandeiraRede> _TcsUsuarioBandeiraRedeList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioBandeiraRede", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioBandeiraRedeList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioBandeiraRede> TcsUsuarioBandeiraRedeList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioBandeiraRedeList == null)
	            	this._TcsUsuarioBandeiraRedeList = new List<TcsUsuarioBandeiraRede>();
	
	            return this._TcsUsuarioBandeiraRedeList;
	        }
	        set
	        {
	            if (this._TcsUsuarioBandeiraRedeList != value)
	            {
	                this._TcsUsuarioBandeiraRedeList = value;
	                this.RaisePropertyChanged("TcsUsuarioBandeiraRedeList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioFilial> _TcsUsuarioFilialList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioFilial", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioFilialList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioFilial> TcsUsuarioFilialList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioFilialList == null)
	            	this._TcsUsuarioFilialList = new List<TcsUsuarioFilial>();
	
	            return this._TcsUsuarioFilialList;
	        }
	        set
	        {
	            if (this._TcsUsuarioFilialList != value)
	            {
	                this._TcsUsuarioFilialList = value;
	                this.RaisePropertyChanged("TcsUsuarioFilialList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioLayout> _TcsUsuarioLayoutList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioLayout", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioLayoutList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioLayout> TcsUsuarioLayoutList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioLayoutList == null)
	            	this._TcsUsuarioLayoutList = new List<TcsUsuarioLayout>();
	
	            return this._TcsUsuarioLayoutList;
	        }
	        set
	        {
	            if (this._TcsUsuarioLayoutList != value)
	            {
	                this._TcsUsuarioLayoutList = value;
	                this.RaisePropertyChanged("TcsUsuarioLayoutList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioPerfil> _TcsUsuarioPerfilList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioPerfil", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioPerfilList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioPerfil> TcsUsuarioPerfilList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioPerfilList == null)
	            	this._TcsUsuarioPerfilList = new List<TcsUsuarioPerfil>();
	
	            return this._TcsUsuarioPerfilList;
	        }
	        set
	        {
	            if (this._TcsUsuarioPerfilList != value)
	            {
	                this._TcsUsuarioPerfilList = value;
	                this.RaisePropertyChanged("TcsUsuarioPerfilList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioRegraColuna> _TcsUsuarioRegraColunaList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioRegraColuna", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioRegraColunaList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioRegraColuna> TcsUsuarioRegraColunaList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioRegraColunaList == null)
	            	this._TcsUsuarioRegraColunaList = new List<TcsUsuarioRegraColuna>();
	
	            return this._TcsUsuarioRegraColunaList;
	        }
	        set
	        {
	            if (this._TcsUsuarioRegraColunaList != value)
	            {
	                this._TcsUsuarioRegraColunaList = value;
	                this.RaisePropertyChanged("TcsUsuarioRegraColunaList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioRegraModulo> _TcsUsuarioRegraModuloList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioRegraModulo", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioRegraModuloList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioRegraModulo> TcsUsuarioRegraModuloList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioRegraModuloList == null)
	            	this._TcsUsuarioRegraModuloList = new List<TcsUsuarioRegraModulo>();
	
	            return this._TcsUsuarioRegraModuloList;
	        }
	        set
	        {
	            if (this._TcsUsuarioRegraModuloList != value)
	            {
	                this._TcsUsuarioRegraModuloList = value;
	                this.RaisePropertyChanged("TcsUsuarioRegraModuloList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioRegraTransacao> _TcsUsuarioRegraTransacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioRegraTransacao", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioRegraTransacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioRegraTransacao> TcsUsuarioRegraTransacaoList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioRegraTransacaoList == null)
	            	this._TcsUsuarioRegraTransacaoList = new List<TcsUsuarioRegraTransacao>();
	
	            return this._TcsUsuarioRegraTransacaoList;
	        }
	        set
	        {
	            if (this._TcsUsuarioRegraTransacaoList != value)
	            {
	                this._TcsUsuarioRegraTransacaoList = value;
	                this.RaisePropertyChanged("TcsUsuarioRegraTransacaoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.UF", Source = "Uf", Target = "UF", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.CEP", Source = "Cep", Target = "CEP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.EMAIL", Source = "Email", Target = "EMAIL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.RAMAL", Source = "Ramal", Target = "RAMAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.BAIRRO", Source = "Bairro", Target = "BAIRRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.NUMERO", Source = "Numero", Target = "NUMERO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.CNPJ_CPF", Source = "CnpjCpf", Target = "CNPJ_CPF", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.FONE_FIXO", Source = "FoneFixo", Target = "FONE_FIXO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.MUNICIPIO", Source = "Municipio", Target = "MUNICIPIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.LOGRADOURO", Source = "Logradouro", Target = "LOGRADOURO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.COMPLEMENTO", Source = "Complemento", Target = "COMPLEMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.UID_USUARIO", Source = "UidUsuario", Target = "UID_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.FONE_CELULAR", Source = "FoneCelular", Target = "FONE_CELULAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.NOME_USUARIO", Source = "NomeUsuario", Target = "NOME_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.OBS_ENDERECO", Source = "ObsEndereco", Target = "OBS_ENDERECO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.DATA_CADASTRO", Source = "DataCadastro", Target = "DATA_CADASTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.DATA_ALTERACAO", Source = "DataAlteracao", Target = "DATA_ALTERACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.INSCR_ESTADUAL_RG", Source = "InscrEstadualRg", Target = "INSCR_ESTADUAL_RG", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.LX_TIPO_LOGRADOURO", Source = "LxTipoLogradouro", Target = "LX_TIPO_LOGRADOURO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA", Source = "LxPfjFisicaJuridica", Target = "LX_PFJ_FISICA_JURIDICA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Perfil];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioPerfil];ReadOnly[false];Entities[TCS_USUARIO_PERFIL:IdTcsUsuarioPerfil];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_PERFIL_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_PERFIL];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioPerfil")]
	[Serializable()]
	public partial class TcsUsuarioPerfil : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuario");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuario
	         this.TcsUsuario = (from r in context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(System.String value);
	    partial void OnDescPerfilChanged();

	    private System.String _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Perfil", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Perfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Perfil\", \"Inativo\" : \"Inativo\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : true}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescPerfil#false##60:0##Perfil#0#true##::LookUpTcsPerfil##true#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL")]
	    public System.String DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(Int64 value);
	    partial void OnIdPerfilChanged();

	    private Int64 _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Perfil", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Perfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Perfil\", \"Inativo\" : \"Inativo\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : true}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdPerfil#true##24:0##Perfil#1#false##::LookUpTcsPerfil##true#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(Int64 value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private Int64 _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
	    public Int64 IdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioPerfil", value);
	    	              this.OnIdTcsUsuarioPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioPerfil");
	    	              this._IdTcsUsuarioPerfil = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioPerfil");
	    	              this.OnIdTcsUsuarioPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Perfil\", \"Inativo\" : \"Inativo\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : true}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#Inativo#false##0:0##Inativo#2#true##::LookUpTcsPerfil##true#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTcsUsuarioPerfil;
	    [DataMember(Name = "TemporaryIdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioPerfil = this._IdTcsUsuarioPerfil;
	    	          return this._TemporaryIdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil != value)
	    	              this._TemporaryIdTcsUsuarioPerfil = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuario _TcsUsuario;
	    [DataMember(Name = "TcsUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioPerfil", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuario TcsUsuario
	    {
	        get
	        {
	            return this._TcsUsuario;
	        }
	        set
	        {
	            if (this._TcsUsuario != value)
	            {
	                this._TcsUsuario = value;
	                this.RaisePropertyChanged("TcsUsuarioList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_PERFIL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_PERFIL), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", Source = "IdTcsUsuarioPerfil", Target = "ID_TCS_USUARIO_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL", RelationPropertyName = "TCS_USUARIO_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulo];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuarioRegraModulo];ReadOnly[false];Entities[TCS_USUARIO_REGRA_MODULO:IdUsuarioRegraModulo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_REGRA_MODULO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_REGRA_MODULO];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioRegraModulo")]
	[Serializable()]
	public partial class TcsUsuarioRegraModulo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuario");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuario
	         this.TcsUsuario = (from r in context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioRegraModulo];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsUsuarioRegraModulo];LookUpFinalize[finalizeLookUpTcsUsuarioRegraModulo];LookUpDisplayColumns[{\"IdModulo\" : \"\", \"DescModulo\" : \"Módulo\", \"DescAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdModulo\" : false, \"DescModulo\" : true, \"DescAplicativo\" : true}];FilterDataKey[TCS_USUARIO_REGRA_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##12###0#false##::LookUpTcsUsuarioRegraModulo##true#false###Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_REGRA_MODULO.ID_MODULO")]
	    public Int64 IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this.ValidateProperty("IdModulo", value);
	    	              this.OnIdModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdModulo");
	    	              this._IdModulo = value;
	    	              this.RaiseDataMemberChanged("IdModulo");
	    	              this.OnIdModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioRegraModulo
	    partial void OnIdUsuarioRegraModuloChanging(Int64 value);
	    partial void OnIdUsuarioRegraModuloChanged();

	    private Int64 _IdUsuarioRegraModulo;

	    [DataMember(IsRequired = true, Name = "IdUsuarioRegraModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO")]
	    public Int64 IdUsuarioRegraModulo
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioRegraModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioRegraModulo != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioRegraModulo", value);
	    	              this.OnIdUsuarioRegraModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioRegraModulo");
	    	              this._IdUsuarioRegraModulo = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioRegraModulo");
	    	              this.OnIdUsuarioRegraModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoModulo
	    partial void OnLxRegraAcessoModuloChanging(Byte value);
	    partial void OnLxRegraAcessoModuloChanged();

	    private Byte _LxRegraAcessoModulo;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso Módulo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_MODULO.LX_REGRA_ACESSO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_MODULO.LX_REGRA_ACESSO_MODULO")]
	    public Byte LxRegraAcessoModulo
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoModulo != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoModulo", value);
	    	              this.OnLxRegraAcessoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoModulo");
	    	              this._LxRegraAcessoModulo = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoModulo");
	    	              this.OnLxRegraAcessoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For RegraTransacao
	    partial void OnRegraTransacaoChanging(System.String value);
	    partial void OnRegraTransacaoChanged();

	    private System.String _RegraTransacao;

	    [DataMember(Name = "RegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_MODULO.REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_MODULO.REGRA_TRANSACAO")]
	    public System.String RegraTransacao
	    {
	    	    get
	    	    {
	    	          return _RegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._RegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("RegraTransacao", value);
	    	              this.OnRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("RegraTransacao");
	    	              this._RegraTransacao = value;
	    	              this.RaiseDataMemberChanged("RegraTransacao");
	    	              this.OnRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(string value);
	    partial void OnDescModuloChanged();

	    private string _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescModulo#false##60:0##Módulo#1#true##::LookUpTcsUsuarioRegraModulo##true#false###Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="")]
	    public string DescModulo
	    {
	    	    get
	    	    {
	    	          if (_DescModulo != (GetDescModulo()))
	    	             _DescModulo =  GetDescModulo();
	    	          return _DescModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModulo != value)
	    	          {
	    	              this.ValidateProperty("DescModulo", value);
	    	              this.OnDescModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescModulo");
	    	              this._DescModulo = value;
	    	              this.RaiseDataMemberChanged("DescModulo");
	    	              this.OnDescModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescAplicativo
	    partial void OnDescAplicativoChanging(string value);
	    partial void OnDescAplicativoChanged();

	    private string _DescAplicativo;

	    [DataMember(Name = "DescAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescAplicativo#false##250:0##Aplicativo#2#true##::LookUpTcsUsuarioRegraModulo##true#false###Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="")]
	    public string DescAplicativo
	    {
	    	    get
	    	    {
	    	          if (_DescAplicativo != (GetDescAplicativo()))
	    	             _DescAplicativo =  GetDescAplicativo();
	    	          return _DescAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescAplicativo != value)
	    	          {
	    	              this.ValidateProperty("DescAplicativo", value);
	    	              this.OnDescAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("DescAplicativo");
	    	              this._DescAplicativo = value;
	    	              this.RaiseDataMemberChanged("DescAplicativo");
	    	              this.OnDescAplicativoChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdUsuarioRegraModulo;
	    [DataMember(Name = "TemporaryIdUsuarioRegraModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdUsuarioRegraModulo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuarioRegraModulo.IsNullOrEmpty())
	    	                this._TemporaryIdUsuarioRegraModulo = this._IdUsuarioRegraModulo;
	    	          return this._TemporaryIdUsuarioRegraModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuarioRegraModulo != value)
	    	              this._TemporaryIdUsuarioRegraModulo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuario _TcsUsuario;
	    [DataMember(Name = "TcsUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioRegraModulo", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuario TcsUsuario
	    {
	        get
	        {
	            return this._TcsUsuario;
	        }
	        set
	        {
	            if (this._TcsUsuario != value)
	            {
	                this._TcsUsuario = value;
	                this.RaisePropertyChanged("TcsUsuarioList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_REGRA_MODULO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO", RelationPropertyName = "TCS_USUARIO_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.REGRA_TRANSACAO", Source = "RegraTransacao", Target = "REGRA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO", RelationPropertyName = "TCS_USUARIO_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.LX_REGRA_ACESSO_MODULO", Source = "LxRegraAcessoModulo", Target = "LX_REGRA_ACESSO_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO", RelationPropertyName = "TCS_USUARIO_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO", Source = "IdUsuarioRegraModulo", Target = "ID_USUARIO_REGRA_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO", RelationPropertyName = "TCS_USUARIO_REGRA_MODULO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoModuloValues()
	    {
	    	    return Linx.Framework.BV.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoModuloName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoModuloName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Acesso Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoModuloName
	    {
	    	    get { if (this.LxRegraAcessoModulo.IsNull()) { _lxRegraAcessoModuloName = String.Empty; } else { string key = this.LxRegraAcessoModulo.ToString(); var dmValues = this.GetLxRegraAcessoModuloValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoModuloName) _lxRegraAcessoModuloName = domainName; } return _lxRegraAcessoModuloName; } set { _lxRegraAcessoModuloName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuarioRegraTransacao];ReadOnly[false];Entities[TCS_USUARIO_REGRA_TRANSACAO:IdUsuarioRegraTransacao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_REGRA_TRANSACAO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_REGRA_TRANSACAO];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioRegraTransacao")]
	[Serializable()]
	public partial class TcsUsuarioRegraTransacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuario");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuario
	         this.TcsUsuario = (from r in context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioRegraTransacao];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsUsuarioRegraTransacao];LookUpFinalize[finalizeLookUpTcsUsuarioRegraTransacao];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Código Transação\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsUsuarioRegraTransacao##true#false###Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.ID_TRANSACAO")]
	    public Int64 IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdTransacao", value);
	    	              this.OnIdTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacao");
	    	              this._IdTransacao = value;
	    	              this.RaiseDataMemberChanged("IdTransacao");
	    	              this.OnIdTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioRegraTransacao
	    partial void OnIdUsuarioRegraTransacaoChanging(Int64 value);
	    partial void OnIdUsuarioRegraTransacaoChanged();

	    private Int64 _IdUsuarioRegraTransacao;

	    [DataMember(IsRequired = true, Name = "IdUsuarioRegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO")]
	    public Int64 IdUsuarioRegraTransacao
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioRegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioRegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioRegraTransacao", value);
	    	              this.OnIdUsuarioRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioRegraTransacao");
	    	              this._IdUsuarioRegraTransacao = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioRegraTransacao");
	    	              this.OnIdUsuarioRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoTransacao
	    partial void OnLxRegraAcessoTransacaoChanging(Byte value);
	    partial void OnLxRegraAcessoTransacaoChanged();

	    private Byte _LxRegraAcessoTransacao;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso Transação", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO")]
	    public Byte LxRegraAcessoTransacao
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoTransacao", value);
	    	              this.OnLxRegraAcessoTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoTransacao");
	    	              this._LxRegraAcessoTransacao = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoTransacao");
	    	              this.OnLxRegraAcessoTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For RegraTransacao
	    partial void OnRegraTransacaoChanging(System.String value);
	    partial void OnRegraTransacaoChanged();

	    private System.String _RegraTransacao;

	    [DataMember(Name = "RegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.REGRA_TRANSACAO")]
	    public System.String RegraTransacao
	    {
	    	    get
	    	    {
	    	          return _RegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._RegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("RegraTransacao", value);
	    	              this.OnRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("RegraTransacao");
	    	              this._RegraTransacao = value;
	    	              this.RaiseDataMemberChanged("RegraTransacao");
	    	              this.OnRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(string value);
	    partial void OnDescTransacaoChanged();

	    private string _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescTransacao#false##60:0##Transação#1#true##::LookUpTcsUsuarioRegraTransacao##true#false###Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="")]
	    public string DescTransacao
	    {
	    	    get
	    	    {
	    	          if (_DescTransacao != (GetDescTransacao()))
	    	             _DescTransacao =  GetDescTransacao();
	    	          return _DescTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTransacao != value)
	    	          {
	    	              this.ValidateProperty("DescTransacao", value);
	    	              this.OnDescTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DescTransacao");
	    	              this._DescTransacao = value;
	    	              this.RaiseDataMemberChanged("DescTransacao");
	    	              this.OnDescTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(string value);
	    partial void OnClasseNomeChanged();

	    private string _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#ClasseNome#false##400:0##Código Transação#2#true##::LookUpTcsUsuarioRegraTransacao##true#false###Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="\"\"")]
	    public string ClasseNome
	    {
	    	    get
	    	    {
	    	          if (_ClasseNome != (GetClasseNome()))
	    	             _ClasseNome =  GetClasseNome();
	    	          return _ClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ClasseNome", value);
	    	              this.OnClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ClasseNome");
	    	              this._ClasseNome = value;
	    	              this.RaiseDataMemberChanged("ClasseNome");
	    	              this.OnClasseNomeChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdUsuarioRegraTransacao;
	    [DataMember(Name = "TemporaryIdUsuarioRegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdUsuarioRegraTransacao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuarioRegraTransacao.IsNullOrEmpty())
	    	                this._TemporaryIdUsuarioRegraTransacao = this._IdUsuarioRegraTransacao;
	    	          return this._TemporaryIdUsuarioRegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuarioRegraTransacao != value)
	    	              this._TemporaryIdUsuarioRegraTransacao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuario _TcsUsuario;
	    [DataMember(Name = "TcsUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioRegraTransacao", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuario TcsUsuario
	    {
	        get
	        {
	            return this._TcsUsuario;
	        }
	        set
	        {
	            if (this._TcsUsuario != value)
	            {
	                this._TcsUsuario = value;
	                this.RaisePropertyChanged("TcsUsuarioList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_REGRA_TRANSACAO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO", RelationPropertyName = "TCS_USUARIO_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.REGRA_TRANSACAO", Source = "RegraTransacao", Target = "REGRA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO", RelationPropertyName = "TCS_USUARIO_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO", Source = "LxRegraAcessoTransacao", Target = "LX_REGRA_ACESSO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO", RelationPropertyName = "TCS_USUARIO_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO", Source = "IdUsuarioRegraTransacao", Target = "ID_USUARIO_REGRA_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO", RelationPropertyName = "TCS_USUARIO_REGRA_TRANSACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoTransacaoValues()
	    {
	    	    return Linx.Framework.BV.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoTransacaoName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoTransacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Acesso Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoTransacaoName
	    {
	    	    get { if (this.LxRegraAcessoTransacao.IsNull()) { _lxRegraAcessoTransacaoName = String.Empty; } else { string key = this.LxRegraAcessoTransacao.ToString(); var dmValues = this.GetLxRegraAcessoTransacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoTransacaoName) _lxRegraAcessoTransacaoName = domainName; } return _lxRegraAcessoTransacaoName; } set { _lxRegraAcessoTransacaoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Coluna];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuarioRegraColuna];ReadOnly[false];Entities[TCS_USUARIO_REGRA_COLUNA:IdUsuarioRegraColuna];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_REGRA_COLUNA_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_REGRA_COLUNA];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioRegraColuna")]
	[Serializable()]
	public partial class TcsUsuarioRegraColuna : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuario");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuario
	         this.TcsUsuario = (from r in context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioRegraColuna];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsUsuarioRegraColuna];LookUpFinalize[finalizeLookUpTcsUsuarioRegraColuna];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Código Transação\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsUsuarioRegraColuna##false#false###Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_REGRA_COLUNA.ID_TRANSACAO")]
	    public Int64 IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdTransacao", value);
	    	              this.OnIdTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacao");
	    	              this._IdTransacao = value;
	    	              this.RaiseDataMemberChanged("IdTransacao");
	    	              this.OnIdTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioRegraColuna
	    partial void OnIdUsuarioRegraColunaChanging(Int32 value);
	    partial void OnIdUsuarioRegraColunaChanged();

	    private Int32 _IdUsuarioRegraColuna;

	    [DataMember(IsRequired = true, Name = "IdUsuarioRegraColuna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Coluna", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA")]
	    public Int32 IdUsuarioRegraColuna
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioRegraColuna;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioRegraColuna != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioRegraColuna", value);
	    	              this.OnIdUsuarioRegraColunaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioRegraColuna");
	    	              this._IdUsuarioRegraColuna = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioRegraColuna");
	    	              this.OnIdUsuarioRegraColunaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoColuna
	    partial void OnLxRegraAcessoColunaChanging(Byte value);
	    partial void OnLxRegraAcessoColunaChanged();

	    private Byte _LxRegraAcessoColuna;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoColuna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso Coluna", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcessoColuna];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.LX_REGRA_ACESSO_COLUNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.LX_REGRA_ACESSO_COLUNA")]
	    public Byte LxRegraAcessoColuna
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoColuna;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoColuna != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoColuna", value);
	    	              this.OnLxRegraAcessoColunaChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoColuna");
	    	              this._LxRegraAcessoColuna = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoColuna");
	    	              this.OnLxRegraAcessoColunaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For RegraTransacao
	    partial void OnRegraTransacaoChanging(System.String value);
	    partial void OnRegraTransacaoChanged();

	    private System.String _RegraTransacao;

	    [DataMember(Name = "RegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.REGRA_TRANSACAO")]
	    public System.String RegraTransacao
	    {
	    	    get
	    	    {
	    	          return _RegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._RegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("RegraTransacao", value);
	    	              this.OnRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("RegraTransacao");
	    	              this._RegraTransacao = value;
	    	              this.RaiseDataMemberChanged("RegraTransacao");
	    	              this.OnRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TransacaoColuna
	    partial void OnTransacaoColunaChanging(System.String value);
	    partial void OnTransacaoColunaChanged();

	    private System.String _TransacaoColuna;

	    [DataMember(IsRequired = true, Name = "TransacaoColuna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação Coluna", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TRANSACAO_COLUNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.TRANSACAO_COLUNA")]
	    public System.String TransacaoColuna
	    {
	    	    get
	    	    {
	    	          return _TransacaoColuna;
	    	    }
	    	    set
	    	    {
	    	          if (this._TransacaoColuna != value)
	    	          {
	    	              this.ValidateProperty("TransacaoColuna", value);
	    	              this.OnTransacaoColunaChanging(value);
	    	              this.RaiseDataMemberChanging("TransacaoColuna");
	    	              this._TransacaoColuna = value;
	    	              this.RaiseDataMemberChanged("TransacaoColuna");
	    	              this.OnTransacaoColunaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(string value);
	    partial void OnDescTransacaoChanged();

	    private string _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescTransacao#false##60:0##Transação#1#true##::LookUpTcsUsuarioRegraColuna##false#false###Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="")]
	    public string DescTransacao
	    {
	    	    get
	    	    {
	    	          if (_DescTransacao != (GetDescTransacao()))
	    	             _DescTransacao =  GetDescTransacao();
	    	          return _DescTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTransacao != value)
	    	          {
	    	              this.ValidateProperty("DescTransacao", value);
	    	              this.OnDescTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DescTransacao");
	    	              this._DescTransacao = value;
	    	              this.RaiseDataMemberChanged("DescTransacao");
	    	              this.OnDescTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(string value);
	    partial void OnClasseNomeChanged();

	    private string _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#ClasseNome#false##40:0##Código Transação#2#true##::LookUpTcsUsuarioRegraColuna##false#false###Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="\"\"")]
	    public string ClasseNome
	    {
	    	    get
	    	    {
	    	          if (_ClasseNome != (GetClasseNome()))
	    	             _ClasseNome =  GetClasseNome();
	    	          return _ClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ClasseNome", value);
	    	              this.OnClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ClasseNome");
	    	              this._ClasseNome = value;
	    	              this.RaiseDataMemberChanged("ClasseNome");
	    	              this.OnClasseNomeChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdUsuarioRegraColuna;
	    [DataMember(Name = "TemporaryIdUsuarioRegraColuna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Coluna (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdUsuarioRegraColuna
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuarioRegraColuna.IsNullOrEmpty())
	    	                this._TemporaryIdUsuarioRegraColuna = this._IdUsuarioRegraColuna;
	    	          return this._TemporaryIdUsuarioRegraColuna;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuarioRegraColuna != value)
	    	              this._TemporaryIdUsuarioRegraColuna = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuario _TcsUsuario;
	    [DataMember(Name = "TcsUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioRegraColuna", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuario TcsUsuario
	    {
	        get
	        {
	            return this._TcsUsuario;
	        }
	        set
	        {
	            if (this._TcsUsuario != value)
	            {
	                this._TcsUsuario = value;
	                this.RaisePropertyChanged("TcsUsuarioList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_REGRA_COLUNA), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.REGRA_TRANSACAO", Source = "RegraTransacao", Target = "REGRA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.TRANSACAO_COLUNA", Source = "TransacaoColuna", Target = "TRANSACAO_COLUNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.LX_REGRA_ACESSO_COLUNA", Source = "LxRegraAcessoColuna", Target = "LX_REGRA_ACESSO_COLUNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA", Source = "IdUsuarioRegraColuna", Target = "ID_USUARIO_REGRA_COLUNA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoColunaValues()
	    {
	    	    return Linx.Framework.BV.Domains.RegraAcessoColuna.GetValues();
	    }
	    private string _lxRegraAcessoColunaName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoColunaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Acesso Coluna", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoColunaName
	    {
	    	    get { if (this.LxRegraAcessoColuna.IsNull()) { _lxRegraAcessoColunaName = String.Empty; } else { string key = this.LxRegraAcessoColuna.ToString(); var dmValues = this.GetLxRegraAcessoColunaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoColunaName) _lxRegraAcessoColunaName = domainName; } return _lxRegraAcessoColunaName; } set { _lxRegraAcessoColunaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE,TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Bandeira / Rede];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];Entities[TBC_BANDEIRA_REDE:IdBandeiraR];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_BANDEIRA_REDE_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_BANDEIRA_REDE];EntityRelations[TCS_USUARIO(TCS_USUARIO)#TBC_BANDEIRA_REDE(TBC_BANDEIRA_REDE)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioBandeiraRede")]
	[Serializable()]
	public partial class TcsUsuarioBandeiraRede : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuario");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuario
	         this.TcsUsuario = (from r in context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescBandeiraRede
	    partial void OnDescBandeiraRedeChanging(System.String value);
	    partial void OnDescBandeiraRedeChanged();

	    private System.String _DescBandeiraRede;

	    [DataMember(IsRequired = true, Name = "DescBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRede];LookUpTitle[Seleção de (Bandeira / Rede)];LookUpQuery[executeLookUpTbcBandeiraRede];LookUpFinalize[finalizeLookUpTbcBandeiraRede];LookUpDisplayColumns[{\"DescBandeiraRede\" : \"Bandeira / Rede\", \"IdBandeiraR\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"DescBandeiraRede\" : true, \"IdBandeiraR\" : false}];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescBandeiraRede#false##60:0##Bandeira / Rede#0#true##::LookUpTbcBandeiraRede##true#false#TBC_BANDEIRA_REDE#TBC_BANDEIRA_REDE#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE")]
	    public System.String DescBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _DescBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("DescBandeiraRede", value);
	    	              this.OnDescBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("DescBandeiraRede");
	    	              this._DescBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("DescBandeiraRede");
	    	              this.OnDescBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraR
	    partial void OnIdBandeiraRChanging(Int32 value);
	    partial void OnIdBandeiraRChanged();

	    private Int32 _IdBandeiraR;

	    [DataMember(IsRequired = true, Name = "IdBandeiraR", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRede];LookUpTitle[Seleção de (Bandeira / Rede)];LookUpQuery[executeLookUpTbcBandeiraRede];LookUpFinalize[finalizeLookUpTbcBandeiraRede];LookUpDisplayColumns[{\"DescBandeiraRede\" : \"Bandeira / Rede\", \"IdBandeiraR\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"DescBandeiraRede\" : true, \"IdBandeiraR\" : false}];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdBandeiraR#true##12:0##Id Bandeira Rede#1#false##::LookUpTbcBandeiraRede##true#false#TBC_BANDEIRA_REDE#TBC_BANDEIRA_REDE#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE")]
	    public Int32 IdBandeiraR
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraR;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraR != value)
	    	          {
	    	              this.ValidateProperty("IdBandeiraR", value);
	    	              this.OnIdBandeiraRChanging(value);
	    	              this.RaiseDataMemberChanging("IdBandeiraR");
	    	              this._IdBandeiraR = value;
	    	              this.RaiseDataMemberChanged("IdBandeiraR");
	    	              this.OnIdBandeiraRChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuario _TcsUsuario;
	    [DataMember(Name = "TcsUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioBandeiraRede", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuario TcsUsuario
	    {
	        get
	        {
	            return this._TcsUsuario;
	        }
	        set
	        {
	            if (this._TcsUsuario != value)
	            {
	                this._TcsUsuario = value;
	                this.RaisePropertyChanged("TcsUsuarioList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_BANDEIRA_REDE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_BANDEIRA_REDE), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_BANDEIRA_REDE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "ID_USUARIO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE", Source = "IdBandeiraR", Target = "ID_BANDEIRA_REDE", TargetKeyName = "ID_BANDEIRA_REDE", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_LAYOUT_USUARIO.TCS_LAYOUT.ID_OBJETO_CONTEUDO,TCS_LAYOUT_USUARIO.TCS_USUARIO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Layouts];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_LAYOUT_USUARIO_LISTA as #Alias#];EdmEntityName[TCS_LAYOUT_USUARIO];EntityRelations[TCS_LAYOUT(TCS_LAYOUT)#TCS_OBJETO_CONTEUDO(TCS_OBJETO_CONTEUDO)#TCS_LAYOUT_LISTA(TCS_LAYOUT)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioLayout")]
	[Serializable()]
	public partial class TcsUsuarioLayout : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuario");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuario
	         this.TcsUsuario = (from r in context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescLayout
	    partial void OnDescLayoutChanging(System.String value);
	    partial void OnDescLayoutChanged();

	    private System.String _DescLayout;

	    [DataMember(IsRequired = true, Name = "DescLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Layout", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayout];LookUpTitle[Seleção de (Layout)];LookUpQuery[executeLookUpTcsLayout];LookUpFinalize[finalizeLookUpTcsLayout];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Inativo\" : \"Inativo\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Inativo\" : true, \"IdObjetoConteudo\" : true}];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_LAYOUT.DESC_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescLayout#false##60:0##Desc Layout#0#true##::LookUpTcsLayout##false#false#TCS_LAYOUT#TCS_LAYOUT#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_LAYOUT_USUARIO.TCS_LAYOUT.DESC_LAYOUT")]
	    public System.String DescLayout
	    {
	    	    get
	    	    {
	    	          return _DescLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLayout != value)
	    	          {
	    	              this.ValidateProperty("DescLayout", value);
	    	              this.OnDescLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("DescLayout");
	    	              this._DescLayout = value;
	    	              this.RaiseDataMemberChanged("DescLayout");
	    	              this.OnDescLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Detalhes
	    partial void OnDetalhesChanging(System.String value);
	    partial void OnDetalhesChanged();

	    private System.String _Detalhes;

	    [DataMember(Name = "Detalhes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhes", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayout];LookUpTitle[Seleção de (Detalhes)];LookUpQuery[executeLookUpTcsLayout];LookUpFinalize[finalizeLookUpTcsLayout];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Inativo\" : \"Inativo\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Inativo\" : true, \"IdObjetoConteudo\" : true}];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_LAYOUT.DETALHES];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Detalhes#false##500:0##Detalhes#1#true##::LookUpTcsLayout##false#false#TCS_LAYOUT#TCS_LAYOUT#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_LAYOUT_USUARIO.TCS_LAYOUT.DETALHES")]
	    public System.String Detalhes
	    {
	    	    get
	    	    {
	    	          return _Detalhes;
	    	    }
	    	    set
	    	    {
	    	          if (this._Detalhes != value)
	    	          {
	    	              this.ValidateProperty("Detalhes", value);
	    	              this.OnDetalhesChanging(value);
	    	              this.RaiseDataMemberChanging("Detalhes");
	    	              this._Detalhes = value;
	    	              this.RaiseDataMemberChanged("Detalhes");
	    	              this.OnDetalhesChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjetoConteudo
	    partial void OnIdObjetoConteudoChanging(Int64 value);
	    partial void OnIdObjetoConteudoChanged();

	    private Int64 _IdObjetoConteudo;

	    [DataMember(IsRequired = true, Name = "IdObjetoConteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayout];LookUpTitle[Seleção de (Id Objeto Conteudo)];LookUpQuery[executeLookUpTcsLayout];LookUpFinalize[finalizeLookUpTcsLayout];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Inativo\" : \"Inativo\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Inativo\" : true, \"IdObjetoConteudo\" : true}];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_LAYOUT.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdObjetoConteudo#true##24:0##Id Objeto Conteudo#3#true##::LookUpTcsLayout##false#false#TCS_LAYOUT#TCS_LAYOUT#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_LAYOUT_USUARIO.TCS_LAYOUT.ID_OBJETO_CONTEUDO")]
	    public Int64 IdObjetoConteudo
	    {
	    	    get
	    	    {
	    	          return _IdObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjetoConteudo != value)
	    	          {
	    	              this.ValidateProperty("IdObjetoConteudo", value);
	    	              this.OnIdObjetoConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjetoConteudo");
	    	              this._IdObjetoConteudo = value;
	    	              this.RaiseDataMemberChanged("IdObjetoConteudo");
	    	              this.OnIdObjetoConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LAYOUT_USUARIO.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayout];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsLayout];LookUpFinalize[finalizeLookUpTcsLayout];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Inativo\" : \"Inativo\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Inativo\" : true, \"IdObjetoConteudo\" : true}];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_LAYOUT.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#Inativo#false##0:0##Inativo#2#true##::LookUpTcsLayout##false#false#TCS_LAYOUT#TCS_LAYOUT#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_LAYOUT_USUARIO.TCS_LAYOUT.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuario _TcsUsuario;
	    [DataMember(Name = "TcsUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioLayout", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuario TcsUsuario
	    {
	        get
	        {
	            return this._TcsUsuario;
	        }
	        set
	        {
	            if (this._TcsUsuario != value)
	            {
	                this._TcsUsuario = value;
	                this.RaisePropertyChanged("TcsUsuarioList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_LAYOUT_USUARIO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_LAYOUT_USUARIO), QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT_USUARIO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LAYOUT_USUARIO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "ID_USUARIO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LAYOUT_USUARIO.TCS_LAYOUT.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "ID_OBJETO_CONTEUDO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Filial];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioFilial];ReadOnly[false];Entities[TCS_USUARIO_FILIAL:IdTcsUsuarioFilial|TBC_FILIAL:IdFilialPfj];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_USUARIO_FILIAL];EntityRelations[TCS_USUARIO(TCS_USUARIO)#TBC_FILIAL(TBC_FILIAL)#MATRIZ_CONTABIL(TBC_FILIAL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TBC_PFJ(TBC_PFJ)#TBC_FILIAL_LISTA(TBC_FILIAL)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioFilial")]
	[Serializable()]
	public partial class TcsUsuarioFilial : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuario");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuario
	         this.TcsUsuario = (from r in context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodigoFilial
	    partial void OnCodigoFilialChanging(System.String value);
	    partial void OnCodigoFilialChanged();

	    private System.String _CodigoFilial;

	    [DataMember(Name = "CodigoFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Código Filial)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_USUARIO_FILIAL.TBC_FILIAL.CODIGO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#CodigoFilial#false##18:0##Código Filial#0#true##::LookUpTbcFilial##false#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_FILIAL.TBC_FILIAL.CODIGO_FILIAL")]
	    public System.String CodigoFilial
	    {
	    	    get
	    	    {
	    	          return _CodigoFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoFilial != value)
	    	          {
	    	              this.ValidateProperty("CodigoFilial", value);
	    	              this.OnCodigoFilialChanging(value);
	    	              this.RaiseDataMemberChanging("CodigoFilial");
	    	              this._CodigoFilial = value;
	    	              this.RaiseDataMemberChanged("CodigoFilial");
	    	              this.OnCodigoFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFilialPfj
	    partial void OnIdFilialPfjChanging(Int32 value);
	    partial void OnIdFilialPfjChanged();

	    private Int32 _IdFilialPfj;

	    [DataMember(IsRequired = true, Name = "IdFilialPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Id Filial Pfj)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_USUARIO_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdFilialPfj#true##12:0##Id Filial Pfj#1#false##::LookUpTbcFilial##false#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ")]
	    public Int32 IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this.ValidateProperty("IdFilialPfj", value);
	    	              this.OnIdFilialPfjChanging(value);
	    	              this.RaiseDataMemberChanging("IdFilialPfj");
	    	              this._IdFilialPfj = value;
	    	              this.RaiseDataMemberChanged("IdFilialPfj");
	    	              this.OnIdFilialPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioFilial
	    partial void OnIdTcsUsuarioFilialChanging(Int64 value);
	    partial void OnIdTcsUsuarioFilialChanged();

	    private Int64 _IdTcsUsuarioFilial;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL")]
	    public Int64 IdTcsUsuarioFilial
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioFilial != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioFilial", value);
	    	              this.OnIdTcsUsuarioFilialChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioFilial");
	    	              this._IdTcsUsuarioFilial = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioFilial");
	    	              this.OnIdTcsUsuarioFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FILIAL.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeFilial
	    partial void OnNomeFilialChanging(System.String value);
	    partial void OnNomeFilialChanged();

	    private System.String _NomeFilial;

	    [DataMember(Name = "NomeFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Fantasia", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Nome Fantasia)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_USUARIO_FILIAL.TBC_FILIAL.NOME_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeFilial#false##60:0##Nome Fantasia#2#true##::LookUpTbcFilial##false#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_FILIAL.TBC_FILIAL.NOME_FILIAL")]
	    public System.String NomeFilial
	    {
	    	    get
	    	    {
	    	          return _NomeFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeFilial != value)
	    	          {
	    	              this.ValidateProperty("NomeFilial", value);
	    	              this.OnNomeFilialChanging(value);
	    	              this.RaiseDataMemberChanging("NomeFilial");
	    	              this._NomeFilial = value;
	    	              this.RaiseDataMemberChanged("NomeFilial");
	    	              this.OnNomeFilialChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTcsUsuarioFilial;
	    [DataMember(Name = "TemporaryIdTcsUsuarioFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Filial (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsUsuarioFilial
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioFilial.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioFilial = this._IdTcsUsuarioFilial;
	    	          return this._TemporaryIdTcsUsuarioFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioFilial != value)
	    	              this._TemporaryIdTcsUsuarioFilial = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuario _TcsUsuario;
	    [DataMember(Name = "TcsUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuario_TcsUsuarioFilial", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuario TcsUsuario
	    {
	        get
	        {
	            return this._TcsUsuario;
	        }
	        set
	        {
	            if (this._TcsUsuario != value)
	            {
	                this._TcsUsuario = value;
	                this.RaisePropertyChanged("TcsUsuarioList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_FILIAL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_FILIAL), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FILIAL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL", Source = "IdTcsUsuarioFilial", Target = "ID_TCS_USUARIO_FILIAL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FILIAL", RelationPropertyName = "TCS_USUARIO_FILIAL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FILIAL.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ", Source = "IdFilialPfj", Target = "ID_FILIAL_PFJ", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_FILIAL", RelationPropertyName = "TBC_FILIAL" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAcessoLocal];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[false];Entities[TCS_USUARIO:IdUsuario];SubQueryInfo[];EdmEntityName[TCS_USUARIO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAcessoLocal")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Usuario.TcsUsuarioAcessoLocal")]
	public partial class TcsUsuarioAcessoLocal : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdUsuario;
	    [DataMember(Name = "TemporaryIdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario (Tmp)", Description="Temporary Key", Order = 11, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdUsuario
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuario.IsNullOrEmpty())
	    	                this._TemporaryIdUsuario = this._IdUsuario;
	    	          return this._TemporaryIdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuario != value)
	    	              this._TemporaryIdUsuario = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioPerfilP];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioPerfil];ReadOnly[false];Entities[TCS_USUARIO_PERFIL:IdTcsUsuarioPerfil];SubQueryInfo[];EdmEntityName[TCS_USUARIO_PERFIL];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioPerfilP")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Usuario.TcsUsuarioPerfilP")]
	public partial class TcsUsuarioPerfilP : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(Int64 value);
	    partial void OnIdPerfilChanged();

	    private Int64 _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(Int64 value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private Int64 _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
	    public Int64 IdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioPerfil", value);
	    	              this.OnIdTcsUsuarioPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioPerfil");
	    	              this._IdTcsUsuarioPerfil = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioPerfil");
	    	              this.OnIdTcsUsuarioPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTcsUsuarioPerfil;
	    [DataMember(Name = "TemporaryIdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioPerfil = this._IdTcsUsuarioPerfil;
	    	          return this._TemporaryIdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil != value)
	    	              this._TemporaryIdTcsUsuarioPerfil = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_PERFIL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_PERFIL), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", Source = "IdTcsUsuarioPerfil", Target = "ID_TCS_USUARIO_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL", RelationPropertyName = "TCS_USUARIO_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Perfil];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioPerfil];ReadOnly[false];Entities[TCS_USUARIO_PERFIL:IdTcsUsuarioPerfil];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_PERFIL_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_PERFIL];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioPerfil")]
	[Serializable()]
	public partial class TcsUsuarioPerfilParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(System.String value);
	    partial void OnDescPerfilChanged();

	    private System.String _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Perfil", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Perfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Perfil\", \"Inativo\" : \"Inativo\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : true}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescPerfil#false##60:0##Perfil#0#true##::LookUpTcsPerfil##true#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL")]
	    public System.String DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(Int64 value);
	    partial void OnIdPerfilChanged();

	    private Int64 _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Perfil", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Perfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Perfil\", \"Inativo\" : \"Inativo\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : true}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdPerfil#true##24:0##Perfil#1#false##::LookUpTcsPerfil##true#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(Int64 value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private Int64 _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
	    public Int64 IdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioPerfil", value);
	    	              this.OnIdTcsUsuarioPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioPerfil");
	    	              this._IdTcsUsuarioPerfil = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioPerfil");
	    	              this.OnIdTcsUsuarioPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Perfil\", \"Inativo\" : \"Inativo\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : true}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#Inativo#false##0:0##Inativo#2#true##::LookUpTcsPerfil##true#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioCopia
	    partial void OnIdUsuarioCopiaChanging(Int64 value);
	    partial void OnIdUsuarioCopiaChanged();

	    private Int64 _IdUsuarioCopia;

	    [DataMember(Name = "IdUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="0")]
	    public Int64 IdUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioCopia", value);
	    	              this.OnIdUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioCopia");
	    	              this._IdUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioCopia");
	    	              this.OnIdUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro / Número", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município / UF", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuarioCopia
	    partial void OnNomeUsuarioCopiaChanging(System.String value);
	    partial void OnNomeUsuarioCopiaChanged();

	    private System.String _NomeUsuarioCopia;

	    [DataMember(Name = "NomeUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Cópia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public System.String NomeUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _NomeUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuarioCopia", value);
	    	              this.OnNomeUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuarioCopia");
	    	              this._NomeUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("NomeUsuarioCopia");
	    	              this.OnNomeUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_PERFIL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_PERFIL), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", Source = "IdTcsUsuarioPerfil", Target = "ID_TCS_USUARIO_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL", RelationPropertyName = "TCS_USUARIO_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulo];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuarioRegraModulo];ReadOnly[false];Entities[TCS_USUARIO_REGRA_MODULO:IdUsuarioRegraModulo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_REGRA_MODULO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_REGRA_MODULO];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioRegraModulo")]
	[Serializable()]
	public partial class TcsUsuarioRegraModuloParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioRegraModulo];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsUsuarioRegraModulo];LookUpFinalize[finalizeLookUpTcsUsuarioRegraModulo];LookUpDisplayColumns[{\"IdModulo\" : \"\", \"DescModulo\" : \"Módulo\", \"DescAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdModulo\" : false, \"DescModulo\" : true, \"DescAplicativo\" : true}];FilterDataKey[TCS_USUARIO_REGRA_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##12###0#false##::LookUpTcsUsuarioRegraModulo##true#false###Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_REGRA_MODULO.ID_MODULO")]
	    public Int64 IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this.ValidateProperty("IdModulo", value);
	    	              this.OnIdModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdModulo");
	    	              this._IdModulo = value;
	    	              this.RaiseDataMemberChanged("IdModulo");
	    	              this.OnIdModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioRegraModulo
	    partial void OnIdUsuarioRegraModuloChanging(Int64 value);
	    partial void OnIdUsuarioRegraModuloChanged();

	    private Int64 _IdUsuarioRegraModulo;

	    [DataMember(IsRequired = true, Name = "IdUsuarioRegraModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO")]
	    public Int64 IdUsuarioRegraModulo
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioRegraModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioRegraModulo != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioRegraModulo", value);
	    	              this.OnIdUsuarioRegraModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioRegraModulo");
	    	              this._IdUsuarioRegraModulo = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioRegraModulo");
	    	              this.OnIdUsuarioRegraModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoModulo
	    partial void OnLxRegraAcessoModuloChanging(Byte value);
	    partial void OnLxRegraAcessoModuloChanged();

	    private Byte _LxRegraAcessoModulo;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso Módulo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_MODULO.LX_REGRA_ACESSO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_MODULO.LX_REGRA_ACESSO_MODULO")]
	    public Byte LxRegraAcessoModulo
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoModulo != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoModulo", value);
	    	              this.OnLxRegraAcessoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoModulo");
	    	              this._LxRegraAcessoModulo = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoModulo");
	    	              this.OnLxRegraAcessoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For RegraTransacao
	    partial void OnRegraTransacaoChanging(System.String value);
	    partial void OnRegraTransacaoChanged();

	    private System.String _RegraTransacao;

	    [DataMember(Name = "RegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_MODULO.REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_MODULO.REGRA_TRANSACAO")]
	    public System.String RegraTransacao
	    {
	    	    get
	    	    {
	    	          return _RegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._RegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("RegraTransacao", value);
	    	              this.OnRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("RegraTransacao");
	    	              this._RegraTransacao = value;
	    	              this.RaiseDataMemberChanged("RegraTransacao");
	    	              this.OnRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioCopia
	    partial void OnIdUsuarioCopiaChanging(Int64 value);
	    partial void OnIdUsuarioCopiaChanged();

	    private Int64 _IdUsuarioCopia;

	    [DataMember(Name = "IdUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="0")]
	    public Int64 IdUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioCopia", value);
	    	              this.OnIdUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioCopia");
	    	              this._IdUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioCopia");
	    	              this.OnIdUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro / Número", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município / UF", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuarioCopia
	    partial void OnNomeUsuarioCopiaChanging(System.String value);
	    partial void OnNomeUsuarioCopiaChanged();

	    private System.String _NomeUsuarioCopia;

	    [DataMember(Name = "NomeUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Cópia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public System.String NomeUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _NomeUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuarioCopia", value);
	    	              this.OnNomeUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuarioCopia");
	    	              this._NomeUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("NomeUsuarioCopia");
	    	              this.OnNomeUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_REGRA_MODULO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO", RelationPropertyName = "TCS_USUARIO_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.REGRA_TRANSACAO", Source = "RegraTransacao", Target = "REGRA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO", RelationPropertyName = "TCS_USUARIO_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.LX_REGRA_ACESSO_MODULO", Source = "LxRegraAcessoModulo", Target = "LX_REGRA_ACESSO_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO", RelationPropertyName = "TCS_USUARIO_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO", Source = "IdUsuarioRegraModulo", Target = "ID_USUARIO_REGRA_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_MODULO", RelationPropertyName = "TCS_USUARIO_REGRA_MODULO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoModuloValues()
	    {
	    	    return Linx.Framework.BV.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoModuloName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoModuloName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Acesso Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoModuloName
	    {
	    	    get { if (this.LxRegraAcessoModulo.IsNull()) { _lxRegraAcessoModuloName = String.Empty; } else { string key = this.LxRegraAcessoModulo.ToString(); var dmValues = this.GetLxRegraAcessoModuloValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoModuloName) _lxRegraAcessoModuloName = domainName; } return _lxRegraAcessoModuloName; } set { _lxRegraAcessoModuloName = value;  }
	    }
	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuarioRegraTransacao];ReadOnly[false];Entities[TCS_USUARIO_REGRA_TRANSACAO:IdUsuarioRegraTransacao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_REGRA_TRANSACAO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_REGRA_TRANSACAO];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioRegraTransacao")]
	[Serializable()]
	public partial class TcsUsuarioRegraTransacaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioRegraTransacao];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsUsuarioRegraTransacao];LookUpFinalize[finalizeLookUpTcsUsuarioRegraTransacao];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Código Transação\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsUsuarioRegraTransacao##true#false###Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.ID_TRANSACAO")]
	    public Int64 IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdTransacao", value);
	    	              this.OnIdTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacao");
	    	              this._IdTransacao = value;
	    	              this.RaiseDataMemberChanged("IdTransacao");
	    	              this.OnIdTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioRegraTransacao
	    partial void OnIdUsuarioRegraTransacaoChanging(Int64 value);
	    partial void OnIdUsuarioRegraTransacaoChanged();

	    private Int64 _IdUsuarioRegraTransacao;

	    [DataMember(IsRequired = true, Name = "IdUsuarioRegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO")]
	    public Int64 IdUsuarioRegraTransacao
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioRegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioRegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioRegraTransacao", value);
	    	              this.OnIdUsuarioRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioRegraTransacao");
	    	              this._IdUsuarioRegraTransacao = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioRegraTransacao");
	    	              this.OnIdUsuarioRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoTransacao
	    partial void OnLxRegraAcessoTransacaoChanging(Byte value);
	    partial void OnLxRegraAcessoTransacaoChanged();

	    private Byte _LxRegraAcessoTransacao;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso Transação", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO")]
	    public Byte LxRegraAcessoTransacao
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoTransacao", value);
	    	              this.OnLxRegraAcessoTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoTransacao");
	    	              this._LxRegraAcessoTransacao = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoTransacao");
	    	              this.OnLxRegraAcessoTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For RegraTransacao
	    partial void OnRegraTransacaoChanging(System.String value);
	    partial void OnRegraTransacaoChanged();

	    private System.String _RegraTransacao;

	    [DataMember(Name = "RegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.REGRA_TRANSACAO")]
	    public System.String RegraTransacao
	    {
	    	    get
	    	    {
	    	          return _RegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._RegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("RegraTransacao", value);
	    	              this.OnRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("RegraTransacao");
	    	              this._RegraTransacao = value;
	    	              this.RaiseDataMemberChanged("RegraTransacao");
	    	              this.OnRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioCopia
	    partial void OnIdUsuarioCopiaChanging(Int64 value);
	    partial void OnIdUsuarioCopiaChanged();

	    private Int64 _IdUsuarioCopia;

	    [DataMember(Name = "IdUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="0")]
	    public Int64 IdUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioCopia", value);
	    	              this.OnIdUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioCopia");
	    	              this._IdUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioCopia");
	    	              this.OnIdUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro / Número", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município / UF", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuarioCopia
	    partial void OnNomeUsuarioCopiaChanging(System.String value);
	    partial void OnNomeUsuarioCopiaChanged();

	    private System.String _NomeUsuarioCopia;

	    [DataMember(Name = "NomeUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Cópia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public System.String NomeUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _NomeUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuarioCopia", value);
	    	              this.OnNomeUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuarioCopia");
	    	              this._NomeUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("NomeUsuarioCopia");
	    	              this.OnNomeUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_REGRA_TRANSACAO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO", RelationPropertyName = "TCS_USUARIO_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.REGRA_TRANSACAO", Source = "RegraTransacao", Target = "REGRA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO", RelationPropertyName = "TCS_USUARIO_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO", Source = "LxRegraAcessoTransacao", Target = "LX_REGRA_ACESSO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO", RelationPropertyName = "TCS_USUARIO_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO", Source = "IdUsuarioRegraTransacao", Target = "ID_USUARIO_REGRA_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_TRANSACAO", RelationPropertyName = "TCS_USUARIO_REGRA_TRANSACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoTransacaoValues()
	    {
	    	    return Linx.Framework.BV.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoTransacaoName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoTransacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Acesso Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoTransacaoName
	    {
	    	    get { if (this.LxRegraAcessoTransacao.IsNull()) { _lxRegraAcessoTransacaoName = String.Empty; } else { string key = this.LxRegraAcessoTransacao.ToString(); var dmValues = this.GetLxRegraAcessoTransacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoTransacaoName) _lxRegraAcessoTransacaoName = domainName; } return _lxRegraAcessoTransacaoName; } set { _lxRegraAcessoTransacaoName = value;  }
	    }
	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Coluna];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuarioRegraColuna];ReadOnly[false];Entities[TCS_USUARIO_REGRA_COLUNA:IdUsuarioRegraColuna];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_REGRA_COLUNA_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_REGRA_COLUNA];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioRegraColuna")]
	[Serializable()]
	public partial class TcsUsuarioRegraColunaParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioRegraColuna];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsUsuarioRegraColuna];LookUpFinalize[finalizeLookUpTcsUsuarioRegraColuna];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Código Transação\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsUsuarioRegraColuna##false#false###Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_REGRA_COLUNA.ID_TRANSACAO")]
	    public Int64 IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdTransacao", value);
	    	              this.OnIdTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacao");
	    	              this._IdTransacao = value;
	    	              this.RaiseDataMemberChanged("IdTransacao");
	    	              this.OnIdTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioRegraColuna
	    partial void OnIdUsuarioRegraColunaChanging(Int32 value);
	    partial void OnIdUsuarioRegraColunaChanged();

	    private Int32 _IdUsuarioRegraColuna;

	    [DataMember(IsRequired = true, Name = "IdUsuarioRegraColuna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Coluna", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA")]
	    public Int32 IdUsuarioRegraColuna
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioRegraColuna;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioRegraColuna != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioRegraColuna", value);
	    	              this.OnIdUsuarioRegraColunaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioRegraColuna");
	    	              this._IdUsuarioRegraColuna = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioRegraColuna");
	    	              this.OnIdUsuarioRegraColunaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoColuna
	    partial void OnLxRegraAcessoColunaChanging(Byte value);
	    partial void OnLxRegraAcessoColunaChanged();

	    private Byte _LxRegraAcessoColuna;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoColuna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso Coluna", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcessoColuna];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.LX_REGRA_ACESSO_COLUNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.LX_REGRA_ACESSO_COLUNA")]
	    public Byte LxRegraAcessoColuna
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoColuna;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoColuna != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoColuna", value);
	    	              this.OnLxRegraAcessoColunaChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoColuna");
	    	              this._LxRegraAcessoColuna = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoColuna");
	    	              this.OnLxRegraAcessoColunaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For RegraTransacao
	    partial void OnRegraTransacaoChanging(System.String value);
	    partial void OnRegraTransacaoChanged();

	    private System.String _RegraTransacao;

	    [DataMember(Name = "RegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.REGRA_TRANSACAO")]
	    public System.String RegraTransacao
	    {
	    	    get
	    	    {
	    	          return _RegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._RegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("RegraTransacao", value);
	    	              this.OnRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("RegraTransacao");
	    	              this._RegraTransacao = value;
	    	              this.RaiseDataMemberChanged("RegraTransacao");
	    	              this.OnRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TransacaoColuna
	    partial void OnTransacaoColunaChanging(System.String value);
	    partial void OnTransacaoColunaChanged();

	    private System.String _TransacaoColuna;

	    [DataMember(IsRequired = true, Name = "TransacaoColuna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação Coluna", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TRANSACAO_COLUNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_REGRA_COLUNA.TRANSACAO_COLUNA")]
	    public System.String TransacaoColuna
	    {
	    	    get
	    	    {
	    	          return _TransacaoColuna;
	    	    }
	    	    set
	    	    {
	    	          if (this._TransacaoColuna != value)
	    	          {
	    	              this.ValidateProperty("TransacaoColuna", value);
	    	              this.OnTransacaoColunaChanging(value);
	    	              this.RaiseDataMemberChanging("TransacaoColuna");
	    	              this._TransacaoColuna = value;
	    	              this.RaiseDataMemberChanged("TransacaoColuna");
	    	              this.OnTransacaoColunaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioCopia
	    partial void OnIdUsuarioCopiaChanging(Int64 value);
	    partial void OnIdUsuarioCopiaChanged();

	    private Int64 _IdUsuarioCopia;

	    [DataMember(Name = "IdUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="0")]
	    public Int64 IdUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioCopia", value);
	    	              this.OnIdUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioCopia");
	    	              this._IdUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioCopia");
	    	              this.OnIdUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro / Número", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município / UF", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuarioCopia
	    partial void OnNomeUsuarioCopiaChanging(System.String value);
	    partial void OnNomeUsuarioCopiaChanged();

	    private System.String _NomeUsuarioCopia;

	    [DataMember(Name = "NomeUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Cópia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public System.String NomeUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _NomeUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuarioCopia", value);
	    	              this.OnNomeUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuarioCopia");
	    	              this._NomeUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("NomeUsuarioCopia");
	    	              this.OnNomeUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_REGRA_COLUNA), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.REGRA_TRANSACAO", Source = "RegraTransacao", Target = "REGRA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.TRANSACAO_COLUNA", Source = "TransacaoColuna", Target = "TRANSACAO_COLUNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.LX_REGRA_ACESSO_COLUNA", Source = "LxRegraAcessoColuna", Target = "LX_REGRA_ACESSO_COLUNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA", Source = "IdUsuarioRegraColuna", Target = "ID_USUARIO_REGRA_COLUNA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_REGRA_COLUNA", RelationPropertyName = "TCS_USUARIO_REGRA_COLUNA" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoColunaValues()
	    {
	    	    return Linx.Framework.BV.Domains.RegraAcessoColuna.GetValues();
	    }
	    private string _lxRegraAcessoColunaName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoColunaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Acesso Coluna", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoColunaName
	    {
	    	    get { if (this.LxRegraAcessoColuna.IsNull()) { _lxRegraAcessoColunaName = String.Empty; } else { string key = this.LxRegraAcessoColuna.ToString(); var dmValues = this.GetLxRegraAcessoColunaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoColunaName) _lxRegraAcessoColunaName = domainName; } return _lxRegraAcessoColunaName; } set { _lxRegraAcessoColunaName = value;  }
	    }
	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Bandeira / Rede];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];Entities[TBC_BANDEIRA_REDE:IdBandeiraR];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_BANDEIRA_REDE_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_BANDEIRA_REDE];EntityRelations[TCS_USUARIO(TCS_USUARIO)#TBC_BANDEIRA_REDE(TBC_BANDEIRA_REDE)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioBandeiraRede")]
	[Serializable()]
	public partial class TcsUsuarioBandeiraRedeParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescBandeiraRede
	    partial void OnDescBandeiraRedeChanging(System.String value);
	    partial void OnDescBandeiraRedeChanged();

	    private System.String _DescBandeiraRede;

	    [DataMember(IsRequired = true, Name = "DescBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRede];LookUpTitle[Seleção de (Bandeira / Rede)];LookUpQuery[executeLookUpTbcBandeiraRede];LookUpFinalize[finalizeLookUpTbcBandeiraRede];LookUpDisplayColumns[{\"DescBandeiraRede\" : \"Bandeira / Rede\", \"IdBandeiraR\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"DescBandeiraRede\" : true, \"IdBandeiraR\" : false}];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescBandeiraRede#false##60:0##Bandeira / Rede#0#true##::LookUpTbcBandeiraRede##true#false#TBC_BANDEIRA_REDE#TBC_BANDEIRA_REDE#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE")]
	    public System.String DescBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _DescBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("DescBandeiraRede", value);
	    	              this.OnDescBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("DescBandeiraRede");
	    	              this._DescBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("DescBandeiraRede");
	    	              this.OnDescBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraR
	    partial void OnIdBandeiraRChanging(Int32 value);
	    partial void OnIdBandeiraRChanged();

	    private Int32 _IdBandeiraR;

	    [DataMember(IsRequired = true, Name = "IdBandeiraR", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRede];LookUpTitle[Seleção de (Bandeira / Rede)];LookUpQuery[executeLookUpTbcBandeiraRede];LookUpFinalize[finalizeLookUpTbcBandeiraRede];LookUpDisplayColumns[{\"DescBandeiraRede\" : \"Bandeira / Rede\", \"IdBandeiraR\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"DescBandeiraRede\" : true, \"IdBandeiraR\" : false}];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdBandeiraR#true##12:0##Id Bandeira Rede#1#false##::LookUpTbcBandeiraRede##true#false#TBC_BANDEIRA_REDE#TBC_BANDEIRA_REDE#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE")]
	    public Int32 IdBandeiraR
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraR;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraR != value)
	    	          {
	    	              this.ValidateProperty("IdBandeiraR", value);
	    	              this.OnIdBandeiraRChanging(value);
	    	              this.RaiseDataMemberChanging("IdBandeiraR");
	    	              this._IdBandeiraR = value;
	    	              this.RaiseDataMemberChanged("IdBandeiraR");
	    	              this.OnIdBandeiraRChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioCopia
	    partial void OnIdUsuarioCopiaChanging(Int64 value);
	    partial void OnIdUsuarioCopiaChanged();

	    private Int64 _IdUsuarioCopia;

	    [DataMember(Name = "IdUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="0")]
	    public Int64 IdUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioCopia", value);
	    	              this.OnIdUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioCopia");
	    	              this._IdUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioCopia");
	    	              this.OnIdUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro / Número", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município / UF", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuarioCopia
	    partial void OnNomeUsuarioCopiaChanging(System.String value);
	    partial void OnNomeUsuarioCopiaChanged();

	    private System.String _NomeUsuarioCopia;

	    [DataMember(Name = "NomeUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Cópia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public System.String NomeUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _NomeUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuarioCopia", value);
	    	              this.OnNomeUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuarioCopia");
	    	              this._NomeUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("NomeUsuarioCopia");
	    	              this.OnNomeUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_BANDEIRA_REDE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_BANDEIRA_REDE), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_BANDEIRA_REDE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_BANDEIRA_REDE.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "ID_USUARIO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE", Source = "IdBandeiraR", Target = "ID_BANDEIRA_REDE", TargetKeyName = "ID_BANDEIRA_REDE", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Layouts];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_LAYOUT_USUARIO_LISTA as #Alias#];EdmEntityName[TCS_LAYOUT_USUARIO];EntityRelations[TCS_LAYOUT(TCS_LAYOUT)#TCS_OBJETO_CONTEUDO(TCS_OBJETO_CONTEUDO)#TCS_LAYOUT_LISTA(TCS_LAYOUT)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioLayout")]
	[Serializable()]
	public partial class TcsUsuarioLayoutParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescLayout
	    partial void OnDescLayoutChanging(System.String value);
	    partial void OnDescLayoutChanged();

	    private System.String _DescLayout;

	    [DataMember(IsRequired = true, Name = "DescLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Layout", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayout];LookUpTitle[Seleção de (Layout)];LookUpQuery[executeLookUpTcsLayout];LookUpFinalize[finalizeLookUpTcsLayout];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Inativo\" : \"Inativo\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Inativo\" : true, \"IdObjetoConteudo\" : true}];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_LAYOUT.DESC_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescLayout#false##60:0##Desc Layout#0#true##::LookUpTcsLayout##false#false#TCS_LAYOUT#TCS_LAYOUT#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_LAYOUT_USUARIO.TCS_LAYOUT.DESC_LAYOUT")]
	    public System.String DescLayout
	    {
	    	    get
	    	    {
	    	          return _DescLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLayout != value)
	    	          {
	    	              this.ValidateProperty("DescLayout", value);
	    	              this.OnDescLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("DescLayout");
	    	              this._DescLayout = value;
	    	              this.RaiseDataMemberChanged("DescLayout");
	    	              this.OnDescLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Detalhes
	    partial void OnDetalhesChanging(System.String value);
	    partial void OnDetalhesChanged();

	    private System.String _Detalhes;

	    [DataMember(Name = "Detalhes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhes", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayout];LookUpTitle[Seleção de (Detalhes)];LookUpQuery[executeLookUpTcsLayout];LookUpFinalize[finalizeLookUpTcsLayout];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Inativo\" : \"Inativo\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Inativo\" : true, \"IdObjetoConteudo\" : true}];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_LAYOUT.DETALHES];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Detalhes#false##500:0##Detalhes#1#true##::LookUpTcsLayout##false#false#TCS_LAYOUT#TCS_LAYOUT#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_LAYOUT_USUARIO.TCS_LAYOUT.DETALHES")]
	    public System.String Detalhes
	    {
	    	    get
	    	    {
	    	          return _Detalhes;
	    	    }
	    	    set
	    	    {
	    	          if (this._Detalhes != value)
	    	          {
	    	              this.ValidateProperty("Detalhes", value);
	    	              this.OnDetalhesChanging(value);
	    	              this.RaiseDataMemberChanging("Detalhes");
	    	              this._Detalhes = value;
	    	              this.RaiseDataMemberChanged("Detalhes");
	    	              this.OnDetalhesChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjetoConteudo
	    partial void OnIdObjetoConteudoChanging(Int64 value);
	    partial void OnIdObjetoConteudoChanged();

	    private Int64 _IdObjetoConteudo;

	    [DataMember(IsRequired = true, Name = "IdObjetoConteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayout];LookUpTitle[Seleção de (Id Objeto Conteudo)];LookUpQuery[executeLookUpTcsLayout];LookUpFinalize[finalizeLookUpTcsLayout];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Inativo\" : \"Inativo\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Inativo\" : true, \"IdObjetoConteudo\" : true}];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_LAYOUT.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdObjetoConteudo#true##24:0##Id Objeto Conteudo#3#true##::LookUpTcsLayout##false#false#TCS_LAYOUT#TCS_LAYOUT#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_LAYOUT_USUARIO.TCS_LAYOUT.ID_OBJETO_CONTEUDO")]
	    public Int64 IdObjetoConteudo
	    {
	    	    get
	    	    {
	    	          return _IdObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjetoConteudo != value)
	    	          {
	    	              this.ValidateProperty("IdObjetoConteudo", value);
	    	              this.OnIdObjetoConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjetoConteudo");
	    	              this._IdObjetoConteudo = value;
	    	              this.RaiseDataMemberChanged("IdObjetoConteudo");
	    	              this.OnIdObjetoConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LAYOUT_USUARIO.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayout];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsLayout];LookUpFinalize[finalizeLookUpTcsLayout];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Inativo\" : \"Inativo\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Inativo\" : true, \"IdObjetoConteudo\" : true}];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_LAYOUT.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#Inativo#false##0:0##Inativo#2#true##::LookUpTcsLayout##false#false#TCS_LAYOUT#TCS_LAYOUT#Linx.Framework.BV.Usuario#IQueryable###true#true", EdmKey="TCS_LAYOUT_USUARIO.TCS_LAYOUT.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioCopia
	    partial void OnIdUsuarioCopiaChanging(Int64 value);
	    partial void OnIdUsuarioCopiaChanged();

	    private Int64 _IdUsuarioCopia;

	    [DataMember(Name = "IdUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="0")]
	    public Int64 IdUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioCopia", value);
	    	              this.OnIdUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioCopia");
	    	              this._IdUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioCopia");
	    	              this.OnIdUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro / Número", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município / UF", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuarioCopia
	    partial void OnNomeUsuarioCopiaChanging(System.String value);
	    partial void OnNomeUsuarioCopiaChanged();

	    private System.String _NomeUsuarioCopia;

	    [DataMember(Name = "NomeUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Cópia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public System.String NomeUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _NomeUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuarioCopia", value);
	    	              this.OnNomeUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuarioCopia");
	    	              this._NomeUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("NomeUsuarioCopia");
	    	              this.OnNomeUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_LAYOUT_USUARIO.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_LAYOUT_USUARIO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_LAYOUT_USUARIO), QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT_USUARIO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LAYOUT_USUARIO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "ID_USUARIO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LAYOUT_USUARIO.TCS_LAYOUT.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "ID_OBJETO_CONTEUDO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Filial];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioFilial];ReadOnly[false];Entities[TCS_USUARIO_FILIAL:IdTcsUsuarioFilial|TBC_FILIAL:IdFilialPfj];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_USUARIO_FILIAL];EntityRelations[TCS_USUARIO(TCS_USUARIO)#TBC_FILIAL(TBC_FILIAL)#MATRIZ_CONTABIL(TBC_FILIAL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TBC_PFJ(TBC_PFJ)#TBC_FILIAL_LISTA(TBC_FILIAL)];EdmParentEntityName[TCS_USUARIO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioFilial")]
	[Serializable()]
	public partial class TcsUsuarioFilialParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodigoFilial
	    partial void OnCodigoFilialChanging(System.String value);
	    partial void OnCodigoFilialChanged();

	    private System.String _CodigoFilial;

	    [DataMember(Name = "CodigoFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Código Filial)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_USUARIO_FILIAL.TBC_FILIAL.CODIGO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#CodigoFilial#false##18:0##Código Filial#0#true##::LookUpTbcFilial##false#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_FILIAL.TBC_FILIAL.CODIGO_FILIAL")]
	    public System.String CodigoFilial
	    {
	    	    get
	    	    {
	    	          return _CodigoFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoFilial != value)
	    	          {
	    	              this.ValidateProperty("CodigoFilial", value);
	    	              this.OnCodigoFilialChanging(value);
	    	              this.RaiseDataMemberChanging("CodigoFilial");
	    	              this._CodigoFilial = value;
	    	              this.RaiseDataMemberChanged("CodigoFilial");
	    	              this.OnCodigoFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFilialPfj
	    partial void OnIdFilialPfjChanging(Int32 value);
	    partial void OnIdFilialPfjChanged();

	    private Int32 _IdFilialPfj;

	    [DataMember(IsRequired = true, Name = "IdFilialPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Id Filial Pfj)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_USUARIO_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdFilialPfj#true##12:0##Id Filial Pfj#1#false##::LookUpTbcFilial##false#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ")]
	    public Int32 IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this.ValidateProperty("IdFilialPfj", value);
	    	              this.OnIdFilialPfjChanging(value);
	    	              this.RaiseDataMemberChanging("IdFilialPfj");
	    	              this._IdFilialPfj = value;
	    	              this.RaiseDataMemberChanged("IdFilialPfj");
	    	              this.OnIdFilialPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioFilial
	    partial void OnIdTcsUsuarioFilialChanging(Int64 value);
	    partial void OnIdTcsUsuarioFilialChanged();

	    private Int64 _IdTcsUsuarioFilial;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL")]
	    public Int64 IdTcsUsuarioFilial
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioFilial != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioFilial", value);
	    	              this.OnIdTcsUsuarioFilialChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioFilial");
	    	              this._IdTcsUsuarioFilial = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioFilial");
	    	              this.OnIdTcsUsuarioFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FILIAL.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeFilial
	    partial void OnNomeFilialChanging(System.String value);
	    partial void OnNomeFilialChanged();

	    private System.String _NomeFilial;

	    [DataMember(Name = "NomeFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Fantasia", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Nome Fantasia)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_USUARIO_FILIAL.TBC_FILIAL.NOME_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeFilial#false##60:0##Nome Fantasia#2#true##::LookUpTbcFilial##false#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.BV.Usuario#IQueryable###true#false", EdmKey="TCS_USUARIO_FILIAL.TBC_FILIAL.NOME_FILIAL")]
	    public System.String NomeFilial
	    {
	    	    get
	    	    {
	    	          return _NomeFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeFilial != value)
	    	          {
	    	              this.ValidateProperty("NomeFilial", value);
	    	              this.OnNomeFilialChanging(value);
	    	              this.RaiseDataMemberChanging("NomeFilial");
	    	              this._NomeFilial = value;
	    	              this.RaiseDataMemberChanged("NomeFilial");
	    	              this.OnNomeFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioCopia
	    partial void OnIdUsuarioCopiaChanging(Int64 value);
	    partial void OnIdUsuarioCopiaChanged();

	    private Int64 _IdUsuarioCopia;

	    [DataMember(Name = "IdUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="0")]
	    public Int64 IdUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioCopia", value);
	    	              this.OnIdUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioCopia");
	    	              this._IdUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioCopia");
	    	              this.OnIdUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro / Número", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município / UF", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuarioCopia
	    partial void OnNomeUsuarioCopiaChanging(System.String value);
	    partial void OnNomeUsuarioCopiaChanged();

	    private System.String _NomeUsuarioCopia;

	    [DataMember(Name = "NomeUsuarioCopia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Cópia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public System.String NomeUsuarioCopia
	    {
	    	    get
	    	    {
	    	          return _NomeUsuarioCopia;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuarioCopia != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuarioCopia", value);
	    	              this.OnNomeUsuarioCopiaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuarioCopia");
	    	              this._NomeUsuarioCopia = value;
	    	              this.RaiseDataMemberChanged("NomeUsuarioCopia");
	    	              this.OnNomeUsuarioCopiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_FILIAL.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_FILIAL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_FILIAL), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FILIAL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL", Source = "IdTcsUsuarioFilial", Target = "ID_TCS_USUARIO_FILIAL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FILIAL", RelationPropertyName = "TCS_USUARIO_FILIAL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FILIAL.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ", Source = "IdFilialPfj", Target = "ID_FILIAL_PFJ", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_FILIAL", RelationPropertyName = "TBC_FILIAL" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewUsuarioDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class UsuarioDomainService : DomainService, IDataServiceContext 
	{
	
	
	    private bool[] _trueMetaCondition = new bool[] { true };
	    private bool[] _falseMetaCondition = new bool[] { };
	    partial void OnCreate();
	    private bool _isInitialized;
	    private bool _controlKeyMapping = false;
	    private List<DataKeyMapping> _keyMappings = new List<DataKeyMapping>();
	    private string connectionString;
	    public bool IsSecure { get; set; }
	    public Dictionary<string, string> Headers { get; set; }
	
	    #region SecurityHelper
	    private static ISecurityHelper _securityHelper;
	    [Ignore]
        private static ISecurityHelper SecurityHelper
        {
            get
            {
                if (_securityHelper == null)
                {
                    try { _securityHelper = ImplementationHelper<ISecurityHelper>.GetInstance("SecurityHelper", "Linx.Business.Tools"); }
                    catch { }
                }
                return _securityHelper;
            }
        }
	    #endregion

	
	    private bool _hasGpeconControl;
	    public bool HasGpeconControl { get { return _hasGpeconControl; } }
	
	    private Linx.Framework.ControleSistema.BM.ControleSistemaContext _dbContext;
	    protected Linx.Framework.ControleSistema.BM.ControleSistemaContext DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Framework.ControleSistema.BM.ControleSistemaContext(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Framework.ControleSistema.BM.ControleSistemaContext).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public UsuarioDomainService() : this("", null, null) { }
	    public UsuarioDomainService(string connectionString) : this(connectionString, null, null) { }
	    public UsuarioDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public UsuarioDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public UsuarioDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
	    { 
	    	this.connectionString = connectionString;
	    	this.Headers = headers;
	    	this._dbContext = dataContext; 


	    	this.OnCreate(); 
	    }

	    [Ignore]
	    public List<DataKeyMapping> SaveEntities(List<ChangeSetEntry> changeSetEntries)
	    {
	      return SaveEntities(changeSetEntries, true);
	    }

	    [Ignore]
	    public List<DataKeyMapping> SaveEntities(List<ChangeSetEntry> changeSetEntries, bool ctrlKeyMapping)
	    {
	      if (changeSetEntries.Count == 0) return null;
	      
	      this.Initialize();
	      _keyMappings.Clear();
	      _controlKeyMapping = ctrlKeyMapping;
	      this.Submit(new ChangeSet(changeSetEntries));
	      _controlKeyMapping = false;
	      return _keyMappings;
	    }

	    protected override int Count<T>(IQueryable<T> query)
	    {
	       return query.Count<T>();
	    }

	    public override void Initialize(DomainServiceContext context)
	    {
	       if (!_isInitialized)
	       {
	    		base.Initialize(context);
	    		this.AuthorizationContext = this.CreateAuthorizationContext();
	    		((System.Data.Entity.Infrastructure.IObjectContextAdapter)(object)this.DbContext).ObjectContext.ContextOptions.ProxyCreationEnabled = false;
	    		_isInitialized = true;
	       }
	    }
	
	    ChangeSet currentChangeSet = null;
	    [Ignore]
	    public ChangeSet GetChangeSet()
        {
          return this.currentChangeSet;
        }

	
	    [Ignore]
	    protected bool InvokeSaveChanges()
	    {
          try
          {
          	if (this._dbContext != null)
          		this._dbContext.SaveChanges();                
          }
          catch (Exception exp)
          {
          	throw new DomainException(exp.GetCompleteMessage("Fail by saving data:"));
          }
          return true;
	    }	

	    protected override void Dispose(bool disposing)
	    {
	      if (disposing)
	      {
	    		if (this._dbContext != null)
	    		{
	    			this._dbContext.Dispose();
	    		}
	      }
	      base.Dispose(disposing);
	    }

	    [Ignore]
	    public Linx.Framework.ControleSistema.BM.ControleSistemaContext GetEDM()
        {
          return this.DbContext;
        }	

			
	    [Ignore]	
	    public void AddCustomChanges(Entity changedEntity, Entity originalEntity, ChangeOperation operation)
	    {
	
 	        changedEntity.ApplyChanges(this.DbContext, originalEntity, operation, null);
	    }	
	
	    private int CurrentIdLinx(string connection)
        {
	        if(SecurityHelper.IsNull()) return 0;
            var idLinx = SecurityHelper.GetCurrentIdLinx(connection, this.Headers);
            return idLinx ?? 0;
        }
        private int CurrentIdGpEcon()
        {
	        if(SecurityHelper.IsNull()) return 0;
            var idGpEcon = SecurityHelper.GetCurrentIdGpecon(this.Headers);
            return idGpEcon ?? 0;
        }
	    private int[] CurrentIdFiliais()
        {
	        if(SecurityHelper.IsNull()) return new int[0] ;
            var idFiliais = SecurityHelper.GetCurrentUserBrandInfo(this.Headers);
            return idFiliais ?? new int[0] ;
        }
	
	    [Ignore]	
	    public void SubmitData(DomainServiceContext context, Entity changedEntity, Entity originalEntity, ChangeOperation operation)
	    {
          var changeSetEntries = new ChangeSetEntry[] { new ChangeSetEntry(0, changedEntity, originalEntity, (DomainOperation)Enum.Parse(typeof(DomainOperation), operation.ToString())) { HasMemberChanges = true } };
          if (context == null) this.Initialize(); else this.Initialize(context);
          this.Submit(new ChangeSet(changeSetEntries));
	    }	

	    [Ignore]
	    public void SubmitData(DomainServiceContext context, List<EntityChange> entityChanges)
	    {
          if (entityChanges.Count == 0) return;
          List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
          for (int changeIndex = 0; changeIndex < entityChanges.Count; changeIndex++)
          {
              changeSetEntries.Add( new ChangeSetEntry(changeIndex, entityChanges[changeIndex].Entity, entityChanges[changeIndex].Original, (DomainOperation)Enum.Parse(typeof(DomainOperation), entityChanges[changeIndex].Operation.ToString())) { HasMemberChanges = true } );
          }
          if (context == null) this.Initialize(); else this.Initialize(context);
          this.Submit(new ChangeSet(changeSetEntries));
	    }
	
	    [Ignore]
	    public void SaveCustomChanges()
	    {
	        this.InvokeSaveChanges();
	    }		

	    #region Workflow Invoke Definitions
		


	    #endregion Workflow Invoke Definitions
	
	    #region KPI Informations
		


	    #endregion KPI Informations

	    #region Entity Event Call Definitions
	
	    private bool OnValidatingChanges(ChangeSet changeSet)
	    {
	
	
	        return true;
	    }

	    private void OnSavingChanges(ChangeSet changeSet)
	    {
	
		
	    }
	
	    private void SaveMedia(ChangeSet changeSet)
	    {
	    		foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries)
	    		{
	    		}
	    }

	    private void OnSavedChanges(ChangeSet changeSet)
	    {
	
	
	        TcsUsuario.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuario).ToArray());
    
	        TcsUsuarioPerfil.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioPerfil).ToArray());
    
	        TcsUsuarioRegraModulo.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioRegraModulo).ToArray());
    
	        TcsUsuarioRegraTransacao.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioRegraTransacao).ToArray());
    
	        TcsUsuarioBandeiraRede.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioBandeiraRede).ToArray());
    
	        TcsUsuarioFilial.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioFilial).ToArray());
    	
	    }
		
	    private void OnTransactingChanges(ChangeSet changeSet)
	    {
	
		
	    }
	
	    private void OnTransactedChanges(ChangeSet changeSet)
	    {
	
		
	    }
		
	    #endregion Entity Event Call Definitions
	
	    #region Transaction Control.
	
	    TransactionScope transactionScope = null;	
	
	    //Adjust Hierarchy Composition
	    private ChangeSet AdjustHierarchyForSaving(ChangeSet changeSet)
	    {

		
 
 	        bool createNewChangeSet = false;
 
 	        //Adjust data hierarchy
 	        var _TcsUsuarioElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuario && e.Entity.GetType().Name == "TcsUsuario" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsUsuarioElements)
 	           if (((TcsUsuario)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioPerfil && e.Entity.GetType().Name == "TcsUsuarioPerfil" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioRegraModulo && e.Entity.GetType().Name == "TcsUsuarioRegraModulo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioRegraTransacao && e.Entity.GetType().Name == "TcsUsuarioRegraTransacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioRegraColuna && e.Entity.GetType().Name == "TcsUsuarioRegraColuna" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioBandeiraRede && e.Entity.GetType().Name == "TcsUsuarioBandeiraRede" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioLayout && e.Entity.GetType().Name == "TcsUsuarioLayout" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioFilial && e.Entity.GetType().Name == "TcsUsuarioFilial" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 
 	        if (createNewChangeSet) changeSet = new ChangeSet(changeSet.ChangeSetEntries.Where(e => e.Operation != DomainOperation.None));
 	        return changeSet;
 	

	    }


	
	    //Transactions control
	    public override bool Submit(ChangeSet changeSet)
	    {
	        bool result = false;
	        try
	        {
	            currentChangeSet = changeSet = AdjustHierarchyForSaving(changeSet);
	            if (!OnValidatingChanges(changeSet)) return false;

	            Dictionary<object, object> oldKeys = new Dictionary<object, object>();
	            //Get temporary keys.
	            if (_controlKeyMapping)
	            {
	                foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries)
	                {	
	                    var keys = ObjectExtension.GetKeyProperties(entry.Entity.GetType());
	                    if (keys.Count == 0) keys.Add("EntityUniqueKey");
	                    string tempKey = String.Join(":::", keys.Select(p => entry.Entity.GetPropertyValue(p)));
	                    if (!tempKey.IsNullOrEmpty())
	                        oldKeys.Add(entry.Entity, tempKey);
	                }
	            }

	            OnSavingChanges(changeSet);
	            result = base.Submit(changeSet);
	            if (!changeSet.HasError)
	            {	
	                

	                //Refresh real keys.
	                foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries)
	                {	
	                    if (entry.Entity is Entity && changeSet.GetChangeOperation(entry.Entity) == ChangeOperation.Insert)
	                    	 ((Entity)entry.Entity).RefreshKeys();
	                
	                    if (_controlKeyMapping && oldKeys.ContainsKey(entry.Entity))
	                    {
	                		   var entityType = entry.Entity.GetType();
	                        var keys = ObjectExtension.GetKeyProperties(entityType);
	                        if (keys.Count == 0) keys.Add("EntityUniqueKey");
	                        string newKey = String.Join(":::", keys.Select(p => entry.Entity.GetPropertyValue(p)));
	                        if (!newKey.IsNullOrEmpty())
	                        {
	                            _keyMappings.Add(new DataKeyMapping
	                           {
	                               EntityTypeName = entityType.FullName,
	                               RealValue = (changeSet.GetChangeOperation(entry.Entity) == ChangeOperation.Delete ? null : newKey),
	                               TempValue = (changeSet.GetChangeOperation(entry.Entity) == ChangeOperation.Insert ? oldKeys[entry.Entity] : newKey)
	                           });
	                        }
	                    }

	                }	

	                OnTransactedChanges(changeSet);
	                if (!transactionScope.IsNull()) transactionScope.Complete();	
	            }
	        }
	        catch (Exception exp)
	        {
	            throw new DomainException(exp.Message, exp.InnerException);
	        }
	        finally
	        {
	            if (!transactionScope.IsNull())
	            {
	                transactionScope.Dispose();
	                transactionScope = null;
	            }
	        }
	    
	        OnSavedChanges(changeSet);
	        SaveMedia(changeSet);
	        return result;
	    }

	
	    protected override bool PersistChangeSet()
	    {
	        transactionScope = (this.GetEDM().ProviderName == "SQLite" ? null : new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }));
	        OnTransactingChanges(this.ChangeSet);
	        bool result = this.InvokeSaveChanges();
	        
	        return result;
	    }
	
	    #endregion Transaction Control.
		


	    #region Get OLAP Definitions.
	
			
	
	    #endregion Get OLAP Definitions.


	    #region Get LookUp Definitions.
	
		
			
        [Ignore]
	    //Get All LookUpTcsUsuario.
	    public IQueryable<LookUpTcsUsuario> GetAllLookUpTcsUsuario()
	    {
	        return this.GetLookUpTcsUsuario(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuario By EntitySearch.
	    public IQueryable<LookUpTcsUsuario> GetLookUpTcsUsuarioByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuario(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuario.
	    public IQueryable<LookUpTcsUsuario> GetLookUpTcsUsuario(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_USUARIO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuario";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuario));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuario> query =  
	
	            (from entity in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsUsuario()		
	            {
	            
                NomeUsuarioCopia = entity.NOME_USUARIO
                , IdUsuarioCopia = entity.ID_USUARIO
                , UidUsuario = entity.UID_USUARIO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsPerfil.
	    public IQueryable<LookUpTcsPerfil> GetAllLookUpTcsPerfil()
	    {
	        return this.GetLookUpTcsPerfil(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsPerfil By EntitySearch.
	    public IQueryable<LookUpTcsPerfil> GetLookUpTcsPerfilByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsPerfil(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsPerfil.
	    public IQueryable<LookUpTcsPerfil> GetLookUpTcsPerfil(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_PERFIL" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsPerfil";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsPerfil));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsPerfil> query =  
	
	            (from entity in this.DbContext.TCS_PERFIL.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsPerfil()		
	            {
	            
                DescPerfil = entity.DESC_PERFIL
                , IdPerfil = entity.ID_PERFIL
                , Inativo = entity.INATIVO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsUsuarioRegraModulo.
	    public IQueryable<LookUpTcsUsuarioRegraModulo> GetAllLookUpTcsUsuarioRegraModulo()
	    {
	        return this.GetLookUpTcsUsuarioRegraModulo(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuarioRegraModulo By EntitySearch.
	    public IQueryable<LookUpTcsUsuarioRegraModulo> GetLookUpTcsUsuarioRegraModuloByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuarioRegraModulo(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuarioRegraModulo.
	    public IQueryable<LookUpTcsUsuarioRegraModulo> GetLookUpTcsUsuarioRegraModulo(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuarioRegraModulo";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuarioRegraModulo));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuarioRegraModulo> query =  null;
		
			
		
	        TcsUsuarioRegraModulo.OnLookUpingLookUpTcsUsuarioRegraModulo(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsUsuarioRegraTransacao.
	    public IQueryable<LookUpTcsUsuarioRegraTransacao> GetAllLookUpTcsUsuarioRegraTransacao()
	    {
	        return this.GetLookUpTcsUsuarioRegraTransacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuarioRegraTransacao By EntitySearch.
	    public IQueryable<LookUpTcsUsuarioRegraTransacao> GetLookUpTcsUsuarioRegraTransacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuarioRegraTransacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuarioRegraTransacao.
	    public IQueryable<LookUpTcsUsuarioRegraTransacao> GetLookUpTcsUsuarioRegraTransacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuarioRegraTransacao";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuarioRegraTransacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuarioRegraTransacao> query =  null;
		
			
		
	        TcsUsuarioRegraTransacao.OnLookUpingLookUpTcsUsuarioRegraTransacao(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsUsuarioRegraColuna.
	    public IQueryable<LookUpTcsUsuarioRegraColuna> GetAllLookUpTcsUsuarioRegraColuna()
	    {
	        return this.GetLookUpTcsUsuarioRegraColuna(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuarioRegraColuna By EntitySearch.
	    public IQueryable<LookUpTcsUsuarioRegraColuna> GetLookUpTcsUsuarioRegraColunaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuarioRegraColuna(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuarioRegraColuna.
	    public IQueryable<LookUpTcsUsuarioRegraColuna> GetLookUpTcsUsuarioRegraColuna(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuarioRegraColuna";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuarioRegraColuna));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuarioRegraColuna> query =  null;
		
			
		
	        TcsUsuarioRegraColuna.OnLookUpingLookUpTcsUsuarioRegraColuna(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTbcBandeiraRede.
	    public IQueryable<LookUpTbcBandeiraRede> GetAllLookUpTbcBandeiraRede()
	    {
	        return this.GetLookUpTbcBandeiraRede(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTbcBandeiraRede By EntitySearch.
	    public IQueryable<LookUpTbcBandeiraRede> GetLookUpTbcBandeiraRedeByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTbcBandeiraRede(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTbcBandeiraRede.
	    public IQueryable<LookUpTbcBandeiraRede> GetLookUpTbcBandeiraRede(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TBC_BANDEIRA_REDE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTbcBandeiraRede";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTbcBandeiraRede));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTbcBandeiraRede> query =  
	
	            (from entity in this.DbContext.TBC_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTbcBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity.ID_BANDEIRA_REDE
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsLayout.
	    public IQueryable<LookUpTcsLayout> GetAllLookUpTcsLayout()
	    {
	        return this.GetLookUpTcsLayout(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsLayout By EntitySearch.
	    public IQueryable<LookUpTcsLayout> GetLookUpTcsLayoutByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsLayout(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsLayout.
	    public IQueryable<LookUpTcsLayout> GetLookUpTcsLayout(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_LAYOUT" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsLayout";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsLayout));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsLayout> query =  
	
	            (from entity in this.DbContext.TCS_LAYOUT.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsLayout()		
	            {
	            
                DescLayout = entity.DESC_LAYOUT
                , Detalhes = entity.DETALHES
                , Inativo = entity.INATIVO
                , IdObjetoConteudo = entity.ID_OBJETO_CONTEUDO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTbcFilial.
	    public IQueryable<LookUpTbcFilial> GetAllLookUpTbcFilial()
	    {
	        return this.GetLookUpTbcFilial(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTbcFilial By EntitySearch.
	    public IQueryable<LookUpTbcFilial> GetLookUpTbcFilialByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTbcFilial(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTbcFilial.
	    public IQueryable<LookUpTbcFilial> GetLookUpTbcFilial(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TBC_FILIAL" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTbcFilial";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTbcFilial));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTbcFilial> query =  
	
	            (from entity in this.DbContext.TBC_FILIAL.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTbcFilial()		
	            {
	            
                CodigoFilial = entity.CODIGO_FILIAL
                , IdFilialPfj = entity.ID_FILIAL_PFJ
                , NomeFilial = entity.NOME_FILIAL
	            });

	            
	
		
	
	
	        return query;

	    }
			
	    #endregion Get LookUp Definitions.
			

	    #region Get Meta Data.

	    [Ignore]
	    public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
	    {
		        return this.GetEDM().GetBmEntityProperties(entityName, parentDataPath);
		    }
	
	    [Ignore]
	    //Get Meta Data.
	    public string GetMetaData(string entityName, bool forceAll = false)
        {
	        return SerializationManager<List<LinxEntityReferenceInfo>>.ObjectToString(GetMetaDataObject(entityName, forceAll));
	    }

	    [Ignore]
	    public List<LinxEntityReferenceInfo> GetMetaDataObject(string entityName, bool forceAll = false, bool removeParentComposition = false)
        {
            List<LinxEntityReferenceInfo> result = new List<LinxEntityReferenceInfo>();
	
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuario"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuario",
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuario",
	        			ClearMethodName = "ClearTcsUsuario",
	        			QueryMethodName  = "GetPagedTcsUsuario",	
	        			CountingMethodName  = "GetTcsUsuario" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuario"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuario"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuario", "Linx.Framework.BV.Usuario.TcsUsuarioPerfil"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuario",	
	        			DisplayName = "Perfil",
	        			ClearMethodName = "ClearTcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioPerfil"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuario", "Linx.Framework.BV.Usuario.TcsUsuarioRegraModulo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioRegraModulo" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuario",	
	        			DisplayName = "Módulo",
	        			ClearMethodName = "ClearTcsUsuarioRegraModulo" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioRegraModulo" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioRegraModulo" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioRegraModulo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioRegraModulo" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuario", "Linx.Framework.BV.Usuario.TcsUsuarioRegraTransacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioRegraTransacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuario",	
	        			DisplayName = "Transação",
	        			ClearMethodName = "ClearTcsUsuarioRegraTransacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioRegraTransacao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioRegraTransacao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioRegraTransacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioRegraTransacao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuario", "Linx.Framework.BV.Usuario.TcsUsuarioRegraColuna"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioRegraColuna" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuario",	
	        			DisplayName = "Coluna",
	        			ClearMethodName = "ClearTcsUsuarioRegraColuna" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioRegraColuna" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioRegraColuna" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioRegraColuna"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioRegraColuna" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuario", "Linx.Framework.BV.Usuario.TcsUsuarioBandeiraRede"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioBandeiraRede" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuario",	
	        			DisplayName = "Bandeira / Rede",
	        			ClearMethodName = "ClearTcsUsuarioBandeiraRede" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioBandeiraRede" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioBandeiraRede" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioBandeiraRede"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioBandeiraRede" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuario", "Linx.Framework.BV.Usuario.TcsUsuarioLayout"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioLayout" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuario",	
	        			DisplayName = "Layouts",
	        			ClearMethodName = "ClearTcsUsuarioLayout" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioLayout" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioLayout" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioLayout"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioLayout" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuario", "Linx.Framework.BV.Usuario.TcsUsuarioFilial"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioFilial" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuario",	
	        			DisplayName = "Filial",
	        			ClearMethodName = "ClearTcsUsuarioFilial" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioFilial" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioFilial" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioFilial"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioFilial" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuarioAcessoLocal"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAcessoLocal",
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioAcessoLocal",
	        			ClearMethodName = "ClearTcsUsuarioAcessoLocal",
	        			QueryMethodName  = "GetPagedTcsUsuarioAcessoLocal",	
	        			CountingMethodName  = "GetTcsUsuarioAcessoLocal" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioAcessoLocal"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioAcessoLocal"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Usuario.TcsUsuarioPerfilP"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioPerfilP",
	        			NameSpace = "Linx.Framework.BV.Usuario",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioPerfilP",
	        			ClearMethodName = "ClearTcsUsuarioPerfilP",
	        			QueryMethodName  = "GetPagedTcsUsuarioPerfilP",	
	        			CountingMethodName  = "GetTcsUsuarioPerfilP" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioPerfilP"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Usuario.TcsUsuarioPerfilP"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "Framework_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "Framework_UsuarioClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.UsuarioClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_usuarioService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.usuarioService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientFactory(string entityName, bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { };	
	    		}
	    		else 
	    		{

         		    return new string[] { };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientFactoryCustomEvents(string entityName, bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { };	
	    		}
	    		else 
	    		{

         		    return new string[] { };	
	    		}

        }
	
	    #endregion Get Meta Data.
	
	    #region Clear Methods Definitions.
	
		
	
	    [Ignore]
	    //Clear TcsUsuario.
	    public IEnumerable<TcsUsuario> ClearTcsUsuario()
	    {
	        List<TcsUsuario> result = new List<TcsUsuario>();
	        result.Add(new TcsUsuario());	
			
	        result[0].TcsUsuarioPerfilList = new List<TcsUsuarioPerfil>();
	        ((List<TcsUsuarioPerfil>)result[0].TcsUsuarioPerfilList).Add(new TcsUsuarioPerfil());
			
	        result[0].TcsUsuarioRegraModuloList = new List<TcsUsuarioRegraModulo>();
	        ((List<TcsUsuarioRegraModulo>)result[0].TcsUsuarioRegraModuloList).Add(new TcsUsuarioRegraModulo());
			
	        result[0].TcsUsuarioRegraTransacaoList = new List<TcsUsuarioRegraTransacao>();
	        ((List<TcsUsuarioRegraTransacao>)result[0].TcsUsuarioRegraTransacaoList).Add(new TcsUsuarioRegraTransacao());
			
	        result[0].TcsUsuarioRegraColunaList = new List<TcsUsuarioRegraColuna>();
	        ((List<TcsUsuarioRegraColuna>)result[0].TcsUsuarioRegraColunaList).Add(new TcsUsuarioRegraColuna());
			
	        result[0].TcsUsuarioBandeiraRedeList = new List<TcsUsuarioBandeiraRede>();
	        ((List<TcsUsuarioBandeiraRede>)result[0].TcsUsuarioBandeiraRedeList).Add(new TcsUsuarioBandeiraRede());
			
	        result[0].TcsUsuarioLayoutList = new List<TcsUsuarioLayout>();
	        ((List<TcsUsuarioLayout>)result[0].TcsUsuarioLayoutList).Add(new TcsUsuarioLayout());
			
	        result[0].TcsUsuarioFilialList = new List<TcsUsuarioFilial>();
	        ((List<TcsUsuarioFilial>)result[0].TcsUsuarioFilialList).Add(new TcsUsuarioFilial());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioPerfil.
	    public IEnumerable<TcsUsuarioPerfil> ClearTcsUsuarioPerfil()
	    {
	        List<TcsUsuarioPerfil> result = new List<TcsUsuarioPerfil>();
	        result.Add(new TcsUsuarioPerfil());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioRegraModulo.
	    public IEnumerable<TcsUsuarioRegraModulo> ClearTcsUsuarioRegraModulo()
	    {
	        List<TcsUsuarioRegraModulo> result = new List<TcsUsuarioRegraModulo>();
	        result.Add(new TcsUsuarioRegraModulo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioRegraTransacao.
	    public IEnumerable<TcsUsuarioRegraTransacao> ClearTcsUsuarioRegraTransacao()
	    {
	        List<TcsUsuarioRegraTransacao> result = new List<TcsUsuarioRegraTransacao>();
	        result.Add(new TcsUsuarioRegraTransacao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioRegraColuna.
	    public IEnumerable<TcsUsuarioRegraColuna> ClearTcsUsuarioRegraColuna()
	    {
	        List<TcsUsuarioRegraColuna> result = new List<TcsUsuarioRegraColuna>();
	        result.Add(new TcsUsuarioRegraColuna());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioBandeiraRede.
	    public IEnumerable<TcsUsuarioBandeiraRede> ClearTcsUsuarioBandeiraRede()
	    {
	        List<TcsUsuarioBandeiraRede> result = new List<TcsUsuarioBandeiraRede>();
	        result.Add(new TcsUsuarioBandeiraRede());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioLayout.
	    public IEnumerable<TcsUsuarioLayout> ClearTcsUsuarioLayout()
	    {
	        List<TcsUsuarioLayout> result = new List<TcsUsuarioLayout>();
	        result.Add(new TcsUsuarioLayout());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioFilial.
	    public IEnumerable<TcsUsuarioFilial> ClearTcsUsuarioFilial()
	    {
	        List<TcsUsuarioFilial> result = new List<TcsUsuarioFilial>();
	        result.Add(new TcsUsuarioFilial());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAcessoLocal.
	    public IEnumerable<TcsUsuarioAcessoLocal> ClearTcsUsuarioAcessoLocal()
	    {
	        List<TcsUsuarioAcessoLocal> result = new List<TcsUsuarioAcessoLocal>();
	        result.Add(new TcsUsuarioAcessoLocal());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioPerfilP.
	    public IEnumerable<TcsUsuarioPerfilP> ClearTcsUsuarioPerfilP()
	    {
	        List<TcsUsuarioPerfilP> result = new List<TcsUsuarioPerfilP>();
	        result.Add(new TcsUsuarioPerfilP());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuario.
	    public IQueryable<TcsUsuario> GetTcsUsuario()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeUsuario = entity0.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
			
                ,TcsUsuarioPerfilList = 
	                        (from entity1 in entity0.TCS_USUARIO_PERFIL_LISTA
                                  let entity1Al1 = entity1.TCS_PERFIL
                                  let entity1Al2 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioPerfil()
	                        {
	                        
                                DescPerfil = entity1Al1.DESC_PERFIL
                                , IdPerfil = entity1Al1.ID_PERFIL
                                , IdTcsUsuarioPerfil = entity1.ID_TCS_USUARIO_PERFIL
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , Inativo = entity1Al1.INATIVO
		
	                        }
	                        )
			
                ,TcsUsuarioRegraModuloList = 
	                        (from entity1 in entity0.TCS_USUARIO_REGRA_MODULO_LISTA
                                  let entity1Al1 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioRegraModulo()
	                        {
	                        
                                IdModulo = entity1.ID_MODULO
                                , IdUsuario = entity1Al1.ID_USUARIO
                                , IdUsuarioRegraModulo = entity1.ID_USUARIO_REGRA_MODULO
                                , LxRegraAcessoModulo = entity1.LX_REGRA_ACESSO_MODULO
                                , LxRegraAcessoModuloName = ((entity1.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                                , RegraTransacao = entity1.REGRA_TRANSACAO
		
	                        }
	                        )
			
                ,TcsUsuarioRegraTransacaoList = 
	                        (from entity1 in entity0.TCS_USUARIO_REGRA_TRANSACAO_LISTA
                                  let entity1Al1 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioRegraTransacao()
	                        {
	                        
                                IdTransacao = entity1.ID_TRANSACAO
                                , IdUsuario = entity1Al1.ID_USUARIO
                                , IdUsuarioRegraTransacao = entity1.ID_USUARIO_REGRA_TRANSACAO
                                , LxRegraAcessoTransacao = entity1.LX_REGRA_ACESSO_TRANSACAO
                                , LxRegraAcessoTransacaoName = ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                                , RegraTransacao = entity1.REGRA_TRANSACAO
                                , UidUsuario = entity1Al1.UID_USUARIO
                                , ClasseNome = ""
		
	                        }
	                        )
			
                ,TcsUsuarioRegraColunaList = 
	                        (from entity1 in entity0.TCS_USUARIO_REGRA_COLUNA_LISTA
                                  let entity1Al1 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioRegraColuna()
	                        {
	                        
                                IdTransacao = entity1.ID_TRANSACAO
                                , IdUsuario = entity1Al1.ID_USUARIO
                                , IdUsuarioRegraColuna = entity1.ID_USUARIO_REGRA_COLUNA
                                , LxRegraAcessoColuna = entity1.LX_REGRA_ACESSO_COLUNA
                                , LxRegraAcessoColunaName = ((entity1.LX_REGRA_ACESSO_COLUNA) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 4 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 5 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 99 ? "Regra Transação" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 3 ? "Visualizar" : ""))))))
                                , RegraTransacao = entity1.REGRA_TRANSACAO
                                , TransacaoColuna = entity1.TRANSACAO_COLUNA
                                , ClasseNome = ""
		
	                        }
	                        )
			
                ,TcsUsuarioBandeiraRedeList = 
	                        (from entity1 in entity0.TCS_USUARIO_BANDEIRA_REDE_LISTA
                                  let entity1Al2 = entity1.TCS_USUARIO
                                  let entity1Al1 = entity1.TBC_BANDEIRA_REDE
	                        
	                        	
	                        select new TcsUsuarioBandeiraRede()
	                        {
	                        
                                DescBandeiraRede = entity1Al1.DESC_BANDEIRA_REDE
                                , IdBandeiraR = entity1Al1.ID_BANDEIRA_REDE
                                , IdUsuario = entity1Al2.ID_USUARIO
		
	                        }
	                        )
			
                ,TcsUsuarioLayoutList = 
	                        (from entity1 in entity0.TCS_LAYOUT_USUARIO_LISTA
                                  let entity1Al1 = entity1.TCS_LAYOUT
                                  let entity1Al2 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioLayout()
	                        {
	                        
                                DescLayout = entity1Al1.DESC_LAYOUT
                                , Detalhes = entity1Al1.DETALHES
                                , IdObjetoConteudo = entity1Al1.ID_OBJETO_CONTEUDO
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , Inativo = entity1Al1.INATIVO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        TcsUsuario.OnSearching(ref result, false, null);	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioPerfil.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfil()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioRegraModulo.
	    public IQueryable<TcsUsuarioRegraModulo> GetTcsUsuarioRegraModulo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_MODULO
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraModulo = entity0.ID_USUARIO_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioRegraTransacao.
	    public IQueryable<TcsUsuarioRegraTransacao> GetTcsUsuarioRegraTransacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraTransacao()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraTransacao = entity0.ID_USUARIO_REGRA_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , UidUsuario = entity0Al1.UID_USUARIO
                , ClasseNome = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioRegraColuna.
	    public IQueryable<TcsUsuarioRegraColuna> GetTcsUsuarioRegraColuna()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraColuna> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_COLUNA
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraColuna()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraColuna = entity0.ID_USUARIO_REGRA_COLUNA
                , LxRegraAcessoColuna = entity0.LX_REGRA_ACESSO_COLUNA
                , LxRegraAcessoColunaName = ((entity0.LX_REGRA_ACESSO_COLUNA) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 4 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 5 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 99 ? "Regra Transação" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 3 ? "Visualizar" : ""))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , TransacaoColuna = entity0.TRANSACAO_COLUNA
                , ClasseNome = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioBandeiraRede.
	    public IQueryable<TcsUsuarioBandeiraRede> GetTcsUsuarioBandeiraRede()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_BANDEIRA_REDE
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsUsuarioBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioLayout.
	    public IQueryable<TcsUsuarioLayout> GetTcsUsuarioLayout()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioLayout> result = 
	            (from entity0 in this.DbContext.TCS_LAYOUT_USUARIO
                  let entity0Al1 = entity0.TCS_LAYOUT
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioLayout()		
	            {
	            
                DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioFilial.
	    public IQueryable<TcsUsuarioFilial> GetTcsUsuarioFilial()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFilial> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FILIAL
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdTcsUsuarioFilial = entity0.ID_TCS_USUARIO_FILIAL
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioNoAssociations.
	    public IQueryable<TcsUsuario> GetTcsUsuarioNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeUsuario = entity0.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            );
		
	
	        TcsUsuario.OnSearching(ref result, true, null);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilNoAssociations.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraModuloNoAssociations.
	    public IQueryable<TcsUsuarioRegraModulo> GetTcsUsuarioRegraModuloNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_MODULO
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraModulo = entity0.ID_USUARIO_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraTransacaoNoAssociations.
	    public IQueryable<TcsUsuarioRegraTransacao> GetTcsUsuarioRegraTransacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraTransacao()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraTransacao = entity0.ID_USUARIO_REGRA_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , UidUsuario = entity0Al1.UID_USUARIO
                , ClasseNome = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraColunaNoAssociations.
	    public IQueryable<TcsUsuarioRegraColuna> GetTcsUsuarioRegraColunaNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraColuna> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_COLUNA
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraColuna()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraColuna = entity0.ID_USUARIO_REGRA_COLUNA
                , LxRegraAcessoColuna = entity0.LX_REGRA_ACESSO_COLUNA
                , LxRegraAcessoColunaName = ((entity0.LX_REGRA_ACESSO_COLUNA) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 4 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 5 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 99 ? "Regra Transação" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 3 ? "Visualizar" : ""))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , TransacaoColuna = entity0.TRANSACAO_COLUNA
                , ClasseNome = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioBandeiraRedeNoAssociations.
	    public IQueryable<TcsUsuarioBandeiraRede> GetTcsUsuarioBandeiraRedeNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_BANDEIRA_REDE
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsUsuarioBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioLayoutNoAssociations.
	    public IQueryable<TcsUsuarioLayout> GetTcsUsuarioLayoutNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioLayout> result = 
	            (from entity0 in this.DbContext.TCS_LAYOUT_USUARIO
                  let entity0Al1 = entity0.TCS_LAYOUT
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioLayout()		
	            {
	            
                DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioFilialNoAssociations.
	    public IQueryable<TcsUsuarioFilial> GetTcsUsuarioFilialNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFilial> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FILIAL
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdTcsUsuarioFilial = entity0.ID_TCS_USUARIO_FILIAL
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAcessoLocal.
	    public IQueryable<TcsUsuarioAcessoLocal> GetTcsUsuarioAcessoLocal()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoLocal> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioAcessoLocal()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoLocalNoAssociations.
	    public IQueryable<TcsUsuarioAcessoLocal> GetTcsUsuarioAcessoLocalNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoLocal> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioAcessoLocal()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioPerfilP.
	    public IQueryable<TcsUsuarioPerfilP> GetTcsUsuarioPerfilP()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfilP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfilP()		
	            {
	            
                IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilPNoAssociations.
	    public IQueryable<TcsUsuarioPerfilP> GetTcsUsuarioPerfilPNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfilP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfilP()		
	            {
	            
                IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsUsuario|IdUsuarioCopia");
	    	result.Add("TcsUsuario|0");
	    	result.Add("TcsUsuario|NomeUsuarioCopia");
	    	result.Add("TcsUsuario|String.Empty");
	    	//Add filtering disabled property for TCS_USUARIO
	    	string[] bmDisabledTcsUsuarioList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO");
	    	if (bmDisabledTcsUsuarioList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.BAIRRO"))
	    		{
	    			result.Add("TcsUsuario|Bairro");
	    			result.Add("TcsUsuario|TCS_USUARIO.BAIRRO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.CEP"))
	    		{
	    			result.Add("TcsUsuario|Cep");
	    			result.Add("TcsUsuario|TCS_USUARIO.CEP");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.CNPJ_CPF"))
	    		{
	    			result.Add("TcsUsuario|CnpjCpf");
	    			result.Add("TcsUsuario|TCS_USUARIO.CNPJ_CPF");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.COMPLEMENTO"))
	    		{
	    			result.Add("TcsUsuario|Complemento");
	    			result.Add("TcsUsuario|TCS_USUARIO.COMPLEMENTO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.DATA_ALTERACAO"))
	    		{
	    			result.Add("TcsUsuario|DataAlteracao");
	    			result.Add("TcsUsuario|TCS_USUARIO.DATA_ALTERACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.DATA_CADASTRO"))
	    		{
	    			result.Add("TcsUsuario|DataCadastro");
	    			result.Add("TcsUsuario|TCS_USUARIO.DATA_CADASTRO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.EMAIL"))
	    		{
	    			result.Add("TcsUsuario|Email");
	    			result.Add("TcsUsuario|TCS_USUARIO.EMAIL");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.FONE_CELULAR"))
	    		{
	    			result.Add("TcsUsuario|FoneCelular");
	    			result.Add("TcsUsuario|TCS_USUARIO.FONE_CELULAR");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.FONE_FIXO"))
	    		{
	    			result.Add("TcsUsuario|FoneFixo");
	    			result.Add("TcsUsuario|TCS_USUARIO.FONE_FIXO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.ID_LINX"))
	    		{
	    			result.Add("TcsUsuario|IdLinx");
	    			result.Add("TcsUsuario|TCS_USUARIO.ID_LINX");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.ID_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|IdUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO.ID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.INSCR_ESTADUAL_RG"))
	    		{
	    			result.Add("TcsUsuario|InscrEstadualRg");
	    			result.Add("TcsUsuario|TCS_USUARIO.INSCR_ESTADUAL_RG");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.LOGRADOURO"))
	    		{
	    			result.Add("TcsUsuario|Logradouro");
	    			result.Add("TcsUsuario|TCS_USUARIO.LOGRADOURO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.LX_PFJ_FISICA_JURIDICA"))
	    		{
	    			result.Add("TcsUsuario|LxPfjFisicaJuridica");
	    			result.Add("TcsUsuario|TCS_USUARIO.LX_PFJ_FISICA_JURIDICA");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.LX_TIPO_LOGRADOURO"))
	    		{
	    			result.Add("TcsUsuario|LxTipoLogradouro");
	    			result.Add("TcsUsuario|TCS_USUARIO.LX_TIPO_LOGRADOURO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.MUNICIPIO"))
	    		{
	    			result.Add("TcsUsuario|Municipio");
	    			result.Add("TcsUsuario|TCS_USUARIO.MUNICIPIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.NOME_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|NomeUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO.NOME_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.NUMERO"))
	    		{
	    			result.Add("TcsUsuario|Numero");
	    			result.Add("TcsUsuario|TCS_USUARIO.NUMERO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.OBS_ENDERECO"))
	    		{
	    			result.Add("TcsUsuario|ObsEndereco");
	    			result.Add("TcsUsuario|TCS_USUARIO.OBS_ENDERECO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.RAMAL"))
	    		{
	    			result.Add("TcsUsuario|Ramal");
	    			result.Add("TcsUsuario|TCS_USUARIO.RAMAL");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.UF"))
	    		{
	    			result.Add("TcsUsuario|Uf");
	    			result.Add("TcsUsuario|TCS_USUARIO.UF");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.UID_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|UidUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO.UID_USUARIO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_PERFIL
	    	string[] bmDisabledTcsUsuarioPerfilList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_PERFIL");
	    	if (bmDisabledTcsUsuarioPerfilList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioPerfilList.Contains("TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL"))
	    		{
	    			result.Add("TcsUsuarioPerfil|IdTcsUsuarioPerfil");
	    			result.Add("TcsUsuarioPerfil|TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_REGRA_MODULO
	    	string[] bmDisabledTcsUsuarioRegraModuloList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_REGRA_MODULO");
	    	if (bmDisabledTcsUsuarioRegraModuloList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioRegraModuloList.Contains("TCS_USUARIO_REGRA_MODULO.ID_MODULO"))
	    		{
	    			result.Add("TcsUsuarioRegraModulo|IdModulo");
	    			result.Add("TcsUsuarioRegraModulo|TCS_USUARIO_REGRA_MODULO.ID_MODULO");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraModuloList.Contains("TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO"))
	    		{
	    			result.Add("TcsUsuarioRegraModulo|IdUsuarioRegraModulo");
	    			result.Add("TcsUsuarioRegraModulo|TCS_USUARIO_REGRA_MODULO.ID_USUARIO_REGRA_MODULO");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraModuloList.Contains("TCS_USUARIO_REGRA_MODULO.LX_REGRA_ACESSO_MODULO"))
	    		{
	    			result.Add("TcsUsuarioRegraModulo|LxRegraAcessoModulo");
	    			result.Add("TcsUsuarioRegraModulo|TCS_USUARIO_REGRA_MODULO.LX_REGRA_ACESSO_MODULO");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraModuloList.Contains("TCS_USUARIO_REGRA_MODULO.REGRA_TRANSACAO"))
	    		{
	    			result.Add("TcsUsuarioRegraModulo|RegraTransacao");
	    			result.Add("TcsUsuarioRegraModulo|TCS_USUARIO_REGRA_MODULO.REGRA_TRANSACAO");
	    		}
	    	}
	    	result.Add("TcsUsuarioRegraTransacao|ClasseNome");
	    	result.Add("TcsUsuarioRegraTransacao|''");
	    	//Add filtering disabled property for TCS_USUARIO_REGRA_TRANSACAO
	    	string[] bmDisabledTcsUsuarioRegraTransacaoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_REGRA_TRANSACAO");
	    	if (bmDisabledTcsUsuarioRegraTransacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioRegraTransacaoList.Contains("TCS_USUARIO_REGRA_TRANSACAO.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsUsuarioRegraTransacao|IdTransacao");
	    			result.Add("TcsUsuarioRegraTransacao|TCS_USUARIO_REGRA_TRANSACAO.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraTransacaoList.Contains("TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO"))
	    		{
	    			result.Add("TcsUsuarioRegraTransacao|IdUsuarioRegraTransacao");
	    			result.Add("TcsUsuarioRegraTransacao|TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO_REGRA_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraTransacaoList.Contains("TCS_USUARIO_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO"))
	    		{
	    			result.Add("TcsUsuarioRegraTransacao|LxRegraAcessoTransacao");
	    			result.Add("TcsUsuarioRegraTransacao|TCS_USUARIO_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraTransacaoList.Contains("TCS_USUARIO_REGRA_TRANSACAO.REGRA_TRANSACAO"))
	    		{
	    			result.Add("TcsUsuarioRegraTransacao|RegraTransacao");
	    			result.Add("TcsUsuarioRegraTransacao|TCS_USUARIO_REGRA_TRANSACAO.REGRA_TRANSACAO");
	    		}
	    	}
	    	result.Add("TcsUsuarioRegraColuna|ClasseNome");
	    	result.Add("TcsUsuarioRegraColuna|''");
	    	//Add filtering disabled property for TCS_USUARIO_REGRA_COLUNA
	    	string[] bmDisabledTcsUsuarioRegraColunaList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_REGRA_COLUNA");
	    	if (bmDisabledTcsUsuarioRegraColunaList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioRegraColunaList.Contains("TCS_USUARIO_REGRA_COLUNA.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsUsuarioRegraColuna|IdTransacao");
	    			result.Add("TcsUsuarioRegraColuna|TCS_USUARIO_REGRA_COLUNA.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraColunaList.Contains("TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA"))
	    		{
	    			result.Add("TcsUsuarioRegraColuna|IdUsuarioRegraColuna");
	    			result.Add("TcsUsuarioRegraColuna|TCS_USUARIO_REGRA_COLUNA.ID_USUARIO_REGRA_COLUNA");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraColunaList.Contains("TCS_USUARIO_REGRA_COLUNA.LX_REGRA_ACESSO_COLUNA"))
	    		{
	    			result.Add("TcsUsuarioRegraColuna|LxRegraAcessoColuna");
	    			result.Add("TcsUsuarioRegraColuna|TCS_USUARIO_REGRA_COLUNA.LX_REGRA_ACESSO_COLUNA");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraColunaList.Contains("TCS_USUARIO_REGRA_COLUNA.REGRA_TRANSACAO"))
	    		{
	    			result.Add("TcsUsuarioRegraColuna|RegraTransacao");
	    			result.Add("TcsUsuarioRegraColuna|TCS_USUARIO_REGRA_COLUNA.REGRA_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioRegraColunaList.Contains("TCS_USUARIO_REGRA_COLUNA.TRANSACAO_COLUNA"))
	    		{
	    			result.Add("TcsUsuarioRegraColuna|TransacaoColuna");
	    			result.Add("TcsUsuarioRegraColuna|TCS_USUARIO_REGRA_COLUNA.TRANSACAO_COLUNA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_BANDEIRA_REDE
	    	string[] bmDisabledTcsUsuarioBandeiraRedeList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_BANDEIRA_REDE");
	    	if (bmDisabledTcsUsuarioBandeiraRedeList.Length > 0)
	    	{
	    	}
	    	//Add filtering disabled property for TCS_LAYOUT_USUARIO
	    	string[] bmDisabledTcsUsuarioLayoutList = this.GetEDM().GetFilteringDisabledList("TCS_LAYOUT_USUARIO");
	    	if (bmDisabledTcsUsuarioLayoutList.Length > 0)
	    	{
	    	}
	    	//Add filtering disabled property for TCS_USUARIO
	    	string[] bmDisabledTcsUsuarioAcessoLocalList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO");
	    	if (bmDisabledTcsUsuarioAcessoLocalList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioAcessoLocalList.Contains("TCS_USUARIO.ID_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAcessoLocal|IdUsuario");
	    			result.Add("TcsUsuarioAcessoLocal|TCS_USUARIO.ID_USUARIO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_FILIAL
	    	string[] bmDisabledTcsUsuarioFilialList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_FILIAL");
	    	if (bmDisabledTcsUsuarioFilialList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioFilialList.Contains("TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL"))
	    		{
	    			result.Add("TcsUsuarioFilial|IdTcsUsuarioFilial");
	    			result.Add("TcsUsuarioFilial|TCS_USUARIO_FILIAL.ID_TCS_USUARIO_FILIAL");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_PERFIL
	    	string[] bmDisabledTcsUsuarioPerfilPList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_PERFIL");
	    	if (bmDisabledTcsUsuarioPerfilPList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioPerfilPList.Contains("TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL"))
	    		{
	    			result.Add("TcsUsuarioPerfilP|IdTcsUsuarioPerfil");
	    			result.Add("TcsUsuarioPerfilP|TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsUsuario By EntitySearchId.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfil By EntitySearchId.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioPerfilByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioRegraModulo By EntitySearchId.
	    public IQueryable<TcsUsuarioRegraModulo> GetTcsUsuarioRegraModuloByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioRegraModuloByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioRegraTransacao By EntitySearchId.
	    public IQueryable<TcsUsuarioRegraTransacao> GetTcsUsuarioRegraTransacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioRegraTransacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioRegraColuna By EntitySearchId.
	    public IQueryable<TcsUsuarioRegraColuna> GetTcsUsuarioRegraColunaByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioRegraColunaByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioBandeiraRede By EntitySearchId.
	    public IQueryable<TcsUsuarioBandeiraRede> GetTcsUsuarioBandeiraRedeByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioBandeiraRedeByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioLayout By EntitySearchId.
	    public IQueryable<TcsUsuarioLayout> GetTcsUsuarioLayoutByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioLayoutByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioFilial By EntitySearchId.
	    public IQueryable<TcsUsuarioFilial> GetTcsUsuarioFilialByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioFilialByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuario By EntitySearchId.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfil By EntitySearchId.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioRegraModulo By EntitySearchId.
	    public IQueryable<TcsUsuarioRegraModulo> GetTcsUsuarioRegraModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioRegraModuloByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioRegraTransacao By EntitySearchId.
	    public IQueryable<TcsUsuarioRegraTransacao> GetTcsUsuarioRegraTransacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioRegraTransacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioRegraColuna By EntitySearchId.
	    public IQueryable<TcsUsuarioRegraColuna> GetTcsUsuarioRegraColunaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioRegraColunaByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioBandeiraRede By EntitySearchId.
	    public IQueryable<TcsUsuarioBandeiraRede> GetTcsUsuarioBandeiraRedeByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioBandeiraRedeByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioLayout By EntitySearchId.
	    public IQueryable<TcsUsuarioLayout> GetTcsUsuarioLayoutByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioLayoutByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioFilial By EntitySearchId.
	    public IQueryable<TcsUsuarioFilial> GetTcsUsuarioFilialByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioFilialByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAcessoLocal By EntitySearchId.
	    public IQueryable<TcsUsuarioAcessoLocal> GetTcsUsuarioAcessoLocalByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAcessoLocalByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAcessoLocal By EntitySearchId.
	    public IQueryable<TcsUsuarioAcessoLocal> GetTcsUsuarioAcessoLocalByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAcessoLocalByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfilP By EntitySearchId.
	    public IQueryable<TcsUsuarioPerfilP> GetTcsUsuarioPerfilPByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioPerfilPByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfilP By EntitySearchId.
	    public IQueryable<TcsUsuarioPerfilP> GetTcsUsuarioPerfilPByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioPerfilPByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsUsuario> GetTcsUsuarioByExample(TcsUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByExample(TcsUsuarioPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioRegraModulo By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioRegraModulo> GetTcsUsuarioRegraModuloByExample(TcsUsuarioRegraModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioRegraModuloByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioRegraTransacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioRegraTransacao> GetTcsUsuarioRegraTransacaoByExample(TcsUsuarioRegraTransacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioRegraTransacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioRegraColuna By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioRegraColuna> GetTcsUsuarioRegraColunaByExample(TcsUsuarioRegraColuna entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioRegraColunaByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioBandeiraRede By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioBandeiraRede> GetTcsUsuarioBandeiraRedeByExample(TcsUsuarioBandeiraRede entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioBandeiraRedeByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioLayout By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioLayout> GetTcsUsuarioLayoutByExample(TcsUsuarioLayout entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioLayoutByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioFilial By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioFilial> GetTcsUsuarioFilialByExample(TcsUsuarioFilial entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioFilialByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsUsuario> GetTcsUsuarioByExampleNoAssociations(TcsUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByExampleNoAssociations(TcsUsuarioPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioRegraModulo By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioRegraModulo> GetTcsUsuarioRegraModuloByExampleNoAssociations(TcsUsuarioRegraModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioRegraModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioRegraTransacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioRegraTransacao> GetTcsUsuarioRegraTransacaoByExampleNoAssociations(TcsUsuarioRegraTransacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioRegraTransacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioRegraColuna By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioRegraColuna> GetTcsUsuarioRegraColunaByExampleNoAssociations(TcsUsuarioRegraColuna entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioRegraColunaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioBandeiraRede By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioBandeiraRede> GetTcsUsuarioBandeiraRedeByExampleNoAssociations(TcsUsuarioBandeiraRede entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioBandeiraRedeByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioLayout By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioLayout> GetTcsUsuarioLayoutByExampleNoAssociations(TcsUsuarioLayout entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioLayoutByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioFilial By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioFilial> GetTcsUsuarioFilialByExampleNoAssociations(TcsUsuarioFilial entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioFilialByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAcessoLocal By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAcessoLocal> GetTcsUsuarioAcessoLocalByExample(TcsUsuarioAcessoLocal entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAcessoLocalByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAcessoLocal By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAcessoLocal> GetTcsUsuarioAcessoLocalByExampleNoAssociations(TcsUsuarioAcessoLocal entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAcessoLocalByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfilP By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfilP> GetTcsUsuarioPerfilPByExample(TcsUsuarioPerfilP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilPByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfilP By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfilP> GetTcsUsuarioPerfilPByExampleNoAssociations(TcsUsuarioPerfilP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilPByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsUsuario GetTcsUsuarioByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuario");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioPerfil GetTcsUsuarioPerfilByKey(Int64 idTcsUsuarioPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioPerfil");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioRegraModulo GetTcsUsuarioRegraModuloByKey(Int64 idUsuarioRegraModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioRegraModulo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuarioRegraModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuarioRegraModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioRegraModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioRegraTransacao GetTcsUsuarioRegraTransacaoByKey(Int64 idUsuarioRegraTransacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioRegraTransacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuarioRegraTransacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuarioRegraTransacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioRegraTransacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioRegraColuna GetTcsUsuarioRegraColunaByKey(Int32 idUsuarioRegraColuna)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioRegraColuna");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuarioRegraColuna"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuarioRegraColuna));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioRegraColunaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioBandeiraRede GetTcsUsuarioBandeiraRedeByKey(Int32 idBandeiraR, Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioBandeiraRede");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdBandeiraR"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idBandeiraR));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioLayout GetTcsUsuarioLayoutByKey(Int64 idObjetoConteudo, Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioLayout");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjetoConteudo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idObjetoConteudo));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioLayoutByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAcessoLocal GetTcsUsuarioAcessoLocalByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAcessoLocal");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAcessoLocalByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioFilial GetTcsUsuarioFilialByKey(Int64 idTcsUsuarioFilial)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioFilial");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioFilial"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioFilial));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioFilialByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioPerfilP GetTcsUsuarioPerfilPByKey(Int64 idTcsUsuarioPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioPerfilP");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioPerfilPByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioByEntitySearch.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeUsuario = entity0.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
			
                ,TcsUsuarioPerfilList = 
	                        (from entity1 in entity0.TCS_USUARIO_PERFIL_LISTA
                                  let entity1Al1 = entity1.TCS_PERFIL
                                  let entity1Al2 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioPerfil()
	                        {
	                        
                                DescPerfil = entity1Al1.DESC_PERFIL
                                , IdPerfil = entity1Al1.ID_PERFIL
                                , IdTcsUsuarioPerfil = entity1.ID_TCS_USUARIO_PERFIL
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , Inativo = entity1Al1.INATIVO
		
	                        }
	                        )
			
                ,TcsUsuarioRegraModuloList = 
	                        (from entity1 in entity0.TCS_USUARIO_REGRA_MODULO_LISTA
                                  let entity1Al1 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioRegraModulo()
	                        {
	                        
                                IdModulo = entity1.ID_MODULO
                                , IdUsuario = entity1Al1.ID_USUARIO
                                , IdUsuarioRegraModulo = entity1.ID_USUARIO_REGRA_MODULO
                                , LxRegraAcessoModulo = entity1.LX_REGRA_ACESSO_MODULO
                                , LxRegraAcessoModuloName = ((entity1.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                                , RegraTransacao = entity1.REGRA_TRANSACAO
		
	                        }
	                        )
			
                ,TcsUsuarioRegraTransacaoList = 
	                        (from entity1 in entity0.TCS_USUARIO_REGRA_TRANSACAO_LISTA
                                  let entity1Al1 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioRegraTransacao()
	                        {
	                        
                                IdTransacao = entity1.ID_TRANSACAO
                                , IdUsuario = entity1Al1.ID_USUARIO
                                , IdUsuarioRegraTransacao = entity1.ID_USUARIO_REGRA_TRANSACAO
                                , LxRegraAcessoTransacao = entity1.LX_REGRA_ACESSO_TRANSACAO
                                , LxRegraAcessoTransacaoName = ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                                , RegraTransacao = entity1.REGRA_TRANSACAO
                                , UidUsuario = entity1Al1.UID_USUARIO
                                , ClasseNome = ""
		
	                        }
	                        )
			
                ,TcsUsuarioRegraColunaList = 
	                        (from entity1 in entity0.TCS_USUARIO_REGRA_COLUNA_LISTA
                                  let entity1Al1 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioRegraColuna()
	                        {
	                        
                                IdTransacao = entity1.ID_TRANSACAO
                                , IdUsuario = entity1Al1.ID_USUARIO
                                , IdUsuarioRegraColuna = entity1.ID_USUARIO_REGRA_COLUNA
                                , LxRegraAcessoColuna = entity1.LX_REGRA_ACESSO_COLUNA
                                , LxRegraAcessoColunaName = ((entity1.LX_REGRA_ACESSO_COLUNA) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 4 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 5 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 99 ? "Regra Transação" : ((entity1.LX_REGRA_ACESSO_COLUNA) == 3 ? "Visualizar" : ""))))))
                                , RegraTransacao = entity1.REGRA_TRANSACAO
                                , TransacaoColuna = entity1.TRANSACAO_COLUNA
                                , ClasseNome = ""
		
	                        }
	                        )
			
                ,TcsUsuarioBandeiraRedeList = 
	                        (from entity1 in entity0.TCS_USUARIO_BANDEIRA_REDE_LISTA
                                  let entity1Al2 = entity1.TCS_USUARIO
                                  let entity1Al1 = entity1.TBC_BANDEIRA_REDE
	                        
	                        	
	                        select new TcsUsuarioBandeiraRede()
	                        {
	                        
                                DescBandeiraRede = entity1Al1.DESC_BANDEIRA_REDE
                                , IdBandeiraR = entity1Al1.ID_BANDEIRA_REDE
                                , IdUsuario = entity1Al2.ID_USUARIO
		
	                        }
	                        )
			
                ,TcsUsuarioLayoutList = 
	                        (from entity1 in entity0.TCS_LAYOUT_USUARIO_LISTA
                                  let entity1Al1 = entity1.TCS_LAYOUT
                                  let entity1Al2 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioLayout()
	                        {
	                        
                                DescLayout = entity1Al1.DESC_LAYOUT
                                , Detalhes = entity1Al1.DETALHES
                                , IdObjetoConteudo = entity1Al1.ID_OBJETO_CONTEUDO
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , Inativo = entity1Al1.INATIVO
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        TcsUsuario.OnSearching(ref result, false, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilByEntitySearch.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraModuloByEntitySearch.
	    public IQueryable<TcsUsuarioRegraModulo> GetTcsUsuarioRegraModuloByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraModulo = entity0.ID_USUARIO_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraTransacaoByEntitySearch.
	    public IQueryable<TcsUsuarioRegraTransacao> GetTcsUsuarioRegraTransacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraTransacao()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraTransacao = entity0.ID_USUARIO_REGRA_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , UidUsuario = entity0Al1.UID_USUARIO
                , ClasseNome = ""
		
	            }
	            );
	
	        SetTcsUsuarioRegraTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraColunaByEntitySearch.
	    public IQueryable<TcsUsuarioRegraColuna> GetTcsUsuarioRegraColunaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraColuna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraColuna> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_COLUNA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraColuna()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraColuna = entity0.ID_USUARIO_REGRA_COLUNA
                , LxRegraAcessoColuna = entity0.LX_REGRA_ACESSO_COLUNA
                , LxRegraAcessoColunaName = ((entity0.LX_REGRA_ACESSO_COLUNA) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 4 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 5 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 99 ? "Regra Transação" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 3 ? "Visualizar" : ""))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , TransacaoColuna = entity0.TRANSACAO_COLUNA
                , ClasseNome = ""
		
	            }
	            );
	
	        SetTcsUsuarioRegraColunaBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioBandeiraRedeByEntitySearch.
	    public IQueryable<TcsUsuarioBandeiraRede> GetTcsUsuarioBandeiraRedeByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsUsuarioBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioLayoutByEntitySearch.
	    public IQueryable<TcsUsuarioLayout> GetTcsUsuarioLayoutByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioLayout> result = 
	            (from entity0 in this.DbContext.TCS_LAYOUT_USUARIO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_LAYOUT
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioLayout()		
	            {
	            
                DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioFilialByEntitySearch.
	    public IQueryable<TcsUsuarioFilial> GetTcsUsuarioFilialByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFilial> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdTcsUsuarioFilial = entity0.ID_TCS_USUARIO_FILIAL
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeUsuario = entity0.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        TcsUsuario.OnSearching(ref result, true, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraModuloByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioRegraModulo> GetTcsUsuarioRegraModuloByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraModulo = entity0.ID_USUARIO_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraTransacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioRegraTransacao> GetTcsUsuarioRegraTransacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraTransacao()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraTransacao = entity0.ID_USUARIO_REGRA_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , UidUsuario = entity0Al1.UID_USUARIO
                , ClasseNome = ""
		
	            }
	            );
	
	        SetTcsUsuarioRegraTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraColunaByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioRegraColuna> GetTcsUsuarioRegraColunaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraColuna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraColuna> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_COLUNA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraColuna()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraColuna = entity0.ID_USUARIO_REGRA_COLUNA
                , LxRegraAcessoColuna = entity0.LX_REGRA_ACESSO_COLUNA
                , LxRegraAcessoColunaName = ((entity0.LX_REGRA_ACESSO_COLUNA) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 4 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 5 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 99 ? "Regra Transação" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 3 ? "Visualizar" : ""))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , TransacaoColuna = entity0.TRANSACAO_COLUNA
                , ClasseNome = ""
		
	            }
	            );
	
	        SetTcsUsuarioRegraColunaBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioBandeiraRedeByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioBandeiraRede> GetTcsUsuarioBandeiraRedeByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsUsuarioBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioLayoutByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioLayout> GetTcsUsuarioLayoutByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioLayout> result = 
	            (from entity0 in this.DbContext.TCS_LAYOUT_USUARIO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_LAYOUT
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioLayout()		
	            {
	            
                DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioFilialByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioFilial> GetTcsUsuarioFilialByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFilial> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdTcsUsuarioFilial = entity0.ID_TCS_USUARIO_FILIAL
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioPerfilParentComposition> GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO", "TCS_USUARIO_PERFIL", "TCS_USUARIO", typeof(TcsUsuarioPerfilParentComposition), typeof(TcsUsuarioRegraModulo), typeof(TcsUsuarioRegraTransacao), typeof(TcsUsuarioRegraColuna), typeof(TcsUsuarioBandeiraRede), typeof(TcsUsuarioLayout), typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfilParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfilParentComposition()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
                //TcsUsuario Properties.
                , Bairro = entity0.TCS_USUARIO.BAIRRO
                , Cep = entity0.TCS_USUARIO.CEP
                , CnpjCpf = entity0.TCS_USUARIO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO.COMPLEMENTO
                , DataAlteracao = entity0.TCS_USUARIO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO.DATA_CADASTRO
                , Email = entity0.TCS_USUARIO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO.FONE_FIXO
                , IdLinx = entity0.TCS_USUARIO.ID_LINX
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.TCS_USUARIO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO.MUNICIPIO
                , NomeUsuario = entity0.TCS_USUARIO.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.TCS_USUARIO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO.RAMAL
                , Uf = entity0.TCS_USUARIO.UF
                , UidUsuario = entity0.TCS_USUARIO.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraModuloParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioRegraModuloParentComposition> GetTcsUsuarioRegraModuloParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO", "TCS_USUARIO_REGRA_MODULO", "TCS_USUARIO", typeof(TcsUsuarioRegraModuloParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsUsuarioRegraTransacao), typeof(TcsUsuarioRegraColuna), typeof(TcsUsuarioBandeiraRede), typeof(TcsUsuarioLayout), typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraModuloParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraModuloParentComposition()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraModulo = entity0.ID_USUARIO_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                //TcsUsuario Properties.
                , Bairro = entity0.TCS_USUARIO.BAIRRO
                , Cep = entity0.TCS_USUARIO.CEP
                , CnpjCpf = entity0.TCS_USUARIO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO.COMPLEMENTO
                , DataAlteracao = entity0.TCS_USUARIO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO.DATA_CADASTRO
                , Email = entity0.TCS_USUARIO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO.FONE_FIXO
                , IdLinx = entity0.TCS_USUARIO.ID_LINX
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.TCS_USUARIO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO.MUNICIPIO
                , NomeUsuario = entity0.TCS_USUARIO.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.TCS_USUARIO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO.RAMAL
                , Uf = entity0.TCS_USUARIO.UF
                , UidUsuario = entity0.TCS_USUARIO.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraTransacaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioRegraTransacaoParentComposition> GetTcsUsuarioRegraTransacaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO", "TCS_USUARIO_REGRA_TRANSACAO", "TCS_USUARIO", typeof(TcsUsuarioRegraTransacaoParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsUsuarioRegraModulo), typeof(TcsUsuarioRegraColuna), typeof(TcsUsuarioBandeiraRede), typeof(TcsUsuarioLayout), typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraTransacaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraTransacaoParentComposition()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraTransacao = entity0.ID_USUARIO_REGRA_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , UidUsuario = entity0Al1.UID_USUARIO
                //TcsUsuario Properties.
                , Bairro = entity0.TCS_USUARIO.BAIRRO
                , Cep = entity0.TCS_USUARIO.CEP
                , CnpjCpf = entity0.TCS_USUARIO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO.COMPLEMENTO
                , DataAlteracao = entity0.TCS_USUARIO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO.DATA_CADASTRO
                , Email = entity0.TCS_USUARIO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO.FONE_FIXO
                , IdLinx = entity0.TCS_USUARIO.ID_LINX
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.TCS_USUARIO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO.MUNICIPIO
                , NomeUsuario = entity0.TCS_USUARIO.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.TCS_USUARIO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO.RAMAL
                , Uf = entity0.TCS_USUARIO.UF
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioRegraColunaParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioRegraColunaParentComposition> GetTcsUsuarioRegraColunaParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO", "TCS_USUARIO_REGRA_COLUNA", "TCS_USUARIO", typeof(TcsUsuarioRegraColunaParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsUsuarioRegraModulo), typeof(TcsUsuarioRegraTransacao), typeof(TcsUsuarioBandeiraRede), typeof(TcsUsuarioLayout), typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraColunaParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_COLUNA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioRegraColunaParentComposition()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraColuna = entity0.ID_USUARIO_REGRA_COLUNA
                , LxRegraAcessoColuna = entity0.LX_REGRA_ACESSO_COLUNA
                , LxRegraAcessoColunaName = ((entity0.LX_REGRA_ACESSO_COLUNA) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 4 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 5 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 99 ? "Regra Transação" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 3 ? "Visualizar" : ""))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , TransacaoColuna = entity0.TRANSACAO_COLUNA
                //TcsUsuario Properties.
                , Bairro = entity0.TCS_USUARIO.BAIRRO
                , Cep = entity0.TCS_USUARIO.CEP
                , CnpjCpf = entity0.TCS_USUARIO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO.COMPLEMENTO
                , DataAlteracao = entity0.TCS_USUARIO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO.DATA_CADASTRO
                , Email = entity0.TCS_USUARIO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO.FONE_FIXO
                , IdLinx = entity0.TCS_USUARIO.ID_LINX
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.TCS_USUARIO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO.MUNICIPIO
                , NomeUsuario = entity0.TCS_USUARIO.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.TCS_USUARIO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO.RAMAL
                , Uf = entity0.TCS_USUARIO.UF
                , UidUsuario = entity0.TCS_USUARIO.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioBandeiraRedeParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioBandeiraRedeParentComposition> GetTcsUsuarioBandeiraRedeParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO", "TCS_USUARIO_BANDEIRA_REDE", "TCS_USUARIO", typeof(TcsUsuarioBandeiraRedeParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsUsuarioRegraModulo), typeof(TcsUsuarioRegraTransacao), typeof(TcsUsuarioRegraColuna), typeof(TcsUsuarioLayout), typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioBandeiraRedeParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsUsuarioBandeiraRedeParentComposition()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdUsuario = entity0Al2.ID_USUARIO
                //TcsUsuario Properties.
                , Bairro = entity0.TCS_USUARIO.BAIRRO
                , Cep = entity0.TCS_USUARIO.CEP
                , CnpjCpf = entity0.TCS_USUARIO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO.COMPLEMENTO
                , DataAlteracao = entity0.TCS_USUARIO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO.DATA_CADASTRO
                , Email = entity0.TCS_USUARIO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO.FONE_FIXO
                , IdLinx = entity0.TCS_USUARIO.ID_LINX
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.TCS_USUARIO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO.MUNICIPIO
                , NomeUsuario = entity0.TCS_USUARIO.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.TCS_USUARIO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO.RAMAL
                , Uf = entity0.TCS_USUARIO.UF
                , UidUsuario = entity0.TCS_USUARIO.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioLayoutParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioLayoutParentComposition> GetTcsUsuarioLayoutParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO", "TCS_LAYOUT_USUARIO", "TCS_USUARIO", typeof(TcsUsuarioLayoutParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsUsuarioRegraModulo), typeof(TcsUsuarioRegraTransacao), typeof(TcsUsuarioRegraColuna), typeof(TcsUsuarioBandeiraRede), typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioLayoutParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_LAYOUT_USUARIO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_LAYOUT
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioLayoutParentComposition()		
	            {
	            
                DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
                //TcsUsuario Properties.
                , Bairro = entity0.TCS_USUARIO.BAIRRO
                , Cep = entity0.TCS_USUARIO.CEP
                , CnpjCpf = entity0.TCS_USUARIO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO.COMPLEMENTO
                , DataAlteracao = entity0.TCS_USUARIO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO.DATA_CADASTRO
                , Email = entity0.TCS_USUARIO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO.FONE_FIXO
                , IdLinx = entity0.TCS_USUARIO.ID_LINX
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.TCS_USUARIO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO.MUNICIPIO
                , NomeUsuario = entity0.TCS_USUARIO.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.TCS_USUARIO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO.RAMAL
                , Uf = entity0.TCS_USUARIO.UF
                , UidUsuario = entity0.TCS_USUARIO.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioFilialParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioFilialParentComposition> GetTcsUsuarioFilialParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO", "TCS_USUARIO_FILIAL", "TCS_USUARIO", typeof(TcsUsuarioFilialParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsUsuarioRegraModulo), typeof(TcsUsuarioRegraTransacao), typeof(TcsUsuarioRegraColuna), typeof(TcsUsuarioBandeiraRede), typeof(TcsUsuarioLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFilialParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFilialParentComposition()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdTcsUsuarioFilial = entity0.ID_TCS_USUARIO_FILIAL
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeFilial = entity0Al1.NOME_FILIAL
                //TcsUsuario Properties.
                , Bairro = entity0.TCS_USUARIO.BAIRRO
                , Cep = entity0.TCS_USUARIO.CEP
                , CnpjCpf = entity0.TCS_USUARIO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO.COMPLEMENTO
                , DataAlteracao = entity0.TCS_USUARIO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO.DATA_CADASTRO
                , Email = entity0.TCS_USUARIO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO.FONE_FIXO
                , IdLinx = entity0.TCS_USUARIO.ID_LINX
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.TCS_USUARIO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO.MUNICIPIO
                , NomeUsuario = entity0.TCS_USUARIO.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.TCS_USUARIO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO.RAMAL
                , Uf = entity0.TCS_USUARIO.UF
                , UidUsuario = entity0.TCS_USUARIO.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsUsuarioBusinessFilter(ref IQueryable<TcsUsuario> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuario"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "IdUsuarioCopia" || e.Value.ToString() == "0")))
	    				{
	    					idxElement = search.Expressions.IndexOf(exp);
	    					if ((idxElement + 2) < search.Expressions.Count)
	    					{
	    						if (search.Expressions[idxElement + 1].Name == "Operator" && search.Expressions[idxElement + 2].Name == "Value")
	    						{
	    								operatorValue = search.Expressions[idxElement + 1].Value.ToString();
	    								value = search.Expressions[idxElement + 2].Value;
	    								if (value.IsNullOrEmpty())
												continue;

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										Int64 tmpIdUsuarioCopia1 = (Int64)value;
	    										query = from r in query where r.IdUsuarioCopia == tmpIdUsuarioCopia1 select r;
	    										break;
	    									case "!=":
	    										Int64 tmpIdUsuarioCopia2 = (Int64)value;
	    										query = from r in query where r.IdUsuarioCopia != tmpIdUsuarioCopia2 select r;
	    										break;

	
	    									case "<":
	    										Int64 tmpIdUsuarioCopia3 = (Int64)value;
	    										query = from r in query where r.IdUsuarioCopia < tmpIdUsuarioCopia3 select r;
	    										break;
	    									case "<=":
	    										Int64 tmpIdUsuarioCopia4 = (Int64)value;
	    										query = from r in query where r.IdUsuarioCopia <= tmpIdUsuarioCopia4 select r;
	    										break;
	    									case ">":
	    										Int64 tmpIdUsuarioCopia5 = (Int64)value;
	    										query = from r in query where r.IdUsuarioCopia > tmpIdUsuarioCopia5 select r;
	    										break;
	    									case ">=":
	    										Int64 tmpIdUsuarioCopia6 = (Int64)value;
	    										query = from r in query where r.IdUsuarioCopia >= tmpIdUsuarioCopia6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "NomeUsuarioCopia" || e.Value.ToString() == "String.Empty")))
	    				{
	    					idxElement = search.Expressions.IndexOf(exp);
	    					if ((idxElement + 2) < search.Expressions.Count)
	    					{
	    						if (search.Expressions[idxElement + 1].Name == "Operator" && search.Expressions[idxElement + 2].Name == "Value")
	    						{
	    								operatorValue = search.Expressions[idxElement + 1].Value.ToString();
	    								value = search.Expressions[idxElement + 2].Value;
	    								if (value.IsNullOrEmpty())
												continue;

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										System.String tmpNomeUsuarioCopia1 = (System.String)value;
	    										query = from r in query where r.NomeUsuarioCopia == tmpNomeUsuarioCopia1 select r;
	    										break;
	    									case "!=":
	    										System.String tmpNomeUsuarioCopia2 = (System.String)value;
	    										query = from r in query where r.NomeUsuarioCopia != tmpNomeUsuarioCopia2 select r;
	    										break;

	
	    									case "Contains":
	    										System.String tmpNomeUsuarioCopia7 = (System.String)value;
	    									    query = from r in query where r.NomeUsuarioCopia.Contains(tmpNomeUsuarioCopia7) select r;
	    									    break;
	    									case "StartsWith":
	    										System.String tmpNomeUsuarioCopia8 = (System.String)value;
	    									    query = from r in query where r.NomeUsuarioCopia.StartsWith(tmpNomeUsuarioCopia8) select r;
	    									    break;
	    									case "EndsWith":
	    										System.String tmpNomeUsuarioCopia9 = (System.String)value;
	    									    query = from r in query where r.NomeUsuarioCopia.EndsWith(tmpNomeUsuarioCopia9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    	
	    				}
	    			}   
	    }



	    [Ignore()]
	    private void SetTcsUsuarioRegraTransacaoBusinessFilter(ref IQueryable<TcsUsuarioRegraTransacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuarioRegraTransacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ClasseNome" || e.Value.ToString() == "''")))
	    				{
	    					idxElement = search.Expressions.IndexOf(exp);
	    					if ((idxElement + 2) < search.Expressions.Count)
	    					{
	    						if (search.Expressions[idxElement + 1].Name == "Operator" && search.Expressions[idxElement + 2].Name == "Value")
	    						{
	    								operatorValue = search.Expressions[idxElement + 1].Value.ToString();
	    								value = search.Expressions[idxElement + 2].Value;
	    								if (value.IsNullOrEmpty())
												continue;

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpClasseNome1 = (string)value;
	    										query = from r in query where r.ClasseNome == tmpClasseNome1 select r;
	    										break;
	    									case "!=":
	    										string tmpClasseNome2 = (string)value;
	    										query = from r in query where r.ClasseNome != tmpClasseNome2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpClasseNome7 = (string)value;
	    									    query = from r in query where r.ClasseNome.Contains(tmpClasseNome7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpClasseNome8 = (string)value;
	    									    query = from r in query where r.ClasseNome.StartsWith(tmpClasseNome8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpClasseNome9 = (string)value;
	    									    query = from r in query where r.ClasseNome.EndsWith(tmpClasseNome9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    	
	    				}
	    			}   
	    }



	    [Ignore()]
	    private void SetTcsUsuarioRegraColunaBusinessFilter(ref IQueryable<TcsUsuarioRegraColuna> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuarioRegraColuna"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ClasseNome" || e.Value.ToString() == "''")))
	    				{
	    					idxElement = search.Expressions.IndexOf(exp);
	    					if ((idxElement + 2) < search.Expressions.Count)
	    					{
	    						if (search.Expressions[idxElement + 1].Name == "Operator" && search.Expressions[idxElement + 2].Name == "Value")
	    						{
	    								operatorValue = search.Expressions[idxElement + 1].Value.ToString();
	    								value = search.Expressions[idxElement + 2].Value;
	    								if (value.IsNullOrEmpty())
												continue;

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpClasseNome1 = (string)value;
	    										query = from r in query where r.ClasseNome == tmpClasseNome1 select r;
	    										break;
	    									case "!=":
	    										string tmpClasseNome2 = (string)value;
	    										query = from r in query where r.ClasseNome != tmpClasseNome2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpClasseNome7 = (string)value;
	    									    query = from r in query where r.ClasseNome.Contains(tmpClasseNome7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpClasseNome8 = (string)value;
	    									    query = from r in query where r.ClasseNome.StartsWith(tmpClasseNome8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpClasseNome9 = (string)value;
	    									    query = from r in query where r.ClasseNome.EndsWith(tmpClasseNome9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    	
	    				}
	    			}   
	    }


		
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoLocalByEntitySearch.
	    public IQueryable<TcsUsuarioAcessoLocal> GetTcsUsuarioAcessoLocalByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcessoLocal));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoLocal> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuarioAcessoLocal()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoLocalByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAcessoLocal> GetTcsUsuarioAcessoLocalByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcessoLocal));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoLocal> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuarioAcessoLocal()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilPByEntitySearch.
	    public IQueryable<TcsUsuarioPerfilP> GetTcsUsuarioPerfilPByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfilP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfilP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfilP()		
	            {
	            
                IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilPByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioPerfilP> GetTcsUsuarioPerfilPByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfilP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfilP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfilP()		
	            {
	            
                IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuario.
	    public IQueryable<TcsUsuario> GetPagedTcsUsuario(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , IdUsuarioCopia = 0
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeUsuario = entity0.NOME_USUARIO
                , NomeUsuarioCopia = String.Empty
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        TcsUsuario.OnSearching(ref result, true, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioPerfil.
	    public IQueryable<TcsUsuarioPerfil> GetPagedTcsUsuarioPerfil(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
                orderby entity0.ID_TCS_USUARIO_PERFIL ascending
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioRegraModulo.
	    public IQueryable<TcsUsuarioRegraModulo> GetPagedTcsUsuarioRegraModulo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
                orderby entity0.ID_USUARIO_REGRA_MODULO ascending
	            
	            	
	            select new TcsUsuarioRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraModulo = entity0.ID_USUARIO_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioRegraTransacao.
	    public IQueryable<TcsUsuarioRegraTransacao> GetPagedTcsUsuarioRegraTransacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
                orderby entity0.ID_USUARIO_REGRA_TRANSACAO ascending
	            
	            	
	            select new TcsUsuarioRegraTransacao()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraTransacao = entity0.ID_USUARIO_REGRA_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , UidUsuario = entity0Al1.UID_USUARIO
                , ClasseNome = ""
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioRegraTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioRegraColuna.
	    public IQueryable<TcsUsuarioRegraColuna> GetPagedTcsUsuarioRegraColuna(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraColuna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioRegraColuna> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_REGRA_COLUNA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
                orderby entity0.ID_USUARIO_REGRA_COLUNA ascending
	            
	            	
	            select new TcsUsuarioRegraColuna()		
	            {
	            
                IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IdUsuarioRegraColuna = entity0.ID_USUARIO_REGRA_COLUNA
                , LxRegraAcessoColuna = entity0.LX_REGRA_ACESSO_COLUNA
                , LxRegraAcessoColunaName = ((entity0.LX_REGRA_ACESSO_COLUNA) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 4 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 5 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 99 ? "Regra Transação" : ((entity0.LX_REGRA_ACESSO_COLUNA) == 3 ? "Visualizar" : ""))))))
                , RegraTransacao = entity0.REGRA_TRANSACAO
                , TransacaoColuna = entity0.TRANSACAO_COLUNA
                , ClasseNome = ""
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioRegraColunaBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioBandeiraRede.
	    public IQueryable<TcsUsuarioBandeiraRede> GetPagedTcsUsuarioBandeiraRede(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
                orderby entity0Al1.ID_BANDEIRA_REDE ascending, entity0Al2.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuarioBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioLayout.
	    public IQueryable<TcsUsuarioLayout> GetPagedTcsUsuarioLayout(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioLayout> result = 
	            (from entity0 in this.DbContext.TCS_LAYOUT_USUARIO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_LAYOUT
                  let entity0Al2 = entity0.TCS_USUARIO
                orderby entity0Al1.ID_OBJETO_CONTEUDO ascending, entity0Al2.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuarioLayout()		
	            {
	            
                DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioFilial.
	    public IQueryable<TcsUsuarioFilial> GetPagedTcsUsuarioFilial(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFilial> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_USUARIO
                orderby entity0.ID_TCS_USUARIO_FILIAL ascending
	            
	            	
	            select new TcsUsuarioFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdTcsUsuarioFilial = entity0.ID_TCS_USUARIO_FILIAL
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioPerfilCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_PERFIL
                  let entityAl2 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioRegraModuloCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioRegraTransacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioRegraColunaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioRegraColuna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_REGRA_COLUNA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioBandeiraRedeCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_USUARIO
                  let entityAl1 = entity.TBC_BANDEIRA_REDE
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioLayoutCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_LAYOUT_USUARIO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_LAYOUT
                  let entityAl2 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioFilialCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TBC_FILIAL
                  let entityAl2 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAcessoLocal.
	    public IQueryable<TcsUsuarioAcessoLocal> GetPagedTcsUsuarioAcessoLocal(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcessoLocal));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoLocal> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuarioAcessoLocal()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAcessoLocalCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcessoLocal));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioPerfilP.
	    public IQueryable<TcsUsuarioPerfilP> GetPagedTcsUsuarioPerfilP(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfilP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfilP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
                orderby entity0.ID_TCS_USUARIO_PERFIL ascending
	            
	            	
	            select new TcsUsuarioPerfilP()		
	            {
	            
                IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioPerfilPCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfilP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_PERFIL
                  let entityAl2 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsUsuario.
	    public void UpdateTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuario.
	    public void InsertTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuario.
	    public void DeleteTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioPerfil.
	    public void UpdateTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuario); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioPerfil.
	    public void InsertTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuario);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioPerfil.
	    public void DeleteTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuario);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioRegraModulo.
	    public void UpdateTcsUsuarioRegraModulo(TcsUsuarioRegraModulo entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuario); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioRegraModulo.
	    public void InsertTcsUsuarioRegraModulo(TcsUsuarioRegraModulo entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuario);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioRegraModulo.
	    public void DeleteTcsUsuarioRegraModulo(TcsUsuarioRegraModulo entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuario);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioRegraTransacao.
	    public void UpdateTcsUsuarioRegraTransacao(TcsUsuarioRegraTransacao entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuario); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioRegraTransacao.
	    public void InsertTcsUsuarioRegraTransacao(TcsUsuarioRegraTransacao entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuario);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioRegraTransacao.
	    public void DeleteTcsUsuarioRegraTransacao(TcsUsuarioRegraTransacao entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuario);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioRegraColuna.
	    public void UpdateTcsUsuarioRegraColuna(TcsUsuarioRegraColuna entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuario); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioRegraColuna.
	    public void InsertTcsUsuarioRegraColuna(TcsUsuarioRegraColuna entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuario);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioRegraColuna.
	    public void DeleteTcsUsuarioRegraColuna(TcsUsuarioRegraColuna entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuario);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioBandeiraRede.
	    public void UpdateTcsUsuarioBandeiraRede(TcsUsuarioBandeiraRede entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuario); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioBandeiraRede.
	    public void InsertTcsUsuarioBandeiraRede(TcsUsuarioBandeiraRede entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuario);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioBandeiraRede.
	    public void DeleteTcsUsuarioBandeiraRede(TcsUsuarioBandeiraRede entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuario);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioLayout.
	    public void UpdateTcsUsuarioLayout(TcsUsuarioLayout entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuario); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioLayout.
	    public void InsertTcsUsuarioLayout(TcsUsuarioLayout entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuario);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioLayout.
	    public void DeleteTcsUsuarioLayout(TcsUsuarioLayout entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuario);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioFilial.
	    public void UpdateTcsUsuarioFilial(TcsUsuarioFilial entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuario); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioFilial.
	    public void InsertTcsUsuarioFilial(TcsUsuarioFilial entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuario);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioFilial.
	    public void DeleteTcsUsuarioFilial(TcsUsuarioFilial entity)
	    {



	
	        if (entity.TcsUsuario.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuario) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuario);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAcessoLocal.
	    public void UpdateTcsUsuarioAcessoLocal(TcsUsuarioAcessoLocal entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAcessoLocal.
	    public void InsertTcsUsuarioAcessoLocal(TcsUsuarioAcessoLocal entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAcessoLocal.
	    public void DeleteTcsUsuarioAcessoLocal(TcsUsuarioAcessoLocal entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioPerfilP.
	    public void UpdateTcsUsuarioPerfilP(TcsUsuarioPerfilP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioPerfilP.
	    public void InsertTcsUsuarioPerfilP(TcsUsuarioPerfilP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioPerfilP.
	    public void DeleteTcsUsuarioPerfilP(TcsUsuarioPerfilP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}