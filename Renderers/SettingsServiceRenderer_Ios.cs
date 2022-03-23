using CoreLocation;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using xam.InterfacesIntegration;
using xam.iOS.CustomRenderers;

[assembly: Dependency(typeof(SettingsServiceRenderer))]
namespace xam.iOS.CustomRenderers
{
    public class SettingsServiceRenderer : ISettingsService

    {
        public bool IsEnabledNotification()
        {
            UIUserNotificationType types = UIApplication.SharedApplication.CurrentUserNotificationSettings.Types;
            if (types.HasFlag(UIUserNotificationType.Alert))
            {
                return true;
            }
            return false;
        }

        public void OpenTab(string url)
        {
            
        }

        public bool IsChromeDefaultBrowser()
        {
            return true;
        }

        public bool IsEnabledGPS()
        {
            bool status = CLLocationManager.LocationServicesEnabled;
            return status;
        }

        public void OpenLocationSettings()
        {
            var url = new NSUrl($"app-settings:");
         
            if (UIApplication.SharedApplication.CanOpenUrl(url))
            {
                UIApplication.SharedApplication.OpenUrl(url);
            }
        }
    }
}