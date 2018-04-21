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

namespace LocationTest
{
		public interface ITouchable
		{
				// touch positions on start, end and the delta during movement
				Vector2 TouchStart { get; set; }
				Vector2 TouchCurrent { get; set; }
				Vector2 TouchEnd { get; set; }
				Vector2 TouchDelta { get; set; }

				/// <summary>
				/// Override this to create custom touch move events
				/// Access current Touch position with TouchCurrent
				/// </summary>
				/// <param name="e">motionevent data</param>
				void OnTouchMove(MotionEvent e);

				/// <summary>
				/// Override this to create custom touch down events
				/// Access touch starting position with TouchStart
				/// </summary>
				/// <param name="e">motionevent data</param>
				void OnTouchStart(MotionEvent e);

				/// <summary>
				/// Override this to create custom touch up events
				/// Access touch ending position with TouchEnd
				/// </summary>
				/// <param name="e">motionevent data</param>
				void OnTouchEnd(MotionEvent e);

				/// <summary>
				/// Handles all the events
				/// </summary>
				/// <param name="e">motionevent</param>
				/// <returns></returns>
				bool DispatchTouchEvent(MotionEvent e);
		}
}