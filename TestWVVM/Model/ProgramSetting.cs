using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using TestWVVM.Annotations;
using TestWVVM.Model.HelpClass;

namespace TestWVVM.Model
{
    [Serializable()]
    public  class MySettings : INotifyPropertyChanged
    {

        private int height = 800;
        private int width = 1000;
        private int left;
        private int top;
        private WindowState windowState;
        private string theme = "VS2017Dark";


        public int Height
        {
            get => height;
            set
            {
                if (value == height) return;
                height = value;
                OnPropertyChanged();
            }
        }

        public int Width
        {
            get => width;
            set
            {
                if (value == width) return;
                width = value;
                OnPropertyChanged();
            }
        }

        public int Left
        {
            get => left;
            set
            {
                if (value == left) return;
                left = value;
                OnPropertyChanged();
            }
        }

        public int Top
        {
            get => top;
            set
            {
                if (value == top) return;
                top = value;
                OnPropertyChanged();
            }
        }

        public WindowState WindowState
        {
            get => windowState;
            set
            {
                if (value == windowState) return;
                windowState = value;
                OnPropertyChanged();
            }
        }

        public string Theme
        {
            get => theme;
            set
            {
                if (value == theme) return;
                theme = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CConfigMng
    {
        private const string DEFAULT_FILENAME = ("\\settings.json");
        private string m_sConfigFileName = ClassUtils.PathMyDocument() + DEFAULT_FILENAME;
        private MySettings m_oConfig = new MySettings();

        public MySettings Config
        {
            get { return m_oConfig; }
            set { m_oConfig = value; }
        }

        // Load configuration file
        public void LoadConfig()
        {
            if (System.IO.File.Exists(m_sConfigFileName))
            {
                System.IO.StreamReader srReader = System.IO.File.OpenText(m_sConfigFileName);
                try
                {
                    
                    Type tType = m_oConfig.GetType();
                    System.Xml.Serialization.XmlSerializer xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                    object oData = xsSerializer.Deserialize(srReader);
                    m_oConfig = (MySettings)oData;
                }
                catch
                {
                    srReader.Close();
                    SaveConfig();
                    srReader = System.IO.File.OpenText(m_sConfigFileName);
                    Type tType = m_oConfig.GetType();
                    System.Xml.Serialization.XmlSerializer xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                    object oData = xsSerializer.Deserialize(srReader);
                    m_oConfig = (MySettings)oData;
                }

              
                srReader.Close();
            }
        }

        // Save configuration file
        public void SaveConfig()
        {
            System.IO.StreamWriter swWriter = System.IO.File.CreateText(m_sConfigFileName);
            Type tType = m_oConfig.GetType();
            if (tType.IsSerializable)
            {
                System.Xml.Serialization.XmlSerializer xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                xsSerializer.Serialize(swWriter, m_oConfig);
                swWriter.Close();
            }
        }
    }

}









