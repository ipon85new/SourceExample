using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xam.Server.RequestModel.InteractiveAccessObjects;

namespace xam.ViewModels.DoorsAndAccessViewModels
{
    public class InteractiveObjectViewModel : BaseViewModel
    {
        public ObservableCollection<InteractiveObject> Doors { get; set; }
        public Command LoadDoors { get; set; }
        public InteractiveObjectViewModel()
        {
            Doors = new ObservableCollection<InteractiveObject>();
            LoadDoors = new Command(() =>
            {
                Task.Run(async () =>
                {
                    var doors = await Server.GetListInteractiveObjects();

                    foreach (var door in doors.Data)
                    {
                        Device.BeginInvokeOnMainThread(() => Doors.Add(door));
                    }

                });
            });
        }
    }
}
