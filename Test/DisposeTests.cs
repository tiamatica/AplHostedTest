﻿using System;
using AplClasses;
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
            using (var test = new NewClass(42, interpreter)) {
                try {
                    var bigvar = test.Number;
                } catch { }
            }
            interpreter.Unload();
        }

        [TestMethod]
        [Description("Check dispose method triggered on derived class.")]
        public void DisposeDerivedClass() {
            var interpreter = GetInterpreter();
            using (var test = new ClassWithDispose(interpreter)) {
                try {
                    test.MakeBigVar(1000, 1000, "Tralala");
                    var bigvar = test.GetBigVar();
                } catch { }
            }
            interpreter.Unload();
        }
    }
}