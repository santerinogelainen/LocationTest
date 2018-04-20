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

    public class Vector2
    {

        public float X { get; set; }
        public float Y { get; set; }

        public Vector2() { }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return String.Format("Vector2 | X: {0}, Y: {1}", X, Y);
        }

        public static implicit operator string(Vector2 vector)
        {
            return vector.ToString();
        }
    }

    /// <summary>
    /// Debug class
    /// </summary>
    public static class D
    {
        /// <summary>
        /// WriteLine
        /// </summary>
        public static void WL(string text, Object caller = null)
        {
            string line = String.Format("{0,0:00}:{1,0:00}:{2,0:00} | ", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (caller != null)
            {
                line += String.Format("{0} | ", caller.GetType().Name);
            }
            line += text;
            System.Diagnostics.Debug.WriteLine(line);
        }
    }

}