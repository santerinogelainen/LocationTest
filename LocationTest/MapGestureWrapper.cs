using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LocationTest
{
    public class MapGestureWrapper : FrameLayout
    {

        public float StartBearing { get; set; }
        public float StartTilt { get; set; }

        public Vector2 TouchStart { get; set; }
        public Vector2 TouchEnd { get; set; }
        public Vector2 TouchDelta { get; set; }
        public GoogleMap Map { get; set; }

        public MapGestureWrapper(Context context) : base(context)
        {
            TouchStart = new Vector2();
            TouchEnd = new Vector2();
            TouchDelta = new Vector2();
        }

        public void SetVector(Vector2 vector, MotionEvent e)
        {
            vector.X = e.GetX();
            vector.Y = e.GetY();
        }


        public override bool DispatchTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    SetVector(TouchStart, e);
                    StartBearing = Map.CameraPosition.Bearing;
                    StartTilt = Map.CameraPosition.Tilt;
                    System.Diagnostics.Debug.WriteLine("Start: X: {0}, Y: {1}", TouchStart.X, TouchStart.Y);
                    break;
                case MotionEventActions.Up:
                    SetVector(TouchEnd, e);
                    System.Diagnostics.Debug.WriteLine("End: X: {0}, Y: {1}", TouchEnd.X, TouchEnd.Y);
                    break;
                case MotionEventActions.Move:
                    TouchDelta.X = TouchStart.X - e.GetX();
                    TouchDelta.Y = TouchStart.Y - e.GetY();
                    System.Diagnostics.Debug.WriteLine("Delta: X: {0}, Y: {1}", TouchDelta.X, TouchDelta.Y);

                    CameraPosition.Builder camera = new CameraPosition.Builder(Map.CameraPosition);
                    camera.Bearing(StartBearing + TouchDelta.X / Settings.Gestures.BearingSpeed);

                    if (e.PointerCount == 2)
                    {
                        float tilt = StartTilt - TouchDelta.Y / Settings.Gestures.TiltSpeed;

                        // do not overflow tilt, min 0 max 90
                        if (tilt < 0)
                        {
                            tilt = 0;
                        }
                        if (tilt > 90)
                        {
                            tilt = 90;
                        }
                        camera.Tilt(tilt);
                    }

                    Map.MoveCamera(CameraUpdateFactory.NewCameraPosition(camera.Build()));

                    break;
            }
            return base.DispatchTouchEvent(e);
        }
    }
}