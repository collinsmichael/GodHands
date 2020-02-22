using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public static partial class Iso9660 {
        public static string ExportDir(Record rec) {
            if (!Iso9660.ReadFile(rec)) {
                return null;
            }

            byte[] data = new byte[rec.LenRecord];
            if (!RamDisk.Get(rec.GetPos(), rec.LenRecord, data)) {
                return null;
            }

            string[] parts = rec.GetUrl().Split(new char[] {':'});
            string dir = AppDomain.CurrentDomain.BaseDirectory+"tmp/";
            string path = (dir+parts[1]).Replace('/', '\\');

            try {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                FileStream file = File.Create(path);
                file.Write(data, 0, rec.LenRecord);
                file.Close();
                return path;
            } catch (Exception e) {
                Logger.Fail("Cannot create file "+path+" "+e.Message);
                return null;
            }
        }

        public static string ExportFile(Record rec) {
            if (!Iso9660.ReadFile(rec)) {
                return null;
            }

            byte[] data = new byte[rec.LenData];
            if (!RamDisk.Get(rec.LbaData*2048, rec.LenData, data)) {
                return null;
            }

            string[] parts = rec.GetUrl().Split(new char[] {':'});
            string dir = AppDomain.CurrentDomain.BaseDirectory+"tmp/";
            string path = (dir+parts[1]).Replace('/', '\\');


            try {
                string folder = Path.GetDirectoryName(path);
                if (Directory.Exists(folder)) {
                    Directory.CreateDirectory(folder);
                    var info = new DirectoryInfo(folder);
                    info.Attributes &= ~FileAttributes.ReadOnly;
                    info.Delete(true);
                }
                if (!Directory.Exists(folder)) {
                    Directory.CreateDirectory(folder);
                }
                FileStream file = File.Create(path);
                file.Write(data, 0, rec.LenData);
                file.Close();
                return path;
            } catch (Exception e) {
                Logger.Fail("Cannot create file "+path+" "+e.Message);
                return null;
            }
        }

        public static bool ImportFiles(Record dir, string[] files) {
            Logger.Info("Dropping files into "+dir.GetFileName());
            foreach (string file in files) {
                Logger.Info("Dropping file "+file);
            }
            return true;
            //if (!Iso9660.ReadFile(rec)) {
            //    return null;
            //}
            //byte[] data = new byte[rec.LenData];
            //if (!RamDisk.Get(rec.LbaData*2048, rec.LenData, data)) {
            //    return null;
            //}
            //string[] parts = rec.GetUrl().Split(new char[] {':'});
            //string dir = AppDomain.CurrentDomain.BaseDirectory+"tmp/";
            //string path = (dir+parts[1]).Replace('/', '\\');
            //try {
            //    Directory.CreateDirectory(Path.GetDirectoryName(path));
            //    FileStream file = File.Create(path);
            //    file.Write(data, 0, rec.LenData);
            //    file.Close();
            //    return path;
            //} catch (Exception e) {
            //    Logger.Fail("Cannot create file "+path+" "+e.Message);
            //    return null;
            //}
        }
    }
}
