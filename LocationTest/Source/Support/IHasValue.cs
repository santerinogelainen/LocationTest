using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocationTest.Views.UI;

namespace LocationTest.Support
{
		public interface IHasValue
		{
				MoneyView Gold { get; set; }
				MoneyView Silver { get; set; }
				MoneyView Copper { get; set; }
		}
}