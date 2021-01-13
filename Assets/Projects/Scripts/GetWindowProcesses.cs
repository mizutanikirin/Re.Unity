using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace GetWindowProcesses
{
    class Program
    {
        public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lparam);

        private static bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
        {
            int pID;
            GetWindowThreadProcessId(hWnd, out pID);
            _DictHWnd[pID] = hWnd;
            return true;
        }

        private static Dictionary<int, IntPtr> _DictHWnd = new Dictionary<int, IntPtr>();

        [Serializable]
        public struct ActiveApp
        {
            private IntPtr hWnd;
            public IntPtr HWnd
            {
                set
                {
                    _hWnd = value.ToInt32();
                    hWnd = value;
                }
                get { return hWnd; }
            }

            public int pID;
            public string exeName;
            public string exePath;
            public int _hWnd;
        }

        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
            var listApp = new List<ActiveApp>();
            var procs = Process.GetProcesses();
            foreach (var proc in procs)
            {
                if (_DictHWnd.ContainsKey(proc.Id))
                {
                    var app = new ActiveApp() { pID = proc.Id };

                    try
                    {
                        app.exePath = proc.MainModule.FileName;
                        app.exeName = proc.MainModule.ModuleName;
                        app.HWnd = _DictHWnd[proc.Id];
                        listApp.Add(app);
                    }
                    catch
                    {
                    }
                }
            }
            watch.Stop();

            Console.WriteLine($"process time {watch.ElapsedMilliseconds} msec");

            foreach (var app in listApp)
            {
                Console.WriteLine($"pID:{app.pID} hWnd:{app.HWnd} exePath:{app.exePath.Substring(0, 4)}*****");
            }

            Console.ReadKey();
        }
    }
}