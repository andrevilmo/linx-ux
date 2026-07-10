using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using System.Collections;
using System.Text.RegularExpressions;

namespace Linx.LinqExtensions.Query
{
    public static class GeneralUtils
    {
        /// <summary>
        /// Returns whether string contains any of the specified keywords as a substring.
        /// </summary>
        /// <param name="value">Value of the string</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains any of the specified keywords</returns>
        [MethodExpander(typeof(StringExpandContainsAny))]
        public static bool ContainsAny(this string value, params string[] keywords)
        {
            return keywords.Any((s) => value.Contains(s));
        }


        /// <summary>
        /// Returns whether string contains all of the specified keywords as a substring.
        /// </summary>
        /// <param name="value">Value of the string</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains all of the specified keywords</returns>
        [MethodExpander(typeof(StringExpandContainsAll))]
        public static bool ContainsAll(this string value, params string[] keywords)
        {
            return keywords.All((s) => value.Contains(s));
        }


        /// <summary>
        /// Returns whether int contains any of the specified keywords as a value.
        /// </summary>
        /// <param name="value">Value of the int</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains any of the specified keywords</returns>
        [MethodExpander(typeof(ExpanderIn))]
        public static bool In(this int value, params int[] keywords)
        {
            return keywords.Any((s) => value.Equals(s));
        }

        // <summary>
        /// Returns whether int contains any of the specified keywords as a value.
        /// </summary>
        /// <param name="value">Value of the Guid</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains any of the specified keywords</returns>
        [MethodExpander(typeof(ExpanderIn))]
        public static bool In(this Guid value, params Guid[] keywords)
        {
            return keywords.Any((s) => value.Equals(s));
        }

        // <summary>
        /// Returns whether int contains any of the specified keywords as a value.
        /// </summary>
        /// <param name="value">Value of the String</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains any of the specified keywords</returns>
        [MethodExpander(typeof(ExpanderIn))]
        public static bool In(this String value, params String[] keywords)
        {
            return keywords.Any((s) => value.Equals(s));
        }

        // <summary>
        /// Returns whether int contains any of the specified keywords as a value.
        /// </summary>
        /// <param name="value">Value of the System.Byte</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains any of the specified keywords</returns>
        [MethodExpander(typeof(ExpanderIn))]
        public static bool In(this System.Byte value, params System.Byte[] keywords)
        {
            return keywords.Any((s) => value.Equals(s));
        }

        // <summary>
        /// Returns whether int contains any of the specified keywords as a value.
        /// </summary>
        /// <param name="value">Value of the System.Int16</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains any of the specified keywords</returns>
        [MethodExpander(typeof(ExpanderIn))]
        public static bool In(this System.Int16 value, params System.Int16[] keywords)
        {
            return keywords.Any((s) => value.Equals(s));
        }

        // <summary>
        /// Returns whether int contains any of the specified keywords as a value.
        /// </summary>
        /// <param name="value">Value of the System.Int64</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains any of the specified keywords</returns>
        [MethodExpander(typeof(ExpanderIn))]
        public static bool In(this System.Int64 value, params System.Int64[] keywords)
        {
            return keywords.Any((s) => value.Equals(s));
        }

    }

    public static class Linq
    {
        /// <summary>
        /// Utility function for building expression trees for lambda functions
        /// that return C# anonymous type as a result (because you can't declare 
        /// it using Expression&lt;Func&lt;...&gt;&gt; syntax)
        /// </summary>
        public static Expression<Func<T, R>> Expr<T, R>(Expression<Func<T, R>> f)
        {
            return f;
        }

        /// <summary>
        /// Utility function for building expression trees for lambda functions
        /// that return C# anonymous type as a result (because you can't declare 
        /// it using Expression&lt;Func&lt;...&gt;&gt; syntax)
        /// </summary>
        public static Expression<Func<T0, T1, R>> Expr<T0, T1, R>(Expression<Func<T0, T1, R>> f)
        {
            return f;
        }

        /// <summary>
        /// Utility function for building expression trees for lambda functions
        /// that return C# anonymous type as a result (because you can't declare 
        /// it using Expression&lt;Func&lt;...&gt;&gt; syntax)
        /// </summary>
        public static Expression<Func<T0, T1, T2, R>> Expr<T0, T1, T2, R>(Expression<Func<T0, T1, T2, R>> f)
        {
            return f;
        }

        /// <summary>
        /// Utility function for building expression trees for lambda functions
        /// that return C# anonymous type as a result (because you can't declare 
        /// it using Expression&lt;Func&lt;...&gt;&gt; syntax)
        /// </summary>
        public static Expression<Func<T0, T1, T2, T3, R>> Expr<T0, T1, T2, T3, R>(Expression<Func<T0, T1, T2, T3, R>> f)
        {
            return f;
        }

        /// <summary>
        /// Utility function for building delegates for lambda functions
        /// that return C# anonymous type as a result (because you can't declare 
        /// it using delegates&lt;Func&lt;...&gt;&gt; syntax)
        /// </summary>
        public static Func<T, R> Func<T, R>(Func<T, R> f)
        {
            return f;
        }

        /// <summary>
        /// Utility function for building delegates for lambda functions
        /// that return C# anonymous type as a result (because you can't declare 
        /// it using delegates&lt;Func&lt;...&gt;&gt; syntax)
        /// </summary>
        public static Func<T0, T1, R> Func<T0, T1, R>(Func<T0, T1, R> f)
        {
            return f;
        }

        /// <summary>
        /// Utility function for building delegates for lambda functions
        /// that return C# anonymous type as a result (because you can't declare 
        /// it using delegates&lt;Func&lt;...&gt;&gt; syntax)
        /// </summary>
        public static Func<T0, T1, T2, R> Func<T0, T1, T2, R>(Func<T0, T1, T2, R> f)
        {
            return f;
        }

        /// <summary>
        /// Utility function for building delegates for lambda functions
        /// that return C# anonymous type as a result (because you can't declare 
        /// it using delegates&lt;Func&lt;...&gt;&gt; syntax)
        /// </summary>
        public static Func<T0, T1, T2, T3, R> Func<T0, T1, T2, T3, R>(Func<T0, T1, T2, T3, R> f)
        {
            return f;
        }
    }

    #region MethodExpanders to String

    abstract class StringArrayExpanderBase : IMethodExpander
    {
        protected MethodInfo contMeth;
        protected Expression selfRef;
        protected abstract string MethodName { get; }
        protected abstract Func<Expression, string, Expression> Agg { get; }

        public Expression Expand(Expression selfRef, IEnumerable<Expression> parameters)
        {
            this.selfRef = selfRef;
            string[] vals;
            try
            {
                vals = (string[])ExpressionExpander.Evaluate(parameters.First());
                if (vals.Length == 0) throw new Exception();
            }
            catch
            {
                throw new ArgumentException(string.Format("First argument for the '{0}' method must be non empty string array!", MethodName));
            }

            // Init parameters
            contMeth = typeof(string).GetMethod("Contains");

            // Combine using And when method is ContainsAll or using Or when method is ConainsAny
            var init = Expression.Call(selfRef, contMeth, Expression.Constant(vals[0]));
            return vals.Skip(1).FoldLeft<string, Expression>(Agg, init);
        }
    }

    class StringExpandContainsAny : StringArrayExpanderBase
    {
        protected override string MethodName
        {
            get { return "ContainsAny"; }
        }

        protected override Func<Expression, string, Expression> Agg
        {
            get
            {
                return (expr, str) => Expression.Or(expr,
                    Expression.Call(selfRef, contMeth, Expression.Constant(str)));
            }
        }
    }
    
    class StringExpandContainsAll : StringArrayExpanderBase
    {
        protected override string MethodName
        {
            get { return "ContainsAll"; }
        }

        protected override Func<Expression, string, Expression> Agg
        {
            get
            {
                return (expr, str) => Expression.And(expr,
                    Expression.Call(selfRef, contMeth, Expression.Constant(str)));
            }
        }
    }

    #endregion



    #region MethodExpanders to Any Type

    abstract class ArrayExpanderBase : IMethodExpander
    {
        protected MethodInfo contMeth;
        protected Expression selfRef;
        protected abstract string MethodName { get; }
        protected abstract Func<Expression, int, Expression> AggInt { get; }
        protected abstract Func<Expression, Guid, Expression> AggGuid { get; }
        protected abstract Func<Expression, String, Expression> AggString { get; }
        protected abstract Func<Expression, Byte, Expression> AggByte { get; }
        protected abstract Func<Expression, Int16, Expression> AggInt16 { get; }
        protected abstract Func<Expression, Int64, Expression> AggInt64 { get; }

        public Expression Expand(Expression selfRef, IEnumerable<Expression> parameters)
        {
            this.selfRef = selfRef;
            ICollection valsCollection;
            Type itemType = null;

            try
            {
                valsCollection = (ICollection)ExpressionExpander.Evaluate(parameters.First());
                if (valsCollection.Count == 0) throw new Exception("Collection is empty!");
            }
            catch
            {
                throw new ArgumentException(string.Format("First argument for the '{0}' method must be non empty string array!", MethodName));
            }

            foreach (var value in valsCollection)
            {
                itemType = value.GetType();
                break;
            }

            if (itemType != null)
            {
                switch (itemType.FullName)
                {
                    case "System.Int32":

                        int[] valsInt = (int[])ExpressionExpander.Evaluate(parameters.First());
                        // Init parameters
                        contMeth = typeof(int).GetMethods().Where(e => e.Name == "Equals" && e.ToString() == "Boolean Equals(Int32)").FirstOrDefault();

                        // Combine using And when method is ContainsAll or using Or when method is ConainsAny
                        var initInt = Expression.Call(selfRef, contMeth, Expression.Constant(valsInt[0]));
                        return valsInt.Skip(1).FoldLeft<int, Expression>(AggInt, initInt);

                    case "System.Guid":

                        System.Guid[] valsGuid = (System.Guid[])ExpressionExpander.Evaluate(parameters.First());
                        // Init parameters
                        contMeth = typeof(System.Guid).GetMethods().Where(e => e.Name == "Equals" && e.ToString() == "Boolean Equals(System.Guid)").FirstOrDefault();

                        // Combine using And when method is ContainsAll or using Or when method is ConainsAny
                        var initGuid = Expression.Call(selfRef, contMeth, Expression.Constant(valsGuid[0]));
                        return valsGuid.Skip(1).FoldLeft<System.Guid, Expression>(AggGuid, initGuid);


                    case "System.String":

                        System.String[] valsString = (System.String[])ExpressionExpander.Evaluate(parameters.First());
                        // Init parameters
                        contMeth = typeof(System.String).GetMethods().Where(e => e.Name == "Equals" && e.ToString() == "Boolean Equals(System.String)").FirstOrDefault();

                        // Combine using And when method is ContainsAll or using Or when method is ConainsAny
                        var initString = Expression.Call(selfRef, contMeth, Expression.Constant(valsString[0]));
                        return valsString.Skip(1).FoldLeft<System.String, Expression>(AggString, initString);

                    case "System.Byte":

                        System.Byte[] valsByte = (System.Byte[])ExpressionExpander.Evaluate(parameters.First());
                        // Init parameters
                        contMeth = typeof(System.Byte).GetMethods().Where(e => e.Name == "Equals" && e.ToString() == "Boolean Equals(Byte)").FirstOrDefault();

                        // Combine using And when method is ContainsAll or using Or when method is ConainsAny
                        var initByte = Expression.Call(selfRef, contMeth, Expression.Constant(valsByte[0]));
                        return valsByte.Skip(1).FoldLeft<System.Byte, Expression>(AggByte, initByte);

                    case "System.Int16":

                        System.Int16[] valsInt16 = (System.Int16[])ExpressionExpander.Evaluate(parameters.First());
                        // Init parameters
                        contMeth = typeof(System.Int16).GetMethods().Where(e => e.Name == "Equals" && e.ToString() == "Boolean Equals(Int16)").FirstOrDefault();

                        // Combine using And when method is ContainsAll or using Or when method is ConainsAny
                        var initInt16 = Expression.Call(selfRef, contMeth, Expression.Constant(valsInt16[0]));
                        return valsInt16.Skip(1).FoldLeft<System.Int16, Expression>(AggInt16, initInt16);

                    case "System.Int64":

                        System.Int64[] valsInt64 = (System.Int64[])ExpressionExpander.Evaluate(parameters.First());
                        // Init parameters
                        contMeth = typeof(System.Int64).GetMethods().Where(e => e.Name == "Equals" && e.ToString() == "Boolean Equals(Int64)").FirstOrDefault();

                        // Combine using And when method is ContainsAll or using Or when method is ConainsAny
                        var initInt64 = Expression.Call(selfRef, contMeth, Expression.Constant(valsInt64[0]));
                        return valsInt64.Skip(1).FoldLeft<System.Int64, Expression>(AggInt64, initInt64);

                    default:
                        return null;
                }

            }
            else
                return null;
        }
    }

    class ExpanderIn : ArrayExpanderBase
    {
        protected override string MethodName
        {
            get { return "In"; }
        }

        protected override Func<Expression, int, Expression> AggInt
        {
            get
            {
                return (expr, str) => Expression.Or(expr,
                    Expression.Call(selfRef, contMeth, Expression.Constant(str)));
            }
        }

        protected override Func<Expression, Guid, Expression> AggGuid
        {
            get
            {
                return (expr, str) => Expression.Or(expr,
                    Expression.Call(selfRef, contMeth, Expression.Constant(str)));
            }
        }

        protected override Func<Expression, String, Expression> AggString
        {
            get
            {
                return (expr, str) => Expression.Or(expr,
                    Expression.Call(selfRef, contMeth, Expression.Constant(str)));
            }
        }

        protected override Func<Expression, Byte, Expression> AggByte
        {
            get
            {
                return (expr, str) => Expression.Or(expr,
                    Expression.Call(selfRef, contMeth, Expression.Constant(str)));
            }
        }

        protected override Func<Expression, Int16, Expression> AggInt16
        {
            get
            {
                return (expr, str) => Expression.Or(expr,
                    Expression.Call(selfRef, contMeth, Expression.Constant(str)));
            }
        }

        protected override Func<Expression, Int64, Expression> AggInt64
        {
            get
            {
                return (expr, str) => Expression.Or(expr,
                    Expression.Call(selfRef, contMeth, Expression.Constant(str)));
            }
        }

    }


    #endregion

}
