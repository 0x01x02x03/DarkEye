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
    class CommandManager
    {
        public ArrayList commands;

        public CommandManager() { commands = new ArrayList(); }

        public void Init(String[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    foreach (String arg in args)
                    {
                        Command command = GetCommandByName(arg);
                        if (command != null) command.writen = true;
                    }
                    WriteUtils.write("CommandManager run with args: " + GetWritenCommands());
                }
                WriteUtils.write("Initialization: CommandManager");
            }
            catch (Exception ex)
            {
                WriteUtils.writeError("Initialization: CommandManager Failed: " + ex.ToString());
            }
        }

        public String GetWritenCommands()
        {
            String cmds = "";
            foreach (Command command in commands)
                if (command.writen) cmds = cmds + command.names[0] + " ";
            return cmds;
        }

        public bool IsWritenCommand(String arg)
        {
            Command command = GetCommandByName(arg);
            return command != null && command.writen;
        }

        public Command GetCommandByName(String arg)
        {
            foreach (Command command in commands)
                foreach(String name in command.names)
                    if (arg.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return command;
            return null;
        }

        public void registerCommand(String arg)
        {
            commands.Add(new Command(new String[] { arg }));
            WriteUtils.write("CommandManager: Registered command [" + arg + "]");
        }

        public void registerCommand(String[] arg)
        {
            commands.Add(new Command(arg));
            WriteUtils.write("CommandManager: Registered command [" + arg + "]");
        }
    }
}
