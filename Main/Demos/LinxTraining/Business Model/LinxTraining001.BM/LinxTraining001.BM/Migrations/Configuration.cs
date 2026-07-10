	


namespace LinxTraining001.BM.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Migrations.Sql;
    using System.Diagnostics;
    using System.Linq;
    using Linx.Tools;
	using System.Data.Entity.SqlServer;

    internal sealed class Configuration : DbMigrationsConfiguration<BMLFWTraining>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;    
			AutomaticMigrationDataLossAllowed = false;
            SetSqlGenerator("System.Data.SqlClient", new SqlMigrator());   
			    
        }
    }

	internal sealed class SqlTableMigrator
    {
        private List<CreateIndexOperation> _indexes = new List<CreateIndexOperation>();
        public List<CreateIndexOperation> Indexes { get { return _indexes; } }

        private Dictionary<string, string> _fks = new Dictionary<string, string>();
        public Dictionary<string, string> Fks { get { return _fks; } }

        private Dictionary<string, string> _defauls = new Dictionary<string, string>();
        public Dictionary<string, string> Defauls { get { return _defauls; } }

		private List<string> _nullables = new List<string>();
        public List<string> Nullables { get { return _nullables; } }

		private Dictionary<string, string> _primaryKeys = new Dictionary<string, string>();
        public Dictionary<string, string> PrimaryKeys { get { return _primaryKeys; } }
		
        private List<string> _views = new List<string>();
        public List<string> Views { get { return _views; } }


        public void AdjustFK(AddForeignKeyOperation addForeignKeyOperation)
        {
            string key = addForeignKeyOperation.PrincipalTable + "." +
                String.Join(".", addForeignKeyOperation.PrincipalColumns.OrderBy(e => e).ToArray()) + "." +                
                addForeignKeyOperation.DependentTable + "." +
                String.Join(".", addForeignKeyOperation.DependentColumns.OrderBy(e => e).ToArray());

            if (Fks.ContainsKey(key))
            {
                string value = Fks[key];
                addForeignKeyOperation.Name = value.Left(",");
                addForeignKeyOperation.CascadeDelete = value.Right(",") == "true";
            }
        }

    }

	internal sealed class SqlMigrator : SqlServerMigrationSqlGenerator
    {
        private SqlTableMigrator _tableMigrator;
        public SqlMigrator()
            : base()
        {
            _tableMigrator = new SqlTableMigrator();
            CreateIndexOperation createIndexOperation;
            //Add Indexes
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.CountryRegionCurrency", IsUnique = false, Name = "IX_CountryRegionCurrency_CurrencyCode" };
            createIndexOperation.Columns.Add("CurrencyCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.CountryRegionCurrency", IsUnique = false, Name = "IX_CurrencyCode" };
            createIndexOperation.Columns.Add("CurrencyCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.CreditCard", IsUnique = true, Name = "AK_CreditCard_CardNumber" };
            createIndexOperation.Columns.Add("CardNumber");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.Currency", IsUnique = true, Name = "AK_Currency_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.CurrencyRate", IsUnique = true, Name = "AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode" };
            createIndexOperation.Columns.Add("CurrencyRateDate");
            createIndexOperation.Columns.Add("FromCurrencyCode");
            createIndexOperation.Columns.Add("ToCurrencyCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.CurrencyRate", IsUnique = false, Name = "IX_FromCurrencyCode" };
            createIndexOperation.Columns.Add("FromCurrencyCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.CurrencyRate", IsUnique = false, Name = "IX_ToCurrencyCode" };
            createIndexOperation.Columns.Add("ToCurrencyCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.Customer", IsUnique = false, Name = "IX_StoreID" };
            createIndexOperation.Columns.Add("StoreID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.Customer", IsUnique = false, Name = "IX_Customer_TerritoryID" };
            createIndexOperation.Columns.Add("TerritoryID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.Customer", IsUnique = false, Name = "IX_TerritoryID" };
            createIndexOperation.Columns.Add("TerritoryID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.Customer", IsUnique = true, Name = "AK_Customer_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.PersonCreditCard", IsUnique = false, Name = "IX_CreditCardID" };
            createIndexOperation.Columns.Add("CreditCardID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderDetail", IsUnique = false, Name = "IX_SalesOrderID" };
            createIndexOperation.Columns.Add("SalesOrderID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderDetail", IsUnique = true, Name = "AK_SalesOrderDetail_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderDetail", IsUnique = false, Name = "IX_SpecialOfferID_ProductID" };
            createIndexOperation.Columns.Add("SpecialOfferID");
            createIndexOperation.Columns.Add("ProductID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderDetail", IsUnique = false, Name = "IX_SalesOrderDetail_ProductID" };
            createIndexOperation.Columns.Add("ProductID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderHeader", IsUnique = false, Name = "IX_CustomerID" };
            createIndexOperation.Columns.Add("CustomerID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderHeader", IsUnique = false, Name = "IX_SalesOrderHeader_CustomerID" };
            createIndexOperation.Columns.Add("CustomerID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderHeader", IsUnique = false, Name = "IX_SalesOrderHeader_SalesPersonID" };
            createIndexOperation.Columns.Add("SalesPersonID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderHeader", IsUnique = false, Name = "IX_SalesPersonID" };
            createIndexOperation.Columns.Add("SalesPersonID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderHeader", IsUnique = false, Name = "IX_CurrencyRateID" };
            createIndexOperation.Columns.Add("CurrencyRateID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderHeader", IsUnique = true, Name = "AK_SalesOrderHeader_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesOrderHeaderSalesReason", IsUnique = false, Name = "IX_SalesReasonID" };
            createIndexOperation.Columns.Add("SalesReasonID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesPerson", IsUnique = true, Name = "AK_SalesPerson_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesPersonQuotaHistory", IsUnique = true, Name = "AK_SalesPersonQuotaHistory_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesPersonQuotaHistory", IsUnique = false, Name = "IX_BusinessEntityID" };
            createIndexOperation.Columns.Add("BusinessEntityID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesTerritory", IsUnique = true, Name = "AK_SalesTerritory_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesTerritory", IsUnique = true, Name = "AK_SalesTerritory_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SalesTerritoryHistory", IsUnique = true, Name = "AK_SalesTerritoryHistory_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SpecialOffer", IsUnique = true, Name = "AK_SpecialOffer_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SpecialOfferProduct", IsUnique = true, Name = "AK_SpecialOfferProduct_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SpecialOfferProduct", IsUnique = false, Name = "IX_SpecialOfferProduct_ProductID" };
            createIndexOperation.Columns.Add("ProductID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.SpecialOfferProduct", IsUnique = false, Name = "IX_SpecialOfferID" };
            createIndexOperation.Columns.Add("SpecialOfferID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.Store", IsUnique = false, Name = "IX_Store_SalesPersonID" };
            createIndexOperation.Columns.Add("SalesPersonID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Sales.Store", IsUnique = true, Name = "AK_Store_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation(new { IsClustered = true }) { Table = "Production.BillOfMaterials", IsUnique = true, Name = "AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate" };
            createIndexOperation.Columns.Add("ProductAssemblyID");
            createIndexOperation.Columns.Add("ComponentID");
            createIndexOperation.Columns.Add("StartDate");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.BillOfMaterials", IsUnique = false, Name = "IX_BillOfMaterials_UnitMeasureCode" };
            createIndexOperation.Columns.Add("UnitMeasureCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.BillOfMaterials", IsUnique = false, Name = "IX_UnitMeasureCode" };
            createIndexOperation.Columns.Add("UnitMeasureCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.BillOfMaterials", IsUnique = false, Name = "IX_ComponentID" };
            createIndexOperation.Columns.Add("ComponentID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.BillOfMaterials", IsUnique = false, Name = "IX_ProductAssemblyID" };
            createIndexOperation.Columns.Add("ProductAssemblyID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Culture", IsUnique = true, Name = "AK_Culture_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Document", IsUnique = true, Name = "AK_Document_DocumentLevel_DocumentNode" };
            createIndexOperation.Columns.Add("DocumentLevel");
            createIndexOperation.Columns.Add("DocumentNode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Document", IsUnique = false, Name = "IX_Document_FileName_Revision" };
            createIndexOperation.Columns.Add("FileName");
            createIndexOperation.Columns.Add("Revision");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Document", IsUnique = true, Name = "AK_Document_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Document", IsUnique = true, Name = "UQ__Document__F73921F793071A63" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Location", IsUnique = true, Name = "AK_Location_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Product", IsUnique = true, Name = "AK_Product_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Product", IsUnique = true, Name = "AK_Product_ProductNumber" };
            createIndexOperation.Columns.Add("ProductNumber");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Product", IsUnique = false, Name = "IX_SizeUnitMeasureCode" };
            createIndexOperation.Columns.Add("SizeUnitMeasureCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Product", IsUnique = false, Name = "IX_WeightUnitMeasureCode" };
            createIndexOperation.Columns.Add("WeightUnitMeasureCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Product", IsUnique = false, Name = "IX_ProductSubcategoryID" };
            createIndexOperation.Columns.Add("ProductSubcategoryID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Product", IsUnique = false, Name = "IX_ProductModelID" };
            createIndexOperation.Columns.Add("ProductModelID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.Product", IsUnique = true, Name = "AK_Product_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductCategory", IsUnique = true, Name = "AK_ProductCategory_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductCategory", IsUnique = true, Name = "AK_ProductCategory_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductCostHistory", IsUnique = false, Name = "IX_ProductID" };
            createIndexOperation.Columns.Add("ProductID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductDescription", IsUnique = true, Name = "AK_ProductDescription_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductDocument", IsUnique = false, Name = "IX_DocumentNode" };
            createIndexOperation.Columns.Add("DocumentNode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductInventory", IsUnique = false, Name = "IX_LocationID" };
            createIndexOperation.Columns.Add("LocationID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductModel", IsUnique = true, Name = "AK_ProductModel_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductModel", IsUnique = true, Name = "AK_ProductModel_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductModelIllustration", IsUnique = false, Name = "IX_IllustrationID" };
            createIndexOperation.Columns.Add("IllustrationID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductModelProductDescriptionCulture", IsUnique = false, Name = "IX_CultureID" };
            createIndexOperation.Columns.Add("CultureID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductModelProductDescriptionCulture", IsUnique = false, Name = "IX_ProductDescriptionID" };
            createIndexOperation.Columns.Add("ProductDescriptionID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductProductPhoto", IsUnique = false, Name = "IX_ProductPhotoID" };
            createIndexOperation.Columns.Add("ProductPhotoID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductReview", IsUnique = false, Name = "IX_ProductReview_ProductID_Name" };
            createIndexOperation.Columns.Add("ProductID");
            createIndexOperation.Columns.Add("ReviewerName");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductSubcategory", IsUnique = false, Name = "IX_ProductCategoryID" };
            createIndexOperation.Columns.Add("ProductCategoryID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductSubcategory", IsUnique = true, Name = "AK_ProductSubcategory_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ProductSubcategory", IsUnique = true, Name = "AK_ProductSubcategory_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.ScrapReason", IsUnique = true, Name = "AK_ScrapReason_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.TransactionHistory", IsUnique = false, Name = "IX_TransactionHistory_ProductID" };
            createIndexOperation.Columns.Add("ProductID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.TransactionHistory", IsUnique = false, Name = "IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID" };
            createIndexOperation.Columns.Add("ReferenceOrderID");
            createIndexOperation.Columns.Add("ReferenceOrderLineID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.UnitMeasure", IsUnique = true, Name = "AK_UnitMeasure_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.WorkOrder", IsUnique = false, Name = "IX_WorkOrder_ProductID" };
            createIndexOperation.Columns.Add("ProductID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.WorkOrder", IsUnique = false, Name = "IX_ScrapReasonID" };
            createIndexOperation.Columns.Add("ScrapReasonID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.WorkOrder", IsUnique = false, Name = "IX_WorkOrder_ScrapReasonID" };
            createIndexOperation.Columns.Add("ScrapReasonID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.WorkOrderRouting", IsUnique = false, Name = "IX_WorkOrderRouting_ProductID" };
            createIndexOperation.Columns.Add("ProductID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Production.WorkOrderRouting", IsUnique = false, Name = "IX_WorkOrderID" };
            createIndexOperation.Columns.Add("WorkOrderID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.Address", IsUnique = true, Name = "IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode" };
            createIndexOperation.Columns.Add("AddressLine1");
            createIndexOperation.Columns.Add("AddressLine2");
            createIndexOperation.Columns.Add("City");
            createIndexOperation.Columns.Add("StateProvinceID");
            createIndexOperation.Columns.Add("PostalCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.Address", IsUnique = false, Name = "IX_Address_StateProvinceID" };
            createIndexOperation.Columns.Add("StateProvinceID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.Address", IsUnique = false, Name = "IX_StateProvinceID" };
            createIndexOperation.Columns.Add("StateProvinceID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.Address", IsUnique = true, Name = "AK_Address_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.AddressType", IsUnique = true, Name = "AK_AddressType_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.AddressType", IsUnique = true, Name = "AK_AddressType_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntity", IsUnique = true, Name = "AK_BusinessEntity_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityAddress", IsUnique = true, Name = "AK_BusinessEntityAddress_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityAddress", IsUnique = false, Name = "IX_BusinessEntityAddress_AddressID" };
            createIndexOperation.Columns.Add("AddressID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityAddress", IsUnique = false, Name = "IX_BusinessEntityAddress_AddressTypeID" };
            createIndexOperation.Columns.Add("AddressTypeID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityAddress", IsUnique = false, Name = "IX_AddressID" };
            createIndexOperation.Columns.Add("AddressID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityAddress", IsUnique = false, Name = "IX_AddressTypeID" };
            createIndexOperation.Columns.Add("AddressTypeID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityContact", IsUnique = true, Name = "AK_BusinessEntityContact_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityContact", IsUnique = false, Name = "IX_BusinessEntityContact_PersonID" };
            createIndexOperation.Columns.Add("PersonID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityContact", IsUnique = false, Name = "IX_BusinessEntityContact_ContactTypeID" };
            createIndexOperation.Columns.Add("ContactTypeID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityContact", IsUnique = false, Name = "IX_ContactTypeID" };
            createIndexOperation.Columns.Add("ContactTypeID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.BusinessEntityContact", IsUnique = false, Name = "IX_PersonID" };
            createIndexOperation.Columns.Add("PersonID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.ContactType", IsUnique = true, Name = "AK_ContactType_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.CountryRegion", IsUnique = true, Name = "AK_CountryRegion_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.Person", IsUnique = false, Name = "IX_Person_LastName_FirstName_MiddleName" };
            createIndexOperation.Columns.Add("LastName");
            createIndexOperation.Columns.Add("FirstName");
            createIndexOperation.Columns.Add("MiddleName");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.Person", IsUnique = true, Name = "AK_Person_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.PersonPhone", IsUnique = false, Name = "IX_PersonPhone_PhoneNumber" };
            createIndexOperation.Columns.Add("PhoneNumber");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.PersonPhone", IsUnique = false, Name = "IX_PhoneNumberTypeID" };
            createIndexOperation.Columns.Add("PhoneNumberTypeID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.StateProvince", IsUnique = true, Name = "AK_StateProvince_StateProvinceCode_CountryRegionCode" };
            createIndexOperation.Columns.Add("StateProvinceCode");
            createIndexOperation.Columns.Add("CountryRegionCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.StateProvince", IsUnique = false, Name = "IX_CountryRegionCode" };
            createIndexOperation.Columns.Add("CountryRegionCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.StateProvince", IsUnique = true, Name = "AK_StateProvince_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Person.StateProvince", IsUnique = true, Name = "AK_StateProvince_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.ProductVendor", IsUnique = false, Name = "IX_ProductVendor_UnitMeasureCode" };
            createIndexOperation.Columns.Add("UnitMeasureCode");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.ProductVendor", IsUnique = false, Name = "IX_ProductVendor_BusinessEntityID" };
            createIndexOperation.Columns.Add("BusinessEntityID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.PurchaseOrderDetail", IsUnique = false, Name = "IX_PurchaseOrderID" };
            createIndexOperation.Columns.Add("PurchaseOrderID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.PurchaseOrderDetail", IsUnique = false, Name = "IX_PurchaseOrderDetail_ProductID" };
            createIndexOperation.Columns.Add("ProductID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.PurchaseOrderHeader", IsUnique = false, Name = "IX_PurchaseOrderHeader_EmployeeID" };
            createIndexOperation.Columns.Add("EmployeeID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.PurchaseOrderHeader", IsUnique = false, Name = "IX_PurchaseOrderHeader_VendorID" };
            createIndexOperation.Columns.Add("VendorID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.PurchaseOrderHeader", IsUnique = false, Name = "IX_VendorID" };
            createIndexOperation.Columns.Add("VendorID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.PurchaseOrderHeader", IsUnique = false, Name = "IX_ShipMethodID" };
            createIndexOperation.Columns.Add("ShipMethodID");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.ShipMethod", IsUnique = true, Name = "AK_ShipMethod_Name" };
            createIndexOperation.Columns.Add("Name");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.ShipMethod", IsUnique = true, Name = "AK_ShipMethod_rowguid" };
            createIndexOperation.Columns.Add("rowguid");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "Purchasing.Vendor", IsUnique = true, Name = "AK_Vendor_AccountNumber" };
            createIndexOperation.Columns.Add("AccountNumber");
            _tableMigrator.Indexes.Add(createIndexOperation);
            //Add Primary Keys
            _tableMigrator.PrimaryKeys["Sales.CountryRegionCurrency"] = "PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode";
            _tableMigrator.PrimaryKeys["Sales.CreditCard"] = "PK_CreditCard_CreditCardID";
            _tableMigrator.PrimaryKeys["Sales.Currency"] = "PK_Currency_CurrencyCode";
            _tableMigrator.PrimaryKeys["Sales.CurrencyRate"] = "PK_CurrencyRate_CurrencyRateID";
            _tableMigrator.PrimaryKeys["Sales.Customer"] = "PK_Customer_CustomerID";
            _tableMigrator.PrimaryKeys["Sales.PersonCreditCard"] = "PK_PersonCreditCard_BusinessEntityID_CreditCardID";
            _tableMigrator.PrimaryKeys["Sales.SalesOrderDetail"] = "PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID";
            _tableMigrator.PrimaryKeys["Sales.SalesOrderHeader"] = "PK_SalesOrderHeader_SalesOrderID";
            _tableMigrator.PrimaryKeys["Sales.SalesOrderHeaderSalesReason"] = "PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID";
            _tableMigrator.PrimaryKeys["Sales.SalesPerson"] = "PK_SalesPerson_BusinessEntityID";
            _tableMigrator.PrimaryKeys["Sales.SalesPersonQuotaHistory"] = "PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate";
            _tableMigrator.PrimaryKeys["Sales.SalesReason"] = "PK_SalesReason_SalesReasonID";
            _tableMigrator.PrimaryKeys["Sales.SalesTerritory"] = "PK_SalesTerritory_TerritoryID";
            _tableMigrator.PrimaryKeys["Sales.SalesTerritoryHistory"] = "PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID";
            _tableMigrator.PrimaryKeys["Sales.SpecialOffer"] = "PK_SpecialOffer_SpecialOfferID";
            _tableMigrator.PrimaryKeys["Sales.SpecialOfferProduct"] = "PK_SpecialOfferProduct_SpecialOfferID_ProductID";
            _tableMigrator.PrimaryKeys["Sales.Store"] = "PK_Store_BusinessEntityID";
            _tableMigrator.PrimaryKeys["Production.BillOfMaterials"] = "PK_BillOfMaterials_BillOfMaterialsID__NC__";
            _tableMigrator.PrimaryKeys["Production.Culture"] = "PK_Culture_CultureID";
            _tableMigrator.PrimaryKeys["Production.Document"] = "PK_Document_DocumentNode";
            _tableMigrator.PrimaryKeys["Production.Illustration"] = "PK_Illustration_IllustrationID";
            _tableMigrator.PrimaryKeys["Production.Location"] = "PK_Location_LocationID";
            _tableMigrator.PrimaryKeys["Production.Product"] = "PK_Product_ProductID";
            _tableMigrator.PrimaryKeys["Production.ProductCategory"] = "PK_ProductCategory_ProductCategoryID";
            _tableMigrator.PrimaryKeys["Production.ProductCostHistory"] = "PK_ProductCostHistory_ProductID_StartDate";
            _tableMigrator.PrimaryKeys["Production.ProductDescription"] = "PK_ProductDescription_ProductDescriptionID";
            _tableMigrator.PrimaryKeys["Production.ProductDocument"] = "PK_ProductDocument_ProductID_DocumentNode";
            _tableMigrator.PrimaryKeys["Production.ProductInventory"] = "PK_ProductInventory_ProductID_LocationID";
            _tableMigrator.PrimaryKeys["Production.ProductListPriceHistory"] = "PK_ProductListPriceHistory_ProductID_StartDate";
            _tableMigrator.PrimaryKeys["Production.ProductModel"] = "PK_ProductModel_ProductModelID";
            _tableMigrator.PrimaryKeys["Production.ProductModelIllustration"] = "PK_ProductModelIllustration_ProductModelID_IllustrationID";
            _tableMigrator.PrimaryKeys["Production.ProductModelProductDescriptionCulture"] = "PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID";
            _tableMigrator.PrimaryKeys["Production.ProductPhoto"] = "PK_ProductPhoto_ProductPhotoID";
            _tableMigrator.PrimaryKeys["Production.ProductProductPhoto"] = "PK_ProductProductPhoto_ProductID_ProductPhotoID__NC__";
            _tableMigrator.PrimaryKeys["Production.ProductReview"] = "PK_ProductReview_ProductReviewID";
            _tableMigrator.PrimaryKeys["Production.ProductSubcategory"] = "PK_ProductSubcategory_ProductSubcategoryID";
            _tableMigrator.PrimaryKeys["Production.ScrapReason"] = "PK_ScrapReason_ScrapReasonID";
            _tableMigrator.PrimaryKeys["Production.TransactionHistory"] = "PK_TransactionHistory_TransactionID";
            _tableMigrator.PrimaryKeys["Production.UnitMeasure"] = "PK_UnitMeasure_UnitMeasureCode";
            _tableMigrator.PrimaryKeys["Production.WorkOrder"] = "PK_WorkOrder_WorkOrderID";
            _tableMigrator.PrimaryKeys["Production.WorkOrderRouting"] = "PK_WorkOrderRouting_WorkOrderID_ProductID_OperationSequence";
            _tableMigrator.PrimaryKeys["Person.Address"] = "PK_Address_AddressID";
            _tableMigrator.PrimaryKeys["Person.AddressType"] = "PK_AddressType_AddressTypeID";
            _tableMigrator.PrimaryKeys["Person.BusinessEntity"] = "PK_BusinessEntity_BusinessEntityID";
            _tableMigrator.PrimaryKeys["Person.BusinessEntityAddress"] = "PK_BusinessEntityAddress_BusinessEntityID_AddressID_AddressTypeID";
            _tableMigrator.PrimaryKeys["Person.BusinessEntityContact"] = "PK_BusinessEntityContact_BusinessEntityID_PersonID_ContactTypeID";
            _tableMigrator.PrimaryKeys["Person.ContactType"] = "PK_ContactType_ContactTypeID";
            _tableMigrator.PrimaryKeys["Person.CountryRegion"] = "PK_CountryRegion_CountryRegionCode";
            _tableMigrator.PrimaryKeys["Person.EmailAddress"] = "PK_EmailAddress_BusinessEntityID_EmailAddressID";
            _tableMigrator.PrimaryKeys["Person.Password"] = "PK_Password_BusinessEntityID";
            _tableMigrator.PrimaryKeys["Person.Person"] = "PK_Person_BusinessEntityID";
            _tableMigrator.PrimaryKeys["Person.PersonPhone"] = "PK_PersonPhone_BusinessEntityID_PhoneNumber_PhoneNumberTypeID";
            _tableMigrator.PrimaryKeys["Person.PhoneNumberType"] = "PK_PhoneNumberType_PhoneNumberTypeID";
            _tableMigrator.PrimaryKeys["Person.StateProvince"] = "PK_StateProvince_StateProvinceID";
            _tableMigrator.PrimaryKeys["Purchasing.ProductVendor"] = "PK_ProductVendor_ProductID_BusinessEntityID";
            _tableMigrator.PrimaryKeys["Purchasing.PurchaseOrderDetail"] = "PK_PurchaseOrderDetail_PurchaseOrderID_PurchaseOrderDetailID";
            _tableMigrator.PrimaryKeys["Purchasing.PurchaseOrderHeader"] = "PK_PurchaseOrderHeader_PurchaseOrderID";
            _tableMigrator.PrimaryKeys["Purchasing.ShipMethod"] = "PK_ShipMethod_ShipMethodID";
            _tableMigrator.PrimaryKeys["Purchasing.Vendor"] = "PK_Vendor_BusinessEntityID";
            //Add Defaults
            _tableMigrator.Defauls.Add("Sales.CountryRegionCurrency.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.CreditCard.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.Currency.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.CurrencyRate.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.Customer.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.Customer.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.PersonCreditCard.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesOrderDetail.UnitPriceDiscount", "((0.0))");
            _tableMigrator.Defauls.Add("Sales.SalesOrderDetail.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.SalesOrderDetail.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.RevisionNumber", "((0))");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.OrderDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.Status", "((1))");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.OnlineOrderFlag", "((1))");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.SubTotal", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.TaxAmt", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.Freight", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeader.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesOrderHeaderSalesReason.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesPerson.Bonus", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesPerson.CommissionPct", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesPerson.SalesYTD", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesPerson.SalesLastYear", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesPerson.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.SalesPerson.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesPersonQuotaHistory.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.SalesPersonQuotaHistory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesReason.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesTerritory.SalesYTD", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesTerritory.SalesLastYear", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesTerritory.CostYTD", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesTerritory.CostLastYear", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SalesTerritory.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.SalesTerritory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SalesTerritoryHistory.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.SalesTerritoryHistory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SpecialOffer.DiscountPct", "((0.00))");
            _tableMigrator.Defauls.Add("Sales.SpecialOffer.MinQty", "((0))");
            _tableMigrator.Defauls.Add("Sales.SpecialOffer.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.SpecialOffer.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.SpecialOfferProduct.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.SpecialOfferProduct.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Sales.Store.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Sales.Store.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.BillOfMaterials.StartDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.BillOfMaterials.PerAssemblyQty", "((1.00))");
            _tableMigrator.Defauls.Add("Production.BillOfMaterials.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.Culture.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.Document.FolderFlag", "((0))");
            _tableMigrator.Defauls.Add("Production.Document.ChangeNumber", "((0))");
            _tableMigrator.Defauls.Add("Production.Document.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Production.Document.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.Illustration.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.Location.CostRate", "((0.00))");
            _tableMigrator.Defauls.Add("Production.Location.Availability", "((0.00))");
            _tableMigrator.Defauls.Add("Production.Location.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.Product.MakeFlag", "((1))");
            _tableMigrator.Defauls.Add("Production.Product.FinishedGoodsFlag", "((1))");
            _tableMigrator.Defauls.Add("Production.Product.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Production.Product.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductCategory.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Production.ProductCategory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductCostHistory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductDescription.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Production.ProductDescription.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductDocument.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductInventory.Quantity", "((0))");
            _tableMigrator.Defauls.Add("Production.ProductInventory.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Production.ProductInventory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductListPriceHistory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductModel.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Production.ProductModel.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductModelIllustration.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductModelProductDescriptionCulture.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductPhoto.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductProductPhoto.Primary", "((0))");
            _tableMigrator.Defauls.Add("Production.ProductProductPhoto.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductReview.ReviewDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductReview.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ProductSubcategory.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Production.ProductSubcategory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.ScrapReason.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.TransactionHistory.ReferenceOrderLineID", "((0))");
            _tableMigrator.Defauls.Add("Production.TransactionHistory.TransactionDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.TransactionHistory.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.UnitMeasure.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.WorkOrder.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Production.WorkOrderRouting.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.Address.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.Address.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.AddressType.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.AddressType.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.BusinessEntity.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.BusinessEntity.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.BusinessEntityAddress.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.BusinessEntityAddress.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.BusinessEntityContact.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.BusinessEntityContact.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.ContactType.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.CountryRegion.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.EmailAddress.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.EmailAddress.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.Password.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.Password.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.Person.NameStyle", "((0))");
            _tableMigrator.Defauls.Add("Person.Person.EmailPromotion", "((0))");
            _tableMigrator.Defauls.Add("Person.Person.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.Person.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.PersonPhone.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.PhoneNumberType.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Person.StateProvince.IsOnlyStateProvinceFlag", "((1))");
            _tableMigrator.Defauls.Add("Person.StateProvince.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Person.StateProvince.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Purchasing.ProductVendor.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Purchasing.PurchaseOrderDetail.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Purchasing.PurchaseOrderHeader.RevisionNumber", "((0))");
            _tableMigrator.Defauls.Add("Purchasing.PurchaseOrderHeader.Status", "((1))");
            _tableMigrator.Defauls.Add("Purchasing.PurchaseOrderHeader.OrderDate", "(getdate())");
            _tableMigrator.Defauls.Add("Purchasing.PurchaseOrderHeader.SubTotal", "((0.00))");
            _tableMigrator.Defauls.Add("Purchasing.PurchaseOrderHeader.TaxAmt", "((0.00))");
            _tableMigrator.Defauls.Add("Purchasing.PurchaseOrderHeader.Freight", "((0.00))");
            _tableMigrator.Defauls.Add("Purchasing.PurchaseOrderHeader.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Purchasing.ShipMethod.ShipBase", "((0.00))");
            _tableMigrator.Defauls.Add("Purchasing.ShipMethod.ShipRate", "((0.00))");
            _tableMigrator.Defauls.Add("Purchasing.ShipMethod.rowguid", "(newid())");
            _tableMigrator.Defauls.Add("Purchasing.ShipMethod.ModifiedDate", "(getdate())");
            _tableMigrator.Defauls.Add("Purchasing.Vendor.PreferredVendorStatus", "((1))");
            _tableMigrator.Defauls.Add("Purchasing.Vendor.ActiveFlag", "((1))");
            _tableMigrator.Defauls.Add("Purchasing.Vendor.ModifiedDate", "(getdate())");
            //Add Nullables
            _tableMigrator.Nullables.Add("Sales.Customer.StoreID");
            _tableMigrator.Nullables.Add("Sales.Customer.TerritoryID");
            _tableMigrator.Nullables.Add("Sales.SalesOrderHeader.SalesPersonID");
            _tableMigrator.Nullables.Add("Sales.SalesOrderHeader.TerritoryID");
            _tableMigrator.Nullables.Add("Sales.SalesOrderHeader.CreditCardID");
            _tableMigrator.Nullables.Add("Sales.SalesOrderHeader.CurrencyRateID");
            _tableMigrator.Nullables.Add("Sales.SalesPerson.TerritoryID");
            _tableMigrator.Nullables.Add("Sales.Store.SalesPersonID");
            _tableMigrator.Nullables.Add("Production.BillOfMaterials.ProductAssemblyID");
            _tableMigrator.Nullables.Add("Production.Product.SizeUnitMeasureCode");
            _tableMigrator.Nullables.Add("Production.Product.WeightUnitMeasureCode");
            _tableMigrator.Nullables.Add("Production.Product.ProductSubcategoryID");
            _tableMigrator.Nullables.Add("Production.Product.ProductModelID");
            _tableMigrator.Nullables.Add("Production.WorkOrder.ScrapReasonID");
            //Add Foreign Keys
            _tableMigrator.Fks.Add("Sales.Currency.CurrencyCode.Sales.CountryRegionCurrency.CurrencyCode", "FK_CountryRegionCurrency_Currency_CurrencyCode,false");
            _tableMigrator.Fks.Add("Sales.Currency.CurrencyCode.Sales.CurrencyRate.FromCurrencyCode", "FK_CurrencyRate_Currency_FromCurrencyCode,false");
            _tableMigrator.Fks.Add("Sales.Currency.CurrencyCode.Sales.CurrencyRate.ToCurrencyCode", "FK_CurrencyRate_Currency_ToCurrencyCode,false");
            _tableMigrator.Fks.Add("Sales.SalesTerritory.TerritoryID.Sales.Customer.TerritoryID", "FK_Customer_SalesTerritory_TerritoryID,false");
            _tableMigrator.Fks.Add("Sales.Store.BusinessEntityID.Sales.Customer.StoreID", "FK_Customer_Store_StoreID,false");
            _tableMigrator.Fks.Add("Sales.CreditCard.CreditCardID.Sales.PersonCreditCard.CreditCardID", "FK_PersonCreditCard_CreditCard_CreditCardID,false");
            _tableMigrator.Fks.Add("Sales.SalesOrderHeader.SalesOrderID.Sales.SalesOrderDetail.SalesOrderID", "FK_SalesOrderDetail_SalesOrderHeader_SalesOrderID,true");
            _tableMigrator.Fks.Add("Sales.SpecialOfferProduct.ProductID.SpecialOfferID.Sales.SalesOrderDetail.ProductID.SpecialOfferID", "FK_SalesOrderDetail_e53464,false");
            _tableMigrator.Fks.Add("Sales.CreditCard.CreditCardID.Sales.SalesOrderHeader.CreditCardID", "FK_SalesOrderHeader_CreditCard_CreditCardID,false");
            _tableMigrator.Fks.Add("Sales.CurrencyRate.CurrencyRateID.Sales.SalesOrderHeader.CurrencyRateID", "FK_SalesOrderHeader_CurrencyRate_CurrencyRateID,false");
            _tableMigrator.Fks.Add("Sales.Customer.CustomerID.Sales.SalesOrderHeader.CustomerID", "FK_SalesOrderHeader_Customer_CustomerID,false");
            _tableMigrator.Fks.Add("Sales.SalesPerson.BusinessEntityID.Sales.SalesOrderHeader.SalesPersonID", "FK_SalesOrderHeader_SalesPerson_SalesPersonID,false");
            _tableMigrator.Fks.Add("Sales.SalesTerritory.TerritoryID.Sales.SalesOrderHeader.TerritoryID", "FK_SalesOrderHeader_SalesTerritory_TerritoryID,false");
            _tableMigrator.Fks.Add("Sales.SalesOrderHeader.SalesOrderID.Sales.SalesOrderHeaderSalesReason.SalesOrderID", "FK_SalesOrderHeaderSalesReason_SalesOrderHeader_SalesOrderID,true");
            _tableMigrator.Fks.Add("Sales.SalesReason.SalesReasonID.Sales.SalesOrderHeaderSalesReason.SalesReasonID", "FK_SalesOrderHeaderSalesReason_SalesReason_SalesReasonID,false");
            _tableMigrator.Fks.Add("Sales.SalesTerritory.TerritoryID.Sales.SalesPerson.TerritoryID", "FK_SalesPerson_SalesTerritory_TerritoryID,false");
            _tableMigrator.Fks.Add("Sales.SalesPerson.BusinessEntityID.Sales.SalesPersonQuotaHistory.BusinessEntityID", "FK_SalesPersonQuotaHistory_SalesPerson_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Sales.SalesPerson.BusinessEntityID.Sales.SalesTerritoryHistory.BusinessEntityID", "FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Sales.SalesTerritory.TerritoryID.Sales.SalesTerritoryHistory.TerritoryID", "FK_SalesTerritoryHistory_SalesTerritory_TerritoryID,false");
            _tableMigrator.Fks.Add("Sales.SpecialOffer.SpecialOfferID.Sales.SpecialOfferProduct.SpecialOfferID", "FK_SpecialOfferProduct_SpecialOffer_SpecialOfferID,false");
            _tableMigrator.Fks.Add("Sales.SalesPerson.BusinessEntityID.Sales.Store.SalesPersonID", "FK_Store_SalesPerson_SalesPersonID,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.BillOfMaterials.ComponentID", "FK_BillOfMaterials_Product_ComponentID,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.BillOfMaterials.ProductAssemblyID", "FK_BillOfMaterials_Product_ProductAssemblyID,false");
            _tableMigrator.Fks.Add("Production.UnitMeasure.UnitMeasureCode.Production.BillOfMaterials.UnitMeasureCode", "FK_BillOfMaterials_UnitMeasure_UnitMeasureCode,false");
            _tableMigrator.Fks.Add("Production.ProductModel.ProductModelID.Production.Product.ProductModelID", "FK_Product_ProductModel_ProductModelID,false");
            _tableMigrator.Fks.Add("Production.ProductSubcategory.ProductSubcategoryID.Production.Product.ProductSubcategoryID", "FK_Product_ProductSubcategory_ProductSubcategoryID,false");
            _tableMigrator.Fks.Add("Production.UnitMeasure.UnitMeasureCode.Production.Product.SizeUnitMeasureCode", "FK_Product_UnitMeasure_SizeUnitMeasureCode,false");
            _tableMigrator.Fks.Add("Production.UnitMeasure.UnitMeasureCode.Production.Product.WeightUnitMeasureCode", "FK_Product_UnitMeasure_WeightUnitMeasureCode,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.ProductCostHistory.ProductID", "FK_ProductCostHistory_Product_ProductID,false");
            _tableMigrator.Fks.Add("Production.Document.DocumentNode.Production.ProductDocument.DocumentNode", "FK_ProductDocument_Document_DocumentNode,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.ProductDocument.ProductID", "FK_ProductDocument_Product_ProductID,false");
            _tableMigrator.Fks.Add("Production.Location.LocationID.Production.ProductInventory.LocationID", "FK_ProductInventory_Location_LocationID,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.ProductInventory.ProductID", "FK_ProductInventory_Product_ProductID,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.ProductListPriceHistory.ProductID", "FK_ProductListPriceHistory_Product_ProductID,false");
            _tableMigrator.Fks.Add("Production.Illustration.IllustrationID.Production.ProductModelIllustration.IllustrationID", "FK_ProductModelIllustration_Illustration_IllustrationID,false");
            _tableMigrator.Fks.Add("Production.ProductModel.ProductModelID.Production.ProductModelIllustration.ProductModelID", "FK_ProductModelIllustration_ProductModel_ProductModelID,false");
            _tableMigrator.Fks.Add("Production.Culture.CultureID.Production.ProductModelProductDescriptionCulture.CultureID", "FK_ProductModelProductDescriptionCulture_Culture_CultureID,false");
            _tableMigrator.Fks.Add("Production.ProductDescription.ProductDescriptionID.Production.ProductModelProductDescriptionCulture.ProductDescriptionID", "FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID,false");
            _tableMigrator.Fks.Add("Production.ProductModel.ProductModelID.Production.ProductModelProductDescriptionCulture.ProductModelID", "FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.ProductProductPhoto.ProductID", "FK_ProductProductPhoto_Product_ProductID,false");
            _tableMigrator.Fks.Add("Production.ProductPhoto.ProductPhotoID.Production.ProductProductPhoto.ProductPhotoID", "FK_ProductProductPhoto_ProductPhoto_ProductPhotoID,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.ProductReview.ProductID", "FK_ProductReview_Product_ProductID,false");
            _tableMigrator.Fks.Add("Production.ProductCategory.ProductCategoryID.Production.ProductSubcategory.ProductCategoryID", "FK_ProductSubcategory_ProductCategory_ProductCategoryID,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.TransactionHistory.ProductID", "FK_TransactionHistory_Product_ProductID,false");
            _tableMigrator.Fks.Add("Production.Product.ProductID.Production.WorkOrder.ProductID", "FK_WorkOrder_Product_ProductID,false");
            _tableMigrator.Fks.Add("Production.ScrapReason.ScrapReasonID.Production.WorkOrder.ScrapReasonID", "FK_WorkOrder_ScrapReason_ScrapReasonID,false");
            _tableMigrator.Fks.Add("Production.Location.LocationID.Production.WorkOrderRouting.LocationID", "FK_WorkOrderRouting_Location_LocationID,false");
            _tableMigrator.Fks.Add("Production.WorkOrder.WorkOrderID.Production.WorkOrderRouting.WorkOrderID", "FK_WorkOrderRouting_WorkOrder_WorkOrderID,false");
            _tableMigrator.Fks.Add("Person.StateProvince.StateProvinceID.Person.Address.StateProvinceID", "FK_Address_StateProvince_StateProvinceID,false");
            _tableMigrator.Fks.Add("Person.Address.AddressID.Person.BusinessEntityAddress.AddressID", "FK_BusinessEntityAddress_Address_AddressID,false");
            _tableMigrator.Fks.Add("Person.AddressType.AddressTypeID.Person.BusinessEntityAddress.AddressTypeID", "FK_BusinessEntityAddress_AddressType_AddressTypeID,false");
            _tableMigrator.Fks.Add("Person.BusinessEntity.BusinessEntityID.Person.BusinessEntityAddress.BusinessEntityID", "FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Person.BusinessEntity.BusinessEntityID.Person.BusinessEntityContact.BusinessEntityID", "FK_BusinessEntityContact_BusinessEntity_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Person.ContactType.ContactTypeID.Person.BusinessEntityContact.ContactTypeID", "FK_BusinessEntityContact_ContactType_ContactTypeID,false");
            _tableMigrator.Fks.Add("Person.Person.BusinessEntityID.Person.BusinessEntityContact.PersonID", "FK_BusinessEntityContact_Person_PersonID,false");
            _tableMigrator.Fks.Add("Person.Person.BusinessEntityID.Person.EmailAddress.BusinessEntityID", "FK_EmailAddress_Person_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Person.Person.BusinessEntityID.Person.Password.BusinessEntityID", "FK_Password_Person_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Person.BusinessEntity.BusinessEntityID.Person.Person.BusinessEntityID", "FK_Person_BusinessEntity_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Person.Person.BusinessEntityID.Person.PersonPhone.BusinessEntityID", "FK_PersonPhone_Person_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Person.PhoneNumberType.PhoneNumberTypeID.Person.PersonPhone.PhoneNumberTypeID", "FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID,false");
            _tableMigrator.Fks.Add("Person.CountryRegion.CountryRegionCode.Person.StateProvince.CountryRegionCode", "FK_StateProvince_CountryRegion_CountryRegionCode,false");
            _tableMigrator.Fks.Add("Purchasing.Vendor.BusinessEntityID.Purchasing.ProductVendor.BusinessEntityID", "FK_ProductVendor_Vendor_BusinessEntityID,false");
            _tableMigrator.Fks.Add("Purchasing.PurchaseOrderHeader.PurchaseOrderID.Purchasing.PurchaseOrderDetail.PurchaseOrderID", "FK_PurchaseOrderDetail_PurchaseOrderHeader_PurchaseOrderID,false");
            _tableMigrator.Fks.Add("Purchasing.ShipMethod.ShipMethodID.Purchasing.PurchaseOrderHeader.ShipMethodID", "FK_PurchaseOrderHeader_ShipMethod_ShipMethodID,false");
            _tableMigrator.Fks.Add("Purchasing.Vendor.BusinessEntityID.Purchasing.PurchaseOrderHeader.VendorID", "FK_PurchaseOrderHeader_Vendor_VendorID,false");

        }
    
            
		protected override void Generate(CreateIndexOperation createIndexOperation)
        {
            using (var writer = Writer())
            {
                writer.Write("CREATE ");

                if (createIndexOperation.IsUnique)
                {
                    writer.Write("UNIQUE ");
                }

                object isClustered;
                createIndexOperation.AnonymousArguments.TryGetValue("IsClustered", out isClustered);

                if (isClustered is bool && (bool)isClustered)
                {
                    writer.Write("CLUSTERED ");
                }
                else
                    writer.Write("NONCLUSTERED ");

                writer.Write("INDEX ");
                writer.Write(Quote(createIndexOperation.Name));
                writer.Write(" ON ");
                writer.Write(Name(createIndexOperation.Table));
                writer.Write("(");
                
                writer.Write(string.Join(", ", createIndexOperation.Columns.Select(c => (c.ToUpper().Right(5) == " DESC" ? Quote(c.Left(c.Length - 5)) + " DESC" : Quote(c)))));

                writer.Write(")");
                Statement(writer);
            }
        }

        public override IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken)
        {
            var operations = new List<MigrationOperation>();

			//Copy all operations           
            foreach (var db in migrationOperations)
            {
                if (db is UpdateDatabaseOperation)
                {
                    foreach (var mg in ((UpdateDatabaseOperation)db).Migrations)
                    {                        
                        operations.AddRange(mg.Operations);
                    }
                }
            }



			//Adjusting elements
            foreach (var op in operations.ToArray())
            {
                if (op is AddForeignKeyOperation)
                {
                    AdjustForeignKey(((AddForeignKeyOperation)op));                    
                }                
                if (op is CreateTableOperation)
                {
						AdjustTable(((CreateTableOperation)op), operations);                    
                }
            }

            var statements = base.Generate(operations, providerManifestToken).ToList();


            if (statements.Count > 0)
            {
				string headerInfo = "-- Script SQLServer was generated by Linx Systems\r\n" +
									"-- Company home page: http://www.linx.com.br\r\n" +
									String.Format("-- Script date {0:d/M/yyyy HH:mm:ss}", DateTime.Now) + "\r\n";
				var first = statements.First();
				first.Sql = headerInfo + first.Sql;
                var migHistory = statements.FirstOrDefault(e => e.Sql.Contains("__MigrationHistory"));
                if (migHistory != null)
                    statements.Remove(migHistory);
                
				//Adjust NONCLUSTERED PrimaryKeys
                foreach (var sql in statements.Where(e => e.Sql.Contains("__NC__] PRIMARY KEY ")))
                {
                    sql.Sql = sql.Sql.Replace("__NC__] PRIMARY KEY ", "] PRIMARY KEY NONCLUSTERED ");
                }
            }
           
            return statements;
        }

        private void AdjustTable(CreateTableOperation createTableOperation, List<MigrationOperation> migrationOperations)
        {
            if (_tableMigrator.Views.Contains(createTableOperation.Name))
            {
                migrationOperations.Remove(createTableOperation);
                migrationOperations.Add(new SqlOperation("/*SQL View " + createTableOperation.Name + " was ignored.*/"));
            }
            else
            {
                if (createTableOperation.Name != "dbo.__MigrationHistory")
                {
					//Adjust Primary Key	
					if (_tableMigrator.PrimaryKeys.ContainsKey(createTableOperation.Name))				
						createTableOperation.PrimaryKey.Name = _tableMigrator.PrimaryKeys[createTableOperation.Name];

                    //Defaults
                    foreach (var column in createTableOperation.Columns)
                    {
                        if (_tableMigrator.Defauls.ContainsKey(createTableOperation.Name + "." + column.Name))
                            column.DefaultValueSql = _tableMigrator.Defauls[createTableOperation.Name + "." + column.Name];

						if (column.IsNullable != null && !column.IsNullable.Value && _tableMigrator.Nullables.Contains(createTableOperation.Name + "." + column.Name))
                            column.IsNullable = true;
                    }

                    //Add Indexes
                    foreach (var index in _tableMigrator.Indexes.Where(e => e.Table == createTableOperation.Name))
                    {
                        migrationOperations.Add(index);
                    }
                }
                else
                {
                    migrationOperations.Remove(createTableOperation);
                }
            }
        }
               

        private void AdjustForeignKey(AddForeignKeyOperation addForeignKeyOperation)
        {
            _tableMigrator.AdjustFK(addForeignKeyOperation);
        }
        
    }

}
