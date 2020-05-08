using System;
using System.Collections.Generic;
using AplClasses;
using DotNetClasses;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    [DoNotParallelize]
    public class AplClassesTests {
        [TestMethod]
        public void SetAndGetData() {
            var interpreter = new DyalogInterpreter();
            interpreter.SingleThreaded = true;
            interpreter.DeleteOnUnload = true;
            try {
                using (var test = new ClassWithDispose(interpreter)) {
                    test.MakeBigVar(1000, 1000, "Tralala");
                    var bigvar = test.GetBigVar();
                    var rank = ((object[,])bigvar).Rank;
                    Assert.AreEqual(2, rank);
                }
            } finally {
                interpreter.Unload();
            }
        }
     //   /*
        [TestMethod]
        [ExpectedException(typeof(AplDomainErrorException))]
        public void DivideByZeroTest() {
            var interpreter = new DyalogInterpreter();
            interpreter.SingleThreaded = true;
            interpreter.DeleteOnUnload = true;
            try {
                var test = new Calculator(interpreter);
                var result = test.Divide(4, 0);
            } finally {
                interpreter.Unload();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AplSystemErrorException))]
        public void WSFullTest() {
            Dictionary<string, string> confSettings = new Dictionary<string, string>();
            confSettings.Add("maxws", "512K");

            var interpreter = new DyalogInterpreter(null, confSettings);
            interpreter.SingleThreaded = true;
            interpreter.DeleteOnUnload = true;
            using (var test = new ClassWithDispose(interpreter)) {
                try {
                    test.MakeBigVar(1000, 1000, "Tralala");
                    var bigvar = test.GetBigVar();
                } finally {
                    interpreter.Unload();
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DyalogSyserrorException))]
        public void APLCoreTest() {
            var interpreter = new DyalogInterpreter();
            interpreter.SingleThreaded = true;
            interpreter.DeleteOnUnload = true;
            var test = new TestSystemErrors(interpreter);
            try {
                test.TryCreateAPLCore();
            } finally {
                interpreter.Unload();
            }
        }
      //  */
    }
}
