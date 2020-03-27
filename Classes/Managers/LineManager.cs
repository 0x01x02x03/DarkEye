using DarkEye.Classes.Lines;
using DarkEye.Classes.Objects;
using DarkEye.Classes.Utils;
using System;

namespace DarkEye.Classes.Managers
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class LineManager
    {
        public Line line;

        public LineManager()
        {
            line = GetLine();
            WriteUtils.write("Initialization: LineManager [" + line.name + "]");
        }

        private Line GetLine()
        {
            try
            {
                //return new Online();
            }
            catch (Exception ex)
            {
                return new Offline();
            }
            //todo connecting to server, if exception return offline mode else online
            return new Offline();
        }
    }
}
