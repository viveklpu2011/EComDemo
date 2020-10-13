using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using EComDemo.Dependency;
using EComDemo.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(IShareService))]
namespace EComDemo.Droid
{
    public class IShareService : Activity, IShare
    {
        IImageSourceHandler handler;
        public async void Share(string subject, string message,
        ImageSource imageSource)
        {
            

            var intent = new Intent(Intent.ActionSend);

            intent.SetType("image/jpeg");

            

            if (imageSource is FileImageSource)
            {
                handler = new FileImageSourceHandler();
            }
            else if (imageSource is StreamImageSource)
            {
                handler = new StreamImagesourceHandler(); // sic
            }
            else if (imageSource is UriImageSource)
            {
                handler = new ImageLoaderSourceHandler(); // sic
            }
            else
            {
                 
            }

            var bitmap = await handler.LoadImageAsync(imageSource, CrossCurrentActivity.Current.Activity);

            Java.IO.File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads
                + Java.IO.File.Separator + "MyDiagram.jpg");

            using (System.IO.FileStream os = new System.IO.FileStream(path.AbsolutePath, System.IO.FileMode.Create))
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
            }

            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            intent.AddFlags(ActivityFlags.GrantWriteUriPermission);
            intent.PutExtra(Intent.ExtraStream, FileProvider.GetUriForFile(CrossCurrentActivity.Current.Activity, "com.companyname.ecomdemo.fileprovider", path));

            CrossCurrentActivity.Current.Activity.StartActivity(Intent.CreateChooser(intent, "Share Image"));
        }


    }

    }
 