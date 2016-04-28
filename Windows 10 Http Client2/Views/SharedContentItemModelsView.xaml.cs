using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OmniShareUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskModelsView : Page
    {
        DataTransferManager dataTransferManager;

        public TaskModelsView()
        {
            this.InitializeComponent();
            dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            //request.FailWithDisplayText("cannot even");
            //request.Data = new DataPackage();
            request.Data.SetText(this.textBox.Text);
            request.Data.Properties.ApplicationName = "OmniShare";
            request.Data.Properties.Title = "Clipboard Content";
            request.Data.Properties.Description = "Some content from your pool.";
            

        }

        private void ShowHideSplitViewPane_OnClick(object sender, RoutedEventArgs e)
        {

            TasksSplitView.IsPaneOpen = !TasksSplitView.IsPaneOpen;
        }

        private void GoToAddNewTaskPage_OnClick(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(NewTaskModelView));
        }

        private async void OpenWebsite_OnClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://taskmodel.azurewebsites.net/taskModelsMvc"));
        }

        private void AppBarButton_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;

        }

        private void AppBarButton_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Opacity = 1.0;
        }

        private async void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataPackageView contents = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
            IReadOnlyList<string> types = contents.AvailableFormats;
            foreach (var t in types)
            {
                System.Diagnostics.Debug.WriteLine(t);
            }
            string s = "";
            try
            {
                if (contents.Contains("Text"))
                {
                    s = await contents.GetTextAsync();
                }
                
            }
            catch (Exception) // I'll have to check the format types before doing a get.
            {

                throw;
            }
            this.textBox.Text = s;
            Uri blah;
            if (Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out blah))
            {
                this.textBox.SelectAll();
            } 

        }

        private void shareButton_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }
    }
}
