using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LocationTest.Support
{
		/// <summary>
		/// Reads attributes from xml
		/// </summary>
		public class AttributeReader
		{
				Context Context { get; set; }
				int[] PossibleAttributes { get; set; }
				IAttributeSet Attributes { get; set; }

				public AttributeReader(Context context, int[] possibleattrs, IAttributeSet attrs)
				{
						Context = context;
						PossibleAttributes = possibleattrs;
						Attributes = attrs;
				}

				/// <summary>
				/// Wrapper for all Get[Type](int id) call. Note! You do not need to check if the array has the value. It is done in this wrapper method.
				/// </summary>
				/// <param name="e">Type specific implementation of getting the value</param>
				private void GetValue(int id, Action<TypedArray> e)
				{
						TypedArray array = Context.Theme.ObtainStyledAttributes(Attributes, PossibleAttributes, 0, 0);
						if (array.HasValue(id))
						{
								e?.Invoke(array);
						}
						array.Recycle();
				}

				/// <summary>
				/// Get a string from the attributeset
				/// </summary>
				/// <param name="id">attribute id</param>
				/// <returns>the string or empty string if not found</returns>
				public string GetString(int id)
				{
						string result = "";
						GetValue(id, (array) => {
								result = array.GetString(id);
						});
						return result;
				}

				/// <summary>
				/// Get a resource id from the attribute set
				/// </summary>
				/// <param name="id">attribute id</param>
				/// <returns>resource id or -1 if not found</returns>
				public int GetResourceId(int id)
				{
						int result = -1;
						GetValue(id, (array) => {
								result = array.GetResourceId(id, -1);
						});
						return result;
				}

				/// <summary>
				/// Get an integer from the attribute set
				/// </summary>
				/// <param name="id">attribute id</param>
				/// <returns>the integer or -1 if not found</returns>
				public int GetInteger(int id, int defValue = -1)
				{
						int result = defValue;
						GetValue(id, (array) => {
								result = array.GetInteger(id, defValue);
						});
						return result;
				}

		}
}