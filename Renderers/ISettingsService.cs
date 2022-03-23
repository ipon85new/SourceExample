
namespace xam.InterfacesIntegration
{
    public interface ISettingsService
    {
        void OpenTab(string url);
        bool IsEnabledNotification();

        bool IsChromeDefaultBrowser();

        bool IsEnabledGPS();

        void OpenLocationSettings();
    }
}
