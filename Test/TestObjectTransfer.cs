using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class TestObjectTransfer {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void TestReturnToCS() {
            var stopwatch = new Stopwatch();
            var interpreter = new Dyalog.DyalogInterpreter(null, null) { SingleThreaded = true };
            var aplc = new AplGroup002.ReturnObjects(interpreter);

            TestContext.WriteLine($"Length, ElapsedTime, ElapsedTimeAverage");
            for (int i = 0; i < 6; i++) {
                var length = 1000 * (int)Math.Pow(2, i);
                stopwatch.Restart();
                var res = aplc.ListToCS(length);
                double elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"{length,10},{elapsed,10},{elapsed / length:0.####}");
            }
        }

        [TestMethod]
        public void TestReturnFromCS() {
            var stopwatch = new Stopwatch();
            var interpreter = new Dyalog.DyalogInterpreter(null, null) { SingleThreaded = true };
            var aplc = new AplGroup002.ReturnObjects(interpreter);

            TestContext.WriteLine($"Length, ElapsedTime, ElapsedTimeAverage");
            for (int i = 0; i < 6; i++) {
                var length = 1000 * (int)Math.Pow(2, i);
                stopwatch.Restart();
                aplc.ListFromCS(length);
                double elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"{length,10},{elapsed,10},{elapsed / length:0.####}");
            }
        }
        [TestMethod]
        public void TestWithinCS() {
            var stopwatch = new Stopwatch();

            TestContext.WriteLine($"Length, ElapsedTime, ElapsedTimeAverage");
            for (int i = 0; i < 6; i++) {
                var length = 1000 * (int)Math.Pow(2, i);
                stopwatch.Restart();
                var res = DotNetClasses.ReturnObjects.ListOfClasses(length);
                double elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"{length,10},{elapsed,10},{elapsed / length:0.####}");
            }
        }
        [TestMethod]
        public void TestInvokingAplProperty() {
            var stopwatch = new Stopwatch();
            var interpreter = new Dyalog.DyalogInterpreter(null, null) { SingleThreaded = true };
            var aplc = new AplGroup002.ReturnObjects(interpreter);

            TestContext.WriteLine($"Step, Length, ElapsedTime, ElapsedTimeAverage");
            for (int i = 0; i < 17; i++) {
                var length = 10 * (int)Math.Pow(2, i % 8);
                var aplinstances = new AplGroup002.ReturnObjects[length];

                stopwatch.Restart();
                for (int j = 0; j < length; j++) {
                    aplinstances[j] = new AplGroup002.ReturnObjects(interpreter);
                }
                double elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"Create,{length,10},{elapsed,10},{elapsed / length:0.####}");

                stopwatch.Restart();
                for (int j = 0; j < length; j++) {
                    aplinstances[j].Name = j.ToString();
                }
                elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"SetProperty,{length,10},{elapsed,10},{elapsed / length:0.####}");

                stopwatch.Restart();
                for (int j = 0; j < length; j++) {
                    var name = aplinstances[j].Name;
                }
                elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"GetProperty,{length,10},{elapsed,10},{elapsed / length:0.####}");

                stopwatch.Restart();
                for (int j = 0; j < length; j++) {
                    var res = aplinstances[j].CallInt(j);
                }
                elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"CallInt,{length,10},{elapsed,10},{elapsed / length:0.####}");

                var obj = new DotNetClasses.DataItemClass();
                stopwatch.Restart();
                for (int j = 0; j < length; j++) {
                    var res = aplinstances[j].CallNetObject(obj);
                }
                elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"CallNetObject,{length,10},{elapsed,10},{elapsed / length:0.####}");

            }
        }
    }
}
