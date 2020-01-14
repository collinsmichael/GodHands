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
        private object selected = null;

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
            case "DiskTool":  selected = View.disktool;     break;
            case "Database":  selected = View.databasetool; break;
            case "Monitor":   selected = View.monitortool;  break;
            case "Options":   selected = View.configtool;   break;
            case "LogFile":   selected = View.logtool;      break;
            case "PRG":       selected = Model.prgs;        break;
            case "DAT":       selected = Model.dats;        break;
            case "SYD":       selected = Model.syds;        break;
            case "ARM":       selected = Model.arms;        break;
            case "ZND":       selected = Model.znds;        break;
            case "MPD":       selected = Model.mpds;        break;
            case "ZUD":       selected = Model.zuds;        break;
            case "SHP":       selected = Model.shps;        break;
            case "WEP":       selected = Model.weps;        break;
            case "SEQ":       selected = Model.seqs;        break;
            case "Publisher": selected = Publisher.dict;    break;
            case "Undo":      selected = UndoRedo.undo;     break;
            case "Redo":      selected = UndoRedo.redo;     break;
            case "Logger":    selected = Logger.log;        break;
            default:          selected = null;              break;
            }
            property.SelectedObject = selected;
        }
    }
}
