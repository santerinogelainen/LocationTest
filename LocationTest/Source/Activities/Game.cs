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
using Android.Graphics;
using System;

namespace LocationTest.Activities
{
    [Activity(Label = "MapActivity", Theme = "@android:style/Theme.Black.NoTitleBar")]
    public class Game : FragmentActivity, Java.Lang.Thread.IUncaughtExceptionHandler
    {

				Map Map { get; set; }
        Character Character { get; set; }
        HiddenMenu UpgradeMenu { get; set; }
				MoneyView GoldView { get; set; }
				MoneyView SilverView { get; set; }
				MoneyView CopperView { get; set; }

				protected override void OnCreate(Bundle savedInstanceState)
        {
						base.OnCreate(savedInstanceState);
						SetContentView(Resource.Layout.Game);
						LoadViews();
				}

				public void LoadViews()
				{
						Map = FindViewById<Map>(Resource.Id.map);
						Character = FindViewById<Character>(Resource.Id.character);
						UpgradeMenu = FindViewById<HiddenMenu>(Resource.Id.upgrademenu);
						GoldView = FindViewById<MoneyView>(Resource.Id.gold);
						SilverView = FindViewById<MoneyView>(Resource.Id.silver);
						CopperView = FindViewById<MoneyView>(Resource.Id.copper);
						
						// every 50 meters add a copper
						Map.LocationProvider.Every(50, () =>
						{
								CopperView.Add();
						});
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

				public void UncaughtException(Java.Lang.Thread t, Java.Lang.Throwable e)
				{
						new MessageBox(this, "Error: ", e.Message);
				}
		}
}

