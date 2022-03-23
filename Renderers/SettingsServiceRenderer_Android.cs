using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using AndroidX.Core.App;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using xam.Droid.CustomRenderers;
using xam.InterfacesIntegration;
using Application = Android.App.Application;
using Uri = Android.Net.Uri;

[assembly:Dependency(typeof(SettingsServiceRenderer))]
namespace xam.Droid.CustomRenderers
{
    public class SettingsServiceRenderer:ISettingsService
    {
        Activity activity { get; set; }
        public SettingsServiceRenderer()
        {
            activity = CrossCurrentActivity.Current.Activity;
        }
        public void OpenTab(string uri)
        {
            return;            
        }

        public bool IsEnabledNotification()
        {
            bool areNotificationsEnabled = NotificationManagerCompat.From(Android.App.Application.Context).AreNotificationsEnabled();
            if (areNotificationsEnabled)
            {
                var notificationChannels = NotificationManagerCompat.From(Android.App.Application.Context).NotificationChannels;
                if(notificationChannels.Any())
                {
                    return notificationChannels.First()
                        .Importance != NotificationImportance.None;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsChromeDefaultBrowser()
        {
            try
            {
                Intent browserIntent = new Intent("android.intent.action.VIEW", Uri.Parse("http://"));  
                ResolveInfo resolveInfo = Application.Context.PackageManager.ResolveActivity(browserIntent,PackageInfoFlags.MatchDefaultOnly);
                return resolveInfo.ActivityInfo.PackageName.Contains("com.android.chrome");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return true;
            }
        }

        public bool IsEnabledGPS()
        {
            LocationManager locationManager = (LocationManager) Application.Context.GetSystemService("location");
            bool enabled = locationManager.IsProviderEnabled(LocationManager.GpsProvider);
         

            return enabled;
        }

        public void OpenLocationSettings()
        {
            try
            {
                Intent intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
