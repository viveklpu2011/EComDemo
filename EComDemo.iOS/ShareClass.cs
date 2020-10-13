using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EComDemo.Dependency;
using EComDemo.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(ShareClass))]
namespace EComDemo.iOS
{
    public class ShareClass : IShare
    {
        public async void Share(string subject, string message, ImageSource image)
        {
            var handler = new ImageLoaderSourceHandler();
            var uiImage = await handler.LoadImageAsync(image);
            var img = NSObject.FromObject(uiImage);
            var mess = NSObject.FromObject(message);
            var activityItems = new[] { mess, img };
            var activityController = new UIActivityViewController(activityItems, null);
            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }

            topController.PresentViewController(activityController, true, () => { });
        }
    }
}