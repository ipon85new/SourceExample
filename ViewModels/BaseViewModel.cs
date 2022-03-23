using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading.Tasks;
using AiForms.Dialogs;
using AiForms.Dialogs.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using AiForms.Dialogs.Abstractions;
using Xamarin.Forms;
using xam.DialogViews;
using xam.Server;
using xam.Utils;

namespace xam.ViewModels
{
    public class BaseViewModel:INotifyPropertyChanged
    {                
        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }            

        public event PropertyChangedEventHandler PropertyChanged;

        public RestClientMP Server => DependencyService.Get<RestClientMP>(DependencyFetchTarget.NewInstance);

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ShowToast(string title)
        {
            Device.BeginInvokeOnMainThread(async () => Toast.Instance.Show<ToastDialog>(new
            {
                Title = title, Duration = 500, ColorB = Color.Gray,
                ColorT = Color.White
            }));
        }
        
    }
}
