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
    public partial class LogTool : Form {
        private SaveFileDialog sfd = new SaveFileDialog();
        private Subscriber_ListView sub_listview = null;

        public LogTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/menu/tools-logfile-16.png");
            listview.SmallImageList = View.ImageListFromDir("/img/status");
            listview.LargeImageList = View.ImageListFromDir("/img/status");
            ColumnHeader header = new ColumnHeader();
            header.Text = "Log File";
            header.TextAlign = HorizontalAlignment.Left;
            header.Width = -1;
            listview.Columns.Add(header);

            Logger.AddStatusBar(statusbar);
            Logger.AddProgressBar(progressbar);
            sub_listview = new Subscriber_ListView("APP:LOG", listview);
            sub_listview.Notify(Logger.log);
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            Logger.RemoveStatusBar(statusbar);
            Logger.RemoveProgressBar(progressbar);
            View.logtool = null;
        }

        private void OnMenu_FileSaveAs(object sender, EventArgs e) {
            sfd.Title = "Save CD Image";
            sfd.Filter = "Log files|*.log|Text files|*.txt|All Files|*.*";
            if (sfd.ShowDialog() == DialogResult.OK) {
                string text = "";
                foreach (string str in Logger.log) {
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
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            string title = "Delete Log?";
            string msg = "Are you sure you want to delete the log?";
            if (MessageBox.Show(msg, title, buttons) == DialogResult.Yes) {
                Logger.Clear();
            }
        }

        private void OnTextChanged(object sender, EventArgs e) {
            sub_listview.SetFilter(textbox.Text);
        }
    }
}
