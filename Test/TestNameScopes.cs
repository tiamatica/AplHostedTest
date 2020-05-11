using System;
using System.Collections.Generic;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class TestNameScopes {
        [TestMethod]
        public void EchoNoDispose() {
            var interpreterSettings = new Dictionary<string, string> {
                ["RUNASSERVICE"] = "2",
                ["MAXWS"] = "1G"
            };
            for (int i = 0; i < 1000; i++) {
                var interpreter = new DyalogInterpreter(null, interpreterSettings) {
                    SingleThreaded = true,
                    DeleteOnUnload = true
                };

                try {
                    var apl = new AplGroup001.SubSpace.A1(interpreter);
                    var result = apl.Echo(i);
                    Assert.AreEqual(i, result);
                } finally {
                    interpreter.Unload();
                }
            }
        }

        [TestMethod]
        public void EchoWithDispose() {
            var interpreterSettings = new Dictionary<string, string> {
                ["RUNASSERVICE"] = "2",
                ["MAXWS"] = "1G"
            };
            for (int i = 0; i < 1000; i++) {
                var interpreter = new DyalogInterpreter(null, interpreterSettings) {
                    SingleThreaded = true,
                    DeleteOnUnload = true
                };

                try {
                    var apl = new AplGroup001.SubSpace.A1(interpreter);
                    var result = apl.Echo(i);
                    Assert.AreEqual(i, result);
                    apl.Dispose();
                } finally {
                    interpreter.Unload();
                }
            }
        }

        [TestMethod]
        public void NestedInstanceMultiHostNullNull() {
            var interpreter = new DyalogInterpreter(null, null) {
                SingleThreaded = true,
                DeleteOnUnload = true
            };

            try {
                var apl = new AplGroup003.B2(1, interpreter);
                Assert.AreEqual(1, apl.ID());
                Assert.AreEqual(1, apl.B2ID());
                apl.Init();
                Assert.AreEqual(2, apl.B1ID());
                Assert.AreEqual(2, apl.B2ID());
            } finally {
                interpreter.Unload();
            }  
        }

        [TestMethod]
        public void NestedInstanceMultiHostPathNull() {
            var interpreter = new DyalogInterpreter(".\\dyalog180_64_unicode.dll", null) {
                SingleThreaded = true,
                DeleteOnUnload = true
            };

            try {
                var apl = new AplGroup003.B2(1, interpreter);
                Assert.AreEqual(1, apl.ID());
                Assert.AreEqual(1, apl.B2ID());
                apl.Init();
                Assert.AreEqual(2, apl.B1ID());
                Assert.AreEqual(2, apl.B2ID());
            } finally {
                interpreter.Unload();
            }

        }
    }
}
