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

    }
}
