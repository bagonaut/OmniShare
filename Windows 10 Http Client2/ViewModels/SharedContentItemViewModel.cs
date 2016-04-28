using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using OmniShareUWP.Models;
using OmniShareUWP.Services;
using OmniShareUWP.Tools;

namespace OmniShareUWP.ViewModels
{
    public class SharedContentItemModel : INotifyPropertyChanged
    {

        private List<SharedContenItemtModel> _allSharedContentItemModels;
        private SharedContenItemtModel _oneTaskModel = new SharedContenItemtModel();

        public List<SharedContenItemtModel> AllSharedContentItemModels
        {
            get
            {
                return _allSharedContentItemModels;
            }
            set
            {
                _allSharedContentItemModels = value;
                OnPropertyChanged();
            }
        }

        public SharedContenItemtModel OneSharedContentItemModel
        {
            get
            {
                return _oneTaskModel;
            }
            set
            {
                _oneTaskModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetSharedContentItemModelCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await DownloadSharedContentItemModelsAsync();
                });
            }
        }

        public ICommand AddSharedContentItemModelCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await AddTSharedContentItemModelAsync();
                });
            }
        }

        public ICommand DeleteSharedContentItemModelCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await DeleteSharedContentItemModelAsync();
                });
            }
        }

        public SharedContentItemModel()
        {
#pragma warning disable
            DownloadSharedContentItemModelsAsync();
#pragma warning restore
        }

        private async Task DownloadSharedContentItemModelsAsync()
        {
            var taskModelServices = new SharedContentItemModelServices();

            AllSharedContentItemModels = await taskModelServices.GetTaskModelsAsync();
        }

        private async Task<bool> AddTSharedContentItemModelAsync()
        {
            var taskModelServices = new SharedContentItemModelServices();

            var isTaskModelAdded = await taskModelServices.AddTaskModelAsync(_oneTaskModel);

            return isTaskModelAdded;
        }

        private async Task DeleteSharedContentItemModelAsync()
        {
            var taskModelServices = new SharedContentItemModelServices();

            await taskModelServices.DeleteTaskModelAsync(_oneTaskModel);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
