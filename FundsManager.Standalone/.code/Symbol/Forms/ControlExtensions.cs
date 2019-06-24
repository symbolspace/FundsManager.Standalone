using System;
using System.Windows.Forms;

namespace Symbol.Forms {

    public static class ControlExtensions {
        public static void ThreadInvoke(/*this */ Control control, ControlThreadInvoke action) {
            if (control.InvokeRequired) {
                //Symbol.Forms.ProgramHelper.ShowCustomInformation("control.InvokeRequired begin");
                control.Invoke(action);
                //Symbol.Forms.ProgramHelper.ShowCustomInformation("control.InvokeRequired end");
            } else {
                action();
            }
        }
        public static void ThreadBeginInvoke(/*this */ Control control, ControlThreadInvoke action) {
            if (control.InvokeRequired) {
                //Symbol.Forms.ProgramHelper.ShowCustomInformation("control.InvokeRequired begin");
                control.BeginInvoke(action);
                //Symbol.Forms.ProgramHelper.ShowCustomInformation("control.InvokeRequired end");
            } else {
                action();
            }
        }

    }
    public delegate void ControlThreadInvoke();
}