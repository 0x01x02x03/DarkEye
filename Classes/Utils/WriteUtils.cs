using DarkEye.Classes.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkEye.Classes.Utils
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class WriteUtils
    {
        public static void write(Object message)
        {
            String str = " [DEBUG]: " + message;
#if DEBUG
            Debug.WriteLine(DarkEye.DARKEYE_TITLE + str);
#endif
            if(DarkEye.globalLog != null)
                DarkEye.globalLog.debug.Add(str + "\n");
            writeDebug();
        }

        public static void writeWarning(Object message)
        {
            String str = " [DEBUG-WARNING]: " + message;
#if DEBUG
            Debug.WriteLine(DarkEye.DARKEYE_TITLE + str);
#endif
            if (DarkEye.globalLog != null)
                DarkEye.globalLog.debug.Add(str + "\n");
            writeDebug();
        }

        public static void writeError(Object message)
        {
            String str = " [DEBUG-ERROR]: " + message;
#if DEBUG
            Debug.WriteLine(DarkEye.DARKEYE_TITLE + str);
#endif
            if (DarkEye.globalLog != null)
                DarkEye.globalLog.debug.Add(str + "\n");
            writeDebug();
        }

        public static void writeDebug()
        {
            if (DarkEye.globalLog != null && DarkEye.commandManager != null && DarkEye.commandManager.IsWritenCommand("-debug"))
            {
                String content = "";
                foreach (String s in DarkEye.globalLog.debug) content = content + s;
                File.WriteAllText("log_" + DateTime.Now.ToString("MM.dd.yyyy") + ".txt", content);
            }
        }
    }
}
