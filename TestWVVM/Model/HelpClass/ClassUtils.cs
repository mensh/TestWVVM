using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;

namespace TestWVVM.Model.HelpClass
{
    public static class ClassUtils
    {
        public static string PathMyDocument()
        {
            try
            {
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
                if (Directory.Exists(md + "\\Test") == false)
                {
                    return Directory.CreateDirectory(md + "\\Test").FullName;
                }
                else
                {
                    return Path.Combine(md, "Test");
                }
            }
            catch (Exception e)
            {
                DXMessageBox.Show(e.Message);
                return string.Empty;
            }
        }

        public static string PathMyDocument(string path)
        {
            try
            {
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
                if (Directory.Exists(md + "\\Test\\" + path) == false)
                {
                    return Directory.CreateDirectory(md + "\\Test\\" + path).FullName;
                }
                else
                {
                    return Path.Combine(md, "Test", path);
                }
            }
            catch (Exception e)
            {
                DXMessageBox.Show(e.Message);
                throw;
            }
        }

        public static bool CreateDirectoryRecursively(string path)
        {
            try
            {
                string[] pathParts = path?.Split('\\');
                for (var i = 0; i < pathParts?.Length; i++)
                {
                    // Correct part for drive letters
                    if (i == 0 && pathParts[i].Contains(":"))
                    {
                        pathParts[i] = pathParts[i] + "\\";
                    } // Do not try to create last part if it has a period (is probably the file name)
                    else if (i == pathParts.Length - 1 && pathParts[i].Contains("."))
                    {
                        return true;
                    }

                    if (i > 0)
                    {
                        pathParts[i] = Path.Combine(pathParts[i - 1], pathParts[i]);
                    }

                    if (!Directory.Exists(pathParts[i]))
                    {
                        Directory.CreateDirectory(pathParts[i]);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                //var recipients = _emailErrorDefaultRecipients;
                //var subject = "ERROR: Failed To Create Directories in " + this.ToString() + " path: " + path;
                //var errorMessage = Error.BuildErrorMessage(ex, subject);
                //Email.SendMail(recipients, subject, errorMessage);
                //Console.WriteLine(errorMessage);
                DXMessageBox.Show(e.Message);
                return false;
            }
        }

        public static bool Bits(int n, int x)
        {
            var b = Convert.ToBoolean((1 << n) & x);
            return b;
        }
    }
}
