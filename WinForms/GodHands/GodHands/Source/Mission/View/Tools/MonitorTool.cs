using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class MonitorTool : Form {
        public MonitorTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/tools-monitor-16.png");
            ShellIcons.GetShellIcons(treeview);

            int iso_normal = ShellIcons.GetFileIconIndex("test.iso");
            int dir_normal = ShellIcons.GetDirIconIndex(false);
            int dir_select = ShellIcons.GetDirIconIndex(true);
            int bin_normal = ShellIcons.GetFileIconIndex("test.bin");
            TreeNode root = treeview.Nodes.Add("GodHands", "GodHands", iso_normal, iso_normal);
            TreeNode view = root.Nodes.Add("GodHands/View", "View", dir_normal, dir_select);
            view.Nodes.Add("GodHands/View/DiskTool", "DiskTool", bin_normal, bin_normal);
            view.Nodes.Add("GodHands/View/Database", "Database", bin_normal, bin_normal);
            view.Nodes.Add("GodHands/View/Monitor", "Monitor", bin_normal, bin_normal);
            view.Nodes.Add("GodHands/View/Options", "Options", bin_normal, bin_normal);
            view.Nodes.Add("GodHands/View/LogFile", "LogFile", bin_normal, bin_normal);

            TreeNode model = root.Nodes.Add("GodHands/Model", "Model", dir_normal, dir_select);
            TreeNode model_disk = model.Nodes.Add("GodHands/Model/OnDisk", "OnDisk", dir_normal, dir_select);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/PRG", "PRG", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/DAT", "DAT", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/SYD", "SYD", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/ARM", "ARM", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/ZND", "ZND", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/MPD", "MPD", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/ZUD", "ZUD", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/SHP", "SHP", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/WEP", "WEP", bin_normal, bin_normal);
            model_disk.Nodes.Add("GodHands/Model/OnDisk/SEQ", "SEQ", bin_normal, bin_normal);
            TreeNode model_mem = model.Nodes.Add("GodHands/Model/InMemory", "InMemory", dir_normal, dir_select);
            model_mem.Nodes.Add("GodHands/Model/InMemory/Zones", "Zones", bin_normal, bin_normal);
            model_mem.Nodes.Add("GodHands/Model/InMemory/Rooms", "Rooms", bin_normal, bin_normal);
            model_mem.Nodes.Add("GodHands/Model/InMemory/Actors", "Actors", bin_normal, bin_normal);
            model_mem.Nodes.Add("GodHands/Model/InMemory/MiniMaps", "MiniMaps", bin_normal, bin_normal);

            TreeNode system = root.Nodes.Add("GodHands/System", "System", dir_normal, dir_select);
            system.Nodes.Add("GodHands/System/Logger", "Logger", bin_normal, bin_normal);
            system.Nodes.Add("GodHands/System/Config", "Config", bin_normal, bin_normal);
            system.Nodes.Add("GodHands/System/Publisher", "Publisher", bin_normal, bin_normal);

            TreeNode undoredo = system.Nodes.Add("GodHands/System/UndoRedo", "UndoRedo", dir_normal, dir_select);
            undoredo.Nodes.Add("GodHands/System/UndoRedo/Undo", "Undo", bin_normal, bin_normal);
            undoredo.Nodes.Add("GodHands/System/UndoRedo/Redo", "Redo", bin_normal, bin_normal);
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            View.monitortool = null;
        }

        private void OnTreeViewSelect(object sender, TreeViewEventArgs e) {
            TreeNode node = treeview.SelectedNode;
            switch (node.Text) {
            case "DiskTool":  property.SelectedObject = View.disktool;     break;
            case "Database":  property.SelectedObject = View.databasetool; break;
            case "Monitor":   property.SelectedObject = View.monitortool;  break;
            case "Options":   property.SelectedObject = View.configtool;   break;
            case "LogFile":   property.SelectedObject = View.logtool;      break;
            case "PRG":       property.SelectedObject = Model.prgs;        break;
            case "DAT":       property.SelectedObject = Model.dats;        break;
            case "SYD":       property.SelectedObject = Model.syds;        break;
            case "ARM":       property.SelectedObject = Model.arms;        break;
            case "ZND":       property.SelectedObject = Model.znds;        break;
            case "MPD":       property.SelectedObject = Model.mpds;        break;
            case "ZUD":       property.SelectedObject = Model.zuds;        break;
            case "SHP":       property.SelectedObject = Model.shps;        break;
            case "WEP":       property.SelectedObject = Model.weps;        break;
            case "SEQ":       property.SelectedObject = Model.seqs;        break;
            case "Publisher": property.SelectedObject = Publisher.dict;    break;
            case "Undo":      property.SelectedObject = UndoRedo.undo;     break;
            case "Redo":      property.SelectedObject = UndoRedo.redo;     break;
            case "Logger":    property.SelectedObject = Logger.log;        break;
            default:          property.SelectedObject = null;              break;
            }
        }
    }
}
