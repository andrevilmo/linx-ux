using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linx.LinqExtensions.Functional
{
	/// <summary>
	/// Class representing a delayed computation, which calculates
	/// some C# expression (given as a delegate) only when the result
	/// is acutally accessed via the Value property
	/// </summary>
	public class Lazy<T>
	{
		private Func<T> func;
		private T result;
		private bool hasValue;

		/// <summary>
		/// Creates a lazy value using a given delegate
		/// </summary>
		public Lazy(Func<T> func)
		{
			this.func = func;
			this.hasValue = false;
		}

		/// <summary>
		/// Forces evaluation of the expression, calculates the result 
		/// if it wasn't calculated earlier and returns it.
		/// </summary>
		public T Value
		{
			get
			{
				if (!this.hasValue)
				{ this.result = this.func(); this.hasValue = true; }
				return this.result;
			}
		}
	}


	/// <summary>
	/// Utility class to allow using type inferrence
	/// </summary>
	public static class Lazy
	{
		/// <summary>
		/// Creates a lazy value using the given delegate
		/// </summary>
		public static Lazy<T> New<T>(Func<T> func)
		{
			return new Lazy<T>(func);
		}
	}
}
