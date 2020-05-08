using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Dyalog;
using SimpleAplSample;

namespace AplResourceTestApp {
    class Program {

        static long Prev_WS = 0L;
        static long Init_WS = 0L;
        static long Prev_PB = 0L;
        static long Init_PB = 0L;

        static void Main(string[] args) {

            Console.WriteLine("Press <ENTER> to begin");
            Console.ReadLine();

            for (int i = 0; i < 10; i++) {
                Console.WriteLine("============================< START OF {0:D4} >============================", i);
                var process = Process.GetCurrentProcess();
                var ws = process.WorkingSet64;
                var pb = process.PrivateMemorySize64;
                if (i == 0) {
                    Init_WS = ws;
                    Init_PB = pb;
                }

                ReportResources(process, "Initial");

                var interpreter = new DyalogInterpreter {
                    SingleThreaded = true,
                    DeleteOnUnload = true
                };

                try {
                    var apl = new SimpleAplClass(interpreter);
                    int result = apl.CallWithArgument(15);
                    Console.WriteLine("Result from APL: {0}", result);
                } finally {
                    Console.WriteLine("Successfully unloaded interpreter: {0}", interpreter.Unload());
                    ReportResources(process, "After unload");
                }
                Console.WriteLine();
                //Console.ReadLine();
            }

            Console.WriteLine("=================================< END >=================================");
            Console.WriteLine("Initial WS {0,15:N0} bytes", Init_WS);
            Console.WriteLine("Final WS   {0,15:N0} bytes", Prev_WS);
            Console.WriteLine("Diff WS    {0,15:N0} bytes", Prev_WS - Init_WS);

            Console.WriteLine();

            Console.WriteLine("Initial PB {0,15:N0} bytes", Init_PB);
            Console.WriteLine("Final PB   {0,15:N0} bytes", Prev_PB);
            Console.WriteLine("Diff PB    {0,15:N0} bytes", Prev_PB - Init_PB);

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to exit");
            Console.ReadLine();
        }

        private static void ReportResources(Process process, string text) {
            var ws = process.WorkingSet64;
            var pb = process.PrivateMemorySize64;

            Console.WriteLine("{0,-35} {1,15:N0} b ({2,15:N0} b)", (text + " working set   "), ws, ws - Prev_WS);
            Console.WriteLine("{0,-35} {1,15:N0} b ({2,15:N0} b)", (text + " private bytes "), pb, pb - Prev_PB);

            Prev_WS = ws;
            Prev_PB = pb;

            ReportGDICount(process);
            ReportUserCount(process);
        }

        [DllImport("User32")]
        extern public static int GetGuiResources(IntPtr hProcess, int uiFlags);

        const int GDI_OBJECTS = 0;
        const int USER_OBJECTS = 1;

        public static void ReportGDICount(Process process) {
            Console.WriteLine("GDI handles {0}", GetGuiResources(process.Handle, GDI_OBJECTS));
        }

        public static void ReportUserCount(Process process) {
            Console.WriteLine("USER handles {0}", GetGuiResources(process.Handle, USER_OBJECTS));
        }

    }
}
