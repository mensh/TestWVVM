using System;
using System.ComponentModel;
using System.Resources;
using System.Windows;
using System.Windows.Forms;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using TestWVVM.Model;


[assembly: NeutralResourcesLanguage("ru")]
namespace TestWVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _currentFileLoaded = string.Empty;
        private AppSettings.MySettings programSettings;
        public AppSettings.MySettings ProgramSettings
        {
            get => programSettings;
            set => SetValue(ref programSettings, value);
        }


        public string ApplicationTitle
        {
            get
            {
                var text = "Application Name";
                if (!string.IsNullOrEmpty(_currentFileLoaded))
                {
                    text += $" ({_currentFileLoaded})";
                }

                return text;
            }
        }

        public MainViewModel()
        {
            programSettings = AppSettings.MySettings.Load();
            if (programSettings == null) programSettings= new AppSettings.MySettings();
        }


        [Command]
        public virtual void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

            }
            openFileDialog.Dispose();
        }

        [Command]
        public void Closed(EventArgs e)
        {
            ProgramSettings.Save();
        }

    }
}