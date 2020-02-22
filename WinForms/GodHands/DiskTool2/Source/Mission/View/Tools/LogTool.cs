using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class LogTool : Form, ISubscriber {
        private SaveFileDialog sfd = new SaveFileDialog();
        private string filter = "";

        public LogTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/menu/tools-logfile-16.png");
            listview.SmallImageList = View.ImageListFromDir("/img/status");
            listview.LargeImageList = View.ImageListFromDir("/img/status");
            ColumnHeader header = new ColumnHeader();
            header.Text = "Log File";
            header.TextAlign = HorizontalAlignment.Left;
            header.Width = this.Width;
            listview.Columns.Add(header);

            Logger.AddStatusBar(statusbar);
            Logger.AddProgressBar(progressbar);
            Publisher.Subscribe("LOG", this);
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            Publisher.Unsubscribe("LOG", this);
            Logger.RemoveStatusBar(statusbar);
            Logger.RemoveProgressBar(progressbar);
            View.logtool = null;
        }

        private void OnMenu_FileSaveAs(object sender, EventArgs e) {
            sfd.Title = "Save CD Image";
            sfd.Filter = "Log files|*.log|Text files|*.txt|All Files|*.*";
            if (sfd.ShowDialog() == DialogResult.OK) {
                string text = "";
                foreach (string str in Logger.log.items) {
                    text = text + str + "\r\n";
                }
                File.WriteAllText(sfd.FileName, text);
            }
        }

        private void OnMenu_FileClose(object sender, EventArgs e) {
            Close();
        }

        private void OnButton_Close(object sender, EventArgs e) {
            Close();
        }

        private void OnButton_Clear(object sender, EventArgs e) {
            string msg = "Are you sure you want to delete the log?";
            if (Logger.YesNoCancel(msg)) {
                Logger.Clear();
            }
        }

        private void OnTextChanged(object sender, EventArgs e) {
            filter = textbox.Text;
            Notify(Logger.log);
        }

        public bool Insert(object obj) { return Notify(obj); }
        public bool Remove(object obj) { return Notify(obj); }
        public bool Notify(object obj) {
            Log log = obj as Log;
            if (log != null) {
                List<ListViewItem> items = new List<ListViewItem>();
                string find = (filter.Length == 0) ? " " : filter.ToUpper();
                foreach (string str in log.items) {
                    if (str.ToUpper().Contains(find)) {
                        int icon = 1;
                        if (str.Contains("[FAIL]")) icon = 0;
                        if (str.Contains("[INFO]")) icon = 1;
                        if (str.Contains("[PASS]")) icon = 2;
                        if (str.Contains("[WARN]")) icon = 3;
                        items.Add(new ListViewItem(str, icon));
                    }
                }
                listview.Items.Clear();
                listview.Items.AddRange(items.ToArray());
            }
            return true;
        }
    }
}
