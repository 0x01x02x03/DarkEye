using DarkEye.Classes.Objects;
using DarkEye.Classes.Utils;
using System;
using System.Collections;
using System.Windows.Forms;

namespace DarkEye.Classes.Managers
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class WindowManager
    {
        public ArrayList windows;

        public WindowManager() { windows = new ArrayList(); }

        public void Init()
        {
            foreach (Owner owner in DarkEye.ownerManager.owners)
                foreach (String target in owner.targets) this.registerWindow(target);
            WriteUtils.write("Initialization: WindowManager");
        }

        public void registerWindow(String name)
        {
            windows.Add(new Window(name));
            WriteUtils.write("WindowManager: Registered window [" + name + "]");
        }

        public void unregisterWindow(Window window)
        {
            windows.Remove(window);
            WriteUtils.write("WindowManager: Unregistered window [" + window.name + "]");
        }

        public void onKeyPressed(object sender, Keys e)
        {
            foreach (Window window in windows)
            {
                if (window.target)
                {
                    window.onKeyPressed(sender, e);
                }
            }
        }
        public void onKeyUnPressed(object sender, Keys e)
        {
            foreach (Window window in windows)
            {
                if (window.target)
                {
                    window.onKeyUnPressed(sender, e);
                }
            }
        }

        public void onMouseClick(object sender, EventArgs e)
        {
            foreach (Window window in windows)
            {
                if (window.target)
                {
                    window.onMouseClick(sender, e);
                }
            }
        }
    }
}
