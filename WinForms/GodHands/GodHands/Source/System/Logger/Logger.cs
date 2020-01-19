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

        public int GetPos() {
            return pos;
        }

        public void SetPos(int pos) {
            this.pos = pos;
        }
    }

    public class ProgressTimeout {
        public void OnTick(object sender, EventArgs e) {
            Logger.timer.Stop();
            Logger.timer.Enabled = false;
            Logger.SetProgress(0);
            Logger.SetStatus("[INFO]", "Idle");
        }
    }

    public static class Logger {
        public static bool enabled = true;
        public static List<string> log = new List<string>();
        public static List<ToolStripProgressBar> progress = new List<ToolStripProgressBar>();
        public static List<ToolStripStatusLabel> status = new List<ToolStripStatusLabel>();
        public static List<ToolStripStatusLabel> icons = new List<ToolStripStatusLabel>();
        private static BoundList<string> bound = new BoundList<string>("APP:LOG", 0, log);
        private static ProgressTimeout timeout = new ProgressTimeout();
        public static Timer timer = new Timer();

        public static void SetUp() {
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
            Image icon = null;
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            switch (level) {
            case "[INFO]": icon = Image.FromFile(dir+"/img/status-info.png"); break;
            case "[PASS]": icon = Image.FromFile(dir+"/img/status-pass.png"); break;
            case "[WARN]": icon = Image.FromFile(dir+"/img/status-warn.png"); break;
            case "[FAIL]": icon = Image.FromFile(dir+"/img/status-fail.png"); break;
            }
            foreach (ToolStripStatusLabel bar in status) {
                bar.Text = text;
                bar.Image = icon;
            }
            return true;
        }

        public static bool AddProgressBar(ToolStripProgressBar progressbar) {
            if (!progress.Contains(progressbar)) {
                progress.Add(progressbar);
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
    }
}
