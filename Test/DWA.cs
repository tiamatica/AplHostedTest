using System;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class DWA {
        private DyalogInterpreter MultiHostNullNull() {
            return new DyalogInterpreter(null, null) {
                SingleThreaded = true
            };
        }

        [TestMethod]
        public void ComplexResultSet() {
            var int1 = MultiHostNullNull();
            try {
                var apl1 = new AplGroup002.DWA(int1);
                var res1 = apl1.TestComplexResultSet();
                Assert.IsTrue(res1);
            } finally {
                int1.Unload();
            }
        }
    }
}
