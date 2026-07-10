using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.BusinessModelDesigner.CustomizedCode.Util
{
    [DataContract]
    public class EntityQueryNode
    {
        public EntityQueryNode()
        {
            Key = Guid.NewGuid();
            Joins = new List<EntityQueryNode>();
            Properties = new List<EntityQueryProperty>();
            Relations = new List<EntityQueryRelation>();
        }

        [DataMember]
        public Guid Key { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Alias { get; set; }
        [DataMember]
        public string WhereClause { get; set; }
        [DataMember]
        public bool Updatable { get; set; }
        [DataMember]
        public QueryNodeType RelationType { get; set; }
        [DataMember]
        public List<EntityQueryNode> Joins { get; set; }
        [DataMember]
        public List<EntityQueryProperty> Properties { get; set; }
        [DataMember]
        public List<EntityQueryRelation> Relations { get; set; }
        [IgnoreDataMember]
        public EntityQueryNode Parent { get; set; }
        [DataMember]
        public bool JustFirstRightRelation { get; set; }

        internal void SyncPropertiesWithView(ModelClass _entity, bool updateFormulas, bool createFormulas, bool syncData)
        {
            if (updateFormulas || createFormulas)
            {
                //Sync model view formulas
                foreach (var attr in _entity.Attributes.Where(e => !e.ModelViewFormula.IsNullOrEmpty()).ToArray())
                {
                    var prop = this.Properties.FirstOrDefault(e => e.Name == attr.Name);
                    if (prop == null && createFormulas)
                    {
                        this.Properties.Add(new EntityQueryProperty()
                        {
                            Name = attr.Name,
                            DisplayName = attr.DisplayName,
                            Formula = attr.ModelViewFormula,
                            DomainName = attr.DomainName,
                            Nullable = attr.IsNullable,
                            PrimaryKey = attr.IsPrimaryKey,
                            Selected = true,
                            SourceName = "",
                            Type = attr.DataType,
                            Precision = attr.Precision,
                            Scale = attr.Scale,
                            MaxLength = attr.MaxLength
                        });
                    }
                    else if (prop != null && updateFormulas)
                    {
                        prop.SourceName = "";
                        prop.Formula = attr.ModelViewFormula;
                        prop.DomainName = attr.DomainName;
                        prop.Type = attr.DataType;
                        prop.PrimaryKey = attr.IsPrimaryKey;
                        prop.Nullable = attr.IsNullable;
                        prop.Precision = attr.Precision;
                        prop.Scale = attr.Scale;
                        prop.MaxLength = attr.MaxLength;
                        prop.Selected = true;
                    }
                }
            }

            if (syncData)
            {
                //Sync data properties
                foreach (var prop in this.Properties.Where(e => e.Formula.IsNullOrEmpty()).ToArray())
                {
                    var vProp = _entity.Attributes.FirstOrDefault(e => e.ModelViewSource == (this.Name + "." + prop.SourceName + "(" + this.Key.ToString() + ")"));
                    prop.Selected = (vProp != null);
                    if (prop.Selected)
                    {
                        prop.Name = vProp.Name;
                        prop.DisplayName = vProp.DisplayName;
                    }
                }
            }
        }

        internal string GetJoinLeftRelation()
        {
            string body = "";

            if (this.RelationType != QueryNodeType.Entity && this.Parent != null)
            {
                if (this.Relations.Count == 1)
                {
                    return this.Relations[0].SourceExpression.Replace("this.", this.Parent.Alias + ".");
                }
                else
                {
                    for (int idx = 0; idx < this.Relations.Count; idx++)
                    {
                        string typeAdjust = "";
                        if (this.Joins.Count > 0 && this.Parent != null)
                        {
                            var sourceProp = this.Parent.Properties.FirstOrDefault(e => ("this." + e.SourceName) == this.Relations[idx].SourceExpression);
                            var targetProp = this.Joins[0].Properties.FirstOrDefault(e => ("this." + e.SourceName) == this.Relations[idx].TargetExpression);                            
                            if (sourceProp != null && targetProp != null && !sourceProp.Nullable && targetProp.Nullable)
                            {
                                string attrDataType = ModelAttribute.GetAttribueDataType(sourceProp.Type, false);
                                if (!attrDataType.ToLower().Contains("string"))
                                    typeAdjust = "(" + attrDataType + "?)";
                            }
                        }

                        body += "Prop" + idx.ToString() + " = " + typeAdjust + this.Relations[idx].SourceExpression.Replace("this.", this.Parent.Alias + ".") + (idx == this.Relations.Count - 1 ? "" : ", ");
                    }
                }
            }

            return "new { " + (String.IsNullOrWhiteSpace(body) ? "Prop = 1" : body) + " }";
        }

        internal string GetJoinRightRelation()
        {
            string body = "";

            if (this.RelationType != QueryNodeType.Entity && this.Joins.Count > 0)
            {
                if (this.Relations.Count == 1)
                {
                    return this.Relations[0].TargetExpression.Replace("this.", this.Joins[0].Alias + (this.RelationType == QueryNodeType.LeftJoin ? "LF" : "") + ".");
                }
                else
                {
                    for (int idx = 0; idx < this.Relations.Count; idx++)
                    {
                        string typeAdjust = "";
                        if (this.Joins.Count > 0 && this.Parent != null)
                        {
                            var sourceProp = this.Parent.Properties.FirstOrDefault(e => ("this." + e.SourceName) == this.Relations[idx].SourceExpression);
                            var targetProp = this.Joins[0].Properties.FirstOrDefault(e => ("this." + e.SourceName) == this.Relations[idx].TargetExpression);                            
                            if (sourceProp != null && targetProp != null && sourceProp.Nullable && !targetProp.Nullable)
                            {
                                string attrDataType = ModelAttribute.GetAttribueDataType(targetProp.Type, false);
                                if (!attrDataType.ToLower().Contains("string"))
                                    typeAdjust = "(" + attrDataType + "?)";
                            }
                        }

                        body += "Prop" + idx.ToString() + " = " + typeAdjust + this.Relations[idx].TargetExpression.Replace("this.", this.Joins[0].Alias + (this.RelationType == QueryNodeType.LeftJoin ? "LF" : "") + ".") + (idx == this.Relations.Count - 1 ? "" : ", ");
                    }
                }
            }

            return "new { " + (String.IsNullOrWhiteSpace(body) ? "Prop = 1" : body) + " }";
        }
    }

    [DataContract]
    public class EntityQueryProperty
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SourceName { get; set; }
        [DataMember]
        public string Formula { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string DomainName { get; set; }
        [DataMember]
        public ModelDataType Type { get; set; }
        [DataMember]
        public bool PrimaryKey { get; set; }
        [DataMember]
        public bool Nullable { get; set; }
        [DataMember]
        public bool Selected { get; set; }
        [DataMember]
        public short Precision { get; set; }
        [DataMember]
        public short Scale { get; set; }
        [DataMember]
        public int MaxLength { get; set; }  
        

        public bool IsComposedKey()
        {
            return (!this.Formula.IsNullOrEmpty() & this.Formula.StartsWith("KEY("));
        }
    }

    [DataContract]
    public class EntityQueryRelation
    {
        public EntityQueryRelation()
        {
            this.Operator = "==";
        }

        [DataMember]
        public string SourceExpression { get; set; }
        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public string TargetExpression { get; set; }
    }

    public enum QueryNodeType
    {
        Entity,
        InnerJoin,
        LeftJoin
    }
}
