using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Common;
using Android.Util;
using Android.Support.V4.App;
using Android.Views;
using LocationTest.Support;
using LocationTest.Views.UI;
using LocationTest.Views.Map;
using Android.Animation;
using Android.Locations;

namespace LocationTest.Activities
{
    [Activity(Label = "MapActivity")]
    public class MapActivity : FragmentActivity
    {

        Map Map { get; set; }
        Character Character { get; set; }
        HiddenMenu UpgradeMenu { get; set; }

				protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MapActivity);

						Map = FindViewById<Map>(Resource.Id.map);
            Character = FindViewById<Character>(Resource.Id.character);
						UpgradeMenu = FindViewById<HiddenMenu>(Resource.Id.upgrademenu);
				}

				/// <summary>
				/// Show a hiddenmenu
				/// Make sure that the calling view has a tag with the id name of the hiddenmenu
				/// </summary>
				[Java.Interop.Export("ShowMenu")]
				public void ShowMenu(View v)
				{
						View menu = ActivityHelper.FindViewByIdName(this, (string)v.Tag);
						if (menu is HiddenMenu && menu != null)
						{
								D.WL("show menu");
								((HiddenMenu)menu).Show();
						}
				}

				/// <summary>
				/// Hide a hiddenmenu
				/// Make sure that the calling view has a tag with the id name of the hiddenmenu
				/// </summary>
				[Java.Interop.Export("HideMenu")]
				public void HideMenu(View v)
				{
						View menu = ActivityHelper.FindViewByIdName(this, (string)v.Tag);
						if (menu is HiddenMenu && menu != null)
						{
								D.WL("hide menu");
								((HiddenMenu)menu).Hide();
						}
				}

		}
}

