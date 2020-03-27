using DarkEye.Classes.Objects;
using DarkEye.Classes.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkEye.Classes.Managers
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class DataManager
    {
        public Data ownerData;

        public DataManager()
        {
            this.ownerData = new Data("NULL", "NULL");
            WriteUtils.write("Initialization: DataManager");
        }
    }
}
