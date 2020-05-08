using System;
using Dyalog;
using AplClasses;
using DotNetClasses;

namespace MultiInstanceNet {
    class Program {        
        static void Main(string[] args) {
            // long result;
            // Stopwatch stop;

            var interpreter = new DyalogInterpreter();
            interpreter.SingleThreaded = true;
            interpreter.DeleteOnUnload = true;

            var calc = new Calculator(interpreter);
            // Test good old exit specification (xs)
            // calc.SetStopThis("Sum");
            try {
                var resultUpper = calc.Upper("TestSystemErrors");
                var result = calc.Sum(new int[0]);
            } catch (AplExitSpecificationException ex) {
                Console.WriteLine("Exception: " + ex.Message);
            }

            // Test Dyalog APL system error - DOMAIN ERROR
            try {
                var result2 = calc.Divide(4, 0);
            } catch (AplDomainErrorException ex) {
                Console.WriteLine("Exception: " + ex.Message);
            }

            var interpreter2 = new DyalogInterpreter();
            interpreter2.SingleThreaded = true;

            using (var test = new ClassWithDispose(interpreter)) {
                // test.SetStopThis("Divide");
                try {
                    var result = test.Divide(4, 0);
                } catch (AplDomainErrorException ex) {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
            var test2 = new ClassWithDispose(interpreter2);
            // test.SetStopThis("MakeBigVar");
            var newClass = test2.GetNewInstance(99);
            Console.WriteLine("[]WA: " + test2.WsAwail());
            //test2.ExecuteExpr("#.BigVar←1000 1000⍴⊂'Trala'");
            //var bigvar = test2.ExecuteExprWithResult("#.BigVar");
            //test2.ExecuteExpr("⎕EX'#.BigVar'");
            test2.MakeBigVar(1000, 1000, "Tralala");

            interpreter.Unload();
            (test2 as IDisposable)?.Dispose();
            interpreter2.Unload();
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();            
        }
    }
}
