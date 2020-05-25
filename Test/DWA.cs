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
        public void ComplexResultSet001() {
            var int1 = MultiHostNullNull();
            try {
                var apl1 = new AplGroup002.DWA(int1);
                var res1 = apl1.TestComplexResultSet001();
                Assert.IsTrue(res1);
            } finally {
                int1.Unload();
            }
        }
        [TestMethod]
        public void ComplexResultSet002() {
            var int1 = MultiHostNullNull();
            try {
                var apl1 = new AplGroup002.DWA(int1);
                var res1 = apl1.TestComplexResultSet002();
                Assert.IsTrue(res1);
            } finally {
                int1.Unload();
            }
        }
        [TestMethod]
        public void ComplexResultSet003() {
            var int1 = MultiHostNullNull();
            try {
                var apl1 = new AplGroup002.DWA(int1);
                var res1 = apl1.TestComplexResultSet003();
                Assert.IsTrue(res1);
            } finally {
                int1.Unload();
            }
        }
        [TestMethod]
        public void ComplexResultSet004() {
            var int1 = MultiHostNullNull();
            try {
                var apl1 = new AplGroup002.DWA(int1);
                var res1 = apl1.TestComplexResultSet004();
                Assert.IsTrue(res1);
            } finally {
                int1.Unload();
            }
        }
        [TestMethod]
        public void ComplexResultSet005() {
            var int1 = MultiHostNullNull();
            try {
                var apl1 = new AplGroup002.DWA(int1);
                var res1 = apl1.TestComplexResultSet005();
                Assert.IsTrue(res1);
            } finally {
                int1.Unload();
            }
        }
    }
}
