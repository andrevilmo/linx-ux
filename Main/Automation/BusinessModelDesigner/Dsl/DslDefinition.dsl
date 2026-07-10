<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="e7f3668d-5282-4fe7-9e53-c84e4e77638e" Description="Business Model Diagrams" Name="BusinessModelDesigner" DisplayName="Business Model Diagrams" Namespace="Linx.BusinessModelDesigner" ProductName="BusinessModelDesigner" CompanyName="Linx" PackageGuid="5f92b837-d6aa-4bc6-9135-86f55732f61b" PackageNamespace="Linx.BusinessModelDesigner" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="6265f113-c51b-4640-870d-35aad410df81" Description="" Name="NamedElement" DisplayName="Named Element" InheritanceModifier="Abstract" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="b5084159-43dc-45dc-9105-a32a6afbc981" Description="" Name="Name" DisplayName="Name" DefaultValue="" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="5bb49240-e986-4344-b507-46351bdef5e4" Description="" Name="BusinessModelDesignerRoot" DisplayName="Business Model Designer Root" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="65a6b640-7832-492d-b819-c521d7a6ac9d" Description="Document file name" Name="DocumentName" DisplayName="Document Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="adc2b548-7281-4379-91c6-7b219472a948" Description="The target namespace for the solution." Name="TargetNamespace" DisplayName="Target Namespace" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b2575abb-e2a4-4304-a476-63e8479a782b" Description="Diagram Document Path" Name="DocumentPath" DisplayName="Document Path" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ee6199ba-5a0a-41a7-b2bc-a800c773004a" Description="If this property is true, this designer does not generate code." Name="NoCode" DisplayName="No Code">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c1df33dd-56c4-4dc1-9181-057ad0423443" Description="Container name for all entities." Name="DataContextName" DisplayName="Data Context Name" DefaultValue="BusinessModel">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="910c15f8-38cf-4c84-adb1-821c692ab744" Description="Do not generate all foreign keys indexes." Name="RemoveAutomaticIndexes" DisplayName="Remove Automatic Indexes">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a92c2634-3eac-492f-9b04-fe4f67af6b9d" Description="Do not generate the required attributes on the properties." Name="RemoveRequiredAttributes" DisplayName="Remove Required Attributes">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0468d299-ed91-4b9f-8475-d773eba4124b" Description="This is the business group name for separating dimension filters (e.g.: Sales)." Name="BusinessGroupForFilteringOfDimension" DisplayName="Business Group For Filtering Of Dimension">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f8da8597-5e1d-4da4-b735-36164d5f3888" Description="" Name="EnableAutomaticAuthorization" DisplayName="Enable Automatic Authorization" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7ce9a662-a89d-48c4-89d1-3e8b2a26372e" Description="" Name="EnableAccessConnectionControl" DisplayName="Enable Access Connection Control" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0aace8af-0f23-4118-9602-99c8b166bbbd" Description="Enable PreGeneratedViews mechanism." Name="EnableViewCacheFactory" DisplayName="Enable View Cache Factory" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="66d713db-f37f-4398-897e-7cbe4c20f110" Description="If this value is true, configure always ID_LINX == ID_GPECON" Name="SetIdLinxWithIdGpecon" DisplayName="Set Id Linx With Id Gpecon">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a1bede2b-999c-4d99-b1ee-482ac2f04d6b" Description="" Name="ForceDynamicForeignKeyNames" DisplayName="Force Dynamic Foreign Key Names" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f4bca899-73ae-42c5-97f8-066eafbb7d10" Description="" Name="ControlIdLinxByApplicative" DisplayName="Control ID_LINX By Applicative">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cfdde3b6-5aaa-44c4-82d6-9dea2d899815" Description="Creates the project, for customization by customer(developer)." Name="GenerateCustomerCustomizationProject" DisplayName="Generate Customer Customization Project" DefaultValue="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ec19668e-c6fd-4c5c-a844-ce3e9cc149a9" Description="Generate Asp.Net Core Web Api " Name="IsAspNetCore" DisplayName="Is Asp Net Core" DefaultValue="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2bdf8165-b4ce-4cc0-ba02-d32af28ca5e2" Description="Disable all audits in this context" Name="DisableAllAudits" DisplayName="Disable All Audits" DefaultValue="true" Category="Audit">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Comment" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BusinessModelDesignerRootHasComments.Comments</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ModelType" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BusinessModelDesignerRootHasTypes.Types</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="StoreScript" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BusinessModelDesignerRootHasStoreScripts.StoreScripts</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="DbProvider" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BusinessModelDesignerRootHasDbProviders.DbProviders</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ModelImplementation" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BusinessModelDesignerRootHasModelImplementations.ModelImplementations</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="WebApiController" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>BusinessModelDesignerRootHasWebApiControllers.WebApiControllers</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="0dc76900-72b6-4f3f-ba8d-09e6e167f9aa" Description="" Name="ModelClass" DisplayName="Model Class" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ModelType" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="90627c50-1ad2-4bb6-95e6-8d9e2c00cca2" Description="Type of main structure of this class." Name="Kind" DisplayName="Structure Type" DefaultValue="Table">
          <Type>
            <DomainEnumerationMoniker Name="ClassKind" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="fabae223-9700-4e21-9d35-dc00c226b51c" Description="" Name="Modifier" DisplayName="Modifier" DefaultValue="None">
          <Type>
            <DomainEnumerationMoniker Name="InheritanceModifier" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b6fea8ea-503b-4367-97e6-0aa305212fb5" Description="Database table name" Name="Table" DisplayName="Table Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ad820c4f-cde1-4bdd-99d2-5e87d7dd80be" Description="Database schema name" Name="Schema" DisplayName="Schema" DefaultValue="dbo">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="57ff7aef-10e7-4843-a8ea-8ec949aebe0a" Description="Primary Key Constraint Name" Name="PrimaryKeyConstraintName" DisplayName="Primary Key Constraint Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="288eab6c-f6ae-4ff1-9f9e-555f219c036c" Description="The display name information." Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3cf2e700-13da-49f5-b168-245db25cd5f5" Description="Not mapped witht the database." Name="NotMapped" DisplayName="Not Mapped">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9f42089b-b788-4242-a565-5177d65f40ea" Description="Custom attribute definitions. All attribute definitions must be separated per #." Name="CustomAttributes" DisplayName="Custom Attributes">
          <Notes>All attribute definitions should be separated per #.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9c3f4736-8adc-456f-a854-dc3aacaa503a" Description="Hide all associations connected with this object." Name="HideAssociations" DisplayName="Hide Associations">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9b7a1d6a-fa39-474e-b5f2-ebbcf4e50d85" Description="The primary key index is clustered." Name="IsClustered" DisplayName="Is Clustered" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1eaf8f24-4f8c-48b6-9329-801d7856d8cf" Description="Enable this entity for filtering of related dimensions." Name="IsFactTable" DisplayName="Is Fact Table" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="922c2680-ed6e-467a-a40c-43879d56807f" Description="This information is a dimension used exclusively for filtering." Name="IsDimensionFilter" DisplayName="Is Dimension Filter" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4cc1cd1a-4c3e-404d-873b-ef3b69b7dc15" Description="" Name="ContentDefinition" DisplayName="Content Definition">
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
        <DomainProperty Id="7fd2f99c-9416-42ae-8ce6-b53a1ffc0527" Description="Column name for inherited primary key." Name="PrimaryKeyColumnMap" DisplayName="Primary Key Column Map">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="be7b89e6-d6aa-4857-92c3-6351842618d8" Description="This entity is read only, that is, it does not support crud operations." Name="IsReadOnly" DisplayName="Is Read Only">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8467e289-4362-433b-a8a5-8a57d4ffcade" Description="This property alerts that this class is in study mode." Name="InStudy" DisplayName="In Study" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d5f537dc-b499-4653-b1ce-e00ec126f493" Description="Enable control of internal business rules for IdLinx." Name="EnableIdLinxForSearching" DisplayName="Enable Id Linx For Searching" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="bac73a20-688e-46c6-9703-25ff38bd5564" Description="Enable control of internal business rules for IdGpecon." Name="EnableIdGpeconForSearching" DisplayName="Enable Id Gpecon For Searching" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f7f09ed0-9cc6-4e93-81af-cb89441efbfd" Description="Enable control of internal business rules for IdLinx." Name="EnableIdLinxForInserting" DisplayName="Enable Id Linx For Inserting" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4670c3f0-49f7-4370-9c1f-b995d1123ef1" Description="Enable control of internal business rules for IdGpecon." Name="EnableIdGpeconForInserting" DisplayName="Enable Id Gpecon For Inserting" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1e9cd34e-cb41-416d-a8e4-111e2b9b6272" Description="Enable validation on saving  for this entity." Name="IsValidatable" DisplayName="Is Validatable">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="fe702044-515b-49b0-93fb-d896221a668e" Description="" Name="ModelViewDefinition" DisplayName="Model View Definition" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="42f44a3c-29ca-45b0-ba27-74385c863a0a" Description="" Name="ModelViewAggregation" DisplayName="Model View Aggregation">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="03142049-ce0d-4d46-a481-f8df197ca297" Description="The main entity of the query definition." Name="ModelViewMainEntity" DisplayName="Model View Main Entity" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f0a2e7b1-4faa-42d5-8f87-9d68c664cb5a" Description="All inner entities for CRUD." Name="ModelViewDbSets" DisplayName="Model View Db Sets" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4190bf40-3671-41c9-8b47-c97a29f81c6c" Description="Enable caching of second level." Name="EnableCache" DisplayName="Enable Cache">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="82306e18-64ea-48e2-a96a-cf02fa7cf259" Description="Example: (HasFilter(PROPERTY_NAME) &amp;&amp; this.ViewFieldName1 == &quot;AAXX&quot; || this.ViewFieldName2 &gt; 20)" Name="Filter" DisplayName="Model View Filter/Having">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="128fb2a9-2b46-4b92-b7d5-e085a86cdae0" Description="Execute distinct command over the  model view select command." Name="ModelViewDistinct" DisplayName="Model View Distinct">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7f4a310a-bc8e-4ff1-863b-bb032b022143" Description="" Name="ModelViewTop" DisplayName="Model View Maximal Number Of Rows" DefaultValue="0">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="287c5888-7649-4fc4-8283-3073b8b138a3" Description="Code for creating resources before the query execution." Name="ModelViewCodePreQuery" DisplayName="Model View Code Pre Query">
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
        <DomainProperty Id="136b3f3d-9df4-4da0-a15e-7f985a4ca55b" Description="Enable automatic search control for ID_FILIAL_PFJ." Name="EnableIdFilialPfjControl" DisplayName="Enable Id Filial Pfj Control" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1db62a02-b646-4160-9e35-14e9c4c302e7" Description="Override model view query." Name="ModelViewCustomQueryEnabled" DisplayName="Model View Custom Query Enabled">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e6d087e1-a597-46b6-9884-1a6281b1fec8" Description="Database Script.  Procedure Example:  PROC:DataType1 ParamName1#DataType2 ParamName2.  Function Example:  FUNC::DataType1 ParamName1#DataType2 ParamName2. Select Example: SELECT * FROM TABLE." Name="DatabaseScriptCommand" DisplayName="Database Script Command">
          <Notes>Database Script.  Procedure Example:  PROC:DataType1 ParamName1#DataType2 ParamName2.  Function Example:  FUNC::DataType1 ParamName1#DataType2 ParamName2. Select Example: SELECT * FROM TABLE.</Notes>
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
        <DomainProperty Id="0d790011-dcaa-4718-b56c-dee48cb7ceb6" Description="Enable audit for this entity." Name="EnableAudit" DisplayName="Enable Audit" Category="Audit">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2a1b7350-b768-41f1-bd43-2facd08c92fa" Description="Entity, All Columns, Selected Columns" Name="AuditType" DisplayName="Audit Type" DefaultValue="Entity" Category="Audit">
          <Notes>Entity, All Columns, Selected Columns
Audit options:
 - Entity: a JSON will be saved with the serialized object, before and after the changed, which has been changed;
 - All columns: will be saved column by column, and all columns should be monitored, which has been changed;
 - By Selected Columns: only the marked columns will be saved, which has been changed.
 - None, not aplied audit for entity</Notes>
          <Type>
            <DomainEnumerationMoniker Name="AuditType" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ModelAttribute" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ClassHasAttributes.Attributes</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ClassOperation" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ClassHasOperations.Operations</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ModelIndex" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ClassHasIndexes.ModelIndexes</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="RouteMapData" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ClassHasDataRoutes.RouteMapDatum</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="b42b1feb-391f-4b49-9cfc-fc22425318c2" Description="An attribute of a class." Name="ModelAttribute" DisplayName="Model Attribute" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ClassModelElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="045468e4-a688-40df-818a-b94204ba324f" Description="" Name="DataType" DisplayName="Data Type" DefaultValue="String">
          <Type>
            <DomainEnumerationMoniker Name="ModelDataType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="49fa45b7-e97a-4f27-8e44-ffb8af0e4088" Description="" Name="DefaultValue" DisplayName="Default Value" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="31183f49-442c-4f92-b76c-e239d6ef7d74" Description="" Name="ColumnName" DisplayName="Column Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3ff42f1f-16cc-43b7-b32b-4679e6a1b77d" Description="" Name="IsPrimaryKey" DisplayName="Is Primary Key" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0cfc55a4-7cd1-4f1e-bc84-0533762cf410" Description="" Name="IsIdentity" DisplayName="Is Identity" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8837defd-29bb-4f1e-9bb7-a15c8d35237a" Description="" Name="IsNullable" DisplayName="Is Nullable" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e7c8d720-479c-4fa4-b618-43c2f21cdcaf" Description="" Name="Precision" DisplayName="Precision" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Int16" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f2db793c-f44d-4b92-bed7-82de6432a4ff" Description="" Name="Scale" DisplayName="Scale">
          <Type>
            <ExternalTypeMoniker Name="/System/Int16" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8eece2a8-1c39-4133-abb9-18bb568a68ed" Description="The display name information." Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4473ac89-4d81-49bd-9777-12f0e0fa355f" Description="Custom attribute definitions. All attribute definitions must be separated per #." Name="CustomAttributes" DisplayName="Custom Attributes">
          <Notes>All attribute definitions should be separated per #.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="15048041-d6a6-4b8e-a03b-83710bc65390" Description="The values range for validation (eg: 1, 7)." Name="Range" DisplayName="Range">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="34f52337-3681-4101-9677-1ac489906d7f" Description="Data Format (Ex: C02, N02, d)" Name="DataFormatString" DisplayName="Data Format String">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="855649df-8411-480d-877f-65d03950ad15" Description="Domain view name." Name="DomainName" DisplayName="Domain Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="90c3dca6-d0a2-4b8a-b3f1-e67795e4abb1" Description="" Name="Mask" DisplayName="Mask">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4d8e86dd-0614-4ea3-ae7a-1422937db280" Description="Code that returns the formula value. You can use the following macros:@Iif(MacroCondition;;TrueMacroExpression;;FalseMacroExpression), @Divide(NumeratorExp;;DenominatorExp), @Year(exp), @Month(exp), @Day(exp), @Hour(exp), @Minute(exp), @Second(exp)." Name="Formula" DisplayName="Formula">
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
        <DomainProperty Id="07c58e79-1a09-46bf-80d8-f25092fa2592" Description="ModelAttributes that trigger the formula execution. Example: Property1, Property2, ..., PropertyN" Name="TriggerAttributes" DisplayName="Trigger Attributes">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0b55b7f6-5d77-442c-b3b9-9c4dfe747aae" Description="" Name="ForeignKey" DisplayName="Foreign Key" DefaultValue="" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2c095882-130f-4558-a411-181bacf34327" Description="Filter by this attribute (Ex1: HasFilter(PROPERTY_NAME) &amp;&amp; [Value] == 10 || [Value] == this.ViewFieldName, Ex2: [Value] &gt;= 20 &amp;&amp; this.ViewFieldName &lt;= 50)." Name="Filter" DisplayName="Model View Filter/Having">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f869bfdf-50ae-4f67-81c6-6e0e03d26699" Description="Store Data Type" Name="ColumnType" DisplayName="Column Type" Kind="Calculated" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7ece0990-e675-49f3-886b-6dec2150e2c0" Description="Not mapped witht the database." Name="NotMapped" DisplayName="Not Mapped">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="35428ed6-0477-426c-bc94-c6f0e79a90f7" Description="Entity Framework don't try to update this column. But EF returns the value from the database after inserting or updating data." Name="IsComputed" DisplayName="Is Computed">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="93cf65f7-5336-4ffd-a018-13254d4daa4a" Description="Max Length For Strings" Name="MaxLength" DisplayName="Max Length" DefaultValue="10">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="595e45d8-cb08-4a34-8b14-ff6e29370594" Description="Default column definition." Name="SqlDefault" DisplayName="Sql Default">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="71607235-cae8-4f28-9c9f-defe60c6b51e" Description="Besides the primary key, this information indicates that this information is a filter suggestion for dimensions." Name="IsDimensionFilterSuggestion" DisplayName="Is Dimension Filter Suggestion">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="77013faa-222b-4c1c-b230-e2bf1befde6a" Description="This property alerts that this property is in study mode." Name="InStudy" DisplayName="In Study" DefaultValue="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a5e5fcd3-6eb1-4302-b316-f1ebc691d63b" Description="Source data property from business query." Name="ModelViewSource" DisplayName="Model View Source" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="abcf1e18-77ac-4984-8b81-a11d0a560ca1" Description="Expression for Model View Linq. You also can use a composed key like this: KEY(PROP1,PROP2,PROP3)." Name="ModelViewFormula" DisplayName="Model View Formula">
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
        <DomainProperty Id="e53ccebd-5aec-4feb-8871-49786d3f0431" Description="" Name="AggregationFunction" DisplayName="Model View Aggregation Function" DefaultValue="None">
          <Type>
            <DomainEnumerationMoniker Name="AggregationFunctions" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d7ad8324-8fc9-4786-858a-6ea0739499c6" Description="This filter will be applied if there is no filter over this property (Ex1: HasFilter(PROPERTY_NAME) &amp;&amp; [Value] == 10 || [Value] == this.ViewFieldName, Ex2: [Value] &gt;= 20 &amp;&amp; this.ViewFieldName &lt;= 50)." Name="ExclusiveFilter" DisplayName="Model View Filter/Having (Default)">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="479e7cad-ad59-49fa-b0e0-0eb8cce56cb9" Description="Index for orderby sequence." Name="ModelViewOrderBySequence" DisplayName="Model View Order By Sequence" DefaultValue="-1">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="625246ef-1060-41ba-b639-4a07989b76c8" Description="" Name="ModelViewOrderByOrientation" DisplayName="Model View Order By Orientation" DefaultValue="Ascending">
          <Type>
            <DomainEnumerationMoniker Name="OrderByOrientationType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b25af81a-0ac4-4781-b578-ce0296ecbd2d" Description="" Name="IsCustomized" DisplayName="Is Customized">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c8e2df4b-7d45-4cee-b5ca-44a9ae52494a" Description="Disable data filtering." Name="FilteringDisabled" DisplayName="Filtering Disabled">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0eda886c-aa74-4f36-8ad9-50ad62b11986" Description="Filter by count distinct (Ex: this.QTDE &gt; 10 || [Value] &lt; 50)." Name="ModelViewCountDistinctFilter" DisplayName="Model View Count Distinct Filter">
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
        <DomainProperty Id="134194ec-14c2-4ae2-bd63-f18fecf84c68" Description="Indicates if this field has a unique value when inserting a new data." Name="IsUniqueValue" DisplayName="Is Unique Value" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2c8d58b7-7e75-412c-9fc4-0bed3d83686f" Description="" Name="CustomDataType" DisplayName="Custom Data Type" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="769b18da-42f8-4f36-86ce-c01d009fc080" Description="Controls the decimals of a number by a brand configuration." Name="BrandDecimalsControl" DisplayName="Brand Decimals Control">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8cae7e9c-387c-4bd0-871f-f2deaf915907" Description="Enable audit for this column, which has been changed. IMPORTANT: Use only when Audit Type is set to &quot;Selected Columns&quot; in this Entity" Name="HasAudit" DisplayName="Has Audit" Category="Audit">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="938ed819-55cb-4891-8319-26231de87b3c" Description="" Name="Comment" DisplayName="Comment" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="e831a1ff-90ca-4037-8d30-3c9a5c69f460" Description="" Name="Text" DisplayName="Text" DefaultValue="">
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
        <DomainProperty Id="14431aa2-e274-4644-96e6-5dcddba75966" Description="" Name="Title" DisplayName="Title">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="e746778c-40cb-48c9-b1d8-44c914b801e5" Description="An Operation of a Class." Name="Operation" DisplayName="Operation" InheritanceModifier="Abstract" Namespace="Linx.BusinessModelDesigner">
      <Notes>Abstract base class of ClassOperation and InterfaceOperation.</Notes>
      <BaseClass>
        <DomainClassMoniker Name="ClassModelElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="479fffc5-1251-474c-bd9f-3b215bd4d674" Description="" Name="Concurrency" DisplayName="Concurrency" DefaultValue="Sequential">
          <Type>
            <DomainEnumerationMoniker Name="OperationConcurrency" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f09d089f-92a9-4bc5-82d4-71ed01b5b6b5" Description="Operation description." Name="Comment" DisplayName="Comment">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4cbc775b-98ca-4cf1-b00d-6a606f793c5a" Description="Return Type." Name="ReturnType" DisplayName="Return Type" DefaultValue="void">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="98794b18-2017-480b-9926-980c392f139a" Description="Operation Access." Name="Access" DisplayName="Access" DefaultValue="Public">
          <Notes>Operation Access.</Notes>
          <Type>
            <DomainEnumerationMoniker Name="OperationAccess" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7c2a3633-0ffd-4f8e-9337-81654238c6ba" Description="Custom attribute definitions. All attribute definitions should be separated per # (Attribute1(ParamList2)#Attribute2(ParamList2)#...)." Name="CustomAttributes" DisplayName="Custom Attributes">
          <Notes>Custom attribute definitions. All attribute definitions should be separated per # (Attribute1(ParamList2)#Attribute2(ParamList2)#...).</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a755155c-19a1-4bb8-b586-7ef50247a20b" Description="Parameters Definitions. All parameters should be separated per # (DataType1 ParamName1#DataType2 ParamName2#...). " Name="Parameters" DisplayName="Parameters">
          <Notes>Parameters Definitions. All parameters should be separated per # (DataType1 ParamName1#DataType2 ParamName2#...). </Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5e24cad3-8286-4cff-90be-2fca674b258e" Description="Sets or gets wether or not the item is statically defined." Name="IsStatic" DisplayName="Is Static">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dccc919f-1e12-4820-865f-2a6b8432f2a2" Description="Sets or gets wether or not a function can be overridden." Name="CanOverride" DisplayName="Can Override">
          <Notes>Sets or gets wether or not a function can be overridden.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a9731258-12ed-498a-8ec3-7affdf974893" Description="Overload Name for the operation." Name="OverloadName" DisplayName="Overload Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dcf3b3d8-ea23-499d-98b2-02ca7bd7764f" Description="Documentation for this opation." Name="DocComment" DisplayName="Doc Comment">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c7be3ce3-2987-48fe-8265-8cde3606ae80" Description="Sets or gets wether or not the item is shared." Name="IsShared" DisplayName="Is Shared">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4152ba6c-b283-4d1b-9ec9-4f685fb76a02" Description="Indicates a partial method." Name="IsPartial" DisplayName="Is Partial" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c5c76311-1d5f-46b7-82f9-56ea34131bf1" Description="The operation has only one overload." Name="IsUniqueOverload" DisplayName="Is Unique Overload" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="9d5808dd-b821-4e4b-a9ab-08a8b206f303" Description="" Name="ClassOperation" DisplayName="Class Operation" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="Operation" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="246ee70f-aa41-4525-bc93-1bf8b991a853" Description="" Name="IsAbstract" DisplayName="Is Abstract" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="47281735-fd32-4120-a6b1-0db10c5ff1fc" Description="Implementation Link Reference." Name="InterfaceLinkId" DisplayName="Interface Link Id" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Guid" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="e7973bd8-3e6d-49b2-9f72-f5b6e89f9835" Description="" Name="ModelInterface" DisplayName="Model Interface" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ModelType" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="b8b0a54b-4ac9-4df8-8e67-1606becb6e37" Description="Enable extension for this business object." Name="IsExtension" DisplayName="Is Extension">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="InterfaceOperation" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>InterfaceHasOperation.Operations</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="41e8a710-c854-42b6-a53c-f44109cd5bdc" Description="" Name="InterfaceOperation" DisplayName="Interface Operation" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="Operation" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="612ab83f-ce9d-4c66-a49d-c38fef1f009d" Description="" Name="MultipleAssociation" DisplayName="Multiple Association" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ModelType" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="7ebe900d-7b1d-44ac-beae-118d884c463b" Description="Description for Linx.BusinessModelDesigner.MultipleAssociation.Id Reference" Name="IdReference" DisplayName="Id Reference" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Guid" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="42666e75-6530-45ce-bae1-f5ea6db3289d" Description="" Name="ModelType" DisplayName="Model Type" InheritanceModifier="Abstract" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ClassModelElement" />
      </BaseClass>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Comment" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>CommentReferencesSubjects.Comments</DomainPath>
            <DomainPath>BusinessModelDesignerRootHasTypes.BusinessModelDesignerRoot/!BusinessModelDesignerRoot/BusinessModelDesignerRootHasComments.Comments</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="d8a5773d-5a5e-4b40-a7eb-383c69fc4600" Description="Element with a Description" Name="ClassModelElement" DisplayName="Class Model Element" InheritanceModifier="Abstract" Namespace="Linx.BusinessModelDesigner">
      <Notes>Abstract base of all elements that have a Description property.</Notes>
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="26ec0d9a-ac83-48e3-a6a6-c9f34ddbfbe8" Description="This is a Description." Name="Description" DisplayName="Description" DefaultValue="">
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
    <DomainClass Id="3ada4246-76ac-4e98-b089-6b1628fbd5e9" Description="Description for Linx.BusinessModelDesigner.DomainView" Name="DomainView" DisplayName="Domain View" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ModelType" />
      </BaseClass>
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
    <DomainClass Id="57ece114-c288-4b5b-a6ef-017957247adb" Description="Description for Linx.BusinessModelDesigner.DomainValue" Name="DomainValue" DisplayName="Domain Value" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="4a6a43d3-cf94-44a3-8d21-83dec7c0f555" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3028f23c-bf98-4917-a633-8ffe71f51d27" Description="The domain value." Name="Value" DisplayName="Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b3e75798-66a7-478f-be06-79e1e4564274" Description="Represents the display name." Name="DisplayName" DisplayName="Display Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="b025fb7d-eef1-4dcf-bfb2-0c9884271c75" Description="Description for Linx.BusinessModelDesigner.ModelIndex" Name="ModelIndex" DisplayName="Model Index" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ClassModelElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="3c06d52b-36f1-4255-bad5-81e6ec2be985" Description="e.g.: Column1,Column2 DESC,..ColumnN" Name="Properties" DisplayName="Properties">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="fd553baa-81cf-44cf-b758-56a8e605ed52" Description="" Name="IsUnique" DisplayName="Is Unique">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="055572bd-a50b-4c01-87d8-5bf646bfde93" Description="This index is clustered." Name="IsClustered" DisplayName="Is Clustered">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dba8f783-c343-40c9-a60a-2aa65c329457" Description="Properties Include in index. e.g.: Column1,Column2 DESC,..ColumnN" Name="IncludeProperties" DisplayName="Include Properties">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="17e49fbc-8a07-44da-9904-74715c99ddfb" Description="" Name="StoreScript" DisplayName="Store Script" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="a1614b02-ccdb-45cb-83cd-6c347d8bbb93" Description="Description for Linx.BusinessModelDesigner.StoreScript.Name" Name="Name" DisplayName="Name" IsElementName="true">
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
    <DomainClass Id="559e8a0d-4efd-4d12-b387-d3b177644229" Description="" Name="StoreQuery" DisplayName="Store Query" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="737cd7f9-b560-431d-bdd7-f48c886ad55b" Description="" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6ea24e84-dd13-4bc6-9e97-263c6802cb6b" Description="e.g.: EXEC LX_PROC {0}, {1}, {2}" Name="Command" DisplayName="Command">
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
        <DomainProperty Id="5838900a-c64a-4d9d-8948-26faae47d999" Description="Parameters Definitions. All parameters should be separated per # (DataType1 ParamName1#DataType2 ParamName2#...). " Name="Parameters" DisplayName="Parameters">
          <Notes>Parameters Definitions. All parameters should be separated per # (DataType1 ParamName1#DataType2 ParamName2#...). </Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="394c1e90-2a65-40cb-8f08-10a7fc64b80c" Description="Generic type for returning." Name="GenericType" DisplayName="Generic Type" DefaultValue="int">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="12aa8696-37b9-4514-a2c7-c92dbca73249" Description="Description for Linx.BusinessModelDesigner.DbProvider" Name="DbProvider" DisplayName="Db Provider" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="1495f41d-2818-4ae7-bd27-af7d5b4ef54e" Description="" Name="Server" DisplayName="Server">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ca97123d-40de-4bb8-8b6c-dc790169b554" Description="" Name="Catalog" DisplayName="Catalog">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2cec8ebf-66c2-40d0-9a4c-3fb73fe3768d" Description="" Name="UserId" DisplayName="User Id">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="67b9a421-1f33-4d6c-9314-13b5b7ee35a3" Description="" Name="Password" DisplayName="Password">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e2172b6d-a212-48e6-b88f-3e451accf4ef" Description="" Name="WindowsAuthentication" DisplayName="Windows Authentication">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="527ee0fb-ff95-41ff-8a48-67194c743e1c" Description="" Name="Type" DisplayName="Type" DefaultValue="SQLServer">
          <Type>
            <DomainEnumerationMoniker Name="Provider" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c9b63fb2-5f13-41da-bbea-a9780491102d" Description="Default provider for generating all code." Name="IsDefault" DisplayName="Is Default">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f33aaaf4-3984-4623-b6e7-f3f6c956cd99" Description="The connection name for this provider." Name="ConnectionName" DisplayName="Connection Name" DefaultValue="Type here the connection name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f68bf267-73ad-4534-bba8-5db3e38db9cc" Description="Enable SQL Migration" Name="EnableMigration" DisplayName="Enable Migration" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="188e94da-b2f2-4762-be92-bdf82dbf45e2" Description="Object representing a reference to a Model Class" Name="ReferenceModelClass" DisplayName="Reference Model Class" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ModelClass" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="f5752428-2503-4da6-9aab-01c38047c2c8" Description="ModelBusReference on a Model Class" Name="ModelClassReference" DisplayName="Model Class Reference" IsBrowsable="false">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.TypeConverter">
              <Parameters>
                <AttributeParameter Value="typeof(Microsoft.VisualStudio.Modeling.Integration.ModelBusReferenceTypeConverter)" />
              </Parameters>
            </ClrAttribute>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(Microsoft.VisualStudio.Modeling.Integration.Picker.ModelElementReferenceEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
            <ClrAttribute Name="Microsoft.VisualStudio.Modeling.Integration.Picker.SupplyFileBasedBrowserConfiguration">
              <Parameters>
                <AttributeParameter Value="&quot;Please choose a business model file&quot;" />
                <AttributeParameter Value="&quot;Business Model files|*.bmd&quot;" />
              </Parameters>
            </ClrAttribute>
            <ClrAttribute Name="Microsoft.VisualStudio.Modeling.Integration.Picker.ApplyElementTypeLimitations">
              <Parameters>
                <AttributeParameter Value="typeof(BusinessModelDesignerRoot)" />
                <AttributeParameter Value="typeof(ModelClass)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/Microsoft.VisualStudio.Modeling.Integration/ModelBusReference" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="aaf3144c-b5ca-4171-847c-cb9b3734f06a" Description="" Name="ReferenceInfo" DisplayName="Reference Info" Kind="Calculated" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="81f632f1-2954-4876-a7ec-69448cea2b28" Description="" Name="HasReferenceError" DisplayName="Has Reference Error" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c6738dca-8e7d-443c-90c5-1dcc6a1f0f78" Description="" Name="ReferenceProjectInfo" DisplayName="Reference Project Info" Kind="Calculated" IsBrowsable="false" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="41174f3e-6ea2-4c8d-9c07-600bf3d4adb8" Description="Description for Linx.BusinessModelDesigner.RouteMapData" Name="RouteMapData" DisplayName="Route Map Data" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ClassModelElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="3e086110-a3fa-428e-824e-5ca2831979ad" Description="Path for reaching the data." Name="Path" DisplayName="Path">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="e498ba51-c414-48f0-a265-8a1d6afbcbe2" Description="" Name="ModelImplementation" DisplayName="Model Implementation" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="d82db880-06be-441d-ba7c-289b480adf4c" Description="The environment creates or uses the project with this suffix." Name="ProjectSuffix" DisplayName="Project Suffix" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c831ba1a-3356-454d-b2e2-6ba9f83a702e" Description="" Name="IsSelected" DisplayName="Is Selected" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="df4acd6b-33f2-46d1-9a16-18871bd15224" Description="Description for Linx.BusinessModelDesigner.WebApiController" Name="WebApiController" DisplayName="Web Api Controller" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="0a156881-5030-4530-b6e5-e29ddb4d79c6" Description="Route prefix for all actions from this controller." Name="RoutePrefix" DisplayName="Route Prefix" DefaultValue="{Name}">
          <Notes>Route prefix for all actions from this controller.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="72e0655a-a8f3-424a-896e-ec5ea3a2824c" Description="The environment creates or uses the project with this suffix." Name="ProjectSuffix" DisplayName="Project Suffix" DefaultValue="">
          <Notes>The environment creates or uses the project with this suffix.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c57d16bd-9793-49fe-b9c2-5e88db6d8d2d" Description="Description for Linx.BusinessModelDesigner.WebApiController.Expose All Context" Name="ExposeAllContext" DisplayName="Expose All Context">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0fb74e9f-a54e-4ee3-b002-81ca2538e502" Description="Description for Linx.BusinessModelDesigner.WebApiController.Is Asp Net Core" Name="IsAspNetCore" DisplayName="Is Asp Net Core" Kind="Calculated" IsBrowsable="false">
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
    <DomainClass Id="d845d1c9-eb8f-417c-bed0-c68fd130fe43" Description="" Name="WebApiAction" DisplayName="Web Api Action" Namespace="Linx.BusinessModelDesigner">
      <BaseClass>
        <DomainClassMoniker Name="Operation" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="d8d1a7c9-6124-42b3-a6db-dd46a8161690" Description="Verb definition for this method." Name="HttpVerb" DisplayName="Http Verb" DefaultValue="GET">
          <Notes>Verb definition for this method.</Notes>
          <Type>
            <DomainEnumerationMoniker Name="HttpRouteAttribute" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7ce55c12-b1dd-4619-97ce-bbc407b7e9b9" Description="Custom routes. All routes should be separated by # (i.g: ./Route1#./Route2#...#./RouteN). Where &quot;.&quot; will be replaced by RouteActionName." Name="CustomRoutes" DisplayName="Custom Routes">
          <Notes>Custom routes. All routes should be separated by # (i.g: ./Route1#./Route2#...#./RouteN). Where "." will be replaced by RouteActionName.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f0ea25a8-a967-416b-beef-34388b4dd3c3" Description="This name participates of route. Use &quot;.&quot; for setting the method name." Name="RouteActionName" DisplayName="Route Action Name" DefaultValue=".">
          <Notes>This name participates of route. Use "." for setting the method name.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="313aa6fa-2a90-4cbd-9f59-38f7d6a5384e" Description="Enable generation of routes for all parameters." Name="EnableRoutesForParameters" DisplayName="Enable Routes For Parameters">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="9d0398c9-4b05-465c-8e5e-67cd3607062e" Description="" Name="ClassHasAttributes" DisplayName="Class Has Attributes" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="3548412a-153d-44c4-9cc7-f1422e7482c9" Description="" Name="ModelClass" DisplayName="Model Class" PropertyName="Attributes" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Attributes">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="43ef91ac-ab09-44a0-a725-a3565869f000" Description="" Name="Attribute" DisplayName="Attribute" PropertyName="ModelClass" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Model Class">
          <RolePlayer>
            <DomainClassMoniker Name="ModelAttribute" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="ef58e100-a1b3-4995-9b6f-b069fdbad275" Description="" Name="BusinessModelDesignerRootHasComments" DisplayName="Business Model Designer Root Has Comments" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="9ca6164a-aad6-40be-ad21-a4a17530c1db" Description="" Name="BusinessModelDesignerRoot" DisplayName="Business Model Designer Root" PropertyName="Comments" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="BusinessModelDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="553b61e8-4705-4590-ad27-097963d2be32" Description="" Name="Comment" DisplayName="Comment" PropertyName="BusinessModelDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Business Model Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="8afbd0c6-3036-415d-bf54-47a778446ae6" Description="" Name="ClassHasOperations" DisplayName="Class Has Operations" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="34ca7b63-3333-4bb0-a9ba-58ff384f7ac1" Description="" Name="ModelClass" DisplayName="ModelClass" PropertyName="Operations" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Operations">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="82a6e40a-2da7-4fd9-8ba0-c0d6d633c499" Description="" Name="Operation" DisplayName="Operation" PropertyName="ModelClass" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Model Class">
          <RolePlayer>
            <DomainClassMoniker Name="ClassOperation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="dc8cf97b-a9cf-438a-9612-a58434480f39" Description="Inheritance between Classes." Name="Generalization" DisplayName="Generalization" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="48ac04f7-5c63-4f64-b9e2-ca9cf5233e5f" Description="" Name="Description" DisplayName="Description">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="df114178-c45b-4617-b771-946109064490" Description="" Name="Superclass" DisplayName="Superclass" PropertyName="Subclasses" PropertyDisplayName="Subclasses">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="34c8f852-33a9-4ca4-b609-d822e531406d" Description="" Name="Subclass" DisplayName="Subclass" PropertyName="Superclass" Multiplicity="ZeroOne" PropertyDisplayName="Superclass">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2a4fb48d-74b5-4e0d-801f-079489ebc09d" Description="" Name="InterfaceHasOperation" DisplayName="Interface Has Operation" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="f8d5564e-8ae6-4336-93a2-f7c8bd0695c4" Description="" Name="Interface" DisplayName="Interface" PropertyName="Operations" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Operations">
          <RolePlayer>
            <DomainClassMoniker Name="ModelInterface" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="dcdbd634-cd7c-471c-ac08-a8e18b0b892a" Description="" Name="Operation" DisplayName="Operation" PropertyName="Interface" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Interface">
          <RolePlayer>
            <DomainClassMoniker Name="InterfaceOperation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="81337f8a-2ff9-4521-92c1-43c673e5f7bb" Description="Links a MultipleAssociation to one of the classes it associates." Name="MultipleAssociationOrigin" DisplayName="Multiple Association Origin" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="b9e34a03-0217-4f83-a235-8ceffe9fdbc4" Description="" Name="Multiplicity" DisplayName="Multiplicity" DefaultValue="Many" IsUIReadOnly="true">
          <Type>
            <DomainEnumerationMoniker Name="Multiplicity" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0615d472-716f-4fae-a681-a1fbc62288b9" Description="" Name="CollectionName" DisplayName="Collection Name" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="14b9fd0b-b8d7-402c-a9b3-991c9f017ee8" Description="Store foreign key name." Name="ForeignKeyConstraintName" DisplayName="Foreign Key Constraint Name" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e1f29a95-a18d-4ed9-a2f8-09a439704bdd" Description="" Name="WillCascadeOnDelete" DisplayName="Will Cascade On Delete">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e460ebb8-9031-4f58-820c-f62aa2356b5e" Description="" Name="Description" DisplayName="Description">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="fe247ff7-0646-47bc-8574-b7a671fd8d4b" Description="" Name="MultipleAssociation" DisplayName="Multiple Association" PropertyName="OriginTypes" PropertyDisplayName="Origin Types">
          <RolePlayer>
            <DomainClassMoniker Name="MultipleAssociation" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="685df7e8-055e-4a41-91e7-b66a6f04a4c3" Description="" Name="OriginType" DisplayName="Origin Type" PropertyName="MultipleAssociations" PropertyDisplayName="Multiple Associations">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="fc708139-ed93-4d67-950e-3528a5300d00" Description="" Name="BusinessModelDesignerRootHasTypes" DisplayName="Business Model Designer Root Has Types" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="9b48591c-5ceb-467a-a16e-1bf26267f773" Description="" Name="BusinessModelDesignerRoot" DisplayName="Business Model Designer Root" PropertyName="Types" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Types">
          <RolePlayer>
            <DomainClassMoniker Name="BusinessModelDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="d18f574c-6656-44ae-9271-06fedcc54ca7" Description="" Name="Type" DisplayName="Type" PropertyName="BusinessModelDesignerRoot" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="">
          <RolePlayer>
            <DomainClassMoniker Name="ModelType" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="9b7fa7ec-bb7b-4579-aeb4-a29496aa188a" Description="" Name="CommentReferencesSubjects" DisplayName="Comment References Subjects" Namespace="Linx.BusinessModelDesigner">
      <Source>
        <DomainRole Id="9c6477b6-c488-4375-8c05-3282aa1c496c" Description="" Name="Comment" DisplayName="Comment" PropertyName="Subjects" PropertyDisplayName="Subjects">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="7b978a01-eb42-4e79-ad91-819f1e2ef65f" Description="" Name="Subject" DisplayName="Subject" PropertyName="Comments" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="ModelType" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="e156e923-f115-40e3-8bc8-28b39b14dd9a" Description="" Name="DomainViewHasDomainValues" DisplayName="Domain View Has Domain Values" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="b9ab0174-a765-40ec-9705-15afc6475c12" Description="Description for Linx.BusinessModelDesigner.DomainViewHasDomainValues.DomainView" Name="DomainView" DisplayName="Domain View" PropertyName="DomainValues" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Domain Values">
          <RolePlayer>
            <DomainClassMoniker Name="DomainView" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3997aa8e-941a-4cc6-b397-510e697b4dc5" Description="Description for Linx.BusinessModelDesigner.DomainViewHasDomainValues.DomainValue" Name="DomainValue" DisplayName="Domain Value" PropertyName="DomainView" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Domain View">
          <RolePlayer>
            <DomainClassMoniker Name="DomainValue" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="d9706034-d9ea-4ec0-b8f3-ed1caa68b944" Description="Description for Linx.BusinessModelDesigner.ClassHasIndexes" Name="ClassHasIndexes" DisplayName="Class Has Indexes" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="daa14897-f288-4fe9-8c15-3fa1cb665cb6" Description="Description for Linx.BusinessModelDesigner.ClassHasIndexes.ModelClass" Name="ModelClass" DisplayName="Model Class" PropertyName="ModelIndexes" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Model Indexes">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c56f14eb-e09b-4ac0-9e9b-ab4bd381bcf9" Description="Description for Linx.BusinessModelDesigner.ClassHasIndexes.ModelIndex" Name="ModelIndex" DisplayName="Model Index" PropertyName="ModelClass" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Model Class">
          <RolePlayer>
            <DomainClassMoniker Name="ModelIndex" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="0dd70b88-84bb-4b1d-81a5-bc2f3e8fc38a" Description="" Name="Association" DisplayName="Association" Namespace="Linx.BusinessModelDesigner" AllowsDuplicates="true">
      <Properties>
        <DomainProperty Id="31e60918-a16e-4257-aae9-f6c10c847d6e" Description="" Name="SourceMultiplicity" DisplayName="Source Multiplicity" DefaultValue="One" IsUIReadOnly="true">
          <Type>
            <DomainEnumerationMoniker Name="SourceMultiplicity" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c0905acf-bb90-4b90-b36f-04ab6a6f0bcf" Description="" Name="TargetMultiplicity" DisplayName="Target Multiplicity" DefaultValue="Many">
          <Type>
            <DomainEnumerationMoniker Name="Multiplicity" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="72af37c3-1e3b-4c51-a8c6-b25db17cecf7" Description="Property name of relation." Name="SourcePropertyNameToTarget" DisplayName="Navigation Name To Target" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="12a22e25-7a71-4c69-96c0-07162cbd358b" Description="Property name of relation." Name="TargetPropertyNameToSource" DisplayName="Navigation Name To Source" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8e8a7a23-6122-4e6d-8cbb-10c33694a814" Description="Store foreign key constraint name." Name="ForeignKeyConstraintName" DisplayName="Foreign Key Constraint Name" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="02542336-9352-46ff-a41e-4a7abbcd014d" Description="Description for Linx.BusinessModelDesigner.Association.Id Reference" Name="IdReference" DisplayName="Id Reference" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Guid" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="161a9b39-5161-40dc-8d5d-6d1fe5ba0ad2" Description="" Name="WillCascadeOnDelete" DisplayName="Will Cascade On Delete">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="28a24e71-d7b7-4c06-8313-1a7c9c7c4b31" Description="" Name="Description" DisplayName="Description">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8ce1f36c-7acd-476e-9c5a-5715cf9e6bf6" Description="Property name of relation." Name="SourcePropertyNameToTargetInfo" DisplayName="Navigation Name To Target" DefaultValue="" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7dab1783-59d9-4320-a142-db89b2dfb9c6" Description="Do not generate index for this foreign key." Name="RemoveAutomaticIndex" DisplayName="Remove Automatic Index" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="a6f050e7-77c8-4ffc-90c8-b885a3c38781" Description="Description for Linx.BusinessModelDesigner.Association.SourceModelClass" Name="SourceModelClass" DisplayName="Source Model Class" PropertyName="TargetModelClasses" PropertyDisplayName="Target Model Classes">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="32bbeed1-e420-4df6-b649-745a60d64934" Description="Description for Linx.BusinessModelDesigner.Association.TargetModelClass" Name="TargetModelClass" DisplayName="Target Model Class" PropertyName="SourceModelClasses" PropertyDisplayName="Source Model Classes">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="631eafe3-222d-4fef-947d-688c84c5cf86" Description="Identifies a MultipleAssociation with a Class, so that it can have attributes." Name="MultipleAssociationTarget" DisplayName="Multiple Association Target" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="17aa81f9-2879-4123-9cd8-af35978d763e" Description="" Name="Description" DisplayName="Description">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="9e345cda-caca-4001-bf12-c5fd52bbfc4f" Description="Description for Linx.BusinessModelDesigner.MultipleAssociationTarget.MultipleAssociation" Name="MultipleAssociation" DisplayName="Multiple Association" PropertyName="TargetType" Multiplicity="ZeroOne" PropertyDisplayName="Target Type">
          <RolePlayer>
            <DomainClassMoniker Name="MultipleAssociation" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="10f1e04e-1fbc-4554-b7b9-a50fa7a287de" Description="Description for Linx.BusinessModelDesigner.MultipleAssociationTarget.TargetType" Name="TargetType" DisplayName="Target Type" PropertyName="MultipleAssociation" Multiplicity="ZeroOne" PropertyDisplayName="Multiple Association">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="67ee3c0b-b451-4e9a-b95a-981da106884e" Description="Description for Linx.BusinessModelDesigner.StoreScriptHasStoreQueries" Name="StoreScriptHasStoreQueries" DisplayName="Store Script Has Store Queries" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="ec082ffb-c219-4f7f-81a2-c88a1e79c59e" Description="Description for Linx.BusinessModelDesigner.StoreScriptHasStoreQueries.StoreScript" Name="StoreScript" DisplayName="Store Script" PropertyName="StoreQueries" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Store Queries">
          <RolePlayer>
            <DomainClassMoniker Name="StoreScript" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a53ce982-29b3-429e-bf5e-dbe539d20f7f" Description="Description for Linx.BusinessModelDesigner.StoreScriptHasStoreQueries.StoreQuery" Name="StoreQuery" DisplayName="Store Query" PropertyName="StoreScript" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Store Script">
          <RolePlayer>
            <DomainClassMoniker Name="StoreQuery" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="4f157daa-115d-4714-bb97-41eb124de91b" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasStoreScripts" Name="BusinessModelDesignerRootHasStoreScripts" DisplayName="Business Model Designer Root Has Store Scripts" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="e895c368-e65d-4ac4-b165-0fef396b9ad9" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasStoreScripts.BusinessModelDesignerRoot" Name="BusinessModelDesignerRoot" DisplayName="Business Model Designer Root" PropertyName="StoreScripts" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Store Scripts">
          <RolePlayer>
            <DomainClassMoniker Name="BusinessModelDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="05f23d40-adab-47d2-892d-f582606fda08" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasStoreScripts.StoreScript" Name="StoreScript" DisplayName="Store Script" PropertyName="BusinessModelDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Business Model Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="StoreScript" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="40a05b7b-22c3-4f1a-8d39-efd463b60709" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasDbProviders" Name="BusinessModelDesignerRootHasDbProviders" DisplayName="Business Model Designer Root Has Db Providers" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="53c89ed5-0c2c-4dca-b52f-34106cbe549b" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasDbProviders.BusinessModelDesignerRoot" Name="BusinessModelDesignerRoot" DisplayName="Business Model Designer Root" PropertyName="DbProviders" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Db Providers">
          <RolePlayer>
            <DomainClassMoniker Name="BusinessModelDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a9435eed-17b5-484e-9cc9-2623f1b958dd" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasDbProviders.DbProvider" Name="DbProvider" DisplayName="Db Provider" PropertyName="BusinessModelDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Business Model Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="DbProvider" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="425736b3-a1e3-4e7e-aaa9-99b7d8a0182e" Description="Description for Linx.BusinessModelDesigner.GeneralizationSh" Name="GeneralizationSh" DisplayName="Generalization Sh" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="3620c16e-85dc-4c17-a282-e92208c44c52" Description="" Name="Discriminator" DisplayName="Discriminator" DefaultValue="PropertyName=Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="19d1df9d-d37e-4d8c-9a0a-24c102786d09" Description="" Name="Description" DisplayName="Description">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="82b19e8d-c27a-4ff1-9f74-f6b09408dd70" Description="Description for Linx.BusinessModelDesigner.GeneralizationSh.SuperclassSh" Name="SuperclassSh" DisplayName="Superclass Sh" PropertyName="SubclassesSh" PropertyDisplayName="Subclasses Sh">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="e1aa7b2d-eb1a-4863-bf1f-3be8ad29e6f4" Description="Description for Linx.BusinessModelDesigner.GeneralizationSh.SubclassSh" Name="SubclassSh" DisplayName="Subclass Sh" PropertyName="SuperclassSh" Multiplicity="ZeroOne" PropertyDisplayName="Superclass Sh">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="181c2432-99a1-4cc0-950c-6a478cca148b" Description="Description for Linx.BusinessModelDesigner.ClassHasDataRoutes" Name="ClassHasDataRoutes" DisplayName="Class Has Data Routes" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="7579d6fb-c78c-4480-8af9-30505a704735" Description="Description for Linx.BusinessModelDesigner.ClassHasDataRoutes.ModelClass" Name="ModelClass" DisplayName="Model Class" PropertyName="RouteMapDatum" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Route Map Datum">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="da4ab591-1d2a-462a-864c-2d495a9f10e9" Description="Description for Linx.BusinessModelDesigner.ClassHasDataRoutes.RouteMapData" Name="RouteMapData" DisplayName="Route Map Data" PropertyName="ModelClass" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Model Class">
          <RolePlayer>
            <DomainClassMoniker Name="RouteMapData" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f4819781-c32c-4fbe-81ca-ff67f1937544" Description="Description for Linx.BusinessModelDesigner.ModelImplementationReferencesModelInterface" Name="ModelImplementationReferencesModelInterface" DisplayName="Model Implementation References Model Interface" Namespace="Linx.BusinessModelDesigner">
      <Source>
        <DomainRole Id="241df608-4885-4a53-a42a-05f3a56c4980" Description="Description for Linx.BusinessModelDesigner.ModelImplementationReferencesModelInterface.ModelImplementation" Name="ModelImplementation" DisplayName="Model Implementation" PropertyName="ModelInterface" Multiplicity="ZeroOne" PropertyDisplayName="Model Interface">
          <RolePlayer>
            <DomainClassMoniker Name="ModelImplementation" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a2b267c9-354f-4092-a62e-63bd2c538c4c" Description="Description for Linx.BusinessModelDesigner.ModelImplementationReferencesModelInterface.ModelInterface" Name="ModelInterface" DisplayName="Model Interface" PropertyName="ModelImplementations" PropertyDisplayName="Model Implementations">
          <RolePlayer>
            <DomainClassMoniker Name="ModelInterface" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="1ca384e2-cc53-407d-98f4-16ceff585359" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasModelImplementations" Name="BusinessModelDesignerRootHasModelImplementations" DisplayName="Business Model Designer Root Has Model Implementations" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="684ec06a-89a1-4c37-8274-efb41234e650" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasModelImplementations.BusinessModelDesignerRoot" Name="BusinessModelDesignerRoot" DisplayName="Business Model Designer Root" PropertyName="ModelImplementations" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Model Implementations">
          <RolePlayer>
            <DomainClassMoniker Name="BusinessModelDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="6731c180-e606-4341-9eda-fc0fc96388b4" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasModelImplementations.ModelImplementation" Name="ModelImplementation" DisplayName="Model Implementation" PropertyName="BusinessModelDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Business Model Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="ModelImplementation" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="8bc45bf6-39dd-47f4-8fd4-91371a5fe444" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasWebApiControllers" Name="BusinessModelDesignerRootHasWebApiControllers" DisplayName="Business Model Designer Root Has Web Api Controllers" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="07a7bbdc-eac8-4779-aece-46ad877e5066" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasWebApiControllers.BusinessModelDesignerRoot" Name="BusinessModelDesignerRoot" DisplayName="Business Model Designer Root" PropertyName="WebApiControllers" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Web Api Controllers">
          <RolePlayer>
            <DomainClassMoniker Name="BusinessModelDesignerRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="d106bd47-eec7-4a80-8200-64d370316705" Description="Description for Linx.BusinessModelDesigner.BusinessModelDesignerRootHasWebApiControllers.WebApiController" Name="WebApiController" DisplayName="Web Api Controller" PropertyName="BusinessModelDesignerRoot" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Business Model Designer Root">
          <RolePlayer>
            <DomainClassMoniker Name="WebApiController" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2db910d9-6a40-40e5-b22b-257a25ca6bce" Description="Description for Linx.BusinessModelDesigner.WebApiControllerHasWebApiActions" Name="WebApiControllerHasWebApiActions" DisplayName="Web Api Controller Has Web Api Actions" Namespace="Linx.BusinessModelDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="b6c92c75-d105-448d-ab8d-86dbb58fd003" Description="Description for Linx.BusinessModelDesigner.WebApiControllerHasWebApiActions.WebApiController" Name="WebApiController" DisplayName="Web Api Controller" PropertyName="WebApiActions" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Web Api Actions">
          <RolePlayer>
            <DomainClassMoniker Name="WebApiController" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9d725b76-d0f7-4060-8bae-f30703a5aa93" Description="Description for Linx.BusinessModelDesigner.WebApiControllerHasWebApiActions.WebApiAction" Name="WebApiAction" DisplayName="Web Api Action" PropertyName="WebApiController" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Web Api Controller">
          <RolePlayer>
            <DomainClassMoniker Name="WebApiAction" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="428c2368-92da-4c34-8a71-e0409e3907c2" Description="Description for Linx.BusinessModelDesigner.ModelViewAssociation" Name="ModelViewAssociation" DisplayName="Model View Association" Namespace="Linx.BusinessModelDesigner">
      <Properties>
        <DomainProperty Id="18677225-5b2c-4741-8e6b-2d25aa954b8f" Description="" Name="CollectionName" DisplayName="Collection Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="398f60b1-abbe-4ae6-bda4-6d748aa0aa5b" Description="Description for Linx.BusinessModelDesigner.ModelViewAssociation.SourceModelClass" Name="SourceModelClass" DisplayName="Source Model Class" PropertyName="ModelViews" PropertyDisplayName="Model Views">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="6c852a96-05b4-4e40-ae60-8d94d57e34c3" Description="Description for Linx.BusinessModelDesigner.ModelViewAssociation.TargetModelClass" Name="TargetModelClass" DisplayName="Target Model Class" PropertyName="ModelView" Multiplicity="ZeroOne" PropertyDisplayName="Model View">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
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
    <DomainEnumeration Name="AccessModifier" Namespace="Linx.BusinessModelDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="Public" Value="0" />
        <EnumerationLiteral Description="" Name="Assembly" Value="1" />
        <EnumerationLiteral Description="" Name="Private" Value="2" />
        <EnumerationLiteral Description="" Name="Family" Value="3" />
        <EnumerationLiteral Description="" Name="FamilyOrAssembly" Value="4" />
        <EnumerationLiteral Description="" Name="FamilyAndAssembly" Value="5" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="TypeAccessModifier" Namespace="Linx.BusinessModelDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="Public" Value="0" />
        <EnumerationLiteral Description="" Name="Private" Value="1" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="InheritanceModifier" Namespace="Linx.BusinessModelDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="None" Value="0" />
        <EnumerationLiteral Description="" Name="Abstract" Value="1" />
        <EnumerationLiteral Description="" Name="Sealed" Value="2" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="Multiplicity" Namespace="Linx.BusinessModelDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="One" Value="1" />
        <EnumerationLiteral Description="" Name="ZeroOne" Value="2" />
        <EnumerationLiteral Description="" Name="Many" Value="3" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.Multiplicity.ZeroMany" Name="ZeroMany" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="OperationConcurrency" Namespace="Linx.BusinessModelDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="Sequential" Value="0" />
        <EnumerationLiteral Description="" Name="Guarded" Value="1" />
        <EnumerationLiteral Description="" Name="Concurrent" Value="2" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="CommandType" Namespace="Linx.BusinessModelDesigner" Description="Description for Linx.BusinessModelDesigner.CommandType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.CommandType.StoredProcedure" Name="StoredProcedure" Value="1" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.CommandType.CommandText" Name="CommandText" Value="0" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="OperationAccess" Namespace="Linx.BusinessModelDesigner" Description="Operation access.">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OperationAccess.AssemblyOrFamily" Name="AssemblyOrFamily" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OperationAccess.Default" Name="Default" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OperationAccess.Private" Name="Private" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OperationAccess.Project" Name="Project" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OperationAccess.ProjectOrProtected" Name="ProjectOrProtected" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OperationAccess.Protected" Name="Protected" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OperationAccess.Public" Name="Public" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OperationAccess.WithEvents" Name="WithEvents" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="ClassKind" Namespace="Linx.BusinessModelDesigner" Description="Description for Linx.BusinessModelDesigner.ClassKind">
      <Literals>
        <EnumerationLiteral Description="" Name="Table" Value="" />
        <EnumerationLiteral Description="" Name="DatabaseView" Value="" />
        <EnumerationLiteral Description="" Name="Multidimensional" Value="" />
        <EnumerationLiteral Description="" Name="ModelView" Value="" />
        <EnumerationLiteral Description="" Name="DatabaseScript" Value="" />
      </Literals>
    </DomainEnumeration>
    <ExternalType Name="Color" Namespace="System.Drawing" />
    <ExternalType Name="DashStyle" Namespace="System.Drawing.Drawing2D" />
    <ExternalType Name="LinearGradientMode" Namespace="System.Drawing.Drawing2D" />
    <DomainEnumeration Name="EntityQueryReturnType" Namespace="Linx.BusinessModelDesigner" Description="Description for Linx.BusinessModelDesigner.EntityQueryReturnType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.EntityQueryReturnType.IQueryable" Name="IQueryable" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.EntityQueryReturnType.IEnumerable" Name="IEnumerable" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="SourceMultiplicity" Namespace="Linx.BusinessModelDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="One" Value="1" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="Provider" Namespace="Linx.BusinessModelDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="Microsoft SQL Server  Database." Name="SQLServer" Value="" />
        <EnumerationLiteral Description="Oracle Database." Name="Oracle" Value="" />
        <EnumerationLiteral Description="SQLite  Database." Name="SQLite" Value="" />
        <EnumerationLiteral Description="MySQL Database." Name="MySQL" Value="" />
        <EnumerationLiteral Description="PostgreDB  Database." Name="PostgreSQL" Value="" />
        <EnumerationLiteral Description="Document Oriented Database." Name="MongoDB" Value="" />
        <EnumerationLiteral Description=" Fully managed proprietary NoSQL database services." Name="DynamoDB" Value="" />
      </Literals>
    </DomainEnumeration>
    <ExternalType Name="ModelBusReference" Namespace="Microsoft.VisualStudio.Modeling.Integration" />
    <DomainEnumeration Name="ModelDataType" Namespace="Linx.BusinessModelDesigner" Description="Description for Linx.BusinessModelDesigner.ModelDataType">
      <Literals>
        <EnumerationLiteral Description="" Name="Byte" Value="" />
        <EnumerationLiteral Description="" Name="SignedByte" Value="" />
        <EnumerationLiteral Description="" Name="UnsignedShort" Value="" />
        <EnumerationLiteral Description="" Name="Int" Value="" />
        <EnumerationLiteral Description="" Name="UnsignedInt" Value="" />
        <EnumerationLiteral Description="" Name="Long" Value="" />
        <EnumerationLiteral Description="" Name="UnsignedLong" Value="" />
        <EnumerationLiteral Description="" Name="Float" Value="" />
        <EnumerationLiteral Description="" Name="Double" Value="" />
        <EnumerationLiteral Description="" Name="Short" Value="" />
        <EnumerationLiteral Description="" Name="String" Value="" />
        <EnumerationLiteral Description="" Name="Decimal" Value="" />
        <EnumerationLiteral Description="" Name="Boolean" Value="" />
        <EnumerationLiteral Description="" Name="DateTime" Value="" />
        <EnumerationLiteral Description="" Name="StringChar" Value="" />
        <EnumerationLiteral Description="" Name="Guid" Value="" />
        <EnumerationLiteral Description="" Name="ByteArray" Value="" />
        <EnumerationLiteral Description="" Name="StringText" Value="" />
        <EnumerationLiteral Description="" Name="Timestamp" Value="" />
        <EnumerationLiteral Description="" Name="DateTimeOffset" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.ModelDataType.Date" Name="Date" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="HttpRouteAttribute" Namespace="Linx.BusinessModelDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="GET" Value="" />
        <EnumerationLiteral Description="" Name="POST" Value="" />
        <EnumerationLiteral Description="" Name="PUT" Value="" />
        <EnumerationLiteral Description="" Name="DELETE" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="AggregationFunctions" Namespace="Linx.BusinessModelDesigner" Description="Aggregation Functions">
      <Literals>
        <EnumerationLiteral Description="Average" Name="Average" Value="5" />
        <EnumerationLiteral Description="Count" Name="Count" Value="4" />
        <EnumerationLiteral Description="Max" Name="Max" Value="3" />
        <EnumerationLiteral Description="Min" Name="Min" Value="2" />
        <EnumerationLiteral Description="None" Name="None" Value="0" />
        <EnumerationLiteral Description="Sum" Name="Sum" Value="1" />
        <EnumerationLiteral Description="CountDistinct" Name="CountDistinct" Value="6" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="OrderByOrientationType" Namespace="Linx.BusinessModelDesigner" Description="Description for Linx.BusinessModelDesigner.OrderByOrientationType">
      <Literals>
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OrderByOrientationType.Ascending" Name="Ascending" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.OrderByOrientationType.Descending" Name="Descending" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="ModelDataType1" Namespace="Linx.BusinessModelDesigner" Description="Description for Linx.BusinessModelDesigner.ModelDataType1">
      <Literals>
        <EnumerationLiteral Description="" Name="Byte" Value="" />
        <EnumerationLiteral Description="" Name="SignedByte" Value="" />
        <EnumerationLiteral Description="" Name="UnsignedShort" Value="" />
        <EnumerationLiteral Description="" Name="Int" Value="" />
        <EnumerationLiteral Description="" Name="UnsignedInt" Value="" />
        <EnumerationLiteral Description="" Name="Long" Value="" />
        <EnumerationLiteral Description="" Name="UnsignedLong" Value="" />
        <EnumerationLiteral Description="" Name="Float" Value="" />
        <EnumerationLiteral Description="" Name="Double" Value="" />
        <EnumerationLiteral Description="" Name="Short" Value="" />
        <EnumerationLiteral Description="" Name="String" Value="" />
        <EnumerationLiteral Description="" Name="Decimal" Value="" />
        <EnumerationLiteral Description="" Name="Boolean" Value="" />
        <EnumerationLiteral Description="" Name="DateTime" Value="" />
        <EnumerationLiteral Description="" Name="StringChar" Value="" />
        <EnumerationLiteral Description="" Name="Guid" Value="" />
        <EnumerationLiteral Description="" Name="ByteArray" Value="" />
        <EnumerationLiteral Description="" Name="StringText" Value="" />
        <EnumerationLiteral Description="" Name="Timestamp" Value="" />
        <EnumerationLiteral Description="" Name="DateTimeOffset" Value="" />
        <EnumerationLiteral Description="Description for Linx.BusinessModelDesigner.ModelDataType1.Date" Name="Date" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="AuditType" Namespace="Linx.BusinessModelDesigner" Description="Audit Type">
      <Literals>
        <EnumerationLiteral Description="a JSON will be saved with the serialized object, before and after the changed, which has been changed" Name="Entity" Value="0" />
        <EnumerationLiteral Description="Will be saved column by column, and all columns should be monitored, which has been changed" Name="AllColumns" Value="1" />
        <EnumerationLiteral Description="Only the marked columns will be saved, which has been changed" Name="SelectedColumns" Value="2" />
      </Literals>
    </DomainEnumeration>
  </Types>
  <Shapes>
    <CompartmentShape Id="fd6c9e4c-5f8a-4c01-910d-6ed74b07912e" Description="" Name="ClassShape" DisplayName="Class Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Class Shape" ExposesTextColor="true" FillColor="211, 220, 239" InitialWidth="2.5" InitialHeight="0.3" OutlineThickness="0.01" FillGradientMode="None" ExposesOutlineColorAsProperty="true" ExposesFillColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" ExposesOutlineThicknessAsProperty="true" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="fa7fd60a-e506-4e63-b729-cc278781ea40" Description="Description for Linx.BusinessModelDesigner.ClassShape.Fill Color" Name="FillColor" DisplayName="Fill Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2fbc746c-98b6-4ba7-8bf2-3dd7f5fc0063" Description="Description for Linx.BusinessModelDesigner.ClassShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="973131a0-acbc-43a2-bd19-6081d375092a" Description="Description for Linx.BusinessModelDesigner.ClassShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9decc1de-bcbf-4f8d-b50e-fa78ba27525e" Description="Description for Linx.BusinessModelDesigner.ClassShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4b5964c8-70b8-45ed-b335-7b228ea43461" Description="Description for Linx.BusinessModelDesigner.ClassShape.Outline Thickness" Name="OutlineThickness" DisplayName="Outline Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="ModelClassMark" DisplayName="Model Class Mark" DefaultIcon="Resources\ClassTool.bmp" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopRight" HorizontalOffset="-0.4" VerticalOffset="0">
        <TextDecorator Name="ModifiertMark" DisplayName="Modifiert Mark" DefaultText="ModifiertMark" FontStyle="Bold, Underline" FontSize="10" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="NoMapMark" DisplayName="No Map Mark" DefaultIcon="Resources\NoDataMap.PNG" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="-0.2" VerticalOffset="0">
        <IconDecorator Name="ViewMark" DisplayName="View Mark" DefaultIcon="Resources\DbViewMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="-0.2" VerticalOffset="0">
        <IconDecorator Name="NoLinksMark" DisplayName="No Links Mark" DefaultIcon="Resources\NoLinksMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="DimensionFilterMark" DisplayName="Dimension Filter Mark" DefaultIcon="Resources\DimensionDataFilter.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="-0.2" VerticalOffset="0">
        <IconDecorator Name="Multidimensional" DisplayName="Multidimensional" DefaultIcon="Resources\MultidimensionalMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopLeft" HorizontalOffset="0.35" VerticalOffset="0.3">
        <IconDecorator Name="InStudy" DisplayName="In Study" DefaultIcon="Resources\InStudy.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="-0.2" VerticalOffset="0">
        <IconDecorator Name="ModelViewMark" DisplayName="Model View Mark" DefaultIcon="Resources\ModelViewMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.2" VerticalOffset="0">
        <IconDecorator Name="AggregationMark" DisplayName="Aggregation Mark" DefaultIcon="Resources\AggregationMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="-0.2" VerticalOffset="0">
        <IconDecorator Name="DbScriptMark" DisplayName="Db Script Mark" DefaultIcon="Resources\DbFuncMark.png" />
      </ShapeHasDecorators>
      <Compartment TitleFillColor="235, 235, 235" Name="AttributesCompartment" Title="Attributes" />
      <Compartment TitleFillColor="235, 235, 235" Name="OperationsCompartment" Title="Operations" />
      <Compartment TitleFillColor="235, 235, 235" Name="IndexesCompartiment" Title="Store Indexes" />
      <Compartment TitleFillColor="Info" Name="DimensionRoutesCompartment" Title="Filter Routes To Dimensions" />
    </CompartmentShape>
    <CompartmentShape Id="92569d75-9b65-47b9-9d21-d093f83ae652" Description="" Name="InterfaceShape" DisplayName="Interface Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Interface Shape" ExposesTextColor="true" FillColor="255, 224, 192" InitialWidth="2.5" InitialHeight="0.5" OutlineThickness="0.01" FillGradientMode="ForwardDiagonal" ExposesOutlineColorAsProperty="true" ExposesFillColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" ExposesOutlineThicknessAsProperty="true" Geometry="Rectangle">
      <Notes>This shape only has one compartment, so by default it would not show the compartment header.
      But we want it to look uniform with the ClassShape, so we set IsSingleCompartmentHeaderVisible.</Notes>
      <Properties>
        <DomainProperty Id="4dc36d94-1a4d-4cf9-ac45-6e15751aabeb" Description="Description for Linx.BusinessModelDesigner.InterfaceShape.Fill Color" Name="FillColor" DisplayName="Fill Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="609d2aeb-d22a-45bb-963f-ac80dc6aac02" Description="Description for Linx.BusinessModelDesigner.InterfaceShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8c78f321-e59b-46ef-8102-3a656501273c" Description="Description for Linx.BusinessModelDesigner.InterfaceShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3a61a7a5-6d91-49f3-860e-7fee7a31cde6" Description="Description for Linx.BusinessModelDesigner.InterfaceShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0691bb2a-c9f5-46a9-a66c-663a03676bc2" Description="Description for Linx.BusinessModelDesigner.InterfaceShape.Outline Thickness" Name="OutlineThickness" DisplayName="Outline Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Sterotype" DisplayName="Sterotype" DefaultText="&lt;&lt;Contract&gt;&gt;">
          <Notes>This decorator is fixed - not mapped to any property.</Notes>
        </TextDecorator>
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.15">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="InterfaceShapeNameDecorator">
          <Notes>The VerticalOffset puts this decorator just below the stereotype, with normal font sizes.</Notes>
        </TextDecorator>
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <Compartment TitleFillColor="235, 235, 235" Name="OperationsCompartment" Title="Operations" />
    </CompartmentShape>
    <GeometryShape Id="7156cc0c-fd0c-4958-b6fd-71797be47094" Description="" Name="CommentBoxShape" DisplayName="Comment Box Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Comment Box Shape" ExposesTextColor="true" FillColor="Pink" InitialHeight="0.3" OutlineThickness="0.01" FillGradientMode="None" ExposesOutlineColorAsProperty="true" ExposesFillColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" ExposesOutlineThicknessAsProperty="true" Geometry="RoundedRectangle">
      <Properties>
        <DomainProperty Id="27e4cab3-c0a0-4b29-9860-69a40df7eebf" Description="Description for Linx.BusinessModelDesigner.CommentBoxShape.Fill Color" Name="FillColor" DisplayName="Fill Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8528d69c-da0a-4d31-ab68-ebae7ddf52a6" Description="Description for Linx.BusinessModelDesigner.CommentBoxShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="14138031-11e3-4a56-9f80-98d970d71d70" Description="Description for Linx.BusinessModelDesigner.CommentBoxShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f75383e0-0e65-4417-b48d-17204f8555bc" Description="Description for Linx.BusinessModelDesigner.CommentBoxShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c7d4a369-614b-44a8-afd5-6a38fffa0d14" Description="Description for Linx.BusinessModelDesigner.CommentBoxShape.Outline Thickness" Name="OutlineThickness" DisplayName="Outline Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Comment" DisplayName="Comment" DefaultText="BusinessRulesShapeNameDecorator" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Title" DisplayName="Title" DefaultText="Title" FontStyle="Bold" FontSize="12" />
      </ShapeHasDecorators>
    </GeometryShape>
    <ImageShape Id="a7c4cbdc-f467-4b55-b09e-2d3d40fdd71b" Description="" Name="MultipleAssociationShape" DisplayName="Multiple Association Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Multiple Association Shape" InitialHeight="1" OutlineThickness="0.01" FillGradientMode="None" Image="Resources\Relation.emf" />
    <CompartmentShape Id="7a20d459-5217-46b6-9f3c-5c65a1b79b80" Description="Domain View." Name="DomainViewShape" DisplayName="Domain View Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Domain View Shape" ExposesTextColor="true" FillColor="PaleGoldenrod" OutlineColor="Transparent" InitialWidth="2" InitialHeight="0.3" FillGradientMode="None" ExposesOutlineColorAsProperty="true" ExposesFillColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" ExposesOutlineThicknessAsProperty="true" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="68f5c0c0-158f-4f46-b57e-d3552dfeb726" Description="Description for Linx.BusinessModelDesigner.DomainViewShape.Fill Color" Name="FillColor" DisplayName="Fill Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d82a48c6-a92f-4216-9efd-bb59fbfe87fc" Description="Description for Linx.BusinessModelDesigner.DomainViewShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="169aba3f-fb53-4145-8a0a-fbbdc3605ec6" Description="Description for Linx.BusinessModelDesigner.DomainViewShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c7d40201-d44f-4d4e-9ede-098b5c41a769" Description="Description for Linx.BusinessModelDesigner.DomainViewShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="12b972b8-c3b1-4ec6-a049-537d4a138bb3" Description="Description for Linx.BusinessModelDesigner.DomainViewShape.Outline Thickness" Name="OutlineThickness" DisplayName="Outline Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <Compartment FillColor="WhiteSmoke" TitleFillColor="Silver" Name="DomainValueCompartment" Title="Values" />
    </CompartmentShape>
    <CompartmentShape Id="1b537c82-4c2f-4faa-8c54-c889609ab5cf" Description="Store Script." Name="StoreScriptShape" DisplayName="Store Script Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Store Script Shape" TextColor="White" ExposesTextColor="true" FillColor="WindowFrame" InitialWidth="2" InitialHeight="0.5" FillGradientMode="None" ExposesOutlineColorAsProperty="true" ExposesFillColorAsProperty="true" ExposesOutlineDashStyleAsProperty="true" ExposesOutlineThicknessAsProperty="true" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="9270d939-65c4-42e9-85b5-8f8812511b09" Description="Description for Linx.BusinessModelDesigner.StoreScriptShape.Fill Color" Name="FillColor" DisplayName="Fill Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="59210a4f-59e9-4982-a4b9-e871fe5c7f34" Description="Description for Linx.BusinessModelDesigner.StoreScriptShape.Outline Color" Name="OutlineColor" DisplayName="Outline Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="12c2cff5-95ea-4225-8461-9769966e72f0" Description="Description for Linx.BusinessModelDesigner.StoreScriptShape.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="47fca47d-b4b6-46be-b52c-eefb98ae2fd8" Description="Description for Linx.BusinessModelDesigner.StoreScriptShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="fdd9b973-e15f-46bf-9ab8-6e3f8ab98cfb" Description="Description for Linx.BusinessModelDesigner.StoreScriptShape.Outline Thickness" Name="OutlineThickness" DisplayName="Outline Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="ScriptMark" DisplayName="Script Mark" DefaultIcon="Resources\StoreScript.png" />
      </ShapeHasDecorators>
      <Compartment TitleFillColor="WindowFrame" Name="StoreQueriesCompartiment" Title="Store Queries" TitleTextColor="White" />
    </CompartmentShape>
    <ImageShape Id="52636474-4b0c-49e9-8687-44f9efeb6836" Description="" Name="DbProviderShape" DisplayName="Db Provider Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Db Provider Shape" FillColor="Tomato" InitialWidth="1" InitialHeight="1" FillGradientMode="None" Image="Resources\DbProvider.png">
      <ShapeHasDecorators Position="OuterBottomCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="ConnectionName" DisplayName="Connection Name" DefaultText="ConnectionName" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Type" DisplayName="Type" DefaultText="Type" FontStyle="Bold" FontSize="12" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopRight" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IsDefault" DisplayName="Is Default" DefaultIcon="Resources\Default.png" />
      </ShapeHasDecorators>
    </ImageShape>
    <CompartmentShape Id="fe142b19-ca55-482b-9130-566c03b4dfd6" Description="Reference for a class from the other model." Name="ReferenceModelClassShape" DisplayName="Reference Model Class Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Reference Model Class Shape" TextColor="Gray" FillColor="211, 220, 239" InitialHeight="1" OutlineDashStyle="Dot" OutlineThickness="0.05" FillGradientMode="None" Geometry="Rectangle">
      <BaseCompartmentShape>
        <CompartmentShapeMoniker Name="ClassShape" />
      </BaseCompartmentShape>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Sterotype" DisplayName="Sterotype" DefaultText="&lt;&lt;Class Reference&gt;&gt;" FontStyle="Bold" FontSize="10">
          <Notes>This decorator is fixed - not mapped to any property.</Notes>
        </TextDecorator>
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="ReferenceError" DisplayName="Reference Error" DefaultIcon="Resources\brokenlink.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="-0.2">
        <TextDecorator Name="ProjectSterotype" DisplayName="Project Sterotype" DefaultText="&lt;&lt;Class Reference&gt;&gt;" FontStyle="Bold" FontSize="10">
          <Notes>This decorator is fixed - not mapped to any property.</Notes>
        </TextDecorator>
      </ShapeHasDecorators>
    </CompartmentShape>
    <GeometryShape Id="b6d2752f-acc2-4ee1-8ccb-03df107d246f" Description="Description for Linx.BusinessModelDesigner.ModelImplementationShape" Name="ModelImplementationShape" DisplayName="Model Implementation Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Model Implementation Shape" FillColor="Beige" InitialWidth="2.5" InitialHeight="0.8" Geometry="RoundedRectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="ImplementMark" DisplayName="Implement Mark" DefaultIcon="Resources\ImplementMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerBottomLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IsSelected" DisplayName="Is Selected" DefaultIcon="Resources\Default.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterBottomCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Info" DisplayName="Info" DefaultText="ImplementationHelper&lt;Contract&gt;.GetInstance(&quot;Name&quot;)">
          <Notes>This decorator is fixed - not mapped to any property.</Notes>
        </TextDecorator>
      </ShapeHasDecorators>
    </GeometryShape>
    <CompartmentShape Id="82251bf9-cebb-410d-a433-1c717911488e" Description="Description for Linx.BusinessModelDesigner.WebApiControllerShape" Name="WebApiControllerShape" DisplayName="Web Api Controller Shape" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Web Api Controller Shape" FillColor="Goldenrod" InitialWidth="2.5" InitialHeight="0.7" FillGradientMode="ForwardDiagonal" Geometry="RoundedRectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="WebApiMark" DisplayName="Web Api Mark" DefaultIcon="Resources\ExposeAsService.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="ExposeAllContextMark" DisplayName="Expose All Context Mark" DefaultIcon="resources\ExposeAllTextMark.png" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0.2">
        <IconDecorator Name="AspNetCore" DisplayName="Asp Net Core" DefaultIcon="Resources\AspNetCore.png" />
      </ShapeHasDecorators>
      <Compartment FillColor="Transparent" TitleFillColor="Moccasin" Name="WebApiActionCompartment" Title="Actions" />
    </CompartmentShape>
  </Shapes>
  <Connectors>
    <Connector Id="78ca4a15-6f73-4c6e-83d9-a03249240799" Description="" Name="AssociationConnector" DisplayName="Association Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Association Connector" ExposesTextColor="true" Color="113, 111, 110" TargetEndStyle="EmptyArrow" Thickness="0.02" ExposesColorAsProperty="true" ExposesDashStyleAsProperty="true" ExposesThicknessAsProperty="true">
      <Properties>
        <DomainProperty Id="7fcf7c4a-6696-447e-a500-92fc7c3a927c" Description="Description for Linx.BusinessModelDesigner.AssociationConnector.Color" Name="Color" DisplayName="Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dd021e7b-d982-4900-8e0a-10ce954d0b2f" Description="Description for Linx.BusinessModelDesigner.AssociationConnector.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="42795746-a6ac-48c9-8bad-27b4b31ae1fa" Description="Description for Linx.BusinessModelDesigner.AssociationConnector.Dash Style" Name="DashStyle" DisplayName="Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="45f688d7-4278-428a-b8bc-bea577f67452" Description="Description for Linx.BusinessModelDesigner.AssociationConnector.Thickness" Name="Thickness" DisplayName="Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ConnectorHasDecorators Position="TargetBottom" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="TargetMultiplicity" DisplayName="Target Multiplicity" DefaultText="TargetMultiplicity" />
      </ConnectorHasDecorators>
      <ConnectorHasDecorators Position="SourceBottom" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="SourceMultiplicity" DisplayName="Source Multiplicity" DefaultText="SourceMultiplicity" />
      </ConnectorHasDecorators>
      <ConnectorHasDecorators Position="TargetTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="TargetPropertyNameToSource" DisplayName="Target Property Name To Source" DefaultText="TargetPropertyNameToSource" />
      </ConnectorHasDecorators>
      <ConnectorHasDecorators Position="SourceTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="SourcePropertyNameToTarget" DisplayName="Source Property Name To Target" DefaultText="SourcePropertyNameToTarget" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="bf6d3cc3-57f6-41da-acb3-0c066b93b5f1" Description="" Name="MultipleAssociationRoleConnector" DisplayName="Multiple Association Role Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Multiple Association Role Connector" ExposesTextColor="true" Color="113, 111, 110" Thickness="0.02" ExposesColorAsProperty="true" ExposesDashStyleAsProperty="true" ExposesThicknessAsProperty="true">
      <Properties>
        <DomainProperty Id="b09b46e2-ddb9-4384-b18d-b77d1d5c4b1e" Description="Description for Linx.BusinessModelDesigner.MultipleAssociationRoleConnector.Color" Name="Color" DisplayName="Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="22fda4a3-9d91-4121-9b96-cc01e0e6767c" Description="Description for Linx.BusinessModelDesigner.MultipleAssociationRoleConnector.Text Color" Name="TextColor" DisplayName="Text Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f0c42d4b-cb71-4eaa-b343-7f72f569e4f2" Description="Description for Linx.BusinessModelDesigner.MultipleAssociationRoleConnector.Dash Style" Name="DashStyle" DisplayName="Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="14dbc88c-eda8-4346-bd5d-a666dd08f0b2" Description="Description for Linx.BusinessModelDesigner.MultipleAssociationRoleConnector.Thickness" Name="Thickness" DisplayName="Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ConnectorHasDecorators Position="TargetBottom" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="TargetMultiplicity" DisplayName="Target Multiplicity" DefaultText="TargetMultiplicity" />
      </ConnectorHasDecorators>
      <ConnectorHasDecorators Position="TargetTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="TargetRoleName" DisplayName="Target Role Name" DefaultText="TargetRoleName" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="8e0165e5-384c-4189-b8f3-2302c0e7da5b" Description="" Name="AssociationClassConnector" DisplayName="Association Class Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Association Class Connector" Color="113, 111, 110" DashStyle="Dash" Thickness="0.01" ExposesColorAsProperty="true">
      <Properties>
        <DomainProperty Id="9276b137-8f02-4171-8e94-39106b578409" Description="Description for Linx.BusinessModelDesigner.AssociationClassConnector.Color" Name="Color" DisplayName="Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
      </Properties>
    </Connector>
    <Connector Id="0b1787aa-9261-407d-a278-7b27dddce538" Description="" Name="GeneralizationConnector" DisplayName="Generalization Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Generalization Connector" Color="Navy" SourceEndStyle="HollowArrow" Thickness="0.02" ExposesColorAsProperty="true" ExposesDashStyleAsProperty="true" ExposesThicknessAsProperty="true" sourceEndWidth="0.2" sourceEndHeight="0.2" targetEndWidth="0.2">
      <Properties>
        <DomainProperty Id="c418dc59-3fde-4b47-b91f-3b71b9f318dd" Description="Description for Linx.BusinessModelDesigner.GeneralizationConnector.Color" Name="Color" DisplayName="Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="19c5d086-27e3-4a54-999f-964a8b52ca4e" Description="Description for Linx.BusinessModelDesigner.GeneralizationConnector.Dash Style" Name="DashStyle" DisplayName="Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cf3cba56-f374-4d95-acd7-a1f49bca5451" Description="Description for Linx.BusinessModelDesigner.GeneralizationConnector.Thickness" Name="Thickness" DisplayName="Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
    </Connector>
    <Connector Id="80ad9fd3-e9c8-4b2b-8d89-2a669cad7c92" Description="" Name="ImplementationConnector" DisplayName="Implementation Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Implementation Connector" Color="192, 64, 0" DashStyle="Dash" SourceEndStyle="HollowArrow" Thickness="0.02" ExposesColorAsProperty="true" ExposesDashStyleAsProperty="true" ExposesThicknessAsProperty="true" sourceEndWidth="0.2" sourceEndHeight="0.2" targetEndWidth="0.2" targetEndHeight="0.2">
      <Properties>
        <DomainProperty Id="3ff90e16-3dcd-4a78-a73c-9358e0b7c89b" Description="Description for Linx.BusinessModelDesigner.ImplementationConnector.Color" Name="Color" DisplayName="Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="bb8d12db-c5a1-41a6-9263-eac69e20c3ea" Description="Description for Linx.BusinessModelDesigner.ImplementationConnector.Dash Style" Name="DashStyle" DisplayName="Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="61010b5c-8b71-4712-948c-49637973e7f0" Description="Description for Linx.BusinessModelDesigner.ImplementationConnector.Thickness" Name="Thickness" DisplayName="Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
    </Connector>
    <Connector Id="44255ee2-c66c-49be-a604-a74c1bc757ac" Description="" Name="CommentConnector" DisplayName="Comment Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Comment Connector" Color="113, 111, 110" DashStyle="Dot" Thickness="0.02" RoutingStyle="Straight" ExposesColorAsProperty="true" ExposesDashStyleAsProperty="true" ExposesThicknessAsProperty="true">
      <Properties>
        <DomainProperty Id="2e8f9a15-f734-4389-846f-d6db1f4af8ab" Description="Description for Linx.BusinessModelDesigner.CommentConnector.Color" Name="Color" DisplayName="Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7b8c30bb-0b88-4f5a-b0d3-0180c3e672c3" Description="Description for Linx.BusinessModelDesigner.CommentConnector.Dash Style" Name="DashStyle" DisplayName="Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="83162d7e-bda1-498a-95b6-567369ea2a13" Description="Description for Linx.BusinessModelDesigner.CommentConnector.Thickness" Name="Thickness" DisplayName="Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
    </Connector>
    <Connector Id="9d37f51c-f1f5-47d7-8722-2f20fb3be710" Description="" Name="GeneralizationShConnector" DisplayName="Generalization Sh Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Generalization Sh Connector" TextColor="LightSeaGreen" Color="LightSeaGreen" SourceEndStyle="FilledArrow" Thickness="0.02" ExposesColorAsProperty="true" ExposesDashStyleAsProperty="true" ExposesThicknessAsProperty="true" sourceEndWidth="0.2" sourceEndHeight="0.2" targetEndWidth="0.2">
      <Properties>
        <DomainProperty Id="2960ba66-0ddb-4cce-8409-d771d0f54450" Description="Description for Linx.BusinessModelDesigner.GeneralizationShConnector.Color" Name="Color" DisplayName="Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d61152ee-4d29-47ea-a22b-db255a141629" Description="Description for Linx.BusinessModelDesigner.GeneralizationShConnector.Dash Style" Name="DashStyle" DisplayName="Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="45cd5dbc-7a04-4df5-8135-2916f47877e9" Description="Description for Linx.BusinessModelDesigner.GeneralizationShConnector.Thickness" Name="Thickness" DisplayName="Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ConnectorHasDecorators Position="TargetTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="TextDiscriminator" DisplayName="Text Discriminator" DefaultText="TextDiscriminator" FontStyle="Bold" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="e224c02e-5643-4151-887b-45195698ff20" Description="Description for Linx.BusinessModelDesigner.ModelImplementationConnector" Name="ModelImplementationConnector" DisplayName="Model Implementation Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Model Implementation Connector" TargetEndStyle="EmptyArrow" />
    <Connector Id="d774a90d-cf42-4cb1-a5ed-8e7c3a3e3d17" Description="" Name="ModelViewConnector" DisplayName="Model View Connector" Namespace="Linx.BusinessModelDesigner" FixedTooltipText="Model View Connector" Color="DarkOrchid" TargetEndStyle="FilledDiamond" Thickness="0.02" ExposesColorAsProperty="true" ExposesDashStyleAsProperty="true" ExposesThicknessAsProperty="true" targetEndWidth="0.2">
      <Properties>
        <DomainProperty Id="f5c3a52f-069d-4ab9-bc47-736490767912" Description="Description for Linx.BusinessModelDesigner.ModelViewConnector.Color" Name="Color" DisplayName="Color" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Color" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7db0947f-54c5-4f3d-8cd6-d76c93832211" Description="Description for Linx.BusinessModelDesigner.ModelViewConnector.Dash Style" Name="DashStyle" DisplayName="Dash Style" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0dc236c4-4547-4c03-bae1-3badbc66c843" Description="Description for Linx.BusinessModelDesigner.ModelViewConnector.Thickness" Name="Thickness" DisplayName="Thickness" Kind="CustomStorage">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ConnectorHasDecorators Position="SourceTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="CollectionNameDiscriminator" DisplayName="Collection Name Discriminator" DefaultText="CollectionNameDiscriminator" FontStyle="Bold" />
      </ConnectorHasDecorators>
    </Connector>
  </Connectors>
  <XmlSerializationBehavior Name="BusinessModelDesignerSerializationBehavior" Namespace="Linx.BusinessModelDesigner">
    <ClassData>
      <XmlClassData TypeName="NamedElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="namedElementMoniker" ElementName="namedElement" MonikerTypeName="NamedElementMoniker">
        <DomainClassMoniker Name="NamedElement" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="NamedElement/Name" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClassHasAttributes" MonikerAttributeName="" SerializeId="true" MonikerElementName="classHasAttributesMoniker" ElementName="classHasAttributes" MonikerTypeName="ClassHasAttributesMoniker">
        <DomainRelationshipMoniker Name="ClassHasAttributes" />
      </XmlClassData>
      <XmlClassData TypeName="BusinessModelDesignerRootHasComments" MonikerAttributeName="" SerializeId="true" MonikerElementName="businessModelDesignerRootHasCommentsMoniker" ElementName="businessModelDesignerRootHasComments" MonikerTypeName="BusinessModelDesignerRootHasCommentsMoniker">
        <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasComments" />
      </XmlClassData>
      <XmlClassData TypeName="ClassHasOperations" MonikerAttributeName="" SerializeId="true" MonikerElementName="classHasOperationsMoniker" ElementName="classHasOperations" MonikerTypeName="ClassHasOperationsMoniker">
        <DomainRelationshipMoniker Name="ClassHasOperations" />
      </XmlClassData>
      <XmlClassData TypeName="Generalization" MonikerAttributeName="" SerializeId="true" MonikerElementName="generalizationMoniker" ElementName="generalization" MonikerTypeName="GeneralizationMoniker">
        <DomainRelationshipMoniker Name="Generalization" />
        <ElementData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="Generalization/Description" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="InterfaceHasOperation" MonikerAttributeName="" SerializeId="true" MonikerElementName="interfaceHasOperationMoniker" ElementName="interfaceHasOperation" MonikerTypeName="InterfaceHasOperationMoniker">
        <DomainRelationshipMoniker Name="InterfaceHasOperation" />
      </XmlClassData>
      <XmlClassData TypeName="MultipleAssociationOrigin" MonikerAttributeName="" SerializeId="true" MonikerElementName="multipleAssociationOriginMoniker" ElementName="multipleAssociationOrigin" MonikerTypeName="MultipleAssociationOriginMoniker">
        <DomainRelationshipMoniker Name="MultipleAssociationOrigin" />
        <ElementData>
          <XmlPropertyData XmlName="multiplicity">
            <DomainPropertyMoniker Name="MultipleAssociationOrigin/Multiplicity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="collectionName">
            <DomainPropertyMoniker Name="MultipleAssociationOrigin/CollectionName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="foreignKeyConstraintName">
            <DomainPropertyMoniker Name="MultipleAssociationOrigin/ForeignKeyConstraintName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="willCascadeOnDelete">
            <DomainPropertyMoniker Name="MultipleAssociationOrigin/WillCascadeOnDelete" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="MultipleAssociationOrigin/Description" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="BusinessModelDesignerRootHasTypes" MonikerAttributeName="" SerializeId="true" MonikerElementName="businessModelDesignerRootHasTypesMoniker" ElementName="businessModelDesignerRootHasTypes" MonikerTypeName="BusinessModelDesignerRootHasTypesMoniker">
        <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasTypes" />
      </XmlClassData>
      <XmlClassData TypeName="CommentReferencesSubjects" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentReferencesSubjectsMoniker" ElementName="commentReferencesSubjects" MonikerTypeName="CommentReferencesSubjectsMoniker">
        <DomainRelationshipMoniker Name="CommentReferencesSubjects" />
      </XmlClassData>
      <XmlClassData TypeName="BusinessModelDesignerRoot" MonikerAttributeName="" SerializeId="true" MonikerElementName="businessModelDesignerRootMoniker" ElementName="businessModelDesignerRoot" MonikerTypeName="BusinessModelDesignerRootMoniker">
        <DomainClassMoniker Name="BusinessModelDesignerRoot" />
        <ElementData>
          <XmlRelationshipData RoleElementName="comments">
            <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasComments" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="types">
            <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasTypes" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="documentName">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/DocumentName" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="storeScripts">
            <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasStoreScripts" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="dbProviders">
            <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasDbProviders" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="targetNamespace">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/TargetNamespace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="documentPath">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/DocumentPath" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="noCode">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/NoCode" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataContextName">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/DataContextName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="removeAutomaticIndexes">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/RemoveAutomaticIndexes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="removeRequiredAttributes">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/RemoveRequiredAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="businessGroupForFilteringOfDimension">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/BusinessGroupForFilteringOfDimension" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="modelImplementations">
            <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasModelImplementations" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="webApiControllers">
            <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasWebApiControllers" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="enableAutomaticAuthorization">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/EnableAutomaticAuthorization" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableAccessConnectionControl">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/EnableAccessConnectionControl" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableViewCacheFactory">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/EnableViewCacheFactory" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="setIdLinxWithIdGpecon">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/SetIdLinxWithIdGpecon" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="forceDynamicForeignKeyNames">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/ForceDynamicForeignKeyNames" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="controlIdLinxByApplicative">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/ControlIdLinxByApplicative" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="generateCustomerCustomizationProject">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/GenerateCustomerCustomizationProject" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isAspNetCore">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/IsAspNetCore" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="disableAllAudits">
            <DomainPropertyMoniker Name="BusinessModelDesignerRoot/DisableAllAudits" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelClass" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelClassMoniker" ElementName="modelClass" MonikerTypeName="ModelClassMoniker">
        <DomainClassMoniker Name="ModelClass" />
        <ElementData>
          <XmlPropertyData XmlName="kind">
            <DomainPropertyMoniker Name="ModelClass/Kind" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modifier">
            <DomainPropertyMoniker Name="ModelClass/Modifier" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="attributes">
            <DomainRelationshipMoniker Name="ClassHasAttributes" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="operations">
            <DomainRelationshipMoniker Name="ClassHasOperations" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="subclasses">
            <DomainRelationshipMoniker Name="Generalization" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="table">
            <DomainPropertyMoniker Name="ModelClass/Table" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="schema">
            <DomainPropertyMoniker Name="ModelClass/Schema" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="primaryKeyConstraintName">
            <DomainPropertyMoniker Name="ModelClass/PrimaryKeyConstraintName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="ModelClass/DisplayName" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="modelIndexes">
            <DomainRelationshipMoniker Name="ClassHasIndexes" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="targetModelClasses">
            <DomainRelationshipMoniker Name="Association" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="subclassesSh">
            <DomainRelationshipMoniker Name="GeneralizationSh" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="notMapped">
            <DomainPropertyMoniker Name="ModelClass/NotMapped" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customAttributes">
            <DomainPropertyMoniker Name="ModelClass/CustomAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="hideAssociations">
            <DomainPropertyMoniker Name="ModelClass/HideAssociations" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isClustered">
            <DomainPropertyMoniker Name="ModelClass/IsClustered" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="routeMapDatum">
            <DomainRelationshipMoniker Name="ClassHasDataRoutes" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="isFactTable">
            <DomainPropertyMoniker Name="ModelClass/IsFactTable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDimensionFilter">
            <DomainPropertyMoniker Name="ModelClass/IsDimensionFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="contentDefinition">
            <DomainPropertyMoniker Name="ModelClass/ContentDefinition" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="primaryKeyColumnMap">
            <DomainPropertyMoniker Name="ModelClass/PrimaryKeyColumnMap" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isReadOnly">
            <DomainPropertyMoniker Name="ModelClass/IsReadOnly" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="inStudy">
            <DomainPropertyMoniker Name="ModelClass/InStudy" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableIdLinxForSearching">
            <DomainPropertyMoniker Name="ModelClass/EnableIdLinxForSearching" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableIdGpeconForSearching">
            <DomainPropertyMoniker Name="ModelClass/EnableIdGpeconForSearching" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableIdLinxForInserting">
            <DomainPropertyMoniker Name="ModelClass/EnableIdLinxForInserting" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableIdGpeconForInserting">
            <DomainPropertyMoniker Name="ModelClass/EnableIdGpeconForInserting" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isValidatable">
            <DomainPropertyMoniker Name="ModelClass/IsValidatable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewDefinition">
            <DomainPropertyMoniker Name="ModelClass/ModelViewDefinition" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewAggregation">
            <DomainPropertyMoniker Name="ModelClass/ModelViewAggregation" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewMainEntity">
            <DomainPropertyMoniker Name="ModelClass/ModelViewMainEntity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewDbSets">
            <DomainPropertyMoniker Name="ModelClass/ModelViewDbSets" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableCache">
            <DomainPropertyMoniker Name="ModelClass/EnableCache" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filter">
            <DomainPropertyMoniker Name="ModelClass/Filter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewDistinct">
            <DomainPropertyMoniker Name="ModelClass/ModelViewDistinct" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewTop">
            <DomainPropertyMoniker Name="ModelClass/ModelViewTop" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="modelViews">
            <DomainRelationshipMoniker Name="ModelViewAssociation" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="modelViewCodePreQuery">
            <DomainPropertyMoniker Name="ModelClass/ModelViewCodePreQuery" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableIdFilialPfjControl">
            <DomainPropertyMoniker Name="ModelClass/EnableIdFilialPfjControl" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewCustomQueryEnabled">
            <DomainPropertyMoniker Name="ModelClass/ModelViewCustomQueryEnabled" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="databaseScriptCommand">
            <DomainPropertyMoniker Name="ModelClass/DatabaseScriptCommand" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableAudit">
            <DomainPropertyMoniker Name="ModelClass/EnableAudit" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="auditType">
            <DomainPropertyMoniker Name="ModelClass/AuditType" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelAttribute" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelAttributeMoniker" ElementName="modelAttribute" MonikerTypeName="ModelAttributeMoniker">
        <DomainClassMoniker Name="ModelAttribute" />
        <ElementData>
          <XmlPropertyData XmlName="dataType">
            <DomainPropertyMoniker Name="ModelAttribute/DataType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="defaultValue">
            <DomainPropertyMoniker Name="ModelAttribute/DefaultValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="columnName">
            <DomainPropertyMoniker Name="ModelAttribute/ColumnName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPrimaryKey">
            <DomainPropertyMoniker Name="ModelAttribute/IsPrimaryKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isIdentity">
            <DomainPropertyMoniker Name="ModelAttribute/IsIdentity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isNullable">
            <DomainPropertyMoniker Name="ModelAttribute/IsNullable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="precision">
            <DomainPropertyMoniker Name="ModelAttribute/Precision" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="scale">
            <DomainPropertyMoniker Name="ModelAttribute/Scale" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="ModelAttribute/DisplayName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customAttributes">
            <DomainPropertyMoniker Name="ModelAttribute/CustomAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="range">
            <DomainPropertyMoniker Name="ModelAttribute/Range" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataFormatString">
            <DomainPropertyMoniker Name="ModelAttribute/DataFormatString" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="domainName">
            <DomainPropertyMoniker Name="ModelAttribute/DomainName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="mask">
            <DomainPropertyMoniker Name="ModelAttribute/Mask" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="formula">
            <DomainPropertyMoniker Name="ModelAttribute/Formula" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="triggerAttributes">
            <DomainPropertyMoniker Name="ModelAttribute/TriggerAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="foreignKey">
            <DomainPropertyMoniker Name="ModelAttribute/ForeignKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filter">
            <DomainPropertyMoniker Name="ModelAttribute/Filter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="columnType" Representation="Ignore">
            <DomainPropertyMoniker Name="ModelAttribute/ColumnType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="notMapped">
            <DomainPropertyMoniker Name="ModelAttribute/NotMapped" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isComputed">
            <DomainPropertyMoniker Name="ModelAttribute/IsComputed" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="maxLength">
            <DomainPropertyMoniker Name="ModelAttribute/MaxLength" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sqlDefault">
            <DomainPropertyMoniker Name="ModelAttribute/SqlDefault" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDimensionFilterSuggestion">
            <DomainPropertyMoniker Name="ModelAttribute/IsDimensionFilterSuggestion" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="inStudy">
            <DomainPropertyMoniker Name="ModelAttribute/InStudy" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewSource">
            <DomainPropertyMoniker Name="ModelAttribute/ModelViewSource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewFormula">
            <DomainPropertyMoniker Name="ModelAttribute/ModelViewFormula" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="aggregationFunction">
            <DomainPropertyMoniker Name="ModelAttribute/AggregationFunction" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="exclusiveFilter">
            <DomainPropertyMoniker Name="ModelAttribute/ExclusiveFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewOrderBySequence">
            <DomainPropertyMoniker Name="ModelAttribute/ModelViewOrderBySequence" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewOrderByOrientation">
            <DomainPropertyMoniker Name="ModelAttribute/ModelViewOrderByOrientation" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isCustomized">
            <DomainPropertyMoniker Name="ModelAttribute/IsCustomized" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="filteringDisabled">
            <DomainPropertyMoniker Name="ModelAttribute/FilteringDisabled" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="modelViewCountDistinctFilter">
            <DomainPropertyMoniker Name="ModelAttribute/ModelViewCountDistinctFilter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isUniqueValue">
            <DomainPropertyMoniker Name="ModelAttribute/IsUniqueValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customDataType">
            <DomainPropertyMoniker Name="ModelAttribute/CustomDataType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="brandDecimalsControl">
            <DomainPropertyMoniker Name="ModelAttribute/BrandDecimalsControl" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="hasAudit">
            <DomainPropertyMoniker Name="ModelAttribute/HasAudit" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="Comment" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentMoniker" ElementName="comment" MonikerTypeName="CommentMoniker">
        <DomainClassMoniker Name="Comment" />
        <ElementData>
          <XmlPropertyData XmlName="text">
            <DomainPropertyMoniker Name="Comment/Text" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="subjects">
            <DomainRelationshipMoniker Name="CommentReferencesSubjects" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="title">
            <DomainPropertyMoniker Name="Comment/Title" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="Operation" MonikerAttributeName="" SerializeId="true" MonikerElementName="operationMoniker" ElementName="operation" MonikerTypeName="OperationMoniker">
        <DomainClassMoniker Name="Operation" />
        <ElementData>
          <XmlPropertyData XmlName="concurrency">
            <DomainPropertyMoniker Name="Operation/Concurrency" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="comment">
            <DomainPropertyMoniker Name="Operation/Comment" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="returnType">
            <DomainPropertyMoniker Name="Operation/ReturnType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="access">
            <DomainPropertyMoniker Name="Operation/Access" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customAttributes">
            <DomainPropertyMoniker Name="Operation/CustomAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="parameters">
            <DomainPropertyMoniker Name="Operation/Parameters" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isStatic">
            <DomainPropertyMoniker Name="Operation/IsStatic" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="canOverride">
            <DomainPropertyMoniker Name="Operation/CanOverride" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="overloadName">
            <DomainPropertyMoniker Name="Operation/OverloadName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="docComment">
            <DomainPropertyMoniker Name="Operation/DocComment" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isShared">
            <DomainPropertyMoniker Name="Operation/IsShared" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPartial">
            <DomainPropertyMoniker Name="Operation/IsPartial" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isUniqueOverload">
            <DomainPropertyMoniker Name="Operation/IsUniqueOverload" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClassOperation" MonikerAttributeName="" SerializeId="true" MonikerElementName="classOperationMoniker" ElementName="classOperation" MonikerTypeName="ClassOperationMoniker">
        <DomainClassMoniker Name="ClassOperation" />
        <ElementData>
          <XmlPropertyData XmlName="isAbstract">
            <DomainPropertyMoniker Name="ClassOperation/IsAbstract" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="interfaceLinkId">
            <DomainPropertyMoniker Name="ClassOperation/InterfaceLinkId" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelInterface" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelInterfaceMoniker" ElementName="modelInterface" MonikerTypeName="ModelInterfaceMoniker">
        <DomainClassMoniker Name="ModelInterface" />
        <ElementData>
          <XmlRelationshipData RoleElementName="operations">
            <DomainRelationshipMoniker Name="InterfaceHasOperation" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="isExtension">
            <DomainPropertyMoniker Name="ModelInterface/IsExtension" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="InterfaceOperation" MonikerAttributeName="" SerializeId="true" MonikerElementName="interfaceOperationMoniker" ElementName="interfaceOperation" MonikerTypeName="InterfaceOperationMoniker">
        <DomainClassMoniker Name="InterfaceOperation" />
      </XmlClassData>
      <XmlClassData TypeName="MultipleAssociation" MonikerAttributeName="" SerializeId="true" MonikerElementName="multipleAssociationMoniker" ElementName="multipleAssociation" MonikerTypeName="MultipleAssociationMoniker">
        <DomainClassMoniker Name="MultipleAssociation" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="originTypes">
            <DomainRelationshipMoniker Name="MultipleAssociationOrigin" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="targetType">
            <DomainRelationshipMoniker Name="MultipleAssociationTarget" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="idReference">
            <DomainPropertyMoniker Name="MultipleAssociation/IdReference" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelType" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelTypeMoniker" ElementName="modelType" MonikerTypeName="ModelTypeMoniker">
        <DomainClassMoniker Name="ModelType" />
      </XmlClassData>
      <XmlClassData TypeName="ClassModelElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="classModelElementMoniker" ElementName="classModelElement" MonikerTypeName="ClassModelElementMoniker">
        <DomainClassMoniker Name="ClassModelElement" />
        <ElementData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="ClassModelElement/Description" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClassShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="classShapeMoniker" ElementName="classShape" MonikerTypeName="ClassShapeMoniker">
        <CompartmentShapeMoniker Name="ClassShape" />
        <ElementData>
          <XmlPropertyData XmlName="fillColor">
            <DomainPropertyMoniker Name="ClassShape/FillColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="ClassShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="ClassShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="ClassShape/OutlineDashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineThickness">
            <DomainPropertyMoniker Name="ClassShape/OutlineThickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="InterfaceShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="interfaceShapeMoniker" ElementName="interfaceShape" MonikerTypeName="InterfaceShapeMoniker">
        <CompartmentShapeMoniker Name="InterfaceShape" />
        <ElementData>
          <XmlPropertyData XmlName="fillColor">
            <DomainPropertyMoniker Name="InterfaceShape/FillColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="InterfaceShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="InterfaceShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="InterfaceShape/OutlineDashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineThickness">
            <DomainPropertyMoniker Name="InterfaceShape/OutlineThickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="CommentBoxShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentBoxShapeMoniker" ElementName="commentBoxShape" MonikerTypeName="CommentBoxShapeMoniker">
        <GeometryShapeMoniker Name="CommentBoxShape" />
        <ElementData>
          <XmlPropertyData XmlName="fillColor">
            <DomainPropertyMoniker Name="CommentBoxShape/FillColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="CommentBoxShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="CommentBoxShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="CommentBoxShape/OutlineDashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineThickness">
            <DomainPropertyMoniker Name="CommentBoxShape/OutlineThickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="MultipleAssociationShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="multipleAssociationShapeMoniker" ElementName="multipleAssociationShape" MonikerTypeName="MultipleAssociationShapeMoniker">
        <ImageShapeMoniker Name="MultipleAssociationShape" />
      </XmlClassData>
      <XmlClassData TypeName="AssociationConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationConnectorMoniker" ElementName="associationConnector" MonikerTypeName="AssociationConnectorMoniker">
        <ConnectorMoniker Name="AssociationConnector" />
        <ElementData>
          <XmlPropertyData XmlName="color">
            <DomainPropertyMoniker Name="AssociationConnector/Color" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="AssociationConnector/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dashStyle">
            <DomainPropertyMoniker Name="AssociationConnector/DashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="thickness">
            <DomainPropertyMoniker Name="AssociationConnector/Thickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="MultipleAssociationRoleConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="multipleAssociationRoleConnectorMoniker" ElementName="multipleAssociationRoleConnector" MonikerTypeName="MultipleAssociationRoleConnectorMoniker">
        <ConnectorMoniker Name="MultipleAssociationRoleConnector" />
        <ElementData>
          <XmlPropertyData XmlName="color">
            <DomainPropertyMoniker Name="MultipleAssociationRoleConnector/Color" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="MultipleAssociationRoleConnector/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dashStyle">
            <DomainPropertyMoniker Name="MultipleAssociationRoleConnector/DashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="thickness">
            <DomainPropertyMoniker Name="MultipleAssociationRoleConnector/Thickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AssociationClassConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationClassConnectorMoniker" ElementName="associationClassConnector" MonikerTypeName="AssociationClassConnectorMoniker">
        <ConnectorMoniker Name="AssociationClassConnector" />
        <ElementData>
          <XmlPropertyData XmlName="color">
            <DomainPropertyMoniker Name="AssociationClassConnector/Color" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="GeneralizationConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="generalizationConnectorMoniker" ElementName="generalizationConnector" MonikerTypeName="GeneralizationConnectorMoniker">
        <ConnectorMoniker Name="GeneralizationConnector" />
        <ElementData>
          <XmlPropertyData XmlName="color">
            <DomainPropertyMoniker Name="GeneralizationConnector/Color" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dashStyle">
            <DomainPropertyMoniker Name="GeneralizationConnector/DashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="thickness">
            <DomainPropertyMoniker Name="GeneralizationConnector/Thickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ImplementationConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="implementationConnectorMoniker" ElementName="implementationConnector" MonikerTypeName="ImplementationConnectorMoniker">
        <ConnectorMoniker Name="ImplementationConnector" />
        <ElementData>
          <XmlPropertyData XmlName="color">
            <DomainPropertyMoniker Name="ImplementationConnector/Color" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dashStyle">
            <DomainPropertyMoniker Name="ImplementationConnector/DashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="thickness">
            <DomainPropertyMoniker Name="ImplementationConnector/Thickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="CommentConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentConnectorMoniker" ElementName="commentConnector" MonikerTypeName="CommentConnectorMoniker">
        <ConnectorMoniker Name="CommentConnector" />
        <ElementData>
          <XmlPropertyData XmlName="color">
            <DomainPropertyMoniker Name="CommentConnector/Color" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dashStyle">
            <DomainPropertyMoniker Name="CommentConnector/DashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="thickness">
            <DomainPropertyMoniker Name="CommentConnector/Thickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="BusinessModelDesignerDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="businessModelDesignerDiagramMoniker" ElementName="businessModelDesignerDiagram" MonikerTypeName="BusinessModelDesignerDiagramMoniker">
        <DiagramMoniker Name="BusinessModelDesignerDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="DomainView" MonikerAttributeName="" SerializeId="true" MonikerElementName="domainViewMoniker" ElementName="domainView" MonikerTypeName="DomainViewMoniker">
        <DomainClassMoniker Name="DomainView" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="domainValues">
            <DomainRelationshipMoniker Name="DomainViewHasDomainValues" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="DomainValue" MonikerAttributeName="" SerializeId="true" MonikerElementName="domainValueMoniker" ElementName="domainValue" MonikerTypeName="DomainValueMoniker">
        <DomainClassMoniker Name="DomainValue" />
        <ElementData>
          <XmlPropertyData XmlName="name">
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
      <XmlClassData TypeName="DomainViewHasDomainValues" MonikerAttributeName="" SerializeId="true" MonikerElementName="domainViewHasDomainValuesMoniker" ElementName="domainViewHasDomainValues" MonikerTypeName="DomainViewHasDomainValuesMoniker">
        <DomainRelationshipMoniker Name="DomainViewHasDomainValues" />
      </XmlClassData>
      <XmlClassData TypeName="DomainViewShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="domainViewShapeMoniker" ElementName="domainViewShape" MonikerTypeName="DomainViewShapeMoniker">
        <CompartmentShapeMoniker Name="DomainViewShape" />
        <ElementData>
          <XmlPropertyData XmlName="fillColor">
            <DomainPropertyMoniker Name="DomainViewShape/FillColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="DomainViewShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="DomainViewShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="DomainViewShape/OutlineDashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineThickness">
            <DomainPropertyMoniker Name="DomainViewShape/OutlineThickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelIndex" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelIndexMoniker" ElementName="modelIndex" MonikerTypeName="ModelIndexMoniker">
        <DomainClassMoniker Name="ModelIndex" />
        <ElementData>
          <XmlPropertyData XmlName="properties">
            <DomainPropertyMoniker Name="ModelIndex/Properties" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isUnique">
            <DomainPropertyMoniker Name="ModelIndex/IsUnique" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isClustered">
            <DomainPropertyMoniker Name="ModelIndex/IsClustered" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="includeProperties">
            <DomainPropertyMoniker Name="ModelIndex/IncludeProperties" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClassHasIndexes" MonikerAttributeName="" SerializeId="true" MonikerElementName="classHasIndexesMoniker" ElementName="classHasIndexes" MonikerTypeName="ClassHasIndexesMoniker">
        <DomainRelationshipMoniker Name="ClassHasIndexes" />
      </XmlClassData>
      <XmlClassData TypeName="Association" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationMoniker" ElementName="association" MonikerTypeName="AssociationMoniker">
        <DomainRelationshipMoniker Name="Association" />
        <ElementData>
          <XmlPropertyData XmlName="sourceMultiplicity">
            <DomainPropertyMoniker Name="Association/SourceMultiplicity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetMultiplicity">
            <DomainPropertyMoniker Name="Association/TargetMultiplicity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sourcePropertyNameToTarget">
            <DomainPropertyMoniker Name="Association/SourcePropertyNameToTarget" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="targetPropertyNameToSource">
            <DomainPropertyMoniker Name="Association/TargetPropertyNameToSource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="foreignKeyConstraintName">
            <DomainPropertyMoniker Name="Association/ForeignKeyConstraintName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="idReference">
            <DomainPropertyMoniker Name="Association/IdReference" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="willCascadeOnDelete">
            <DomainPropertyMoniker Name="Association/WillCascadeOnDelete" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="Association/Description" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sourcePropertyNameToTargetInfo" Representation="Ignore">
            <DomainPropertyMoniker Name="Association/SourcePropertyNameToTargetInfo" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="removeAutomaticIndex">
            <DomainPropertyMoniker Name="Association/RemoveAutomaticIndex" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="MultipleAssociationTarget" MonikerAttributeName="" SerializeId="true" MonikerElementName="multipleAssociationTargetMoniker" ElementName="multipleAssociationTarget" MonikerTypeName="MultipleAssociationTargetMoniker">
        <DomainRelationshipMoniker Name="MultipleAssociationTarget" />
        <ElementData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="MultipleAssociationTarget/Description" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="StoreScript" MonikerAttributeName="" SerializeId="true" MonikerElementName="storeScriptMoniker" ElementName="storeScript" MonikerTypeName="StoreScriptMoniker">
        <DomainClassMoniker Name="StoreScript" />
        <ElementData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="StoreScript/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="storeQueries">
            <DomainRelationshipMoniker Name="StoreScriptHasStoreQueries" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="StoreQuery" MonikerAttributeName="" SerializeId="true" MonikerElementName="storeQueryMoniker" ElementName="storeQuery" MonikerTypeName="StoreQueryMoniker">
        <DomainClassMoniker Name="StoreQuery" />
        <ElementData>
          <XmlPropertyData XmlName="name">
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
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="StoreScriptHasStoreQueries" MonikerAttributeName="" SerializeId="true" MonikerElementName="storeScriptHasStoreQueriesMoniker" ElementName="storeScriptHasStoreQueries" MonikerTypeName="StoreScriptHasStoreQueriesMoniker">
        <DomainRelationshipMoniker Name="StoreScriptHasStoreQueries" />
      </XmlClassData>
      <XmlClassData TypeName="BusinessModelDesignerRootHasStoreScripts" MonikerAttributeName="" SerializeId="true" MonikerElementName="businessModelDesignerRootHasStoreScriptsMoniker" ElementName="businessModelDesignerRootHasStoreScripts" MonikerTypeName="BusinessModelDesignerRootHasStoreScriptsMoniker">
        <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasStoreScripts" />
      </XmlClassData>
      <XmlClassData TypeName="StoreScriptShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="storeScriptShapeMoniker" ElementName="storeScriptShape" MonikerTypeName="StoreScriptShapeMoniker">
        <CompartmentShapeMoniker Name="StoreScriptShape" />
        <ElementData>
          <XmlPropertyData XmlName="fillColor">
            <DomainPropertyMoniker Name="StoreScriptShape/FillColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineColor">
            <DomainPropertyMoniker Name="StoreScriptShape/OutlineColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="textColor">
            <DomainPropertyMoniker Name="StoreScriptShape/TextColor" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="StoreScriptShape/OutlineDashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineThickness">
            <DomainPropertyMoniker Name="StoreScriptShape/OutlineThickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="DbProvider" MonikerAttributeName="" SerializeId="true" MonikerElementName="dbProviderMoniker" ElementName="dbProvider" MonikerTypeName="DbProviderMoniker">
        <DomainClassMoniker Name="DbProvider" />
        <ElementData>
          <XmlPropertyData XmlName="server">
            <DomainPropertyMoniker Name="DbProvider/Server" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="catalog">
            <DomainPropertyMoniker Name="DbProvider/Catalog" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="userId">
            <DomainPropertyMoniker Name="DbProvider/UserId" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="password">
            <DomainPropertyMoniker Name="DbProvider/Password" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="windowsAuthentication">
            <DomainPropertyMoniker Name="DbProvider/WindowsAuthentication" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="DbProvider/Type" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDefault">
            <DomainPropertyMoniker Name="DbProvider/IsDefault" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="connectionName">
            <DomainPropertyMoniker Name="DbProvider/ConnectionName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="enableMigration">
            <DomainPropertyMoniker Name="DbProvider/EnableMigration" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="BusinessModelDesignerRootHasDbProviders" MonikerAttributeName="" SerializeId="true" MonikerElementName="businessModelDesignerRootHasDbProvidersMoniker" ElementName="businessModelDesignerRootHasDbProviders" MonikerTypeName="BusinessModelDesignerRootHasDbProvidersMoniker">
        <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasDbProviders" />
      </XmlClassData>
      <XmlClassData TypeName="DbProviderShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="dbProviderShapeMoniker" ElementName="dbProviderShape" MonikerTypeName="DbProviderShapeMoniker">
        <ImageShapeMoniker Name="DbProviderShape" />
      </XmlClassData>
      <XmlClassData TypeName="ReferenceModelClass" MonikerAttributeName="" SerializeId="true" MonikerElementName="referenceModelClassMoniker" ElementName="referenceModelClass" MonikerTypeName="ReferenceModelClassMoniker">
        <DomainClassMoniker Name="ReferenceModelClass" />
        <ElementData>
          <XmlPropertyData XmlName="modelClassReference">
            <DomainPropertyMoniker Name="ReferenceModelClass/ModelClassReference" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="referenceInfo" Representation="Ignore">
            <DomainPropertyMoniker Name="ReferenceModelClass/ReferenceInfo" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="hasReferenceError">
            <DomainPropertyMoniker Name="ReferenceModelClass/HasReferenceError" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="referenceProjectInfo" Representation="Ignore">
            <DomainPropertyMoniker Name="ReferenceModelClass/ReferenceProjectInfo" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ReferenceModelClassShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="referenceModelClassShapeMoniker" ElementName="referenceModelClassShape" MonikerTypeName="ReferenceModelClassShapeMoniker">
        <CompartmentShapeMoniker Name="ReferenceModelClassShape" />
      </XmlClassData>
      <XmlClassData TypeName="GeneralizationShConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="generalizationShConnectorMoniker" ElementName="generalizationShConnector" MonikerTypeName="GeneralizationShConnectorMoniker">
        <ConnectorMoniker Name="GeneralizationShConnector" />
        <ElementData>
          <XmlPropertyData XmlName="color">
            <DomainPropertyMoniker Name="GeneralizationShConnector/Color" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dashStyle">
            <DomainPropertyMoniker Name="GeneralizationShConnector/DashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="thickness">
            <DomainPropertyMoniker Name="GeneralizationShConnector/Thickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="GeneralizationSh" MonikerAttributeName="" SerializeId="true" MonikerElementName="generalizationShMoniker" ElementName="generalizationSh" MonikerTypeName="GeneralizationShMoniker">
        <DomainRelationshipMoniker Name="GeneralizationSh" />
        <ElementData>
          <XmlPropertyData XmlName="discriminator">
            <DomainPropertyMoniker Name="GeneralizationSh/Discriminator" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="GeneralizationSh/Description" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="RouteMapData" MonikerAttributeName="" SerializeId="true" MonikerElementName="routeMapDataMoniker" ElementName="routeMapData" MonikerTypeName="RouteMapDataMoniker">
        <DomainClassMoniker Name="RouteMapData" />
        <ElementData>
          <XmlPropertyData XmlName="path">
            <DomainPropertyMoniker Name="RouteMapData/Path" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClassHasDataRoutes" MonikerAttributeName="" SerializeId="true" MonikerElementName="classHasDataRoutesMoniker" ElementName="classHasDataRoutes" MonikerTypeName="ClassHasDataRoutesMoniker">
        <DomainRelationshipMoniker Name="ClassHasDataRoutes" />
      </XmlClassData>
      <XmlClassData TypeName="ModelImplementation" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelImplementationMoniker" ElementName="modelImplementation" MonikerTypeName="ModelImplementationMoniker">
        <DomainClassMoniker Name="ModelImplementation" />
        <ElementData>
          <XmlPropertyData XmlName="projectSuffix">
            <DomainPropertyMoniker Name="ModelImplementation/ProjectSuffix" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isSelected" Representation="Ignore">
            <DomainPropertyMoniker Name="ModelImplementation/IsSelected" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="modelInterface">
            <DomainRelationshipMoniker Name="ModelImplementationReferencesModelInterface" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelImplementationReferencesModelInterface" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelImplementationReferencesModelInterfaceMoniker" ElementName="modelImplementationReferencesModelInterface" MonikerTypeName="ModelImplementationReferencesModelInterfaceMoniker">
        <DomainRelationshipMoniker Name="ModelImplementationReferencesModelInterface" />
      </XmlClassData>
      <XmlClassData TypeName="ModelImplementationShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelImplementationShapeMoniker" ElementName="modelImplementationShape" MonikerTypeName="ModelImplementationShapeMoniker">
        <GeometryShapeMoniker Name="ModelImplementationShape" />
      </XmlClassData>
      <XmlClassData TypeName="ModelImplementationConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelImplementationConnectorMoniker" ElementName="modelImplementationConnector" MonikerTypeName="ModelImplementationConnectorMoniker">
        <ConnectorMoniker Name="ModelImplementationConnector" />
      </XmlClassData>
      <XmlClassData TypeName="WebApiController" MonikerAttributeName="" SerializeId="true" MonikerElementName="webApiControllerMoniker" ElementName="webApiController" MonikerTypeName="WebApiControllerMoniker">
        <DomainClassMoniker Name="WebApiController" />
        <ElementData>
          <XmlPropertyData XmlName="routePrefix">
            <DomainPropertyMoniker Name="WebApiController/RoutePrefix" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="projectSuffix">
            <DomainPropertyMoniker Name="WebApiController/ProjectSuffix" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="webApiActions">
            <DomainRelationshipMoniker Name="WebApiControllerHasWebApiActions" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="exposeAllContext">
            <DomainPropertyMoniker Name="WebApiController/ExposeAllContext" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isAspNetCore" Representation="Ignore">
            <DomainPropertyMoniker Name="WebApiController/IsAspNetCore" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="BusinessModelDesignerRootHasModelImplementations" MonikerAttributeName="" SerializeId="true" MonikerElementName="businessModelDesignerRootHasModelImplementationsMoniker" ElementName="businessModelDesignerRootHasModelImplementations" MonikerTypeName="BusinessModelDesignerRootHasModelImplementationsMoniker">
        <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasModelImplementations" />
      </XmlClassData>
      <XmlClassData TypeName="BusinessModelDesignerRootHasWebApiControllers" MonikerAttributeName="" SerializeId="true" MonikerElementName="businessModelDesignerRootHasWebApiControllersMoniker" ElementName="businessModelDesignerRootHasWebApiControllers" MonikerTypeName="BusinessModelDesignerRootHasWebApiControllersMoniker">
        <DomainRelationshipMoniker Name="BusinessModelDesignerRootHasWebApiControllers" />
      </XmlClassData>
      <XmlClassData TypeName="WebApiAction" MonikerAttributeName="" SerializeId="true" MonikerElementName="webApiActionMoniker" ElementName="webApiAction" MonikerTypeName="WebApiActionMoniker">
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
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="WebApiControllerHasWebApiActions" MonikerAttributeName="" SerializeId="true" MonikerElementName="webApiControllerHasWebApiActionsMoniker" ElementName="webApiControllerHasWebApiActions" MonikerTypeName="WebApiControllerHasWebApiActionsMoniker">
        <DomainRelationshipMoniker Name="WebApiControllerHasWebApiActions" />
      </XmlClassData>
      <XmlClassData TypeName="WebApiControllerShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="webApiControllerShapeMoniker" ElementName="webApiControllerShape" MonikerTypeName="WebApiControllerShapeMoniker">
        <CompartmentShapeMoniker Name="WebApiControllerShape" />
      </XmlClassData>
      <XmlClassData TypeName="ModelViewAssociation" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelViewAssociationMoniker" ElementName="modelViewAssociation" MonikerTypeName="ModelViewAssociationMoniker">
        <DomainRelationshipMoniker Name="ModelViewAssociation" />
        <ElementData>
          <XmlPropertyData XmlName="collectionName">
            <DomainPropertyMoniker Name="ModelViewAssociation/CollectionName" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelViewConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelViewConnectorMoniker" ElementName="modelViewConnector" MonikerTypeName="ModelViewConnectorMoniker">
        <ConnectorMoniker Name="ModelViewConnector" />
        <ElementData>
          <XmlPropertyData XmlName="color">
            <DomainPropertyMoniker Name="ModelViewConnector/Color" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dashStyle">
            <DomainPropertyMoniker Name="ModelViewConnector/DashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="thickness">
            <DomainPropertyMoniker Name="ModelViewConnector/Thickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="BusinessModelDesignerExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="GeneralizationBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="Generalization" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="MultipleAssociationOriginBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="MultipleAssociationOrigin" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="MultipleAssociation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="CommentReferencesSubjectsBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="CommentReferencesSubjects" />
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
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="MultipleAssociation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="AssociationBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="Association" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="MultipleAssociationTargetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="MultipleAssociationTarget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="MultipleAssociation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="GeneralizationShBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="GeneralizationSh" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ModelImplementationReferencesModelInterfaceBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ModelImplementationReferencesModelInterface" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelImplementation" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelInterface" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ModelViewAssociationBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ModelViewAssociation" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="6075066f-daf8-4b47-b2fb-14bb4b9efe45" Description="Business Models Diagram." Name="BusinessModelDesignerDiagram" DisplayName="Business Models Diagram" Namespace="Linx.BusinessModelDesigner">
    <Class>
      <DomainClassMoniker Name="BusinessModelDesignerRoot" />
    </Class>
    <ShapeMaps>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ModelClass" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasTypes.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ClassShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ClassShape/ModifiertMark" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ModelClass/Modifier" />
            </PropertyPath>
          </PropertyDisplayed>
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/Modifier" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="Abstract" />
              <PropertyFilter FilteringValue="Sealed" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/ModelClassMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/NotMapped" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="False" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/NoMapMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/NotMapped" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/NoLinksMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/HideAssociations" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/DimensionFilterMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/IsDimensionFilter" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/Multidimensional" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/Kind" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="Multidimensional" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/InStudy" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/InStudy" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/ViewMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/Kind" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="DatabaseView" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/ModelViewMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/Kind" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="ModelView" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/AggregationMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/ModelViewAggregation" />
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ClassShape/DbScriptMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelClass/Kind" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="DatabaseScript" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="ClassShape" />
        <CompartmentMap DisplaysCustomString="true">
          <CompartmentMoniker Name="ClassShape/AttributesCompartment" />
          <ElementsDisplayed>
            <DomainPath>ClassHasAttributes.Attributes/!Attribute</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="ClassShape/OperationsCompartment" />
          <ElementsDisplayed>
            <DomainPath>ClassHasOperations.Operations/!Operation</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="ClassShape/IndexesCompartiment" />
          <ElementsDisplayed>
            <DomainPath>ClassHasIndexes.ModelIndexes/!ModelIndex</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="ClassShape/DimensionRoutesCompartment" />
          <ElementsDisplayed>
            <DomainPath>ClassHasDataRoutes.RouteMapDatum/!RouteMapData</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ModelInterface" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasTypes.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="InterfaceShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="InterfaceShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="InterfaceShape/OperationsCompartment" />
          <ElementsDisplayed>
            <DomainPath>InterfaceHasOperation.Operations/!Operation</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Comment" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasComments.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CommentBoxShape/Comment" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Comment/Text" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CommentBoxShape/Title" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Comment/Title" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="CommentBoxShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="MultipleAssociation" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasTypes.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <ImageShapeMoniker Name="MultipleAssociationShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="DomainView" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasTypes.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="DomainViewShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="DomainViewShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="DomainViewShape/DomainValueCompartment" />
          <ElementsDisplayed>
            <DomainPath>DomainViewHasDomainValues.DomainValues/!DomainValue</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="DomainValue/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="StoreScript" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasStoreScripts.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
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
        <DomainClassMoniker Name="DbProvider" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasDbProviders.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="DbProviderShape/ConnectionName" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="DbProvider/ConnectionName" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="DbProviderShape/Type" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="DbProvider/Type" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="DbProviderShape/IsDefault" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="DbProvider/IsDefault" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <ImageShapeMoniker Name="DbProviderShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ReferenceModelClass" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasTypes.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ReferenceModelClassShape/Sterotype" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ReferenceModelClass/ReferenceInfo" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ReferenceModelClassShape/ReferenceError" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ReferenceModelClass/HasReferenceError" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ReferenceModelClassShape/ProjectSterotype" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ReferenceModelClass/ReferenceProjectInfo" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="ReferenceModelClassShape" />
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="ModelImplementation" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasModelImplementations.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ModelImplementationShape/IsSelected" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="ModelImplementation/IsSelected" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ModelImplementationShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="ModelImplementationShape" />
      </ShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="WebApiController" />
        <ParentElementPath>
          <DomainPath>BusinessModelDesignerRootHasWebApiControllers.BusinessModelDesignerRoot/!BusinessModelDesignerRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="WebApiControllerShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="WebApiControllerShape/ExposeAllContextMark" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="WebApiController/ExposeAllContext" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
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
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap>
        <ConnectorMoniker Name="MultipleAssociationRoleConnector" />
        <DomainRelationshipMoniker Name="MultipleAssociationOrigin" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="MultipleAssociationRoleConnector/TargetMultiplicity" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="MultipleAssociationOrigin/Multiplicity" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="MultipleAssociationRoleConnector/TargetRoleName" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="MultipleAssociationOrigin/CollectionName" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="CommentConnector" />
        <DomainRelationshipMoniker Name="CommentReferencesSubjects" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="GeneralizationConnector" />
        <DomainRelationshipMoniker Name="Generalization" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationConnector" />
        <DomainRelationshipMoniker Name="Association" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="AssociationConnector/SourceMultiplicity" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Association/SourceMultiplicity" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="AssociationConnector/SourcePropertyNameToTarget" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Association/SourcePropertyNameToTargetInfo" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="AssociationConnector/TargetMultiplicity" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Association/TargetMultiplicity" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="AssociationConnector/TargetPropertyNameToSource" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Association/TargetPropertyNameToSource" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationClassConnector" />
        <DomainRelationshipMoniker Name="MultipleAssociationTarget" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="GeneralizationShConnector" />
        <DomainRelationshipMoniker Name="GeneralizationSh" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="GeneralizationShConnector/TextDiscriminator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="GeneralizationSh/Discriminator" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="ModelImplementationConnector" />
        <DomainRelationshipMoniker Name="ModelImplementationReferencesModelInterface" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="ModelViewConnector" />
        <DomainRelationshipMoniker Name="ModelViewAssociation" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="ModelViewConnector/CollectionNameDiscriminator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ModelViewAssociation/CollectionName" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="bmd" EditorGuid="cd72f27f-08bd-433e-a571-f01ced07858e">
    <RootClass>
      <DomainClassMoniker Name="BusinessModelDesignerRoot" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="BusinessModelDesignerSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="Business Model Diagrams">
      <ElementTool Name="ModelClass" ToolboxIcon="Resources\ClassTool.bmp" Caption="Class" Tooltip="" HelpKeyword="ModelClassF1Keyword">
        <DomainClassMoniker Name="ModelClass" />
      </ElementTool>
      <ElementTool Name="ModelInterface" ToolboxIcon="Resources\InterfaceTool.bmp" Caption="Contract (Interface)" Tooltip="" HelpKeyword="ModelInterfaceF1Keyword">
        <DomainClassMoniker Name="ModelInterface" />
      </ElementTool>
      <ConnectionTool Name="Association" ToolboxIcon="Resources\AssociationTool.bmp" Caption="Association" Tooltip="" HelpKeyword="">
        <ConnectionBuilderMoniker Name="BusinessModelDesigner/AssociationBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="Generalization" ToolboxIcon="resources\generalizationtool.bmp" Caption="Inheritance" Tooltip="" HelpKeyword="GeneralizationF1Keyword" ReversesDirection="true">
        <ConnectionBuilderMoniker Name="BusinessModelDesigner/GeneralizationBuilder" />
      </ConnectionTool>
      <ElementTool Name="MultipleAssociation" ToolboxIcon="resources\multipleassociationtool.bmp" Caption="Multiple Association" Tooltip="" HelpKeyword="MultipleAssociationF1Keyword">
        <DomainClassMoniker Name="MultipleAssociation" />
      </ElementTool>
      <ConnectionTool Name="MultipleAssociationOrigin" ToolboxIcon="Resources\AssociationLinkTool.bmp" Caption="Multiple Association Origin" Tooltip="" HelpKeyword="MultipleAssociationOriginF1Keyword">
        <ConnectionBuilderMoniker Name="BusinessModelDesigner/MultipleAssociationOriginBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="MultipleAssociationTarget" ToolboxIcon="Resources\AssociationClassTool.bmp" Caption="Multiple Association Target" Tooltip="" HelpKeyword="MultipleAssociationTargetF1Keyword">
        <ConnectionBuilderMoniker Name="BusinessModelDesigner/MultipleAssociationTargetBuilder" />
      </ConnectionTool>
      <ElementTool Name="Comment" ToolboxIcon="resources\commenttool.bmp" Caption="Comment" Tooltip="" HelpKeyword="CommentF1Keyword">
        <DomainClassMoniker Name="Comment" />
      </ElementTool>
      <ConnectionTool Name="CommentsReferenceTypes" ToolboxIcon="resources\commentlinktool.bmp" Caption="Comment Link" Tooltip="" HelpKeyword="">
        <ConnectionBuilderMoniker Name="BusinessModelDesigner/CommentReferencesSubjectsBuilder" />
      </ConnectionTool>
      <ElementTool Name="DomainViewTool" ToolboxIcon="Resources\DomainView.bmp" Caption="Domain Values" Tooltip="" HelpKeyword="DomainViewTool">
        <DomainClassMoniker Name="DomainView" />
      </ElementTool>
      <ElementTool Name="StoreScriptTool" ToolboxIcon="Resources\StoreScriptTool.bmp" Caption="Native Script" Tooltip="" HelpKeyword="StoreScriptTool">
        <DomainClassMoniker Name="StoreScript" />
      </ElementTool>
      <ElementTool Name="DbProviderTool" ToolboxIcon="Resources\DbProviderTool.bmp" Caption="Provider" Tooltip="" HelpKeyword="DbProviderTool">
        <DomainClassMoniker Name="DbProvider" />
      </ElementTool>
      <ConnectionTool Name="GeneralizationShared" ToolboxIcon="resources\generalizationshtool.bmp" Caption="Inheritance Shared Table" Tooltip="" HelpKeyword="GeneralizationSharedF1Keyword" ReversesDirection="true">
        <ConnectionBuilderMoniker Name="BusinessModelDesigner/GeneralizationShBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="AssociationImplementationTool" ToolboxIcon="Resources\AssociationImplementationTool.bmp" Caption="Implementation" Tooltip="Association Implementation Tool" HelpKeyword="AssociationImplementationTool">
        <ConnectionBuilderMoniker Name="BusinessModelDesigner/ModelImplementationReferencesModelInterfaceBuilder" />
      </ConnectionTool>
      <ElementTool Name="WebApiControllerTool" ToolboxIcon="Resources\WebApiControllerTool.bmp" Caption="Web API Controller" Tooltip="Web Api Controller Tool" HelpKeyword="WebApiControllerTool">
        <DomainClassMoniker Name="WebApiController" />
      </ElementTool>
      <ElementTool Name="ModelImplementationTool" ToolboxIcon="Resources\ModelImplementationTool.bmp" Caption="Contract Implementation" Tooltip="Model Implementation Tool" HelpKeyword="ModelImplementationTool">
        <DomainClassMoniker Name="ModelImplementation" />
      </ElementTool>
      <ConnectionTool Name="CollectionAssociationTool" ToolboxIcon="Resources\CollectionAssociationTool.bmp" Caption="Collection Association" Tooltip="" HelpKeyword="">
        <ConnectionBuilderMoniker Name="BusinessModelDesigner/ModelViewAssociationBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <Validation UsesMenu="true" UsesOpen="false" UsesSave="true" UsesCustom="true" UsesLoad="false" />
    <DiagramMoniker Name="BusinessModelDesignerDiagram" />
  </Designer>
  <Explorer ExplorerGuid="0d254e07-0be2-4043-8981-9f415878e786" Title="">
    <ExplorerBehaviorMoniker Name="BusinessModelDesigner/BusinessModelDesignerExplorer" />
  </Explorer>
</Dsl>