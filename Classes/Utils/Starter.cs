using System;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace DarkEye.Classes.Utils
{
    class Starter
    {
        //public event EventHandler proc_finished = delegate { };
        //string path;

        //public Starter(String path)
        //{
        //    this.path = path;
        //}

        //public void restart(String[] args)
        //{
        //    this.start(args);
        //    //Environment.Exit(0);
        //}

        //public static void Start(String[] args)
        //{
        //    if (args.Length > 0 && args[0] == "null") return;
        //    Starter starter = new Starter(Utils.GetThisPath());
        //    starter.restart(new String[] { "null" });
        //}

        //public void start(String[] arguments)
        //{
        //    var bw = new BackgroundWorker();
        //    bw.DoWork += (sender, args) =>
        //    {
        //        Process proc = new Process();
        //        proc.StartInfo.FileName = path;
        //        String a = "";
        //        foreach (String arg in arguments)
        //        {
        //            a = a + " " + arg;
        //        }
        //        proc.StartInfo.Arguments = a;
        //        proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //        proc.Start();

        //        Thread.Sleep(1000);

        //        proc.WaitForExit(5000);
        //        if (proc.HasExited == false)
        //            if (proc.Responding)
        //                proc.CloseMainWindow();
        //            else
        //                proc.Kill();
        //    };
        //    bw.RunWorkerCompleted += (sender, args) =>
        //    {
        //        if (args.Error != null) { }
        //        this.proc_finished(this, new EventArgs());

        //    };
        //    bw.RunWorkerAsync();
        //}
    }
}
