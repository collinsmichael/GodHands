using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class TestForm : Form, ISubscriber {
        private string key = null;

        public TestForm() {
            InitializeComponent();
            textbox.Text = "/actor/0";
        }

        public bool Notify(object obj) {
            property.SelectedObject = obj;
            return true;
        }

        private void OnClick_Close(object sender, EventArgs e) {
            Close();
        }

        private void OnTextChanged(object sender, EventArgs e) {
            string text = textbox.Text;
            if (key != null) {
                Publisher.Unsubscribe(key, this);
            }
            if (text != null) {
                Publisher.Subscribe(text, this);
            } else {
                property.SelectedObject = null;
            }
            key = text;
        }

        private void OnValueChanged(object s, PropertyValueChangedEventArgs e) {
            //Console.WriteLine(s.ToString());
        }

        private void OnMenuOpen(object sender, EventArgs e) {

        }

        private void OnMenuUndo(object sender, EventArgs e) {
            UndoRedo.Undo();
        }

        private void OnMenuRedo(object sender, EventArgs e) {
            UndoRedo.Redo();
        }
    }
}
