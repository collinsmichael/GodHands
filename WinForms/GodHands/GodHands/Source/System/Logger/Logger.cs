using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public static class Logger {
        public static List<string> log = new List<string>();
        public static bool enabled = true;

        // ********************************************************************
        // clear the log
        // ********************************************************************
        public static void Clear() {
            log.Clear();
        }

        // ********************************************************************
        // log messages
        // ********************************************************************
        public static void Format(string level, string text) {
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string msg = level + " (" + now + ") " + text;
            log.Add(msg);
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
            return true;
        }

        // ********************************************************************
        // log warning messages
        // ********************************************************************
        public static bool Warn(string text) {
            Format("[WARN]", text);
            Show(text, "Warning", MessageBoxIcon.Warning);
            return true;
        }

        // ********************************************************************
        // log error messages
        // ********************************************************************
        public static bool Fail(string text) {
            Format("[FAIL]", text);
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
