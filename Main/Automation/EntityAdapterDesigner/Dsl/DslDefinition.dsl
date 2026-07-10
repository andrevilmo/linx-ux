<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="4e2ca77b-ca98-4ef3-b979-bfcf1c75e023" Description="Designer for creating business views." Name="EntityAdapterDesigner" DisplayName="Business View Designer" Namespace="Linx.EntityAdapterDesigner" ProductName="EntityAdapterDesigner" CompanyName="Linx" PackageGuid="8763d601-a024-4873-9ef7-98724e3ce8cb" PackageNamespace="Linx.EntityAdapterDesigner" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Notes>Overload name for the operation.</Notes>
  <Classes>
    <DomainClass Id="8c4918d4-0ea3-4b47-acb9-a428ef250bd9" Description="The root in which all other elements are embedded. Appears as a diagram." Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="87b0dde2-3396-48ed-8af3-a796eacc1bac" Description="The target namespace for the solution." Name="TargetNamespace" DisplayName="Target Namespace" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9570c797-97b9-4299-9342-fdcf80f08a9e" Description="Description for Entity Adapter Model" Name="Title" DisplayName="Title">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="efc8c74c-7b2a-410d-bc31-143b1a6359cc" Description="Diagram Document Name" Name="DocumentName" DisplayName="Document Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ceb0c733-d175-4963-9c0a-93a6c84ffb12" Description="" Name="EnableAutomaticAuthorization" DisplayName="Enable Automatic Authorization" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="45ad2b37-76b0-4ba0-aac9-5b6a5c08604c" Description="Enable generating documentation." Name="EnableDocumentation" DisplayName="Enable Documentation" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="50a83913-f81c-4963-8e13-8206faa08e27" Description="Diagram Document Path" Name="DocumentPath" DisplayName="Document Path" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a2c73e50-f39c-4537-adca-4d464129c058" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRoot.Version" Name="Version" DisplayName="Version" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6e8e6f6d-32f5-407f-b040-036211a77986" Description="Save all representations before normal entities." Name="FirstSaveRepresentations" DisplayName="First Save Representations">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f4fa3448-8267-4418-ae9d-cc6d29890021" Description="" Name="RefreshIdentityKeysAfterSave" DisplayName="Refresh Identity Keys After Save" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7004307e-e21e-4103-9df6-3c8e24d4d533" Description="Generate Asp.Net Core Projects" Name="IsAspNetCore" DisplayName="Is Asp Net Core" DefaultValue="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityDataModel" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasEntityDataModels.EntityDataModels</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapter" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasEntityAdapters.EntityAdapters</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Comment" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasComments.Comments</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="DomainServiceExtension" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasDomainServiceExtensions.DomainServiceExtensions</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="LookUpAdapter" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasLookUpAdapters.LookUpAdapters</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterUserInterface" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasEntityAdapterUserInterfaces.EntityAdapterUserInterfaces</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="DomainView" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasDomainViews.DomainViews</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Subscription" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasSubscriptions.Subscriptions</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="KeyPerformanceIndicator" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasKeyPerformanceIndicators.KeyPerformanceIndicators</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Workflow" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasWorkflows.Workflows</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterRepresentation" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasEntityAdapterRepresentations.EntityAdapterRepresentations</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="WebApiController" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasWebApiControllers.WebApiControllers</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="RepositoryInterface" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasRepositoryInterfaces.RepositoryInterfaces</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="RepositoryImplementation" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasRepositoryImplementations.RepositoryImplementations</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="StoreScript" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasStoreScripts.StoreScripts</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="OlapCatalog" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasOlapCatalogs.OlapCatalogs</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ClientLocalService" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterDesignerRootHasClientLocalServices.ClientLocalServices</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="2e451398-c267-48ec-be99-b9c89b005088" Description="Entity Adapter Business View." Name="EntityAdapter" DisplayName="Entity Adapter" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="6d72cfd2-0ea6-4b96-8aa1-c259bdbb6120" Description="Entity Adapter Name." Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c38c3c0e-9e9f-4a5d-a547-5cb5a15653f2" Description="Other updatable Entities for the Edm." Name="SecondaryEntities" DisplayName="Secondary Entities" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d9a1f22d-4668-4118-a0f3-2be85b567ee9" Description="Entity Description." Name="Description" DisplayName="Description" DefaultValue=" ">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7feaded2-e665-4b62-b4df-5fa13c8a01b0" Description="All entities related with this entity." Name="EntityRelations" DisplayName="Entity Relations" DefaultValue=" " IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4dd627eb-4129-4bc1-8adc-907947ddcc4d" Description="All details related with this entity." Name="DetailRelations" DisplayName="Detail Relations" DefaultValue=" " IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="25c7945f-dc86-4113-9b83-919c05e8003f" Description="Entity is Read Only." Name="IsReadOnly" DisplayName="Is Read Only">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d5507186-707c-4018-bf70-bd183b4c5646" Description="All references related with this entity." Name="ReferenceRelations" DisplayName="Reference Relations" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4376f468-1798-43d9-853f-82e5d990cb3b" Description="The display name information." Name="DisplayName" DisplayName="Display Name">
          <Notes>Obs: in exportation to the Excel, will truncate in 32 char if has than 32 chars</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9a89a9db-f8bc-40af-a5d6-557ad2abfdf6" Description="The top entity in the DataContext." Name="PrimaryEntity" DisplayName="Primary Entity">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4903ce99-15cb-45c4-bd71-d46bae04601f" Description="Entity Sets for the Meta Data Composition." Name="EntitySets" DisplayName="Entity Sets">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9e6c0cdb-d96c-4863-b59a-e3b994a78d8c" Description="All attribute definitions should be separated per #." Name="CustomAttributes" DisplayName="Custom Attributes">
          <Notes>All attribute definitions should be separated per #.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c42ed253-63e7-4f80-a7ba-bf95a64cd1a2" Description="The method name for a custom validation. After save this information, double click to open it." Name="CustomValidationMethod" DisplayName="Custom Validation Method">
          <Notes>This method will be created as a shared resource.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0bd7e5c3-7775-4860-9379-8fa1c3990ddc" Description="This view is an aggregation view." Name="IsAggregationView" DisplayName="Is Aggregation View">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="378a3709-e70f-4293-8ec5-60b6852c8dea" Description="Configure like this: O#Total Field of O, E#Total Field Of E, ... (Where O = O1 -&gt; O48 and E = E1 -&gt; E48)" Name="SizeGridConfigurations" DisplayName="Product Size Grid">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="fc99dacf-fdf2-489f-a545-6e9182722312" Description="Remove the parent association property." Name="EnableDetailsSerialization" DisplayName="Enable Details Serialization">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cdd9ec82-86d2-431d-877a-234fd8403729" Description="This is the return type collection for this entity." Name="QueryReturnType" DisplayName="Query Return Type" DefaultValue="IQueryable">
          <Type>
            <DomainEnumerationMoniker Name="EntityQueryReturnType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1a4bcb33-4de2-43d2-abe8-674f9dffffde" Description="Public this element." Name="EnableForPublication" DisplayName="Enable For Publication" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="bb734056-abec-4b9b-8e36-f2a315974fed" Description="Copy Configuration From Default User Interface." Name="CopyConfigurationFromDefaultUI" DisplayName="Copy Configuration From Default UI" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7228ee77-30b9-4f92-ae79-e664be5232c7" Description="Combine all properties of this view with all parent properties." Name="ParentCompositionEnabled" DisplayName="Enable Parent Composition" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d0620ed0-1480-4a67-ad9f-ddc105446282" Description="Attribute Order For Properties Compartiment." Name="PropertyOrder" DisplayName="Property Order" DefaultValue="Name" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <DomainEnumerationMoniker Name="AttributeOrder" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="013e71d1-3a45-4f9b-8517-d68b5a5d952f" Description="Execute a new query for each detail after save operation." Name="RequeryDetailsAfterSave" DisplayName="Requery Details After Save">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="99fbc43a-0b8c-40f7-a0d1-85c1cc5a11a1" Description="Create all methods for Create/Read/Update/Delete." Name="CreateCRUD" DisplayName="Create CRUD" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7944dc0a-9a1e-4af2-abdf-76e8b364c440" Description="" Name="CreateDynamicPrimaryKey" DisplayName="Create Dynamic Primary Key">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8423d2ec-4c92-496b-8315-152cff9821a8" Description="Generate Look Ups Automatically." Name="EnableAutomaticLookUps" DisplayName="Enable Automatic Look Ups" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e66ef7ae-9e91-491e-aedd-1ba55237f283" Description="" Name="EntityClassInfo" DisplayName="Entity Class Info" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="78bd81c9-1be9-4091-b9bd-75b293d4edff" Description="Reverse insert order when the view has more then one entity form maintenance." Name="ReverseInsertOrder" DisplayName="Reverse Insert Order">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="26aa4d05-62f6-4af6-9ceb-34cb1a24db68" Description="Maximum level for DataContext tree entities" Name="EdmTreeMaximumLevel" DisplayName="DataContext Tree Maximum Level" DefaultValue="3">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="13c8e749-cf91-499e-a2a3-62ae5cb38eb9" Description="Property Name For Surrogate Control" Name="SurrogateProperty" DisplayName="Surrogate Property">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1f08edfa-27ad-48d8-a372-e4600b6c0af3" Description="Show &quot;Load Process&quot; while loading the data of details." Name="ShowDetailsLoadProcess" DisplayName="Show Details Load Process" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4c24f097-2e07-4660-b66c-b64a2bdf5c2e" Description="If aggregation view, remove all measures if not used." Name="RemoveMeasureIfNotUsed" DisplayName="Remove Measure If Not Used" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="97c8b2cb-2d7b-4613-ab0c-338b9613c0ca" Description="Enable the filter selection of properties for metadata." Name="EnableMetaDataFilter" DisplayName="Enable Meta Data Filter">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="69360ea2-437d-4d00-9973-3ee0f9ab0d7f" Description="Data key that controls the publication of this entity." Name="ReplicationKey" DisplayName="Replication Key">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="18304e53-55aa-49bd-b337-bdc36b37221f" Description="" Name="IsUpdatableWhenPublished" DisplayName="Is Updatable When Published">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8b88bacb-2a66-4ac2-8c8f-5b047f2a1d53" Description="Check the inserted EDM entity. If exists, execute an update command." Name="CheckExistenceOnInserting" DisplayName="Check Existence On Inserting">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6cbe9d42-50d4-4524-9075-a42ad78a34d3" Description="" Name="DetailsCollectionType" DisplayName="Details Collection Type" DefaultValue="IEnumerable&lt;T&gt;">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="81f6fc29-1eaa-45fe-8cf8-e28d7aa1fbf4" Description="" Name="DataContractName" DisplayName="Data Contract Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="500355d5-3838-41dc-b2b9-60f0320759cf" Description="Indicates that this class will be a Plain Old CLR Object." Name="IsPOCO" DisplayName="Is POCO">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="904696d7-e30f-4b90-92ee-451863bcbf9c" Description="" Name="POCOInfo" DisplayName="POCOInfo" DefaultValue="POCO" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="fc1c360b-c695-47ef-bf61-48682e4b455f" Description="" Name="CustomBaseType" DisplayName="Custom Base Type">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e96899f3-1dd1-4955-90a1-6889aa4115e6" Description="" Name="IsCollectionDataContract" DisplayName="Is Collection Data Contract">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0c93a1e5-9491-4e3e-8990-0fe19c916f95" Description="If value is '.', the Namespace will not be explicit." Name="DataContractNamespace" DisplayName="Data Contract Namespace" DefaultValue=".">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ca8ae525-45c4-4483-a4f8-7c1c88304540" Description="Generate Data Member Order For All Properties." Name="GenerateDataMemberOrder" DisplayName="Generate Data Member Order" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="92a4de55-1243-4521-85d3-8ed462cba0e0" Description="Generate update only for changed properties in buffer." Name="IsSingleBufferUpdate" DisplayName="Is Single Buffer Update">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3760cde0-5965-465c-ac05-4a525bf5424d" Description="Emit default value for all datamembers of this entity." Name="DataMemberEmitDefaultValue" DisplayName="Data Member Emit Default Value" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c5a16ee4-c417-4831-bfa4-0523a9235779" Description="Enable Query By Example Searching." Name="EnableQBE" DisplayName="Enable QBE" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a22e5d37-1646-47d8-8cb9-46954f8bda0b" Description="The base type of PrimaryEntity." Name="PrimaryEntityBase" DisplayName="Primary Entity Base">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cc63aea1-7c33-4f96-ae7f-a07a82921fcf" Description="Derived " Name="SourceDerivedClasses" DisplayName="Source Derived Classes" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0d6ee1fd-84ca-49fc-8341-c2d610c56e95" Description="Implements a well known business." Name="BusinessExtension" DisplayName="Business" DefaultValue="None">
          <Type>
            <DomainEnumerationMoniker Name="BusinessExtensions" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f29628be-4ec4-4656-aaa6-8484b9a1ac3b" Description="Execute the save command immediately after buffer adding." Name="NoBufferChanges" DisplayName="No Buffer Changes">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9e6ebd0f-b538-4ef3-a236-a7df76179b53" Description="Force data paging when this view is aggregated." Name="ForceAggregationPaging" DisplayName="Force Aggregation Paging">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4b02f4a1-2d7a-4d73-994b-e304f59eae9b" Description="Name of OLAP Cube that is the datasource of this business view." Name="CubeName" DisplayName="Cube Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="41c05646-29b7-43e7-8bf1-f77dd640e947" Description="EF: (SALE_ITEM.PRODUCT.ID_PRODUCT == 10 || SALE_ITEM.SALE.CUSTOMER.ID_CUSTOMER == 1234), OLAP: select { [DimProduct].[color].[color].&amp;[Black] } on columns from [Model]" Name="Filter" DisplayName="Filter">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="793ec8a8-7f76-43bc-9143-f1b2396e07bc" Description="" Name="ForceBrandFilter" DisplayName="Force Brand Filter">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c150ab35-f23c-408e-be0e-3f4ea295cdd9" Description="" Name="SendAllRowsOnSubmitting" DisplayName="Send All Rows On Submitting">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="73259793-5d45-44aa-88f9-f132d7956b6f" Description="Execute distinct command over the select." Name="Distinct" DisplayName="Distinct">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9e452892-d6c2-4e88-b17f-2152fa73fd3a" Description="Expose as a service." Name="ExposeAsService" DisplayName="Expose As Service" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="19f4767b-0745-4c55-9395-81069c2e45eb" Description="" Name="LoadDataOnlyIfVisible" DisplayName="Load Data Only If Visible" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0f99faeb-a331-42eb-b644-62fedb34f7f2" Description="This entity will be a dashboard filter definition, with all details working as master entities." Name="IsDashboardFilter" DisplayName="Is Dashboard Filter">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c44efe36-a645-41ee-a36a-65e55e38f4a8" Description="Execute a new query after save operation." Name="RequeryAfterSave" DisplayName="Requery After Save">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9c7e7655-fce3-4f28-a819-dfc172f1fc41" Description="Execute the parent query and returns the result from this collection in the parent instance." Name="EnableQueryByParent" DisplayName="Enable Query By Parent">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a90999b7-5f54-4a7a-be8c-f6f1c21e05bc" Description="" Name="EnableClientLookupOnQueryMode" DisplayName="Enable Client Lookup On Query Mode">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="142e79ef-ee8e-4b7f-85b6-85a216d3e567" Description="Enable QBE optimization by lookup primary keys when these keys have values." Name="EnableLookupOptimizationForQBE" DisplayName="Enable Lookup Optimization For QBE" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a6f816b6-65b5-4e64-98d4-73a335edf11c" Description="Serialized Business View" Name="ModelViewDefinition" DisplayName="Model View Definition" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="25628483-4718-428a-be2e-dab53bdd4323" Description="Enable flexible model view designer." Name="IsModelView" DisplayName="Is Query View">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="931a396c-3596-4863-9bf2-7349bc67f10c" Description="All inner entities for CRUD." Name="ModelViewDbSets" DisplayName="Query View DbSets" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="65154b10-d10a-42fe-acf0-aca386ce2907" Description="Details navigation suggestion. Ex: DetailName1(Navigation1)#DetailName2(Navigation2)" Name="DetailRelationsSuggestion" DisplayName="Detail Relations Suggestion" DefaultValue=" ">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0e52b441-c5c3-440e-b277-073586a0ab5a" Description="Mechanism for working with a large data range on client side." Name="IsLargeDataMode" DisplayName="Is Large Data Mode">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterHasEntityAdapterProperties.EntityAdapterProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterOperation" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterHasEntityAdapterOperations.EntityAdapterOperations</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterFormula" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterHasEntityAdapterFormulas.EntityAdapterFormulas</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterEvent" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterHasEntityAdapterEvents.EntityAdapterEvents</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterPublicationProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterHasEntityAdapterPublicationProperties.EntityAdapterPublicationProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterExtendedFilter" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterHasEntityAdapterExtendedFilters.EntityAdapterExtendedFilters</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterClientEvent" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterHasEntityAdapterClientEvented.EntityAdapterClientEvented</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="6b106bfe-f980-4c4d-9d64-6237a2f9045f" Description="Description for Linx.EntityAdapterDesigner.EntityDataModel" Name="EntityDataModel" DisplayName="Entity Data Model" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="7ac238ac-3008-4d48-8d0c-e88c1f663e70" Description="DataContext Name." Name="Name" DisplayName="Name" IsElementName="true" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="92d4bcc5-febf-4840-83cf-a5dfd9c46a30" Description="DataContext assembly path." Name="Path" DisplayName="DataContext Assembly Path" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9dd9d405-939d-4681-83f7-986d43e44946" Description="DataContext namespace." Name="TargetNamespace" DisplayName="Target Namespace" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="df645487-0feb-4447-96bf-7577a12b35a1" Description="DataContext description." Name="Description" DisplayName="Description">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c2ca038a-f6b1-45bf-8f88-8732724606a7" Description="Connection name for DataContext access." Name="ConnectionName" DisplayName="Connection Name" DefaultValue=" ">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1a06b55a-f5d0-4d51-b4aa-8a2f3bd544c8" Description="" Name="ContextType" DisplayName="Context Type" Kind="Calculated" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="47e69aab-ac52-4df0-8ac7-92f05be0342e" Description="" Name="HasError" DisplayName="Has Error" Kind="Calculated">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="872a4bb9-c7f9-4de3-b17a-b8f184274239" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterProperty" Name="EntityAdapterProperty" DisplayName="Entity Adapter Property" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="EntityAdapterAttribute" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="77faea6d-d641-4d81-8e12-7895528608bf" Description="Full field path on DataContext or use the following macros:.@Iif[Contition|TrueResult|FalseResult], @Divide[Dividend|Divisor], @Year[exp], @Month[exp], @Day[exp], @DayOfYear[exp], @Hour[exp], @Minute[exp], @Second[exp]." Name="EdmKey" DisplayName="DataContext Related Property">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.EntityAdapterDesigner.CustomizedCode.Forms.Editors.MacroEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="293a05b8-9c7f-4b67-aad9-c26f6a2fc035" Description="Constant or Parameter. Parameter example: [PARAMETER NAME]" Name="DefaultValue" DisplayName="Default Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e036d3c0-7ef2-4ec1-a66e-bd53454ae9d6" Description="Filter by this property (Ex1: [Value] == 10 || [Value] == [ThisRef].FieldValue, Ex2: [Value] &gt;= 20 &amp;&amp; [Value] &lt;= 50)." Name="Filter" DisplayName="Filter">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3cc97a39-58bf-4bfe-b268-c3ee968d6ff7" Description="Edm key name for this property." Name="TargetKeyName" DisplayName="Target Key Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="97684220-584c-4b05-aac0-416a9659e423" Description="Index for orderby sequence." Name="OrderBySequence" DisplayName="Order By Sequence" DefaultValue="-1">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4c77ec5c-11a2-48d1-a427-01c732bd517a" Description="" Name="OrderByOrientation" DisplayName="Order By Orientation" DefaultValue="Ascending">
          <Type>
            <DomainEnumerationMoniker Name="OrderByOrientationType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cafd0ac9-a6db-4aee-aaf6-83fc8358bac3" Description="" Name="DisplayValue" DisplayName="Display Value" Kind="Calculated" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3b84af11-dd5a-4490-b622-496c12841d41" Description="This property will be used for comparing with suggestion of publication properties." Name="PublicationRelatedKey" DisplayName="Publication Related Key">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2906f2e1-e176-4cdb-8360-d962fbeb9748" Description="All the properties of the related publication will be fixed this value at the end." Name="PublicationSuffix" DisplayName="Publication Suffix">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b49794f2-8d90-4132-bd8f-2f765391ae0f" Description="Generate a new sequence on add action.." Name="IsAutomaticSequency" DisplayName="Is Automatic Sequency">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="60289667-39b1-4153-9c33-c5062c338eb6" Description="Indicates denormalized data for composing this property (e.g: PropName[1-16]  or PropNameA,PropNameB,PropNameC)." Name="DenormalizedDataInfo" DisplayName="Denormalized Data Info">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7a01aae0-26a2-4f0c-8c2e-f9bf2b17444d" Description="Indicates if this number is generated automatically." Name="IsIdentity" DisplayName="Is Identity">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="46cd3448-edd1-4dd3-b8b3-08cb25fa11d6" Description="Call methods of this property for LINQ." Name="LinqMethod" DisplayName="Linq Method">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="144895d2-bc12-4c54-a761-1efde6dbc17f" Description="This property is required before searching." Name="IsRequiredBeforeSearching" DisplayName="Is Required Before Searching">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="20941dff-b316-4f40-9f27-678557260eab" Description="Expression for Model View Linq." Name="ModelViewFormula" DisplayName="Query View Formula">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a5bfa91f-785f-4f68-b3ac-44e20a7178aa" Description="Source data property from business query." Name="ModelViewSource" DisplayName="Query View Source" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="24e8472d-f949-4bff-884a-13e6d8bcebd8" Description="This field will be used as a quick search." Name="QuickSearchIndex" DisplayName="Quick Search Index" DefaultValue="-1">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="6b52038d-d3f8-4c53-bd68-5a66a870b333" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterOperation" Name="EntityAdapterOperation" DisplayName="Entity Adapter Operation" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="GenericOperation" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="c92eafee-ce36-4619-8216-9a7e14ea6f48" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterAttribute" Name="EntityAdapterAttribute" DisplayName="Entity Adapter Attribute" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="d0946b66-240f-455d-bc73-b344e6ab4bbe" Description="Attribute name." Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7eb81960-0258-4e86-ba65-d1228a9aef94" Description="Order to display the attribute on UI." Name="DisplayOrder" DisplayName="Display Order" DefaultValue="" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="125eb6f1-5051-4ce3-a13b-c945f85b996d" Description="The attribute is visible on UI." Name="IsBrowsable" DisplayName="Is Browsable" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="979a6a24-a1af-4e62-bab2-2d03daa0c0b9" Description="Connected attribute. The current attribute will be connected to this attribute." Name="ConnectedAttribute" DisplayName="Connected Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4c42dbe0-aa79-4b6a-8a57-eb350f36ea50" Description="Data type." Name="Datatype" DisplayName="Datatype" DefaultValue="int">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dd736181-a80d-41b3-9150-dd9ba2bcae63" Description="Attribute precision." Name="Precision" DisplayName="Precision" DefaultValue="0">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ffa5b772-f02b-4975-8a3c-8385d1df2a2e" Description="The attribute is primary key on data source." Name="IsPK" DisplayName="Is Primary Key (Source)">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="313cca40-2a40-4b2d-a295-584393a45c16" Description="The attribute is foreign key." Name="IsFK" DisplayName="Is Foreign Key" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d0a61e79-8156-4856-96b9-751abbf976c9" Description="The attribute allow null value." Name="IsNull" DisplayName="Is Null">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3861906d-5e83-4bfd-ae02-fa0735e3a57b" Description="The attribute is editable." Name="IsEditable" DisplayName="Is Editable">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4bfbf7a5-2d1f-45b6-bce0-773fba2792ae" Description="The display name information." Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="52b999d5-d75c-43ac-9a70-7b09e7b01c98" Description="The control class to display this attribute on UI." Name="DisplayControl" DisplayName="Display Control" DefaultValue="TextBox">
          <Type>
            <DomainEnumerationMoniker Name="DisplayControlType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="84750980-d449-46ad-b172-d2bf10453b2e" Description="UI Group. " Name="GroupName" DisplayName="Group Name" DefaultValue="0001::||False" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c57a4f07-ed02-47ba-9c60-f7e71284fb71" Description="Attribute description." Name="Description" DisplayName="Description">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9104dd12-5d13-40f2-8cc3-00c86c7e1eb3" Description="Custom attribute definitions. All attribute definitions should be separated per #." Name="CustomAttributes" DisplayName="Custom Attributes">
          <Notes>All attribute definitions should be separated per #.</Notes>
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7ba8e813-a6ce-4fbd-a477-b19a2578d8e2" Description="This element was customized." Name="IsCustomized" DisplayName="Is Customized" DefaultValue="true">
          <Notes>Customized field. There is no automatic generation for this attribute.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="44ccbcaa-7935-4ba3-b4d9-186c4fc3f12e" Description="The values range for validation (Ex: 1, 7)." Name="Range" DisplayName="Range">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1d5069a5-ed07-4550-b738-0b49695924ab" Description="Data Format (Ex: C02, N02, d or none) * none is used to remove the thousand separator." Name="DataFormatString" DisplayName="Data Format String">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ed9d2128-0f47-4c83-9960-612be671bcd5" Description="The method name for a custom validation. After save this information, double click to open it." Name="CustomValidationMethod" DisplayName="Custom Validation Method">
          <Notes>This method will be created as a shared resource.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4419672f-a936-4301-b0ea-525339379f77" Description="Aggregation Function" Name="AggregationFunction" DisplayName="Aggregation Function" DefaultValue="None">
          <Type>
            <DomainEnumerationMoniker Name="UIAggregationFunctions" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4bf71148-6e95-4c29-a459-8e45ade157e2" Description="Domain view name." Name="DomainName" DisplayName="Domain Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4e3560b6-4cd7-448c-9026-0f983b31052a" Description="The attribute is Compulsory." Name="IsCompulsory" DisplayName="Is Compulsory">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7a4d37c8-b2eb-45a2-a15b-00aa946cd74d" Description="Publication Suggestion." Name="IsPublicationSuggestion" DisplayName="Is Publication Suggestion">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="50d9759f-c21f-4c3f-9271-9149edfadbab" Description="Remove all validation attributes." Name="RemoveValidations" DisplayName="Remove Validations">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2573a120-7420-49ec-970d-94e036754739" Description="Key Performance Indicator Reference." Name="KpiName" DisplayName="Kpi Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4ca5d728-848e-45bd-95ad-a28910cfc7c9" Description="" Name="KpiRelatedAttribute" DisplayName="Kpi Related Attribute" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="473c1ac0-1998-447c-a325-56b2f4b2d25a" Description="Force this attribute as an automatic filter." Name="ForceAsFilter" DisplayName="Force As Filter">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a11aa8c2-48c0-485d-97e1-1126978c8c75" Description="This is the data key related with this element." Name="DataRelationKey" DisplayName="Business Related Property">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1bf1b8d2-957a-4af4-bf97-854c3b8a42a2" Description="This attribute is a Flat Pivot Measure." Name="IsMeasure" DisplayName="Is Measure">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5b6090a7-0d66-4bee-bad6-a234f4ee8168" Description="Set this attribute as a calculated measure. Example: {0} / {1}, SalesValue, SalesQuantity. You can use the following macros:.@Iif[Contition|TrueResult|FalseResult], @Divide[Dividend|Divisor], @Year[exp], @Month[exp], @Day[exp], @DayOfYear[exp], @Hour[exp], @Minute[exp], @Second[exp]." Name="MeasureFormula" DisplayName="Measure Formula">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ca4305a5-1058-4a90-bb28-7401177e21fe" Description="If true this property never will be used in server queries." Name="IgnoreForQuery" DisplayName="Is Not Used As Filter">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="183d777c-de7d-4924-8d8c-2df5bca8c5c5" Description="Information about the LookUp available from publisher." Name="LookUpSubscription" DisplayName="Look Up Subscription" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="25db29bb-dbd1-4736-a855-970adfa14879" Description="" Name="MaskType" DisplayName="Mask Type">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="50636bc1-59a7-4b2d-9f74-7f6ed24fa1a4" Description="" Name="Mask" DisplayName="Mask">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8e02a4cb-5ae1-4adb-a887-8b6f637315db" Description="" Name="DataMemberName" DisplayName="Data Member Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8d0458cf-8b96-4d3c-9ce1-aa46bbab469d" Description="" Name="IgnoreDataMember" DisplayName="Ignore Data Member For Services">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6428006d-4a56-4c1f-bee2-f49785b040fe" Description="Force a media table for this element (e.g.: TABLE_NAME). " Name="CustomMediaTable" DisplayName="Custom Media Table">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ed2b304a-6cc9-4bc1-a25e-842b0ded69d2" Description="This property is never used as a QBE filter." Name="RemoveFilterFromClientLayer" DisplayName="Disable QBE Filtering From Client Side">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="906bf1a1-0db7-47b2-bd8e-f062f81955a1" Description="If true this property never will be fetched from server, it will be used only as a filter. " Name="IgnoreMetaData" DisplayName="Is Only Used As Filter">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="adff4739-00eb-438e-9eb3-bc25908ecfdf" Description="Don't send update command to the database." Name="NoUpdatable" DisplayName="No Updatable" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a29e9d3a-1653-4c84-a97e-9ba095dcd5a3" Description="Filter by count distinct (Ex1: [Value] == 10 || [Value] == [ThisRef].FieldValue, Ex2: [Value] &gt;= 20 &amp;&amp; [Value] &lt;= 50)." Name="CountDistinctFilter" DisplayName="Count Distinct Filter">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e7f6f270-2421-4989-963e-3eb1c89863de" Description="If it is a number, zero value will not allowed." Name="IsZeroNotAllowed" DisplayName="Is Zero Not Allowed">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9f67b459-3ca6-4353-b0df-6f4daf67e688" Description="Controls the decimals of a number by a brand configuration." Name="BrandDecimalsControl" DisplayName="Brand Decimals Control">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="9aeaa519-fc23-4bbe-a573-a21f82b12c66" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterFormula" Name="EntityAdapterFormula" DisplayName="Entity Adapter Formula" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="EntityAdapterAttribute" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="68ad2996-d2a1-4328-a9e8-ab620d75ec8e" Description="Properties that trigger the formula execution. Example: Property1, Property2, ..., PropertyN" Name="TriggerAttributes" DisplayName="Trigger Attributes">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="90738e45-042d-4ced-b3c4-92d58b49acf7" Description="Code that returns the formula value. You can use the following macros:.@Iif[Contition|TrueResult|FalseResult], @Divide[Dividend|Divisor], @Year[exp], @Month[exp], @Day[exp], @DayOfYear[exp], @Hour[exp], @Minute[exp], @Second[exp]." Name="Formula" DisplayName="Formula">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="555cf88c-e791-4a64-b6bb-d1c2b7ab2b8c" Description="The same formula as Linq command or use the following macros:.@Iif[Contition|TrueResult|FalseResult], @Divide[Dividend|Divisor], @Year[exp], @Month[exp], @Day[exp], @DayOfYear[exp], @Hour[exp], @Minute[exp], @Second[exp]." Name="LinqDefinition" DisplayName="DataContext Related Property">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d774b89f-a023-4b4a-a7ef-c799c658e71f" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterFormula.Is Updatable" Name="IsUpdatable" DisplayName="Is Updatable">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="905a9d5b-e099-44fb-9514-ea51bd639eb6" Description="Description for Linx.EntityAdapterDesigner.Comment" Name="Comment" DisplayName="Comment" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="90d1a2ed-8f8d-4a0d-b901-33f593ffd919" Description="Description for Linx.EntityAdapterDesigner.Comment.Text" Name="Text" DisplayName="Text">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="29cca522-43e4-49cf-b34d-b02d08c33597" Description="" Name="GenericOperation" DisplayName="Generic Operation" InheritanceModifier="Abstract" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="062072ca-4cf8-4a57-b12c-74545c62e54a" Description="Operation name." Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2274db2b-b0e6-420c-b5c6-40981e549f1e" Description="Operation description." Name="Comment" DisplayName="Comment">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="aff0a43b-1c1d-442a-ab11-77b7be2dcb01" Description="Return Type." Name="ReturnType" DisplayName="Return Type" DefaultValue="void">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="fe07874a-f234-42c3-bcfc-932763ecf140" Description="Operation Access." Name="Access" DisplayName="Access" DefaultValue="Public">
          <Notes>Operation Access.</Notes>
          <Type>
            <DomainEnumerationMoniker Name="OperationAccess" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="addb0ac6-b298-4b5b-aef2-5a1462ebb74a" Description="Custom attribute definitions. All attribute definitions should be separated per # (Attribute1(ParamList2)#Attribute2(ParamList2)#...)." Name="CustomAttributes" DisplayName="Custom Attributes">
          <Notes>Custom attribute definitions. All attribute definitions should be separated per # (Attribute1(ParamList2)#Attribute2(ParamList2)#...).</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="99219f7c-2119-484c-ab8f-f1eaf4367ba3" Description="Parameters Definitions. All parameters should be separated per # (DataType1 ParamName1#DataType2 ParamName2#...). " Name="Parameters" DisplayName="Parameters">
          <Notes>Parameters Definitions. All parameters should be separated per # (DataType1 ParamName1#DataType2 ParamName2#...). </Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="178061bb-22dc-4268-b999-25b3b3308848" Description="Sets or gets wether or not the item is statically defined." Name="IsStatic" DisplayName="Is Static">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="59f71a6a-626b-4c05-a9f2-b0fd39c3c473" Description="Sets or gets wether or not a function can be overridden." Name="CanOverride" DisplayName="Can Override">
          <Notes>Sets or gets wether or not a function can be overridden.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c0517fab-5978-4d38-a234-f104469c8461" Description="Overload Name for the operation." Name="OverloadName" DisplayName="Overload Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5d5e6b42-7880-4745-ae72-8caa4279f208" Description="Documentation for this opation." Name="DocComment" DisplayName="Doc Comment">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a1554c64-3dcf-42f4-8c90-1bce16679c99" Description="The operation has only one overload." Name="IsUniqueOverload" DisplayName="Is Unique Overload" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="768baa07-b3b1-4054-a54b-004430d47843" Description="Sets or gets wether or not the item is shared." Name="IsShared" DisplayName="Is Shared">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5d97a35c-523b-46cb-9e81-64da56720da0" Description="Indicates a partial method." Name="IsPartial" DisplayName="Is Partial" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8dd93d00-01d7-4b8f-b331-958a6d39571f" Description="Description for Linx.EntityAdapterDesigner.GenericOperation.Is Activity" Name="IsActivity" DisplayName="Is Activity" DefaultValue="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="d31fbf8b-0aff-4ad6-9a69-cfa9a9a208ee" Description="" Name="EntityAdapterEvent" DisplayName="Entity Adapter Event" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="GenericOperation" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="7ccadffc-2ea8-49c1-a1c5-f67c8fee68c9" Description="" Name="DomainServiceOperation" DisplayName="Domain Service Operation" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="GenericOperation" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="e626337b-8304-466e-9e24-63c2005a9576" Description="Describes the attribute type for the DomainService operation." Name="DomainAttribute" DisplayName="Domain Attribute" DefaultValue="Invoke">
          <Type>
            <DomainEnumerationMoniker Name="DomainAttributeType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a8766561-f90f-44d8-b44d-c020aaada9b2" Description="Enable REST calls with JSON endpoint." Name="IsJson" DisplayName="Is Json" DefaultValue="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="588602c7-d614-4b13-b6b2-4f7b782e75b2" Description="Description for Linx.EntityAdapterDesigner.DomainServiceExtension" Name="DomainServiceExtension" DisplayName="Domain Service Extension" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="6daedd20-87de-4f18-aff9-33b32b6f9238" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="DomainServiceOperation" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>DomainServiceExtensionHasDomainServiceOperations.DomainServiceOperations</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="60414ded-0b3d-4ed5-9e12-e56a2c7af740" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapter" Name="LookUpAdapter" DisplayName="Look Up Adapter" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="ba91641e-813d-4ef8-b68a-3975d47a47e9" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapter.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2b6197f2-c839-4ed9-96f6-21194b549676" Description="DataContext entity source." Name="EntitySource" DisplayName="Entity Source" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="790e8a16-f803-4891-82dc-1e8851c278ad" Description="Description for this look up." Name="Description" DisplayName="Description">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8335d5a8-aad4-4366-a4c8-9c38d675b62b" Description="The display name information." Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="13968529-fb97-400f-bbc6-578a236565a6" Description="The relation name on entity." Name="RelationName" DisplayName="Relation Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="14194889-0d0e-4a12-a69c-4987b3bdb842" Description="" Name="IsCustomized" DisplayName="Is Customized" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="527296ca-4264-475a-b802-db7816bdd8ae" Description="Enable multi selection." Name="IsMultiSelection" DisplayName="Is Multi Selection" DefaultValue="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ea2b74ba-922c-4a4e-b209-2d786fd45271" Description="Replace all properties on clear state." Name="ReplaceAllOnClearState" DisplayName="Replace All On Clear State">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ff329f38-aec4-4785-b8fb-0581b1540a4e" Description="UI name for rendering this LookUp (e.g: Linx.Sales.BV.SPA/UI_Name)." Name="SpecializedUI" DisplayName="Specialized UI">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1b0c5476-9437-4774-8ab5-9a95bc1004f2" Description="" Name="DisableSpecializedUI" DisplayName="Specialized UI Disabled">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8b6f2ded-eb29-494d-92cd-fe678025d815" Description="Initial width." Name="Width" DisplayName="Width" DefaultValue="0">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="83f559fc-1744-4950-b3c9-2ac0aeb38318" Description="Initial height." Name="Height" DisplayName="Height" DefaultValue="0">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b363b532-5821-4b66-93fa-58602fb70286" Description="" Name="LookUpClassInfo" DisplayName="Look Up Class Info" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="44a28f8c-bf9a-444f-8473-9f3e47bb2799" Description="This is the return type collection for this entity." Name="QueryReturnType" DisplayName="Query Return Type" DefaultValue="IQueryable">
          <Type>
            <DomainEnumerationMoniker Name="EntityQueryReturnType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="600ef569-33b9-4d18-8117-461747cc2f85" Description="The base type of EntitySource." Name="EntitySourceBase" DisplayName="Entity Source Base">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="79a1c13a-002f-4e3e-8d88-62ac8a03b93b" Description="All entities related with this entity." Name="EntityRelations" DisplayName="Entity Relations" DefaultValue=" " IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5a7845ca-a784-409f-b95b-184b31df301a" Description="Documentation: PropertyName#Operator#DataType{n} where ({n} is the value), (Operators: ==, &gt;=, &lt;=, &gt;, &lt;, In Like) and (DataTypes: S: String, L: Long, H: Short, I: Int, Y: Byte, D: Decimal, C: Char, T: DateTime, B: Boolean, G: Guid, F: Float). E.G.: ProductId#==#I{0};ProductName#Like#S{1}[this.ProductView().ProductId(), 'Shirt%'] " Name="ClientFilterExpression" DisplayName="Client Filter Expression">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3992648b-eabe-467b-bf73-b245579cb072" Description="The client should maintain informations of this lookup, avoiding the database access on each execution." Name="CacheOnClientSide" DisplayName="Cache On Client Side">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="462c2f41-26ef-41ca-acf8-722c00653714" Description="Execute distinct command over the select." Name="Distinct" DisplayName="Distinct">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0d5d7502-63fb-42a0-af6c-6dfd9293f3dc" Description="Filter from ClientFilter or BeforeGetLookup will be considered on clear state." Name="ApplyClientFilterOnClear" DisplayName="Apply Client Filter On Clear" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="99e0233c-17b7-4c20-972e-b03808c50c9c" Description="EF: (SALE_ITEM.PRODUCT.ID_PRODUCT == 10 || SALE_ITEM.SALE.CUSTOMER.ID_CUSTOMER == 1234), OLAP: select { [DimProduct].[color].[color].&amp;[Black] } on columns from [Model]" Name="Filter" DisplayName="Filter">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4298989d-8fd0-41ed-8ebb-40a2efecedd3" Description="Check if the lookup key already exists in target source." Name="CheckExistence" DisplayName="Check Existence">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="77955727-744c-4691-8afa-e2d310a4a6fd" Description="Enable automatic generation of internal SubLookups by the hierarchy of relationship." Name="EnableSubLookups" DisplayName="Enable Sub Lookups" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4bb16be1-2d90-40c8-91b4-ebfcd3911ebb" Description="Enable maintenance for creating new records." Name="CanAddNew" DisplayName="Can Add New">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="LookUpProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>LookUpAdapterHasLookUpProperties.LookUpProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="b73f96a9-6efc-4c15-aced-0c633dae9a7d" Description="Description for Linx.EntityAdapterDesigner.LookUpProperty" Name="LookUpProperty" DisplayName="Look Up Property" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="2bff5e17-2d08-470a-94a0-ad41ce1b5fb0" Description="Description for Linx.EntityAdapterDesigner.LookUpProperty.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="73177819-5068-4d53-a0bc-3b1d3ced21a9" Description="The attribute is visible on UI." Name="IsBrowsable" DisplayName="Is Browsable" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="046b0bd9-a45e-437e-b3fb-d31abb34b907" Description="Property Data Type." Name="Datatype" DisplayName="Datatype" DefaultValue="int">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0489c20b-5324-4478-aa30-0db64d43e359" Description="The display name information." Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a2a8af12-6944-4161-86d5-4e4fc4e00271" Description="This element was customized." Name="IsCustomized" DisplayName="Is Customized" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="869e758e-747e-451f-abe9-2cb7fdd07dec" Description="Data Format (Ex: C02, N02, d)" Name="DataFormatString" DisplayName="Data Format String">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="25e97f12-90e3-46a2-b1a9-72b2d03baf76" Description="Property Precision." Name="Precision" DisplayName="Precision" DefaultValue="0">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3df6259e-b7c2-43fd-aeb0-638a5b6e0c7d" Description="Full field path on DataContext or use the following macros:.@Iif[Contition|TrueResult|FalseResult], @Divide[Dividend|Divisor], @Year[exp], @Month[exp], @Day[exp], @DayOfYear[exp], @Hour[exp], @Minute[exp], @Second[exp]." Name="EdmKey" DisplayName="DataContext Related Property">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cc06a589-dbf2-4706-9e28-2e328f659585" Description="Name of entity property related." Name="EntityPropertyRelated" DisplayName="Entity Property Related">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="21b0e5b3-79a4-4a1f-9292-9672eb770139" Description="This is a primary key." Name="IsPrimaryKey" DisplayName="Is Primary Key">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="07058e66-0282-4692-9384-f74a8b25b656" Description="Domain view name." Name="DomainName" DisplayName="Domain Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b10c0570-61f2-457d-a7b6-f5b9db481569" Description="Key Performance Indicator Reference." Name="KpiName" DisplayName="Kpi Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="41276839-2feb-451b-bda3-2828fca7c12e" Description="Substitute properties for multiple lookup filters.  E.G: Property1,Property2, ...,PropertyN" Name="SubstituteProperties" DisplayName="Substitute Properties">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0469230d-c019-4f63-8e5e-58e822a042d1" Description="If true this property never will be fetched from server, it will be used only as a filter. " Name="IgnoreMetaData" DisplayName="Is Only Used As Filter">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6740add0-6603-493b-96ef-991054cc5b03" Description="This property must be informed before executing this lookup." Name="DependencyProperty" DisplayName="Dependency Property">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d21ee8d4-c923-46ea-af73-5cc5f37f3ce8" Description="Filter by this property (Ex1: [Value] == 10 || [Value] == [ThisRef].FieldValue, Ex2: [Value] &gt;= 20 &amp;&amp; [Value] &lt;= 50)." Name="Filter" DisplayName="Filter">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="61b52bda-d9a8-4e42-91dd-2b9ab2f07f06" Description="e.g.: 1.2.3 or City.State.Country" Name="CustomHierarchy" DisplayName="Custom Hierarchy">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="a63842c2-52c2-4191-8f3d-3a8695935a8c" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterface" Name="EntityAdapterUserInterface" DisplayName="Entity Adapter User Interface" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="c5ac54eb-de86-441b-85d5-6dc8cdecbcb5" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c75ddab0-8723-4d9e-839a-06cb7e33d66f" Description="User Interface Solution Of This Element." Name="SolutionName" DisplayName="Solution Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7c732c6f-8d5b-4700-8528-0754af0bd611" Description="Fields or DataGrid View." Name="StructuralType" DisplayName="Structural Type" DefaultValue="DataFields" IsBrowsable="false">
          <Type>
            <DomainEnumerationMoniker Name="DomainStructuralType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="92e53409-33be-4dd2-a6ce-23acd0582310" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterface.Load Type" Name="LoadType" DisplayName="Load Type" DefaultValue="OnDemand" IsBrowsable="false">
          <Type>
            <DomainEnumerationMoniker Name="DomainLoadType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="36353b19-c6c0-433a-899f-736c0b6b1c26" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterface.Generating Type" Name="GeneratingType" DisplayName="Generating Type" DefaultValue="AutomaticLayout">
          <Type>
            <DomainEnumerationMoniker Name="DomainGeneratingType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="697c4473-c541-4d8e-908c-174432786a92" Description="To disable the paging, set this value to -1. " Name="PageSize" DisplayName="Page Size" DefaultValue="100">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="683d856e-0664-4aa9-a527-f4b89ff2b649" Description="Serialized layout content ." Name="LayoutContent" DisplayName="Layout Content" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7bc91c3f-613a-4cd7-8ede-46ede462fb0e" Description="Specialized Filter." Name="SpecializedLayoutType" DisplayName="Specialized Layout Type" DefaultValue="None">
          <Type>
            <DomainEnumerationMoniker Name="SpecializedLayout" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e9fa22a5-bfaa-4f35-8e94-d4a517dc7b98" Description="" Name="NameSpace" DisplayName="Name Space">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="44a250fa-6631-4e43-865c-d7ed52d14bff" Description="Default Interface." Name="IsDefault" DisplayName="Is Default">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="393027bc-02f7-49e5-9bff-2dbb218117ac" Description="" Name="IsMaintenanceLookUp" DisplayName="Is Maintenance Look Up">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2f180818-2631-4598-b685-7260122cff5f" Description="Execute clear function automatically by parent object." Name="FilterClearIsAutomatic" DisplayName="Filter Clear Is Automatic" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ad9655e3-5ed3-44ce-ab2e-2c297cb4c0eb" Description="" Name="EntityClassInfo" DisplayName="Entity Class Info" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d9467e89-1995-4d8f-a027-a3b00f0a27e6" Description="The environment always search the data, even though the data field is empty." Name="AlwaysSearchIfLookUp" DisplayName="Always Search If Look Up">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ac6f37d3-b6e9-4ce2-9e6b-fcc6c4b63d88" Description="" Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="63236004-1581-4533-9b27-aec6c72f0a45" Description="" Name="SubscriptionNameSpace" DisplayName="Subscription Name Space" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="79462548-ea40-4de6-a198-29d57f37d61d" Description="" Name="SubscriptionEntityAdapterName" DisplayName="Subscription Entity Adapter Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6f56607a-6c66-40e3-af1e-c0d3100a4b3c" Description="Allows the ViewModel customization." Name="HasCustomization" DisplayName="Has Customization">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ac31695d-d1d3-4fc8-9892-c9c830945a1f" Description="The environment executes the query on loading." Name="QueryOnLoad" DisplayName="Query On Load">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="33442a60-2ef5-496c-814c-aa857b45639d" Description="Show table view for wizard forms." Name="EnableWizardTableView" DisplayName="Enable Wizard Table View">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="00280853-d7d4-48ec-ad93-8796aaf983da" Description="Visual type of the user interface." Name="VisualType" DisplayName="Visual Type" DefaultValue="Web">
          <Type>
            <DomainEnumerationMoniker Name="InterfaceType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ca564339-256a-4bd3-b4f8-64aae6fc34da" Description="If true, the value will sorrounded with '%', before searching." Name="UseLikeCommandAsDefault" DisplayName="Use Like Command As Default">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="68459fe4-85b6-45e7-b8c6-5a0665c5d621" Description="Help tags." Name="HelpTags" DisplayName="Help Tags" DefaultValue="MODAprod,Moda">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="UserInterfaceClientEvent" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterUserInterfaceHasUserInterfaceClientEvented.UserInterfaceClientEvented</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="a5bf7b97-614d-4192-a58e-66a68664d77b" Description="Description for Linx.EntityAdapterDesigner.DomainView" Name="DomainView" DisplayName="Domain View" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="0b9dea76-7df2-49e3-8e0d-3f0c551edff5" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e527e6f1-93d6-4cfe-a769-627a8d564e7c" Description="Indicates that the values come via a custom method." Name="HasCustomValues" DisplayName="Has Custom Values">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="DomainValue" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>DomainViewHasDomainValues.DomainValues</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="cb16bdc2-8832-4d16-bcdd-b97334e05581" Description="Description for Linx.EntityAdapterDesigner.DomainValue" Name="DomainValue" DisplayName="Domain Value" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="3ec30921-984c-4dc3-a9e3-1206641aa202" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="892beefd-48bb-4161-81e6-d4286f8881d5" Description="The domain value." Name="Value" DisplayName="Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1baeb7bc-1e7f-444b-bab7-2f46221dfbf9" Description="Represents the display name." Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="3a217e4a-bc37-4f01-9cce-65f578f7dd3a" Description="Description for Linx.EntityAdapterDesigner.Subscription" Name="Subscription" DisplayName="Subscription" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="293e0198-71fd-4de3-93ce-8f9487d5a5d0" Description="" Name="Name" DisplayName="Name" IsElementName="true" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d59f9d5a-4342-4ea0-b18f-ce4062bc6e22" Description="Business Object Assembly Path." Name="BusinessObjectPath" DisplayName="Business Object Path" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="70160a91-2c0b-48d9-a4fe-4ec06965417e" Description="" Name="Title" DisplayName="Title">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e098a667-0677-41ed-82c7-17de44a04208" Description="" Name="HasError" DisplayName="Has Error" Kind="Calculated">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="0d3590ca-e0d6-4152-a72b-b5666a169e80" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterPublicationProperty" Name="EntityAdapterPublicationProperty" DisplayName="Entity Adapter Publication Property" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="EntityAdapterAttribute" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="c45a23af-527e-47a6-95d2-85b43415c4e4" Description="Full field path on DataContext or use the following macros:.@Iif[Contition|TrueResult|FalseResult], @Divide[Dividend|Divisor], @Year[exp], @Month[exp], @Day[exp], @DayOfYear[exp], @Hour[exp], @Minute[exp], @Second[exp]." Name="EdmKey" DisplayName="DataContext Related Property">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="25cb3f3c-24f2-4fb8-b400-2339bbd0a8f5" Description="Constant or Parameter. Parameter example: [PARAMETER NAME]" Name="DefaultValue" DisplayName="Default Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d2a3eeb6-7763-4485-825b-49c6802e42c7" Description="Filter by this property (Ex1: [Value] == 10 || [Value] == [ThisRef].FieldValue, Ex2: [Value] &gt;= 20 &amp;&amp; [Value] &lt;= 50)." Name="Filter" DisplayName="Filter">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="92bda761-6eb7-443b-8894-d3f609430664" Description="Edm key name for this property." Name="TargetKeyName" DisplayName="Target Key Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e6318046-4c67-4d56-9892-81324aa342cb" Description="Suffix for the original publication property." Name="Suffix" DisplayName="Suffix">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="f3a0d64d-3eae-4456-97db-cfdc916dff5b" Description="Description" Name="KeyPerformanceIndicator" DisplayName="Key Performance Indicator" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="68aa58a2-5e37-44be-b5c0-f56f2037c4aa" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d847f44b-0bfb-4282-8f9b-6b6b011fd59c" Description="" Name="Description" DisplayName="Description">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="313b9a40-1835-47b7-858c-d48326ba0cf6" Description="" Name="ShowType" DisplayName="Show Type" DefaultValue="Description">
          <Type>
            <DomainEnumerationMoniker Name="KpiShowType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="eefe2a6d-1ab7-47e3-92f7-a5ed17006004" Description="Description for Linx.EntityAdapterDesigner.KeyPerformanceIndicator.Name Space" Name="NameSpace" DisplayName="Name Space" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="KpiRangeItem" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>KeyPerformanceIndicatorHasKpiRangeItems.KpiRangeItems</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="0117a839-27b1-4472-a87b-46ed0beabf0b" Description="Description for Linx.EntityAdapterDesigner.KpiRangeItem" Name="KpiRangeItem" DisplayName="Kpi Range Item" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="f994d189-5ee8-4d7a-a7d1-c76adaec97d0" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6226fd78-77e5-4690-ba67-1938b445c9e5" Description="" Name="Description" DisplayName="Description">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4e0d1811-90e6-4cea-8ac8-450c2e0c03d1" Description="" Name="StartValue" DisplayName="Start Value" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e989b370-e31d-4fab-8fc4-9c76a5d6c2ed" Description="KPI Color Alpha Part." Name="Alpha" DisplayName="Alpha" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="affd65c9-9681-4511-9ce3-152038539f2d" Description="KPI Color Red Part." Name="Red" DisplayName="Red" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="61272412-ade3-4324-9599-b3a14c4b2150" Description="KPI Color Green Part." Name="Green" DisplayName="Green" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="11781a0f-68e9-4825-97bb-be466c7565b8" Description=" KPI Color Blue Part." Name="Blue" DisplayName="Blue" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2bfcaee8-de64-4750-8f30-87e95ef8a478" Description="" Name="EndValue" DisplayName="End Value" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="2e02b709-792b-4813-a114-b27e71aafebc" Description="Description for Linx.EntityAdapterDesigner.Workflow" Name="Workflow" DisplayName="Workflow" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="e6bb13c6-8671-4757-8e9e-409ed0e4345e" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3089c5f0-7bc0-474d-81f8-de260e354e19" Description="" Name="Comments" DisplayName="Comments">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="70d34c4f-6c0f-492e-810b-5e3ce0afe839" Description="Display Name" Name="Display" DisplayName="Display">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2d833102-471f-41c6-9b27-e1d4e58764ec" Description="" Name="IsOperationRelated" DisplayName="Is Operation Related" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="b613f988-2cb6-485f-abbc-077fadb0f416" Description="Extended Filter For Entity." Name="EntityAdapterExtendedFilter" DisplayName="Entity Adapter Extended Filter" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="cc1d282e-a3d9-44a7-adbf-c16e961e180f" Description="" Name="Name" DisplayName="Name" IsElementName="true" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="112f3d4a-730d-46e1-aa5f-e4ec99b11086" Description="DataContext Entity Name" Name="EntityName" DisplayName="Entity Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="aede79aa-40cd-4b6b-9375-58ebc1013ba6" Description="DataContext Relation Path." Name="RelationName" DisplayName="Relation Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="afcecbd6-436f-4396-9fb6-2286482c0d0c" Description="" Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3e3a203b-c303-42a6-a272-5c78b8b0d431" Description="" Name="DisplayInfo" DisplayName="Display Info" Kind="Calculated" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b5c3cc81-8de0-424d-ae26-cb6479d5d471" Description="" Name="IsUsedInTheLinq" DisplayName="Is Used In The Linq" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EntityAdapterPropertyExtendedFilter" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters.EntityAdapterPropertyExtendedFilters</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="aefed451-eace-44b3-a80b-6b83418123c6" Description="Extended Filter For Field." Name="EntityAdapterPropertyExtendedFilter" DisplayName="Entity Adapter Property Extended Filter" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="7a895ed6-6dc7-4c83-af75-d277f8f14488" Description="" Name="Name" DisplayName="Name" IsElementName="true" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="53f93364-6e69-4d2f-859c-e28d07768783" Description="" Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6230f5ab-7683-478f-b9d4-343266095ba4" Description="" Name="DataType" DisplayName="Data Type" DefaultValue="string" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0228544f-df40-4064-8372-2672b2c38233" Description="Enable this element for filter extension." Name="IsEnabled" DisplayName="Is Enabled" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="fd9a2834-5f42-4f62-b7ba-2949aa44d105" Description="Full field path on DataContext." Name="EdmKey" DisplayName="DataContext Key" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="591e4476-df60-45dc-a3f2-cdbb56f1b7f3" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterRepresentation" Name="EntityAdapterRepresentation" DisplayName="Entity Adapter Representation" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="71b59992-18d3-40a7-8472-af4bf2eac4ff" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="97d01a19-13c2-4aca-985d-fa194c22877f" Description="" Name="TargetEntityAdapterName" DisplayName="Entity Adapter Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0a84f85a-b13f-4ac6-8fba-953d3861ffc0" Description="" Name="BusinessObject" DisplayName="Business Object" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3d8eb95d-a659-4934-9fea-81f461632d0e" Description="" Name="TargetNameSpace" DisplayName="Target Name Space" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5d85204f-a60f-4027-be74-e0c81eeac71e" Description="DataContext Name" Name="TargetEdmName" DisplayName="Target Edm Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="02d58f97-1c06-4d6b-a3e7-da06441318bd" Description="DataContext Entity Name" Name="TargetEdmEntityName" DisplayName="Target Edm Entity Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2f47cbee-102a-4c36-9d67-cc06908eda1f" Description="" Name="IsIQueryable" DisplayName="Is IQueryable" DefaultValue="true" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a655ffe8-73fe-438e-a80e-2c87154575c2" Description="The Publisher allows Insert/Update/Delete operations." Name="IsPublisherUpdatable" DisplayName="Is Publisher Updatable" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9f9aad09-d92a-4859-bf96-776c033f2345" Description="Does not send Insert/Update/Delete operations to Publisher." Name="IsReadOnly" DisplayName="Is Read Only" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2841d2b6-ad73-42ac-9c11-6c5ab885b18a" Description="Generate all non selected properties as extended filters." Name="EnableExtendedFilter" DisplayName="Enable Extended Filter">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f17c3d75-3a6a-4a1b-bf24-16abb9a70b3f" Description="Filter Expression (e.g.: [ThisRef].Field1 == Value1 &amp;&amp; [ThisRef].Field2 &gt; Value2)." Name="Filter" DisplayName="Filter">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="c714f0f7-a0a6-4ba8-b12f-b0c5c886c529" Description="" Name="WebApiAction" DisplayName="Web Api Action" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="GenericOperation" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="f033f205-fec4-4c2b-a4e0-056fd0bb9dc6" Description="Verb definition for this method." Name="HttpVerb" DisplayName="Http Verb" DefaultValue="GET">
          <Notes>Verb definition for this method.</Notes>
          <Type>
            <DomainEnumerationMoniker Name="HttpRouteAttribute" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c5d67c8c-916b-41e3-b992-a51aac362471" Description="Custom routes. All routes should be separated by # (i.g: ./Route1#./Route2#...#./RouteN). Where &quot;.&quot; will be replaced by RouteActionName." Name="CustomRoutes" DisplayName="Custom Routes">
          <Notes>Custom routes. All routes should be separated by # (i.g: ./Route1#./Route2#...#./RouteN). Where "." will be replaced by RouteActionName.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c2ecbc61-7edb-4b20-8d3d-754b33c1b293" Description="This name participates of route. Use &quot;.&quot; for setting the method name." Name="RouteActionName" DisplayName="Route Action Name" DefaultValue=".">
          <Notes>This name participates of route. Use "." for setting the method name.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9fc21b12-bc5a-477f-a0a2-4125b5aa58ec" Description="Enable generation of routes for all parameters." Name="EnableRoutesForParameters" DisplayName="Enable Routes For Parameters">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e3eba5fc-939b-4dee-95f8-54f84605ffdf" Description="" Name="EnableAccessControl" DisplayName="Enable Access Control" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="a4bbf192-983a-4ef1-bf3e-8c1ef85433c6" Description="Operation for a custom repository." Name="RepositoryMethod" DisplayName="Repository Method" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="GenericOperation" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="17e3bca4-62c9-4d80-91c8-e7938f340a58" Description="Description for Linx.EntityAdapterDesigner.WebApiController" Name="WebApiController" DisplayName="Web Api Controller" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="12d5fae7-8893-46a7-9274-2c39afb2d8f4" Description="Description for Linx.EntityAdapterDesigner.WebApiController.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="69d6e828-2d03-41c7-8655-3119cfcf50a2" Description="Route prefix for all actions from this controller." Name="RoutePrefix" DisplayName="Route Prefix">
          <Notes>Route prefix for all actions from this controller.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="52c64d9b-2f11-45e1-a4f9-9860eaafc580" Description="The environment creates or uses the project with this suffix." Name="ProjectSuffix" DisplayName="Project Suffix" DefaultValue="">
          <Notes>The environment creates or uses the project with this suffix.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f54a1f1a-4eb1-4fa3-9256-9234c014f9d6" Description="Enable generation of client project for this API." Name="EnableClient" DisplayName="Enable Client">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0a262c8f-a62c-4bf6-98d5-e5509bab6c72" Description="Generate automatically all data access with this Domain Service." Name="SynchronizedWithDomainService" DisplayName="Synchronized With Domain Service" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2bc14fbd-1d28-4ba1-aadb-977c5ddec764" Description="Enable OData access." Name="IsDataService" DisplayName="Is Data Service">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a2cc4357-e705-4337-abcd-72d953ea2f7b" Description="Description for Linx.EntityAdapterDesigner.WebApiController.Is Asp Net Core" Name="IsAspNetCore" DisplayName="Is Asp Net Core" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="WebApiAction" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>WebApiControllerHasWebApiActions.WebApiActions</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="fbfce393-87b2-400e-bc08-d52aa0498e90" Description="Description for Linx.EntityAdapterDesigner.RepositoryInterface" Name="RepositoryInterface" DisplayName="Repository Interface" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="716155ab-8fa6-4965-a24f-f96ff1d85356" Description="Description for Linx.EntityAdapterDesigner.RepositoryInterface.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0050bb93-3671-433d-8205-dc6ddcfd6646" Description="Represents the base name of related project. [BO] is a macro for teh BO project name." Name="ProjectName" DisplayName="Project Name" DefaultValue="[BO].Repository">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3e8179f1-fc98-4157-9e50-87824fa6cc54" Description="Enable extension for this business object." Name="IsExtension" DisplayName="Is Extension">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="RepositoryMethod" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>RepositoryInterfaceHasRepositoryMethods.RepositoryMethods</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="5eacc4a9-b2ec-4689-b274-7c172107400b" Description="Description for Linx.EntityAdapterDesigner.RepositoryImplementation" Name="RepositoryImplementation" DisplayName="Repository Implementation" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="4c37a7c3-0302-4b3b-ab81-d747b87efa65" Description="Description for Linx.EntityAdapterDesigner.RepositoryImplementation.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d60fc2e8-2615-4d52-9f9f-3f0f0b6724f8" Description="The environment creates or uses the project with this suffix." Name="ProjectSuffix" DisplayName="Project Suffix" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ab8f8b07-d50d-48a2-8ec7-be5598bcd922" Description="Repository name  used by MEF." Name="RepositoryName" DisplayName="Repository Name">
          <Notes>Repository name  used by MEF.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6dcc9387-7236-4b8e-b53e-c7432ae1c1cb" Description="" Name="IsDefault" DisplayName="Is Default">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dddee7b1-3ade-4b32-8ddd-95832ad40ae9" Description="" Name="IsSelected" DisplayName="Is Selected" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="c64873af-346f-40ab-8b70-6d935a917182" Description="" Name="StoreScript" DisplayName="Store Script" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="7b750d0e-138e-4a74-b367-624dda296261" Description="Description for Linx.EntityAdapterDesigner.StoreScript.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="StoreQuery" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>StoreScriptHasStoreQueries.StoreQueries</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="49a64748-11a3-4ff5-94ad-138768d13040" Description="" Name="StoreQuery" DisplayName="Store Query" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="fd6f4dad-b2b1-4a19-baae-143e8c2cd89c" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5b773603-b8b6-4c8a-ba7a-d621e6436f33" Description="e.g.: EXEC LX_PROC {0}, {1}, {2}" Name="Command" DisplayName="Command">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="38f614c9-5bdc-4e50-8ace-f3d789b92124" Description="Parameters Definitions. All parameters should be separated per # (DataType1 ParamName1#DataType2 ParamName2#...). " Name="Parameters" DisplayName="Parameters">
          <Notes>Parameters Definitions. All parameters should be separated per # (DataType1 ParamName1#DataType2 ParamName2#...). </Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7a12cdb8-478a-4972-a98b-9b017a0513cd" Description="Generic type for returning." Name="GenericType" DisplayName="Generic Type" DefaultValue="int">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8bc4b549-4a01-422d-bed7-b497d66ff068" Description="This is the return type collection for this script." Name="QueryReturnType" DisplayName="Query Return Type" DefaultValue="IEnumerable">
          <Type>
            <DomainEnumerationMoniker Name="EntityQueryReturnType" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="e3e4fd01-41f0-46ab-b754-0ad302b4ada8" Description="Description for Linx.EntityAdapterDesigner.OlapCatalog" Name="OlapCatalog" DisplayName="Olap Catalog" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="998296e0-a5f0-4753-a879-fe9c5a82c393" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3532b788-07e9-4f8a-922e-0928fae13725" Description="" Name="Server" DisplayName="Server" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ee7c23fb-5df8-4a6e-bd7d-f887e51c03d9" Description="" Name="Catalog" DisplayName="Catalog" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cad27d4e-8f3f-4b01-8e00-77142f025a13" Description="" Name="UserId" DisplayName="User Id" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6d8938c1-c83a-42c7-ae6c-4e17315ce52f" Description="" Name="Password" DisplayName="Password" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e99b2983-2236-49e1-9328-6bcde9e8e20b" Description="" Name="WindowsAuthentication" DisplayName="Windows Authentication" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2095f28b-e2b2-4f22-9716-86a2e612c3af" Description="Dimensions that contains ID_LINX" Name="IdLinxDimensions" DisplayName="Id Linx Dimensions" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="787e55d7-93cf-43a2-8136-1bdaffa23c25" Description="Dimensions that contains ID_GPECON" Name="IdGpeconDimensions" DisplayName="Id Grupo Econômico Dimensions" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="713af9ea-c4c0-4133-8a73-94b125d040e5" Description="Dimensions that contains ID_BANDEIRA_REDE" Name="IdBandeiraRedeDimensions" DisplayName="Id Bandeira Rede Dimensions" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ab31b4e8-85c6-4566-8dcd-3c75d4083390" Description="Contains measures and the dimensions matching" Name="MeasuresDimensions" DisplayName="Measures x Dimensions" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="148c8c09-2d1f-4fca-bfbb-09e2e9b3fc3f" Description="Dimensions that contains ID_FILIAL" Name="IdFilialDimensions" DisplayName="Id Filial Dimensions" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="bc403adf-6b94-40f6-afd9-cdccea1279f8" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterClientEvent" Name="EntityAdapterClientEvent" DisplayName="Entity Adapter Client Event" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ClientEvent" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="5153f40b-7268-44b6-a4be-12c36649f4fc" Description="Description for Linx.EntityAdapterDesigner.ClientEvent" Name="ClientEvent" DisplayName="Client Event" InheritanceModifier="Abstract" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="GenericOperation" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="2b7094b3-2e94-41d5-96ac-b1eabca4bcef" Description="Script of macros for output coding in any technology." Name="MacroScript" DisplayName="Macro Script" IsBrowsable="false">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Linx.Tools.Automation.StringEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="5f0e1913-fd3d-46e4-b054-8d3c346075f1" Description="Description for Linx.EntityAdapterDesigner.UserInterfaceClientEvent" Name="UserInterfaceClientEvent" DisplayName="User Interface Client Event" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ClientEvent" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="c500c5eb-4027-4007-9acc-8d6891460ae7" Description="Expose this action/event" Name="ExposedByViewModel" DisplayName="Exposed">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="b1922cfd-f7b1-4c2f-9329-b65a223b13ff" Description="Description for Linx.EntityAdapterDesigner.ClientLocalService" Name="ClientLocalService" DisplayName="Client Local Service" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="61f89c4a-ba80-43b4-a4ff-2604db151a2c" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="19fb6bcc-f185-47b4-b9e4-edc07f02f673" Description="To disable the paging, set this value to -1. " Name="PageSize" DisplayName="Page Size" DefaultValue="100">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f1c1130b-2a44-46d4-8ec9-8c5ec14c04b3" Description="Injection of factories and services into this element. E.g: variable1#componentLib1,variable2#componentLib2" Name="ComponentInjection" DisplayName="Component Injection">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ServiceClientEvent" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ClientLocalServiceHasServiceClientEvents.ServiceClientEvents</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ServiceClientProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ClientLocalServiceHasServiceClientProperties.ServiceClientProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="0871eafa-1df4-43fb-9002-0851dd4eccca" Description="Description for Linx.EntityAdapterDesigner.ServiceClientEvent" Name="ServiceClientEvent" DisplayName="Service Client Event" Namespace="Linx.EntityAdapterDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ClientEvent" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="3c5f071f-92bd-4591-ad87-5ebb91ed5811" Description="Expose this action/event" Name="Exposed" DisplayName="Exposed">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="65112f06-7cb7-42a8-b945-d725fd4a2213" Description="This is a message for alerting in broadcast." Name="IsOutputMessage" DisplayName="Is Output Message">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6d1b6080-6285-4d6e-b5a6-04ef635b1ec4" Description="This is a message for alerting in broadcast." Name="IsInputMessage" DisplayName="Is Input Message">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="10813132-c6ea-481b-98de-044cd334e85b" Description="Description for Linx.EntityAdapterDesigner.ServiceClientProperty" Name="ServiceClientProperty" DisplayName="Service Client Property" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="a5ff5c14-efb4-448f-97b2-e4f67949c8b9" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="209035ca-455b-4617-be56-1508d7bddd1f" Description="" Name="DefaultValue" DisplayName="Default Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="80315cb8-54e6-4141-a82f-e9d3dccac5a1" Description="Expose this property" Name="Exposed" DisplayName="Exposed" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="87d8d5c3-bcd6-479e-86d4-7d8f22981dda" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityDataModels" Name="EntityAdapterDesignerRootHasEntityDataModels" DisplayName="Entity Adapter Designer Root Has Entity Data Models" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="b455c8bd-d373-4886-88d6-4a1eabb921aa" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityDataModels.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="EntityDataModels" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Data Models">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="47321b0c-8f04-4b34-97bc-970fee9ddef4" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityDataModels.EntityDataModel" Name="EntityDataModel" DisplayName="Entity Data Model" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="EntityDataModel" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="01718caa-11a1-4da9-a497-22d51cbf562c" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapters" Name="EntityAdapterDesignerRootHasEntityAdapters" DisplayName="Entity Adapter Designer Root Has Entity Adapters" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="24dc82ca-8493-489e-a16f-656c92e0eb70" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapters.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="EntityAdapters" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="83c58d79-2516-4cd9-b436-35de00b5b844" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapters.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="89caaa36-9a87-4e35-b580-a1f6001e850c" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterProperties" Name="EntityAdapterHasEntityAdapterProperties" DisplayName="Entity Adapter Has Entity Adapter Properties" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="e4f10d1d-4096-48f3-856c-445b1f7ab4b6" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterProperties.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterProperties" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Properties">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="37641349-fdf4-4cd0-a50f-d911e5718798" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterProperties.EntityAdapterProperty" Name="EntityAdapterProperty" DisplayName="Entity Adapter Property" PropertyName="EntityAdapter" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="a866b04e-4d5e-4615-a207-249af9a4984d" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterOperations" Name="EntityAdapterHasEntityAdapterOperations" DisplayName="Entity Adapter Has Entity Adapter Operations" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="1c344dfe-d430-420b-a52a-05a2047d5407" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterOperations.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterOperations" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Operations">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="5a3d31dc-de90-4e15-ad4d-219f491a21b1" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterOperations.EntityAdapterOperation" Name="EntityAdapterOperation" DisplayName="Entity Adapter Operation" PropertyName="EntityAdapter" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterOperation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="662ff604-15e7-4a1b-a83d-23d3c63db8c8" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesTargetEntityAdapter" Name="EntityAdapterReferencesTargetEntityAdapter" DisplayName="Entity Adapter References Target Entity Adapter" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="588f2fa7-e5cb-4660-addb-c5541725c7f1" Description="Parent fields separated with commas" Name="ParentKeyFields" DisplayName="Parent Key Fields">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="08d56fbf-e66a-43fa-83f5-95d90c8adc4b" Description="Detail fields separated with commas." Name="DetailKeyFields" DisplayName="Detail Key Fields">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d2c06e4e-2f65-453b-a9cd-f947c30408ae" Description="Detail data member name." Name="DataMemberName" DisplayName="Data Member Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="be1cd246-a2c6-4287-beb1-6fb98bf5d960" Description="The detail will work as a independent master entity, without relations." Name="IsDashboard" DisplayName="Is Dashboard">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7d47913e-79c0-48bf-a7ce-e3623d911c14" Description="Remove query of field if its value is empty." Name="RemoveFieldIfEmpty" DisplayName="Remove Field If Empty">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="f5949ac5-866b-4b81-8206-d67a12138196" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesTargetEntityAdapter.SourceEntityAdapter" Name="SourceEntityAdapter" DisplayName="Source Entity Adapter" PropertyName="TargetEntityAdapter" Multiplicity="ZeroOne" PropertyDisplayName="Target Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3da5a226-e8fd-47bf-accb-2bead665d418" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesTargetEntityAdapter.TargetEntityAdapter" Name="TargetEntityAdapter" DisplayName="Target Entity Adapter" PropertyName="SourceEntityAdapters" PropertyDisplayName="Source Entity Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f154cba1-b88a-4180-be03-a5cfdbf3f5b3" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesEntityDataModel" Name="EntityAdapterReferencesEntityDataModel" DisplayName="Entity Adapter References Entity Data Model" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="b4625b00-a94a-43b8-b8a1-50314f7a0201" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesEntityDataModel.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityDataModel" Multiplicity="ZeroOne" PropertyDisplayName="Entity Data Model">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="5b1fe15c-cd37-4372-9c5f-db10bb9ecda2" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesEntityDataModel.EntityDataModel" Name="EntityDataModel" DisplayName="Entity Data Model" PropertyName="EntityAdapters" PropertyDisplayName="Entity Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityDataModel" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="8f33bdaa-7a90-4834-baff-88c90d2fd673" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterFormulas" Name="EntityAdapterHasEntityAdapterFormulas" DisplayName="Entity Adapter Has Entity Adapter Formulas" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="8792077c-f99c-4447-8445-1207d5086f00" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterFormulas.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterFormulas" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Formulas">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="4bba587e-df61-4762-8cd5-f31bf8ab7a80" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterFormulas.EntityAdapterFormula" Name="EntityAdapterFormula" DisplayName="Entity Adapter Formula" PropertyName="EntityAdapter" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterFormula" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="0ea7f9b4-cdd7-4f7f-8f90-3d578b887d1a" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasComments" Name="EntityAdapterDesignerRootHasComments" DisplayName="Entity Adapter Designer Root Has Comments" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="095f7d61-d191-4bc2-ab51-2a3e3fbef331" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasComments.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="Comments" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="99ad2f67-f7f8-4e71-9a40-d10242d5d146" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasComments.Comment" Name="Comment" DisplayName="Comment" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="ffd43cef-2775-4eeb-bf5d-6c56a42fdb7f" Description="Description for Linx.EntityAdapterDesigner.CommentReferencesEntityAdapters" Name="CommentReferencesEntityAdapters" DisplayName="Comment References Entity Adapters" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="cbc27683-18dd-4e91-971e-9179ac243411" Description="Description for Linx.EntityAdapterDesigner.CommentReferencesEntityAdapters.Comment" Name="Comment" DisplayName="Comment" PropertyName="EntityAdapters" PropertyDisplayName="Entity Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3734c258-a516-4640-9c86-d62cb250484b" Description="Description for Linx.EntityAdapterDesigner.CommentReferencesEntityAdapters.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="Comments" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="6c88af19-2832-41c7-b5d8-91fbdf359773" Description="Description for Linx.EntityAdapterDesigner.CommentReferencesEntityDataModels" Name="CommentReferencesEntityDataModels" DisplayName="Comment References Entity Data Models" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="737988cc-7d84-410e-b81c-f518f7570a33" Description="Description for Linx.EntityAdapterDesigner.CommentReferencesEntityDataModels.Comment" Name="Comment" DisplayName="Comment" PropertyName="EntityDataModels" PropertyDisplayName="Entity Data Models">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="ff8ad432-9e63-4bdd-bcd5-2da8f22262cc" Description="Description for Linx.EntityAdapterDesigner.CommentReferencesEntityDataModels.EntityDataModel" Name="EntityDataModel" DisplayName="Entity Data Model" PropertyName="Comments" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="EntityDataModel" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="8849c7fb-a6af-4575-a8f5-f595c3318074" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterEvents" Name="EntityAdapterHasEntityAdapterEvents" DisplayName="Entity Adapter Has Entity Adapter Events" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="dcc26436-49be-46c0-a6e4-1e88717fa8c0" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterEvents.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterEvents" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Events">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="bb4c6e2d-231c-4e7c-a79b-8761bc6ccc28" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterEvents.EntityAdapterEvent" Name="EntityAdapterEvent" DisplayName="Entity Adapter Event" PropertyName="EntityAdapter" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterEvent" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="537ec93e-bd15-4e14-964a-a5b3e294a569" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasDomainServiceExtensions" Name="EntityAdapterDesignerRootHasDomainServiceExtensions" DisplayName="Entity Adapter Designer Root Has Domain Service Extensions" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="304cb2c4-6044-4494-9ad9-b53697842204" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasDomainServiceExtensions.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="DomainServiceExtensions" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Domain Service Extensions">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="eac5f91d-33d9-43e5-8377-1eadfdc9ec0f" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasDomainServiceExtensions.DomainServiceExtension" Name="DomainServiceExtension" DisplayName="Domain Service Extension" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="DomainServiceExtension" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="5248e870-93ff-4104-aa09-ed82603d5bad" Description="Description for Linx.EntityAdapterDesigner.DomainServiceExtensionHasDomainServiceOperations" Name="DomainServiceExtensionHasDomainServiceOperations" DisplayName="Domain Service Extension Has Domain Service Operations" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="b2d1ed10-2ab5-4ff3-a3af-79474419408c" Description="Description for Linx.EntityAdapterDesigner.DomainServiceExtensionHasDomainServiceOperations.DomainServiceExtension" Name="DomainServiceExtension" DisplayName="Domain Service Extension" PropertyName="DomainServiceOperations" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Domain Service Operations">
          <RolePlayer>
            <DomainClassMoniker Name="DomainServiceExtension" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="15ca2696-deff-4011-8ddf-b32dbb0f82f2" Description="Description for Linx.EntityAdapterDesigner.DomainServiceExtensionHasDomainServiceOperations.DomainServiceOperation" Name="DomainServiceOperation" DisplayName="Domain Service Operation" PropertyName="DomainServiceExtension" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Domain Service Extension">
          <RolePlayer>
            <DomainClassMoniker Name="DomainServiceOperation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="b8d30f8d-3ea1-4153-8183-140716cb3b72" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasLookUpAdapters" Name="EntityAdapterDesignerRootHasLookUpAdapters" DisplayName="Entity Adapter Designer Root Has Look Up Adapters" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="f15c656f-d6ee-4ab1-bede-e0d043d6d241" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasLookUpAdapters.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="LookUpAdapters" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Look Up Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="f76f9791-c6b4-46c5-b501-cbf42a0ea9a0" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasLookUpAdapters.LookUpAdapter" Name="LookUpAdapter" DisplayName="Look Up Adapter" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="LookUpAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="9f1fd533-b678-4eaa-b51d-6fecd9a68d04" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterHasLookUpProperties" Name="LookUpAdapterHasLookUpProperties" DisplayName="Look Up Adapter Has Look Up Properties" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="1f938ae7-f77e-4771-b17f-8926d64bb96b" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterHasLookUpProperties.LookUpAdapter" Name="LookUpAdapter" DisplayName="Look Up Adapter" PropertyName="LookUpProperties" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Look Up Properties">
          <RolePlayer>
            <DomainClassMoniker Name="LookUpAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="d6ba2bfa-38c4-4cff-81c2-54a3bac3de42" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterHasLookUpProperties.LookUpProperty" Name="LookUpProperty" DisplayName="Look Up Property" PropertyName="LookUpAdapter" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Look Up Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="LookUpProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="da81fbfe-b2d7-4b3d-a461-867707941add" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesLookUpAdapters" Name="EntityAdapterReferencesLookUpAdapters" DisplayName="Entity Adapter References Look Up Adapters" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="d78d5a01-465e-4275-bab2-66e5faa7a3b8" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesLookUpAdapters.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="LookUpAdapters" PropertyDisplayName="Look Up Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="ffe0d84d-1573-45d4-9ef4-acd44251210d" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesLookUpAdapters.LookUpAdapter" Name="LookUpAdapter" DisplayName="Look Up Adapter" PropertyName="EntityAdapter" Multiplicity="ZeroOne" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="LookUpAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="a59d9be5-ddd8-4aad-8259-6cac75dd93b8" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapterUserInterfaces" Name="EntityAdapterDesignerRootHasEntityAdapterUserInterfaces" DisplayName="Entity Adapter Designer Root Has Entity Adapter User Interfaces" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="310634bf-5776-4e19-90ba-8b15bd1c6379" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapterUserInterfaces.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="EntityAdapterUserInterfaces" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter User Interfaces">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="4299dcb1-6926-415d-9149-3ff38ffd8b03" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapterUserInterfaces.EntityAdapterUserInterface" Name="EntityAdapterUserInterface" DisplayName="Entity Adapter User Interface" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterUserInterface" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="20e89e23-e175-4866-8667-54907a271586" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesEntityAdapter" Name="EntityAdapterUserInterfaceReferencesEntityAdapter" DisplayName="Entity Adapter User Interface References Entity Adapter" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="66d9b6e8-8131-4202-bff6-486b0f62fd81" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesEntityAdapter.EntityAdapterUserInterface" Name="EntityAdapterUserInterface" DisplayName="Entity Adapter User Interface" PropertyName="EntityAdapter" Multiplicity="ZeroOne" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterUserInterface" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="934c509f-bb8a-48ce-85c8-02f7dba21f85" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesEntityAdapter.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterUserInterfaces" PropertyDisplayName="Entity Adapter User Interfaces">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="1df340d6-4ff0-479f-911b-e77934201ccd" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasDomainViews" Name="EntityAdapterDesignerRootHasDomainViews" DisplayName="Entity Adapter Designer Root Has Domain Views" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="8cb2a70a-cd87-4af9-a04f-963a251c188e" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasDomainViews.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="DomainViews" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Domain Views">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="ffe3fca1-89bb-4a5e-bfe7-d1a38480bd7e" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasDomainViews.DomainView" Name="DomainView" DisplayName="Domain View" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="DomainView" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="ef30cb38-2e32-485f-b854-b0fca11cfebc" Description="Description for Linx.EntityAdapterDesigner.DomainViewHasDomainValues" Name="DomainViewHasDomainValues" DisplayName="Domain View Has Domain Values" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="6f4fc7f7-096d-404a-bb91-32cfb504880f" Description="Description for Linx.EntityAdapterDesigner.DomainViewHasDomainValues.DomainView" Name="DomainView" DisplayName="Domain View" PropertyName="DomainValues" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Domain Values">
          <RolePlayer>
            <DomainClassMoniker Name="DomainView" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="158e700d-34a7-4fcd-80c4-21e11c7cbe9e" Description="Description for Linx.EntityAdapterDesigner.DomainViewHasDomainValues.DomainValue" Name="DomainValue" DisplayName="Domain Value" PropertyName="DomainView" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Domain View">
          <RolePlayer>
            <DomainClassMoniker Name="DomainValue" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="d7d9b4fa-48c5-4366-8974-be8ef7b8cda1" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasSubscriptions" Name="EntityAdapterDesignerRootHasSubscriptions" DisplayName="Entity Adapter Designer Root Has Subscriptions" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="7738ff21-287d-4a1e-91a8-05d75b538fd8" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasSubscriptions.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="Subscriptions" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Subscriptions">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="864e3967-2da1-43d9-8490-bac29bccf837" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasSubscriptions.Subscription" Name="Subscription" DisplayName="Subscription" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="Subscription" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f81ebdfb-227d-49dc-bdd9-a20990310b2f" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterPublicationProperties" Name="EntityAdapterHasEntityAdapterPublicationProperties" DisplayName="Entity Adapter Has Entity Adapter Publication Properties" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="616b29e7-9c45-4b2e-86b0-8e27e01fa988" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterPublicationProperties.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterPublicationProperties" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Publication Properties">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="08d43e0c-f1c2-48ac-aac2-72b4682599e8" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterPublicationProperties.EntityAdapterPublicationProperty" Name="EntityAdapterPublicationProperty" DisplayName="Entity Adapter Publication Property" PropertyName="EntityAdapter" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterPublicationProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="489efe2d-320c-4d9e-8b24-c2b6f9c82484" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasKeyPerformanceIndicators" Name="EntityAdapterDesignerRootHasKeyPerformanceIndicators" DisplayName="Entity Adapter Designer Root Has Key Performance Indicators" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="5ce33980-6254-473a-9721-fdc7b767c5ac" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasKeyPerformanceIndicators.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="KeyPerformanceIndicators" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Key Performance Indicators">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a39d431a-5e02-49d9-8ddc-1eaef9416c2c" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasKeyPerformanceIndicators.KeyPerformanceIndicator" Name="KeyPerformanceIndicator" DisplayName="Key Performance Indicator" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="KeyPerformanceIndicator" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="5d406425-1d3f-46cc-831d-ae7cbe8f1eea" Description="Description for Linx.EntityAdapterDesigner.KeyPerformanceIndicatorHasKpiRangeItems" Name="KeyPerformanceIndicatorHasKpiRangeItems" DisplayName="Key Performance Indicator Has Kpi Range Items" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="92835dc3-f44f-4877-9762-b3609ea92581" Description="Description for Linx.EntityAdapterDesigner.KeyPerformanceIndicatorHasKpiRangeItems.KeyPerformanceIndicator" Name="KeyPerformanceIndicator" DisplayName="Key Performance Indicator" PropertyName="KpiRangeItems" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Kpi Range Items">
          <RolePlayer>
            <DomainClassMoniker Name="KeyPerformanceIndicator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9616924f-aafd-4449-9a1f-d35769b0d43f" Description="Description for Linx.EntityAdapterDesigner.KeyPerformanceIndicatorHasKpiRangeItems.KpiRangeItem" Name="KpiRangeItem" DisplayName="Kpi Range Item" PropertyName="KeyPerformanceIndicator" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Key Performance Indicator">
          <RolePlayer>
            <DomainClassMoniker Name="KpiRangeItem" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="cea54165-36d5-415d-afd6-68ce3fd0d42e" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasWorkflows" Name="EntityAdapterDesignerRootHasWorkflows" DisplayName="Entity Adapter Designer Root Has Workflows" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="f37a66f2-8dfc-40e6-9f33-77039947405a" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasWorkflows.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="Workflows" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Workflows">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c8d5606f-5fa5-4f65-a3be-d86cfe7c8db7" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasWorkflows.Workflow" Name="Workflow" DisplayName="Workflow" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="Workflow" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2ba4132a-5bae-46c8-bcc2-2df7c5c46c29" Description="Description for Linx.EntityAdapterDesigner.GenericOperationReferencesWorkflow" Name="GenericOperationReferencesWorkflow" DisplayName="Generic Operation References Workflow" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="a8d12e60-e8fa-4ef4-8014-85b867fba602" Description="Description for Linx.EntityAdapterDesigner.GenericOperationReferencesWorkflow.GenericOperation" Name="GenericOperation" DisplayName="Generic Operation" PropertyName="Workflow" Multiplicity="ZeroOne" PropertyDisplayName="Workflow">
          <RolePlayer>
            <DomainClassMoniker Name="GenericOperation" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="127bec48-3fd3-40c9-a776-1ad880863416" Description="Description for Linx.EntityAdapterDesigner.GenericOperationReferencesWorkflow.Workflow" Name="Workflow" DisplayName="Workflow" PropertyName="GenericOperation" Multiplicity="ZeroOne" PropertyDisplayName="Generic Operation">
          <RolePlayer>
            <DomainClassMoniker Name="Workflow" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="9cc7962c-416c-427c-b862-163ab8bc78f6" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters" Name="EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters" DisplayName="Entity Adapter Extended Filter Has Entity Adapter Property Extended Filters" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="d0dfde07-fd36-486c-88bd-de29953e4554" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters.EntityAdapterExtendedFilter" Name="EntityAdapterExtendedFilter" DisplayName="Entity Adapter Extended Filter" PropertyName="EntityAdapterPropertyExtendedFilters" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Property Extended Filters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterExtendedFilter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="23f9bbd0-e627-48a7-b368-6d7656bd149e" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters.EntityAdapterPropertyExtendedFilter" Name="EntityAdapterPropertyExtendedFilter" DisplayName="Entity Adapter Property Extended Filter" PropertyName="EntityAdapterExtendedFilter" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Extended Filter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterPropertyExtendedFilter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="dea0fbbc-d541-4477-af67-c5c0dd28a92d" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterExtendedFilters" Name="EntityAdapterHasEntityAdapterExtendedFilters" DisplayName="Entity Adapter Has Entity Adapter Extended Filters" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="3c71d08e-dcab-4d13-a9c5-45880f21b329" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterExtendedFilters.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterExtendedFilters" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Extended Filters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="40599760-07e0-47ad-aa15-6cb822d24a2d" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterExtendedFilters.EntityAdapterExtendedFilter" Name="EntityAdapterExtendedFilter" DisplayName="Entity Adapter Extended Filter" PropertyName="EntityAdapter" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterExtendedFilter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2cfb37fd-84cc-446b-919c-be6b86ffa335" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesBaseEntityAdapter" Name="EntityAdapterReferencesBaseEntityAdapter" DisplayName="Entity Adapter References Base Entity Adapter" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="d57c324c-ac10-49db-a338-eb8eec8e078d" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesBaseEntityAdapter.SourceEntityAdapter" Name="SourceEntityAdapter" DisplayName="Source Entity Adapter" PropertyName="BaseEntityAdapter" Multiplicity="ZeroOne" PropertyDisplayName="Base Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9a5529a2-1eaa-4232-8d69-3694ec3431ac" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesBaseEntityAdapter.TargetEntityAdapter" Name="TargetEntityAdapter" DisplayName="Target Entity Adapter" PropertyName="DerivedEntityAdapters" PropertyDisplayName="Derived Entity Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="b0436d88-30dd-4181-aa11-9e6d5cbd2187" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterReferencesBaseLookUpAdapter" Name="LookUpAdapterReferencesBaseLookUpAdapter" DisplayName="Look Up Adapter References Base Look Up Adapter" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="0c3c22bd-fb01-43f8-869a-0353578aa5e2" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterReferencesBaseLookUpAdapter.SourceLookUpAdapter" Name="SourceLookUpAdapter" DisplayName="Source Look Up Adapter" PropertyName="BaseLookUpAdapter" Multiplicity="ZeroOne" PropertyDisplayName="Base Look Up Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="LookUpAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9b4c76e3-3b60-41af-8592-d4631f5436df" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterReferencesBaseLookUpAdapter.TargetLookUpAdapter" Name="TargetLookUpAdapter" DisplayName="Target Look Up Adapter" PropertyName="DerivedLookUpAdapters" PropertyDisplayName="Derived Look Up Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="LookUpAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="3b55396b-777e-4855-9ec7-53ee20a0b359" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesLocalEntityAdapter" Name="EntityAdapterReferencesLocalEntityAdapter" DisplayName="Entity Adapter References Local Entity Adapter" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="169576f3-6a84-49b1-9c8b-f8c0ef364280" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesLocalEntityAdapter.SourceEntityAdapter" Name="SourceEntityAdapter" DisplayName="Source Entity Adapter" PropertyName="LocalEntityAdapter" Multiplicity="ZeroOne" PropertyDisplayName="Local Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="27e268ba-f321-4880-b972-34036fccd206" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesLocalEntityAdapter.TargetEntityAdapter" Name="TargetEntityAdapter" DisplayName="Target Entity Adapter" PropertyName="LocalResultEntityAdapters" PropertyDisplayName="Local Result Entity Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="28b4209b-e346-4cdd-b457-4e0792f50355" Description="Description for Linx.EntityAdapterDesigner.UserInterfaceReferencesBaseUserInterface" Name="UserInterfaceReferencesBaseUserInterface" DisplayName="User Interface References Base User Interface" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="14379cac-b47d-41a7-8bd6-ec5208f6cd4e" Description="Description for Linx.EntityAdapterDesigner.UserInterfaceReferencesBaseUserInterface.SourceEntityAdapterUserInterface" Name="SourceEntityAdapterUserInterface" DisplayName="Source Entity Adapter User Interface" PropertyName="BaseUserInterface" Multiplicity="ZeroOne" PropertyDisplayName="Base User Interface">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterUserInterface" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="d40904e0-8dbe-4734-9852-1ba79cec87a6" Description="Description for Linx.EntityAdapterDesigner.UserInterfaceReferencesBaseUserInterface.TargetEntityAdapterUserInterface" Name="TargetEntityAdapterUserInterface" DisplayName="Target Entity Adapter User Interface" PropertyName="DerivedUserInterfaces" PropertyDisplayName="Derived User Interfaces">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterUserInterface" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f4d5ea6b-a320-4eb5-87cf-113f3d37f8e1" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation" Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation" DisplayName="Entity Adapter Representation References Target Entity Adapter Representation" Namespace="Linx.EntityAdapterDesigner">
      <Properties>
        <DomainProperty Id="e17f4613-1c0f-43f5-a6aa-7a52a552ebdb" Description="Join Type For Linq Command." Name="JoinType" DisplayName="Join Type" DefaultValue="InnerJoin">
          <Type>
            <DomainEnumerationMoniker Name="EntityAdapterJoinType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="72e9094a-a2dc-4ec0-9b7a-0d8180ea2b9e" Description="Properties of Target separated by commas." Name="TargetProperties" DisplayName="Target Properties" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="51942e0c-e3dd-4ad3-b3c0-52207a215143" Description="Properties of Target separated by commas." Name="SourceProperties" DisplayName="Source Properties" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="f1410d1b-4653-4415-a6e6-328255f4614a" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation.SourceEntityAdapterRepresentation" Name="SourceEntityAdapterRepresentation" DisplayName="Source Entity Adapter Representation" PropertyName="TargetEntityAdapterRepresentation" Multiplicity="ZeroOne" PropertyDisplayName="Target Entity Adapter Representation">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterRepresentation" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="d740e594-ff9b-483b-8047-1bc6df97d2a6" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation.TargetEntityAdapterRepresentation" Name="TargetEntityAdapterRepresentation" DisplayName="Target Entity Adapter Representation" PropertyName="SourceEntityAdapterRepresentations" PropertyDisplayName="Source Entity Adapter Representations">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterRepresentation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="06f9d489-bad6-41d7-b5e2-f26ee52f4713" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapterRepresentations" Name="EntityAdapterDesignerRootHasEntityAdapterRepresentations" DisplayName="Entity Adapter Designer Root Has Entity Adapter Representations" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="656365de-05aa-4364-9cef-79ddb4d281cc" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapterRepresentations.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="EntityAdapterRepresentations" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Representations">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="0b389427-e0b2-4b70-b732-9452278855a5" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasEntityAdapterRepresentations.EntityAdapterRepresentation" Name="EntityAdapterRepresentation" DisplayName="Entity Adapter Representation" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterRepresentation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="8fc7408a-3299-4390-9e42-7c7a93e79b74" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesEntityAdapterRepresentation" Name="EntityAdapterReferencesEntityAdapterRepresentation" DisplayName="Entity Adapter References Entity Adapter Representation" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="c3c9b1bd-8fd3-4d52-a04c-b7264e35f164" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesEntityAdapterRepresentation.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterRepresentation" Multiplicity="ZeroOne" PropertyDisplayName="Entity Adapter Representation">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="384e8e6a-9c21-4255-83aa-4499cd0f0a01" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesEntityAdapterRepresentation.EntityAdapterRepresentation" Name="EntityAdapterRepresentation" DisplayName="Entity Adapter Representation" PropertyName="EntityAdapters" PropertyDisplayName="Entity Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterRepresentation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="24302ce2-674e-4a7e-9424-3b35bcdb20d4" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasWebApiControllers" Name="EntityAdapterDesignerRootHasWebApiControllers" DisplayName="Entity Adapter Designer Root Has Web Api Controllers" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="1ffaa959-d3db-49e2-b49a-1a22ce16546a" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasWebApiControllers.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="WebApiControllers" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Web Api Controllers">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="2da7c698-9aa4-4554-b05e-70a5a606d8c8" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasWebApiControllers.WebApiController" Name="WebApiController" DisplayName="Web Api Controller" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="WebApiController" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="5ee72196-f75c-4b64-a572-706f3c84943a" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasRepositoryInterfaces" Name="EntityAdapterDesignerRootHasRepositoryInterfaces" DisplayName="Entity Adapter Designer Root Has Repository Interfaces" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="dbee34d3-87d6-452e-8ac7-e85f1229b79b" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasRepositoryInterfaces.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="RepositoryInterfaces" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Repository Interfaces">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3669e639-da6b-4860-88c0-8f09e39d600f" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasRepositoryInterfaces.RepositoryInterface" Name="RepositoryInterface" DisplayName="Repository Interface" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="RepositoryInterface" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="0823d572-c2b0-48dc-9619-72214ac3d5e0" Description="Description for Linx.EntityAdapterDesigner.WebApiControllerHasWebApiActions" Name="WebApiControllerHasWebApiActions" DisplayName="Web Api Controller Has Web Api Actions" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="fb375763-d3f4-4587-b4dd-872c1cca937c" Description="Description for Linx.EntityAdapterDesigner.WebApiControllerHasWebApiActions.WebApiController" Name="WebApiController" DisplayName="Web Api Controller" PropertyName="WebApiActions" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Web Api Actions">
          <RolePlayer>
            <DomainClassMoniker Name="WebApiController" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="e1f7426c-4dc3-40f1-b495-3006c601cf35" Description="Description for Linx.EntityAdapterDesigner.WebApiControllerHasWebApiActions.WebApiAction" Name="WebApiAction" DisplayName="Web Api Action" PropertyName="WebApiController" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Web Api Controller">
          <RolePlayer>
            <DomainClassMoniker Name="WebApiAction" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f668b3a7-9ad5-4c00-9778-a6206d49d857" Description="Description for Linx.EntityAdapterDesigner.RepositoryInterfaceHasRepositoryMethods" Name="RepositoryInterfaceHasRepositoryMethods" DisplayName="Repository Interface Has Repository Methods" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="3e7bf1f6-afcf-4b98-99ec-fc53ba2cc2cd" Description="Description for Linx.EntityAdapterDesigner.RepositoryInterfaceHasRepositoryMethods.RepositoryInterface" Name="RepositoryInterface" DisplayName="Repository Interface" PropertyName="RepositoryMethods" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Repository Methods">
          <RolePlayer>
            <DomainClassMoniker Name="RepositoryInterface" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="cf6b308a-5ce6-45f7-a216-49d57b096d27" Description="Description for Linx.EntityAdapterDesigner.RepositoryInterfaceHasRepositoryMethods.RepositoryMethod" Name="RepositoryMethod" DisplayName="Repository Method" PropertyName="RepositoryInterface" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Repository Interface">
          <RolePlayer>
            <DomainClassMoniker Name="RepositoryMethod" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="39697d55-8e2c-4779-8f36-7fff7b73bb04" Description="Description for Linx.EntityAdapterDesigner.WebApiControllerReferencesRepositoryInterface" Name="WebApiControllerReferencesRepositoryInterface" DisplayName="Web Api Controller References Repository Interface" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="70e027cf-45c2-48d7-9c00-a0f150f47f50" Description="Description for Linx.EntityAdapterDesigner.WebApiControllerReferencesRepositoryInterface.WebApiController" Name="WebApiController" DisplayName="Web Api Controller" PropertyName="RepositoryInterface" Multiplicity="ZeroOne" PropertyDisplayName="Repository Interface">
          <RolePlayer>
            <DomainClassMoniker Name="WebApiController" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="be0e4a21-5a5a-4cf2-9b2e-c54dc11eaf0a" Description="Description for Linx.EntityAdapterDesigner.WebApiControllerReferencesRepositoryInterface.RepositoryInterface" Name="RepositoryInterface" DisplayName="Repository Interface" PropertyName="WebApiControllers" PropertyDisplayName="Web Api Controllers">
          <RolePlayer>
            <DomainClassMoniker Name="RepositoryInterface" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="7d2d98f4-784c-47c5-90a3-0540fa060091" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasRepositoryImplementations" Name="EntityAdapterDesignerRootHasRepositoryImplementations" DisplayName="Entity Adapter Designer Root Has Repository Implementations" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="5ab3652a-d017-400e-a99b-d03fb656b7b9" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasRepositoryImplementations.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="RepositoryImplementations" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Repository Implementations">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="46fc2e7b-1c23-42c5-954a-762d6488861d" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasRepositoryImplementations.RepositoryImplementation" Name="RepositoryImplementation" DisplayName="Repository Implementation" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="RepositoryImplementation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="d054de66-790d-4aef-bd9d-bc6207005c70" Description="Description for Linx.EntityAdapterDesigner.RepositoryImplementationReferencesRepositoryInterface" Name="RepositoryImplementationReferencesRepositoryInterface" DisplayName="Repository Implementation References Repository Interface" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="375eaa6f-8d13-472e-a7a2-0cc0a6bed569" Description="Description for Linx.EntityAdapterDesigner.RepositoryImplementationReferencesRepositoryInterface.RepositoryImplementation" Name="RepositoryImplementation" DisplayName="Repository Implementation" PropertyName="RepositoryInterface" Multiplicity="ZeroOne" PropertyDisplayName="Repository Interface">
          <RolePlayer>
            <DomainClassMoniker Name="RepositoryImplementation" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3fdb784a-9718-4834-9034-714c34e6d67d" Description="Description for Linx.EntityAdapterDesigner.RepositoryImplementationReferencesRepositoryInterface.RepositoryInterface" Name="RepositoryInterface" DisplayName="Repository Interface" PropertyName="RepositoryImplementations" PropertyDisplayName="Repository Implementations">
          <RolePlayer>
            <DomainClassMoniker Name="RepositoryInterface" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="b944f902-1e4c-439f-a2e6-2d2cd3ab5ee3" Description="Description for Linx.EntityAdapterDesigner.EntityCollectionReferencesEntityOwners" Name="EntityCollectionReferencesEntityOwners" DisplayName="Entity Collection References Entity Owners" Namespace="Linx.EntityAdapterDesigner" AllowsDuplicates="true">
      <Properties>
        <DomainProperty Id="2275f1cb-59d6-4e19-985b-92ccb2ab63e9" Description="" Name="DataMemberName" DisplayName="Data Member Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9bcd718d-b36c-4783-b271-11a400d553fd" Description="Collection name." Name="Name" DisplayName="Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4767786b-349b-478f-a775-6edb18304a4a" Description="" Name="DataType" DisplayName="Data Type" DefaultValue="IEnumerable&lt;T&gt;">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e076bcf6-ca6f-457f-9127-b4f750e2a0cb" Description="" Name="CreateEmptyInstance" DisplayName="Create Empty Instance" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="f9ba77ef-1cde-4ca3-9f5c-5e73f3ff050f" Description="Description for Linx.EntityAdapterDesigner.EntityCollectionReferencesEntityOwners.SourceEntityAdapter" Name="SourceEntityAdapter" DisplayName="Source Entity Adapter" PropertyName="OwnerCollectionEntities" PropertyDisplayName="Owner Collection Entities">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="41d2610d-5d72-470a-9346-d8091374202d" Description="Description for Linx.EntityAdapterDesigner.EntityCollectionReferencesEntityOwners.TargetEntityAdapter" Name="TargetEntityAdapter" DisplayName="Target Entity Adapter" PropertyName="CollectionEntities" PropertyDisplayName="Collection Entities">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="91f610b3-efca-4f28-8624-0fd6837eda95" Description="Description for Linx.EntityAdapterDesigner.EntityInstanceReferencesEntityOwners" Name="EntityInstanceReferencesEntityOwners" DisplayName="Entity Instance References Entity Owners" Namespace="Linx.EntityAdapterDesigner" AllowsDuplicates="true">
      <Properties>
        <DomainProperty Id="467f1846-90e2-494a-a6d6-9b0d846f33e9" Description="" Name="DataMemberName" DisplayName="Data Member Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f1089587-91d0-46a0-b0c5-e168a2a7f21e" Description="Collection name." Name="Name" DisplayName="Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="f3cfa2f2-16a5-4532-aaa5-f6c6717cacdf" Description="Description for Linx.EntityAdapterDesigner.EntityInstanceReferencesEntityOwners.SourceEntityAdapter" Name="SourceEntityAdapter" DisplayName="Source Entity Adapter" PropertyName="OwnerInstanceEntities" PropertyDisplayName="Owner Instance Entities">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a3b973b4-bdc1-4939-bcb9-cba065f8db9b" Description="Description for Linx.EntityAdapterDesigner.EntityInstanceReferencesEntityOwners.TargetEntityAdapter" Name="TargetEntityAdapter" DisplayName="Target Entity Adapter" PropertyName="InstanceEntities" PropertyDisplayName="Instance Entities">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="19f6210c-5bb5-4b5e-842f-5efb4b808392" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesSubscription" Name="EntityAdapterUserInterfaceReferencesSubscription" DisplayName="Entity Adapter User Interface References Subscription" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="0f8dacd2-c0c9-4ec5-8d2a-ec3fab76c424" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesSubscription.EntityAdapterUserInterface" Name="EntityAdapterUserInterface" DisplayName="Entity Adapter User Interface" PropertyName="Subscription" Multiplicity="ZeroOne" PropertyDisplayName="Subscription">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterUserInterface" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="14f3f525-5c76-4330-a6bc-4e64bc0f9b69" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesSubscription.Subscription" Name="Subscription" DisplayName="Subscription" PropertyName="EntityAdapterUserInterfaces" PropertyDisplayName="Entity Adapter User Interfaces">
          <RolePlayer>
            <DomainClassMoniker Name="Subscription" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="86759af8-8908-48f8-abf1-d8809a826b52" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterReferencesEntityDataModel" Name="LookUpAdapterReferencesEntityDataModel" DisplayName="Look Up Adapter References Entity Data Model" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="6e7ca66a-55ef-40d6-ac05-fce29dd69673" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterReferencesEntityDataModel.LookUpAdapter" Name="LookUpAdapter" DisplayName="Look Up Adapter" PropertyName="EntityDataModel" Multiplicity="ZeroOne" PropertyDisplayName="Entity Data Model">
          <RolePlayer>
            <DomainClassMoniker Name="LookUpAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="aedf31dc-aa85-4213-989e-055654d5afc6" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterReferencesEntityDataModel.EntityDataModel" Name="EntityDataModel" DisplayName="Entity Data Model" PropertyName="LookUpAdapters" PropertyDisplayName="Look Up Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="EntityDataModel" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="80eff3a7-8acf-4211-8fb3-5a88a80879a5" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasStoreScripts" Name="EntityAdapterDesignerRootHasStoreScripts" DisplayName="Entity Adapter Designer Root Has Store Scripts" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="6166c4d4-2b7c-4330-b3c0-639c6ddddb68" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasStoreScripts.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="StoreScripts" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Store Scripts">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="e5093de3-0aec-453b-bd2a-ef7f039885c1" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasStoreScripts.StoreScript" Name="StoreScript" DisplayName="Store Script" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="StoreScript" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="94f3ef6e-eb75-4681-82d6-7a73cfafd382" Description="Description for Linx.EntityAdapterDesigner.StoreScriptHasStoreQueries" Name="StoreScriptHasStoreQueries" DisplayName="Store Script Has Store Queries" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="29baf3ef-6be8-47c2-9c76-4f0d0eaabf7e" Description="Description for Linx.EntityAdapterDesigner.StoreScriptHasStoreQueries.StoreScript" Name="StoreScript" DisplayName="Store Script" PropertyName="StoreQueries" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Store Queries">
          <RolePlayer>
            <DomainClassMoniker Name="StoreScript" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="76bd5fd9-2762-4e6a-b178-8a0cd2fb3758" Description="Description for Linx.EntityAdapterDesigner.StoreScriptHasStoreQueries.StoreQuery" Name="StoreQuery" DisplayName="Store Query" PropertyName="StoreScript" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Store Script">
          <RolePlayer>
            <DomainClassMoniker Name="StoreQuery" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="45cc9677-4730-4cb6-811f-6d8f05b1fa5f" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasOlapCatalogs" Name="EntityAdapterDesignerRootHasOlapCatalogs" DisplayName="Entity Adapter Designer Root Has Olap Catalogs" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="9b5b009b-d285-4ced-9ebc-f68955100ebe" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasOlapCatalogs.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="OlapCatalogs" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Olap Catalogs">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="59257aeb-d0d1-41bb-b9a0-77a6d871bdd4" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasOlapCatalogs.OlapCatalog" Name="OlapCatalog" DisplayName="Olap Catalog" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="OlapCatalog" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="135a4ceb-141b-4371-b203-d068505fef4b" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesOlapCatalog" Name="EntityAdapterReferencesOlapCatalog" DisplayName="Entity Adapter References Olap Catalog" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="e437fc3a-064e-47af-a18b-00cfd1dd9ff5" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesOlapCatalog.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="OlapCatalog" Multiplicity="ZeroOne" PropertyDisplayName="Olap Catalog">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="2ca17852-24d3-49c0-8c49-4a36ccbdbd49" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterReferencesOlapCatalog.OlapCatalog" Name="OlapCatalog" DisplayName="Olap Catalog" PropertyName="EntityAdapters" PropertyDisplayName="Entity Adapters">
          <RolePlayer>
            <DomainClassMoniker Name="OlapCatalog" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="9681fb83-34d0-4463-bd97-58f092be943c" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterClientEvented" Name="EntityAdapterHasEntityAdapterClientEvented" DisplayName="Entity Adapter Has Entity Adapter Client Evented" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="4277478a-6706-4d95-ade3-e01bb5830db2" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterClientEvented.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="EntityAdapterClientEvented" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Entity Adapter Client Evented">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="58b37746-bf9b-4a8b-9584-bb3705a53d31" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterHasEntityAdapterClientEvented.EntityAdapterClientEvent" Name="EntityAdapterClientEvent" DisplayName="Entity Adapter Client Event" PropertyName="EntityAdapter" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterClientEvent" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="390208b6-2921-4ead-b88a-5dea724d8f6d" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceHasUserInterfaceClientEvented" Name="EntityAdapterUserInterfaceHasUserInterfaceClientEvented" DisplayName="Entity Adapter User Interface Has User Interface Client Evented" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="f574fe87-8aa0-4065-b9fd-140f78ed887e" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceHasUserInterfaceClientEvented.EntityAdapterUserInterface" Name="EntityAdapterUserInterface" DisplayName="Entity Adapter User Interface" PropertyName="UserInterfaceClientEvented" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="User Interface Client Evented">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterUserInterface" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="f763d440-8df8-4968-b378-0f69949b67ed" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceHasUserInterfaceClientEvented.UserInterfaceClientEvent" Name="UserInterfaceClientEvent" DisplayName="User Interface Client Event" PropertyName="EntityAdapterUserInterface" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter User Interface">
          <RolePlayer>
            <DomainClassMoniker Name="UserInterfaceClientEvent" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="cc1f1695-f154-4213-b3f1-ca133469d4c8" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasClientLocalServices" Name="EntityAdapterDesignerRootHasClientLocalServices" DisplayName="Entity Adapter Designer Root Has Client Local Services" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="17efa1f3-f822-454b-88f7-a9749c4c5b56" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasClientLocalServices.EntityAdapterDesignerRoot" Name="EntityAdapterDesignerRoot" DisplayName="Entity Adapter Designer Root" PropertyName="ClientLocalServices" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Client Local Services">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3427fd68-e348-4df4-9dc7-73fdba07a761" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterDesignerRootHasClientLocalServices.ClientLocalService" Name="ClientLocalService" DisplayName="Client Local Service" PropertyName="EntityAdapterDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Entity Adapter Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="ClientLocalService" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f8c286f6-dd6b-4097-97d7-c779c86394a9" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesClientLocalService" Name="EntityAdapterUserInterfaceReferencesClientLocalService" DisplayName="Entity Adapter User Interface References Client Local Service" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="c8f2d14f-626d-4b93-a88b-4e565c4fb980" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesClientLocalService.EntityAdapterUserInterface" Name="EntityAdapterUserInterface" DisplayName="Entity Adapter User Interface" PropertyName="ClientLocalService" Multiplicity="ZeroOne" PropertyDisplayName="Client Local Service">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapterUserInterface" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9922548b-85d2-4a94-9595-71249caf9591" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceReferencesClientLocalService.ClientLocalService" Name="ClientLocalService" DisplayName="Client Local Service" PropertyName="EntityAdapterUserInterfaces" PropertyDisplayName="Entity Adapter User Interfaces">
          <RolePlayer>
            <DomainClassMoniker Name="ClientLocalService" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="815426b4-7ed1-42a1-8e96-fbdaa6e713eb" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceHasServiceClientEvents" Name="ClientLocalServiceHasServiceClientEvents" DisplayName="Client Local Service Has Service Client Events" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="7a682d0a-4ab7-4bf0-85bb-45b2e4ec2712" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceHasServiceClientEvents.ClientLocalService" Name="ClientLocalService" DisplayName="Client Local Service" PropertyName="ServiceClientEvents" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Service Client Events">
          <RolePlayer>
            <DomainClassMoniker Name="ClientLocalService" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="63a82d71-2ce0-4637-aaf6-c2d5e51b7289" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceHasServiceClientEvents.ServiceClientEvent" Name="ServiceClientEvent" DisplayName="Service Client Event" PropertyName="ClientLocalService" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Client Local Service">
          <RolePlayer>
            <DomainClassMoniker Name="ServiceClientEvent" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="a0ec22a7-26dc-4922-8637-12029c56fb0c" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceReferencesEntityAdapter" Name="ClientLocalServiceReferencesEntityAdapter" DisplayName="Client Local Service References Entity Adapter" Namespace="Linx.EntityAdapterDesigner">
      <Source>
        <DomainRole Id="5236d6e5-339b-4fb9-a318-590e9b9db8c2" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceReferencesEntityAdapter.ClientLocalService" Name="ClientLocalService" DisplayName="Client Local Service" PropertyName="EntityAdapter" Multiplicity="ZeroOne" PropertyDisplayName="Entity Adapter">
          <RolePlayer>
            <DomainClassMoniker Name="ClientLocalService" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="af44d770-8864-4062-ab0d-494f06ec35ee" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceReferencesEntityAdapter.EntityAdapter" Name="EntityAdapter" DisplayName="Entity Adapter" PropertyName="ClientLocalServices" PropertyDisplayName="Client Local Services">
          <RolePlayer>
            <DomainClassMoniker Name="EntityAdapter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="e4a5a956-6488-4202-8351-147a9b6295e2" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceHasServiceClientProperties" Name="ClientLocalServiceHasServiceClientProperties" DisplayName="Client Local Service Has Service Client Properties" Namespace="Linx.EntityAdapterDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="f7bbaf36-c2fe-4fe4-ad18-f91af67d92aa" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceHasServiceClientProperties.ClientLocalService" Name="ClientLocalService" DisplayName="Client Local Service" PropertyName="ServiceClientProperties" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Service Client Properties">
          <RolePlayer>
            <DomainClassMoniker Name="ClientLocalService" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="16b9b45f-81b3-4fd8-9e31-2120122e75cc" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceHasServiceClientProperties.ServiceClientProperty" Name="ServiceClientProperty" DisplayName="Service Client Property" PropertyName="ClientLocalService" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Client Local Service">
          <RolePlayer>
            <DomainClassMoniker Name="ServiceClientProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <DomainEnumeration Name="DisplayControlType" Namespace="Linx.EntityAdapterDesigner" Description="Control for displaying the data field.">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.TextBox" Name="TextBox" Value="1" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.NumericTextBox" Name="NumericTextBox" Value="2" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.DateTimeTextBox" Name="DateTimeTextBox" Value="3" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.LookUpTextBox" Name="LookUpTextBox" Value="4" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.CheckBox" Name="CheckBox" Value="5" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.ComboBox" Name="ComboBox" Value="6" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.MultimediaControl" Name="MultimediaControl" Value="7" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.EditBox" Name="EditBox" Value="8" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DisplayControlType.KpiBox" Name="KpiBox" Value="9" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="OperationAccess" Namespace="Linx.EntityAdapterDesigner" Description="Operation access.">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OperationAccess.AssemblyOrFamily" Name="AssemblyOrFamily" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OperationAccess.Default" Name="Default" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OperationAccess.Private" Name="Private" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OperationAccess.Project" Name="Project" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OperationAccess.ProjectOrProtected" Name="ProjectOrProtected" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OperationAccess.Protected" Name="Protected" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OperationAccess.Public" Name="Public" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OperationAccess.WithEvents" Name="WithEvents" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="DomainAttributeType" Namespace="Linx.EntityAdapterDesigner" Description="Attributes for a DomainService operation.">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainAttributeType.IgnoreOperation" Name="IgnoreOperation" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainAttributeType.Invoke" Name="Invoke" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainAttributeType.Query" Name="Query" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="UIAggregationFunctions" Namespace="Linx.EntityAdapterDesigner" Description="Aggregation Functions For UIs">
      <Literals>
        <EnumerationLiteral Description="Avg" Name="Avg" Value="5" />
        <EnumerationLiteral Description="Count" Name="Count" Value="4" />
        <EnumerationLiteral Description="Max" Name="Max" Value="3" />
        <EnumerationLiteral Description="Min" Name="Min" Value="2" />
        <EnumerationLiteral Description="None" Name="None" Value="0" />
        <EnumerationLiteral Description="Sum" Name="Sum" Value="1" />
        <EnumerationLiteral Description="CountDistinct" Name="CountDistinct" Value="6" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="DomainStructuralType" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.DomainStructuralType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainStructuralType.DataGrid" Name="DataGrid" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainStructuralType.DataFields" Name="DataFields" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainStructuralType.LeftDataGrid_RightDataFields" Name="LeftDataGrid_RightDataFields" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainStructuralType.TopDataGrid_BottomDataFields" Name="TopDataGrid_BottomDataFields" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainStructuralType.BottomDataGrid_TopDataFields" Name="BottomDataGrid_TopDataFields" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainStructuralType.RightDataGrid_LeftDataFields" Name="RightDataGrid_LeftDataFields" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="DomainLoadType" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.DomainLoadType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainLoadType.WhenAvailable" Name="WhenAvailable" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainLoadType.OnDemand" Name="OnDemand" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="DomainGeneratingType" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.DomainGeneratingType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainGeneratingType.AutomaticLayout" Name="AutomaticLayout" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.DomainGeneratingType.CustomizableLayout" Name="CustomizableLayout" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="EntityQueryReturnType" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.EntityQueryReturnType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.EntityQueryReturnType.IQueryable" Name="IQueryable" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.EntityQueryReturnType.IEnumerable" Name="IEnumerable" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="SpecializedLayout" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.SpecializedLayout">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.SpecializedLayout.None" Name="None" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.SpecializedLayout.IsSpecializedLookUp" Name="IsSpecializedLookUp" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="OrderByOrientationType" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.OrderByOrientationType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OrderByOrientationType.Ascending" Name="Ascending" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.OrderByOrientationType.Descending" Name="Descending" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="KpiShowType" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.KpiShowType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.KpiShowType.Progress" Name="Progress" Value="0" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.KpiShowType.Value" Name="Value" Value="1" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.KpiShowType.Description" Name="Description" Value="2" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="AttributeOrder" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.AttributeOrder">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.AttributeOrder.Name" Name="Name" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.AttributeOrder.DisplayName" Name="DisplayName" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.AttributeOrder.EdmKey" Name="EdmKey" Value="" />
      </Literals>
    </DomainEnumeration>
    <ExternalType Name="Color" Namespace="System.Drawing" />
    <DomainEnumeration Name="EntityAdapterJoinType" Namespace="Linx.EntityAdapterDesigner" Description="Join Type Between Two Entities.">
      <Literals>
        <EnumerationLiteral Description="Inner Join" Name="InnerJoin" Value="" />
        <EnumerationLiteral Description="Left Join" Name="LeftJoin" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="HttpRouteAttribute" Namespace="Linx.EntityAdapterDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="GET" Value="" />
        <EnumerationLiteral Description="" Name="POST" Value="" />
        <EnumerationLiteral Description="" Name="PUT" Value="" />
        <EnumerationLiteral Description="" Name="DELETE" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="BusinessExtensions" Namespace="Linx.EntityAdapterDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.BusinessExtensions.None" Name="None" Value="" />
        <EnumerationLiteral Description="" Name="SKU" Value="" />
      </Literals>
    </DomainEnumeration>
    <ExternalType Name="DashStyle" Namespace="System.Drawing.Drawing2D" />
    <DomainEnumeration Name="InterfaceType" Namespace="Linx.EntityAdapterDesigner" Description="Description for Linx.EntityAdapterDesigner.InterfaceType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.InterfaceType.Web" Name="Web" Value="" />
        <EnumerationLiteral Description="Description for Linx.EntityAdapterDesigner.InterfaceType.Mobile" Name="Mobile" Value="" />
      </Literals>
    </DomainEnumeration>
  </Types>
  <Shapes>
    <ImageShape Id="b2d85634-1e55-4638-a90f-5175aa1352e2" Description="Entity Data Model." Name="EntityDataModelShape" DisplayName="Entity Data Model Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Entity Framework Element" TextColor="Silver" FillColor="Black" InitialWidth="1" InitialHeight="1" FillGradientMode="None" Image="Resources\Edm.png">
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="ContextType" DisplayName="Context Type" DefaultText="ContextType" FontStyle="Bold" FontSize="10" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="Alert" DisplayName="Alert" DefaultIcon="Resources\Alert.png" />
      </ShapeHasDecorators>
    </ImageShape>
    <CompartmentShape Id="16a903e6-fbe4-45b8-8dc8-613b0631ef15" Description="Entity Adapter." Name="EntityAdapterShape" DisplayName="Entity Adapter Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Entity Adapter Shape" TextColor="White" ExposesTextColor="true" FillColor="211, 220, 239" OutlineColor="Transparent" InitialWidth="2" InitialHeight="0.6" FillGradientMode="None" ExposesOutlineColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="58f817f4-769f-44bb-9fc5-e7a517e57c6f" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3b88b155-e5a7-49a6-83b2-72c57be49761" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="83d688e4-8ba3-4b65-8d77-78448de4e56e" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="EnableForPublicationIndicator" DisplayName="Enable For Publication Indicator" DefaultIcon="Resources\Publication.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0.2">
        <TextDecorator Name="EntityInfo" DisplayName="Entity Info" DefaultText="EntityInfo" FontStyle="Bold, Italic" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="AggregationMark" DisplayName="Aggregation Mark" DefaultIcon="Resources\AggregationMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0.2">
        <TextDecorator Name="IsPOCO" DisplayName="POCO" DefaultText="POCO" FontStyle="Bold, Underline" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0.2">
        <TextDecorator Name="CustomBaseType" DisplayName="Custom Base Type" DefaultText="CustomBaseType" FontStyle="Bold, Italic" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopLeft" HorizontalOffset="0.3" VerticalOffset="0">
        <IconDecorator Name="IsDashboardFilter" DisplayName="Is Dashboard Filter" DefaultIcon="Resources\IsDashboardFilter.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.15">
        <IconDecorator Name="AutoPk" DisplayName="Auto Pk" DefaultIcon="Resources\AutoPk.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="-0.1" VerticalOffset="0.15">
        <IconDecorator Name="MetaDataFilter" DisplayName="Meta Data Filter" DefaultIcon="Resources\MetaDataFilter.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="-0.2" VerticalOffset="0">
        <IconDecorator Name="ModelViewMark" DisplayName="Model View Mark" DefaultIcon="Resources\ModelViewMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="-0.2" VerticalOffset="0.15">
        <IconDecorator Name="BigDataTrack" DisplayName="Big Data Track" DefaultIcon="Resources\BigDataTrack.png" />
      </ShapeHasDecorators>
      <Compartment FillColor="WhiteSmoke" Name="PropertiesCompartiment" TitleFontStyle="Bold" Title="Properties" />
      <Compartment FillColor="WhiteSmoke" Name="FormulasCompartiment" TitleFontStyle="Bold" Title="Formulas" />
      <Compartment FillColor="WhiteSmoke" Name="OperationsCompartiment" TitleFontStyle="Bold" Title="Operations" />
      <Compartment FillColor="WhiteSmoke" Name="EventsCompartiment" TitleFontStyle="Bold" Title="Server Events" />
      <Compartment FillColor="WhiteSmoke" Name="ClientEventsCompartiment" TitleFontStyle="Bold" Title="Client Events/Actions" />
      <Compartment FillColor="WhiteSmoke" Name="PublicationPropertiesCompartiment" TitleFontStyle="Bold" Title="Suggested Properties" />
      <Compartment FillColor="WhiteSmoke" Name="ExtendedFilters" TitleFontStyle="Bold" Title="Extended Filters" />
    </CompartmentShape>
    <GeometryShape Id="d8c37e15-5201-4067-970c-49172195df27" Description="Comment." Name="CommentShape" DisplayName="Comment Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Comment Shape" FillColor="255, 255, 204" OutlineColor="204, 204, 102" InitialHeight="0.3" OutlineThickness="0.01" FillGradientMode="None" Geometry="RoundedRectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Comment" DisplayName="Comment" DefaultText="Comment" />
      </ShapeHasDecorators>
    </GeometryShape>
    <CompartmentShape Id="f42e8fac-335c-4a07-934b-81aee1e052ae" Description="Domain Service Extension." Name="DomainServiceExtensionShape" DisplayName="Domain Service Extension Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Domain Service Extension Shape" FillColor="LightSteelBlue" InitialWidth="2.5" InitialHeight="0.5" FillGradientMode="None" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="DSMark" DisplayName="DSMark" DefaultIcon="Resources\DSInfo.png" />
      </ShapeHasDecorators>
      <Compartment FillColor="Transparent" Name="DomainServiceOperationsCompartiment" Title="Service Contract Operations" />
    </CompartmentShape>
    <CompartmentShape Id="c4e7a361-27e2-4382-974e-554fce7e43f5" Description="Look Up." Name="LookUpAdapterShape" DisplayName="Look Up Adapter Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Look Up Adapter Shape" TextColor="White" ExposesTextColor="true" FillColor="MenuHighlight" OutlineColor="Transparent" InitialWidth="2" InitialHeight="0.4" FillGradientMode="None" ExposesOutlineColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="c8556f5d-5bda-4041-9a8a-a796d28b9809" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b0f64e42-dd05-4418-8e60-a82339726454" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="94bdac96-0702-4af3-a0fd-55be294e7c05" Description="Description for Linx.EntityAdapterDesigner.LookUpAdapterShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterMiddleLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="CustomDecorator" DisplayName="Custom Decorator" DefaultIcon="Resources\UICustom.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="MultiSelection" DisplayName="Multi Selection" DefaultIcon="resources\MUltiselection.bmp" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0.2">
        <TextDecorator Name="LookUpInfo" DisplayName="Look Up Info" DefaultText="LookUpInfo" FontStyle="Bold, Italic" />
      </ShapeHasDecorators>
      <Compartment FillColor="WhiteSmoke" Name="LookUpPropertiesDomainServiceOperationsCompartiment" Title="Properties" />
    </CompartmentShape>
    <CompartmentShape Id="0802e969-6f71-40f9-a99f-4121e97b6bfc" Description="Domain View." Name="DomainViewShape" DisplayName="Domain View Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Domain View Shape" FillColor="Orange" OutlineColor="Transparent" InitialWidth="2" InitialHeight="0.3" FillGradientMode="Vertical" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="HasCustomValues" DisplayName="Has Custom Values" DefaultIcon="Resources\UICustom.png" />
      </ShapeHasDecorators>
      <Compartment FillColor="WhiteSmoke" TitleFillColor="Transparent" Name="DomainValueCompartment" Title="Values" />
    </CompartmentShape>
    <ImageShape Id="19b87689-f6c8-4612-aecf-d92bd4cd8331" Description="Subscription." Name="SubscriptionShape" DisplayName="Subscription Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Subscription Shape" InitialWidth="1" InitialHeight="1" FillGradientMode="None" Image="Resources\Subscription.png">
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TitleDecorator" DisplayName="Title Decorator" DefaultText="TitleDecorator" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="Alert" DisplayName="Alert" DefaultIcon="Resources\Alert.png" />
      </ShapeHasDecorators>
    </ImageShape>
    <CompartmentShape Id="24373cd7-bf45-487e-8fd8-077906f41a1d" Description="Key Performance Indicator." Name="KeyPerformanceIndicatorShape" DisplayName="Key Performance Indicator Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Key Performance Indicator Shape" FillColor="Lavender" OutlineColor="Transparent" InitialWidth="2" InitialHeight="0.3" FillGradientMode="Vertical" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="ImageInfo" DisplayName="Image Info" DefaultIcon="Resources\Kpi.png" />
      </ShapeHasDecorators>
      <Compartment FillColor="WhiteSmoke" TitleFillColor="Transparent" Name="KpiRangeItemdecorator" Title="Ranges" />
    </CompartmentShape>
    <ImageShape Id="f3751360-5d52-4580-b995-2b43d37fcc7a" Description="Workflow." Name="WorkflowShape" DisplayName="Workflow Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Workflow Shape" InitialWidth="1" InitialHeight="1" Image="Resources\FlowChart.png">
      <ShapeHasDecorators Position="OuterBottomCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="DisplayDecorator" DisplayName="Display Decorator" DefaultText="DisplayDecorator" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IsOperationRelated" DisplayName="Is Operation Related" DefaultIcon="Resources\OperationConnected.png" />
      </ShapeHasDecorators>
    </ImageShape>
    <ImageShape Id="adecb368-72dd-45a1-8e59-1522b15fc152" Description="Entity Adapter Representation" Name="EntityAdapterRepresentationShape" DisplayName="Entity Adapter Representation Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Entity Adapter Representation Shape" TextColor="SteelBlue" InitialWidth="1" InitialHeight="1" Image="Resources\EntityAdapterRepresentation.png">
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TargetEntityAdapterName" DisplayName="Target Entity Adapter Name" DefaultText="TargetEntityAdapterName" FontStyle="Underline" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterBottomCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
    </ImageShape>
    <CompartmentShape Id="aeff3247-a9ad-46ee-9731-532f7a5f5bf6" Description="Description for Linx.EntityAdapterDesigner.WebApiControllerShape" Name="WebApiControllerShape" DisplayName="Web Api Controller Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Web Api Controller Shape" TextColor="White" FillColor="Goldenrod" InitialWidth="3" InitialHeight="0.8" Geometry="RoundedRectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="WebApiMark" DisplayName="Web Api Mark" DefaultIcon="Resources\WebApi.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="DataServiceMark" DisplayName="Data Service Mark" DefaultIcon="resources\DataInfo.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="WebApiSyncMark" DisplayName="Web Api Sync Mark" DefaultIcon="resources\WebApiSync.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="AspNetCore" DisplayName="Asp Net Core" DefaultIcon="Resources\AspNetCore.png" />
      </ShapeHasDecorators>
      <Compartment FillColor="Transparent" TitleFillColor="Moccasin" Name="WebApiActionCompartment" Title="Actions" />
    </CompartmentShape>
    <CompartmentShape Id="6d5147ff-b895-4822-b723-ba32916206a5" Description="Description for Linx.EntityAdapterDesigner.RepositoryInterfaceShape" Name="RepositoryInterfaceShape" DisplayName="Repository Interface Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Repository Interface Shape" FillColor="Gainsboro" InitialWidth="3" InitialHeight="0.8" FillGradientMode="None" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="RepositoryMark" DisplayName="Repository Mark" DefaultIcon="Resources\RepositoryInterface.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="ExtensionMark" DisplayName="Extension Mark" DefaultIcon="Resources\ExtensionInterface.png" />
      </ShapeHasDecorators>
      <Compartment TitleFillColor="CornflowerBlue" Name="RepositoryMethodCompartment" Title="Methods" />
    </CompartmentShape>
    <GeometryShape Id="55f04bee-0929-4251-9a84-4618c7ace7d3" Description="Description for Linx.EntityAdapterDesigner.RepositoryImplementationShape" Name="RepositoryImplementationShape" DisplayName="Repository Implementation Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Repository Implementation Shape" FillColor="Beige" InitialWidth="3" InitialHeight="0.8" Geometry="RoundedRectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="RepositoryMark" DisplayName="Repository Mark" DefaultIcon="Resources\RepositoryImplementation.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerBottomRight" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IsDefault" DisplayName="Is Default" DefaultIcon="Resources\Default.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerBottomLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IsSelected" DisplayName="Is Selected" DefaultIcon="Resources\IsSelected.png" />
      </ShapeHasDecorators>
    </GeometryShape>
    <CompartmentShape Id="21e4a8d8-5e80-401a-952b-bd3daf10f337" Description="Store Script." Name="StoreScriptShape" DisplayName="Store Script Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Store Script Shape" FillColor="DarkGray" InitialWidth="2" InitialHeight="0.5" FillGradientMode="None" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="ScriptMark" DisplayName="Script Mark" DefaultIcon="Resources\StoreScriptInfo.png" />
      </ShapeHasDecorators>
      <Compartment FillColor="Transparent" Name="StoreQueriesCompartiment" Title="Store Queries" />
    </CompartmentShape>
    <ImageShape Id="6b802d89-919a-44cb-8c7c-cfffbaba9805" Description="" Name="OlapCatalogShape" DisplayName="Olap Catalog Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Olap Catalog Shape" InitialWidth="1" InitialHeight="1" Image="Resources\OlapCatalog.png">
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Catalog" DisplayName="Catalog" DefaultText="Catalog" FontStyle="Bold" FontSize="12" />
      </ShapeHasDecorators>
    </ImageShape>
    <CompartmentShape Id="c58328cf-460d-4f98-923a-3e6db8f290b3" Description="User Interface." Name="EntityAdapterUserInterfaceShape" DisplayName="Entity Adapter User Interface Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Entity Adapter User Interface Shape" ExposesTextColor="true" FillColor="Khaki" OutlineColor="Transparent" InitialWidth="2" InitialHeight="0.8" FillGradientMode="None" ExposesOutlineColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="0b2ac67f-e61d-4f6b-9b20-b70252922c73" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ed7e7fe9-44f5-4c08-803e-79f00a600bf7" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b15478f7-03ff-4c99-913a-56e19405e617" Description="Description for Linx.EntityAdapterDesigner.EntityAdapterUserInterfaceShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.35" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="UICustom" DisplayName="UICustom" DefaultIcon="Resources\UICustom.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterMiddleRight" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IsSpecializedLookUp" DisplayName="Is Specialized Look Up" DefaultIcon="Resources\SpecializedLookUp.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0.4">
        <IconDecorator Name="IsDefault" DisplayName="Is Default" DefaultIcon="Resources\Default.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.35" VerticalOffset="0.3">
        <TextDecorator Name="UIInfo" DisplayName="UIInfo" DefaultText="UIInfo" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.35" VerticalOffset="0.15">
        <TextDecorator Name="EntityName" DisplayName="Entity Name" DefaultText="EntityName" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="LayoutInfo" DisplayName="Layout Info" DefaultIcon="Resources\layouts.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="LayoutInfoMobile" DisplayName="Layout Info Mobile" DefaultIcon="Resources\layoutsMobile.png" />
      </ShapeHasDecorators>
      <Compartment FillColor="WhiteSmoke" Name="ClientEventsCompartiment" TitleFontStyle="Bold" Title="Events/Actions" />
    </CompartmentShape>
    <CompartmentShape Id="d14191b8-b0ea-42c1-9cac-32d92e1358fb" Description="Client Local Service." Name="ClientLocalServiceShape" DisplayName="Client Local Service Shape" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Client Local Service Shape" ExposesTextColor="true" FillColor="AliceBlue" OutlineColor="Transparent" InitialWidth="2" InitialHeight="0.8" FillGradientMode="None" ExposesOutlineColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" Geometry="RoundedRectangle">
      <Properties>
        <DomainProperty Id="07ebb6fd-d85e-4bdd-8634-e2ab6ab22f76" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0d7aad69-f5e4-4dfc-8a5e-efa575e1f1d9" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2072eca8-d4e1-4c2f-bcfa-b921a76f332e" Description="Description for Linx.EntityAdapterDesigner.ClientLocalServiceShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.35" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="Icon" DisplayName="Icon" DefaultIcon="Resources\ClientService.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <Compartment FillColor="WhiteSmoke" Name="ServiceClientEventsCompartiment" TitleFontStyle="Bold" Title="Events/Actions" />
      <Compartment FillColor="WhiteSmoke" Name="ServiceClientPropertiesCompartiment" TitleFontStyle="Bold" Title="Properties" />
    </CompartmentShape>
  </Shapes>
  <Connectors>
    <Connector Id="f5d7559e-ac5e-40b0-b755-a6bd670fd4c9" Description="Connector between EntityAdapter and EDM." Name="AssociationEdmConnector" DisplayName="Association Edm Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association Edm Connector" DashStyle="Dot" SourceEndStyle="EmptyDiamond" TargetEndStyle="EmptyDiamond" />
    <Connector Id="21d6c00f-ec30-43ad-86b6-0eb47f728ee8" Description="Connector between EntityAdapters." Name="AssociationEntityConnector" DisplayName="Association Entity Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association Entity Connector" Color="HotTrack" SourceEndStyle="FilledDiamond">
      <ConnectorHasDecorators Position="SourceTop" OffsetFromShape="0" OffsetFromLine="0">
        <IconDecorator Name="IsDashboard" DisplayName="Is Dashboard" DefaultIcon="Resources\IsDashboardRelation.png" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="12ea59b3-91a9-44e3-82b3-cdb4e61dc99b" Description="Connector for Comments." Name="CommentEntityConnector" DisplayName="Comment Entity Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Comment Entity Connector" DashStyle="Dot" Thickness="0.01" RoutingStyle="Straight" />
    <Connector Id="3ef34270-3cd7-4974-b127-4850410def98" Description="Connector between LookUp and EntityAdapter." Name="AssociationLookUpConnector" DisplayName="Association Look Up Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association Look Up Connector" />
    <Connector Id="c62a55c0-dec3-4fed-a2af-8188482d4896" Description="Connector between UserInterface and EntityAdapter." Name="AssociationUserInterfaceConnector" DisplayName="Association User Interface Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association User Interface Connector" Thickness="0.035" RoutingStyle="Straight" />
    <Connector Id="b0e5b779-7553-4330-b3a3-395b5774d8f2" Description="Connector for Inheritance of EntityAdapters." Name="InheritanceEntityConnector" DisplayName="Inheritance Entity Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Inheritance Entity Connector" DashStyle="Dash" TargetEndStyle="HollowArrow" />
    <Connector Id="01d213d1-231b-4ea2-a2d0-f8a9b292e22e" Description="Connector for Inheritance of LookUps." Name="InheritanceLookUpConnector" DisplayName="Inheritance Look Up Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Inheritance Look Up Connector" DashStyle="Dash" TargetEndStyle="HollowArrow" />
    <Connector Id="905226ce-9792-441f-beae-85842ed2a59e" Description="Connector for Local EntityAdapters." Name="LocalEntityConnector" DisplayName="Local Entity Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Local Entity Connector" DashStyle="Dot" TargetEndStyle="HollowArrow" />
    <Connector Id="11724ecc-29c9-4ec3-b1bb-4d70597b9ee8" Description="Inheritance for UserInterfaces." Name="InheritanceUserInterface" DisplayName="Inheritance User Interface" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Inheritance User Interface" DashStyle="Dash" TargetEndStyle="HollowArrow" />
    <Connector Id="9483d7ca-540b-4d3a-b634-641f41e73aa9" Description="Association Entity Representation" Name="AssociationEntityRepresentationConnector" DisplayName="Association Entity Representation Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association Entity Representation Connector" Color="SkyBlue" TargetEndStyle="HollowArrow" Thickness="0.05">
      <ConnectorHasDecorators Position="SourceTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="InnerType" DisplayName="Inner Type" DefaultText="InnerType" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="8f2f7136-d0c3-47c4-bbf0-6788a705fa2f" Description="Entity To Entity Representation" Name="EntityToEntityRepresentationConnector" DisplayName="Entity To Entity Representation Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Entity To Entity Representation Connector" TargetEndStyle="EmptyArrow" />
    <Connector Id="2d2e312b-bc62-4532-9034-6382639df182" Description="Description for Linx.EntityAdapterDesigner.RepositoryConnector" Name="RepositoryConnector" DisplayName="Repository Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Repository Connector" />
    <Connector Id="249224af-1529-4738-b4af-cc27c8a2b790" Description="Description for Linx.EntityAdapterDesigner.RepositoryImplementationConnector" Name="RepositoryImplementationConnector" DisplayName="Repository Implementation Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Repository Implementation Connector" TargetEndStyle="EmptyArrow" />
    <Connector Id="60434ccc-c667-4fd0-9f12-9bf7e4faca99" Description="Connector between EntityAdapters." Name="CollectionConnector" DisplayName="Collection Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Collection Connector" SourceEndStyle="FilledDiamond" targetEndWidth="0.2">
      <ConnectorHasDecorators Position="TargetTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="e0fd115e-7b39-47ad-a07f-515b55c05d6c" Description="Connector between EntityAdapters." Name="InstanceConnector" DisplayName="Instance Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Instance Connector" targetEndWidth="0.2">
      <ConnectorHasDecorators Position="TargetTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="5c98e6bb-7313-4313-99b7-329a85124a44" Description="Connector between UserInterface and EntityAdapter." Name="AssociationUserInterfaceToSubscription" DisplayName="Association User Interface To Subscription" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association User Interface To Subscription" Thickness="0.035" RoutingStyle="Straight" />
    <Connector Id="d8f8652a-1bae-4c72-b22c-8e8dd6236bcc" Description="Description for Linx.EntityAdapterDesigner.LookupEdmConnector" Name="LookupEdmConnector" DisplayName="Lookup Edm Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Lookup Edm Connector" DashStyle="Dot" SourceEndStyle="EmptyDiamond" TargetEndStyle="EmptyDiamond" />
    <Connector Id="df41b6bc-94d8-44c6-b4f9-2603d01b9496" Description="Connector between EntityAdapter and EDM." Name="AssociationOlapConnector" DisplayName="Association Olap Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association Olap Connector" DashStyle="DashDotDot" SourceEndStyle="EmptyDiamond" TargetEndStyle="EmptyDiamond" />
    <Connector Id="9339408e-8ec2-418a-94cc-5cce999fede9" Description="" Name="AssociationUIToClientLocalServiceConnector" DisplayName="Association UITo Client Local Service Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association UITo Client Local Service Connector" DashStyle="DashDotDot" SourceEndStyle="FilledDiamond" TargetEndStyle="FilledDiamond" />
    <Connector Id="c275aa0c-f2b8-496b-b82f-38d3635bd1a7" Description="" Name="AssociationClientLocalServiceToExternalConnector" DisplayName="Association Client Local Service To External Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association Client Local Service To External Connector" DashStyle="DashDotDot" SourceEndStyle="FilledDiamond" TargetEndStyle="FilledDiamond" />
    <Connector Id="4328d7b3-9ffc-4480-98e3-91022b9595cf" Description="" Name="AssociationClientLocalServiceToEntityConnector" DisplayName="Association Client Local Service To Entity Connector" Namespace="Linx.EntityAdapterDesigner" FixedTooltipText="Association Client Local Service To Entity Connector" DashStyle="DashDotDot" SourceEndStyle="FilledDiamond" TargetEndStyle="FilledDiamond" />
  </Connectors>
  <XmlSerializationBehavior Name="EntityAdapterDesignerSerializationBehavior" Namespace="Linx.EntityAdapterDesigner">
    <ClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRoot" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootMoniker" ElementName="entityAdapterDesignerRoot" MonikerTypeName="EntityAdapterDesignerRootMoniker">
        <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
        <ElementData>
          <XmlRelationshipData RoleElementName="entityDataModels">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasEntityDataModels" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="entityAdapters">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasEntityAdapters" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="targetNamespace">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/TargetNamespace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="title">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/Title" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="comments">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasComments" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="domainServiceExtensions">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasDomainServiceExtensions" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="lookUpAdapters">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasLookUpAdapters" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="documentName">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/DocumentName" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapterUserInterfaces">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasEntityAdapterUserInterfaces" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="domainViews">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasDomainViews" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="subscriptions">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasSubscriptions" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="enableAutomaticAuthorization">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/EnableAutomaticAuthorization" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="keyPerformanceIndicators">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasKeyPerformanceIndicators" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="workflows">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasWorkflows" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapterRepresentations">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasEntityAdapterRepresentations" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="webApiControllers">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasWebApiControllers" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="repositoryInterfaces">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasRepositoryInterfaces" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="enableDocumentation">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/EnableDocumentation" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="repositoryImplementations">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasRepositoryImplementations" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="documentPath">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/DocumentPath" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="version">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/Version" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="firstSaveRepresentations">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/FirstSaveRepresentations" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="storeScripts">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasStoreScripts" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="olapCatalogs">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasOlapCatalogs" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="refreshIdentityKeysAfterSave">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/RefreshIdentityKeysAfterSave" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="clientLocalServices">
            <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasClientLocalServices" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="isAspNetCore">
            <DomainPropertyMoniker Name="EntityAdapterDesignerRoot/IsAspNetCore" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerDiagram" MonikerAttributeName="" MonikerElementName="minimalLanguageDiagramMoniker" ElementName="minimalLanguageDiagram" MonikerTypeName="EntityAdapterDesignerDiagramMoniker">
        <DiagramMoniker Name="EntityAdapterDesignerDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapter" MonikerAttributeName="name" MonikerElementName="entityAdapterMoniker" ElementName="entityAdapter" MonikerTypeName="EntityAdapterMoniker">
        <DomainClassMoniker Name="EntityAdapter" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="EntityAdapter/Name" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="entityAdapterProperties">
            <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterProperties" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="entityAdapterOperations">
            <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterOperations" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="targetEntityAdapter">
            <DomainRelationshipMoniker Name="EntityAdapterReferencesTargetEntityAdapter" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="entityDataModel">
            <DomainRelationshipMoniker Name="EntityAdapterReferencesEntityDataModel" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="secondaryEntities">
            <DomainPropertyMoniker Name="EntityAdapter/SecondaryEntities" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="EntityAdapter/Description" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="entityAdapterFormulas">
            <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterFormulas" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="entityAdapterEvents">
            <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterEvents" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="entityRelations">
            <DomainPropertyMoniker Name="EntityAdapter/EntityRelations" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="detailRelations">
            <DomainPropertyMoniker Name="EntityAdapter/DetailRelations" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isReadOnly">
            <DomainPropertyMoniker Name="EntityAdapter/IsReadOnly" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="referenceRelations">
            <DomainPropertyMoniker Name="EntityAdapter/ReferenceRelations" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="EntityAdapter/DisplayName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="primaryEntity">
            <DomainPropertyMoniker Name="EntityAdapter/PrimaryEntity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="entitySets">
            <DomainPropertyMoniker Name="EntityAdapter/EntitySets" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customAttributes">
            <DomainPropertyMoniker Name="EntityAdapter/CustomAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customValidationMethod">
            <DomainPropertyMoniker Name="EntityAdapter/CustomValidationMethod" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="lookUpAdapters">
            <DomainRelationshipMoniker Name="EntityAdapterReferencesLookUpAdapters" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="isAggregationView">
            <DomainPropertyMoniker Name="EntityAdapter/IsAggregationView" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sizeGridConfigurations">
            <DomainPropertyMoniker Name="EntityAdapter/SizeGridConfigurations" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableDetailsSerialization">
            <DomainPropertyMoniker Name="EntityAdapter/EnableDetailsSerialization" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="queryReturnType">
            <DomainPropertyMoniker Name="EntityAdapter/QueryReturnType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableForPublication">
            <DomainPropertyMoniker Name="EntityAdapter/EnableForPublication" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapterPublicationProperties">
            <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterPublicationProperties" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="copyConfigurationFromDefaultUI">
            <DomainPropertyMoniker Name="EntityAdapter/CopyConfigurationFromDefaultUI" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="parentCompositionEnabled">
            <DomainPropertyMoniker Name="EntityAdapter/ParentCompositionEnabled" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapterExtendedFilters">
            <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterExtendedFilters" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="propertyOrder">
            <DomainPropertyMoniker Name="EntityAdapter/PropertyOrder" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="requeryDetailsAfterSave">
            <DomainPropertyMoniker Name="EntityAdapter/RequeryDetailsAfterSave" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="createCRUD">
            <DomainPropertyMoniker Name="EntityAdapter/CreateCRUD" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="createDynamicPrimaryKey">
            <DomainPropertyMoniker Name="EntityAdapter/CreateDynamicPrimaryKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableAutomaticLookUps">
            <DomainPropertyMoniker Name="EntityAdapter/EnableAutomaticLookUps" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="baseEntityAdapter">
            <DomainRelationshipMoniker Name="EntityAdapterReferencesBaseEntityAdapter" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="entityClassInfo" Representation="Ignore">
            <DomainPropertyMoniker Name="EntityAdapter/EntityClassInfo" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="localEntityAdapter">
            <DomainRelationshipMoniker Name="EntityAdapterReferencesLocalEntityAdapter" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="reverseInsertOrder">
            <DomainPropertyMoniker Name="EntityAdapter/ReverseInsertOrder" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="edmTreeMaximumLevel">
            <DomainPropertyMoniker Name="EntityAdapter/EdmTreeMaximumLevel" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="surrogateProperty">
            <DomainPropertyMoniker Name="EntityAdapter/SurrogateProperty" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="showDetailsLoadProcess">
            <DomainPropertyMoniker Name="EntityAdapter/ShowDetailsLoadProcess" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="removeMeasureIfNotUsed">
            <DomainPropertyMoniker Name="EntityAdapter/RemoveMeasureIfNotUsed" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableMetaDataFilter">
            <DomainPropertyMoniker Name="EntityAdapter/EnableMetaDataFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="replicationKey">
            <DomainPropertyMoniker Name="EntityAdapter/ReplicationKey" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapterRepresentation">
            <DomainRelationshipMoniker Name="EntityAdapterReferencesEntityAdapterRepresentation" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="isUpdatableWhenPublished">
            <DomainPropertyMoniker Name="EntityAdapter/IsUpdatableWhenPublished" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="checkExistenceOnInserting">
            <DomainPropertyMoniker Name="EntityAdapter/CheckExistenceOnInserting" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="detailsCollectionType">
            <DomainPropertyMoniker Name="EntityAdapter/DetailsCollectionType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataContractName">
            <DomainPropertyMoniker Name="EntityAdapter/DataContractName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPOCO">
            <DomainPropertyMoniker Name="EntityAdapter/IsPOCO" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="pOCOInfo">
            <DomainPropertyMoniker Name="EntityAdapter/POCOInfo" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="ownerCollectionEntities">
            <DomainRelationshipMoniker Name="EntityCollectionReferencesEntityOwners" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="ownerInstanceEntities">
            <DomainRelationshipMoniker Name="EntityInstanceReferencesEntityOwners" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="customBaseType">
            <DomainPropertyMoniker Name="EntityAdapter/CustomBaseType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isCollectionDataContract">
            <DomainPropertyMoniker Name="EntityAdapter/IsCollectionDataContract" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataContractNamespace">
            <DomainPropertyMoniker Name="EntityAdapter/DataContractNamespace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="generateDataMemberOrder">
            <DomainPropertyMoniker Name="EntityAdapter/GenerateDataMemberOrder" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isSingleBufferUpdate">
            <DomainPropertyMoniker Name="EntityAdapter/IsSingleBufferUpdate" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataMemberEmitDefaultValue">
            <DomainPropertyMoniker Name="EntityAdapter/DataMemberEmitDefaultValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableQBE">
            <DomainPropertyMoniker Name="EntityAdapter/EnableQBE" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="primaryEntityBase">
            <DomainPropertyMoniker Name="EntityAdapter/PrimaryEntityBase" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sourceDerivedClasses">
            <DomainPropertyMoniker Name="EntityAdapter/SourceDerivedClasses" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="businessExtension">
            <DomainPropertyMoniker Name="EntityAdapter/BusinessExtension" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="noBufferChanges">
            <DomainPropertyMoniker Name="EntityAdapter/NoBufferChanges" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="forceAggregationPaging">
            <DomainPropertyMoniker Name="EntityAdapter/ForceAggregationPaging" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="olapCatalog">
            <DomainRelationshipMoniker Name="EntityAdapterReferencesOlapCatalog" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="cubeName">
            <DomainPropertyMoniker Name="EntityAdapter/CubeName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filter">
            <DomainPropertyMoniker Name="EntityAdapter/Filter" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapterClientEvented">
            <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterClientEvented" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="forceBrandFilter">
            <DomainPropertyMoniker Name="EntityAdapter/ForceBrandFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sendAllRowsOnSubmitting">
            <DomainPropertyMoniker Name="EntityAdapter/SendAllRowsOnSubmitting" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="distinct">
            <DomainPropertyMoniker Name="EntityAdapter/Distinct" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="exposeAsService">
            <DomainPropertyMoniker Name="EntityAdapter/ExposeAsService" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="loadDataOnlyIfVisible">
            <DomainPropertyMoniker Name="EntityAdapter/LoadDataOnlyIfVisible" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDashboardFilter">
            <DomainPropertyMoniker Name="EntityAdapter/IsDashboardFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="requeryAfterSave">
            <DomainPropertyMoniker Name="EntityAdapter/RequeryAfterSave" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableQueryByParent">
            <DomainPropertyMoniker Name="EntityAdapter/EnableQueryByParent" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableClientLookupOnQueryMode">
            <DomainPropertyMoniker Name="EntityAdapter/EnableClientLookupOnQueryMode" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableLookupOptimizationForQBE">
            <DomainPropertyMoniker Name="EntityAdapter/EnableLookupOptimizationForQBE" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewDefinition">
            <DomainPropertyMoniker Name="EntityAdapter/ModelViewDefinition" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isModelView">
            <DomainPropertyMoniker Name="EntityAdapter/IsModelView" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewDbSets">
            <DomainPropertyMoniker Name="EntityAdapter/ModelViewDbSets" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="detailRelationsSuggestion">
            <DomainPropertyMoniker Name="EntityAdapter/DetailRelationsSuggestion" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isLargeDataMode">
            <DomainPropertyMoniker Name="EntityAdapter/IsLargeDataMode" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityDataModel" MonikerAttributeName="name" MonikerElementName="entityDataModelMoniker" ElementName="entityDataModel" MonikerTypeName="EntityDataModelMoniker">
        <DomainClassMoniker Name="EntityDataModel" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="EntityDataModel/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="path">
            <DomainPropertyMoniker Name="EntityDataModel/Path" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetNamespace">
            <DomainPropertyMoniker Name="EntityDataModel/TargetNamespace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="EntityDataModel/Description" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="connectionName">
            <DomainPropertyMoniker Name="EntityDataModel/ConnectionName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="contextType" Representation="Ignore">
            <DomainPropertyMoniker Name="EntityDataModel/ContextType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="hasError" Representation="Ignore">
            <DomainPropertyMoniker Name="EntityDataModel/HasError" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityDataModelShape" MonikerAttributeName="" MonikerElementName="entityDataModelShapeMoniker" ElementName="entityDataModelShape" MonikerTypeName="EntityDataModelShapeMoniker">
        <ImageShapeMoniker Name="EntityDataModelShape" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasEntityDataModels" MonikerAttributeName="" MonikerElementName="entityAdapterDesignerRootHasEntityDataModelsMoniker" ElementName="entityAdapterDesignerRootHasEntityDataModels" MonikerTypeName="EntityAdapterDesignerRootHasEntityDataModelsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasEntityDataModels" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterShape" MonikerAttributeName="" MonikerElementName="entityAdapterShapeMoniker" ElementName="entityAdapterShape" MonikerTypeName="EntityAdapterShapeMoniker">
        <CompartmentShapeMoniker Name="EntityAdapterShape" />
        <ElementData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="EntityAdapterShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="EntityAdapterShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="EntityAdapterShape/OutlineDashStyle" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasEntityAdapters" MonikerAttributeName="" MonikerElementName="entityAdapterDesignerRootHasEntityAdaptersMoniker" ElementName="entityAdapterDesignerRootHasEntityAdapters" MonikerTypeName="EntityAdapterDesignerRootHasEntityAdaptersMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasEntityAdapters" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterProperty" MonikerAttributeName="" MonikerElementName="entityAdapterPropertyMoniker" ElementName="entityAdapterProperty" MonikerTypeName="EntityAdapterPropertyMoniker">
        <DomainClassMoniker Name="EntityAdapterProperty" />
        <ElementData>
          <XmlPropertyData XmlName="edmKey">
            <DomainPropertyMoniker Name="EntityAdapterProperty/EdmKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="defaultValue">
            <DomainPropertyMoniker Name="EntityAdapterProperty/DefaultValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filter">
            <DomainPropertyMoniker Name="EntityAdapterProperty/Filter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetKeyName">
            <DomainPropertyMoniker Name="EntityAdapterProperty/TargetKeyName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="orderBySequence">
            <DomainPropertyMoniker Name="EntityAdapterProperty/OrderBySequence" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="orderByOrientation">
            <DomainPropertyMoniker Name="EntityAdapterProperty/OrderByOrientation" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayValue" Representation="Ignore">
            <DomainPropertyMoniker Name="EntityAdapterProperty/DisplayValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="publicationRelatedKey">
            <DomainPropertyMoniker Name="EntityAdapterProperty/PublicationRelatedKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="publicationSuffix">
            <DomainPropertyMoniker Name="EntityAdapterProperty/PublicationSuffix" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isAutomaticSequency">
            <DomainPropertyMoniker Name="EntityAdapterProperty/IsAutomaticSequency" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="denormalizedDataInfo">
            <DomainPropertyMoniker Name="EntityAdapterProperty/DenormalizedDataInfo" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isIdentity">
            <DomainPropertyMoniker Name="EntityAdapterProperty/IsIdentity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="linqMethod">
            <DomainPropertyMoniker Name="EntityAdapterProperty/LinqMethod" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isRequiredBeforeSearching">
            <DomainPropertyMoniker Name="EntityAdapterProperty/IsRequiredBeforeSearching" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewFormula">
            <DomainPropertyMoniker Name="EntityAdapterProperty/ModelViewFormula" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewSource">
            <DomainPropertyMoniker Name="EntityAdapterProperty/ModelViewSource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="quickSearchIndex">
            <DomainPropertyMoniker Name="EntityAdapterProperty/QuickSearchIndex" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterOperation" MonikerAttributeName="" MonikerElementName="entityAdapterOperationMoniker" ElementName="entityAdapterOperation" MonikerTypeName="EntityAdapterOperationMoniker">
        <DomainClassMoniker Name="EntityAdapterOperation" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterHasEntityAdapterProperties" MonikerAttributeName="" MonikerElementName="entityAdapterHasEntityAdapterPropertiesMoniker" ElementName="entityAdapterHasEntityAdapterProperties" MonikerTypeName="EntityAdapterHasEntityAdapterPropertiesMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterProperties" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterHasEntityAdapterOperations" MonikerAttributeName="" MonikerElementName="entityAdapterHasEntityAdapterOperationsMoniker" ElementName="entityAdapterHasEntityAdapterOperations" MonikerTypeName="EntityAdapterHasEntityAdapterOperationsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterOperations" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterReferencesTargetEntityAdapter" MonikerAttributeName="" MonikerElementName="entityAdapterReferencesTargetEntityAdapterMoniker" ElementName="entityAdapterReferencesTargetEntityAdapter" MonikerTypeName="EntityAdapterReferencesTargetEntityAdapterMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterReferencesTargetEntityAdapter" />
        <ElementData>
          <XmlPropertyData XmlName="parentKeyFields">
            <DomainPropertyMoniker Name="EntityAdapterReferencesTargetEntityAdapter/ParentKeyFields" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="detailKeyFields">
            <DomainPropertyMoniker Name="EntityAdapterReferencesTargetEntityAdapter/DetailKeyFields" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataMemberName">
            <DomainPropertyMoniker Name="EntityAdapterReferencesTargetEntityAdapter/DataMemberName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDashboard">
            <DomainPropertyMoniker Name="EntityAdapterReferencesTargetEntityAdapter/IsDashboard" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="removeFieldIfEmpty">
            <DomainPropertyMoniker Name="EntityAdapterReferencesTargetEntityAdapter/RemoveFieldIfEmpty" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AssociationEdmConnector" MonikerAttributeName="" MonikerElementName="associationEdmConnectorMoniker" ElementName="associationEdmConnector" MonikerTypeName="AssociationEdmConnectorMoniker">
        <ConnectorMoniker Name="AssociationEdmConnector" />
      </XmlClassData>
      <XmlClassData TypeName="AssociationEntityConnector" MonikerAttributeName="" MonikerElementName="associationEntityConnectorMoniker" ElementName="associationEntityConnector" MonikerTypeName="AssociationEntityConnectorMoniker">
        <ConnectorMoniker Name="AssociationEntityConnector" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterReferencesEntityDataModel" MonikerAttributeName="" MonikerElementName="entityAdapterReferencesEntityDataModelMoniker" ElementName="entityAdapterReferencesEntityDataModel" MonikerTypeName="EntityAdapterReferencesEntityDataModelMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterReferencesEntityDataModel" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterAttribute" MonikerAttributeName="name" MonikerElementName="entityAdapterAttributeMoniker" ElementName="entityAdapterAttribute" MonikerTypeName="EntityAdapterAttributeMoniker">
        <DomainClassMoniker Name="EntityAdapterAttribute" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayOrder">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/DisplayOrder" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isBrowsable">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsBrowsable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="connectedAttribute">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/ConnectedAttribute" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="datatype">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/Datatype" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="precision">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/Precision" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPK">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsPK" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isFK">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsFK" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isNull">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsNull" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isEditable">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsEditable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/DisplayName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayControl">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/DisplayControl" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="groupName">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/GroupName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/Description" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customAttributes">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/CustomAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isCustomized">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsCustomized" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="range">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/Range" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataFormatString">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/DataFormatString" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customValidationMethod">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/CustomValidationMethod" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="aggregationFunction">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/AggregationFunction" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="domainName">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/DomainName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isCompulsory">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsCompulsory" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPublicationSuggestion">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsPublicationSuggestion" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="removeValidations">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/RemoveValidations" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="kpiName">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/KpiName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="kpiRelatedAttribute">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/KpiRelatedAttribute" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="forceAsFilter">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/ForceAsFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataRelationKey">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/DataRelationKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isMeasure">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsMeasure" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="measureFormula">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/MeasureFormula" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="ignoreForQuery">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IgnoreForQuery" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="lookUpSubscription">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/LookUpSubscription" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="maskType">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/MaskType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="mask">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/Mask" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataMemberName">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/DataMemberName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="ignoreDataMember">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IgnoreDataMember" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customMediaTable">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/CustomMediaTable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="removeFilterFromClientLayer">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/RemoveFilterFromClientLayer" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="ignoreMetaData">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IgnoreMetaData" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="noUpdatable">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/NoUpdatable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="countDistinctFilter">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/CountDistinctFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isZeroNotAllowed">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/IsZeroNotAllowed" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="brandDecimalsControl">
            <DomainPropertyMoniker Name="EntityAdapterAttribute/BrandDecimalsControl" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterFormula" MonikerAttributeName="" MonikerElementName="entityAdapterFormulaMoniker" ElementName="entityAdapterFormula" MonikerTypeName="EntityAdapterFormulaMoniker">
        <DomainClassMoniker Name="EntityAdapterFormula" />
        <ElementData>
          <XmlPropertyData XmlName="triggerAttributes">
            <DomainPropertyMoniker Name="EntityAdapterFormula/TriggerAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="formula">
            <DomainPropertyMoniker Name="EntityAdapterFormula/Formula" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="linqDefinition">
            <DomainPropertyMoniker Name="EntityAdapterFormula/LinqDefinition" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isUpdatable">
            <DomainPropertyMoniker Name="EntityAdapterFormula/IsUpdatable" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterHasEntityAdapterFormulas" MonikerAttributeName="" MonikerElementName="entityAdapterHasEntityAdapterFormulasMoniker" ElementName="entityAdapterHasEntityAdapterFormulas" MonikerTypeName="EntityAdapterHasEntityAdapterFormulasMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterFormulas" />
      </XmlClassData>
      <XmlClassData TypeName="Comment" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentMoniker" ElementName="comment" MonikerTypeName="CommentMoniker">
        <DomainClassMoniker Name="Comment" />
        <ElementData>
          <XmlPropertyData XmlName="text">
            <DomainPropertyMoniker Name="Comment/Text" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="entityAdapters">
            <DomainRelationshipMoniker Name="CommentReferencesEntityAdapters" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="entityDataModels">
            <DomainRelationshipMoniker Name="CommentReferencesEntityDataModels" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasComments" MonikerAttributeName="" MonikerElementName="entityAdapterDesignerRootHasCommentsMoniker" ElementName="entityAdapterDesignerRootHasComments" MonikerTypeName="EntityAdapterDesignerRootHasCommentsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasComments" />
      </XmlClassData>
      <XmlClassData TypeName="CommentReferencesEntityAdapters" MonikerAttributeName="" MonikerElementName="commentReferencesEntityAdaptersMoniker" ElementName="commentReferencesEntityAdapters" MonikerTypeName="CommentReferencesEntityAdaptersMoniker">
        <DomainRelationshipMoniker Name="CommentReferencesEntityAdapters" />
      </XmlClassData>
      <XmlClassData TypeName="CommentReferencesEntityDataModels" MonikerAttributeName="" MonikerElementName="commentReferencesEntityDataModelsMoniker" ElementName="commentReferencesEntityDataModels" MonikerTypeName="CommentReferencesEntityDataModelsMoniker">
        <DomainRelationshipMoniker Name="CommentReferencesEntityDataModels" />
      </XmlClassData>
      <XmlClassData TypeName="CommentShape" MonikerAttributeName="" MonikerElementName="commentShapeMoniker" ElementName="commentShape" MonikerTypeName="CommentShapeMoniker">
        <GeometryShapeMoniker Name="CommentShape" />
      </XmlClassData>
      <XmlClassData TypeName="CommentEntityConnector" MonikerAttributeName="" MonikerElementName="commentEntityConnectorMoniker" ElementName="commentEntityConnector" MonikerTypeName="CommentEntityConnectorMoniker">
        <ConnectorMoniker Name="CommentEntityConnector" />
      </XmlClassData>
      <XmlClassData TypeName="GenericOperation" MonikerAttributeName="name" MonikerElementName="genericOperationMoniker" ElementName="genericOperation" MonikerTypeName="GenericOperationMoniker">
        <DomainClassMoniker Name="GenericOperation" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="GenericOperation/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="comment">
            <DomainPropertyMoniker Name="GenericOperation/Comment" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="returnType">
            <DomainPropertyMoniker Name="GenericOperation/ReturnType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="access">
            <DomainPropertyMoniker Name="GenericOperation/Access" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customAttributes">
            <DomainPropertyMoniker Name="GenericOperation/CustomAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="parameters">
            <DomainPropertyMoniker Name="GenericOperation/Parameters" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isStatic">
            <DomainPropertyMoniker Name="GenericOperation/IsStatic" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="canOverride">
            <DomainPropertyMoniker Name="GenericOperation/CanOverride" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="overloadName">
            <DomainPropertyMoniker Name="GenericOperation/OverloadName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="docComment">
            <DomainPropertyMoniker Name="GenericOperation/DocComment" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isUniqueOverload">
            <DomainPropertyMoniker Name="GenericOperation/IsUniqueOverload" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isShared">
            <DomainPropertyMoniker Name="GenericOperation/IsShared" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPartial">
            <DomainPropertyMoniker Name="GenericOperation/IsPartial" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isActivity">
            <DomainPropertyMoniker Name="GenericOperation/IsActivity" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="workflow">
            <DomainRelationshipMoniker Name="GenericOperationReferencesWorkflow" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterEvent" MonikerAttributeName="" MonikerElementName="entityAdapterEventMoniker" ElementName="entityAdapterEvent" MonikerTypeName="EntityAdapterEventMoniker">
        <DomainClassMoniker Name="EntityAdapterEvent" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterHasEntityAdapterEvents" MonikerAttributeName="" MonikerElementName="entityAdapterHasEntityAdapterEventsMoniker" ElementName="entityAdapterHasEntityAdapterEvents" MonikerTypeName="EntityAdapterHasEntityAdapterEventsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterEvents" />
      </XmlClassData>
      <XmlClassData TypeName="DomainServiceOperation" MonikerAttributeName="" MonikerElementName="domainServiceOperationMoniker" ElementName="domainServiceOperation" MonikerTypeName="DomainServiceOperationMoniker">
        <DomainClassMoniker Name="DomainServiceOperation" />
        <ElementData>
          <XmlPropertyData XmlName="domainAttribute">
            <DomainPropertyMoniker Name="DomainServiceOperation/DomainAttribute" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isJson">
            <DomainPropertyMoniker Name="DomainServiceOperation/IsJson" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="DomainServiceExtension" MonikerAttributeName="name" MonikerElementName="domainServiceExtensionMoniker" ElementName="domainServiceExtension" MonikerTypeName="DomainServiceExtensionMoniker">
        <DomainClassMoniker Name="DomainServiceExtension" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="DomainServiceExtension/Name" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="domainServiceOperations">
            <DomainRelationshipMoniker Name="DomainServiceExtensionHasDomainServiceOperations" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasDomainServiceExtensions" MonikerAttributeName="" MonikerElementName="entityAdapterDesignerRootHasDomainServiceExtensionsMoniker" ElementName="entityAdapterDesignerRootHasDomainServiceExtensions" MonikerTypeName="EntityAdapterDesignerRootHasDomainServiceExtensionsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasDomainServiceExtensions" />
      </XmlClassData>
      <XmlClassData TypeName="DomainServiceExtensionHasDomainServiceOperations" MonikerAttributeName="" MonikerElementName="domainServiceExtensionHasDomainServiceOperationsMoniker" ElementName="domainServiceExtensionHasDomainServiceOperations" MonikerTypeName="DomainServiceExtensionHasDomainServiceOperationsMoniker">
        <DomainRelationshipMoniker Name="DomainServiceExtensionHasDomainServiceOperations" />
      </XmlClassData>
      <XmlClassData TypeName="DomainServiceExtensionShape" MonikerAttributeName="" MonikerElementName="domainServiceExtensionShapeMoniker" ElementName="domainServiceExtensionShape" MonikerTypeName="DomainServiceExtensionShapeMoniker">
        <CompartmentShapeMoniker Name="DomainServiceExtensionShape" />
      </XmlClassData>
      <XmlClassData TypeName="LookUpAdapter" MonikerAttributeName="name" MonikerElementName="lookUpAdapterMoniker" ElementName="lookUpAdapter" MonikerTypeName="LookUpAdapterMoniker">
        <DomainClassMoniker Name="LookUpAdapter" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="LookUpAdapter/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="entitySource">
            <DomainPropertyMoniker Name="LookUpAdapter/EntitySource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="LookUpAdapter/Description" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="LookUpAdapter/DisplayName" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="lookUpProperties">
            <DomainRelationshipMoniker Name="LookUpAdapterHasLookUpProperties" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="relationName">
            <DomainPropertyMoniker Name="LookUpAdapter/RelationName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isCustomized">
            <DomainPropertyMoniker Name="LookUpAdapter/IsCustomized" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isMultiSelection">
            <DomainPropertyMoniker Name="LookUpAdapter/IsMultiSelection" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="replaceAllOnClearState">
            <DomainPropertyMoniker Name="LookUpAdapter/ReplaceAllOnClearState" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="specializedUI">
            <DomainPropertyMoniker Name="LookUpAdapter/SpecializedUI" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="disableSpecializedUI">
            <DomainPropertyMoniker Name="LookUpAdapter/DisableSpecializedUI" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="width">
            <DomainPropertyMoniker Name="LookUpAdapter/Width" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="height">
            <DomainPropertyMoniker Name="LookUpAdapter/Height" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="lookUpClassInfo" Representation="Ignore">
            <DomainPropertyMoniker Name="LookUpAdapter/LookUpClassInfo" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="baseLookUpAdapter">
            <DomainRelationshipMoniker Name="LookUpAdapterReferencesBaseLookUpAdapter" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="queryReturnType">
            <DomainPropertyMoniker Name="LookUpAdapter/QueryReturnType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="entitySourceBase">
            <DomainPropertyMoniker Name="LookUpAdapter/EntitySourceBase" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityDataModel">
            <DomainRelationshipMoniker Name="LookUpAdapterReferencesEntityDataModel" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="entityRelations">
            <DomainPropertyMoniker Name="LookUpAdapter/EntityRelations" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="clientFilterExpression">
            <DomainPropertyMoniker Name="LookUpAdapter/ClientFilterExpression" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="cacheOnClientSide">
            <DomainPropertyMoniker Name="LookUpAdapter/CacheOnClientSide" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="distinct">
            <DomainPropertyMoniker Name="LookUpAdapter/Distinct" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="applyClientFilterOnClear">
            <DomainPropertyMoniker Name="LookUpAdapter/ApplyClientFilterOnClear" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filter">
            <DomainPropertyMoniker Name="LookUpAdapter/Filter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="checkExistence">
            <DomainPropertyMoniker Name="LookUpAdapter/CheckExistence" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableSubLookups">
            <DomainPropertyMoniker Name="LookUpAdapter/EnableSubLookups" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="canAddNew">
            <DomainPropertyMoniker Name="LookUpAdapter/CanAddNew" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="LookUpProperty" MonikerAttributeName="name" MonikerElementName="lookUpPropertyMoniker" ElementName="lookUpProperty" MonikerTypeName="LookUpPropertyMoniker">
        <DomainClassMoniker Name="LookUpProperty" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="LookUpProperty/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isBrowsable">
            <DomainPropertyMoniker Name="LookUpProperty/IsBrowsable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="datatype">
            <DomainPropertyMoniker Name="LookUpProperty/Datatype" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="LookUpProperty/DisplayName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isCustomized">
            <DomainPropertyMoniker Name="LookUpProperty/IsCustomized" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataFormatString">
            <DomainPropertyMoniker Name="LookUpProperty/DataFormatString" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="precision">
            <DomainPropertyMoniker Name="LookUpProperty/Precision" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="edmKey">
            <DomainPropertyMoniker Name="LookUpProperty/EdmKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="entityPropertyRelated">
            <DomainPropertyMoniker Name="LookUpProperty/EntityPropertyRelated" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPrimaryKey">
            <DomainPropertyMoniker Name="LookUpProperty/IsPrimaryKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="domainName">
            <DomainPropertyMoniker Name="LookUpProperty/DomainName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="kpiName">
            <DomainPropertyMoniker Name="LookUpProperty/KpiName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="substituteProperties">
            <DomainPropertyMoniker Name="LookUpProperty/SubstituteProperties" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="ignoreMetaData">
            <DomainPropertyMoniker Name="LookUpProperty/IgnoreMetaData" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dependencyProperty">
            <DomainPropertyMoniker Name="LookUpProperty/DependencyProperty" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filter">
            <DomainPropertyMoniker Name="LookUpProperty/Filter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customHierarchy">
            <DomainPropertyMoniker Name="LookUpProperty/CustomHierarchy" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasLookUpAdapters" MonikerAttributeName="" MonikerElementName="entityAdapterDesignerRootHasLookUpAdaptersMoniker" ElementName="entityAdapterDesignerRootHasLookUpAdapters" MonikerTypeName="EntityAdapterDesignerRootHasLookUpAdaptersMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasLookUpAdapters" />
      </XmlClassData>
      <XmlClassData TypeName="LookUpAdapterHasLookUpProperties" MonikerAttributeName="" MonikerElementName="lookUpAdapterHasLookUpPropertiesMoniker" ElementName="lookUpAdapterHasLookUpProperties" MonikerTypeName="LookUpAdapterHasLookUpPropertiesMoniker">
        <DomainRelationshipMoniker Name="LookUpAdapterHasLookUpProperties" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterReferencesLookUpAdapters" MonikerAttributeName="" MonikerElementName="entityAdapterReferencesLookUpAdaptersMoniker" ElementName="entityAdapterReferencesLookUpAdapters" MonikerTypeName="EntityAdapterReferencesLookUpAdaptersMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterReferencesLookUpAdapters" />
      </XmlClassData>
      <XmlClassData TypeName="LookUpAdapterShape" MonikerAttributeName="" MonikerElementName="lookUpAdapterShapeMoniker" ElementName="lookUpAdapterShape" MonikerTypeName="LookUpAdapterShapeMoniker">
        <CompartmentShapeMoniker Name="LookUpAdapterShape" />
        <ElementData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="LookUpAdapterShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="LookUpAdapterShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="LookUpAdapterShape/OutlineDashStyle" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AssociationLookUpConnector" MonikerAttributeName="" MonikerElementName="associationLookUpConnectorMoniker" ElementName="associationLookUpConnector" MonikerTypeName="AssociationLookUpConnectorMoniker">
        <ConnectorMoniker Name="AssociationLookUpConnector" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterUserInterface" MonikerAttributeName="name" SerializeId="true" MonikerElementName="entityAdapterUserInterfaceMoniker" ElementName="entityAdapterUserInterface" MonikerTypeName="EntityAdapterUserInterfaceMoniker">
        <DomainClassMoniker Name="EntityAdapterUserInterface" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="solutionName">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/SolutionName" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapter">
            <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesEntityAdapter" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="structuralType">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/StructuralType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="loadType">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/LoadType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="generatingType">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/GeneratingType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="pageSize">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/PageSize" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="layoutContent">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/LayoutContent" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="specializedLayoutType">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/SpecializedLayoutType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="nameSpace">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/NameSpace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDefault">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/IsDefault" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isMaintenanceLookUp">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/IsMaintenanceLookUp" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filterClearIsAutomatic">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/FilterClearIsAutomatic" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="baseUserInterface">
            <DomainRelationshipMoniker Name="UserInterfaceReferencesBaseUserInterface" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="entityClassInfo" Representation="Ignore">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/EntityClassInfo" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="alwaysSearchIfLookUp">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/AlwaysSearchIfLookUp" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/DisplayName" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="subscription">
            <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesSubscription" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="subscriptionNameSpace">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/SubscriptionNameSpace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="subscriptionEntityAdapterName">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/SubscriptionEntityAdapterName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="hasCustomization">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/HasCustomization" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="queryOnLoad">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/QueryOnLoad" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="userInterfaceClientEvented">
            <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceHasUserInterfaceClientEvented" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="enableWizardTableView">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/EnableWizardTableView" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="visualType">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/VisualType" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="clientLocalService">
            <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesClientLocalService" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="useLikeCommandAsDefault">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/UseLikeCommandAsDefault" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="helpTags">
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/HelpTags" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasEntityAdapterUserInterfaces" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasEntityAdapterUserInterfacesMoniker" ElementName="entityAdapterDesignerRootHasEntityAdapterUserInterfaces" MonikerTypeName="EntityAdapterDesignerRootHasEntityAdapterUserInterfacesMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasEntityAdapterUserInterfaces" />
      </XmlClassData>
      <XmlClassData TypeName="AssociationUserInterfaceConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationUserInterfaceConnectorMoniker" ElementName="associationUserInterfaceConnector" MonikerTypeName="AssociationUserInterfaceConnectorMoniker">
        <ConnectorMoniker Name="AssociationUserInterfaceConnector" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterUserInterfaceReferencesEntityAdapter" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterUserInterfaceReferencesEntityAdapterMoniker" ElementName="entityAdapterUserInterfaceReferencesEntityAdapter" MonikerTypeName="EntityAdapterUserInterfaceReferencesEntityAdapterMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesEntityAdapter" />
      </XmlClassData>
      <XmlClassData TypeName="DomainView" MonikerAttributeName="name" SerializeId="true" MonikerElementName="domainViewMoniker" ElementName="domainView" MonikerTypeName="DomainViewMoniker">
        <DomainClassMoniker Name="DomainView" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="DomainView/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="domainValues">
            <DomainRelationshipMoniker Name="DomainViewHasDomainValues" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="hasCustomValues">
            <DomainPropertyMoniker Name="DomainView/HasCustomValues" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="DomainValue" MonikerAttributeName="name" SerializeId="true" MonikerElementName="domainValueMoniker" ElementName="domainValue" MonikerTypeName="DomainValueMoniker">
        <DomainClassMoniker Name="DomainValue" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="DomainValue/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="value">
            <DomainPropertyMoniker Name="DomainValue/Value" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="DomainValue/DisplayName" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasDomainViews" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasDomainViewsMoniker" ElementName="entityAdapterDesignerRootHasDomainViews" MonikerTypeName="EntityAdapterDesignerRootHasDomainViewsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasDomainViews" />
      </XmlClassData>
      <XmlClassData TypeName="DomainViewHasDomainValues" MonikerAttributeName="" SerializeId="true" MonikerElementName="domainViewHasDomainValuesMoniker" ElementName="domainViewHasDomainValues" MonikerTypeName="DomainViewHasDomainValuesMoniker">
        <DomainRelationshipMoniker Name="DomainViewHasDomainValues" />
      </XmlClassData>
      <XmlClassData TypeName="DomainViewShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="domainViewShapeMoniker" ElementName="domainViewShape" MonikerTypeName="DomainViewShapeMoniker">
        <CompartmentShapeMoniker Name="DomainViewShape" />
      </XmlClassData>
      <XmlClassData TypeName="Subscription" MonikerAttributeName="name" SerializeId="true" MonikerElementName="subscriptionMoniker" ElementName="subscription" MonikerTypeName="SubscriptionMoniker">
        <DomainClassMoniker Name="Subscription" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="Subscription/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="businessObjectPath">
            <DomainPropertyMoniker Name="Subscription/BusinessObjectPath" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="title">
            <DomainPropertyMoniker Name="Subscription/Title" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="hasError" Representation="Ignore">
            <DomainPropertyMoniker Name="Subscription/HasError" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasSubscriptions" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasSubscriptionsMoniker" ElementName="entityAdapterDesignerRootHasSubscriptions" MonikerTypeName="EntityAdapterDesignerRootHasSubscriptionsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasSubscriptions" />
      </XmlClassData>
      <XmlClassData TypeName="SubscriptionShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="subscriptionShapeMoniker" ElementName="subscriptionShape" MonikerTypeName="SubscriptionShapeMoniker">
        <ImageShapeMoniker Name="SubscriptionShape" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterPublicationProperty" MonikerAttributeName="" MonikerElementName="entityAdapterPublicationPropertyMoniker" ElementName="entityAdapterPublicationProperty" MonikerTypeName="EntityAdapterPublicationPropertyMoniker">
        <DomainClassMoniker Name="EntityAdapterPublicationProperty" />
        <ElementData>
          <XmlPropertyData XmlName="edmKey">
            <DomainPropertyMoniker Name="EntityAdapterPublicationProperty/EdmKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="defaultValue">
            <DomainPropertyMoniker Name="EntityAdapterPublicationProperty/DefaultValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filter">
            <DomainPropertyMoniker Name="EntityAdapterPublicationProperty/Filter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetKeyName">
            <DomainPropertyMoniker Name="EntityAdapterPublicationProperty/TargetKeyName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="suffix">
            <DomainPropertyMoniker Name="EntityAdapterPublicationProperty/Suffix" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterHasEntityAdapterPublicationProperties" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterHasEntityAdapterPublicationPropertiesMoniker" ElementName="entityAdapterHasEntityAdapterPublicationProperties" MonikerTypeName="EntityAdapterHasEntityAdapterPublicationPropertiesMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterPublicationProperties" />
      </XmlClassData>
      <XmlClassData TypeName="KeyPerformanceIndicator" MonikerAttributeName="name" SerializeId="true" MonikerElementName="keyPerformanceIndicatorMoniker" ElementName="keyPerformanceIndicator" MonikerTypeName="KeyPerformanceIndicatorMoniker">
        <DomainClassMoniker Name="KeyPerformanceIndicator" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="KeyPerformanceIndicator/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="KeyPerformanceIndicator/Description" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="kpiRangeItems">
            <DomainRelationshipMoniker Name="KeyPerformanceIndicatorHasKpiRangeItems" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="showType">
            <DomainPropertyMoniker Name="KeyPerformanceIndicator/ShowType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="nameSpace">
            <DomainPropertyMoniker Name="KeyPerformanceIndicator/NameSpace" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="KpiRangeItem" MonikerAttributeName="name" SerializeId="true" MonikerElementName="kpiRangeItemMoniker" ElementName="kpiRangeItem" MonikerTypeName="KpiRangeItemMoniker">
        <DomainClassMoniker Name="KpiRangeItem" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="KpiRangeItem/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="KpiRangeItem/Description" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="startValue">
            <DomainPropertyMoniker Name="KpiRangeItem/StartValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="alpha">
            <DomainPropertyMoniker Name="KpiRangeItem/Alpha" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="red">
            <DomainPropertyMoniker Name="KpiRangeItem/Red" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="green">
            <DomainPropertyMoniker Name="KpiRangeItem/Green" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="blue">
            <DomainPropertyMoniker Name="KpiRangeItem/Blue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="endValue">
            <DomainPropertyMoniker Name="KpiRangeItem/EndValue" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasKeyPerformanceIndicators" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasKeyPerformanceIndicatorsMoniker" ElementName="entityAdapterDesignerRootHasKeyPerformanceIndicators" MonikerTypeName="EntityAdapterDesignerRootHasKeyPerformanceIndicatorsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasKeyPerformanceIndicators" />
      </XmlClassData>
      <XmlClassData TypeName="KeyPerformanceIndicatorHasKpiRangeItems" MonikerAttributeName="" SerializeId="true" MonikerElementName="keyPerformanceIndicatorHasKpiRangeItemsMoniker" ElementName="keyPerformanceIndicatorHasKpiRangeItems" MonikerTypeName="KeyPerformanceIndicatorHasKpiRangeItemsMoniker">
        <DomainRelationshipMoniker Name="KeyPerformanceIndicatorHasKpiRangeItems" />
      </XmlClassData>
      <XmlClassData TypeName="KeyPerformanceIndicatorShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="keyPerformanceIndicatorShapeMoniker" ElementName="keyPerformanceIndicatorShape" MonikerTypeName="KeyPerformanceIndicatorShapeMoniker">
        <CompartmentShapeMoniker Name="KeyPerformanceIndicatorShape" />
      </XmlClassData>
      <XmlClassData TypeName="Workflow" MonikerAttributeName="name" SerializeId="true" MonikerElementName="workflowMoniker" ElementName="workflow" MonikerTypeName="WorkflowMoniker">
        <DomainClassMoniker Name="Workflow" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="Workflow/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="comments">
            <DomainPropertyMoniker Name="Workflow/Comments" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="display">
            <DomainPropertyMoniker Name="Workflow/Display" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isOperationRelated">
            <DomainPropertyMoniker Name="Workflow/IsOperationRelated" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasWorkflows" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasWorkflowsMoniker" ElementName="entityAdapterDesignerRootHasWorkflows" MonikerTypeName="EntityAdapterDesignerRootHasWorkflowsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasWorkflows" />
      </XmlClassData>
      <XmlClassData TypeName="WorkflowShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="workflowShapeMoniker" ElementName="workflowShape" MonikerTypeName="WorkflowShapeMoniker">
        <ImageShapeMoniker Name="WorkflowShape" />
      </XmlClassData>
      <XmlClassData TypeName="GenericOperationReferencesWorkflow" MonikerAttributeName="" SerializeId="true" MonikerElementName="genericOperationReferencesWorkflowMoniker" ElementName="genericOperationReferencesWorkflow" MonikerTypeName="GenericOperationReferencesWorkflowMoniker">
        <DomainRelationshipMoniker Name="GenericOperationReferencesWorkflow" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterExtendedFilter" MonikerAttributeName="name" SerializeId="true" MonikerElementName="entityAdapterExtendedFilterMoniker" ElementName="entityAdapterExtendedFilter" MonikerTypeName="EntityAdapterExtendedFilterMoniker">
        <DomainClassMoniker Name="EntityAdapterExtendedFilter" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="EntityAdapterExtendedFilter/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="entityName">
            <DomainPropertyMoniker Name="EntityAdapterExtendedFilter/EntityName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="relationName">
            <DomainPropertyMoniker Name="EntityAdapterExtendedFilter/RelationName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="EntityAdapterExtendedFilter/DisplayName" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapterPropertyExtendedFilters">
            <DomainRelationshipMoniker Name="EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="displayInfo" Representation="Ignore">
            <DomainPropertyMoniker Name="EntityAdapterExtendedFilter/DisplayInfo" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isUsedInTheLinq">
            <DomainPropertyMoniker Name="EntityAdapterExtendedFilter/IsUsedInTheLinq" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterPropertyExtendedFilter" MonikerAttributeName="name" SerializeId="true" MonikerElementName="entityAdapterPropertyExtendedFilterMoniker" ElementName="entityAdapterPropertyExtendedFilter" MonikerTypeName="EntityAdapterPropertyExtendedFilterMoniker">
        <DomainClassMoniker Name="EntityAdapterPropertyExtendedFilter" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="EntityAdapterPropertyExtendedFilter/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="EntityAdapterPropertyExtendedFilter/DisplayName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataType">
            <DomainPropertyMoniker Name="EntityAdapterPropertyExtendedFilter/DataType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isEnabled">
            <DomainPropertyMoniker Name="EntityAdapterPropertyExtendedFilter/IsEnabled" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="edmKey">
            <DomainPropertyMoniker Name="EntityAdapterPropertyExtendedFilter/EdmKey" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFiltersMoniker" ElementName="entityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters" MonikerTypeName="EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFiltersMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterExtendedFilterHasEntityAdapterPropertyExtendedFilters" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterHasEntityAdapterExtendedFilters" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterHasEntityAdapterExtendedFiltersMoniker" ElementName="entityAdapterHasEntityAdapterExtendedFilters" MonikerTypeName="EntityAdapterHasEntityAdapterExtendedFiltersMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterExtendedFilters" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterReferencesBaseEntityAdapter" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterReferencesBaseEntityAdapterMoniker" ElementName="entityAdapterReferencesBaseEntityAdapter" MonikerTypeName="EntityAdapterReferencesBaseEntityAdapterMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterReferencesBaseEntityAdapter" />
      </XmlClassData>
      <XmlClassData TypeName="InheritanceEntityConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="inheritanceEntityConnectorMoniker" ElementName="inheritanceEntityConnector" MonikerTypeName="InheritanceEntityConnectorMoniker">
        <ConnectorMoniker Name="InheritanceEntityConnector" />
      </XmlClassData>
      <XmlClassData TypeName="LookUpAdapterReferencesBaseLookUpAdapter" MonikerAttributeName="" SerializeId="true" MonikerElementName="lookUpAdapterReferencesBaseLookUpAdapterMoniker" ElementName="lookUpAdapterReferencesBaseLookUpAdapter" MonikerTypeName="LookUpAdapterReferencesBaseLookUpAdapterMoniker">
        <DomainRelationshipMoniker Name="LookUpAdapterReferencesBaseLookUpAdapter" />
      </XmlClassData>
      <XmlClassData TypeName="InheritanceLookUpConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="inheritanceLookUpConnectorMoniker" ElementName="inheritanceLookUpConnector" MonikerTypeName="InheritanceLookUpConnectorMoniker">
        <ConnectorMoniker Name="InheritanceLookUpConnector" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterReferencesLocalEntityAdapter" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterReferencesLocalEntityAdapterMoniker" ElementName="entityAdapterReferencesLocalEntityAdapter" MonikerTypeName="EntityAdapterReferencesLocalEntityAdapterMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterReferencesLocalEntityAdapter" />
      </XmlClassData>
      <XmlClassData TypeName="LocalEntityConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="localEntityConnectorMoniker" ElementName="localEntityConnector" MonikerTypeName="LocalEntityConnectorMoniker">
        <ConnectorMoniker Name="LocalEntityConnector" />
      </XmlClassData>
      <XmlClassData TypeName="UserInterfaceReferencesBaseUserInterface" MonikerAttributeName="" SerializeId="true" MonikerElementName="userInterfaceReferencesBaseUserInterfaceMoniker" ElementName="userInterfaceReferencesBaseUserInterface" MonikerTypeName="UserInterfaceReferencesBaseUserInterfaceMoniker">
        <DomainRelationshipMoniker Name="UserInterfaceReferencesBaseUserInterface" />
      </XmlClassData>
      <XmlClassData TypeName="InheritanceUserInterface" MonikerAttributeName="" SerializeId="true" MonikerElementName="inheritanceUserInterfaceMoniker" ElementName="inheritanceUserInterface" MonikerTypeName="InheritanceUserInterfaceMoniker">
        <ConnectorMoniker Name="InheritanceUserInterface" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterRepresentation" MonikerAttributeName="name" SerializeId="true" MonikerElementName="entityAdapterRepresentationMoniker" ElementName="entityAdapterRepresentation" MonikerTypeName="EntityAdapterRepresentationMoniker">
        <DomainClassMoniker Name="EntityAdapterRepresentation" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetEntityAdapterName">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/TargetEntityAdapterName" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="targetEntityAdapterRepresentation">
            <DomainRelationshipMoniker Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="businessObject">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/BusinessObject" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetNameSpace">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/TargetNameSpace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetEdmName">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/TargetEdmName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetEdmEntityName">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/TargetEdmEntityName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isIQueryable">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/IsIQueryable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPublisherUpdatable">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/IsPublisherUpdatable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isReadOnly">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/IsReadOnly" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableExtendedFilter">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/EnableExtendedFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filter">
            <DomainPropertyMoniker Name="EntityAdapterRepresentation/Filter" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterRepresentationReferencesTargetEntityAdapterRepresentationMoniker" ElementName="entityAdapterRepresentationReferencesTargetEntityAdapterRepresentation" MonikerTypeName="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentationMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation" />
        <ElementData>
          <XmlPropertyData XmlName="joinType">
            <DomainPropertyMoniker Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation/JoinType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetProperties">
            <DomainPropertyMoniker Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation/TargetProperties" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sourceProperties">
            <DomainPropertyMoniker Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation/SourceProperties" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasEntityAdapterRepresentations" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasEntityAdapterRepresentationsMoniker" ElementName="entityAdapterDesignerRootHasEntityAdapterRepresentations" MonikerTypeName="EntityAdapterDesignerRootHasEntityAdapterRepresentationsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasEntityAdapterRepresentations" />
      </XmlClassData>
      <XmlClassData TypeName="AssociationEntityRepresentationConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationEntityRepresentationConnectorMoniker" ElementName="associationEntityRepresentationConnector" MonikerTypeName="AssociationEntityRepresentationConnectorMoniker">
        <ConnectorMoniker Name="AssociationEntityRepresentationConnector" />
      </XmlClassData>
      <XmlClassData TypeName="EntityToEntityRepresentationConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityToEntityRepresentationConnectorMoniker" ElementName="entityToEntityRepresentationConnector" MonikerTypeName="EntityToEntityRepresentationConnectorMoniker">
        <ConnectorMoniker Name="EntityToEntityRepresentationConnector" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterReferencesEntityAdapterRepresentation" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterReferencesEntityAdapterRepresentationMoniker" ElementName="entityAdapterReferencesEntityAdapterRepresentation" MonikerTypeName="EntityAdapterReferencesEntityAdapterRepresentationMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterReferencesEntityAdapterRepresentation" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterRepresentationShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterRepresentationShapeMoniker" ElementName="entityAdapterRepresentationShape" MonikerTypeName="EntityAdapterRepresentationShapeMoniker">
        <ImageShapeMoniker Name="EntityAdapterRepresentationShape" />
      </XmlClassData>
      <XmlClassData TypeName="WebApiAction" MonikerAttributeName="" MonikerElementName="webApiActionMoniker" ElementName="webApiAction" MonikerTypeName="WebApiActionMoniker">
        <DomainClassMoniker Name="WebApiAction" />
        <ElementData>
          <XmlPropertyData XmlName="httpVerb">
            <DomainPropertyMoniker Name="WebApiAction/HttpVerb" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customRoutes">
            <DomainPropertyMoniker Name="WebApiAction/CustomRoutes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="routeActionName">
            <DomainPropertyMoniker Name="WebApiAction/RouteActionName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableRoutesForParameters">
            <DomainPropertyMoniker Name="WebApiAction/EnableRoutesForParameters" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableAccessControl">
            <DomainPropertyMoniker Name="WebApiAction/EnableAccessControl" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="RepositoryMethod" MonikerAttributeName="" MonikerElementName="repositoryMethodMoniker" ElementName="repositoryMethod" MonikerTypeName="RepositoryMethodMoniker">
        <DomainClassMoniker Name="RepositoryMethod" />
      </XmlClassData>
      <XmlClassData TypeName="WebApiController" MonikerAttributeName="name" SerializeId="true" MonikerElementName="webApiControllerMoniker" ElementName="webApiController" MonikerTypeName="WebApiControllerMoniker">
        <DomainClassMoniker Name="WebApiController" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="WebApiController/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="routePrefix">
            <DomainPropertyMoniker Name="WebApiController/RoutePrefix" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="webApiActions">
            <DomainRelationshipMoniker Name="WebApiControllerHasWebApiActions" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="repositoryInterface">
            <DomainRelationshipMoniker Name="WebApiControllerReferencesRepositoryInterface" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="projectSuffix">
            <DomainPropertyMoniker Name="WebApiController/ProjectSuffix" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableClient">
            <DomainPropertyMoniker Name="WebApiController/EnableClient" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="synchronizedWithDomainService">
            <DomainPropertyMoniker Name="WebApiController/SynchronizedWithDomainService" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDataService">
            <DomainPropertyMoniker Name="WebApiController/IsDataService" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isAspNetCore" Representation="Ignore">
            <DomainPropertyMoniker Name="WebApiController/IsAspNetCore" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="RepositoryInterface" MonikerAttributeName="name" SerializeId="true" MonikerElementName="repositoryInterfaceMoniker" ElementName="repositoryInterface" MonikerTypeName="RepositoryInterfaceMoniker">
        <DomainClassMoniker Name="RepositoryInterface" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="RepositoryInterface/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="repositoryMethods">
            <DomainRelationshipMoniker Name="RepositoryInterfaceHasRepositoryMethods" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="projectName">
            <DomainPropertyMoniker Name="RepositoryInterface/ProjectName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isExtension">
            <DomainPropertyMoniker Name="RepositoryInterface/IsExtension" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasWebApiControllers" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasWebApiControllersMoniker" ElementName="entityAdapterDesignerRootHasWebApiControllers" MonikerTypeName="EntityAdapterDesignerRootHasWebApiControllersMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasWebApiControllers" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasRepositoryInterfaces" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasRepositoryInterfacesMoniker" ElementName="entityAdapterDesignerRootHasRepositoryInterfaces" MonikerTypeName="EntityAdapterDesignerRootHasRepositoryInterfacesMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasRepositoryInterfaces" />
      </XmlClassData>
      <XmlClassData TypeName="WebApiControllerHasWebApiActions" MonikerAttributeName="" SerializeId="true" MonikerElementName="webApiControllerHasWebApiActionsMoniker" ElementName="webApiControllerHasWebApiActions" MonikerTypeName="WebApiControllerHasWebApiActionsMoniker">
        <DomainRelationshipMoniker Name="WebApiControllerHasWebApiActions" />
      </XmlClassData>
      <XmlClassData TypeName="RepositoryInterfaceHasRepositoryMethods" MonikerAttributeName="" SerializeId="true" MonikerElementName="repositoryInterfaceHasRepositoryMethodsMoniker" ElementName="repositoryInterfaceHasRepositoryMethods" MonikerTypeName="RepositoryInterfaceHasRepositoryMethodsMoniker">
        <DomainRelationshipMoniker Name="RepositoryInterfaceHasRepositoryMethods" />
      </XmlClassData>
      <XmlClassData TypeName="WebApiControllerShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="webApiControllerShapeMoniker" ElementName="webApiControllerShape" MonikerTypeName="WebApiControllerShapeMoniker">
        <CompartmentShapeMoniker Name="WebApiControllerShape" />
      </XmlClassData>
      <XmlClassData TypeName="RepositoryInterfaceShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="repositoryInterfaceShapeMoniker" ElementName="repositoryInterfaceShape" MonikerTypeName="RepositoryInterfaceShapeMoniker">
        <CompartmentShapeMoniker Name="RepositoryInterfaceShape" />
      </XmlClassData>
      <XmlClassData TypeName="WebApiControllerReferencesRepositoryInterface" MonikerAttributeName="" SerializeId="true" MonikerElementName="webApiControllerReferencesRepositoryInterfaceMoniker" ElementName="webApiControllerReferencesRepositoryInterface" MonikerTypeName="WebApiControllerReferencesRepositoryInterfaceMoniker">
        <DomainRelationshipMoniker Name="WebApiControllerReferencesRepositoryInterface" />
      </XmlClassData>
      <XmlClassData TypeName="RepositoryConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="repositoryConnectorMoniker" ElementName="repositoryConnector" MonikerTypeName="RepositoryConnectorMoniker">
        <ConnectorMoniker Name="RepositoryConnector" />
      </XmlClassData>
      <XmlClassData TypeName="RepositoryImplementation" MonikerAttributeName="" SerializeId="true" MonikerElementName="repositoryImplementationMoniker" ElementName="repositoryImplementation" MonikerTypeName="RepositoryImplementationMoniker">
        <DomainClassMoniker Name="RepositoryImplementation" />
        <ElementData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="RepositoryImplementation/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="projectSuffix">
            <DomainPropertyMoniker Name="RepositoryImplementation/ProjectSuffix" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="repositoryInterface">
            <DomainRelationshipMoniker Name="RepositoryImplementationReferencesRepositoryInterface" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="repositoryName">
            <DomainPropertyMoniker Name="RepositoryImplementation/RepositoryName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDefault">
            <DomainPropertyMoniker Name="RepositoryImplementation/IsDefault" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isSelected" Representation="Ignore">
            <DomainPropertyMoniker Name="RepositoryImplementation/IsSelected" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasRepositoryImplementations" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasRepositoryImplementationsMoniker" ElementName="entityAdapterDesignerRootHasRepositoryImplementations" MonikerTypeName="EntityAdapterDesignerRootHasRepositoryImplementationsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasRepositoryImplementations" />
      </XmlClassData>
      <XmlClassData TypeName="RepositoryImplementationConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="repositoryImplementationConnectorMoniker" ElementName="repositoryImplementationConnector" MonikerTypeName="RepositoryImplementationConnectorMoniker">
        <ConnectorMoniker Name="RepositoryImplementationConnector" />
      </XmlClassData>
      <XmlClassData TypeName="RepositoryImplementationReferencesRepositoryInterface" MonikerAttributeName="" SerializeId="true" MonikerElementName="repositoryImplementationReferencesRepositoryInterfaceMoniker" ElementName="repositoryImplementationReferencesRepositoryInterface" MonikerTypeName="RepositoryImplementationReferencesRepositoryInterfaceMoniker">
        <DomainRelationshipMoniker Name="RepositoryImplementationReferencesRepositoryInterface" />
      </XmlClassData>
      <XmlClassData TypeName="RepositoryImplementationShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="repositoryImplementationShapeMoniker" ElementName="repositoryImplementationShape" MonikerTypeName="RepositoryImplementationShapeMoniker">
        <GeometryShapeMoniker Name="RepositoryImplementationShape" />
      </XmlClassData>
      <XmlClassData TypeName="EntityCollectionReferencesEntityOwners" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityCollectionReferencesEntityOwnersMoniker" ElementName="entityCollectionReferencesEntityOwners" MonikerTypeName="EntityCollectionReferencesEntityOwnersMoniker">
        <DomainRelationshipMoniker Name="EntityCollectionReferencesEntityOwners" />
        <ElementData>
          <XmlPropertyData XmlName="dataMemberName">
            <DomainPropertyMoniker Name="EntityCollectionReferencesEntityOwners/DataMemberName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="EntityCollectionReferencesEntityOwners/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataType">
            <DomainPropertyMoniker Name="EntityCollectionReferencesEntityOwners/DataType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="createEmptyInstance">
            <DomainPropertyMoniker Name="EntityCollectionReferencesEntityOwners/CreateEmptyInstance" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="CollectionConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="collectionConnectorMoniker" ElementName="collectionConnector" MonikerTypeName="CollectionConnectorMoniker">
        <ConnectorMoniker Name="CollectionConnector" />
      </XmlClassData>
      <XmlClassData TypeName="EntityInstanceReferencesEntityOwners" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityInstanceReferencesEntityOwnersMoniker" ElementName="entityInstanceReferencesEntityOwners" MonikerTypeName="EntityInstanceReferencesEntityOwnersMoniker">
        <DomainRelationshipMoniker Name="EntityInstanceReferencesEntityOwners" />
        <ElementData>
          <XmlPropertyData XmlName="dataMemberName">
            <DomainPropertyMoniker Name="EntityInstanceReferencesEntityOwners/DataMemberName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="EntityInstanceReferencesEntityOwners/Name" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="InstanceConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="instanceConnectorMoniker" ElementName="instanceConnector" MonikerTypeName="InstanceConnectorMoniker">
        <ConnectorMoniker Name="InstanceConnector" />
      </XmlClassData>
      <XmlClassData TypeName="AssociationUserInterfaceToSubscription" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationUserInterfaceToSubscriptionMoniker" ElementName="associationUserInterfaceToSubscription" MonikerTypeName="AssociationUserInterfaceToSubscriptionMoniker">
        <ConnectorMoniker Name="AssociationUserInterfaceToSubscription" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterUserInterfaceReferencesSubscription" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterUserInterfaceReferencesSubscriptionMoniker" ElementName="entityAdapterUserInterfaceReferencesSubscription" MonikerTypeName="EntityAdapterUserInterfaceReferencesSubscriptionMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesSubscription" />
      </XmlClassData>
      <XmlClassData TypeName="LookUpAdapterReferencesEntityDataModel" MonikerAttributeName="" SerializeId="true" MonikerElementName="lookUpAdapterReferencesEntityDataModelMoniker" ElementName="lookUpAdapterReferencesEntityDataModel" MonikerTypeName="LookUpAdapterReferencesEntityDataModelMoniker">
        <DomainRelationshipMoniker Name="LookUpAdapterReferencesEntityDataModel" />
      </XmlClassData>
      <XmlClassData TypeName="LookupEdmConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="lookupEdmConnectorMoniker" ElementName="lookupEdmConnector" MonikerTypeName="LookupEdmConnectorMoniker">
        <ConnectorMoniker Name="LookupEdmConnector" />
      </XmlClassData>
      <XmlClassData TypeName="StoreScript" MonikerAttributeName="name" SerializeId="true" MonikerElementName="storeScriptMoniker" ElementName="storeScript" MonikerTypeName="StoreScriptMoniker">
        <DomainClassMoniker Name="StoreScript" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="StoreScript/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="storeQueries">
            <DomainRelationshipMoniker Name="StoreScriptHasStoreQueries" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasStoreScripts" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasStoreScriptsMoniker" ElementName="entityAdapterDesignerRootHasStoreScripts" MonikerTypeName="EntityAdapterDesignerRootHasStoreScriptsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasStoreScripts" />
      </XmlClassData>
      <XmlClassData TypeName="StoreQuery" MonikerAttributeName="name" SerializeId="true" MonikerElementName="storeQueryMoniker" ElementName="storeQuery" MonikerTypeName="StoreQueryMoniker">
        <DomainClassMoniker Name="StoreQuery" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="StoreQuery/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="command">
            <DomainPropertyMoniker Name="StoreQuery/Command" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="parameters">
            <DomainPropertyMoniker Name="StoreQuery/Parameters" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="genericType">
            <DomainPropertyMoniker Name="StoreQuery/GenericType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="queryReturnType">
            <DomainPropertyMoniker Name="StoreQuery/QueryReturnType" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="StoreScriptShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="storeScriptShapeMoniker" ElementName="storeScriptShape" MonikerTypeName="StoreScriptShapeMoniker">
        <CompartmentShapeMoniker Name="StoreScriptShape" />
      </XmlClassData>
      <XmlClassData TypeName="StoreScriptHasStoreQueries" MonikerAttributeName="" SerializeId="true" MonikerElementName="storeScriptHasStoreQueriesMoniker" ElementName="storeScriptHasStoreQueries" MonikerTypeName="StoreScriptHasStoreQueriesMoniker">
        <DomainRelationshipMoniker Name="StoreScriptHasStoreQueries" />
      </XmlClassData>
      <XmlClassData TypeName="OlapCatalog" MonikerAttributeName="name" SerializeId="true" MonikerElementName="olapCatalogMoniker" ElementName="olapCatalog" MonikerTypeName="OlapCatalogMoniker">
        <DomainClassMoniker Name="OlapCatalog" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="OlapCatalog/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="server">
            <DomainPropertyMoniker Name="OlapCatalog/Server" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="catalog">
            <DomainPropertyMoniker Name="OlapCatalog/Catalog" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="userId">
            <DomainPropertyMoniker Name="OlapCatalog/UserId" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="password">
            <DomainPropertyMoniker Name="OlapCatalog/Password" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="windowsAuthentication">
            <DomainPropertyMoniker Name="OlapCatalog/WindowsAuthentication" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="idLinxDimensions">
            <DomainPropertyMoniker Name="OlapCatalog/IdLinxDimensions" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="idGpeconDimensions">
            <DomainPropertyMoniker Name="OlapCatalog/IdGpeconDimensions" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="idBandeiraRedeDimensions">
            <DomainPropertyMoniker Name="OlapCatalog/IdBandeiraRedeDimensions" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="measuresDimensions">
            <DomainPropertyMoniker Name="OlapCatalog/MeasuresDimensions" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="idFilialDimensions">
            <DomainPropertyMoniker Name="OlapCatalog/IdFilialDimensions" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasOlapCatalogs" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasOlapCatalogsMoniker" ElementName="entityAdapterDesignerRootHasOlapCatalogs" MonikerTypeName="EntityAdapterDesignerRootHasOlapCatalogsMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasOlapCatalogs" />
      </XmlClassData>
      <XmlClassData TypeName="OlapCatalogShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="olapCatalogShapeMoniker" ElementName="olapCatalogShape" MonikerTypeName="OlapCatalogShapeMoniker">
        <ImageShapeMoniker Name="OlapCatalogShape" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterReferencesOlapCatalog" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterReferencesOlapCatalogMoniker" ElementName="entityAdapterReferencesOlapCatalog" MonikerTypeName="EntityAdapterReferencesOlapCatalogMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterReferencesOlapCatalog" />
      </XmlClassData>
      <XmlClassData TypeName="AssociationOlapConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationOlapConnectorMoniker" ElementName="associationOlapConnector" MonikerTypeName="AssociationOlapConnectorMoniker">
        <ConnectorMoniker Name="AssociationOlapConnector" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterClientEvent" MonikerAttributeName="" MonikerElementName="entityAdapterClientEventMoniker" ElementName="entityAdapterClientEvent" MonikerTypeName="EntityAdapterClientEventMoniker">
        <DomainClassMoniker Name="EntityAdapterClientEvent" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterHasEntityAdapterClientEvented" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterHasEntityAdapterClientEventedMoniker" ElementName="entityAdapterHasEntityAdapterClientEvented" MonikerTypeName="EntityAdapterHasEntityAdapterClientEventedMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterHasEntityAdapterClientEvented" />
      </XmlClassData>
      <XmlClassData TypeName="ClientEvent" MonikerAttributeName="" MonikerElementName="clientEventMoniker" ElementName="clientEvent" MonikerTypeName="ClientEventMoniker">
        <DomainClassMoniker Name="ClientEvent" />
        <ElementData>
          <XmlPropertyData XmlName="macroScript">
            <DomainPropertyMoniker Name="ClientEvent/MacroScript" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="UserInterfaceClientEvent" MonikerAttributeName="" MonikerElementName="userInterfaceClientEventMoniker" ElementName="userInterfaceClientEvent" MonikerTypeName="UserInterfaceClientEventMoniker">
        <DomainClassMoniker Name="UserInterfaceClientEvent" />
        <ElementData>
          <XmlPropertyData XmlName="exposedByViewModel">
            <DomainPropertyMoniker Name="UserInterfaceClientEvent/ExposedByViewModel" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterUserInterfaceHasUserInterfaceClientEvented" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterUserInterfaceHasUserInterfaceClientEventedMoniker" ElementName="entityAdapterUserInterfaceHasUserInterfaceClientEvented" MonikerTypeName="EntityAdapterUserInterfaceHasUserInterfaceClientEventedMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceHasUserInterfaceClientEvented" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterUserInterfaceShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterUserInterfaceShapeMoniker" ElementName="entityAdapterUserInterfaceShape" MonikerTypeName="EntityAdapterUserInterfaceShapeMoniker">
        <CompartmentShapeMoniker Name="EntityAdapterUserInterfaceShape" />
        <ElementData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="EntityAdapterUserInterfaceShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="EntityAdapterUserInterfaceShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="EntityAdapterUserInterfaceShape/OutlineDashStyle" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AssociationUIToClientLocalServiceConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationUIToClientLocalServiceConnectorMoniker" ElementName="associationUIToClientLocalServiceConnector" MonikerTypeName="AssociationUIToClientLocalServiceConnectorMoniker">
        <ConnectorMoniker Name="AssociationUIToClientLocalServiceConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ClientLocalService" MonikerAttributeName="name" SerializeId="true" MonikerElementName="clientLocalServiceMoniker" ElementName="clientLocalService" MonikerTypeName="ClientLocalServiceMoniker">
        <DomainClassMoniker Name="ClientLocalService" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="ClientLocalService/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="serviceClientEvents">
            <DomainRelationshipMoniker Name="ClientLocalServiceHasServiceClientEvents" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="entityAdapter">
            <DomainRelationshipMoniker Name="ClientLocalServiceReferencesEntityAdapter" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="pageSize">
            <DomainPropertyMoniker Name="ClientLocalService/PageSize" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="serviceClientProperties">
            <DomainRelationshipMoniker Name="ClientLocalServiceHasServiceClientProperties" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="componentInjection">
            <DomainPropertyMoniker Name="ClientLocalService/ComponentInjection" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterDesignerRootHasClientLocalServices" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterDesignerRootHasClientLocalServicesMoniker" ElementName="entityAdapterDesignerRootHasClientLocalServices" MonikerTypeName="EntityAdapterDesignerRootHasClientLocalServicesMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterDesignerRootHasClientLocalServices" />
      </XmlClassData>
      <XmlClassData TypeName="EntityAdapterUserInterfaceReferencesClientLocalService" MonikerAttributeName="" SerializeId="true" MonikerElementName="entityAdapterUserInterfaceReferencesClientLocalServiceMoniker" ElementName="entityAdapterUserInterfaceReferencesClientLocalService" MonikerTypeName="EntityAdapterUserInterfaceReferencesClientLocalServiceMoniker">
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesClientLocalService" />
      </XmlClassData>
      <XmlClassData TypeName="ServiceClientEvent" MonikerAttributeName="" MonikerElementName="serviceClientEventMoniker" ElementName="serviceClientEvent" MonikerTypeName="ServiceClientEventMoniker">
        <DomainClassMoniker Name="ServiceClientEvent" />
        <ElementData>
          <XmlPropertyData XmlName="exposed">
            <DomainPropertyMoniker Name="ServiceClientEvent/Exposed" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isOutputMessage">
            <DomainPropertyMoniker Name="ServiceClientEvent/IsOutputMessage" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isInputMessage">
            <DomainPropertyMoniker Name="ServiceClientEvent/IsInputMessage" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClientLocalServiceHasServiceClientEvents" MonikerAttributeName="" SerializeId="true" MonikerElementName="clientLocalServiceHasServiceClientEventsMoniker" ElementName="clientLocalServiceHasServiceClientEvents" MonikerTypeName="ClientLocalServiceHasServiceClientEventsMoniker">
        <DomainRelationshipMoniker Name="ClientLocalServiceHasServiceClientEvents" />
      </XmlClassData>
      <XmlClassData TypeName="ClientLocalServiceShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="clientLocalServiceShapeMoniker" ElementName="clientLocalServiceShape" MonikerTypeName="ClientLocalServiceShapeMoniker">
        <CompartmentShapeMoniker Name="ClientLocalServiceShape" />
        <ElementData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="ClientLocalServiceShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="ClientLocalServiceShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="ClientLocalServiceShape/OutlineDashStyle" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AssociationClientLocalServiceToExternalConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationClientLocalServiceToExternalConnectorMoniker" ElementName="associationClientLocalServiceToExternalConnector" MonikerTypeName="AssociationClientLocalServiceToExternalConnectorMoniker">
        <ConnectorMoniker Name="AssociationClientLocalServiceToExternalConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ClientLocalServiceReferencesEntityAdapter" MonikerAttributeName="" SerializeId="true" MonikerElementName="clientLocalServiceReferencesEntityAdapterMoniker" ElementName="clientLocalServiceReferencesEntityAdapter" MonikerTypeName="ClientLocalServiceReferencesEntityAdapterMoniker">
        <DomainRelationshipMoniker Name="ClientLocalServiceReferencesEntityAdapter" />
      </XmlClassData>
      <XmlClassData TypeName="AssociationClientLocalServiceToEntityConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationClientLocalServiceToEntityConnectorMoniker" ElementName="associationClientLocalServiceToEntityConnector" MonikerTypeName="AssociationClientLocalServiceToEntityConnectorMoniker">
        <ConnectorMoniker Name="AssociationClientLocalServiceToEntityConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ServiceClientProperty" MonikerAttributeName="name" SerializeId="true" MonikerElementName="serviceClientPropertyMoniker" ElementName="serviceClientProperty" MonikerTypeName="ServiceClientPropertyMoniker">
        <DomainClassMoniker Name="ServiceClientProperty" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="ServiceClientProperty/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="defaultValue">
            <DomainPropertyMoniker Name="ServiceClientProperty/DefaultValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="exposed">
            <DomainPropertyMoniker Name="ServiceClientProperty/Exposed" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClientLocalServiceHasServiceClientProperties" MonikerAttributeName="" SerializeId="true" MonikerElementName="clientLocalServiceHasServiceClientPropertiesMoniker" ElementName="clientLocalServiceHasServiceClientProperties" MonikerTypeName="ClientLocalServiceHasServiceClientPropertiesMoniker">
        <DomainRelationshipMoniker Name="ClientLocalServiceHasServiceClientProperties" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="EntityAdapterDesignerExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="EntityAdapterReferencesTargetEntityAdapterBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterReferencesTargetEntityAdapter" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterReferencesEntityDataModelBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterReferencesEntityDataModel" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityDataModel" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="CommentReferencesEntityAdaptersBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="CommentReferencesEntityAdapters" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Comment" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="CommentReferencesEntityDataModelsBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="CommentReferencesEntityDataModels" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Comment" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityDataModel" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterReferencesLookUpAdaptersBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterReferencesLookUpAdapters" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="LookUpAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterUserInterfaceReferencesEntityAdapterBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesEntityAdapter" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapterUserInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="GenericOperationReferencesWorkflowBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="GenericOperationReferencesWorkflow" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="GenericOperation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Workflow" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterReferencesBaseEntityAdapterBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterReferencesBaseEntityAdapter" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="LookUpAdapterReferencesBaseLookUpAdapterBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="LookUpAdapterReferencesBaseLookUpAdapter" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="LookUpAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="LookUpAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterReferencesLocalEntityAdapterBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterReferencesLocalEntityAdapter" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="UserInterfaceReferencesBaseUserInterfaceBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="UserInterfaceReferencesBaseUserInterface" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapterUserInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapterUserInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentationBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapterRepresentation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapterRepresentation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterReferencesEntityAdapterRepresentationBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterReferencesEntityAdapterRepresentation" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapterRepresentation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="WebApiControllerReferencesRepositoryInterfaceBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="WebApiControllerReferencesRepositoryInterface" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="WebApiController" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="RepositoryInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="RepositoryImplementationReferencesRepositoryInterfaceBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="RepositoryImplementationReferencesRepositoryInterface" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="RepositoryImplementation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="RepositoryInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityCollectionReferencesEntityOwnersBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityCollectionReferencesEntityOwners" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityInstanceReferencesEntityOwnersBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityInstanceReferencesEntityOwners" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterUserInterfaceReferencesSubscriptionBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesSubscription" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapterUserInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Subscription" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="LookUpAdapterReferencesEntityDataModelBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="LookUpAdapterReferencesEntityDataModel" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="LookUpAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityDataModel" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterReferencesOlapCatalogBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterReferencesOlapCatalog" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="OlapCatalog" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="EntityAdapterUserInterfaceReferencesClientLocalServiceBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesClientLocalService" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapterUserInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ClientLocalService" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ClientLocalServiceReferencesEntityAdapterBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ClientLocalServiceReferencesEntityAdapter" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ClientLocalService" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="EntityAdapter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="f04924a9-b953-4eec-9242-f599def4ec95" Description="Business Views Diagram." Name="EntityAdapterDesignerDiagram" DisplayName="Business Views Diagram" Namespace="Linx.EntityAdapterDesigner">
    <Class>
      <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
    </Class>
    <ShapeMaps>
      <ShapeMap>
        <DomainClassMoniker Name="EntityDataModel" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasEntityDataModels.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityDataModelShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityDataModel/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityDataModelShape/ContextType" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityDataModel/ContextType" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityDataModelShape/Alert" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityDataModel/HasError" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <ImageShapeMoniker Name="EntityDataModelShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="EntityAdapter" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasEntityAdapters.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapter/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterShape/EnableForPublicationIndicator" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapter/EnableForPublication" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterShape/EntityInfo" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapter/EntityClassInfo" />
              <DomainPath />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterShape/AggregationMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapter/IsAggregationView" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterShape/IsPOCO" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapter/POCOInfo" />
            </PropertyPath>
          </PropertyDisplayed>
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapter/IsPOCO" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterShape/CustomBaseType" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapter/CustomBaseType" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterShape/IsDashboardFilter" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapter/IsDashboardFilter" />
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterShape/AutoPk" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapter/CreateDynamicPrimaryKey" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterShape/MetaDataFilter" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapter/EnableMetaDataFilter" />
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterShape/ModelViewMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapter/IsModelView" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterShape/BigDataTrack" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapter/IsLargeDataMode" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="EntityAdapterShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="EntityAdapterShape/PropertiesCompartiment" />
          <ElementsDisplayed>
            <DomainPath>EntityAdapterHasEntityAdapterProperties.EntityAdapterProperties/!EntityAdapterProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterProperty/DisplayValue" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="EntityAdapterShape/FormulasCompartiment" />
          <ElementsDisplayed>
            <DomainPath>EntityAdapterHasEntityAdapterFormulas.EntityAdapterFormulas/!EntityAdapterFormula</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterAttribute/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="EntityAdapterShape/OperationsCompartiment" />
          <ElementsDisplayed>
            <DomainPath>EntityAdapterHasEntityAdapterOperations.EntityAdapterOperations/!EntityAdapterOperation</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GenericOperation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="EntityAdapterShape/EventsCompartiment" />
          <ElementsDisplayed>
            <DomainPath>EntityAdapterHasEntityAdapterEvents.EntityAdapterEvents/!EntityAdapterEvent</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GenericOperation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="EntityAdapterShape/ExtendedFilters" />
          <ElementsDisplayed>
            <DomainPath>EntityAdapterHasEntityAdapterExtendedFilters.EntityAdapterExtendedFilters/!EntityAdapterExtendedFilter</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterExtendedFilter/DisplayInfo" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="EntityAdapterShape/PublicationPropertiesCompartiment" />
          <ElementsDisplayed>
            <DomainPath>EntityAdapterHasEntityAdapterPublicationProperties.EntityAdapterPublicationProperties/!EntityAdapterPublicationProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterAttribute/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="EntityAdapterShape/ClientEventsCompartiment" />
          <ElementsDisplayed>
            <DomainPath>EntityAdapterHasEntityAdapterClientEvented.EntityAdapterClientEvented/!EntityAdapterClientEvent</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GenericOperation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Comment" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasComments.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CommentShape/Comment" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Comment/Text" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="CommentShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="DomainServiceExtension" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasDomainServiceExtensions.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="DomainServiceExtensionShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="DomainServiceExtension/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="DomainServiceExtensionShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="DomainServiceExtensionShape/DomainServiceOperationsCompartiment" />
          <ElementsDisplayed>
            <DomainPath>DomainServiceExtensionHasDomainServiceOperations.DomainServiceOperations/!DomainServiceOperation</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GenericOperation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="LookUpAdapter" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasLookUpAdapters.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="LookUpAdapterShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="LookUpAdapter/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="LookUpAdapterShape/CustomDecorator" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="LookUpAdapter/IsCustomized" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="LookUpAdapterShape/MultiSelection" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="LookUpAdapter/IsMultiSelection" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="LookUpAdapterShape/LookUpInfo" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="LookUpAdapter/LookUpClassInfo" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="LookUpAdapterShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="LookUpAdapterShape/LookUpPropertiesDomainServiceOperationsCompartiment" />
          <ElementsDisplayed>
            <DomainPath>LookUpAdapterHasLookUpProperties.LookUpProperties/!LookUpProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="LookUpProperty/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="DomainView" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasDomainViews.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="DomainViewShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="DomainView/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="DomainViewShape/HasCustomValues" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="DomainView/HasCustomValues" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="DomainViewShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="DomainViewShape/DomainValueCompartment" />
          <ElementsDisplayed>
            <DomainPath>DomainViewHasDomainValues.DomainValues/!DomainValue</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="DomainValue/DisplayName" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Subscription" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasSubscriptions.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="SubscriptionShape/TitleDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Subscription/Title" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="SubscriptionShape/Alert" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="Subscription/HasError" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <ImageShapeMoniker Name="SubscriptionShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="KeyPerformanceIndicator" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasKeyPerformanceIndicators.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="KeyPerformanceIndicatorShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="KeyPerformanceIndicator/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="KeyPerformanceIndicatorShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="KeyPerformanceIndicatorShape/KpiRangeItemdecorator" />
          <ElementsDisplayed>
            <DomainPath>KeyPerformanceIndicatorHasKpiRangeItems.KpiRangeItems/!KpiRangeItem</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="KpiRangeItem/Description" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Workflow" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasWorkflows.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="WorkflowShape/DisplayDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Workflow/Display" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="WorkflowShape/IsOperationRelated" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="Workflow/IsOperationRelated" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <ImageShapeMoniker Name="WorkflowShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="EntityAdapterRepresentation" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasEntityAdapterRepresentations.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterRepresentationShape/TargetEntityAdapterName" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterRepresentation/TargetEntityAdapterName" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterRepresentationShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterRepresentation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <ImageShapeMoniker Name="EntityAdapterRepresentationShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="WebApiController" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasWebApiControllers.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="WebApiControllerShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="WebApiController/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="WebApiControllerShape/DataServiceMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="WebApiController/IsDataService" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="WebApiControllerShape/WebApiSyncMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="WebApiController/SynchronizedWithDomainService" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="WebApiControllerShape/WebApiMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="WebApiController/SynchronizedWithDomainService" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="False" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="WebApiControllerShape/AspNetCore" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="WebApiController/IsAspNetCore" />
            <DomainPath />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="WebApiControllerShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="WebApiControllerShape/WebApiActionCompartment" />
          <ElementsDisplayed>
            <DomainPath>WebApiControllerHasWebApiActions.WebApiActions/!WebApiAction</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GenericOperation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="RepositoryInterface" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasRepositoryInterfaces.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="RepositoryInterfaceShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="RepositoryInterface/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="RepositoryInterfaceShape/ExtensionMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="RepositoryInterface/IsExtension" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="RepositoryInterfaceShape/RepositoryMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="RepositoryInterface/IsExtension" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="False" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="RepositoryInterfaceShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="RepositoryInterfaceShape/RepositoryMethodCompartment" />
          <ElementsDisplayed>
            <DomainPath>RepositoryInterfaceHasRepositoryMethods.RepositoryMethods/!RepositoryMethod</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GenericOperation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="RepositoryImplementation" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasRepositoryImplementations.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="RepositoryImplementationShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="RepositoryImplementation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="RepositoryImplementationShape/IsDefault" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="RepositoryImplementation/IsDefault" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="RepositoryImplementationShape/IsSelected" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="RepositoryImplementation/IsSelected" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <GeometryShapeMoniker Name="RepositoryImplementationShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="StoreScript" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasStoreScripts.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="StoreScriptShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="StoreScript/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="StoreScriptShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="StoreScriptShape/StoreQueriesCompartiment" />
          <ElementsDisplayed>
            <DomainPath>StoreScriptHasStoreQueries.StoreQueries/!StoreQuery</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="StoreQuery/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="OlapCatalog" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasOlapCatalogs.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="OlapCatalogShape/Catalog" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="OlapCatalog/Catalog" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <ImageShapeMoniker Name="OlapCatalogShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="EntityAdapterUserInterface" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasEntityAdapterUserInterfaces.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterUserInterfaceShape/EntityName" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterUserInterface/SubscriptionEntityAdapterName" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterUserInterfaceShape/IsDefault" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/IsDefault" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterUserInterfaceShape/IsSpecializedLookUp" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/SpecializedLayoutType" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="IsSpecializedLookUp" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterUserInterfaceShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterUserInterface/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterUserInterfaceShape/UICustom" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/GeneratingType" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="CustomizableLayout" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="EntityAdapterUserInterfaceShape/UIInfo" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterUserInterface/EntityClassInfo" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterUserInterfaceShape/LayoutInfo" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/VisualType" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="Web" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="EntityAdapterUserInterfaceShape/LayoutInfoMobile" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapterUserInterface/VisualType" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="Mobile" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="EntityAdapterUserInterfaceShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="EntityAdapterUserInterfaceShape/ClientEventsCompartiment" />
          <ElementsDisplayed>
            <DomainPath>EntityAdapterUserInterfaceHasUserInterfaceClientEvented.UserInterfaceClientEvented/!UserInterfaceClientEvent</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GenericOperation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ClientLocalService" />
        <ParentElementPath>
          <DomainPath>EntityAdapterDesignerRootHasClientLocalServices.EntityAdapterDesignerRoot/!EntityAdapterDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ClientLocalServiceShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ClientLocalService/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="ClientLocalServiceShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="ClientLocalServiceShape/ServiceClientEventsCompartiment" />
          <ElementsDisplayed>
            <DomainPath>ClientLocalServiceHasServiceClientEvents.ServiceClientEvents/!ServiceClientEvent</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GenericOperation/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="ClientLocalServiceShape/ServiceClientPropertiesCompartiment" />
          <ElementsDisplayed>
            <DomainPath>ClientLocalServiceHasServiceClientProperties.ServiceClientProperties/!ServiceClientProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ServiceClientProperty/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationEntityConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterReferencesTargetEntityAdapter" />
        <DecoratorMap>
          <IconDecoratorMoniker Name="AssociationEntityConnector/IsDashboard" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="EntityAdapterReferencesTargetEntityAdapter/IsDashboard" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationEdmConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterReferencesEntityDataModel" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="CommentEntityConnector" />
        <DomainRelationshipMoniker Name="CommentReferencesEntityAdapters" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="CommentEntityConnector" />
        <DomainRelationshipMoniker Name="CommentReferencesEntityDataModels" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationLookUpConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterReferencesLookUpAdapters" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationUserInterfaceConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesEntityAdapter" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="InheritanceEntityConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterReferencesBaseEntityAdapter" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="InheritanceLookUpConnector" />
        <DomainRelationshipMoniker Name="LookUpAdapterReferencesBaseLookUpAdapter" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="LocalEntityConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterReferencesLocalEntityAdapter" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="InheritanceUserInterface" />
        <DomainRelationshipMoniker Name="UserInterfaceReferencesBaseUserInterface" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationEntityRepresentationConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="AssociationEntityRepresentationConnector/InnerType" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation/JoinType" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="EntityToEntityRepresentationConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterReferencesEntityAdapterRepresentation" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="RepositoryConnector" />
        <DomainRelationshipMoniker Name="WebApiControllerReferencesRepositoryInterface" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="RepositoryImplementationConnector" />
        <DomainRelationshipMoniker Name="RepositoryImplementationReferencesRepositoryInterface" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="CollectionConnector" />
        <DomainRelationshipMoniker Name="EntityCollectionReferencesEntityOwners" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="CollectionConnector/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityCollectionReferencesEntityOwners/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="InstanceConnector" />
        <DomainRelationshipMoniker Name="EntityInstanceReferencesEntityOwners" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="InstanceConnector/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="EntityInstanceReferencesEntityOwners/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationUserInterfaceToSubscription" />
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesSubscription" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="LookupEdmConnector" />
        <DomainRelationshipMoniker Name="LookUpAdapterReferencesEntityDataModel" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationOlapConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterReferencesOlapCatalog" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationUIToClientLocalServiceConnector" />
        <DomainRelationshipMoniker Name="EntityAdapterUserInterfaceReferencesClientLocalService" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationClientLocalServiceToEntityConnector" />
        <DomainRelationshipMoniker Name="ClientLocalServiceReferencesEntityAdapter" />
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="ead" EditorGuid="4c78c854-115c-4e8f-a7f7-af46a11d0dd1">
    <RootClass>
      <DomainClassMoniker Name="EntityAdapterDesignerRoot" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="EntityAdapterDesignerSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="Business Model">
      <ElementTool Name="DataContextTool" ToolboxIcon="Resources\EdmTool.bmp" Caption="Data Context" Tooltip="" HelpKeyword="DataContextTool">
        <DomainClassMoniker Name="EntityDataModel" />
      </ElementTool>
      <ElementTool Name="EntityAdapterTool" ToolboxIcon="Resources\EntityAdapterTool.bmp" Caption="Business View" Tooltip="" HelpKeyword="EntityAdapterTool">
        <DomainClassMoniker Name="EntityAdapter" />
      </ElementTool>
      <ConnectionTool Name="AssociationDataContextTool" ToolboxIcon="Resources\AssociationEdmTool.bmp" Caption="Link View To DataContext" Tooltip="" HelpKeyword="AssociationDataContextTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterReferencesEntityDataModelBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="AssociationEntityTool" ToolboxIcon="Resources\AssociationEntityTool.bmp" Caption="Link View To Parent" Tooltip="" HelpKeyword="AssociationEntityTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterReferencesTargetEntityAdapterBuilder" />
      </ConnectionTool>
      <ElementTool Name="LookUpAdapter" ToolboxIcon="Resources\LookUpAdapterTool.bmp" Caption="Look Up" Tooltip="" HelpKeyword="LookUpAdapter">
        <DomainClassMoniker Name="LookUpAdapter" />
      </ElementTool>
      <ConnectionTool Name="AssociationLookUpTool" ToolboxIcon="Resources\AssociationLookUpTool.bmp" Caption="Link View To LookUp" Tooltip="" HelpKeyword="AssociationLookUpTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterReferencesLookUpAdaptersBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="InheritanceEntityTool" ToolboxIcon="Resources\GeneralizationTool.bmp" Caption="Business View Inheritance" Tooltip="" HelpKeyword="InheritanceEntityTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterReferencesBaseEntityAdapterBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="InheritanceLookUpTool" ToolboxIcon="Resources\GeneralizationTool.bmp" Caption="LookUp Inheritance" Tooltip="" HelpKeyword="InheritanceLookUpTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/LookUpAdapterReferencesBaseLookUpAdapterBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="LocalEntityAssociationTool" ToolboxIcon="Resources\LocalAssociationTool.bmp" Caption="Link View To Local View" Tooltip="" HelpKeyword="LocalEntityAssociationTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterReferencesLocalEntityAdapterBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="EntityCollectionTool" ToolboxIcon="Resources\CollectionConnector.bmp" Caption="Link View As Collection" Tooltip="" HelpKeyword="EntityCollectionTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityCollectionReferencesEntityOwnersBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="EntityInstanceTool" ToolboxIcon="Resources\InstanceConnector.bmp" Caption="Link View As Instance" Tooltip="Entity Instance Tool" HelpKeyword="EntityInstanceTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityInstanceReferencesEntityOwnersBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="AssociationLookUpEdmTool" ToolboxIcon="Resources\AssociationEdmTool.bmp" Caption="Link LookUp To DataContext" Tooltip="" HelpKeyword="AssociationLookUpEdmTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/LookUpAdapterReferencesEntityDataModelBuilder" />
      </ConnectionTool>
      <ElementTool Name="OlapCatalogTool" ToolboxIcon="Resources\OlapCatalogTool.bmp" Caption="OLAP Catalog" Tooltip="" HelpKeyword="OlapCatalogTool">
        <DomainClassMoniker Name="OlapCatalog" />
      </ElementTool>
      <ConnectionTool Name="AssociationOlapTool" ToolboxIcon="Resources\AssociationEdmTool.bmp" Caption="Link View To OlapCatalog" Tooltip="" HelpKeyword="AssociationOlapTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterReferencesOlapCatalogBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <ToolboxTab TabText="Presentation Layer">
      <ConnectionTool Name="InheritanceUITool" ToolboxIcon="Resources\GeneralizationTool.bmp" Caption="User Interface Inheritance" Tooltip="" HelpKeyword="InheritanceUITool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/UserInterfaceReferencesBaseUserInterfaceBuilder" />
      </ConnectionTool>
      <ElementTool Name="EntityAdapterUserInterfaceTool" ToolboxIcon="Resources\LayoutTool.bmp" Caption="User Interface" Tooltip="" HelpKeyword="EntityAdapterUserInterfaceTool">
        <DomainClassMoniker Name="EntityAdapterUserInterface" />
      </ElementTool>
      <ConnectionTool Name="AssociationUserInterfaceTool" ToolboxIcon="Resources\ExampleConnectorTool.bmp" Caption="Link UserInterface To  View  " Tooltip="" HelpKeyword="AssociationUserInterfaceTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterUserInterfaceReferencesEntityAdapterBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="AssociationUserInterfaceSubscriptionTool" ToolboxIcon="Resources\ExampleConnectorTool.bmp" Caption="Link UI To  Subscription" Tooltip="" HelpKeyword="AssociationUserInterfaceSubscriptionTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterUserInterfaceReferencesSubscriptionBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="AssociationClientExternalServiceTool" ToolboxIcon="Resources\Copy of AssociationClientServiceTool.bmp" Caption="Link UI to LocalService" Tooltip="" HelpKeyword="AssociationClientExternalServiceTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterUserInterfaceReferencesClientLocalServiceBuilder" />
      </ConnectionTool>
      <ElementTool Name="ClientLocalServiceTool" ToolboxIcon="Resources\ClientServiceTool.bmp" Caption="Local Service" Tooltip="" HelpKeyword="ClientLocalServiceTool">
        <DomainClassMoniker Name="ClientLocalService" />
      </ElementTool>
      <ConnectionTool Name="AssociationLocalServiceToViewTool" ToolboxIcon="Resources\Copy of AssociationClientServiceTool.bmp" Caption="Link LocalService To View" Tooltip="" HelpKeyword="AssociationLocalServiceToViewTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/ClientLocalServiceReferencesEntityAdapterBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <ToolboxTab TabText="Representation">
      <ElementTool Name="EntityAdapterRepresentationTool" ToolboxIcon="Resources\EntityRepresentationTool.bmp" Caption="Business Representation" Tooltip="" HelpKeyword="EntityAdapterRepresentationTool">
        <DomainClassMoniker Name="EntityAdapterRepresentation" />
      </ElementTool>
      <ConnectionTool Name="AssociationEntityRepresentationTool" ToolboxIcon="Resources\AssociationEntityRepresentationTool.bmp" Caption="Link Two Representations" Tooltip="" HelpKeyword="AssociationEntityRepresentationTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentationBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="AssociationEntityToEntityRepresentationTool" ToolboxIcon="Resources\ConnectorEntityToEntityRepresentation.bmp" Caption="Link View to Representation" Tooltip="" HelpKeyword="AssociationEntityToEntityRepresentationTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/EntityAdapterReferencesEntityAdapterRepresentationBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <ToolboxTab TabText="Workflow">
      <ElementTool Name="Workflow" ToolboxIcon="Resources\Workflow.bmp" Caption="Workflow" Tooltip="" HelpKeyword="Workflow">
        <DomainClassMoniker Name="Workflow" />
      </ElementTool>
    </ToolboxTab>
    <ToolboxTab TabText="Business Common">
      <ElementTool Name="DomainViewTool" ToolboxIcon="Resources\DomainView.bmp" Caption="Domain Enumerator" Tooltip="" HelpKeyword="DomainViewTool">
        <DomainClassMoniker Name="DomainView" />
      </ElementTool>
      <ElementTool Name="KeyPerformanceIndicatorTool" ToolboxIcon="Resources\KPI.bmp" Caption="Key Performance Indicator" Tooltip="" HelpKeyword="KeyPerformanceIndicator">
        <DomainClassMoniker Name="KeyPerformanceIndicator" />
      </ElementTool>
    </ToolboxTab>
    <ToolboxTab TabText="Business API">
      <ElementTool Name="DomainServiceExtensionTool" ToolboxIcon="Resources\DomainServiceExtensionTool.bmp" Caption="Domain Service Extension" Tooltip="" HelpKeyword="DomainServiceExtensionTool">
        <DomainClassMoniker Name="DomainServiceExtension" />
      </ElementTool>
      <ElementTool Name="WebApiControllerTool" ToolboxIcon="Resources\WebApiControllerTool.bmp" Caption="Web API Controller" Tooltip="" HelpKeyword="WebApiControllerTool">
        <DomainClassMoniker Name="WebApiController" />
      </ElementTool>
      <ElementTool Name="RepositoryInterfaceTool" ToolboxIcon="Resources\RepositoryInterfaceTool.bmp" Caption="Interface" Tooltip="" HelpKeyword="RepositoryInterfaceTool">
        <DomainClassMoniker Name="RepositoryInterface" />
      </ElementTool>
      <ConnectionTool Name="AssociationRepositoryInterfaceTool" ToolboxIcon="Resources\AssociationRepositoryInterfaceTool.bmp" Caption="Link WebAPI to Interface" Tooltip="" HelpKeyword="AssociationRepositoryInterfaceTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/WebApiControllerReferencesRepositoryInterfaceBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="AssociationRepositoryImplementationTool" ToolboxIcon="Resources\AssociationRepositoryImplementationTool.bmp" Caption="Link Implementation to Interface" Tooltip="" HelpKeyword="AssociationRepositoryImplementationTool">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/RepositoryImplementationReferencesRepositoryInterfaceBuilder" />
      </ConnectionTool>
      <ElementTool Name="RepositoryImplementationTool" ToolboxIcon="Resources\RepositoryImplementationTool.bmp" Caption="Implementation" Tooltip="" HelpKeyword="RepositoryImplementationTool">
        <DomainClassMoniker Name="RepositoryImplementation" />
      </ElementTool>
      <ElementTool Name="StoreScriptTool" ToolboxIcon="Resources\StoreScriptTool.bmp" Caption="Store Script" Tooltip="" HelpKeyword="StoreScriptTool">
        <DomainClassMoniker Name="StoreScript" />
      </ElementTool>
    </ToolboxTab>
    <ToolboxTab TabText="Business Documentation">
      <ElementTool Name="CommentTool" ToolboxIcon="resources\commenttool.bmp" Caption="Comment" Tooltip="" HelpKeyword="CommentTool">
        <DomainClassMoniker Name="Comment" />
      </ElementTool>
      <ConnectionTool Name="CommentsReferenceEntities" ToolboxIcon="resources\CommentEntityLinkTool.bmp" Caption="Link Comment To View" Tooltip="" HelpKeyword="CommentsReferenceEntities">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/CommentReferencesEntityAdaptersBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="CommentsReferenceDataContext" ToolboxIcon="resources\CommentEdmLinkTool.bmp" Caption="Link Comment To  DataContext" Tooltip="" HelpKeyword="CommentsReferenceDataContext">
        <ConnectionBuilderMoniker Name="EntityAdapterDesigner/CommentReferencesEntityDataModelsBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <ToolboxTab TabText="Subscription">
      <ElementTool Name="SubscriptionTool" ToolboxIcon="Resources\SubscriptionTool.bmp" Caption="BO Subscription" Tooltip="" HelpKeyword="SubscriptionTool">
        <DomainClassMoniker Name="Subscription" />
      </ElementTool>
    </ToolboxTab>
    <Validation UsesMenu="true" UsesOpen="false" UsesSave="true" UsesLoad="false" />
    <DiagramMoniker Name="EntityAdapterDesignerDiagram" />
  </Designer>
  <Explorer ExplorerGuid="f52344f8-7ec8-4912-8642-36997fa6fa3d" Title="EntityAdapterDesigner Explorer">
    <ExplorerBehaviorMoniker Name="EntityAdapterDesigner/EntityAdapterDesignerExplorer" />
  </Explorer>
</Dsl>