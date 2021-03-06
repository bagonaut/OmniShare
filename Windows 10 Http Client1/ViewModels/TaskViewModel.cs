﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows_10_Http_Client1.Models;
using Windows_10_Http_Client1.Services;
using Windows_10_Http_Client1.Tools;

namespace Windows_10_Http_Client1.ViewModels
{
    public class TasksViewModel : INotifyPropertyChanged
    {

        private List<TaskModel> _allTaskModels;
        private TaskModel _oneTaskModel = new TaskModel();

        public List<TaskModel> AllTaskModels
        {
            get
            {
                return _allTaskModels;
            }
            set
            {
                _allTaskModels = value;
                OnPropertyChanged();
            }
        }

        public TaskModel OneTaskModel
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

        public ICommand GetTaskModelCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await DownloadTaskModelsAsync();
                });
            }
        }

        public ICommand AddTaskModelCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await AddTaskModelAsync();
                });
            }
        }

        public ICommand DeleteTaskModelCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await DeleteTaskModelAsync();
                });
            }
        }

        public TasksViewModel()
        {
#pragma warning disable
            DownloadTaskModelsAsync();
#pragma warning restore
        }

        private async Task DownloadTaskModelsAsync()
        {
            var taskModelServices = new TaskModelServices();

            AllTaskModels = await taskModelServices.GetTaskModelsAsync();
        }

        private async Task<bool> AddTaskModelAsync()
        {
            var taskModelServices = new TaskModelServices();

            var isTaskModelAdded = await taskModelServices.AddTaskModelAsync(_oneTaskModel);

            return isTaskModelAdded;
        }

        private async Task DeleteTaskModelAsync()
        {
            var taskModelServices = new TaskModelServices();

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
