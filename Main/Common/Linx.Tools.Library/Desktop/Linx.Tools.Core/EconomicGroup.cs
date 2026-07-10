using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
//using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Linx.Tools
{
    /// <summary>
    /// Economic Group
    /// </summary>
    public class EconomicGroup : INotifyPropertyChanged
    {

        private static string economicGroupCurrentFile = "CurrentEconomicGroup.info";
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }
        public EconomicGroup Parent { get; set; }
        List<EconomicGroup> subgroups;
        public List<EconomicGroup> Subgroups
        {
            get
            {
                if (subgroups == null)
                    subgroups = new List<EconomicGroup>();
                return subgroups;
            }
        }

        private bool isExpanded = false;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
                if (isSelected && Parent != null && !Parent.IsExpanded)
                    ExpandBranch(Parent);
            }
        }

        private void ExpandBranch(EconomicGroup node)
        {
            node.IsExpanded = true;
            if (node.Parent != null)
                ExpandBranch(node.Parent);
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion



        public static int RestoreEconomicGroup()
        {
            string currentGroup = SerializationManager<string>.Retrieve(economicGroupCurrentFile);
            if (!currentGroup.IsNull() && currentGroup.IsNumeric())
                return int.Parse(currentGroup);
            else
                return 0;
        }

        public void Save()
        {
            SerializationManager<string>.Store(economicGroupCurrentFile, this.Id.ToString());
        }

        /// <summary>
        /// Find a economic group by description. 
        /// </summary>
        /// <param name="list">List with all top economic groups.</param>
        /// <param name="description">Description of one economic group</param>
        /// <returns></returns>
        public static EconomicGroup Find(List<EconomicGroup> list, string description)
        {
            EconomicGroup result = null;
            Action<EconomicGroup> finder = null;

            finder = (e) =>
            {
                if (result == null)
                {

                    if (e.Description == description)
                    {
                        result = e;
                    }
                    else
                    {
                        e.Subgroups.ForEach(r => finder(r));
                    }
                }
            };

            list.ForEach(r => finder(r));

            return result;
        }

        /// <summary>
        /// Find a economic group by id. 
        /// </summary>
        /// <param name="list">List with all top economic groups.</param>
        /// <param name="description">Id of one economic group</param>
        /// <returns></returns>
        public static EconomicGroup Find(List<EconomicGroup> list, int id)
        {
            EconomicGroup result = null;
            Action<EconomicGroup> finder = null;

            finder = (e) =>
            {
                if (result == null)
                {

                    if (e.Id == id)
                    {
                        result = e;
                    }
                    else
                    {
                        e.Subgroups.ForEach(r => finder(r));
                    }
                }
            };

            list.ForEach(r => finder(r));

            return result;
        }

        public static List<EconomicGroup> Clone(List<EconomicGroup> list)
        {
            List<EconomicGroup> result = new List<EconomicGroup>();
            Action<EconomicGroup, EconomicGroup> reentrantCloner = null;

            reentrantCloner = (e, c) =>
            {
                e.Subgroups.ForEach(r =>
                {
                    EconomicGroup clone = new EconomicGroup() { Description = r.Description, Id = r.Id, ImagePath = r.ImagePath, IsExpanded = r.IsExpanded, IsSelected = r.IsSelected, Parent = c };
                    c.Subgroups.Add(clone);
                    reentrantCloner(r, clone);
                }
            );
            };

            list.ForEach(r =>
            {
                EconomicGroup clone = new EconomicGroup() { Description = r.Description, Id = r.Id, ImagePath = r.ImagePath, IsExpanded = r.IsExpanded, IsSelected = r.IsSelected, Parent = null };
                result.Add(clone);
                reentrantCloner(r, clone);
            }
            );

            return result;
        }

        /// <summary>
        /// Get all terminations of one economic group.
        /// </summary>
        /// <param name="group">Root economic group.</param>
        /// <returns></returns>
        public static List<EconomicGroup> GetTerminationsOfTheBranch(EconomicGroup group)
        {
            List<EconomicGroup> result = new List<EconomicGroup>();
            Action<EconomicGroup> finder = null;

            finder = (e) =>
            {
                if (e.Subgroups.Count == 0)
                {
                    result.Add(e);
                }
                else
                {
                    e.Subgroups.ForEach(r => finder(r));
                }
            };

            finder(group);

            return result;
        }

        public bool IsValid(object entity)
        {
            if (entity == null)
                return false;
            else 
                return IsValid(entity.GetType());
        }

        public bool IsValid(Type type)
        {
            if (type == null)
                return false;
            
            return (type.GetProperty("IdGpecon") != null && !(Linx.Tools.ObjectExtension.ExistsAttributeOnProperty(type, "IdGpecon", typeof(KeyAttribute))));
        }

        /// <summary>
        /// Get entity economic group search from a business entity.
        /// </summary>
        /// <param name="entity">Business object.</param>
        /// <returns></returns>
        public EntitySearch ReadQueryFromEconomicGroup(object entity)
        {
            if (entity == null)
                return null;
            else 
                return ReadQueryFromEconomicGroup(entity.GetType());
        }


        /// <summary>
        /// Get entity economic group search from a type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public EntitySearch ReadQueryFromEconomicGroup(Type type)
        {
            if (!this.IsValid(type))
                return null;

            string eGroups = ReadEconomicGroups();            
            if (eGroups.IsNullOrEmpty())
                return null;

            EntitySearch currentDTO = new EntitySearch(type.Name);
            currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGpecon"));
            currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "In"));
            currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, eGroups));

            return currentDTO;
        }

        /// <summary>
        /// Get economic group list.
        /// </summary>
        /// <returns></returns>
        public string ReadEconomicGroups()
        {
            string eGroups = String.Empty;
            foreach (EconomicGroup group in EconomicGroup.GetTerminationsOfTheBranch(this))
            {
                eGroups += (eGroups.IsNullOrEmpty() ? String.Empty : ",") + group.Id.ToString();
            }                        
            return eGroups;
        }

    }
}
