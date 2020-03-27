using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkEye.Classes.Objects
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class Command
    {
        public String[] names;
        public bool writen;

        public Command(String[] names)
        {
            this.names = names;
            this.writen = false;
        }
    }
}
