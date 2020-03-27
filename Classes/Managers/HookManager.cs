using DarkEye.Classes.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkEye.Classes.Managers
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class HookManager
    {
        public HookManager() { }

        public void Init()
        {
            try
            {
                KeyboardHook keyboardHook = new KeyboardHook();
                MouseHook mouseHook = new MouseHook();

                keyboardHook.OnKeyPressed += onKeyPressed;
                keyboardHook.OnKeyUnpressed += onKeyUnPressed;
                keyboardHook.Hook();

                mouseHook.MouseAction += onMouseClick;
                mouseHook.Hook();

                Application.Run();

                keyboardHook.UnHook();
                mouseHook.UnHook();
                WriteUtils.write("Initialization: HookManager");
            }
            catch (Exception ex)
            {
                WriteUtils.writeError("Initialization: HookManager Failed: " + ex.ToString());
            }
        }

        void onKeyPressed(object sender, Keys e)
        {
            DarkEye.windowManager.onKeyPressed(sender, e);
        }

        void onKeyUnPressed(object sender, Keys e)
        {
            DarkEye.windowManager.onKeyUnPressed(sender, e);
        }

        void onMouseClick(object sender, EventArgs e)
        {
            DarkEye.windowManager.onMouseClick(sender, e);
        }
    }
}
