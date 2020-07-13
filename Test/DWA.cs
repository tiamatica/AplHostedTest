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
        [ExpectedException(typeof(DyalogSyserrorException))]
        public void ComplexResultSet001() {
            // should throw at some point due to incorrect DWA operations
            var int1 = MultiHostNullNull();
            var apl1 = new AplGroup002.DWA(int1);
            var res1 = apl1.TestComplexResultSet001();
            Assert.IsTrue(res1);
            int1.Unload();
        }
        [TestMethod]
        public void ComplexResultSet002() {
            var int1 = MultiHostNullNull();
            var apl1 = new AplGroup002.DWA(int1);
            var res1 = apl1.TestComplexResultSet002();
            Assert.IsTrue(res1);
            int1.Unload();
        }
        [TestMethod]
        public void ComplexResultSet003() {
            var int1 = MultiHostNullNull();
            var apl1 = new AplGroup002.DWA(int1);
            var res1 = apl1.TestComplexResultSet003();
            Assert.IsTrue(res1);
            int1.Unload();
        }
        [TestMethod]
        public void ComplexResultSet004() {
            var int1 = MultiHostNullNull();
            var apl1 = new AplGroup002.DWA(int1);
            var res1 = apl1.TestComplexResultSet004();
            Assert.IsTrue(res1);
            int1.Unload();
        }
        [TestMethod]
        [ExpectedException(typeof(DyalogSyserrorException))]
        public void ComplexResultSet005() {
            // should throw at some point due to incorrect DWA operations
            var int1 = MultiHostNullNull();
            var apl1 = new AplGroup002.DWA(int1);
            var res1 = apl1.TestComplexResultSet005();
            Assert.IsTrue(res1);
            int1.Unload();
        }
        [TestMethod]
        [Ignore] // this one actually corrupts the stack to such an extent it can't even be run
        [ExpectedException(typeof(DyalogSyserrorException))]
        public void ResizeArray001() {
            // should throw at some point due to incorrect DWA operations
            var int1 = MultiHostNullNull();
            var apl1 = new AplGroup002.DWA(int1);
            try {
                var res1 = apl1.TestResizeArray001();
                Assert.IsTrue(res1);
            } catch (Exception ex) {
                Console.Write(ex);
            }
            int1.Unload();
        }
        [TestMethod]
        public void ResizeArray002() {
            var int1 = MultiHostNullNull();
            var apl1 = new AplGroup002.DWA(int1);
            var res1 = apl1.TestResizeArray002();
            Assert.IsTrue(res1);
            int1.Unload();
        }
    }
}
