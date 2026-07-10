using Linx.Tools.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Linx.BusinessDataModelDesigner.AppUI.Model
{
    public static class DatabaseExtension
    {
        public static TreeNode GetTreeNode(this DbInfo info)
        {
            var node = new TreeNode()
            {
                Text = info.Name,
                Tag = info
            };

            DecorDB(node, info.GetType().Name);

            return node;
        }

        public static IEnumerable<DbInfo> GetChildren(this DbInfo info)
        {
            switch (info.GetType().Name.ToLower())
            {
                case "database":
                    return ((Database)info).Schemas;
                case "schema":
                    return ((Schema)info).TablesBase;
                default:
                    return null;
            }

        }

        private static void DecorDB(TreeNode node, string typeName)
        {
            string acronym = string.Empty;

            switch (typeName.ToLower())
            {
                case "database":
                    acronym = "DB";
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    break;
                case "schema":
                    acronym = "SC";
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    break;
                case "table":
                    acronym = "TB";
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                    break;
                case "view":
                    acronym = "VW";
                    node.ImageIndex = 3;
                    node.SelectedImageIndex = 3;
                    break;
                case "column":
                    acronym = "COL";
                    break;
                default:
                    break;
            }

            //  node.Text += "(" + acronym + ")";
        }

        public static Table FindTable<T>(this Database database, T objectId)
        {
            return database.Schemas.SelectMany(s => s.TablesBase.OfType<Table>()).
                SingleOrDefault(t => ((T)t.Id).Equals(objectId));
        }

        public static T[] Find<T>(this IEnumerable<T> list, Func<T, IEnumerable<T>> childrenList, Func<T, bool> predicate, bool findFirst = false)
        {
            List<T> listFound = new List<T>();
            Action<T> find = null;
            find = item =>
            {
                if (!(findFirst && listFound.Count > 0))
                {
                    if (predicate(item))
                        listFound.Add(item);

                    childrenList(item).ToList().ForEach(find);
                }
            };

            list.ToList().ForEach(find);

            return listFound.ToArray();
        }
    }
}
