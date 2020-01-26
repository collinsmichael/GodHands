using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class BoundList<T> : IBound {
        private string url;
        private int pos;
        private List<T> list;

        public BoundList(string url, int pos, List<T> list) {
            this.url = url;
            this.pos = pos;
            this.list = list;
        }

        public string GetUrl() {
            return url;
        }

        public string GetText() {
            return "";
        }

        public int GetPos() {
            return pos;
        }

        public void SetPos(int pos) {
            this.pos = pos;
        }

        public int GetLen() {
            return list.Count;
        }
    }

    public class ProgressTimeout {
        public void OnTick(object sender, EventArgs e) {
            Logger.timer.Stop();
            Logger.SetProgress(0);
            Logger.SetStatus("[INFO]", "Idle");
            Logger.timer.Stop();
            Logger.timer.Enabled = false;
        }
    }

    public static class Logger {
        public static bool enabled = true;
        public static List<string> log = new List<string>();
        public static List<ToolStripProgressBar> progress = new List<ToolStripProgressBar>();
        public static List<ToolStripStatusLabel> status = new List<ToolStripStatusLabel>();
        private static BoundList<string> bound = new BoundList<string>("APP:LOG", 0, log);
        private static ProgressTimeout timeout = new ProgressTimeout();
        public static Timer timer = new Timer();
        public static Image[] icons = new Image[4];
        public static Image icon = null;

        public static string message = "Idle";
        public static int percent = 0;

        public static void SetUp() {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            icons[0] = Image.FromFile(dir+"/img/status/status-info.png");
            icons[1] = Image.FromFile(dir+"/img/status/status-pass.png");
            icons[2] = Image.FromFile(dir+"/img/status/status-warn.png");
            icons[3] = Image.FromFile(dir+"/img/status/status-fail.png");
            icon = icons[0];

            Publisher.Register(bound);
            timer.Interval = 5000;
            timer.Tick += new EventHandler(timeout.OnTick);
            timer.Enabled = false;
        }

        // ********************************************************************
        // clear the log
        // ********************************************************************
        public static void Clear() {
            log.Clear();
            Publisher.Publish("APP:LOG", log);
        }

        public static bool AddStatusBar(ToolStripStatusLabel statusbar) {
            if (!status.Contains(statusbar)) {
                status.Add(statusbar);
                statusbar.Text = message;
                statusbar.Image = icon;
                statusbar.Invalidate();
            }
            return true;
        }

        public static bool RemoveStatusBar(ToolStripStatusLabel statusbar) {
            if (status.Contains(statusbar)) {
                status.Remove(statusbar);
            }
            return true;
        }

        public static bool SetStatus(string level, string text) {
            switch (level) {
            case "[INFO]": icon = icons[0]; break;
            case "[PASS]": icon = icons[1]; break;
            case "[WARN]": icon = icons[2]; break;
            case "[FAIL]": icon = icons[3]; break;
            }
            message = text;
            timer.Stop();
            foreach (ToolStripStatusLabel bar in status) {
                bar.Text = text;
                bar.Image = icon;
                bar.Invalidate();
            }
            timer.Start();
            return true;
        }

        public static bool AddProgressBar(ToolStripProgressBar progressbar) {
            if (!progress.Contains(progressbar)) {
                progress.Add(progressbar);
                progressbar.Value = percent;
                progressbar.Invalidate();
            }
            return true;
        }

        public static bool RemoveProgressBar(ToolStripProgressBar progressbar) {
            if (progress.Contains(progressbar)) {
                progress.Remove(progressbar);
            }
            return true;
        }

        public static bool SetProgress(int percent) {
            timer.Stop();
            foreach (ToolStripProgressBar bar in progress) {
                bar.Value = percent;
                bar.Invalidate();
            }
            timer.Start();
            return true;
        }

        // ********************************************************************
        // log messages
        // ********************************************************************
        public static void Format(string level, string text) {
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string msg = level + " (" + now + ") " + text;
            log.Add(msg);
            Publisher.Publish("APP:LOG", log);
            Console.WriteLine(msg);
        }

        // ********************************************************************
        // log informational messages
        // ********************************************************************
        public static bool Info(string text) {
            Format("[INFO]", text);
            return true;
        }

        // ********************************************************************
        // log success messages
        // ********************************************************************
        public static bool Pass(string text) {
            Format("[PASS]", text);
            SetStatus("[PASS]", text);
            return true;
        }

        // ********************************************************************
        // log warning messages
        // ********************************************************************
        public static bool Warn(string text) {
            Format("[WARN]", text);
            SetStatus("[WARN]", text);
            Show(text, "Warning", MessageBoxIcon.Warning);
            return true;
        }

        // ********************************************************************
        // log error messages
        // ********************************************************************
        public static bool Fail(string text) {
            Format("[FAIL]", text);
            SetStatus("[FAIL]", text);
            Show(text, "Error", MessageBoxIcon.Error);
            return false;
        }

        // ********************************************************************
        // display a messagebox
        // ********************************************************************
        public static void Show(string msg, string title, MessageBoxIcon ico) {
            if (enabled) {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(msg, title, buttons, ico);
            }
        }

        public static bool YesNoCancel(string msg) {
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            MessageBoxIcon icon = MessageBoxIcon.Exclamation;
            DialogResult yesno = MessageBox.Show(msg, "Warning", buttons, icon);
            return (yesno == DialogResult.Yes);
        }
    }
}
