using System;
using AplClasses;
using AplDisposable;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class DisposeTests {
        private DyalogInterpreter GetInterpreter() {
            return new DyalogInterpreter() {
                SingleThreaded = true,
                DeleteOnUnload = true,
                UnloadWhenEmpty = false
            };
        }

        [TestMethod]
        [Description("Check dispose method triggered simple class.")]
        public void DisposeSimpleClass() {
            var interpreter = GetInterpreter();
            using (var test = new AplDisposableAplBaseNonDisposable(interpreter)) {
            }
            interpreter.Unload();
        }

        //[TestMethod]
        //[Description("Check dispose method triggered on derived class.")]
        //public void DisposeDerivedClass() {
        //    var interpreter = GetInterpreter();
        //    using (var test = new (interpreter)) {
        //        try {
        //        } catch { }
        //    }
        //    interpreter.Unload();
        //}

    }
}
