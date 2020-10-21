using AplClasses;
using Dyalog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFilePerf {
    class Program {
        static void Main(string[] args) {
            RunAndTrace(true);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static int N = alphabet.Length;

        private static DyalogInterpreter CreateInterpreter(string[] codeFiles = null) {
            var interpreterSettings = new Dictionary<string, string> {
                ["RUNASSERVICE"] = "2",
                ["MAXWS"] = "1G"
            };
            var interpreter = new DyalogInterpreter(null, interpreterSettings) {
                SingleThreaded = true,
                DeleteOnUnload = true,
                UnloadWhenEmpty = false
            };
            if (codeFiles != null) {
                var aplc = new CodeFile(interpreter);
                aplc.AttachCodeFiles(codeFiles);
                aplc.Dispose();
            }
            return interpreter;
        }
        private static void CreateCodeFiles(string file) {
            var codefile = $"{file}.dwx";
            var dwsfile = $"{file}.dws";
            if (File.Exists(codefile)) return;

            Console.WriteLine($"Create missing codefiles: {codefile}.");
            var interpreter = CreateInterpreter();
            var aplc = new CodeFile(interpreter);
            for (var i = 0; i < N; i++) {
                var letter = alphabet.Substring(i, 1);
                Console.WriteLine($"Generating function tree: {letter}.");
                aplc.GenerateTfns(letter);
            }
            Console.WriteLine($"Save to: {codefile}.");
            aplc.CreateCodeFile(codefile, 1);
            Console.WriteLine($"Save to: {dwsfile}.");
            aplc.Dispose();
            Console.WriteLine($"Done creating codefiles.");
            interpreter.Unload();

        }
        public static void RunAndTrace(bool usecodefile = false) {
            var process = Process.GetCurrentProcess();
            var stopwatch = new Stopwatch();
            var fn = "externalcodefile";
            var ext = usecodefile ? ".dwx" : ".dws";
            Console.WriteLine($"Run and trace using {(usecodefile ? "codefile" : "standard dws")} {fn}{ext}.");

            var file = Path.Combine(Path.GetTempPath(), fn);
            var codefile = $"{file}{ext}";

            CreateCodeFiles(file);
            //if (File.Exists(codefile)) File.Delete(codefile);
            //var interpreterc = CreateInterpreter();
            //var aplcc = new CodeFile(interpreterc);
            //aplcc.CreateLargeCache(codefile);
            //aplcc.Dispose();
            //interpreterc.Unload();

            process.Refresh();
            var initialWS = process.WorkingSet64;
            var initialPB = process.PrivateMemorySize64;

            Console.WriteLine($"Create array of {N} interpreters.");
            DyalogInterpreter[] interpreterArray = new DyalogInterpreter[N];
            CodeFile[] aplcArray = new CodeFile[N];
            string[] codeFiles = usecodefile ? new[] { codefile } : null;

            Console.WriteLine($"Interpreter, WS, PB, ElapsedMS");
            Console.WriteLine($" -1,{initialWS,15},{initialPB,15},0");
            for (var i = 0; i < N; i++) {
                stopwatch.Restart();
                var interpreter = CreateInterpreter(codeFiles);
                var aplc = new CodeFile(interpreter);
                interpreterArray[i] = interpreter;
                aplcArray[i] = aplc;
                if (!usecodefile) aplc.LoadWS(codefile);
                stopwatch.Stop();
                process.Refresh();
                Console.WriteLine($"{i,3},{process.WorkingSet64,15},{process.PrivateMemorySize64,15}, {stopwatch.ElapsedMilliseconds}");
            }

            // execute function in each interpreter sequentially
            Console.WriteLine($"Execute different function trees in each interpreter.");
            Console.WriteLine($"Interpreter, WS, PB, ElapsedMS");
            for (var i = 0; i < N; i++) {
                var letter = alphabet.Substring(i, 1);
                stopwatch.Restart();
                aplcArray[i].Execute($"{i} #.A {i}");
                //aplcArray[i].Execute($"+/+/¨#.Cache{letter}Data");
                //aplcArray[i].Execute($"#.Cache{letter}LookUp 1");
                //aplcArray[i].Execute($"#.Cache{letter}LookUp 1");
                //aplcArray[i].Execute($"+/+/¨#.Cache{letter}Data");
                stopwatch.Stop();
                process.Refresh();
                Console.WriteLine($"{i,3},{process.WorkingSet64,15},{process.PrivateMemorySize64,15}, {stopwatch.ElapsedMilliseconds}");
            }
            // check time spent and memory
            process.Refresh();
            var finalWS = process.WorkingSet64;
            var finalPB = process.PrivateMemorySize64;
            var deltaWS_FinalTotal = finalWS - initialWS;
            var deltaPB_FinalTotal = finalPB - initialPB;
            Console.WriteLine($"Delta WS:{deltaWS_FinalTotal}; PB:{deltaPB_FinalTotal}");
            try {
                for (var i = 0; i < N; i++) {
                    aplcArray[i].Dispose();
                    interpreterArray[i].Unload();
                }
            } catch (Exception) { }
        }
    }
}
