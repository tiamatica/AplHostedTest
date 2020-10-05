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
            var aplc = new AplGroup002.ReturnObjects();

            for (int i = 0; i <= 6; i++) {
                stopwatch.Restart();
                var length = 1000 * (int)Math.Pow(2, i);
                var res = aplc.ListToCS(length);
                var elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"Array length: {length}; Elapsed time: {elapsed}ms; Average elapsed time: {elapsed / length}ms");
            }
        }

        [TestMethod]
        public void TestReturnFromCS() {
            var stopwatch = new Stopwatch();
            var interpreter = new Dyalog.DyalogInterpreter(null, null) { SingleThreaded = true };
            var aplc = new AplGroup002.ReturnObjects();

            for (int i = 0; i < 6; i++) {
                stopwatch.Restart();
                var length = 1000 * (int)Math.Pow(2, i);
                aplc.ListFromCS(length);
                var elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"Array length: {length}; Elapsed time: {elapsed}ms; Average elapsed time: {elapsed / length}ms");
            }
        }
        [TestMethod]
        public void TestWithinCS() {
            var stopwatch = new Stopwatch();

            for (int i = 0; i < 6; i++) {
                stopwatch.Restart();
                var length = 1000 * (int)Math.Pow(2, i);
                var res = DotNetClasses.ReturnObjects.ListOfClasses(length);
                var elapsed = stopwatch.ElapsedMilliseconds;
                TestContext.WriteLine($"Array length: {length}; Elapsed time: {elapsed}ms; Average elapsed time: {elapsed / length}ms");
            }
        }
    }
}
