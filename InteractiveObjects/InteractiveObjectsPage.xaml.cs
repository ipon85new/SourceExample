using AiForms.Dialogs;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xam.DialogViews;
using xam.InterfacesIntegration;
using xam.Server;
using xam.RequestModel;
using xam.Utils;
using xam.ViewModels;

namespace xam.InteractiveObjects
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InteractiveObjectsPage : ContentPage
    {
        InteractiveObjectViewModel viewModel;
        RestClientMP _server = new RestClientMP();

        public InteractiveObjectsPage()
        {
            InitializeComponent();
            Analytics.TrackEvent("Список интерактивных объектов: дверей, домофонов ... ");
            BindingContext = viewModel = new InteractiveObjectViewModel();
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    int statusBarHeight = DependencyService.Get<IStatusBar>().GetHeight();
                    break;
                default:
                    break;
            }
            MessagingCenter.Subscribe<HeaderViewStack>(this, "GoBack", async sender =>
            {
                if (Application.Current.MainPage.Navigation.ModalStack.Count > 1)
                {
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await Navigation.PopAsync();
                }
            });
            viewModel.LoadDoors.Execute(null);

            UkName.Text = Settings.MobileSettings.main_name;
        }
        
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {                
                try
                {
                    var stack = (StackLayout)sender;
                    var interActivObj = (InteractiveObject)stack.BindingContext;

                    {
                        var openResult = await _server.InteractiveaccessobjectActivate(interActivObj.ID);
                        if (string.IsNullOrWhiteSpace(openResult.Error))
                        {
                            Analytics.TrackEvent(openResult.Error);
#if DEBUG
                            await Dialog.Instance.ShowAsync<AlertView>(new { textMessage = AppResources.ObjectNotOpened });
#endif
                        }
                        else
                        {
                            await Dialog.Instance.ShowAsync<AlertView>(new { textMessage = AppResources.DoorOpened });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}