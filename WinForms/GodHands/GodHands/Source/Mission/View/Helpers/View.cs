using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public static class View {
        public static DiskTool disktool = null;
        public static DatabaseTool databasetool = null;
        public static MonitorTool monitortool = null;
        public static LogTool logtool = null;
        public static ConfigTool configtool = null;

        private static CompilerParameters parameters = new CompilerParameters();
        private static CSharpCodeProvider provider = new CSharpCodeProvider(
            new Dictionary<String, String>{{ "CompilerVersion","v3.5" }}
        );

        public static Icon IconFromFile(string path) {
            try {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                Bitmap bitmap = new Bitmap(dir+path);
                return Icon.FromHandle(bitmap.GetHicon());
            } catch {
                return null;
            }
        }

        public static Image ImageFromFile(string path) {
            try {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                Image icon = Image.FromFile(dir+path);
                return icon;
            } catch {
                return null;
            }
        }

        public static ImageList ImageListFromDir(string path) {
            try {
                ImageList list = new ImageList();
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                List<string> files = Directory.GetFiles(dir+path).ToList();
                files.Sort();
                foreach(string file in files) {
                    list.Images.Add(Image.FromFile(file));
                }
                return list;
            } catch {
                return null;
            }
        }

        public static Form CompileForm(string path) {
            string name = Path.GetFileNameWithoutExtension(path);
            string code = null;
            try {
                code = File.ReadAllText(path);
            } catch (Exception e) {
                Logger.Fail("File not found! "+e.Message);
                return null;
            }
            parameters.GenerateInMemory = true;
            parameters.ReferencedAssemblies.Add("GodHands.exe");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors) {
                string error = "";
                foreach (CompilerError err in results.Errors) {
                    error = error + err + "\r\n";
                }
                Logger.Fail("Compile Error!\r\n"+error);
                return null;
            }

            Type type = results.CompiledAssembly.GetType("GodHands."+name);
            Form form = (Form)Activator.CreateInstance(type);
            return form;
        }
    }
}
