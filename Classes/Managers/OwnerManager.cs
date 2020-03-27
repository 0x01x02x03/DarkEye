using DarkEye.Classes.Objects;
using DarkEye.Classes.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkEye.Classes.Managers
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class OwnerManager
    {
        public ArrayList owners;

        public OwnerManager()
        {
            owners = new ArrayList();
            WriteUtils.write("Initialization: OwnerManager");
        }

        public void registerOwner(Data data)
        {
            owners.Add(new Owner(data));
            WriteUtils.write("OwnerManager: Registered owner");
        }

        public void unregisterOwner(Data data)
        {
            owners.Remove(new Owner(data));
            WriteUtils.write("OwnerManager: Unregistered owner");
        }
    }
}
