using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Plugin.CurrentActivity;

namespace EComDemo.Droid
{
    [Activity(Label = "EComDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity activity;
        const string permissionCam = Manifest.Permission.Camera;
        const string permissionInt = Manifest.Permission.Internet;
        const string permissionRS = Manifest.Permission.ReadExternalStorage;
        const string permissionWS = Manifest.Permission.WriteExternalStorage;
        const int RequestLocationId = 0;

        readonly string[] permissions =
   {
       Manifest.Permission.Camera,
       Manifest.Permission.Internet,
         Manifest.Permission.ReadExternalStorage,
           Manifest.Permission.WriteExternalStorage,
    };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            GetContactPermissions();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public void GetContactPermissions()
        {



            if (CheckSelfPermission(permissionCam) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }

            if (CheckSelfPermission(permissionInt) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }

            if (CheckSelfPermission(permissionRS) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if (CheckSelfPermission(permissionWS) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
        }
    }
}