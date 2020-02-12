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
    public static class AppSettings
    {
        public static MySettings Settings = MySettings.Load();

        [Serializable]
        public class MySettings : myAppSettings<MySettings>, INotifyPropertyChanged
        {


            private int height=800;

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

            private int width=1000;
            private int left;
            private int top;
            private WindowState windowState;
            private string theme = "VS2017Dark";


            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public class myAppSettings<T> where T : new()
            {
                private const string DEFAULT_FILENAME = ("\\settings.json");

                public void Save(string fileName = DEFAULT_FILENAME)
                {
                    File.WriteAllText(ClassUtils.PathMyDocument() + fileName,
                        (new JavaScriptSerializer()).Serialize(this));

                }

                public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
                {
                    File.WriteAllText(ClassUtils.PathMyDocument() + fileName,
                        (new JavaScriptSerializer()).Serialize(pSettings));

                }

                public static T Load(string fileName = DEFAULT_FILENAME)
                {
                    T t = new T();
                    if (File.Exists(ClassUtils.PathMyDocument() + fileName))
                    {
                        t = (new JavaScriptSerializer()).Deserialize<T>(
                            File.ReadAllText(ClassUtils.PathMyDocument() + fileName));
                    }
                    else
                    {
                        MySettings st = new MySettings();
                        st.Save();
                    }

                    return t;
                }
            }
        }
    }


