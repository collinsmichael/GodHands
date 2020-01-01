using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class Frame : Form {
        public Actor[] actors = new Actor[] {
            new Actor("John Doe", 121, 101, 130),
            new Actor("Joe Bloggs", 121, 101, 130),
            new Actor("Jane Doe", 112, 111, 150)
        };

        public Frame() {
            InitializeComponent();
            property.SelectedObject = null;
            foreach (Actor actor in actors) {
                listview.Items.Add(actor.Name);
            }
        }

        private void listview_SelectedIndexChanged(object sender, EventArgs e) {
            property.SelectedObject = null;
            if (listview.SelectedItems.Count == 0) return;
            int index = listview.SelectedItems[0].Index;
            property.SelectedObject = actors[index];
        }

        private void property_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            string item = e.ChangedItem.Label;
            if (item == "Name") {
                string name = e.OldValue.ToString();
                if (listview.SelectedItems.Count == 0) return;
                ListViewItem lvi = listview.SelectedItems[0];
                int index = lvi.Index;
                lvi.Text = actors[index].Name;
            }
        }
    }
}
