using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.BusinessDataModelDesigner.CustomizedCode.Util
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
        public string TableName { get; set; }
        [DataMember]
        public string Schema { get; set; }
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
        
        public string ReplaceWhereClause(string aliasWrapper)
        {
            return this.ReplaceExpression(this.WhereClause, aliasWrapper);            
        }

        public string GetTableName(bool removeSchema = false, string aliasWrapper = "")
        {
            return (removeSchema ? "" : aliasWrapper + (this.Schema.IsNullOrEmpty() ? "dbo" : this.Schema) + aliasWrapper + ".") + aliasWrapper + this.TableName + aliasWrapper;
        }


        internal string ReplaceExpression(string expression, string aliasWrapper)
        {
            string exp = expression;
            if (aliasWrapper.IsNullOrEmpty())
            {
                exp = exp.Replace("this.", this.Alias + ".");
            }
            else
            {
                foreach (var prop in this.Properties)
                {
                    exp = exp.Replace("this." + prop.SourceName, aliasWrapper + this.Alias + aliasWrapper + "." + aliasWrapper + prop.SourceName + aliasWrapper);
                }
            }
            return exp;
        }

        internal string[] GetJoinLeftRelation(string aliasWrapper, EntityQueryNode propOwner)
        {
            List<string> body = new List<string>();

            if (this.RelationType != QueryNodeType.Entity && this.Parent != null)
            {
                for (int idx = 0; idx < this.Relations.Count; idx++)
                {
                    body.Add(propOwner.ReplaceExpression(this.Relations[idx].SourceExpression, aliasWrapper));
                }
            }

            return body.ToArray();
        }

        internal string[] GetJoinRightRelation(string aliasWrapper, EntityQueryNode propOwner)
        {
            List<string> body = new List<string>();

            if (this.RelationType != QueryNodeType.Entity && this.Joins.Count > 0)
            {
                for (int idx = 0; idx < this.Relations.Count; idx++)
                {
                    body.Add(propOwner.ReplaceExpression(this.Relations[idx].TargetExpression, aliasWrapper));
                }
            }

            return body.ToArray();
        }

        internal string GetJoinRelation(string aliasWrapper, EntityQueryNode rightEntity)
        {
            EntityQueryNode leftEntity = this.Parent;
            string body = "";
            var leftExpr = this.GetJoinLeftRelation(aliasWrapper, leftEntity);
            var rightExp = this.GetJoinRightRelation(aliasWrapper, rightEntity);

            if (leftExpr.Length > 0 && leftExpr.Length == rightExp.Length)
            {
                for (int idx = 0; idx < leftExpr.Length; idx++)
                {
                    body += (body.IsNullOrEmpty() ? "" : " AND ") + leftExpr[idx] + " = " + rightExp[idx];
                }
            }
            else
            {
                body = "1 = 1";
            }
            return "(" + body + ")";
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
