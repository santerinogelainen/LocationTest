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

namespace LocationTest.Support
{
		public class MessageBox : Java.Lang.Object, IDialogInterfaceOnClickListener
		{

				AlertDialog.Builder Builder { get; set; }
				

				public MessageBox(Context context, string title, string message)
				{
						Builder = new AlertDialog.Builder(context);
						Builder.SetMessage(message);
						Builder.SetTitle(title);
						Builder.SetPositiveButton("OK", this);
						Builder.Create().Show();
				}

				public void OnClick(IDialogInterface dialog, int which)
				{

				}
		}
}