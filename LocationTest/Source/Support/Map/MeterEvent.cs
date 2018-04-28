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

namespace LocationTest.Support.Map
{
		public class MeterEvent
		{

				public Action Action { get; set; }
				public int TriggerThreshold { get; set; }
				public float MetersMoved { get; set; }

				/// <summary>
				/// Represents an event that happens every x meters
				/// </summary>
				/// <param name="threshold">meter threshold</param>
				/// <param name="action">event</param>
				public MeterEvent(int threshold, Action action)
				{
						TriggerThreshold = threshold;
						Action = action;
				}

				public void RunAction(float movedbetweenchecks)
				{
						MetersMoved += movedbetweenchecks;
						if (MetersMoved >= TriggerThreshold)
						{
								Action?.Invoke();
								MetersMoved = 0;
						}
				}

		}
}