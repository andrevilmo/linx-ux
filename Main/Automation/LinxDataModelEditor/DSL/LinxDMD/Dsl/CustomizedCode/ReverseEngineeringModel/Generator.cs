using Linx.Tools.Migration;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Linx.BusinessDataModelDesigner.AppUI.Model
{
    public class Generator
    {
        const string TagName = "Name";

        XmlDocument xDoc;
        XmlElement entities;
        IEnumerable<TableBase> listTable;
        public Generator(IEnumerable<TableBase> tables)
        {
            xDoc = new XmlDocument();

            listTable = tables;

            entities = xDoc.CreateElement("Entities");

            foreach (TableBase t in tables)
                entities.AppendChild(GetXmlTable(t));

            xDoc.AppendChild(entities);
        }

        private XmlElement GetXmlTable(TableBase table)
        {
            XmlElement elem = xDoc.CreateElement("Entity");
            //add table informations
            GetTableInfo(table, elem);
            //add columns informations
            elem.AppendChild(GetColumnsInfo(table));

            //add foreign keys
            if (table.IsTable)
                elem.AppendChild(GetForeignKeysInfo(table as Table));

            return elem;
        }

        private XmlElement GetForeignKeysInfo(Table table)
        {
            var foreignKeys = xDoc.CreateElement("ForeignKeys");

            foreach (var foreignKey in table.ForeignKey.Where(fk => listTable.Contains(fk.Referenced)))
                foreignKeys.AppendChild(GetForeignKeyInfo(foreignKey));

            return foreignKeys;
        }

        private XmlElement GetForeignKeyInfo(ForeignKey foreignKey)
        {
            var foreingKeyNode = xDoc.CreateElement("ForeignKey");

            var originProperties = string.Join(",",
                foreignKey.ForeignKeyColumns.Select(f => f.ReferencedColumn.Name));
            var selfProperties = string.Join(",",
                foreignKey.ForeignKeyColumns.Select(f => f.ParentColumn.Name));

            foreingKeyNode.AddAttribute(TagName, foreignKey.Name);
            foreingKeyNode.AddAttribute("OriginEntityName", foreignKey.Referenced.Name);
            foreingKeyNode.AddAttribute("OriginProperties", originProperties);
            foreingKeyNode.AddAttribute("SelfProperties", selfProperties);

            return foreingKeyNode;
        }

        private XmlElement GetColumnsInfo(TableBase table)
        {
            var properties = xDoc.CreateElement("Properties");

            foreach (var column in table.Columns)
                properties.AppendChild(GetColumnInfo(column));


            return properties;
        }

        private XmlElement GetColumnInfo(Column column)
        {
            var columnNode = xDoc.CreateElement("Column");

            columnNode.AddAttribute(TagName, column.Name);
            columnNode.AddAttribute("IsPK", column.IsPK);
            columnNode.AddAttribute("DataTypeStore", column.DbDataType);
            columnNode.AddAttribute("DataType", column.DataType.Name);
            columnNode.AddAttribute("Precision", column.Precision);
            columnNode.AddAttribute("Scale", column.Scale);
            columnNode.AddAttribute("IsNull", column.IsNullable);
            columnNode.AddAttribute("MaxLength", column.MaxLength);

            return columnNode;
        }

        private void GetTableInfo(TableBase table, XmlElement elem)
        {
            elem.AddAttribute(TagName, table.Name);
            elem.AddAttribute("Type", table.IsTable ? "T" : "V");
            if (table is Table && ((Table)table).PrimaryKey != null)
                elem.AddAttribute("PrimaryKeyName", ((Table)table).PrimaryKey.Name);
            elem.AddAttribute("Schema", table.Schema.Name);
        }


        internal string GetGeneratedString()
        {
            using (var textWriter = new System.IO.StringWriter())
            {
                xDoc.Save(textWriter);

                return textWriter.ToString();
            }
        }
    }

    public static class XmlExtensions
    {
        public static void AddAttribute(this XmlElement element, string name, object value)
        {
            var attr = element.OwnerDocument.CreateAttribute(name);
            attr.Value = value.ToString();

            element.Attributes.Append(attr);
        }
    }
}
