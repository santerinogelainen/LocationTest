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
using LocationTest.Support;

namespace LocationTest.Views.Support
{
    /// <summary>
    /// This class represents a layout that can have custom touch events on touch move, up and down
    /// This is a subclass of framelayout
    /// </summary>
    public class TouchableLayout : FrameLayout, ITouchable
    {

        // touch positions on start, end and the delta during movement
        public Vector2 TouchStart { get; set; }
        public Vector2 TouchCurrent { get; set; }
        public Vector2 TouchEnd { get; set; }
        public Vector2 TouchDelta { get; set; }

        /// <summary>
        /// This class represents a layout that can have custom touch events on touch move, up and down
        /// This is a subclass of framelayout
        /// </summary>
        /// <param name="c">context</param>
        public TouchableLayout(Context c) : base(c)
        {
            InitTouchEvents();
        }

        /// <summary>
        /// Sets a vector's x and y values with motionevent e.GetX or e.GetY 
        /// </summary>
        /// <param name="vector">vector</param>
        /// <param name="e">motion event</param>
        public void SetVector(Vector2 vector, MotionEvent e)
        {
            vector.X = e.GetX();
            vector.Y = e.GetY();
        }

        /// <summary>
        /// Reset the touchstart
        /// Emulates that a new touch has started
        /// </summary>
        /// <param name="e">motionevent</param>
        public void ResetTouchStart(MotionEvent e)
        {
            SetVector(TouchStart, e);
            OnTouchStart(e);
        }

        /// <summary>
        /// Initializes the touch events
        /// </summary>
        public void InitTouchEvents()
        {
            TouchStart = new Vector2();
            TouchEnd = new Vector2();
            TouchDelta = new Vector2();
            TouchCurrent = new Vector2();
        }

        /// <summary>
        /// Override this to create custom touch move events
        /// Access current Touch position with TouchCurrent
        /// </summary>
        /// <param name="e">motionevent data</param>
        public virtual void OnTouchMove(MotionEvent e) { }

        /// <summary>
        /// Override this to create custom touch down events
        /// Access touch starting position with TouchStart
        /// </summary>
        /// <param name="e">motionevent data</param>
        public virtual void OnTouchStart(MotionEvent e) { }

        /// <summary>
        /// Override this to create custom touch up events
        /// Access touch ending position with TouchEnd
        /// </summary>
        /// <param name="e">motionevent data</param>
        public virtual void OnTouchEnd(MotionEvent e) { }

        /// <summary>
        /// Handles all the events
        /// </summary>
        /// <param name="e">motionevent</param>
        /// <returns></returns>
        public override bool DispatchTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                // on first touch, set the touchstart location
                case MotionEventActions.Down:
                    ResetTouchStart(e);
                    break;
                // on touch up, set the touch end location
                case MotionEventActions.Up:
                    SetVector(TouchEnd, e);
                    OnTouchEnd(e);
                    break;
                // when we move update the delta position and current position
                case MotionEventActions.Move:
                    SetVector(TouchCurrent, e);
                    TouchDelta.X = TouchStart.X - e.GetX();
                    TouchDelta.Y = TouchStart.Y - e.GetY();

                    OnTouchMove(e);
                    break;
            }
            return base.DispatchTouchEvent(e);
        }

    }
}