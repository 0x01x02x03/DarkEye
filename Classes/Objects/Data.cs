using DarkEye.Classes.Utils;
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
    class Data
    {
        private readonly string data;
        private readonly string key;

        public Data(string data, string key)
        {
            this.data = data;
            this.key = key;
        }

        public String GetData()
        {
            String decrypted = CryptUtils.Decrypt(this.data, this.key);
            return decrypted;
        }
        
        public String GetDataByIndex(int index)
        {
            String decrypted = CryptUtils.Decrypt(this.data, this.key);
            try
            {
                return decrypted.Split(':')[index];
            } 
            catch (Exception ex)
            {
                WriteUtils.writeError("GetDataByIndex returned null");
                return null;
            }
        }
    }
}
